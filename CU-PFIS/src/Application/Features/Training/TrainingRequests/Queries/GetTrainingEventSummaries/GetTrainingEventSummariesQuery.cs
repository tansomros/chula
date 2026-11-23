using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Enums.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingRequests.Queries.GetTrainingEventSummaries;

/// <summary>
/// Staff View 2 ("ชื่องานประชุม/อบรมทั้งหมด" per req071169.pdf pages 2–4):
/// Groups approved trainings by event title, work type, and date range, computing average cost per attendee
/// with expandable attendee breakdown.
/// </summary>
public sealed record GetTrainingEventSummariesQuery(int? FiscalYearBe = null)
    : IRequest<IReadOnlyList<TrainingEventSummaryDto>>;

public sealed record TrainingEventAttendeeDto(
    Guid TrainingRequestExternalId,
    Guid FacultyMemberExternalId,
    string PersonnelCode,
    string FacultyNameTh,
    string FacultyNameEn,
    string DepartmentNameTh,
    string DepartmentNameEn,
    decimal ApprovedAmount);

public sealed record TrainingEventSummaryDto(
    int FiscalYearBe,
    string Category,
    string WorkType,
    string TitleTh,
    string? TitleEn,
    string OrganizerAndVenueTh,
    string? OrganizerAndVenueEn,
    DateOnly StartDate,
    DateOnly EndDate,
    int AttendeeCount,
    decimal TotalApprovedAmount,
    decimal AverageAmountPerAttendee,
    IReadOnlyList<TrainingEventAttendeeDto> Attendees);

public sealed class GetTrainingEventSummariesQueryHandler
    : IRequestHandler<GetTrainingEventSummariesQuery, IReadOnlyList<TrainingEventSummaryDto>>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetTrainingEventSummariesQueryHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<IReadOnlyList<TrainingEventSummaryDto>> Handle(
        GetTrainingEventSummariesQuery request,
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
            throw new ForbiddenAccessException("Missing permission to view training events.");
        }

        var query = _context.TrainingRequests
            .AsNoTracking()
            .Include(x => x.FacultyMember)
                .ThenInclude(f => f.Department)
            .Where(x => x.Status == TrainingRequestStatus.Approved);

        if (!canViewAll)
        {
            if (_currentUserService.FacultyMemberExternalId is null)
            {
                return Array.Empty<TrainingEventSummaryDto>();
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

        var rows = await query
            .OrderByDescending(x => x.StartDate)
            .ToListAsync(cancellationToken);

        return rows
            .GroupBy(x => new
            {
                x.FiscalYearBe,
                x.Category,
                x.WorkType,
                TitleKey = x.TitleTh.Trim().ToLowerInvariant(),
                x.StartDate,
                x.EndDate
            })
            .Select(g =>
            {
                var first = g.First();
                var attendees = g.Select(x => new TrainingEventAttendeeDto(
                    x.ExternalId,
                    x.FacultyMember.ExternalId,
                    x.FacultyMember.PersonnelCode,
                    $"{x.FacultyMember.ProfessionalTitleTh}{x.FacultyMember.FirstNameTh} {x.FacultyMember.LastNameTh}".Trim(),
                    $"{x.FacultyMember.ProfessionalTitleEn} {x.FacultyMember.FirstNameEn} {x.FacultyMember.LastNameEn}".Trim(),
                    x.FacultyMember.Department.NameTh,
                    x.FacultyMember.Department.NameEn,
                    x.ApprovedAmount)).ToList();

                var total = attendees.Sum(a => a.ApprovedAmount);
                var avg = attendees.Count > 0 ? decimal.Round(total / attendees.Count, 2, MidpointRounding.AwayFromZero) : 0m;

                return new TrainingEventSummaryDto(
                    first.FiscalYearBe,
                    first.Category.ToString(),
                    first.WorkType.ToString(),
                    first.TitleTh,
                    first.TitleEn,
                    first.OrganizerAndVenueTh,
                    first.OrganizerAndVenueEn,
                    first.StartDate,
                    first.EndDate,
                    attendees.Count,
                    total,
                    avg,
                    attendees);
            })
            .OrderByDescending(x => x.StartDate)
            .ThenBy(x => x.TitleTh)
            .ToList();
    }
}
