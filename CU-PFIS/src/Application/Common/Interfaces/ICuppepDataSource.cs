namespace Chula.Application.Common.Interfaces;

/// <summary>
/// Port for pulling incremental student records (including advisor &amp; internship rotations)
/// from CU-PPEP MS SQL Server (chula_pps) via a read-only adapter (Decision D11 — Option 1B).
/// Keeps Application/Domain layers completely decoupled from MS SQL Server details.
/// </summary>
public interface ICuppepDataSource
{
    Task<CuppepHealthStatus> CheckHealthAsync(CancellationToken cancellationToken);

    Task<IReadOnlyList<CuppepStudentSnapshot>> PullStudentsAsync(
        DateTimeOffset? updatedSince,
        int? maxRecords = null,
        CancellationToken cancellationToken = default);
}

public sealed record CuppepHealthStatus(
    bool IsReachable,
    string BaseUrl,
    string? Message = null);

public sealed record CuppepInternshipSnapshot(
    string SourceRecordId,
    string SiteTh,
    string? SiteEn,
    string RotationTypeTh,
    string? RotationTypeEn,
    string PeriodLabel);

public sealed record CuppepStudentSnapshot(
    string SourceRecordId,
    string StudentCode,
    string FirstNameTh,
    string LastNameTh,
    string FirstNameEn,
    string LastNameEn,
    int CohortYearBe,
    int StudyYear,
    string MajorCode,
    string? HighSchoolTh,
    string? HighSchoolEn,
    DateTimeOffset UpdatedAt,
    string? AcademicAdvisorNameTh = null,
    string? AcademicAdvisorFirstNameTh = null,
    string? AcademicAdvisorLastNameTh = null,
    string? AcademicAdvisorEmail = null,
    IReadOnlyList<CuppepInternshipSnapshot>? Internships = null);
