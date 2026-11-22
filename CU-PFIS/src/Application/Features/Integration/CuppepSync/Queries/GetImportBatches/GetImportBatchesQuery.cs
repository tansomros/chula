using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Application.Features.Integration.CuppepSync.Commands.SyncCuppepStudents;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Integration.CuppepSync.Queries.GetImportBatches;

[RequirePermission(PermissionConstants.StudentsCv.View)]
public sealed record GetImportBatchesQuery(int Take = 20) : IRequest<IReadOnlyList<ImportBatchDto>>;

public sealed class GetImportBatchesQueryHandler
    : IRequestHandler<GetImportBatchesQuery, IReadOnlyList<ImportBatchDto>>
{
    private readonly IChulaDbContext _context;

    public GetImportBatchesQueryHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ImportBatchDto>> Handle(
        GetImportBatchesQuery request,
        CancellationToken cancellationToken)
    {
        var take = Math.Clamp(request.Take, 1, 100);
        var batches = await _context.ImportBatches
            .AsNoTracking()
            .OrderByDescending(x => x.StartedAt)
            .Take(take)
            .ToListAsync(cancellationToken);

        return batches
            .Select(SyncCuppepStudentsCommandHandler.MapBatchDto)
            .ToList();
    }
}
