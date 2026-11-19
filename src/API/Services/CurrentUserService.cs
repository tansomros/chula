using System.Security.Claims;
using Chula.Application.Common.Interfaces;

namespace Chula.API.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private const string PermissionClaimType = "permission";
    private const string FacultyMemberExternalIdClaimType = "faculty_member_external_id";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? LoginName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

    public IReadOnlyCollection<string> Roles =>
        _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
            .Select(c => c.Value)
            .ToArray() ?? Array.Empty<string>();

    public IReadOnlyCollection<string> Permissions =>
        _httpContextAccessor.HttpContext?.User?.FindAll(PermissionClaimType)
            .Select(c => c.Value)
            .ToArray() ?? Array.Empty<string>();

    public Guid? FacultyMemberExternalId
    {
        get
        {
            var raw = _httpContextAccessor.HttpContext?.User?.FindFirstValue(FacultyMemberExternalIdClaimType);
            return Guid.TryParse(raw, out var parsed) ? parsed : null;
        }
    }

    public bool HasPermission(string permissionCode)
    {
        return Permissions.Contains(permissionCode, StringComparer.OrdinalIgnoreCase);
    }

    public bool IsInRole(string roleName)
    {
        return Roles.Contains(roleName, StringComparer.OrdinalIgnoreCase);
    }
}
