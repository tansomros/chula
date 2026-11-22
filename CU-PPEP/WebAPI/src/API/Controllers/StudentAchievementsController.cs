using CUPPEP.Application.Features.Extensions.StudentAchievements;
using Microsoft.AspNetCore.Mvc;

namespace CUPPEP.API.Controllers;

public sealed class StudentAchievementsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StudentAchievementExtDto>>> GetAll(
        [FromQuery] string? studentCode = null,
        [FromQuery] DateTimeOffset? updatedSince = null,
        [FromQuery] bool includeDeleted = false,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(
            new GetStudentAchievementsQuery(studentCode, updatedSince, includeDeleted),
            cancellationToken));
    }

    [HttpGet("{externalId:guid}")]
    public async Task<ActionResult<StudentAchievementExtDto>> GetById(
        Guid externalId,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetStudentAchievementByIdQuery(externalId), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateStudentAchievementCommand command,
        CancellationToken cancellationToken = default)
    {
        var externalId = await Mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { externalId }, externalId);
    }

    [HttpPut("{externalId:guid}")]
    public async Task<IActionResult> Update(
        Guid externalId,
        [FromBody] UpdateStudentAchievementCommand command,
        CancellationToken cancellationToken = default)
    {
        await Mediator.Send(command with { ExternalId = externalId }, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{externalId:guid}")]
    public async Task<IActionResult> Delete(
        Guid externalId,
        CancellationToken cancellationToken = default)
    {
        await Mediator.Send(new DeleteStudentAchievementCommand(externalId), cancellationToken);
        return NoContent();
    }
}
