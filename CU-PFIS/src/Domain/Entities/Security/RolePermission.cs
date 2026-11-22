using Ardalis.GuardClauses;
using Chula.Domain.Common;

namespace Chula.Domain.Entities.Security;

public sealed class RolePermission : EntityBase
{
    private RolePermission()
    {
    }

    public RolePermission(string roleName, int permissionId)
    {
        RoleName = Guard.Against.NullOrWhiteSpace(roleName, nameof(roleName)).Trim();
        PermissionId = Guard.Against.NegativeOrZero(permissionId, nameof(permissionId));
    }

    public string RoleName { get; private set; } = string.Empty;

    public int PermissionId { get; private set; }

    public Permission? Permission { get; private set; }
}
