using Ardalis.GuardClauses;
using CUPPEP.Domain.Common;

namespace CUPPEP.Domain.Entities.Extensions;

/// <summary>
/// Student achievements & awards self-confirmed in CU-PPEP (A02, A08: no file attachment required).
/// </summary>
public sealed class StudentAchievementExt : SoftDeletableEntityBase
{
    private StudentAchievementExt()
    {
    }

    public StudentAchievementExt(
        string studentCode,
        int academicYearBe,
        int studyYear,
        string eventName,
        string awardName,
        DateOnly eventDate,
        decimal? grantAmount = null)
    {
        StudentCode = Guard.Against.NullOrWhiteSpace(studentCode, nameof(studentCode)).Trim();
        Update(academicYearBe, studyYear, eventName, awardName, eventDate, grantAmount);
    }

    public string StudentCode { get; private set; } = string.Empty;

    public int AcademicYearBe { get; private set; }

    public int StudyYear { get; private set; }

    public string EventName { get; private set; } = string.Empty;

    public string AwardName { get; private set; } = string.Empty;

    public DateOnly EventDate { get; private set; }

    public decimal? GrantAmount { get; private set; }

    public void Update(
        int academicYearBe,
        int studyYear,
        string eventName,
        string awardName,
        DateOnly eventDate,
        decimal? grantAmount)
    {
        AcademicYearBe = Guard.Against.OutOfRange(academicYearBe, nameof(academicYearBe), 2500, 2700);
        StudyYear = Guard.Against.OutOfRange(studyYear, nameof(studyYear), 1, 8);
        EventName = Guard.Against.NullOrWhiteSpace(eventName, nameof(eventName)).Trim();
        AwardName = Guard.Against.NullOrWhiteSpace(awardName, nameof(awardName)).Trim();
        EventDate = eventDate;
        if (grantAmount.HasValue && grantAmount.Value < 0)
        {
            throw new DomainException("GrantAmount cannot be negative.");
        }

        GrantAmount = grantAmount;
    }
}
