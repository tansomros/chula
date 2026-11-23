using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Entities.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.SubmitTrainingRequest;

public sealed record SubmitTrainingRequestCommand(Guid ExternalId, string? Note = null) : IRequest;

public sealed class SubmitTrainingRequestCommandHandler : IRequestHandler<SubmitTrainingRequestCommand>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public SubmitTrainingRequestCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task Handle(SubmitTrainingRequestCommand request, CancellationToken cancellationToken)
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
            throw new ForbiddenAccessException("You do not have permission to submit this training request.");
        }

        var actor = _currentUserService.LoginName ?? _currentUserService.UserId!;
        entity.Submit(actor, _timeProvider.GetUtcNow(), request.Note);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
