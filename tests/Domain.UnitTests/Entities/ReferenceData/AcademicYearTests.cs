using Chula.Domain.Common;
using Chula.Domain.Entities.ReferenceData;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.ReferenceData;

[TestFixture]
public class AcademicYearTests
{
    [Test]
    public void Constructor_WithValidFields_ShouldInitializeCorrectly()
    {
        var startDate = new DateOnly(2026, 8, 1);
        var endDate = new DateOnly(2027, 5, 31);

        var academicYear = new AcademicYear(2569, startDate, endDate, isCurrent: true);

        academicYear.ExternalId.ShouldNotBe(Guid.Empty);
        academicYear.YearBe.ShouldBe(2569);
        academicYear.YearCe.ShouldBe(2026);
        academicYear.StartDate.ShouldBe(startDate);
        academicYear.EndDate.ShouldBe(endDate);
        academicYear.IsCurrent.ShouldBeTrue();
        academicYear.IsActive.ShouldBeTrue();
        academicYear.IsDeleted.ShouldBeFalse();
    }

    [Test]
    public void Constructor_WhenYearBeOutOfRange_ShouldThrowArgumentOutOfRangeException()
    {
        var startDate = new DateOnly(2026, 8, 1);
        var endDate = new DateOnly(2027, 5, 31);

        Should.Throw<ArgumentOutOfRangeException>(() =>
            new AcademicYear(1999, startDate, endDate));
    }

    [Test]
    public void Constructor_WhenEndDateBeforeStartDate_ShouldThrowDomainException()
    {
        var startDate = new DateOnly(2026, 8, 1);
        var endDate = new DateOnly(2026, 7, 31);

        Should.Throw<DomainException>(() =>
            new AcademicYear(2569, startDate, endDate));
    }

    [Test]
    public void MarkAndUnmarkCurrent_ShouldTransitionStateCorrectly()
    {
        var academicYear = new AcademicYear(
            2569,
            new DateOnly(2026, 8, 1),
            new DateOnly(2027, 5, 31),
            isCurrent: false);

        academicYear.MarkAsCurrent();
        academicYear.IsCurrent.ShouldBeTrue();

        academicYear.UnmarkAsCurrent();
        academicYear.IsCurrent.ShouldBeFalse();
    }

    [Test]
    public void DeactivateAndActivate_ShouldTransitionActiveFlag()
    {
        var academicYear = new AcademicYear(
            2569,
            new DateOnly(2026, 8, 1),
            new DateOnly(2027, 5, 31));

        academicYear.Deactivate();
        academicYear.IsActive.ShouldBeFalse();

        academicYear.Activate();
        academicYear.IsActive.ShouldBeTrue();
    }
}
