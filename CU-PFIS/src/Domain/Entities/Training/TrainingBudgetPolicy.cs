using Ardalis.GuardClauses;
using Chula.Domain.Common;

namespace Chula.Domain.Entities.Training;

/// <summary>
/// Configurable annual training budget policy per Thai Fiscal Year (1 Oct – 30 Sep, Decision A06).
/// Default quota is 10,000 THB/person/fiscal year, configurable without hardcoding.
/// </summary>
public sealed class TrainingBudgetPolicy : EntityBase, IHasActiveFlag
{
    public const decimal DefaultAnnualQuota = 10_000m;

    private TrainingBudgetPolicy()
    {
    }

    public TrainingBudgetPolicy(
        int fiscalYearBe,
        decimal annualQuotaAmount = DefaultAnnualQuota,
        bool allowFutureQuotaBorrowing = true,
        decimal? maxBorrowAmount = null)
    {
        FiscalYearBe = Guard.Against.OutOfRange(fiscalYearBe, nameof(fiscalYearBe), 2500, 2700);
        FiscalYearCe = FiscalYearBe - 543;
        var (startDate, endDate) = FiscalYearCalculator.GetDateRange(FiscalYearBe);
        StartDate = startDate;
        EndDate = endDate;
        AnnualQuotaAmount = Guard.Against.Negative(annualQuotaAmount, nameof(annualQuotaAmount));
        AllowFutureQuotaBorrowing = allowFutureQuotaBorrowing;
        MaxBorrowAmount = Guard.Against.Negative(
            maxBorrowAmount ?? (allowFutureQuotaBorrowing ? annualQuotaAmount : 0m),
            nameof(maxBorrowAmount));
        IsActive = true;
    }

    public int FiscalYearBe { get; private set; }

    public int FiscalYearCe { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public decimal AnnualQuotaAmount { get; private set; }

    public bool AllowFutureQuotaBorrowing { get; private set; }

    /// <summary>
    /// Configurable cap on how much future fiscal-year quota may be borrowed into this year (known-issues §2).
    /// </summary>
    public decimal MaxBorrowAmount { get; private set; }

    public bool IsActive { get; private set; } = true;

    public void UpdatePolicy(
        decimal annualQuotaAmount,
        bool allowFutureQuotaBorrowing,
        decimal maxBorrowAmount)
    {
        AnnualQuotaAmount = Guard.Against.Negative(annualQuotaAmount, nameof(annualQuotaAmount));
        AllowFutureQuotaBorrowing = allowFutureQuotaBorrowing;
        MaxBorrowAmount = allowFutureQuotaBorrowing
            ? Guard.Against.Negative(maxBorrowAmount, nameof(maxBorrowAmount))
            : 0m;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}
