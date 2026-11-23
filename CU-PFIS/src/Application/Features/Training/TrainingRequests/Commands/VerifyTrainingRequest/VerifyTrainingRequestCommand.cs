using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Entities.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.VerifyTrainingRequest;

[RequirePermission(PermissionConstants.TrainingRequests.Verify)]
public sealed record VerifyTrainingRequestCommand(Guid ExternalId, string? Note = null) : IRequest;

public sealed class VerifyTrainingRequestCommandHandler : IRequestHandler<VerifyTrainingRequestCommand>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public VerifyTrainingRequestCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task Handle(VerifyTrainingRequestCommand request, CancellationToken cancellationToken)
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
            throw new ForbiddenAccessException("Staff cannot verify training requests outside their assigned department.");
        }

        var actor = _currentUserService.LoginName ?? _currentUserService.UserId ?? "staff";
        entity.MarkVerified(actor, _timeProvider.GetUtcNow(), request.Note);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
