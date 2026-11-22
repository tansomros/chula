using FluentValidation;

namespace Chula.Application.Features.Faculty.FacultyMembers.Commands.CreateFacultyMember;

public sealed class CreateFacultyMemberValidator : AbstractValidator<CreateFacultyMemberCommand>
{
    public CreateFacultyMemberValidator()
    {
        RuleFor(x => x.PersonnelCode)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.FirstNameTh)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.LastNameTh)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.FirstNameEn)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.LastNameEn)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.AcademicPositionExternalId)
            .NotEmpty();

        RuleFor(x => x.DepartmentExternalId)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(50);

        RuleFor(x => x.ScopusAuthorId)
            .MaximumLength(50);

        RuleFor(x => x.OrcidId)
            .MaximumLength(50);
    }
}
