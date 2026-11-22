using CUPPEP.Application.Features.Extensions.StudentLicenseResults;
using Microsoft.AspNetCore.Mvc;

namespace CUPPEP.API.Controllers;

public sealed class StudentLicenseResultsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StudentLicenseResultExtDto>>> GetAll(
        [FromQuery] DateTimeOffset? updatedSince = null,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetStudentLicenseResultsQuery(updatedSince), cancellationToken));
    }

    [HttpGet("{studentCode}")]
    public async Task<ActionResult<StudentLicenseResultExtDto>> GetByCode(
        string studentCode,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetStudentLicenseResultByCodeQuery(studentCode), cancellationToken));
    }

    [HttpPut("{studentCode}")]
    public async Task<ActionResult<StudentLicenseResultExtDto>> Upsert(
        string studentCode,
        [FromBody] UpsertStudentLicenseResultRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(
            new UpsertStudentLicenseResultCommand(
                studentCode,
                request.PassedCc1,
                request.PassedCc2,
                request.PassedIp1,
                request.PassedIp2,
                request.PassedPc1,
                request.PassedPc2),
            cancellationToken);
        return Ok(result);
    }
}

public sealed record UpsertStudentLicenseResultRequest(
    bool PassedCc1,
    bool PassedCc2,
    bool PassedIp1,
    bool PassedIp2,
    bool PassedPc1,
    bool PassedPc2);
