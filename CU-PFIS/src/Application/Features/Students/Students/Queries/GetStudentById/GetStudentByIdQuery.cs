using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Students.Students.Queries.GetStudentById;

[RequirePermission(PermissionConstants.Students.View)]
public sealed record GetStudentByIdQuery(Guid ExternalId) : IRequest<StudentDetailDto>;

public sealed record StudentDetailDto(
    Guid ExternalId,
    string StudentCode,
    string FirstNameTh,
    string LastNameTh,
    string FirstNameEn,
    string LastNameEn,
    Guid CohortAcademicYearExternalId,
    int CohortYearBe,
    int StudyYear,
    Guid MajorExternalId,
    string MajorCode,
    string MajorNameTh,
    string MajorNameEn,
    Guid? DepartmentExternalId,
    string? DepartmentNameTh,
    string? DepartmentNameEn,
    Guid? AdmissionRoundExternalId,
    Guid? ParentAdmissionRoundExternalId,
    string? AdmissionRoundNameTh,
    string? AdmissionRoundNameEn,
    Guid? AcademicAdvisorExternalId,
    string? AcademicAdvisorNameTh,
    string? AcademicAdvisorNameEn,
    string? HighSchoolTh,
    string? HighSchoolEn,
    string SourceSystem,
    string? SourceRecordId,
    DateTimeOffset? ImportedAt,
    bool IsStaffVerifiedEdit,
    IReadOnlyList<StudentInternshipDto> Internships);

public sealed record StudentInternshipDto(
    Guid ExternalId,
    string SiteTh,
    string? SiteEn,
    string RotationTypeTh,
    string? RotationTypeEn,
    string PeriodLabel);

public sealed class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDetailDto>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetStudentByIdQueryHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<StudentDetailDto> Handle(
        GetStudentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .AsNoTracking()
            .Include(x => x.CohortAcademicYear)
            .Include(x => x.Major)
            .Include(x => x.Department)
            .Include(x => x.AdmissionRound)
                .ThenInclude(r => r!.ParentRound)
            .Include(x => x.AcademicAdvisor)
            .Include(x => x.Internships)
            .FirstOrDefaultAsync(x => x.ExternalId == request.ExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Students.Student), request.ExternalId);

        if (!_currentUserService.IsInRole(RoleConstants.Admin) &&
            _currentUserService.DepartmentExternalId.HasValue &&
            (student.Department is null || student.Department.ExternalId != _currentUserService.DepartmentExternalId.Value))
        {
            throw new ForbiddenAccessException("Staff cannot access student records outside their assigned department.");
        }

        return new StudentDetailDto(
            student.ExternalId,
            student.StudentCode,
            student.FirstNameTh,
            student.LastNameTh,
            student.FirstNameEn,
            student.LastNameEn,
            student.CohortAcademicYear.ExternalId,
            student.CohortAcademicYear.YearBe,
            student.StudyYear,
            student.Major.ExternalId,
            student.Major.Code,
            student.Major.NameTh,
            student.Major.NameEn,
            student.Department?.ExternalId,
            student.Department?.NameTh,
            student.Department?.NameEn,
            student.AdmissionRound?.ExternalId,
            student.AdmissionRound?.ParentRound?.ExternalId,
            student.AdmissionRound?.NameTh,
            student.AdmissionRound?.NameEn,
            student.AcademicAdvisor?.ExternalId,
            student.AcademicAdvisor is null
                ? student.AcademicAdvisorNameTh
                : $"{student.AcademicAdvisor.ProfessionalTitleTh}{student.AcademicAdvisor.FirstNameTh} {student.AcademicAdvisor.LastNameTh}".Trim(),
            student.AcademicAdvisor is null
                ? student.AcademicAdvisorNameEn ?? student.AcademicAdvisorNameTh
                : $"{student.AcademicAdvisor.ProfessionalTitleEn} {student.AcademicAdvisor.FirstNameEn} {student.AcademicAdvisor.LastNameEn}".Trim(),
            student.HighSchoolTh,
            student.HighSchoolEn,
            student.SourceSystem,
            student.SourceRecordId,
            student.ImportedAt,
            student.IsStaffVerifiedEdit,
            student.Internships
                .Select(i => new StudentInternshipDto(
                    i.ExternalId,
                    i.SiteTh,
                    i.SiteEn,
                    i.RotationTypeTh,
                    i.RotationTypeEn,
                    i.PeriodLabel))
                .ToList());
    }
}
