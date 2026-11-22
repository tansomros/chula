using Ardalis.GuardClauses;
using Chula.Domain.Common;

namespace Chula.Domain.Entities.ReferenceData;

public sealed class AcademicPosition : SoftDeletableEntityBase, IHasActiveFlag
{
    private AcademicPosition()
    {
    }

    public AcademicPosition(
        string code,
        string titleTh,
        string titleEn,
        string shortTitleTh,
        string shortTitleEn,
        int sortOrder = 0)
    {
        Update(
            code,
            titleTh,
            titleEn,
            shortTitleTh,
            shortTitleEn,
            sortOrder);
        IsActive = true;
    }

    public string Code { get; private set; } = string.Empty;

    public string TitleTh { get; private set; } = string.Empty;

    public string TitleEn { get; private set; } = string.Empty;

    public string ShortTitleTh { get; private set; } = string.Empty;

    public string ShortTitleEn { get; private set; } = string.Empty;

    public int SortOrder { get; private set; }

    public bool IsActive { get; private set; } = true;

    public void Update(
        string code,
        string titleTh,
        string titleEn,
        string shortTitleTh,
        string shortTitleEn,
        int sortOrder)
    {
        Code = Guard.Against.NullOrWhiteSpace(code, nameof(code)).Trim().ToUpperInvariant();
        TitleTh = Guard.Against.NullOrWhiteSpace(titleTh, nameof(titleTh)).Trim();
        TitleEn = Guard.Against.NullOrWhiteSpace(titleEn, nameof(titleEn)).Trim();
        ShortTitleTh = Guard.Against.NullOrWhiteSpace(shortTitleTh, nameof(shortTitleTh)).Trim();
        ShortTitleEn = Guard.Against.NullOrWhiteSpace(shortTitleEn, nameof(shortTitleEn)).Trim();
        SortOrder = Guard.Against.Negative(sortOrder, nameof(sortOrder));
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
