using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Students.Students.Commands.UpdateStudent;

[RequirePermission(PermissionConstants.StudentsCv.Edit)]
public sealed record UpdateStudentCommand(
    Guid ExternalId,
    string FirstNameTh,
    string LastNameTh,
    string FirstNameEn,
    string LastNameEn,
    int StudyYear,
    Guid MajorExternalId,
    Guid? DepartmentExternalId = null,
    Guid? AdmissionRoundExternalId = null,
    Guid? AcademicAdvisorExternalId = null,
    string? HighSchoolTh = null,
    string? HighSchoolEn = null) : IRequest;

public sealed class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateStudentCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.ExternalId == request.ExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Students.Student), request.ExternalId);

        if (!_currentUserService.IsInRole(RoleConstants.Admin) &&
            _currentUserService.DepartmentExternalId.HasValue &&
            (student.Department is null || student.Department.ExternalId != _currentUserService.DepartmentExternalId.Value))
        {
            throw new ForbiddenAccessException("Staff cannot edit student records outside their assigned department.");
        }

        var major = await _context.Majors
            .FirstOrDefaultAsync(x => x.ExternalId == request.MajorExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.ReferenceData.Major), request.MajorExternalId);

        int? departmentId = student.DepartmentId;
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

        student.UpdateProfileByStaff(
            request.FirstNameTh,
            request.LastNameTh,
            request.FirstNameEn,
            request.LastNameEn,
            request.StudyYear,
            major.Id,
            departmentId,
            admissionRoundId,
            advisorId,
            request.HighSchoolTh,
            request.HighSchoolEn);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
