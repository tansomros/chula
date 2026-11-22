using System.Text.Json;
using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Application.Features.Integration.CuppepSync.Commands.SyncCuppepStudents;
using Chula.Domain.Common;
using Chula.Domain.Entities.Integration;
using Chula.Domain.Entities.Students;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Integration.CuppepSync.Commands.ResolveDataConflict;

[RequirePermission(PermissionConstants.StudentsCv.Edit)]
public sealed record ResolveDataConflictCommand(
    Guid ConflictExternalId,
    string Action,
    string? Note = null) : IRequest;

public sealed class ResolveDataConflictCommandHandler : IRequestHandler<ResolveDataConflictCommand>
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public ResolveDataConflictCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task Handle(ResolveDataConflictCommand request, CancellationToken cancellationToken)
    {
        var conflict = await _context.DataConflictQueueItems
            .FirstOrDefaultAsync(x => x.ExternalId == request.ConflictExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(DataConflictQueueItem), request.ConflictExternalId);

        var resolvedBy = _currentUserService.LoginName ?? _currentUserService.UserId ?? "system";
        var now = _timeProvider.GetUtcNow();

        if (string.Equals(request.Action, "KeepCuPfis", StringComparison.OrdinalIgnoreCase))
        {
            conflict.ResolveKeepCuPfis(resolvedBy, now, request.Note);
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        if (string.Equals(request.Action, "AcceptIncoming", StringComparison.OrdinalIgnoreCase))
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(x => x.ExternalId == conflict.TargetExternalId, cancellationToken)
                ?? throw new NotFoundException(nameof(Student), conflict.TargetExternalId);

            var incoming = JsonSerializer.Deserialize<StudentSyncPayloadSnapshot>(conflict.IncomingValuesJson, JsonOptions)
                ?? throw new DomainException("Failed to deserialize incoming conflict snapshot.");

            var major = await _context.Majors
                .FirstOrDefaultAsync(x => x.Code.ToUpper() == incoming.MajorCode.ToUpper(), cancellationToken);
            var majorId = major?.Id ?? student.MajorId;

            student.ApplyConflictResolutionAcceptExternal(
                incoming.FirstNameTh,
                incoming.LastNameTh,
                incoming.FirstNameEn,
                incoming.LastNameEn,
                Math.Clamp(incoming.StudyYear, 1, 8),
                majorId,
                incoming.HighSchoolTh,
                incoming.HighSchoolEn,
                incoming.SourceRecordId,
                now);

            conflict.ResolveAcceptIncoming(resolvedBy, now, request.Note);
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        throw new DomainException($"Unsupported conflict resolution action '{request.Action}'. Use 'KeepCuPfis' or 'AcceptIncoming'.");
    }
}
