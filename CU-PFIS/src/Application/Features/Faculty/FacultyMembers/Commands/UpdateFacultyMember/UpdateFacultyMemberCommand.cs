using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Application.Features.Faculty.FacultyMembers.Commands.CreateFacultyMember;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Faculty.FacultyMembers.Commands.UpdateFacultyMember;

public sealed record UpdateFacultyMemberCommand(
    Guid ExternalId,
    string FirstNameTh,
    string LastNameTh,
    string FirstNameEn,
    string LastNameEn,
    Guid AcademicPositionExternalId,
    Guid DepartmentExternalId,
    string Email,
    string? ProfessionalTitleTh = null,
    string? ProfessionalTitleEn = null,
    string? AdministrativePositionsTh = null,
    string? AdministrativePositionsEn = null,
    string? PhoneNumber = null,
    string? ScopusAuthorId = null,
    string? OrcidId = null,
    IReadOnlyList<CreateFacultyEducationItem>? Educations = null,
    IReadOnlyList<CreateFacultyPublicationItem>? Publications = null) : IRequest;

public sealed class UpdateFacultyMemberCommandHandler : IRequestHandler<UpdateFacultyMemberCommand>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateFacultyMemberCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UpdateFacultyMemberCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var member = await _context.FacultyMembers
            .Include(x => x.Department)
            .Include(x => x.Educations)
            .Include(x => x.Publications)
            .FirstOrDefaultAsync(x => x.ExternalId == request.ExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Faculty.FacultyMember), request.ExternalId);

        EnsureCanEdit(member.ExternalId, member.Department.ExternalId);

        var department = await _context.Departments
            .FirstOrDefaultAsync(x => x.ExternalId == request.DepartmentExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.ReferenceData.Department), request.DepartmentExternalId);

        var position = await _context.AcademicPositions
            .FirstOrDefaultAsync(x => x.ExternalId == request.AcademicPositionExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.ReferenceData.AcademicPosition), request.AcademicPositionExternalId);

        member.UpdateProfile(
            request.FirstNameTh,
            request.LastNameTh,
            request.FirstNameEn,
            request.LastNameEn,
            position.Id,
            department.Id,
            request.Email,
            request.ProfessionalTitleTh,
            request.ProfessionalTitleEn,
            request.AdministrativePositionsTh,
            request.AdministrativePositionsEn,
            request.PhoneNumber,
            request.ScopusAuthorId,
            request.OrcidId);

        if (request.Educations is not null)
        {
            member.ClearEducations();
            foreach (var edu in request.Educations)
            {
                member.AddEducation(
                    edu.DegreeTh,
                    edu.InstitutionTh,
                    edu.GraduationYear,
                    edu.DegreeEn,
                    edu.InstitutionEn,
                    edu.SortOrder);
            }
        }

        if (request.Publications is not null)
        {
            member.ClearPublications();
            foreach (var pub in request.Publications)
            {
                member.AddPublication(
                    pub.Title,
                    pub.JournalOrVenue,
                    pub.PublishedYear,
                    pub.Doi,
                    pub.ScopusId);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    private void EnsureCanEdit(Guid memberExternalId, Guid departmentExternalId)
    {
        if (_currentUserService.IsInRole(RoleConstants.Admin))
        {
            return;
        }

        if (_currentUserService.HasPermission(PermissionConstants.FacultyCv.EditAll))
        {
            if (_currentUserService.DepartmentExternalId.HasValue &&
                _currentUserService.DepartmentExternalId.Value != departmentExternalId)
            {
                throw new ForbiddenAccessException("Staff cannot edit faculty records outside their assigned department.");
            }

            return;
        }

        if (_currentUserService.HasPermission(PermissionConstants.FacultyCv.EditOwn) &&
            _currentUserService.FacultyMemberExternalId == memberExternalId)
        {
            return;
        }

        throw new ForbiddenAccessException("You do not have permission to edit this Faculty CV.");
    }
}
