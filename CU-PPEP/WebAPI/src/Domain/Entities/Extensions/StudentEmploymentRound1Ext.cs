using Ardalis.GuardClauses;
using CUPPEP.Domain.Common;

namespace CUPPEP.Domain.Entities.Extensions;

/// <summary>
/// Graduate employment Round 1 (faculty survey) entered by students in CU-PPEP (A07).
/// </summary>
public sealed class StudentEmploymentRound1Ext : EntityBase
{
    private StudentEmploymentRound1Ext()
    {
    }

    public StudentEmploymentRound1Ext(
        string studentCode,
        int graduationYearBe,
        string employmentStatus,
        string? jobTitle = null,
        string? workplaceName = null,
        string? surveyAnswersJson = null)
    {
        StudentCode = Guard.Against.NullOrWhiteSpace(studentCode, nameof(studentCode)).Trim();
        Update(graduationYearBe, employmentStatus, jobTitle, workplaceName, surveyAnswersJson);
    }

    public string StudentCode { get; private set; } = string.Empty;

    public int GraduationYearBe { get; private set; }

    public string EmploymentStatus { get; private set; } = string.Empty;

    public string? JobTitle { get; private set; }

    public string? WorkplaceName { get; private set; }

    public string? SurveyAnswersJson { get; private set; }

    public void Update(
        int graduationYearBe,
        string employmentStatus,
        string? jobTitle,
        string? workplaceName,
        string? surveyAnswersJson)
    {
        GraduationYearBe = Guard.Against.OutOfRange(graduationYearBe, nameof(graduationYearBe), 2500, 2700);
        EmploymentStatus = Guard.Against.NullOrWhiteSpace(employmentStatus, nameof(employmentStatus)).Trim();
        JobTitle = string.IsNullOrWhiteSpace(jobTitle) ? null : jobTitle.Trim();
        WorkplaceName = string.IsNullOrWhiteSpace(workplaceName) ? null : workplaceName.Trim();
        SurveyAnswersJson = string.IsNullOrWhiteSpace(surveyAnswersJson) ? null : surveyAnswersJson.Trim();
    }
}
