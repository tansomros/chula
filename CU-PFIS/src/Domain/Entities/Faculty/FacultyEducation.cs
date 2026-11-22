using Ardalis.GuardClauses;
using Chula.Domain.Common;

namespace Chula.Domain.Entities.Faculty;

public sealed class FacultyEducation : EntityBase
{
    private FacultyEducation()
    {
    }

    public FacultyEducation(
        string degreeTh,
        string institutionTh,
        int graduationYear,
        string? degreeEn = null,
        string? institutionEn = null,
        int sortOrder = 0)
    {
        DegreeTh = Guard.Against.NullOrWhiteSpace(degreeTh, nameof(degreeTh)).Trim();
        InstitutionTh = Guard.Against.NullOrWhiteSpace(institutionTh, nameof(institutionTh)).Trim();
        GraduationYear = Guard.Against.OutOfRange(graduationYear, nameof(graduationYear), 1950, 2600);
        DegreeEn = string.IsNullOrWhiteSpace(degreeEn) ? null : degreeEn.Trim();
        InstitutionEn = string.IsNullOrWhiteSpace(institutionEn) ? null : institutionEn.Trim();
        SortOrder = sortOrder;
    }

    public int FacultyMemberId { get; private set; }

    public FacultyMember FacultyMember { get; private set; } = null!;

    public string DegreeTh { get; private set; } = string.Empty;

    public string? DegreeEn { get; private set; }

    public string InstitutionTh { get; private set; } = string.Empty;

    public string? InstitutionEn { get; private set; }

    public int GraduationYear { get; private set; }

    public int SortOrder { get; private set; }
}
