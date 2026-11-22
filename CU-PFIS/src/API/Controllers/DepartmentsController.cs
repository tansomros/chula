using Chula.Application.Features.ReferenceData.Departments.Queries.GetDepartments;
using Microsoft.AspNetCore.Mvc;

namespace Chula.API.Controllers;

public sealed class DepartmentsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetDepartments(
        [FromQuery] bool activeOnly = true,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetDepartmentsQuery(activeOnly), cancellationToken));
    }
}
