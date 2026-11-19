using Ardalis.GuardClauses;
using Chula.Domain.Common;

namespace Chula.Domain.Entities.ReferenceData;

public sealed class Department : SoftDeletableEntityBase, IHasActiveFlag
{
    private Department()
    {
    }

    public Department(string code, string nameTh, string nameEn)
    {
        Update(code, nameTh, nameEn);
        IsActive = true;
    }

    public string Code { get; private set; } = string.Empty;

    public string NameTh { get; private set; } = string.Empty;

    public string NameEn { get; private set; } = string.Empty;

    public bool IsActive { get; private set; } = true;

    public void Update(string code, string nameTh, string nameEn)
    {
        Code = Guard.Against.NullOrWhiteSpace(code, nameof(code)).Trim().ToUpperInvariant();
        NameTh = Guard.Against.NullOrWhiteSpace(nameTh, nameof(nameTh)).Trim();
        NameEn = Guard.Against.NullOrWhiteSpace(nameEn, nameof(nameEn)).Trim();
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
