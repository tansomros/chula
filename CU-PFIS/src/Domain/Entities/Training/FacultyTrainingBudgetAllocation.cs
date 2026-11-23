using Ardalis.GuardClauses;
using Chula.Domain.Common;
using Chula.Domain.Entities.Faculty;

namespace Chula.Domain.Entities.Training;

/// <summary>
/// Optional per-faculty budget quota override for a specific Thai Fiscal Year.
/// When present, overrides the global <see cref="TrainingBudgetPolicy"/> for that individual faculty member;
/// when absent (or deleted), the faculty member automatically inherits the global fiscal-year policy.
/// </summary>
public sealed class FacultyTrainingBudgetAllocation : EntityBase
{
    private FacultyTrainingBudgetAllocation()
    {
    }

    public FacultyTrainingBudgetAllocation(
        int facultyMemberId,
        int fiscalYearBe,
        decimal annualQuotaAmount,
        decimal? maxBorrowAmount = null,
        string? note = null)
    {
        FacultyMemberId = Guard.Against.NegativeOrZero(facultyMemberId, nameof(facultyMemberId));
        FiscalYearBe = Guard.Against.OutOfRange(fiscalYearBe, nameof(fiscalYearBe), 2500, 2700);
        AnnualQuotaAmount = decimal.Round(
            Guard.Against.Negative(annualQuotaAmount, nameof(annualQuotaAmount)),
            2,
            MidpointRounding.AwayFromZero);
        MaxBorrowAmount = maxBorrowAmount.HasValue
            ? decimal.Round(Guard.Against.Negative(maxBorrowAmount.Value, nameof(maxBorrowAmount)), 2, MidpointRounding.AwayFromZero)
            : null;
        Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
    }

    public int FacultyMemberId { get; private set; }

    public FacultyMember FacultyMember { get; private set; } = null!;

    public int FiscalYearBe { get; private set; }

    public decimal AnnualQuotaAmount { get; private set; }

    /// <summary>
    /// Optional per-faculty borrow cap override; when null, inherits <see cref="TrainingBudgetPolicy.MaxBorrowAmount"/>.
    /// </summary>
    public decimal? MaxBorrowAmount { get; private set; }

    public string? Note { get; private set; }

    public void UpdateAllocation(
        decimal annualQuotaAmount,
        decimal? maxBorrowAmount = null,
        string? note = null)
    {
        AnnualQuotaAmount = decimal.Round(
            Guard.Against.Negative(annualQuotaAmount, nameof(annualQuotaAmount)),
            2,
            MidpointRounding.AwayFromZero);
        MaxBorrowAmount = maxBorrowAmount.HasValue
            ? decimal.Round(Guard.Against.Negative(maxBorrowAmount.Value, nameof(maxBorrowAmount)), 2, MidpointRounding.AwayFromZero)
            : null;
        Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
    }
}
