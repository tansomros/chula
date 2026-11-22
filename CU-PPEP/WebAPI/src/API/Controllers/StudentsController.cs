using CUPPEP.Application.Features.Students.Queries.GetStudentByCode;
using CUPPEP.Application.Features.Students.Queries.GetStudentsIncremental;
using Microsoft.AspNetCore.Mvc;

namespace CUPPEP.API.Controllers;

public sealed class StudentsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IncrementalStudentsResponse>> GetStudents(
        [FromQuery] DateTime? updatedSince = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 200,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(
            new GetStudentsIncrementalQuery(updatedSince, page, pageSize),
            cancellationToken));
    }

    [HttpGet("{studentCode}")]
    public async Task<ActionResult<CuppepStudentDetailResponse>> GetStudentByCode(
        string studentCode,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetStudentByCodeQuery(studentCode), cancellationToken));
    }
}
