using Ardalis.GuardClauses;
using CUPPEP.Domain.Common;

namespace CUPPEP.Domain.Entities.Extensions;

/// <summary>
/// Student profile extension entered in CU-PPEP (A02): 2-tier admission round & senior project titles.
/// </summary>
public sealed class StudentProfileExt : SoftDeletableEntityBase
{
    private StudentProfileExt()
    {
    }

    public StudentProfileExt(
        string studentCode,
        string? admissionRoundCode = null,
        string? seniorProjectTitleTh = null,
        string? seniorProjectTitleEn = null)
    {
        StudentCode = Guard.Against.NullOrWhiteSpace(studentCode, nameof(studentCode)).Trim();
        Update(admissionRoundCode, seniorProjectTitleTh, seniorProjectTitleEn);
    }

    public string StudentCode { get; private set; } = string.Empty;

    public string? AdmissionRoundCode { get; private set; }

    public string? SeniorProjectTitleTh { get; private set; }

    public string? SeniorProjectTitleEn { get; private set; }

    public void Update(
        string? admissionRoundCode,
        string? seniorProjectTitleTh,
        string? seniorProjectTitleEn)
    {
        AdmissionRoundCode = string.IsNullOrWhiteSpace(admissionRoundCode) ? null : admissionRoundCode.Trim().ToUpperInvariant();
        SeniorProjectTitleTh = string.IsNullOrWhiteSpace(seniorProjectTitleTh) ? null : seniorProjectTitleTh.Trim();
        SeniorProjectTitleEn = string.IsNullOrWhiteSpace(seniorProjectTitleEn) ? null : seniorProjectTitleEn.Trim();
    }
}
