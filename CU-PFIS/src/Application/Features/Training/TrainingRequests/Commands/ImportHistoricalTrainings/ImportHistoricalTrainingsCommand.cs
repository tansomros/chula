using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Application.Features.Training.Common;
using Chula.Domain.Entities.Training;
using Chula.Domain.Enums.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.ImportHistoricalTrainings;

/// <summary>
/// Historical Training Excel/CSV Migration import (Decision A04).
/// Matches faculty by stable PersonnelCode, deduplicates idempotently via SourceRecordId,
/// and writes append-only ledger entries for AcademicFunded historical items when DeductFromBudgetLedger is enabled.
/// </summary>
[RequirePermission(PermissionConstants.TrainingRequests.Approve)]
public sealed record ImportHistoricalTrainingsCommand(
    IReadOnlyList<HistoricalTrainingImportItem> Items,
    bool DeductFromBudgetLedger = true,
    string SourceSystem = "HistoricalExcel") : IRequest<ImportHistoricalTrainingsResultDto>;

public sealed record HistoricalTrainingImportItem(
    string SourceRecordId,
    string PersonnelCode,
    TrainingCategory Category,
    TrainingWorkType WorkType,
    string TitleTh,
    string? TitleEn,
    string OrganizerAndVenueTh,
    string? OrganizerAndVenueEn,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal ApprovedAmount);

public sealed record ImportHistoricalTrainingsResultDto(
    int ImportedCount,
    int SkippedDuplicateCount,
    int FailedCount,
    IReadOnlyList<string> Errors);

public sealed class ImportHistoricalTrainingsCommandHandler
    : IRequestHandler<ImportHistoricalTrainingsCommand, ImportHistoricalTrainingsResultDto>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public ImportHistoricalTrainingsCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task<ImportHistoricalTrainingsResultDto> Handle(
        ImportHistoricalTrainingsCommand request,
        CancellationToken cancellationToken)
    {
        var isAdmin = _currentUserService.IsInRole(RoleConstants.Admin);
        var scopedDept = !isAdmin ? _currentUserService.DepartmentExternalId : null;

        var facultyByCode = await _context.FacultyMembers
            .Include(x => x.Department)
            .ToDictionaryAsync(x => x.PersonnelCode.Trim(), StringComparer.OrdinalIgnoreCase, cancellationToken);

        var sourceIds = request.Items
            .Select(x => x.SourceRecordId.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var existingSourceIds = (await _context.TrainingRequests
            .AsNoTracking()
            .Where(x => x.SourceRecordId != null && sourceIds.Contains(x.SourceRecordId))
            .Select(x => x.SourceRecordId!)
            .ToListAsync(cancellationToken))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var actor = _currentUserService.LoginName ?? _currentUserService.UserId ?? "import";
        var now = _timeProvider.GetUtcNow();
        var imported = 0;
        var skipped = 0;
        var errors = new List<string>();

        foreach (var item in request.Items)
        {
            var sourceKey = item.SourceRecordId.Trim();
            if (existingSourceIds.Contains(sourceKey))
            {
                skipped++;
                continue;
            }

            if (!facultyByCode.TryGetValue(item.PersonnelCode.Trim(), out var faculty))
            {
                errors.Add($"[{sourceKey}] PersonnelCode '{item.PersonnelCode}' not found.");
                continue;
            }

            if (scopedDept.HasValue && faculty.Department.ExternalId != scopedDept.Value)
            {
                errors.Add($"[{sourceKey}] PersonnelCode '{item.PersonnelCode}' is outside your assigned department.");
                continue;
            }

            if (item.EndDate < item.StartDate)
            {
                errors.Add($"[{sourceKey}] EndDate cannot be earlier than StartDate.");
                continue;
            }

            var entity = new TrainingRequest(
                faculty.Id,
                item.Category,
                item.WorkType,
                item.TitleTh,
                item.OrganizerAndVenueTh,
                item.StartDate,
                item.EndDate,
                item.ApprovedAmount,
                item.TitleEn,
                organizerAndVenueEn: item.OrganizerAndVenueEn,
                sourceSystem: request.SourceSystem,
                sourceRecordId: sourceKey,
                importedAt: now);

            entity.RecordDirectlyAsApproved(
                actor,
                item.ApprovedAmount,
                now,
                $"Historical import from {request.SourceSystem} ({sourceKey}).");

            _context.TrainingRequests.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.DeductFromBudgetLedger &&
                item.Category == TrainingCategory.AcademicFunded &&
                item.ApprovedAmount > 0m)
            {
                var (currentPolicy, nextPolicy) = await TrainingBudgetCalculator.EnsurePoliciesAsync(
                    _context,
                    entity.FiscalYearBe,
                    cancellationToken);

                var ledger = await _context.TrainingBudgetLedgerEntries
                    .Where(x =>
                        x.FacultyMemberId == faculty.Id &&
                        (x.FiscalYearBe == entity.FiscalYearBe || x.FiscalYearBe == entity.FiscalYearBe + 1))
                    .ToListAsync(cancellationToken);

                var allocations = await _context.FacultyTrainingBudgetAllocations
                    .AsNoTracking()
                    .Where(x =>
                        x.FacultyMemberId == faculty.Id &&
                        (x.FiscalYearBe == entity.FiscalYearBe || x.FiscalYearBe == entity.FiscalYearBe + 1))
                    .ToListAsync(cancellationToken);

                var balance = TrainingBudgetCalculator.ComputeBalance(
                    entity.FiscalYearBe,
                    currentPolicy,
                    nextPolicy,
                    ledger,
                    allocations.FirstOrDefault(x => x.FiscalYearBe == entity.FiscalYearBe),
                    allocations.FirstOrDefault(x => x.FiscalYearBe == entity.FiscalYearBe + 1));

                // For historical records, record Consume (and BorrowAdvance if within cap, otherwise direct Consume)
                if (item.ApprovedAmount <= balance.TotalAvailableIncludingBorrow)
                {
                    var entries = TrainingBudgetCalculator.CreateDeductionEntries(
                        entity,
                        item.ApprovedAmount,
                        balance,
                        actor,
                        now);
                    foreach (var entry in entries)
                    {
                        _context.TrainingBudgetLedgerEntries.Add(entry);
                    }
                }
                else
                {
                    _context.TrainingBudgetLedgerEntries.Add(new TrainingBudgetLedgerEntry(
                        faculty.Id,
                        entity.Id,
                        entity.FiscalYearBe,
                        BudgetLedgerEntryType.Consume,
                        item.ApprovedAmount,
                        now,
                        actor,
                        $"Historical imported spend: {entity.TitleTh}"));
                }

                await _context.SaveChangesAsync(cancellationToken);
            }

            existingSourceIds.Add(sourceKey);
            imported++;
        }

        return new ImportHistoricalTrainingsResultDto(imported, skipped, errors.Count, errors);
    }
}
