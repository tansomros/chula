using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Application.Features.Training.Common;
using Chula.Domain.Entities.Training;
using Chula.Domain.Enums.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.ApproveTrainingRequest;

/// <summary>
/// Records Dean's physical/external approval (A05), atomically writing append-only TrainingBudgetLedger entries
/// including Future Fiscal-Year Quota Borrowing (A06) when approved spend exceeds remaining current-year quota.
/// </summary>
[RequirePermission(PermissionConstants.TrainingRequests.Approve)]
public sealed record ApproveTrainingRequestCommand(
    Guid ExternalId,
    decimal? ApprovedAmount = null,
    string? Note = null) : IRequest<ApproveTrainingResultDto>;

public sealed record ApproveTrainingResultDto(
    Guid ExternalId,
    decimal ApprovedAmount,
    decimal ConsumedFromCurrentYear,
    decimal BorrowedFromNextYear,
    int FiscalYearBe);

public sealed class ApproveTrainingRequestCommandHandler
    : IRequestHandler<ApproveTrainingRequestCommand, ApproveTrainingResultDto>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public ApproveTrainingRequestCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task<ApproveTrainingResultDto> Handle(
        ApproveTrainingRequestCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingRequests
            .Include(x => x.FacultyMember)
                .ThenInclude(f => f.Department)
            .FirstOrDefaultAsync(x => x.ExternalId == request.ExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(TrainingRequest), request.ExternalId);

        if (!_currentUserService.IsInRole(RoleConstants.Admin) &&
            _currentUserService.DepartmentExternalId.HasValue &&
            _currentUserService.DepartmentExternalId.Value != entity.FacultyMember.Department.ExternalId)
        {
            throw new ForbiddenAccessException("Staff cannot approve training requests outside their assigned department.");
        }

        var actor = _currentUserService.LoginName ?? _currentUserService.UserId ?? "staff";
        var now = _timeProvider.GetUtcNow();
        var finalApprovedAmount = entity.Category == TrainingCategory.AcademicFunded
            ? request.ApprovedAmount ?? entity.RequestedAmount
            : 0m;

        decimal consumed = 0m;
        decimal borrowed = 0m;

        if (entity.Category == TrainingCategory.AcademicFunded && finalApprovedAmount > 0m)
        {
            var (currentPolicy, nextPolicy) = await TrainingBudgetCalculator.EnsurePoliciesAsync(
                _context,
                entity.FiscalYearBe,
                cancellationToken);

            var existingLedger = await _context.TrainingBudgetLedgerEntries
                .Where(x =>
                    x.FacultyMemberId == entity.FacultyMemberId &&
                    (x.FiscalYearBe == entity.FiscalYearBe || x.FiscalYearBe == entity.FiscalYearBe + 1))
                .ToListAsync(cancellationToken);

            var facultyAllocations = await _context.FacultyTrainingBudgetAllocations
                .AsNoTracking()
                .Where(x =>
                    x.FacultyMemberId == entity.FacultyMemberId &&
                    (x.FiscalYearBe == entity.FiscalYearBe || x.FiscalYearBe == entity.FiscalYearBe + 1))
                .ToListAsync(cancellationToken);

            var balance = TrainingBudgetCalculator.ComputeBalance(
                entity.FiscalYearBe,
                currentPolicy,
                nextPolicy,
                existingLedger,
                facultyAllocations.FirstOrDefault(x => x.FiscalYearBe == entity.FiscalYearBe),
                facultyAllocations.FirstOrDefault(x => x.FiscalYearBe == entity.FiscalYearBe + 1));

            var newLedgerEntries = TrainingBudgetCalculator.CreateDeductionEntries(
                entity,
                finalApprovedAmount,
                balance,
                actor,
                now);

            entity.ApproveByDean(actor, finalApprovedAmount, now, request.Note);

            foreach (var entry in newLedgerEntries)
            {
                _context.TrainingBudgetLedgerEntries.Add(entry);
                if (entry.EntryType == BudgetLedgerEntryType.Consume) consumed += entry.Amount;
                if (entry.EntryType == BudgetLedgerEntryType.BorrowAdvance) borrowed += entry.Amount;
            }
        }
        else
        {
            entity.ApproveByDean(actor, finalApprovedAmount, now, request.Note);
        }

        // Single SaveChangesAsync wraps TrainingRequest + ApprovalHistory + TrainingBudgetLedgerEntries in one DB transaction
        await _context.SaveChangesAsync(cancellationToken);

        return new ApproveTrainingResultDto(
            entity.ExternalId,
            entity.ApprovedAmount,
            consumed,
            borrowed,
            entity.FiscalYearBe);
    }
}
