using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Entities.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.RequestTrainingRevision;

[RequirePermission(PermissionConstants.TrainingRequests.Verify)]
public sealed record RequestTrainingRevisionCommand(Guid ExternalId, string Reason) : IRequest;

public sealed class RequestTrainingRevisionCommandHandler : IRequestHandler<RequestTrainingRevisionCommand>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public RequestTrainingRevisionCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task Handle(RequestTrainingRevisionCommand request, CancellationToken cancellationToken)
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
            throw new ForbiddenAccessException("Staff cannot request revision outside their assigned department.");
        }

        var actor = _currentUserService.LoginName ?? _currentUserService.UserId ?? "staff";
        entity.RequestRevision(actor, request.Reason, _timeProvider.GetUtcNow());
        await _context.SaveChangesAsync(cancellationToken);
    }
}
