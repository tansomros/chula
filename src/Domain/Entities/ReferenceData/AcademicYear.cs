using Ardalis.GuardClauses;
using Chula.Domain.Common;

namespace Chula.Domain.Entities.ReferenceData;

public sealed class AcademicYear : SoftDeletableEntityBase, IHasActiveFlag
{
    private const int BuddhistEraOffset = 543;

    private AcademicYear()
    {
    }

    public AcademicYear(
        int yearBe,
        DateOnly startDate,
        DateOnly endDate,
        bool isCurrent = false)
    {
        Update(
            yearBe,
            startDate,
            endDate);
        IsCurrent = isCurrent;
        IsActive = true;
    }

    public int YearBe { get; private set; }

    public int YearCe { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public bool IsCurrent { get; private set; }

    public bool IsActive { get; private set; } = true;

    public void Update(int yearBe, DateOnly startDate, DateOnly endDate)
    {
        Guard.Against.OutOfRange(yearBe, nameof(yearBe), 2500, 2700);
        Guard.Against.Default(startDate, nameof(startDate));
        Guard.Against.Default(endDate, nameof(endDate));

        if (endDate < startDate)
        {
            throw new DomainException("วันสิ้นสุดปีการศึกษาต้องมากกว่าหรือเท่ากับวันเริ่มต้นปีการศึกษา (EndDate must be greater than or equal to StartDate).");
        }

        YearBe = yearBe;
        YearCe = yearBe - BuddhistEraOffset;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void MarkAsCurrent()
    {
        IsCurrent = true;
    }

    public void UnmarkAsCurrent()
    {
        IsCurrent = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
