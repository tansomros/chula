using Ardalis.GuardClauses;
using Chula.Domain.Common;

namespace Chula.Domain.Entities.Security;

public sealed class Permission : SoftDeletableEntityBase, IHasActiveFlag
{
    private readonly List<RolePermission> _rolePermissions = new();

    private Permission()
    {
    }

    public Permission(
        string code,
        string module,
        string nameTh,
        string nameEn,
        string? description = null)
    {
        Update(
            code,
            module,
            nameTh,
            nameEn,
            description);
        IsActive = true;
    }

    public string Code { get; private set; } = string.Empty;

    public string Module { get; private set; } = string.Empty;

    public string NameTh { get; private set; } = string.Empty;

    public string NameEn { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; } = true;

    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    public void Update(
        string code,
        string module,
        string nameTh,
        string nameEn,
        string? description = null)
    {
        var normalizedCode = Guard.Against.NullOrWhiteSpace(code, nameof(code)).Trim().ToLowerInvariant();
        if (!normalizedCode.Contains('.'))
        {
            throw new DomainException("รหัสสิทธิ์ (Permission Code) ต้องอยู่ในรูปแบบ 'module.action' (เช่น 'training-requests.verify').");
        }

        Code = normalizedCode;
        Module = Guard.Against.NullOrWhiteSpace(module, nameof(module)).Trim();
        NameTh = Guard.Against.NullOrWhiteSpace(nameTh, nameof(nameTh)).Trim();
        NameEn = Guard.Against.NullOrWhiteSpace(nameEn, nameof(nameEn)).Trim();
        Description = description?.Trim();
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
