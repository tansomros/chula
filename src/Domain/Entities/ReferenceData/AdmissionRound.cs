using Ardalis.GuardClauses;
using Chula.Domain.Common;
using Chula.Domain.Enums.ReferenceData;

namespace Chula.Domain.Entities.ReferenceData;

/// <summary>
/// 2-tier admission round hierarchy (Portfolio -> Port โอลิมปิกวิชาการ / Port ภาษาอังกฤษ, Quota -> ..., กสพท.).
/// </summary>
public sealed class AdmissionRound : SoftDeletableEntityBase, IHasActiveFlag
{
    private readonly List<AdmissionRound> _subRounds = new();

    private AdmissionRound()
    {
    }

    public AdmissionRound(
        string code,
        string nameTh,
        string nameEn,
        AdmissionRoundTier tier,
        int? parentRoundId = null,
        int sortOrder = 0)
    {
        Update(
            code,
            nameTh,
            nameEn,
            tier,
            parentRoundId,
            sortOrder);
        IsActive = true;
    }

    public string Code { get; private set; } = string.Empty;

    public string NameTh { get; private set; } = string.Empty;

    public string NameEn { get; private set; } = string.Empty;

    public AdmissionRoundTier Tier { get; private set; }

    public int? ParentRoundId { get; private set; }

    public AdmissionRound? ParentRound { get; private set; }

    public IReadOnlyCollection<AdmissionRound> SubRounds => _subRounds.AsReadOnly();

    public int SortOrder { get; private set; }

    public bool IsActive { get; private set; } = true;

    public void Update(
        string code,
        string nameTh,
        string nameEn,
        AdmissionRoundTier tier,
        int? parentRoundId,
        int sortOrder)
    {
        Code = Guard.Against.NullOrWhiteSpace(code, nameof(code)).Trim().ToUpperInvariant();
        NameTh = Guard.Against.NullOrWhiteSpace(nameTh, nameof(nameTh)).Trim();
        NameEn = Guard.Against.NullOrWhiteSpace(nameEn, nameof(nameEn)).Trim();
        Tier = Guard.Against.EnumOutOfRange(tier, nameof(tier));
        SortOrder = Guard.Against.Negative(sortOrder, nameof(sortOrder));

        if (tier == AdmissionRoundTier.Tier1Category && parentRoundId.HasValue)
        {
            throw new DomainException("หมวดรอบรับเข้าหลัก (Tier1Category) ต้องไม่มี ParentRoundId.");
        }

        if (tier == AdmissionRoundTier.Tier2SubRound && (!parentRoundId.HasValue || parentRoundId.Value <= 0))
        {
            throw new DomainException("รอบรับเข้าย่อย (Tier2SubRound) ต้องระบุ ParentRoundId ที่ถูกต้อง.");
        }

        ParentRoundId = parentRoundId;
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
