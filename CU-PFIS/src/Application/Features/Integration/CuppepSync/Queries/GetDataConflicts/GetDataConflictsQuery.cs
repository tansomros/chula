using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Enums.Integration;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Integration.CuppepSync.Queries.GetDataConflicts;

[RequirePermission(PermissionConstants.StudentsCv.View)]
public sealed record GetDataConflictsQuery(string? Status = "Pending")
    : IRequest<IReadOnlyList<DataConflictQueueItemDto>>;

public sealed record DataConflictQueueItemDto(
    Guid ExternalId,
    Guid ImportBatchExternalId,
    string SourceSystem,
    string EntityType,
    Guid TargetExternalId,
    string BusinessKey,
    string SourceRecordId,
    string CurrentValuesJson,
    string IncomingValuesJson,
    string ChangedFieldsSummary,
    string Status,
    DateTimeOffset CreatedOn,
    string? ResolvedBy,
    DateTimeOffset? ResolvedAt,
    string? ResolutionNote);

public sealed class GetDataConflictsQueryHandler
    : IRequestHandler<GetDataConflictsQuery, IReadOnlyList<DataConflictQueueItemDto>>
{
    private readonly IChulaDbContext _context;

    public GetDataConflictsQueryHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DataConflictQueueItemDto>> Handle(
        GetDataConflictsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.DataConflictQueueItems
            .AsNoTracking()
            .Include(x => x.ImportBatch)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Status) &&
            !string.Equals(request.Status, "All", StringComparison.OrdinalIgnoreCase) &&
            Enum.TryParse<ConflictResolutionStatus>(request.Status, ignoreCase: true, out var parsedStatus))
        {
            query = query.Where(x => x.Status == parsedStatus);
        }

        return await query
            .OrderByDescending(x => x.CreatedOn)
            .Select(x => new DataConflictQueueItemDto(
                x.ExternalId,
                x.ImportBatch.ExternalId,
                x.SourceSystem,
                x.EntityType,
                x.TargetExternalId,
                x.BusinessKey,
                x.SourceRecordId,
                x.CurrentValuesJson,
                x.IncomingValuesJson,
                x.ChangedFieldsSummary,
                x.Status.ToString(),
                x.CreatedOn,
                x.ResolvedBy,
                x.ResolvedAt,
                x.ResolutionNote))
            .ToListAsync(cancellationToken);
    }
}
