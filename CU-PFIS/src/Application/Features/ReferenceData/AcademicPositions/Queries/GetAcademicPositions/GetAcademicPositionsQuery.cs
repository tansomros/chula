using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.ReferenceData.AcademicPositions.Queries.GetAcademicPositions;

[RequirePermission(PermissionConstants.ReferenceData.View)]
public sealed record GetAcademicPositionsQuery(bool ActiveOnly = true)
    : IRequest<IReadOnlyList<AcademicPositionDto>>;

public sealed record AcademicPositionDto(
    Guid ExternalId,
    string Code,
    string TitleTh,
    string TitleEn,
    string ShortTitleTh,
    string ShortTitleEn,
    int SortOrder,
    bool IsActive);

public sealed class GetAcademicPositionsQueryHandler
    : IRequestHandler<GetAcademicPositionsQuery, IReadOnlyList<AcademicPositionDto>>
{
    private readonly IChulaDbContext _context;

    public GetAcademicPositionsQueryHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<AcademicPositionDto>> Handle(
        GetAcademicPositionsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.AcademicPositions.AsNoTracking();
        if (request.ActiveOnly)
        {
            query = query.Where(x => x.IsActive);
        }

        return await query
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Code)
            .Select(x => new AcademicPositionDto(
                x.ExternalId,
                x.Code,
                x.TitleTh,
                x.TitleEn,
                x.ShortTitleTh,
                x.ShortTitleEn,
                x.SortOrder,
                x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
