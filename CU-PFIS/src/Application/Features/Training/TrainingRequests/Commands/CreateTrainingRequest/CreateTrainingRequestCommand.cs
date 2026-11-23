using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Entities.Training;
using Chula.Domain.Enums.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.CreateTrainingRequest;

public sealed record CreateTrainingRequestCommand(
    IReadOnlyList<Guid> FacultyMemberExternalIds,
    TrainingCategory Category,
    TrainingWorkType WorkType,
    string TitleTh,
    string OrganizerAndVenueTh,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal RequestedAmount = 0m,
    string? TitleEn = null,
    string? SessionTopicTh = null,
    string? SessionTopicEn = null,
    string? OrganizerAndVenueEn = null,
    string? BankName = null,
    string? BankAccountNumber = null,
    string? BankAccountHolderName = null,
    bool SubmitImmediately = true) : IRequest<IReadOnlyList<Guid>>;

public sealed class CreateTrainingRequestCommandHandler
    : IRequestHandler<CreateTrainingRequestCommand, IReadOnlyList<Guid>>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public CreateTrainingRequestCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task<IReadOnlyList<Guid>> Handle(
        CreateTrainingRequestCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var isAdmin = _currentUserService.IsInRole(RoleConstants.Admin);
        var isStaff = _currentUserService.HasPermission(PermissionConstants.TrainingRequests.ViewAll);
        var canCreateOwn = _currentUserService.HasPermission(PermissionConstants.TrainingRequests.CreateOwn);

        if (!isAdmin && !isStaff && !canCreateOwn)
        {
            throw new ForbiddenAccessException("Missing permission to create training requests.");
        }

        // Determine target faculty members (Faculty role can ONLY create for themselves)
        IReadOnlyList<Guid> targetFacultyIds;
        if (!isAdmin && !isStaff)
        {
            if (_currentUserService.FacultyMemberExternalId is null)
            {
                throw new ForbiddenAccessException("Faculty account is not linked to a FacultyMember profile.");
            }

            if (request.FacultyMemberExternalIds.Count > 0 &&
                request.FacultyMemberExternalIds.Any(id => id != _currentUserService.FacultyMemberExternalId.Value))
            {
                throw new ForbiddenAccessException("Faculty members can only submit training requests for themselves.");
            }

            targetFacultyIds = new[] { _currentUserService.FacultyMemberExternalId.Value };
        }
        else
        {
            targetFacultyIds = request.FacultyMemberExternalIds.Distinct().ToList();
        }

        var facultyMembers = await _context.FacultyMembers
            .Include(x => x.Department)
            .Where(x => targetFacultyIds.Contains(x.ExternalId))
            .ToListAsync(cancellationToken);

        if (facultyMembers.Count != targetFacultyIds.Count)
        {
            throw new NotFoundException("FacultyMember", string.Join(",", targetFacultyIds));
        }

        // A03: Department-scoped Staff can only create training records for faculty in their department
        if (!isAdmin && isStaff && _currentUserService.DepartmentExternalId.HasValue)
        {
            var deptId = _currentUserService.DepartmentExternalId.Value;
            if (facultyMembers.Any(f => f.Department.ExternalId != deptId))
            {
                throw new ForbiddenAccessException("Staff cannot create training records outside their assigned department.");
            }
        }

        var actor = _currentUserService.LoginName ?? _currentUserService.UserId!;
        var now = _timeProvider.GetUtcNow();
        var createdIds = new List<Guid>(facultyMembers.Count);

        foreach (var member in facultyMembers)
        {
            var effectiveAmount = request.Category == TrainingCategory.AcademicFunded
                ? request.RequestedAmount
                : request.RequestedAmount;

            var entity = new TrainingRequest(
                member.Id,
                request.Category,
                request.WorkType,
                request.TitleTh,
                request.OrganizerAndVenueTh,
                request.StartDate,
                request.EndDate,
                effectiveAmount,
                request.TitleEn,
                request.SessionTopicTh,
                request.SessionTopicEn,
                request.OrganizerAndVenueEn,
                request.BankName,
                request.BankAccountNumber,
                request.BankAccountHolderName);

            if (!entity.RequiresApprovalWorkflow)
            {
                // Category 3 (FacultyOrganized) & Category 4 (ShortTermOrOverseas) require no Dean approval
                entity.RecordDirectlyAsApproved(actor, effectiveAmount, now);
            }
            else if (request.SubmitImmediately)
            {
                entity.Submit(actor, now, "Submitted training request.");
            }

            _context.TrainingRequests.Add(entity);
            createdIds.Add(entity.ExternalId);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return createdIds;
    }
}
