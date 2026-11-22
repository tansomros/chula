using CUPPEP.Application.Features.Teachers.Queries.GetTeachers;
using Microsoft.AspNetCore.Mvc;

namespace CUPPEP.API.Controllers;

public sealed class TeachersController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CuppepTeacherDto>>> GetTeachers(
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetTeachersQuery(), cancellationToken));
    }
}
