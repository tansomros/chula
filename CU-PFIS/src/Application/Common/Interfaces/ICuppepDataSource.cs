namespace Chula.Application.Common.Interfaces;

/// <summary>
/// Port for pulling incremental student records from the separate .NET 10 CU-PPEP Integration API (Decision D11).
/// CU-PFIS never queries raw MS SQL Server tables directly.
/// </summary>
public interface ICuppepDataSource
{
    Task<IReadOnlyList<CuppepStudentSnapshot>> PullStudentsAsync(
        DateTimeOffset? updatedSince,
        CancellationToken cancellationToken);
}

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
