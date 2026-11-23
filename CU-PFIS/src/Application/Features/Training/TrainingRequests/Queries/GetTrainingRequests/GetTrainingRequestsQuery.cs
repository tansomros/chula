using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Enums.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Queries.GetTrainingRequests;

public sealed record GetTrainingRequestsQuery(
    int? FiscalYearBe = null,
    TrainingCategory? Category = null,
    TrainingRequestStatus? Status = null,
    Guid? FacultyMemberExternalId = null,
    string? Search = null) : IRequest<IReadOnlyList<TrainingRequestListItemDto>>;

/// <summary>
/// General list projection — strictly NEVER exposes PDPA-sensitive BankAccountNumber (A18).
/// </summary>
public sealed record TrainingRequestListItemDto(
    Guid ExternalId,
    Guid FacultyMemberExternalId,
    string PersonnelCode,
    string FacultyNameTh,
    string FacultyNameEn,
    Guid DepartmentExternalId,
    string DepartmentNameTh,
    string DepartmentNameEn,
    int FiscalYearBe,
    string Category,
    string WorkType,
    string TitleTh,
    string? TitleEn,
    string OrganizerAndVenueTh,
    string? OrganizerAndVenueEn,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal RequestedAmount,
    decimal ApprovedAmount,
    string Status,
    string SourceSystem,
    DateTimeOffset? SubmittedAt,
    DateTimeOffset? ApprovedAt);

public sealed class GetTrainingRequestsQueryHandler
    : IRequestHandler<GetTrainingRequestsQuery, IReadOnlyList<TrainingRequestListItemDto>>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetTrainingRequestsQueryHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<IReadOnlyList<TrainingRequestListItemDto>> Handle(
        GetTrainingRequestsQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var isAdmin = _currentUserService.IsInRole(RoleConstants.Admin);
        var canViewAll = isAdmin || _currentUserService.HasPermission(PermissionConstants.TrainingRequests.ViewAll);
        var canViewOwn = _currentUserService.HasPermission(PermissionConstants.TrainingRequests.ViewOwn);

        if (!canViewAll && !canViewOwn)
        {
            throw new ForbiddenAccessException("Missing permission to view training records.");
        }

        var query = _context.TrainingRequests
            .AsNoTracking()
            .Include(x => x.FacultyMember)
                .ThenInclude(f => f.Department)
            .AsQueryable();

        // A03: Faculty own-only & Staff department-scoped access
        if (!canViewAll)
        {
            if (_currentUserService.FacultyMemberExternalId is null)
            {
                return Array.Empty<TrainingRequestListItemDto>();
            }

            var ownId = _currentUserService.FacultyMemberExternalId.Value;
            query = query.Where(x => x.FacultyMember.ExternalId == ownId);
        }
        else if (!isAdmin && _currentUserService.DepartmentExternalId.HasValue)
        {
            var deptId = _currentUserService.DepartmentExternalId.Value;
            query = query.Where(x => x.FacultyMember.Department.ExternalId == deptId);
        }

        if (request.FiscalYearBe.HasValue)
        {
            query = query.Where(x => x.FiscalYearBe == request.FiscalYearBe.Value);
        }

        if (request.Category.HasValue)
        {
            query = query.Where(x => x.Category == request.Category.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(x => x.Status == request.Status.Value);
        }

        if (request.FacultyMemberExternalId.HasValue)
        {
            var fId = request.FacultyMemberExternalId.Value;
            query = query.Where(x => x.FacultyMember.ExternalId == fId);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim().ToLower();
            query = query.Where(x =>
                x.TitleTh.ToLower().Contains(term) ||
                (x.TitleEn != null && x.TitleEn.ToLower().Contains(term)) ||
                x.OrganizerAndVenueTh.ToLower().Contains(term) ||
                x.FacultyMember.FirstNameTh.ToLower().Contains(term) ||
                x.FacultyMember.LastNameTh.ToLower().Contains(term) ||
                x.FacultyMember.PersonnelCode.ToLower().Contains(term));
        }

        return await query
            .OrderByDescending(x => x.StartDate)
            .ThenByDescending(x => x.CreatedOn)
            .Select(x => new TrainingRequestListItemDto(
                x.ExternalId,
                x.FacultyMember.ExternalId,
                x.FacultyMember.PersonnelCode,
                ((x.FacultyMember.ProfessionalTitleTh ?? "") + x.FacultyMember.FirstNameTh + " " + x.FacultyMember.LastNameTh).Trim(),
                ((x.FacultyMember.ProfessionalTitleEn ?? "") + " " + x.FacultyMember.FirstNameEn + " " + x.FacultyMember.LastNameEn).Trim(),
                x.FacultyMember.Department.ExternalId,
                x.FacultyMember.Department.NameTh,
                x.FacultyMember.Department.NameEn,
                x.FiscalYearBe,
                x.Category.ToString(),
                x.WorkType.ToString(),
                x.TitleTh,
                x.TitleEn,
                x.OrganizerAndVenueTh,
                x.OrganizerAndVenueEn,
                x.StartDate,
                x.EndDate,
                x.RequestedAmount,
                x.ApprovedAmount,
                x.Status.ToString(),
                x.SourceSystem,
                x.SubmittedAt,
                x.ApprovedAt))
            .ToListAsync(cancellationToken);
    }
}
