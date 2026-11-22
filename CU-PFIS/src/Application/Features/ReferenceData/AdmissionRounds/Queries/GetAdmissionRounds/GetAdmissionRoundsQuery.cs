using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Enums.ReferenceData;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.ReferenceData.AdmissionRounds.Queries.GetAdmissionRounds;

[RequirePermission(PermissionConstants.ReferenceData.View)]
public sealed record GetAdmissionRoundsQuery(bool ActiveOnly = true)
    : IRequest<IReadOnlyList<AdmissionRoundDto>>;

public sealed record AdmissionRoundDto(
    Guid ExternalId,
    Guid? ParentExternalId,
    AdmissionRoundTier Tier,
    string Code,
    string NameTh,
    string NameEn,
    bool IsActive);

public sealed class GetAdmissionRoundsQueryHandler
    : IRequestHandler<GetAdmissionRoundsQuery, IReadOnlyList<AdmissionRoundDto>>
{
    private readonly IChulaDbContext _context;

    public GetAdmissionRoundsQueryHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<AdmissionRoundDto>> Handle(
        GetAdmissionRoundsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.AdmissionRounds
            .AsNoTracking()
            .Include(x => x.ParentRound)
            .AsQueryable();

        if (request.ActiveOnly)
        {
            query = query.Where(x => x.IsActive);
        }

        return await query
            .OrderBy(x => x.Tier)
            .ThenBy(x => x.Code)
            .Select(x => new AdmissionRoundDto(
                x.ExternalId,
                x.ParentRound != null ? x.ParentRound.ExternalId : null,
                x.Tier,
                x.Code,
                x.NameTh,
                x.NameEn,
                x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
