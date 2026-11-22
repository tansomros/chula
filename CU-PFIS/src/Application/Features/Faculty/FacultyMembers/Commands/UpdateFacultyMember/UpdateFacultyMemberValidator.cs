using FluentValidation;

namespace Chula.Application.Features.Faculty.FacultyMembers.Commands.UpdateFacultyMember;

public sealed class UpdateFacultyMemberValidator : AbstractValidator<UpdateFacultyMemberCommand>
{
    public UpdateFacultyMemberValidator()
    {
        RuleFor(x => x.ExternalId)
            .NotEmpty();

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
    }
}
