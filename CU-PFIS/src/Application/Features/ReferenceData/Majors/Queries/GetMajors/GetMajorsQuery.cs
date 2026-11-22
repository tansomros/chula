using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.ReferenceData.Majors.Queries.GetMajors;

[RequirePermission(PermissionConstants.ReferenceData.View)]
public sealed record GetMajorsQuery(bool ActiveOnly = true) : IRequest<IReadOnlyList<MajorDto>>;

public sealed record MajorDto(
    Guid ExternalId,
    string Code,
    string NameTh,
    string NameEn,
    bool IsActive);

public sealed class GetMajorsQueryHandler : IRequestHandler<GetMajorsQuery, IReadOnlyList<MajorDto>>
{
    private readonly IChulaDbContext _context;

    public GetMajorsQueryHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<MajorDto>> Handle(
        GetMajorsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Majors.AsNoTracking();
        if (request.ActiveOnly)
        {
            query = query.Where(x => x.IsActive);
        }

        return await query
            .OrderBy(x => x.Code)
            .Select(x => new MajorDto(x.ExternalId, x.Code, x.NameTh, x.NameEn, x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
