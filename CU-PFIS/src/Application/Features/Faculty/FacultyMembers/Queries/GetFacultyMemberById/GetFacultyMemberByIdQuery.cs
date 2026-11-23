using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Enums.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Faculty.FacultyMembers.Queries.GetFacultyMemberById;

public sealed record GetFacultyMemberByIdQuery(Guid ExternalId) : IRequest<FacultyMemberDetailDto>;

public sealed record FacultyMemberDetailDto(
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
    string? AdministrativePositionsTh,
    string? AdministrativePositionsEn,
    Guid DepartmentExternalId,
    string DepartmentNameTh,
    string DepartmentNameEn,
    string Email,
    string? PhoneNumber,
    string? ScopusAuthorId,
    string? OrcidId,
    DateTimeOffset? LastCvConfirmedAt,
    DateTimeOffset LastUpdatedAt,
    IReadOnlyList<FacultyEducationDto> Educations,
    IReadOnlyList<FacultyPublicationDto> Publications,
    IReadOnlyList<FacultyApprovedTrainingDto> ApprovedTrainings);

public sealed record FacultyApprovedTrainingDto(
    Guid ExternalId,
    int FiscalYearBe,
    string Category,
    string WorkType,
    string TitleTh,
    string? TitleEn,
    string OrganizerAndVenueTh,
    string? OrganizerAndVenueEn,
    DateOnly StartDate,
    DateOnly EndDate);

public sealed record FacultyEducationDto(
    Guid ExternalId,
    string DegreeTh,
    string? DegreeEn,
    string InstitutionTh,
    string? InstitutionEn,
    int GraduationYear,
    int SortOrder);

public sealed record FacultyPublicationDto(
    Guid ExternalId,
    string Title,
    string? JournalOrVenue,
    int PublishedYear,
    string? Doi,
    string? ScopusId,
    string SourceSystem);

public sealed class GetFacultyMemberByIdQueryHandler
    : IRequestHandler<GetFacultyMemberByIdQuery, FacultyMemberDetailDto>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetFacultyMemberByIdQueryHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<FacultyMemberDetailDto> Handle(
        GetFacultyMemberByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var member = await _context.FacultyMembers
            .AsNoTracking()
            .Include(x => x.Department)
            .Include(x => x.AcademicPosition)
            .Include(x => x.Educations)
            .Include(x => x.Publications)
            .FirstOrDefaultAsync(x => x.ExternalId == request.ExternalId, cancellationToken);

        if (member is null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Faculty.FacultyMember), request.ExternalId);
        }

        EnsureCanView(member.ExternalId, member.Department.ExternalId);

        // Auto-link Section 8 (Training History) directly from Approved TrainingRequests (A05)
        var approvedTrainings = await _context.TrainingRequests
            .AsNoTracking()
            .Where(t => t.FacultyMemberId == member.Id && t.Status == TrainingRequestStatus.Approved)
            .OrderByDescending(t => t.StartDate)
            .Select(t => new FacultyApprovedTrainingDto(
                t.ExternalId,
                t.FiscalYearBe,
                t.Category.ToString(),
                t.WorkType.ToString(),
                t.TitleTh,
                t.TitleEn,
                t.OrganizerAndVenueTh,
                t.OrganizerAndVenueEn,
                t.StartDate,
                t.EndDate))
            .ToListAsync(cancellationToken);

        return new FacultyMemberDetailDto(
            member.ExternalId,
            member.PersonnelCode,
            member.ProfessionalTitleTh,
            member.ProfessionalTitleEn,
            member.FirstNameTh,
            member.LastNameTh,
            member.FirstNameEn,
            member.LastNameEn,
            member.AcademicPosition.ExternalId,
            member.AcademicPosition.TitleTh,
            member.AcademicPosition.TitleEn,
            member.AdministrativePositionsTh,
            member.AdministrativePositionsEn,
            member.Department.ExternalId,
            member.Department.NameTh,
            member.Department.NameEn,
            member.Email,
            member.PhoneNumber,
            member.ScopusAuthorId,
            member.OrcidId,
            member.LastCvConfirmedAt,
            member.LastModified ?? member.CreatedOn,
            member.Educations
                .OrderByDescending(e => e.GraduationYear)
                .ThenBy(e => e.SortOrder)
                .Select(e => new FacultyEducationDto(
                    e.ExternalId,
                    e.DegreeTh,
                    e.DegreeEn,
                    e.InstitutionTh,
                    e.InstitutionEn,
                    e.GraduationYear,
                    e.SortOrder))
                .ToList(),
            member.Publications
                .OrderByDescending(p => p.PublishedYear)
                .ThenBy(p => p.Title)
                .Select(p => new FacultyPublicationDto(
                    p.ExternalId,
                    p.Title,
                    p.JournalOrVenue,
                    p.PublishedYear,
                    p.Doi,
                    p.ScopusId,
                    p.SourceSystem))
                .ToList(),
            approvedTrainings);
    }

    private void EnsureCanView(Guid memberExternalId, Guid departmentExternalId)
    {
        if (_currentUserService.IsInRole(RoleConstants.Admin))
        {
            return;
        }

        if (_currentUserService.HasPermission(PermissionConstants.Faculty.ViewAll))
        {
            if (_currentUserService.DepartmentExternalId.HasValue &&
                _currentUserService.DepartmentExternalId.Value != departmentExternalId)
            {
                throw new ForbiddenAccessException("Staff cannot access faculty records outside their assigned department.");
            }

            return;
        }

        if (_currentUserService.HasPermission(PermissionConstants.Faculty.ViewOwn) &&
            _currentUserService.FacultyMemberExternalId == memberExternalId)
        {
            return;
        }

        throw new ForbiddenAccessException("You do not have permission to view this Faculty profile.");
    }
}
