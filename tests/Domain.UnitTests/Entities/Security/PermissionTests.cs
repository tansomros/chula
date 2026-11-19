using Chula.Domain.Common;
using Chula.Domain.Entities.Security;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.Security;

[TestFixture]
public class PermissionTests
{
    [Test]
    public void Constructor_WithModuleDotActionFormat_ShouldSucceed()
    {
        var permission = new Permission(
            "Training-Requests.Verify",
            "Training",
            "ตรวจสอบความถูกต้องคำขออบรม",
            "Verify Training Request");

        permission.Code.ShouldBe("training-requests.verify");
        permission.Module.ShouldBe("Training");
        permission.IsActive.ShouldBeTrue();
    }

    [Test]
    public void Constructor_WithoutDotInCode_ShouldThrowDomainException()
    {
        Should.Throw<DomainException>(() =>
            new Permission(
                "InvalidPermissionFormat",
                "Training",
                "สิทธิ์ไม่ถูกรูปแบบ",
                "Invalid Permission"));
    }
}
