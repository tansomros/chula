using Chula.Domain.Entities.Security;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.Security;

[TestFixture]
public class PdpaConsentTests
{
    [Test]
    public void Constructor_WithValidFields_ShouldInitializeCorrectly()
    {
        var acceptedAt = new DateTimeOffset(2026, 11, 21, 10, 0, 0, TimeSpan.Zero);

        var consent = new PdpaConsent(
            "user-001",
            "somchai.p",
            "2026.1",
            acceptedAt,
            "127.0.0.1",
            "Mozilla/5.0");

        consent.ExternalId.ShouldNotBe(Guid.Empty);
        consent.UserId.ShouldBe("user-001");
        consent.LoginName.ShouldBe("somchai.p");
        consent.PolicyVersion.ShouldBe("2026.1");
        consent.AcceptedAt.ShouldBe(acceptedAt);
        consent.IpAddress.ShouldBe("127.0.0.1");
        consent.UserAgent.ShouldBe("Mozilla/5.0");
    }

    [Test]
    public void Constructor_WithEmptyUserId_ShouldThrowArgumentException()
    {
        Should.Throw<ArgumentException>(() =>
            new PdpaConsent("", "somchai.p", "2026.1", DateTimeOffset.UtcNow));
    }

    [Test]
    public void Constructor_WithEmptyPolicyVersion_ShouldThrowArgumentException()
    {
        Should.Throw<ArgumentException>(() =>
            new PdpaConsent("user-001", "somchai.p", "   ", DateTimeOffset.UtcNow));
    }
}
