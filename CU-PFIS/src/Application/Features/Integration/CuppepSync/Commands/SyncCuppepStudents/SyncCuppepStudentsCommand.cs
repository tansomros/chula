using System.Text.Json;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Common;
using Chula.Domain.Entities.Integration;
using Chula.Domain.Entities.ReferenceData;
using Chula.Domain.Entities.Students;
using Chula.Domain.Enums.Integration;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Integration.CuppepSync.Commands.SyncCuppepStudents;

[RequirePermission(PermissionConstants.StudentsCv.Edit)]
public sealed record SyncCuppepStudentsCommand(
    DateTimeOffset? UpdatedSince = null,
    int? MaxRecords = null) : IRequest<ImportBatchDto>;

public sealed record ImportBatchDto(
    Guid ExternalId,
    string SourceSystem,
    string EntityType,
    DateTimeOffset? UpdatedSinceUtc,
    DateTimeOffset StartedAt,
    DateTimeOffset? CompletedAt,
    string Status,
    int TotalPulledCount,
    int CreatedCount,
    int UpdatedCount,
    int SkippedUnchangedCount,
    int ConflictCount,
    string? ErrorMessage,
    string? TriggeredBy);

public sealed record StudentSyncPayloadSnapshot(
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
    string? HighSchoolEn);

public sealed class SyncCuppepStudentsCommandHandler
    : IRequestHandler<SyncCuppepStudentsCommand, ImportBatchDto>
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IChulaDbContext _context;
    private readonly ICuppepDataSource _cuppepDataSource;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public SyncCuppepStudentsCommandHandler(
        IChulaDbContext context,
        ICuppepDataSource cuppepDataSource,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _cuppepDataSource = cuppepDataSource;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task<ImportBatchDto> Handle(
        SyncCuppepStudentsCommand request,
        CancellationToken cancellationToken)
    {
        var startedAt = _timeProvider.GetUtcNow();
        var batch = new ImportBatch(
            "CUPPEP",
            "Student",
            startedAt,
            request.UpdatedSince,
            _currentUserService.LoginName ?? _currentUserService.UserId);

        _context.ImportBatches.Add(batch);
        await _context.SaveChangesAsync(cancellationToken);

        IReadOnlyList<CuppepStudentSnapshot> pulled;
        try
        {
            pulled = await _cuppepDataSource.PullStudentsAsync(
                request.UpdatedSince,
                request.MaxRecords,
                cancellationToken);
        }
        catch (Exception ex)
        {
            batch.Fail(ex.Message, _timeProvider.GetUtcNow());
            await _context.SaveChangesAsync(cancellationToken);
            throw new DomainException($"Failed to pull students from CU-PPEP API: {ex.Message}");
        }

        // Load reference lookup maps
        var majorsByCode = await _context.Majors
            .ToDictionaryAsync(x => x.Code.ToUpperInvariant(), cancellationToken);

        if (majorsByCode.Count == 0)
        {
            batch.Fail("No Majors are seeded in CU-PFIS ReferenceData.", _timeProvider.GetUtcNow());
            await _context.SaveChangesAsync(cancellationToken);
            throw new DomainException("ReferenceData Majors must be seeded before syncing students.");
        }

        var defaultMajor = majorsByCode.GetValueOrDefault("PC") ?? majorsByCode.Values.First();

        var academicYearsByBe = await _context.AcademicYears
            .ToDictionaryAsync(x => x.YearBe, cancellationToken);

        // Deduplicate incoming batch by StudentCode (keep latest UpdatedAt if source returns duplicates)
        var dedupedSnapshots = pulled
            .Where(s => !string.IsNullOrWhiteSpace(s.StudentCode))
            .GroupBy(s => s.StudentCode.Trim(), StringComparer.OrdinalIgnoreCase)
            .Select(g => g.OrderByDescending(x => x.UpdatedAt).First())
            .ToList();

        var incomingCodes = dedupedSnapshots
            .Select(x => x.StudentCode.Trim())
            .ToList();

        var existingStudents = await _context.Students
            .Include(x => x.Major)
            .Include(x => x.CohortAcademicYear)
            .Where(x => incomingCodes.Contains(x.StudentCode))
            .ToDictionaryAsync(x => x.StudentCode, StringComparer.OrdinalIgnoreCase, cancellationToken);

        var targetExternalIds = existingStudents.Values.Select(x => x.ExternalId).ToList();
        var existingPendingConflicts = await _context.DataConflictQueueItems
            .Where(x =>
                x.EntityType == "Student" &&
                x.Status == ConflictResolutionStatus.Pending &&
                targetExternalIds.Contains(x.TargetExternalId))
            .ToDictionaryAsync(x => x.TargetExternalId, cancellationToken);

        var now = _timeProvider.GetUtcNow();
        var created = 0;
        var updated = 0;
        var skipped = 0;
        var conflicts = 0;

        // Pre-create any missing historical AcademicYears (10+ year legacy CUPPEP data)
        var missingYears = dedupedSnapshots
            .Select(x => x.CohortYearBe)
            .Distinct()
            .Where(y => y >= 2500 && y <= 2650 && !academicYearsByBe.ContainsKey(y))
            .ToList();

        if (missingYears.Count > 0)
        {
            foreach (var yearBe in missingYears)
            {
                var yearCe = yearBe - 543;
                var ay = new AcademicYear(
                    yearBe,
                    new DateOnly(yearCe, 8, 1),
                    new DateOnly(yearCe + 1, 5, 31),
                    isCurrent: false);
                _context.AcademicYears.Add(ay);
                academicYearsByBe[yearBe] = ay;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        foreach (var snap in dedupedSnapshots)
        {
            var code = snap.StudentCode.Trim();
            var major = majorsByCode.GetValueOrDefault(snap.MajorCode.Trim().ToUpperInvariant()) ?? defaultMajor;
            var studyYear = Math.Clamp(snap.StudyYear, 1, 8);
            var firstNameTh = string.IsNullOrWhiteSpace(snap.FirstNameTh) ? code : snap.FirstNameTh.Trim();
            var lastNameTh = string.IsNullOrWhiteSpace(snap.LastNameTh) ? "-" : snap.LastNameTh.Trim();
            var firstNameEn = string.IsNullOrWhiteSpace(snap.FirstNameEn) ? firstNameTh : snap.FirstNameEn.Trim();
            var lastNameEn = string.IsNullOrWhiteSpace(snap.LastNameEn) ? lastNameTh : snap.LastNameEn.Trim();

            if (!existingStudents.TryGetValue(code, out var student))
            {
                if (!academicYearsByBe.TryGetValue(snap.CohortYearBe, out var cohortYear))
                {
                    cohortYear = academicYearsByBe.Values.OrderByDescending(x => x.YearBe).First();
                }

                var newStudent = new Student(
                    code,
                    firstNameTh,
                    lastNameTh,
                    firstNameEn,
                    lastNameEn,
                    cohortYear.Id,
                    studyYear,
                    major.Id,
                    departmentId: null,
                    admissionRoundId: null,
                    academicAdvisorId: null,
                    highSchoolTh: snap.HighSchoolTh,
                    highSchoolEn: snap.HighSchoolEn,
                    sourceSystem: "CUPPEP",
                    sourceRecordId: snap.SourceRecordId,
                    importedAt: now);

                _context.Students.Add(newStudent);
                existingStudents[code] = newStudent;
                created++;
                continue;
            }

            var diffs = student.GetExternalSyncDifferences(
                firstNameTh,
                lastNameTh,
                firstNameEn,
                lastNameEn,
                studyYear,
                major.Id,
                snap.HighSchoolTh,
                snap.HighSchoolEn);

            if (diffs.Count == 0)
            {
                skipped++;
                continue;
            }

            if (!student.IsStaffVerifiedEdit)
            {
                student.ApplyExternalSyncUpdate(
                    firstNameTh,
                    lastNameTh,
                    firstNameEn,
                    lastNameEn,
                    studyYear,
                    major.Id,
                    snap.HighSchoolTh,
                    snap.HighSchoolEn,
                    snap.SourceRecordId,
                    now);
                updated++;
            }
            else
            {
                // P01 Non-destructive rule: NEVER silently overwrite staff-verified student data!
                var currentPayload = new StudentSyncPayloadSnapshot(
                    student.SourceRecordId ?? student.StudentCode,
                    student.StudentCode,
                    student.FirstNameTh,
                    student.LastNameTh,
                    student.FirstNameEn,
                    student.LastNameEn,
                    student.CohortAcademicYear.YearBe,
                    student.StudyYear,
                    student.Major.Code,
                    student.HighSchoolTh,
                    student.HighSchoolEn);

                var incomingPayload = new StudentSyncPayloadSnapshot(
                    snap.SourceRecordId,
                    code,
                    firstNameTh,
                    lastNameTh,
                    firstNameEn,
                    lastNameEn,
                    snap.CohortYearBe,
                    studyYear,
                    major.Code,
                    snap.HighSchoolTh,
                    snap.HighSchoolEn);

                var currentJson = JsonSerializer.Serialize(currentPayload, JsonOptions);
                var incomingJson = JsonSerializer.Serialize(incomingPayload, JsonOptions);
                var summary = string.Join(", ", diffs);

                if (existingPendingConflicts.TryGetValue(student.ExternalId, out var pending))
                {
                    pending.RefreshIncomingSnapshot(batch.Id, currentJson, incomingJson, summary);
                }
                else
                {
                    var conflictItem = new DataConflictQueueItem(
                        batch.Id,
                        "CUPPEP",
                        "Student",
                        student.ExternalId,
                        student.StudentCode,
                        snap.SourceRecordId,
                        currentJson,
                        incomingJson,
                        summary);
                    _context.DataConflictQueueItems.Add(conflictItem);
                    existingPendingConflicts[student.ExternalId] = conflictItem;
                }

                conflicts++;
            }
        }

        batch.Complete(
            dedupedSnapshots.Count,
            created,
            updated,
            skipped,
            conflicts,
            _timeProvider.GetUtcNow());

        await _context.SaveChangesAsync(cancellationToken);

        return MapBatchDto(batch);
    }

    internal static ImportBatchDto MapBatchDto(ImportBatch batch) => new(
        batch.ExternalId,
        batch.SourceSystem,
        batch.EntityType,
        batch.UpdatedSinceUtc,
        batch.StartedAt,
        batch.CompletedAt,
        batch.Status.ToString(),
        batch.TotalPulledCount,
        batch.CreatedCount,
        batch.UpdatedCount,
        batch.SkippedUnchangedCount,
        batch.ConflictCount,
        batch.ErrorMessage,
        batch.TriggeredBy);
}
