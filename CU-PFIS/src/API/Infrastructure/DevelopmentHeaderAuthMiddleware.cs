using System.Security.Claims;
using Chula.Application.Common.Security;

namespace Chula.API.Infrastructure;

/// <summary>
/// Local development-only header authentication middleware (never registered in Staging/Production).
/// Enables testing Admin, Department-scoped Staff, and Faculty roles before SSO/OIDC is wired.
/// </summary>
public sealed class DevelopmentHeaderAuthMiddleware
{
    private readonly RequestDelegate _next;

    public DevelopmentHeaderAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated != true)
        {
            var role = context.Request.Headers["X-Dev-Role"].FirstOrDefault() ?? RoleConstants.Admin;
            var userId = context.Request.Headers["X-Dev-User-Id"].FirstOrDefault() ?? $"dev-{role.ToLowerInvariant()}";
            var loginName = context.Request.Headers["X-Dev-Login"].FirstOrDefault() ?? $"{role.ToLowerInvariant()}.dev";
            var departmentExternalId = context.Request.Headers["X-Dev-Department-Id"].FirstOrDefault();
            var facultyExternalId = context.Request.Headers["X-Dev-Faculty-Id"].FirstOrDefault();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, loginName),
                new(ClaimTypes.Role, role),
                new("permission", PermissionConstants.ReferenceData.View)
            };

            if (string.Equals(role, RoleConstants.Admin, StringComparison.OrdinalIgnoreCase))
            {
                claims.Add(new Claim("permission", PermissionConstants.ReferenceData.Manage));
                claims.Add(new Claim("permission", PermissionConstants.FacultyCv.ViewAll));
                claims.Add(new Claim("permission", PermissionConstants.FacultyCv.EditAll));
                claims.Add(new Claim("permission", PermissionConstants.FacultyCv.ExportAll));
                claims.Add(new Claim("permission", PermissionConstants.FacultyCv.NotifyUpdate));
                claims.Add(new Claim("permission", PermissionConstants.StudentsCv.View));
                claims.Add(new Claim("permission", PermissionConstants.StudentsCv.Edit));
                claims.Add(new Claim("permission", PermissionConstants.StudentsCv.Export));
                claims.Add(new Claim("permission", PermissionConstants.EmployerEvaluations.View));
            }
            else if (string.Equals(role, RoleConstants.Staff, StringComparison.OrdinalIgnoreCase))
            {
                claims.Add(new Claim("permission", PermissionConstants.FacultyCv.ViewAll));
                claims.Add(new Claim("permission", PermissionConstants.FacultyCv.EditAll));
                claims.Add(new Claim("permission", PermissionConstants.FacultyCv.ExportAll));
                claims.Add(new Claim("permission", PermissionConstants.FacultyCv.NotifyUpdate));
                claims.Add(new Claim("permission", PermissionConstants.StudentsCv.View));
                claims.Add(new Claim("permission", PermissionConstants.StudentsCv.Edit));
                claims.Add(new Claim("permission", PermissionConstants.StudentsCv.Export));
                claims.Add(new Claim("permission", PermissionConstants.EmployerEvaluations.View));
            }
            else if (string.Equals(role, RoleConstants.Faculty, StringComparison.OrdinalIgnoreCase))
            {
                claims.Add(new Claim("permission", PermissionConstants.FacultyCv.ViewOwn));
                claims.Add(new Claim("permission", PermissionConstants.FacultyCv.EditOwn));
                claims.Add(new Claim("permission", PermissionConstants.FacultyCv.ExportOwn));
            }

            if (!string.IsNullOrWhiteSpace(departmentExternalId))
            {
                claims.Add(new Claim("department_external_id", departmentExternalId));
            }

            if (!string.IsNullOrWhiteSpace(facultyExternalId))
            {
                claims.Add(new Claim("faculty_member_external_id", facultyExternalId));
            }

            var identity = new ClaimsIdentity(claims, "DevelopmentHeaderAuth");
            context.User = new ClaimsPrincipal(identity);
        }

        await _next(context);
    }
}
