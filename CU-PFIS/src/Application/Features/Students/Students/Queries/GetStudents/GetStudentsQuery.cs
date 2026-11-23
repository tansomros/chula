using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Students.Students.Queries.GetStudents;

[RequirePermission(PermissionConstants.Students.View)]
public sealed record GetStudentsQuery(
    string? Search = null,
    int? CohortYearBe = null,
    Guid? MajorExternalId = null,
    Guid? AdmissionRoundExternalId = null,
    bool ActiveOnly = true) : IRequest<IReadOnlyList<StudentListItemDto>>;

public sealed record StudentListItemDto(
    Guid ExternalId,
    string StudentCode,
    string FirstNameTh,
    string LastNameTh,
    string FirstNameEn,
    string LastNameEn,
    int CohortYearBe,
    int StudyYear,
    Guid MajorExternalId,
    string MajorCode,
    string MajorNameTh,
    string MajorNameEn,
    Guid? DepartmentExternalId,
    Guid? AdmissionRoundExternalId,
    string? AdmissionRoundNameTh,
    string? AdmissionRoundNameEn,
    string SourceSystem,
    DateTimeOffset? ImportedAt,
    bool IsStaffVerifiedEdit);

public sealed class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, IReadOnlyList<StudentListItemDto>>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetStudentsQueryHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<IReadOnlyList<StudentListItemDto>> Handle(
        GetStudentsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Students
            .AsNoTracking()
            .Include(x => x.CohortAcademicYear)
            .Include(x => x.Major)
            .Include(x => x.Department)
            .Include(x => x.AdmissionRound)
            .AsQueryable();

        if (request.ActiveOnly)
        {
            query = query.Where(x => x.IsActive);
        }

        // A03: Department-scoped access for Staff
        if (!_currentUserService.IsInRole(RoleConstants.Admin) &&
            _currentUserService.DepartmentExternalId.HasValue)
        {
            var scopedDeptId = _currentUserService.DepartmentExternalId.Value;
            query = query.Where(x => x.Department != null && x.Department.ExternalId == scopedDeptId);
        }

        if (request.CohortYearBe.HasValue)
        {
            query = query.Where(x => x.CohortAcademicYear.YearBe == request.CohortYearBe.Value);
        }

        if (request.MajorExternalId.HasValue)
        {
            var majorId = request.MajorExternalId.Value;
            query = query.Where(x => x.Major.ExternalId == majorId);
        }

        if (request.AdmissionRoundExternalId.HasValue)
        {
            var roundId = request.AdmissionRoundExternalId.Value;
            query = query.Where(x =>
                x.AdmissionRound != null &&
                (x.AdmissionRound.ExternalId == roundId ||
                 (x.AdmissionRound.ParentRound != null && x.AdmissionRound.ParentRound.ExternalId == roundId)));
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim().ToLower();
            query = query.Where(x =>
                x.StudentCode.ToLower().Contains(term) ||
                x.FirstNameTh.ToLower().Contains(term) ||
                x.LastNameTh.ToLower().Contains(term) ||
                x.FirstNameEn.ToLower().Contains(term) ||
                x.LastNameEn.ToLower().Contains(term));
        }

        return await query
            .OrderByDescending(x => x.CohortAcademicYear.YearBe)
            .ThenBy(x => x.StudentCode)
            .Select(x => new StudentListItemDto(
                x.ExternalId,
                x.StudentCode,
                x.FirstNameTh,
                x.LastNameTh,
                x.FirstNameEn,
                x.LastNameEn,
                x.CohortAcademicYear.YearBe,
                x.StudyYear,
                x.Major.ExternalId,
                x.Major.Code,
                x.Major.NameTh,
                x.Major.NameEn,
                x.Department != null ? x.Department.ExternalId : null,
                x.AdmissionRound != null ? x.AdmissionRound.ExternalId : null,
                x.AdmissionRound != null ? x.AdmissionRound.NameTh : null,
                x.AdmissionRound != null ? x.AdmissionRound.NameEn : null,
                x.SourceSystem,
                x.ImportedAt,
                x.IsStaffVerifiedEdit))
            .ToListAsync(cancellationToken);
    }
}
