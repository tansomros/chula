using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Entities.Training;
using Chula.Domain.Enums.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.UpdateTrainingRequest;

public sealed record UpdateTrainingRequestCommand(
    Guid ExternalId,
    TrainingWorkType WorkType,
    string TitleTh,
    string OrganizerAndVenueTh,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal RequestedAmount,
    string? TitleEn = null,
    string? SessionTopicTh = null,
    string? SessionTopicEn = null,
    string? OrganizerAndVenueEn = null,
    string? BankName = null,
    string? BankAccountNumber = null,
    string? BankAccountHolderName = null,
    bool Resubmit = false) : IRequest;

public sealed class UpdateTrainingRequestCommandHandler : IRequestHandler<UpdateTrainingRequestCommand>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public UpdateTrainingRequestCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task Handle(UpdateTrainingRequestCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var entity = await _context.TrainingRequests
            .Include(x => x.FacultyMember)
                .ThenInclude(f => f.Department)
            .FirstOrDefaultAsync(x => x.ExternalId == request.ExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(TrainingRequest), request.ExternalId);

        EnsureCanUpdate(entity.FacultyMember.ExternalId, entity.FacultyMember.Department.ExternalId);

        entity.UpdateDetails(
            request.WorkType,
            request.TitleTh,
            request.OrganizerAndVenueTh,
            request.StartDate,
            request.EndDate,
            request.RequestedAmount,
            request.TitleEn,
            request.SessionTopicTh,
            request.SessionTopicEn,
            request.OrganizerAndVenueEn,
            request.BankName,
            request.BankAccountNumber,
            request.BankAccountHolderName);

        if (request.Resubmit && entity.RequiresApprovalWorkflow)
        {
            var actor = _currentUserService.LoginName ?? _currentUserService.UserId!;
            entity.Submit(actor, _timeProvider.GetUtcNow(), "Updated and resubmitted.");
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    private void EnsureCanUpdate(Guid facultyExternalId, Guid departmentExternalId)
    {
        if (_currentUserService.IsInRole(RoleConstants.Admin))
        {
            return;
        }

        if (_currentUserService.HasPermission(PermissionConstants.TrainingRequests.Verify))
        {
            if (_currentUserService.DepartmentExternalId.HasValue &&
                _currentUserService.DepartmentExternalId.Value != departmentExternalId)
            {
                throw new ForbiddenAccessException("Staff cannot edit training requests outside their assigned department.");
            }

            return;
        }

        if (_currentUserService.HasPermission(PermissionConstants.TrainingRequests.UpdateOwn) &&
            _currentUserService.FacultyMemberExternalId == facultyExternalId)
        {
            return;
        }

        throw new ForbiddenAccessException("You do not have permission to edit this training request.");
    }
}
