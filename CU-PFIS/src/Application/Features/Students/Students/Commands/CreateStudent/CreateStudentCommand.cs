using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Common;
using Chula.Domain.Entities.Students;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Students.Students.Commands.CreateStudent;

[RequirePermission(PermissionConstants.Students.Edit)]
public sealed record CreateStudentCommand(
    string StudentCode,
    string FirstNameTh,
    string LastNameTh,
    string FirstNameEn,
    string LastNameEn,
    Guid CohortAcademicYearExternalId,
    int StudyYear,
    Guid MajorExternalId,
    Guid? DepartmentExternalId = null,
    Guid? AdmissionRoundExternalId = null,
    Guid? AcademicAdvisorExternalId = null,
    string? HighSchoolTh = null,
    string? HighSchoolEn = null,
    string SourceSystem = "CU-PFIS") : IRequest<Guid>;

public sealed class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public CreateStudentCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsInRole(RoleConstants.Admin) &&
            _currentUserService.DepartmentExternalId.HasValue &&
            request.DepartmentExternalId != _currentUserService.DepartmentExternalId.Value)
        {
            throw new ForbiddenAccessException("Staff can only create student records within their assigned department.");
        }

        var normalizedCode = request.StudentCode.Trim();
        var duplicateExists = await _context.Students
            .AnyAsync(x => x.StudentCode == normalizedCode, cancellationToken);

        if (duplicateExists)
        {
            throw new DomainException($"Student with code '{normalizedCode}' already exists.");
        }

        var cohortYear = await _context.AcademicYears
            .FirstOrDefaultAsync(x => x.ExternalId == request.CohortAcademicYearExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.ReferenceData.AcademicYear), request.CohortAcademicYearExternalId);

        var major = await _context.Majors
            .FirstOrDefaultAsync(x => x.ExternalId == request.MajorExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.ReferenceData.Major), request.MajorExternalId);

        int? departmentId = null;
        if (request.DepartmentExternalId.HasValue)
        {
            var dept = await _context.Departments
                .FirstOrDefaultAsync(x => x.ExternalId == request.DepartmentExternalId.Value, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.ReferenceData.Department), request.DepartmentExternalId.Value);
            departmentId = dept.Id;
        }

        int? admissionRoundId = null;
        if (request.AdmissionRoundExternalId.HasValue)
        {
            var round = await _context.AdmissionRounds
                .FirstOrDefaultAsync(x => x.ExternalId == request.AdmissionRoundExternalId.Value, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.ReferenceData.AdmissionRound), request.AdmissionRoundExternalId.Value);
            admissionRoundId = round.Id;
        }

        int? advisorId = null;
        if (request.AcademicAdvisorExternalId.HasValue)
        {
            var advisor = await _context.FacultyMembers
                .FirstOrDefaultAsync(x => x.ExternalId == request.AcademicAdvisorExternalId.Value, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.Faculty.FacultyMember), request.AcademicAdvisorExternalId.Value);
            advisorId = advisor.Id;
        }

        var entity = new Student(
            normalizedCode,
            request.FirstNameTh,
            request.LastNameTh,
            request.FirstNameEn,
            request.LastNameEn,
            cohortYear.Id,
            request.StudyYear,
            major.Id,
            departmentId,
            admissionRoundId,
            advisorId,
            request.HighSchoolTh,
            request.HighSchoolEn,
            request.SourceSystem,
            normalizedCode,
            _timeProvider.GetUtcNow());

        _context.Students.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.ExternalId;
    }
}
