using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Application.Features.Training.Common;
using Chula.Domain.Entities.Training;
using Chula.Domain.Enums.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Queries.GetFacultyBudgetSummaries;

/// <summary>
/// Staff View 1 ("ข้อมูลอบรมของอาจารย์" per req071169.pdf pages 2–4):
/// Displays each faculty member's fiscal-year quota, amount used, future quota borrowed,
/// carry-forward deducted from prior year, and remaining balance derived strictly from TrainingBudgetLedger.
/// </summary>
public sealed record GetFacultyBudgetSummariesQuery(int? FiscalYearBe = null)
    : IRequest<FacultyBudgetSummaryResponseDto>;

public sealed record FacultyBudgetSummaryResponseDto(
    int FiscalYearBe,
    DateOnly FiscalYearStartDate,
    DateOnly FiscalYearEndDate,
    decimal PolicyAnnualQuota,
    bool AllowFutureQuotaBorrowing,
    decimal PolicyMaxBorrowAmount,
    IReadOnlyList<FacultyBudgetRowDto> Items);

public sealed record FacultyBudgetRowDto(
    Guid FacultyMemberExternalId,
    string PersonnelCode,
    string FacultyNameTh,
    string FacultyNameEn,
    string AcademicPositionTitleTh,
    string AcademicPositionTitleEn,
    Guid DepartmentExternalId,
    string DepartmentNameTh,
    string DepartmentNameEn,
    decimal AnnualQuota,
    decimal ConsumedInYear,
    decimal BorrowedFromNextYear,
    decimal CarryForwardDeductedInYear,
    decimal TotalApprovedSpendInYear,
    decimal RemainingInYear,
    decimal BorrowableFromNextYear,
    int ApprovedTrainingsCount,
    bool HasCustomAllocation,
    decimal? CustomMaxBorrowAmount,
    string? AllocationNote);

public sealed class GetFacultyBudgetSummariesQueryHandler
    : IRequestHandler<GetFacultyBudgetSummariesQuery, FacultyBudgetSummaryResponseDto>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public GetFacultyBudgetSummariesQueryHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task<FacultyBudgetSummaryResponseDto> Handle(
        GetFacultyBudgetSummariesQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var isAdmin = _currentUserService.IsInRole(RoleConstants.Admin);
        var canViewAll = isAdmin || _currentUserService.HasPermission(PermissionConstants.TrainingRequests.ViewAll);
        var canViewOwn = _currentUserService.HasPermission(PermissionConstants.TrainingRequests.ViewOwn);

        if (!canViewAll && !canViewOwn)
        {
            throw new ForbiddenAccessException("Missing permission to view training budgets.");
        }

        var targetFy = request.FiscalYearBe
            ?? FiscalYearCalculator.FromDate(DateOnly.FromDateTime(_timeProvider.GetUtcNow().UtcDateTime));

        var (currentPolicy, nextPolicy) = await TrainingBudgetCalculator.EnsurePoliciesAsync(
            _context,
            targetFy,
            cancellationToken);

        var facultyQuery = _context.FacultyMembers
            .AsNoTracking()
            .Include(x => x.Department)
            .Include(x => x.AcademicPosition)
            .Where(x => x.IsActive);

        if (!canViewAll)
        {
            if (_currentUserService.FacultyMemberExternalId is null)
            {
                return new FacultyBudgetSummaryResponseDto(
                    targetFy,
                    currentPolicy.StartDate,
                    currentPolicy.EndDate,
                    currentPolicy.AnnualQuotaAmount,
                    currentPolicy.AllowFutureQuotaBorrowing,
                    currentPolicy.MaxBorrowAmount,
                    Array.Empty<FacultyBudgetRowDto>());
            }

            var ownId = _currentUserService.FacultyMemberExternalId.Value;
            facultyQuery = facultyQuery.Where(x => x.ExternalId == ownId);
        }
        else if (!isAdmin && _currentUserService.DepartmentExternalId.HasValue)
        {
            var deptId = _currentUserService.DepartmentExternalId.Value;
            facultyQuery = facultyQuery.Where(x => x.Department.ExternalId == deptId);
        }

        var facultyMembers = await facultyQuery
            .OrderBy(x => x.PersonnelCode)
            .ToListAsync(cancellationToken);

        var facultyIds = facultyMembers.Select(x => x.Id).ToList();

        var ledgerEntries = await _context.TrainingBudgetLedgerEntries
            .AsNoTracking()
            .Where(x =>
                facultyIds.Contains(x.FacultyMemberId) &&
                (x.FiscalYearBe == targetFy || x.FiscalYearBe == targetFy + 1))
            .ToListAsync(cancellationToken);

        var ledgerByFaculty = ledgerEntries
            .GroupBy(x => x.FacultyMemberId)
            .ToDictionary(g => g.Key, g => (IReadOnlyCollection<TrainingBudgetLedgerEntry>)g.ToList());

        var allocations = await _context.FacultyTrainingBudgetAllocations
            .AsNoTracking()
            .Where(x =>
                facultyIds.Contains(x.FacultyMemberId) &&
                (x.FiscalYearBe == targetFy || x.FiscalYearBe == targetFy + 1))
            .ToListAsync(cancellationToken);

        var currentAllocByFaculty = allocations
            .Where(x => x.FiscalYearBe == targetFy)
            .ToDictionary(x => x.FacultyMemberId);

        var nextAllocByFaculty = allocations
            .Where(x => x.FiscalYearBe == targetFy + 1)
            .ToDictionary(x => x.FacultyMemberId);

        var approvedCountByFaculty = await _context.TrainingRequests
            .AsNoTracking()
            .Where(x =>
                facultyIds.Contains(x.FacultyMemberId) &&
                x.FiscalYearBe == targetFy &&
                x.Status == TrainingRequestStatus.Approved)
            .GroupBy(x => x.FacultyMemberId)
            .Select(g => new { FacultyId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.FacultyId, x => x.Count, cancellationToken);

        var items = facultyMembers.Select(f =>
        {
            var entries = ledgerByFaculty.GetValueOrDefault(f.Id) ?? Array.Empty<TrainingBudgetLedgerEntry>();
            var currentAlloc = currentAllocByFaculty.GetValueOrDefault(f.Id);
            var nextAlloc = nextAllocByFaculty.GetValueOrDefault(f.Id);
            var bal = TrainingBudgetCalculator.ComputeBalance(
                targetFy,
                currentPolicy,
                nextPolicy,
                entries,
                currentAlloc,
                nextAlloc);

            return new FacultyBudgetRowDto(
                f.ExternalId,
                f.PersonnelCode,
                $"{f.ProfessionalTitleTh}{f.FirstNameTh} {f.LastNameTh}".Trim(),
                $"{f.ProfessionalTitleEn} {f.FirstNameEn} {f.LastNameEn}".Trim(),
                f.AcademicPosition.TitleTh,
                f.AcademicPosition.TitleEn,
                f.Department.ExternalId,
                f.Department.NameTh,
                f.Department.NameEn,
                bal.AnnualQuota,
                bal.ConsumedInYear,
                bal.BorrowedFromNextYear,
                bal.CarryForwardDeductedInYear,
                bal.TotalApprovedSpendInYear,
                bal.RemainingInYear,
                bal.BorrowableFromNextYear,
                approvedCountByFaculty.GetValueOrDefault(f.Id),
                bal.HasCustomAllocation,
                currentAlloc?.MaxBorrowAmount,
                bal.AllocationNote);
        }).ToList();

        return new FacultyBudgetSummaryResponseDto(
            targetFy,
            currentPolicy.StartDate,
            currentPolicy.EndDate,
            currentPolicy.AnnualQuotaAmount,
            currentPolicy.AllowFutureQuotaBorrowing,
            currentPolicy.MaxBorrowAmount,
            items);
    }
}
