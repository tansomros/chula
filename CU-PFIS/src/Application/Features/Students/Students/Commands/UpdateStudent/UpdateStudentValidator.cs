using FluentValidation;

namespace Chula.Application.Features.Students.Students.Commands.UpdateStudent;

public sealed class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentValidator()
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

        RuleFor(x => x.StudyYear)
            .InclusiveBetween(1, 8);

        RuleFor(x => x.MajorExternalId)
            .NotEmpty();
    }
}
