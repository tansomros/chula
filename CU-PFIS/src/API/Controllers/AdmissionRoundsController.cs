using Chula.Application.Features.ReferenceData.AdmissionRounds.Queries.GetAdmissionRounds;
using Microsoft.AspNetCore.Mvc;

namespace Chula.API.Controllers;

public sealed class AdmissionRoundsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AdmissionRoundDto>>> GetAdmissionRounds(
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetAdmissionRoundsQuery(activeOnly), cancellationToken));
    }
}
