namespace Chula.Application.Common.Interfaces;

/// <summary>
/// Port for pulling incremental student records from the separate .NET 10 CU-PPEP Integration API (Decision D11).
/// CU-PFIS never queries raw MS SQL Server tables directly.
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
    DateTimeOffset UpdatedAt);
