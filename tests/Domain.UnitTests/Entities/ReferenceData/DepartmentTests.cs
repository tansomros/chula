using Chula.Domain.Entities.ReferenceData;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.ReferenceData;

[TestFixture]
public class DepartmentTests
{
    [Test]
    public void Constructor_WithValidFields_ShouldNormalizeCodeAndInitialize()
    {
        var department = new Department("  pharm-prac ", "ภาควิชาเภสัชกรรมปฏิบัติ", "Department of Pharmacy Practice");

        department.ExternalId.ShouldNotBe(Guid.Empty);
        department.Code.ShouldBe("PHARM-PRAC");
        department.NameTh.ShouldBe("ภาควิชาเภสัชกรรมปฏิบัติ");
        department.NameEn.ShouldBe("Department of Pharmacy Practice");
        department.IsActive.ShouldBeTrue();
    }

    [TestCase("")]
    [TestCase("   ")]
    public void Constructor_WhenCodeIsInvalid_ShouldThrowArgumentException(string invalidCode)
    {
        Should.Throw<ArgumentException>(() =>
            new Department(invalidCode, "ภาควิชาเภสัชกรรมปฏิบัติ", "Department of Pharmacy Practice"));
    }

    [TestCase("")]
    [TestCase("   ")]
    public void Constructor_WhenNameThIsInvalid_ShouldThrowArgumentException(string invalidNameTh)
    {
        Should.Throw<ArgumentException>(() =>
            new Department("PHARM-PRAC", invalidNameTh, "Department of Pharmacy Practice"));
    }

    [TestCase("")]
    [TestCase("   ")]
    public void Constructor_WhenNameEnIsInvalid_ShouldThrowArgumentException(string invalidNameEn)
    {
        Should.Throw<ArgumentException>(() =>
            new Department("PHARM-PRAC", "ภาควิชาเภสัชกรรมปฏิบัติ", invalidNameEn));
    }
}
