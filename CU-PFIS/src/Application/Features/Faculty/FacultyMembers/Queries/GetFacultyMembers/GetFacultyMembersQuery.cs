using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Faculty.FacultyMembers.Queries.GetFacultyMembers;

public sealed record GetFacultyMembersQuery(
    string? Search = null,
    Guid? DepartmentExternalId = null,
    Guid? AcademicPositionExternalId = null,
    bool ActiveOnly = true) : IRequest<IReadOnlyList<FacultyMemberListItemDto>>;

public sealed record FacultyMemberListItemDto(
    Guid ExternalId,
    string PersonnelCode,
    string? ProfessionalTitleTh,
    string? ProfessionalTitleEn,
    string FirstNameTh,
    string LastNameTh,
    string FirstNameEn,
    string LastNameEn,
    Guid AcademicPositionExternalId,
    string AcademicPositionTitleTh,
    string AcademicPositionTitleEn,
    Guid DepartmentExternalId,
    string DepartmentNameTh,
    string DepartmentNameEn,
    string Email,
    string? PhoneNumber,
    string? ScopusAuthorId,
    int PublicationsCount,
    DateTimeOffset? LastModifiedOrCreated);

public sealed class GetFacultyMembersQueryHandler
    : IRequestHandler<GetFacultyMembersQuery, IReadOnlyList<FacultyMemberListItemDto>>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetFacultyMembersQueryHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<IReadOnlyList<FacultyMemberListItemDto>> Handle(
        GetFacultyMembersQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var canViewAll = _currentUserService.IsInRole(RoleConstants.Admin) ||
                         _currentUserService.HasPermission(PermissionConstants.FacultyCv.ViewAll);
        var canViewOwn = _currentUserService.HasPermission(PermissionConstants.FacultyCv.ViewOwn);

        if (!canViewAll && !canViewOwn)
        {
            throw new ForbiddenAccessException("Missing permission to view Faculty CV records.");
        }

        var query = _context.FacultyMembers
            .AsNoTracking()
            .Include(x => x.Department)
            .Include(x => x.AcademicPosition)
            .Include(x => x.Publications)
            .AsQueryable();

        if (request.ActiveOnly)
        {
            query = query.Where(x => x.IsActive);
        }

        // A03: Enforce resource ownership for Faculty & department scoping for Staff
        if (!canViewAll)
        {
            if (_currentUserService.FacultyMemberExternalId is null)
            {
                return Array.Empty<FacultyMemberListItemDto>();
            }

            query = query.Where(x => x.ExternalId == _currentUserService.FacultyMemberExternalId.Value);
        }
        else if (!_currentUserService.IsInRole(RoleConstants.Admin) &&
                 _currentUserService.DepartmentExternalId.HasValue)
        {
            var scopedDeptId = _currentUserService.DepartmentExternalId.Value;
            query = query.Where(x => x.Department.ExternalId == scopedDeptId);
        }

        if (request.DepartmentExternalId.HasValue)
        {
            var deptExternalId = request.DepartmentExternalId.Value;
            query = query.Where(x => x.Department.ExternalId == deptExternalId);
        }

        if (request.AcademicPositionExternalId.HasValue)
        {
            var posExternalId = request.AcademicPositionExternalId.Value;
            query = query.Where(x => x.AcademicPosition.ExternalId == posExternalId);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim().ToLower();
            query = query.Where(x =>
                x.PersonnelCode.ToLower().Contains(term) ||
                x.FirstNameTh.ToLower().Contains(term) ||
                x.LastNameTh.ToLower().Contains(term) ||
                x.FirstNameEn.ToLower().Contains(term) ||
                x.LastNameEn.ToLower().Contains(term) ||
                x.Email.ToLower().Contains(term));
        }

        return await query
            .OrderBy(x => x.PersonnelCode)
            .Select(x => new FacultyMemberListItemDto(
                x.ExternalId,
                x.PersonnelCode,
                x.ProfessionalTitleTh,
                x.ProfessionalTitleEn,
                x.FirstNameTh,
                x.LastNameTh,
                x.FirstNameEn,
                x.LastNameEn,
                x.AcademicPosition.ExternalId,
                x.AcademicPosition.TitleTh,
                x.AcademicPosition.TitleEn,
                x.Department.ExternalId,
                x.Department.NameTh,
                x.Department.NameEn,
                x.Email,
                x.PhoneNumber,
                x.ScopusAuthorId,
                x.Publications.Count,
                x.LastModified ?? x.CreatedOn))
            .ToListAsync(cancellationToken);
    }
}
