using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;

namespace Chula.Application.FunctionalTests;

public sealed class TestCurrentUserService : ICurrentUserService
{
    public string? UserId { get; private set; } = "test-admin";

    public string? LoginName { get; private set; } = "admin.test";

    public IReadOnlyCollection<string> Roles { get; private set; } = new[] { RoleConstants.Admin };

    public IReadOnlyCollection<string> Permissions { get; private set; } = Array.Empty<string>();

    public Guid? FacultyMemberExternalId { get; private set; }

    public Guid? DepartmentExternalId { get; private set; }

    public bool HasPermission(string permissionCode) =>
        Permissions.Contains(permissionCode, StringComparer.OrdinalIgnoreCase);

    public bool IsInRole(string roleName) =>
        Roles.Contains(roleName, StringComparer.OrdinalIgnoreCase);

    public void RunAsAdmin(string userId = "test-admin", string loginName = "admin.test")
    {
        UserId = userId;
        LoginName = loginName;
        Roles = new[] { RoleConstants.Admin };
        Permissions = new[]
        {
            PermissionConstants.ReferenceData.View,
            PermissionConstants.ReferenceData.Manage,
            PermissionConstants.Faculty.ViewAll,
            PermissionConstants.Faculty.EditAll,
            PermissionConstants.Students.View,
            PermissionConstants.Students.Edit,
            PermissionConstants.TrainingRequests.ViewAll,
            PermissionConstants.TrainingRequests.Verify,
            PermissionConstants.TrainingRequests.Approve,
            PermissionConstants.TrainingRequests.ViewBankAccount,
            PermissionConstants.TrainingRequests.Export,
            PermissionConstants.TrainingBudgetPolicies.Manage
        };
        DepartmentExternalId = null;
        FacultyMemberExternalId = null;
    }

    public void RunAsDepartmentStaff(
        Guid departmentExternalId,
        string userId = "test-staff",
        string loginName = "staff.test")
    {
        UserId = userId;
        LoginName = loginName;
        Roles = new[] { RoleConstants.Staff };
        Permissions = new[]
        {
            PermissionConstants.ReferenceData.View,
            PermissionConstants.Faculty.ViewAll,
            PermissionConstants.Faculty.EditAll,
            PermissionConstants.Students.View,
            PermissionConstants.Students.Edit,
            PermissionConstants.TrainingRequests.ViewAll,
            PermissionConstants.TrainingRequests.Verify,
            PermissionConstants.TrainingRequests.Approve,
            PermissionConstants.TrainingRequests.Export
        };
        DepartmentExternalId = departmentExternalId;
        FacultyMemberExternalId = null;
    }

    public void RunAsFaculty(
        Guid facultyMemberExternalId,
        string userId = "test-faculty",
        string loginName = "faculty.test")
    {
        UserId = userId;
        LoginName = loginName;
        Roles = new[] { RoleConstants.Faculty };
        Permissions = new[]
        {
            PermissionConstants.ReferenceData.View,
            PermissionConstants.Faculty.ViewOwn,
            PermissionConstants.Faculty.EditOwn,
            PermissionConstants.TrainingRequests.ViewOwn,
            PermissionConstants.TrainingRequests.CreateOwn,
            PermissionConstants.TrainingRequests.UpdateOwn
        };
        DepartmentExternalId = null;
        FacultyMemberExternalId = facultyMemberExternalId;
    }
}
