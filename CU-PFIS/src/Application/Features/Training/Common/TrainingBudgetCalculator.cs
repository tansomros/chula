using Chula.Application.Common.Interfaces;
using Chula.Domain.Common;
using Chula.Domain.Entities.Training;
using Chula.Domain.Enums.Training;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.Common;

public sealed record FacultyFiscalYearBalance(
    int FiscalYearBe,
    decimal AnnualQuota,
    decimal ConsumedInYear,
    decimal CarryForwardDeductedInYear,
    decimal AdjustmentsInYear,
    decimal BorrowedFromNextYear,
    decimal RemainingInYear,
    bool AllowFutureQuotaBorrowing,
    decimal MaxBorrowAmount,
    decimal NextYearRemainingBeforeBorrow,
    decimal BorrowableFromNextYear,
    bool HasCustomAllocation = false,
    string? AllocationNote = null)
{
    public decimal TotalApprovedSpendInYear => ConsumedInYear + BorrowedFromNextYear;

    public decimal TotalAvailableIncludingBorrow => Math.Max(0m, RemainingInYear) + BorrowableFromNextYear;
}

/// <summary>
/// Single source of truth for deriving faculty training budget balances and generating append-only ledger entries
/// (including Per-Faculty Quota Overrides and Future Fiscal-Year Quota Borrowing per Decisions A05 &amp; A06).
/// </summary>
public static class TrainingBudgetCalculator
{
    public static FacultyFiscalYearBalance ComputeBalance(
        int fiscalYearBe,
        TrainingBudgetPolicy currentPolicy,
        TrainingBudgetPolicy nextPolicy,
        IReadOnlyCollection<TrainingBudgetLedgerEntry> facultyLedgerEntries,
        FacultyTrainingBudgetAllocation? currentAllocation = null,
        FacultyTrainingBudgetAllocation? nextAllocation = null)
    {
        var effectiveCurrentQuota = currentAllocation?.AnnualQuotaAmount ?? currentPolicy.AnnualQuotaAmount;
        var effectiveCurrentMaxBorrow = currentAllocation?.MaxBorrowAmount ?? currentPolicy.MaxBorrowAmount;
        var effectiveNextQuota = nextAllocation?.AnnualQuotaAmount ?? nextPolicy.AnnualQuotaAmount;

        var currentEntries = facultyLedgerEntries.Where(x => x.FiscalYearBe == fiscalYearBe).ToList();
        var nextEntries = facultyLedgerEntries.Where(x => x.FiscalYearBe == fiscalYearBe + 1).ToList();

        var consumed = currentEntries
            .Where(x => x.EntryType == BudgetLedgerEntryType.Consume)
            .Sum(x => x.Amount);

        var carryForwardDeducted = currentEntries
            .Where(x => x.EntryType == BudgetLedgerEntryType.CarryForwardDeduction)
            .Sum(x => x.Amount);

        var adjustments = currentEntries
            .Where(x => x.EntryType == BudgetLedgerEntryType.Adjustment)
            .Sum(x => x.Amount);

        var borrowedFromNext = currentEntries
            .Where(x => x.EntryType == BudgetLedgerEntryType.BorrowAdvance)
            .Sum(x => x.Amount);

        var remaining = effectiveCurrentQuota - (consumed + carryForwardDeducted + adjustments);

        var nextYearUsed = nextEntries
            .Where(x => x.EntryType is BudgetLedgerEntryType.Consume
                or BudgetLedgerEntryType.CarryForwardDeduction
                or BudgetLedgerEntryType.Adjustment)
            .Sum(x => x.Amount);

        var nextYearRemaining = Math.Max(0m, effectiveNextQuota - nextYearUsed);

        decimal borrowable = 0m;
        if (currentPolicy.AllowFutureQuotaBorrowing)
        {
            var remainingBorrowCap = Math.Max(0m, effectiveCurrentMaxBorrow - borrowedFromNext);
            borrowable = Math.Min(remainingBorrowCap, nextYearRemaining);
        }

        return new FacultyFiscalYearBalance(
            fiscalYearBe,
            effectiveCurrentQuota,
            consumed,
            carryForwardDeducted,
            adjustments,
            borrowedFromNext,
            remaining,
            currentPolicy.AllowFutureQuotaBorrowing,
            effectiveCurrentMaxBorrow,
            nextYearRemaining,
            borrowable,
            HasCustomAllocation: currentAllocation is not null,
            AllocationNote: currentAllocation?.Note);
    }

    public static async Task<(TrainingBudgetPolicy CurrentPolicy, TrainingBudgetPolicy NextPolicy)> EnsurePoliciesAsync(
        IChulaDbContext context,
        int fiscalYearBe,
        CancellationToken cancellationToken)
    {
        var policies = await context.TrainingBudgetPolicies
            .Where(x => x.FiscalYearBe == fiscalYearBe || x.FiscalYearBe == fiscalYearBe + 1)
            .ToListAsync(cancellationToken);

        var current = policies.FirstOrDefault(x => x.FiscalYearBe == fiscalYearBe);
        var next = policies.FirstOrDefault(x => x.FiscalYearBe == fiscalYearBe + 1);
        var added = false;

        if (current is null)
        {
            current = new TrainingBudgetPolicy(fiscalYearBe);
            context.TrainingBudgetPolicies.Add(current);
            added = true;
        }

        if (next is null)
        {
            next = new TrainingBudgetPolicy(fiscalYearBe + 1);
            context.TrainingBudgetPolicies.Add(next);
            added = true;
        }

        if (added)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        return (current, next);
    }

    /// <summary>
    /// Creates the append-only ledger entries for an approved AcademicFunded training amount,
    /// automatically splitting across current-year Consume + next-year BorrowAdvance/CarryForwardDeduction per A06.
    /// </summary>
    public static IReadOnlyList<TrainingBudgetLedgerEntry> CreateDeductionEntries(
        TrainingRequest request,
        decimal approvedAmount,
        FacultyFiscalYearBalance balance,
        string actorLogin,
        DateTimeOffset effectiveAt)
    {
        if (approvedAmount <= 0m)
        {
            return Array.Empty<TrainingBudgetLedgerEntry>();
        }

        var currentAvailable = Math.Max(0m, balance.RemainingInYear);
        if (approvedAmount > currentAvailable + balance.BorrowableFromNextYear)
        {
            throw new DomainException(
                $"Insufficient training budget for FY {request.FiscalYearBe}. " +
                $"Approved amount: {approvedAmount:N2} THB, current remaining: {currentAvailable:N2} THB, " +
                $"borrowable from FY {request.FiscalYearBe + 1}: {balance.BorrowableFromNextYear:N2} THB.");
        }

        var entries = new List<TrainingBudgetLedgerEntry>();
        var consumeCurrent = Math.Min(approvedAmount, currentAvailable);
        var borrowFromNext = approvedAmount - consumeCurrent;

        if (consumeCurrent > 0m)
        {
            entries.Add(new TrainingBudgetLedgerEntry(
                request.FacultyMemberId,
                request.Id,
                request.FiscalYearBe,
                BudgetLedgerEntryType.Consume,
                consumeCurrent,
                effectiveAt,
                actorLogin,
                $"Approved training spend: {request.TitleTh}"));
        }

        if (borrowFromNext > 0m)
        {
            var nextYearBe = request.FiscalYearBe + 1;
            entries.Add(new TrainingBudgetLedgerEntry(
                request.FacultyMemberId,
                request.Id,
                request.FiscalYearBe,
                BudgetLedgerEntryType.BorrowAdvance,
                borrowFromNext,
                effectiveAt,
                actorLogin,
                $"Borrowed {borrowFromNext:N2} THB advance from FY {nextYearBe}: {request.TitleTh}",
                sourceFiscalYearBe: nextYearBe));

            entries.Add(new TrainingBudgetLedgerEntry(
                request.FacultyMemberId,
                request.Id,
                nextYearBe,
                BudgetLedgerEntryType.CarryForwardDeduction,
                borrowFromNext,
                effectiveAt,
                actorLogin,
                $"Carry-forward deduction for advance borrowed in FY {request.FiscalYearBe}: {request.TitleTh}",
                sourceFiscalYearBe: request.FiscalYearBe));
        }

        return entries;
    }
}
