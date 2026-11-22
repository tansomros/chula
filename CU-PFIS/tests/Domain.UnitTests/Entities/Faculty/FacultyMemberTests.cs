using Chula.Domain.Common;
using Chula.Domain.Entities.Faculty;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.Faculty;

[TestFixture]
public class FacultyMemberTests
{
    [Test]
    public void Constructor_WithValidFields_ShouldInitializeCorrectly()
    {
        var member = CreateSampleMember();

        member.ExternalId.ShouldNotBe(Guid.Empty);
        member.PersonnelCode.ShouldBe("PHARM-1001");
        member.FirstNameTh.ShouldBe("สมชาย");
        member.FirstNameEn.ShouldBe("Somchai");
        member.Email.ShouldBe("somchai.p@pharm.chula.ac.th");
        member.IsActive.ShouldBeTrue();
        member.IsDeleted.ShouldBeFalse();
    }

    [Test]
    public void Constructor_WithBlankPersonnelCode_ShouldThrowArgumentException()
    {
        Should.Throw<ArgumentException>(() =>
            new FacultyMember(
                " ",
                "สมชาย",
                "เภสัชศาสตร์",
                "Somchai",
                "Phesatchasat",
                1,
                1,
                "somchai.p@pharm.chula.ac.th"));
    }

    [Test]
    public void Constructor_WithInvalidEmail_ShouldThrowDomainException()
    {
        Should.Throw<DomainException>(() =>
            new FacultyMember(
                "PHARM-1001",
                "สมชาย",
                "เภสัชศาสตร์",
                "Somchai",
                "Phesatchasat",
                1,
                1,
                "not-an-email"));
    }

    [Test]
    public void TryImportPublication_ShouldDeduplicateByScopusIdAndDoi()
    {
        var member = CreateSampleMember();
        var now = DateTimeOffset.UtcNow;

        var first = member.TryImportPublication(
            "Paper One",
            "Journal A",
            2025,
            "10.1000/abc",
            "2-s2.0-11111",
            "ScopusCsv",
            now);

        var duplicateByEid = member.TryImportPublication(
            "Paper One Renamed",
            "Journal A",
            2025,
            null,
            "2-s2.0-11111",
            "ScopusCsv",
            now);

        var duplicateByDoi = member.TryImportPublication(
            "Paper One Case DOI",
            "Journal A",
            2025,
            "10.1000/ABC",
            "2-s2.0-99999",
            "ScopusCsv",
            now);

        var secondDistinct = member.TryImportPublication(
            "Paper Two",
            "Journal B",
            2026,
            "10.1000/xyz",
            "2-s2.0-22222",
            "ScopusCsv",
            now);

        first.ShouldBeTrue();
        duplicateByEid.ShouldBeFalse();
        duplicateByDoi.ShouldBeFalse();
        secondDistinct.ShouldBeTrue();
        member.Publications.Count.ShouldBe(2);
    }

    [Test]
    public void AddEducation_WithInvalidGraduationYear_ShouldThrowArgumentOutOfRangeException()
    {
        var member = CreateSampleMember();

        Should.Throw<ArgumentOutOfRangeException>(() =>
            member.AddEducation("ปร.ด.", "จุฬาฯ", 1800));
    }

    private static FacultyMember CreateSampleMember()
    {
        return new FacultyMember(
            "PHARM-1001",
            "สมชาย",
            "เภสัชศาสตร์",
            "Somchai",
            "Phesatchasat",
            1,
            1,
            "somchai.p@pharm.chula.ac.th",
            "ดร.ภก.",
            "Dr. Pharm.");
    }
}
