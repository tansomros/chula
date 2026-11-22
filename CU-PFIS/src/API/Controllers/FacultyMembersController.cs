using Chula.Application.Features.Faculty.FacultyMembers.Commands.CreateFacultyMember;
using Chula.Application.Features.Faculty.FacultyMembers.Commands.ImportScopusPublications;
using Chula.Application.Features.Faculty.FacultyMembers.Commands.UpdateFacultyMember;
using Chula.Application.Features.Faculty.FacultyMembers.Queries.GetFacultyMemberById;
using Chula.Application.Features.Faculty.FacultyMembers.Queries.GetFacultyMembers;
using Microsoft.AspNetCore.Mvc;

namespace Chula.API.Controllers;

public sealed class FacultyMembersController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<FacultyMemberListItemDto>>> GetFacultyMembers(
        [FromQuery] string? search = null,
        [FromQuery] Guid? departmentExternalId = null,
        [FromQuery] Guid? academicPositionExternalId = null,
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(
            new GetFacultyMembersQuery(search, departmentExternalId, academicPositionExternalId, activeOnly),
            cancellationToken);
        return Ok(result);
    }

    [HttpGet("{externalId:guid}")]
    public async Task<ActionResult<FacultyMemberDetailDto>> GetFacultyMemberById(
        Guid externalId,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetFacultyMemberByIdQuery(externalId), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateFacultyMember(
        [FromBody] CreateFacultyMemberCommand command,
        CancellationToken cancellationToken = default)
    {
        var externalId = await Mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetFacultyMemberById), new { externalId }, externalId);
    }

    [HttpPut("{externalId:guid}")]
    public async Task<IActionResult> UpdateFacultyMember(
        Guid externalId,
        [FromBody] UpdateFacultyMemberCommand command,
        CancellationToken cancellationToken = default)
    {
        await Mediator.Send(command with { ExternalId = externalId }, cancellationToken);
        return NoContent();
    }

    [HttpPost("{externalId:guid}/import-scopus")]
    public async Task<ActionResult<ImportScopusPublicationsResult>> ImportScopusPublications(
        Guid externalId,
        [FromBody] ImportScopusPublicationsCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(
            command with { FacultyMemberExternalId = externalId },
            cancellationToken);
        return Ok(result);
    }
}
