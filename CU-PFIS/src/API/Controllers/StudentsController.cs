using Chula.Application.Features.Students.Students.Commands.CreateStudent;
using Chula.Application.Features.Students.Students.Commands.UpdateStudent;
using Chula.Application.Features.Students.Students.Queries.GetStudentById;
using Chula.Application.Features.Students.Students.Queries.GetStudents;
using Microsoft.AspNetCore.Mvc;

namespace Chula.API.Controllers;

public sealed class StudentsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StudentListItemDto>>> GetStudents(
        [FromQuery] string? search = null,
        [FromQuery] int? cohortYearBe = null,
        [FromQuery] Guid? majorExternalId = null,
        [FromQuery] Guid? admissionRoundExternalId = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(
            new GetStudentsQuery(search, cohortYearBe, majorExternalId, admissionRoundExternalId, activeOnly),
            cancellationToken);
        return Ok(result);
    }

    [HttpGet("{externalId:guid}")]
    public async Task<ActionResult<StudentDetailDto>> GetStudentById(
        Guid externalId,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetStudentByIdQuery(externalId), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateStudent(
        [FromBody] CreateStudentCommand command,
        CancellationToken cancellationToken = default)
    {
        var externalId = await Mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetStudentById), new { externalId }, externalId);
    }

    [HttpPut("{externalId:guid}")]
    public async Task<IActionResult> UpdateStudent(
        Guid externalId,
        [FromBody] UpdateStudentCommand command,
        CancellationToken cancellationToken = default)
    {
        await Mediator.Send(command with { ExternalId = externalId }, cancellationToken);
        return NoContent();
    }
}
