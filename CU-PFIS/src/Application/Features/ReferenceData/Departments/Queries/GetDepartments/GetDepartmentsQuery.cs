using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.ReferenceData.Departments.Queries.GetDepartments;

[RequirePermission(PermissionConstants.ReferenceData.View)]
public sealed record GetDepartmentsQuery(bool ActiveOnly = true) : IRequest<IReadOnlyList<DepartmentDto>>;

public sealed record DepartmentDto(
    Guid ExternalId,
    string Code,
    string NameTh,
    string NameEn,
    bool IsActive);

public sealed class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, IReadOnlyList<DepartmentDto>>
{
    private readonly IChulaDbContext _context;

    public GetDepartmentsQueryHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DepartmentDto>> Handle(
        GetDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Departments.AsNoTracking();
        if (request.ActiveOnly)
        {
            query = query.Where(x => x.IsActive);
        }

        return await query
            .OrderBy(x => x.Code)
            .Select(x => new DepartmentDto(x.ExternalId, x.Code, x.NameTh, x.NameEn, x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
