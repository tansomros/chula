using Chula.Domain.Entities.ReferenceData;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.ReferenceData;

[TestFixture]
public class MajorTests
{
    [Test]
    public void Constructor_WithIndustrialPharmacyAndPharmaceuticalCare_ShouldInitializeCorrectly()
    {
        var ipMajor = new Major("ip", "สาขาวิชาเภสัชกรรมอุตสาหการ", "Industrial Pharmacy");
        var pcMajor = new Major("pc", "สาขาวิชาการบริบาลทางเภสัชกรรม", "Pharmaceutical Care");

        ipMajor.Code.ShouldBe("IP");
        ipMajor.NameTh.ShouldBe("สาขาวิชาเภสัชกรรมอุตสาหการ");
        pcMajor.Code.ShouldBe("PC");
        pcMajor.NameTh.ShouldBe("สาขาวิชาการบริบาลทางเภสัชกรรม");
    }

    [Test]
    public void Constructor_WhenRequiredFieldIsEmpty_ShouldThrowArgumentException()
    {
        Should.Throw<ArgumentException>(() => new Major("", "สาขาวิชา", "Major"));
        Should.Throw<ArgumentException>(() => new Major("IP", "", "Major"));
        Should.Throw<ArgumentException>(() => new Major("IP", "สาขาวิชา", ""));
    }
}
