using Ardalis.GuardClauses;
using Chula.Domain.Common;

namespace Chula.Domain.Entities.Students;

public sealed class StudentInternship : EntityBase
{
    private StudentInternship()
    {
    }

    public StudentInternship(
        string siteTh,
        string rotationTypeTh,
        string periodLabel,
        string? siteEn = null,
        string? rotationTypeEn = null,
        string? sourceRecordId = null)
    {
        SiteTh = Guard.Against.NullOrWhiteSpace(siteTh, nameof(siteTh)).Trim();
        RotationTypeTh = Guard.Against.NullOrWhiteSpace(rotationTypeTh, nameof(rotationTypeTh)).Trim();
        PeriodLabel = Guard.Against.NullOrWhiteSpace(periodLabel, nameof(periodLabel)).Trim();
        SiteEn = string.IsNullOrWhiteSpace(siteEn) ? null : siteEn.Trim();
        RotationTypeEn = string.IsNullOrWhiteSpace(rotationTypeEn) ? null : rotationTypeEn.Trim();
        SourceRecordId = string.IsNullOrWhiteSpace(sourceRecordId) ? null : sourceRecordId.Trim();
    }

    public int StudentId { get; private set; }

    public Student Student { get; private set; } = null!;

    public string SiteTh { get; private set; } = string.Empty;

    public string? SiteEn { get; private set; }

    public string RotationTypeTh { get; private set; } = string.Empty;

    public string? RotationTypeEn { get; private set; }

    public string PeriodLabel { get; private set; } = string.Empty;

    public string? SourceRecordId { get; private set; }
}
