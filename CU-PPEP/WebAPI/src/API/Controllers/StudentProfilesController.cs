using CUPPEP.Application.Features.Extensions.StudentProfiles;
using Microsoft.AspNetCore.Mvc;

namespace CUPPEP.API.Controllers;

public sealed class StudentProfilesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StudentProfileExtDto>>> GetAll(
        [FromQuery] DateTimeOffset? updatedSince = null,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetStudentProfilesQuery(updatedSince), cancellationToken));
    }

    [HttpGet("{studentCode}")]
    public async Task<ActionResult<StudentProfileExtDto>> GetByCode(
        string studentCode,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetStudentProfileByCodeQuery(studentCode), cancellationToken));
    }

    [HttpPut("{studentCode}")]
    public async Task<ActionResult<StudentProfileExtDto>> Upsert(
        string studentCode,
        [FromBody] UpsertStudentProfileRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(
            new UpsertStudentProfileCommand(
                studentCode,
                request.AdmissionRoundCode,
                request.SeniorProjectTitleTh,
                request.SeniorProjectTitleEn),
            cancellationToken);
        return Ok(result);
    }
}

public sealed record UpsertStudentProfileRequest(
    string? AdmissionRoundCode,
    string? SeniorProjectTitleTh,
    string? SeniorProjectTitleEn);
