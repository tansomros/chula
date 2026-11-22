using Chula.Application.Features.ReferenceData.AcademicYears.Commands.CreateAcademicYear;
using Chula.Application.Features.ReferenceData.AcademicYears.Queries.GetAcademicYears;
using Microsoft.AspNetCore.Mvc;

namespace Chula.API.Controllers;

public sealed class AcademicYearsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AcademicYearDto>>> GetAcademicYears(
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetAcademicYearsQuery(activeOnly), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAcademicYear(
        [FromBody] CreateAcademicYearCommand command,
        CancellationToken cancellationToken = default)
    {
        var externalId = await Mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAcademicYears), new { externalId }, externalId);
    }
}
