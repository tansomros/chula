using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Enums.Training;
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
    int ApprovedTrainingsCount,
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
                         _currentUserService.HasPermission(PermissionConstants.Faculty.ViewAll);
        var canViewOwn = _currentUserService.HasPermission(PermissionConstants.Faculty.ViewOwn);

        if (!canViewAll && !canViewOwn)
        {
            throw new ForbiddenAccessException("Missing permission to view Faculty records.");
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

        var rows = await query
            .OrderBy(x => x.PersonnelCode)
            .Select(x => new
            {
                x.Id,
                x.ExternalId,
                x.PersonnelCode,
                x.ProfessionalTitleTh,
                x.ProfessionalTitleEn,
                x.FirstNameTh,
                x.LastNameTh,
                x.FirstNameEn,
                x.LastNameEn,
                AcademicPositionExternalId = x.AcademicPosition.ExternalId,
                AcademicPositionTitleTh = x.AcademicPosition.TitleTh,
                AcademicPositionTitleEn = x.AcademicPosition.TitleEn,
                DepartmentExternalId = x.Department.ExternalId,
                DepartmentNameTh = x.Department.NameTh,
                DepartmentNameEn = x.Department.NameEn,
                x.Email,
                x.PhoneNumber,
                x.ScopusAuthorId,
                PublicationsCount = x.Publications.Count,
                LastModifiedOrCreated = x.LastModified ?? x.CreatedOn
            })
            .ToListAsync(cancellationToken);

        var facultyIds = rows.Select(r => r.Id).ToList();
        var trainingCounts = await _context.TrainingRequests
            .AsNoTracking()
            .Where(t => facultyIds.Contains(t.FacultyMemberId) && t.Status == TrainingRequestStatus.Approved)
            .GroupBy(t => t.FacultyMemberId)
            .Select(g => new { FacultyId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.FacultyId, x => x.Count, cancellationToken);

        return rows.Select(x => new FacultyMemberListItemDto(
            x.ExternalId,
            x.PersonnelCode,
            x.ProfessionalTitleTh,
            x.ProfessionalTitleEn,
            x.FirstNameTh,
            x.LastNameTh,
            x.FirstNameEn,
            x.LastNameEn,
            x.AcademicPositionExternalId,
            x.AcademicPositionTitleTh,
            x.AcademicPositionTitleEn,
            x.DepartmentExternalId,
            x.DepartmentNameTh,
            x.DepartmentNameEn,
            x.Email,
            x.PhoneNumber,
            x.ScopusAuthorId,
            x.PublicationsCount,
            trainingCounts.GetValueOrDefault(x.Id),
            x.LastModifiedOrCreated)).ToList();
    }
}
