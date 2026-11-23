using Ardalis.GuardClauses;
using Chula.Domain.Common;
using Chula.Domain.Entities.Faculty;
using Chula.Domain.Enums.Training;

namespace Chula.Domain.Entities.Training;

/// <summary>
/// Append-only financial ledger for faculty training budgets (Decisions A05, A06).
/// Remaining balances are ALWAYS derived from TrainingBudgetPolicy - SUM(LedgerEntries), never mutated in place.
/// </summary>
public sealed class TrainingBudgetLedgerEntry : EntityBase
{
    private TrainingBudgetLedgerEntry()
    {
    }

    public TrainingBudgetLedgerEntry(
        int facultyMemberId,
        int? trainingRequestId,
        int fiscalYearBe,
        BudgetLedgerEntryType entryType,
        decimal amount,
        DateTimeOffset effectiveAt,
        string recordedBy,
        string description,
        int? sourceFiscalYearBe = null)
    {
        FacultyMemberId = Guard.Against.NegativeOrZero(facultyMemberId, nameof(facultyMemberId));
        TrainingRequestId = trainingRequestId;
        FiscalYearBe = Guard.Against.OutOfRange(fiscalYearBe, nameof(fiscalYearBe), 2500, 2700);
        EntryType = entryType;

        if (entryType != BudgetLedgerEntryType.Adjustment && amount <= 0)
        {
            throw new DomainException($"Ledger entry type '{entryType}' must have a positive amount.");
        }

        if (entryType == BudgetLedgerEntryType.Adjustment && amount == 0)
        {
            throw new DomainException("Adjustment ledger entry cannot have zero amount.");
        }

        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        EffectiveAt = effectiveAt;
        RecordedBy = Guard.Against.NullOrWhiteSpace(recordedBy, nameof(recordedBy)).Trim();
        Description = Guard.Against.NullOrWhiteSpace(description, nameof(description)).Trim();
        SourceFiscalYearBe = sourceFiscalYearBe;
    }

    public int FacultyMemberId { get; private set; }

    public FacultyMember FacultyMember { get; private set; } = null!;

    public int? TrainingRequestId { get; private set; }

    public TrainingRequest? TrainingRequest { get; private set; }

    public int FiscalYearBe { get; private set; }

    public int? SourceFiscalYearBe { get; private set; }

    public BudgetLedgerEntryType EntryType { get; private set; }

    public decimal Amount { get; private set; }

    public DateTimeOffset EffectiveAt { get; private set; }

    public string RecordedBy { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;
}
