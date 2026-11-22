using CUPPEP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CUPPEP.Application.Features.Teachers.Queries.GetTeachers;

public sealed record GetTeachersQuery : IRequest<IReadOnlyList<CuppepTeacherDto>>;

public sealed record CuppepTeacherDto(
    string SourceRecordId,
    int PersonId,
    string FirstNameTh,
    string LastNameTh,
    string? PositionNameTh,
    int? DepartmentId,
    string? DepartmentNameTh,
    string? Email,
    string? Telephone,
    string? StatusFlag);

public sealed class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, IReadOnlyList<CuppepTeacherDto>>
{
    private readonly ICuPpepDbContext _context;

    public GetTeachersQueryHandler(ICuPpepDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<CuppepTeacherDto>> Handle(
        GetTeachersQuery request,
        CancellationToken cancellationToken)
    {
        var rows = await _context.Persons
            .AsNoTracking()
            .Where(x => x.PersonType == "T")
            .OrderBy(x => x.PersonId)
            .Select(x => new
            {
                x.PersonId,
                x.FirstName,
                x.LastName,
                x.PositionName,
                x.DepartmentUid,
                x.DepartmentName,
                x.Email,
                x.Telephone,
                x.StatusFlag
            })
            .ToListAsync(cancellationToken);

        return rows
            .Select(x => new CuppepTeacherDto(
                x.PersonId.ToString(),
                x.PersonId,
                (x.FirstName ?? string.Empty).Trim(),
                (x.LastName ?? string.Empty).Trim(),
                x.PositionName,
                x.DepartmentUid,
                x.DepartmentName,
                x.Email,
                x.Telephone,
                x.StatusFlag))
            .ToList();
    }
}
