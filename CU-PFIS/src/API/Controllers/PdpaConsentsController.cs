using Chula.Application.Features.Security.PdpaConsents.Commands.RecordPdpaConsent;
using Chula.Application.Features.Security.PdpaConsents.Queries.GetMyPdpaConsent;
using Microsoft.AspNetCore.Mvc;

namespace Chula.API.Controllers;

public sealed class PdpaConsentsController : BaseController
{
    [HttpGet("me")]
    public async Task<ActionResult<PdpaConsentStatusDto>> GetMyConsent(
        [FromQuery] string policyVersion = "2026.1",
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetMyPdpaConsentQuery(policyVersion), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PdpaConsentStatusDto>> RecordConsent(
        [FromBody] RecordPdpaConsentRequest request,
        CancellationToken cancellationToken = default)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = Request.Headers.UserAgent.ToString();

        var result = await Mediator.Send(
            new RecordPdpaConsentCommand(request.PolicyVersion, ipAddress, userAgent),
            cancellationToken);

        return Ok(result);
    }
}

public sealed record RecordPdpaConsentRequest(string PolicyVersion = "2026.1");
