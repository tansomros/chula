using Chula.Application.Features.ReferenceData.Majors.Queries.GetMajors;
using Microsoft.AspNetCore.Mvc;

namespace Chula.API.Controllers;

public sealed class MajorsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MajorDto>>> GetMajors(
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetMajorsQuery(activeOnly), cancellationToken));
    }
}
