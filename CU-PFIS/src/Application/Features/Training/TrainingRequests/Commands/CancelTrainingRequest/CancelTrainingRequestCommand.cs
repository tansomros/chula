using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Entities.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.CancelTrainingRequest;

public sealed record CancelTrainingRequestCommand(Guid ExternalId, string? Reason = null) : IRequest;

public sealed class CancelTrainingRequestCommandHandler : IRequestHandler<CancelTrainingRequestCommand>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public CancelTrainingRequestCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task Handle(CancelTrainingRequestCommand request, CancellationToken cancellationToken)
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

        var isAllowed = _currentUserService.IsInRole(RoleConstants.Admin) ||
            (_currentUserService.HasPermission(PermissionConstants.TrainingRequests.Verify) &&
             (!_currentUserService.DepartmentExternalId.HasValue ||
              _currentUserService.DepartmentExternalId.Value == entity.FacultyMember.Department.ExternalId)) ||
            (_currentUserService.HasPermission(PermissionConstants.TrainingRequests.UpdateOwn) &&
             _currentUserService.FacultyMemberExternalId == entity.FacultyMember.ExternalId);

        if (!isAllowed)
        {
            throw new ForbiddenAccessException("You do not have permission to cancel this training request.");
        }

        var actor = _currentUserService.LoginName ?? _currentUserService.UserId!;
        entity.Cancel(actor, _timeProvider.GetUtcNow(), request.Reason);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
