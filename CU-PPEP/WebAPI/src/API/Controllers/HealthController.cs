using Microsoft.AspNetCore.Mvc;

namespace CUPPEP.API.Controllers;

public sealed class HealthController : BaseController
{
    [HttpGet]
    public IActionResult Get() => Ok(new { status = "ok", service = "CU-PPEP API", utcNow = DateTimeOffset.UtcNow });
}
