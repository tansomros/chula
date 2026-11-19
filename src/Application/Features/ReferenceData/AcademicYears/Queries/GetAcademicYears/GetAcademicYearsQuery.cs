using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.ReferenceData.AcademicYears.Queries.GetAcademicYears;

[RequirePermission(PermissionConstants.ReferenceData.View)]
public sealed record GetAcademicYearsQuery(bool ActiveOnly = true) : IRequest<IReadOnlyList<AcademicYearDto>>;

public sealed record AcademicYearDto(
    Guid ExternalId,
    int YearBe,
    int YearCe,
    DateOnly StartDate,
    DateOnly EndDate,
    bool IsCurrent,
    bool IsActive);

public sealed class GetAcademicYearsQueryHandler : IRequestHandler<GetAcademicYearsQuery, IReadOnlyList<AcademicYearDto>>
{
    private readonly IChulaDbContext _context;

    public GetAcademicYearsQueryHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<AcademicYearDto>> Handle(
        GetAcademicYearsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.AcademicYears.AsNoTracking();

        if (request.ActiveOnly)
        {
            query = query.Where(x => x.IsActive);
        }

        return await query
            .OrderByDescending(x => x.YearBe)
            .Select(x => new AcademicYearDto(
                x.ExternalId,
                x.YearBe,
                x.YearCe,
                x.StartDate,
                x.EndDate,
                x.IsCurrent,
                x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
