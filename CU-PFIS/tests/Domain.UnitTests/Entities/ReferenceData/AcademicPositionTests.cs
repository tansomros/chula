using Chula.Domain.Entities.ReferenceData;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.ReferenceData;

[TestFixture]
public class AcademicPositionTests
{
    [Test]
    public void Constructor_WithValidFields_ShouldInitializeCorrectly()
    {
        var position = new AcademicPosition(
            "assoc_prof",
            "รองศาสตราจารย์",
            "Associate Professor",
            "รศ.",
            "Assoc. Prof.",
            sortOrder: 2);

        position.Code.ShouldBe("ASSOC_PROF");
        position.TitleTh.ShouldBe("รองศาสตราจารย์");
        position.ShortTitleTh.ShouldBe("รศ.");
        position.SortOrder.ShouldBe(2);
        position.IsActive.ShouldBeTrue();
    }

    [Test]
    public void Constructor_WhenSortOrderIsNegative_ShouldThrowArgumentException()
    {
        Should.Throw<ArgumentException>(() =>
            new AcademicPosition(
                "PROF",
                "ศาสตราจารย์",
                "Professor",
                "ศ.",
                "Prof.",
                sortOrder: -1));
    }
}
