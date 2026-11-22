using CUPPEP.Application.Features.Extensions.StudentEmployments;
using Microsoft.AspNetCore.Mvc;

namespace CUPPEP.API.Controllers;

public sealed class StudentEmploymentsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StudentEmploymentRound1ExtDto>>> GetAll(
        [FromQuery] DateTimeOffset? updatedSince = null,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetStudentEmploymentsQuery(updatedSince), cancellationToken));
    }

    [HttpGet("{studentCode}")]
    public async Task<ActionResult<StudentEmploymentRound1ExtDto>> GetByCode(
        string studentCode,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetStudentEmploymentByCodeQuery(studentCode), cancellationToken));
    }

    [HttpPut("{studentCode}")]
    public async Task<ActionResult<StudentEmploymentRound1ExtDto>> Upsert(
        string studentCode,
        [FromBody] UpsertStudentEmploymentRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(
            new UpsertStudentEmploymentCommand(
                studentCode,
                request.GraduationYearBe,
                request.EmploymentStatus,
                request.JobTitle,
                request.WorkplaceName,
                request.SurveyAnswersJson),
            cancellationToken);
        return Ok(result);
    }
}

public sealed record UpsertStudentEmploymentRequest(
    int GraduationYearBe,
    string EmploymentStatus,
    string? JobTitle = null,
    string? WorkplaceName = null,
    string? SurveyAnswersJson = null);
