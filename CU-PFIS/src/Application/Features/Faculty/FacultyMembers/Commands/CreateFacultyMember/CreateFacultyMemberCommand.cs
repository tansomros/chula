using Chula.Application.Common.Exceptions;
using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Domain.Common;
using Chula.Domain.Entities.Faculty;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Faculty.FacultyMembers.Commands.CreateFacultyMember;

[RequirePermission(PermissionConstants.FacultyCv.EditAll)]
public sealed record CreateFacultyMemberCommand(
    string PersonnelCode,
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
    IReadOnlyList<CreateFacultyPublicationItem>? Publications = null) : IRequest<Guid>;

public sealed record CreateFacultyEducationItem(
    string DegreeTh,
    string? DegreeEn,
    string InstitutionTh,
    string? InstitutionEn,
    int GraduationYear,
    int SortOrder = 0);

public sealed record CreateFacultyPublicationItem(
    string Title,
    string? JournalOrVenue,
    int PublishedYear,
    string? Doi = null,
    string? ScopusId = null);

public sealed class CreateFacultyMemberCommandHandler : IRequestHandler<CreateFacultyMemberCommand, Guid>
{
    private readonly IChulaDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateFacultyMemberCommandHandler(
        IChulaDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateFacultyMemberCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsInRole(RoleConstants.Admin) &&
            _currentUserService.DepartmentExternalId.HasValue &&
            _currentUserService.DepartmentExternalId.Value != request.DepartmentExternalId)
        {
            throw new ForbiddenAccessException("Staff can only create faculty records within their assigned department.");
        }

        var normalizedCode = request.PersonnelCode.Trim();
        var duplicateExists = await _context.FacultyMembers
            .AnyAsync(x => x.PersonnelCode == normalizedCode, cancellationToken);

        if (duplicateExists)
        {
            throw new DomainException($"Faculty member with personnel code '{normalizedCode}' already exists.");
        }

        var department = await _context.Departments
            .FirstOrDefaultAsync(x => x.ExternalId == request.DepartmentExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.ReferenceData.Department), request.DepartmentExternalId);

        var position = await _context.AcademicPositions
            .FirstOrDefaultAsync(x => x.ExternalId == request.AcademicPositionExternalId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.ReferenceData.AcademicPosition), request.AcademicPositionExternalId);

        var entity = new FacultyMember(
            normalizedCode,
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
            foreach (var edu in request.Educations)
            {
                entity.AddEducation(
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
            foreach (var pub in request.Publications)
            {
                entity.AddPublication(
                    pub.Title,
                    pub.JournalOrVenue,
                    pub.PublishedYear,
                    pub.Doi,
                    pub.ScopusId);
            }
        }

        _context.FacultyMembers.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.ExternalId;
    }
}
