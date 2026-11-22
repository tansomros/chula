using Chula.Application.Features.Security.PdpaConsents.Commands.RecordPdpaConsent;
using Chula.Application.Features.Security.PdpaConsents.Queries.GetMyPdpaConsent;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Chula.Application.FunctionalTests.Security;

[TestFixture]
public sealed class PdpaConsentsTests : FunctionalTestBase
{
    [Test]
    public async Task RecordPdpaConsent_ShouldPersistAndBeIdempotentForSamePolicyVersion()
    {
        var uniqueUserId = $"pdpa-user-{Guid.NewGuid():N}";
        CurrentUser.RunAsAdmin(uniqueUserId, "pdpa.tester");

        var before = await SendAsync(new GetMyPdpaConsentQuery("2026.1"));
        before.HasAccepted.ShouldBeFalse();

        var first = await SendAsync(new RecordPdpaConsentCommand("2026.1", "127.0.0.1", "NUnit"));
        first.HasAccepted.ShouldBeTrue();
        first.ExternalId.ShouldNotBe(Guid.Empty);
        first.AcceptedAt.ShouldNotBeNull();

        var second = await SendAsync(new RecordPdpaConsentCommand("2026.1", "127.0.0.1", "NUnit"));
        second.ExternalId.ShouldBe(first.ExternalId);

        var count = await WithDbContextAsync(db =>
            db.PdpaConsents.CountAsync(x => x.UserId == uniqueUserId && x.PolicyVersion == "2026.1"));
        count.ShouldBe(1);
    }
}
