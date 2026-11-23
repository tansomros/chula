using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Application.Features.Training.Common;
using Chula.Domain.Entities.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Queries.GetTrainingRequestById;

public sealed record GetTrainingRequestByIdQuery(Guid ExternalId) : IRequest<TrainingRequestDetailDto>;

public sealed record TrainingApprovalHistoryDto(
    Guid ExternalId,
    string FromStatus,
    string ToStatus,
    string ActorLogin,
    DateTimeOffset ActionAt,
    string? Note);

public sealed record TrainingBudgetLedgerItemDto(
    Guid ExternalId,
    int FiscalYearBe,
    int? SourceFiscalYearBe,
    string EntryType,
    decimal Amount,
    DateTimeOffset EffectiveAt,
    string RecordedBy,
    string Description);

public sealed record TrainingRequestDetailDto(
    Guid ExternalId,
    Guid FacultyMemberExternalId,
    string PersonnelCode,
    string FacultyNameTh,
    string FacultyNameEn,
    string AcademicPositionTitleTh,
    string AcademicPositionTitleEn,
    Guid DepartmentExternalId,
    string DepartmentNameTh,
    string DepartmentNameEn,
    int FiscalYearBe,
    string Category,
    string WorkType,
    string TitleTh,
    string? TitleEn,
    string? SessionTopicTh,
    string? SessionTopicEn,
    string OrganizerAndVenueTh,
    string? OrganizerAndVenueEn,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal RequestedAmount,
    decimal ApprovedAmount,
    bool CanViewBankAccount,
    string? BankName,
    string? BankAccountNumber,
    string? BankAccountHolderName,
    string Status,
    string? RevisionNote,
    DateTimeOffset? SubmittedAt,
    DateTimeOffset? VerifiedAt,
    DateTimeOffset? ApprovedAt,
    string SourceSystem,
    string? SourceRecordId,
    FacultyFiscalYearBalance FacultyFiscalYearBalance,
    IReadOnlyList<TrainingApprovalHistoryDto> ApprovalHistory,
    IReadOnlyList<TrainingBudgetLedgerItemDto> LedgerEntries);

public sealed class GetTrainingRequestByIdQueryHandler
    : IRequestHandler<GetTrainingRequestByIdQuery, TrainingRequestDetailDto>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetTrainingRequestByIdQueryHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<TrainingRequestDetailDto> Handle(
        GetTrainingRequestByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var entity = await _context.TrainingRequests
            .AsNoTracking()
            .Include(x => x.FacultyMember)
                .ThenInclude(f => f.Department)
            .Include(x => x.FacultyMember)
                .ThenInclude(f => f.AcademicPosition)
            .Include(x => x.ApprovalHistory)
            .Include(x => x.BudgetLedgerEntries)
            .FirstOrDefaultAsync(x => x.ExternalId == request.ExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(TrainingRequest), request.ExternalId);

        EnsureCanView(entity.FacultyMember.ExternalId, entity.FacultyMember.Department.ExternalId);

        // PDPA (A18): Bank account info is strictly restricted to owner or training-requests.view-bank-account holders
        var isOwner = _currentUserService.FacultyMemberExternalId == entity.FacultyMember.ExternalId;
        var canViewBank = _currentUserService.IsInRole(RoleConstants.Admin) ||
                          isOwner ||
                          _currentUserService.HasPermission(PermissionConstants.TrainingRequests.ViewBankAccount);

        var (currentPolicy, nextPolicy) = await TrainingBudgetCalculator.EnsurePoliciesAsync(
            _context,
            entity.FiscalYearBe,
            cancellationToken);

        var facultyLedger = await _context.TrainingBudgetLedgerEntries
            .AsNoTracking()
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
            facultyLedger,
            facultyAllocations.FirstOrDefault(x => x.FiscalYearBe == entity.FiscalYearBe),
            facultyAllocations.FirstOrDefault(x => x.FiscalYearBe == entity.FiscalYearBe + 1));

        return new TrainingRequestDetailDto(
            entity.ExternalId,
            entity.FacultyMember.ExternalId,
            entity.FacultyMember.PersonnelCode,
            $"{entity.FacultyMember.ProfessionalTitleTh}{entity.FacultyMember.FirstNameTh} {entity.FacultyMember.LastNameTh}".Trim(),
            $"{entity.FacultyMember.ProfessionalTitleEn} {entity.FacultyMember.FirstNameEn} {entity.FacultyMember.LastNameEn}".Trim(),
            entity.FacultyMember.AcademicPosition.TitleTh,
            entity.FacultyMember.AcademicPosition.TitleEn,
            entity.FacultyMember.Department.ExternalId,
            entity.FacultyMember.Department.NameTh,
            entity.FacultyMember.Department.NameEn,
            entity.FiscalYearBe,
            entity.Category.ToString(),
            entity.WorkType.ToString(),
            entity.TitleTh,
            entity.TitleEn,
            entity.SessionTopicTh,
            entity.SessionTopicEn,
            entity.OrganizerAndVenueTh,
            entity.OrganizerAndVenueEn,
            entity.StartDate,
            entity.EndDate,
            entity.RequestedAmount,
            entity.ApprovedAmount,
            canViewBank,
            canViewBank ? entity.BankName : null,
            canViewBank ? entity.BankAccountNumber : null,
            canViewBank ? entity.BankAccountHolderName : null,
            entity.Status.ToString(),
            entity.RevisionNote,
            entity.SubmittedAt,
            entity.VerifiedAt,
            entity.ApprovedAt,
            entity.SourceSystem,
            entity.SourceRecordId,
            balance,
            entity.ApprovalHistory
                .OrderByDescending(h => h.ActionAt)
                .Select(h => new TrainingApprovalHistoryDto(
                    h.ExternalId,
                    h.FromStatus.ToString(),
                    h.ToStatus.ToString(),
                    h.ActorLogin,
                    h.ActionAt,
                    h.Note))
                .ToList(),
            entity.BudgetLedgerEntries
                .OrderBy(l => l.EffectiveAt)
                .Select(l => new TrainingBudgetLedgerItemDto(
                    l.ExternalId,
                    l.FiscalYearBe,
                    l.SourceFiscalYearBe,
                    l.EntryType.ToString(),
                    l.Amount,
                    l.EffectiveAt,
                    l.RecordedBy,
                    l.Description))
                .ToList());
    }

    private void EnsureCanView(Guid facultyExternalId, Guid departmentExternalId)
    {
        if (_currentUserService.IsInRole(RoleConstants.Admin))
        {
            return;
        }

        if (_currentUserService.HasPermission(PermissionConstants.TrainingRequests.ViewAll))
        {
            if (_currentUserService.DepartmentExternalId.HasValue &&
                _currentUserService.DepartmentExternalId.Value != departmentExternalId)
            {
                throw new ForbiddenAccessException("Staff cannot view training requests outside their assigned department.");
            }

            return;
        }

        if (_currentUserService.HasPermission(PermissionConstants.TrainingRequests.ViewOwn) &&
            _currentUserService.FacultyMemberExternalId == facultyExternalId)
        {
            return;
        }

        throw new ForbiddenAccessException("You do not have permission to view this training request.");
    }
}
