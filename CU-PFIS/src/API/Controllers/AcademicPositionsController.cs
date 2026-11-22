using Chula.Application.Features.ReferenceData.AcademicPositions.Queries.GetAcademicPositions;
using Microsoft.AspNetCore.Mvc;

namespace Chula.API.Controllers;

public sealed class AcademicPositionsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AcademicPositionDto>>> GetAcademicPositions(
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetAcademicPositionsQuery(activeOnly), cancellationToken));
    }
}
