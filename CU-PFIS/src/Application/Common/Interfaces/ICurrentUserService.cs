namespace Chula.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }

    string? LoginName { get; }

    IReadOnlyCollection<string> Roles { get; }

    IReadOnlyCollection<string> Permissions { get; }

    Guid? FacultyMemberExternalId { get; }

    Guid? DepartmentExternalId { get; }

    bool HasPermission(string permissionCode);

    bool IsInRole(string roleName);
}
