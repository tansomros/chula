using FluentValidation;

namespace Chula.Application.Features.Students.Students.Commands.CreateStudent;

public sealed class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentValidator()
    {
        RuleFor(x => x.StudentCode)
            .NotEmpty()
            .MaximumLength(30);

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

        RuleFor(x => x.CohortAcademicYearExternalId)
            .NotEmpty();

        RuleFor(x => x.StudyYear)
            .InclusiveBetween(1, 8);

        RuleFor(x => x.MajorExternalId)
            .NotEmpty();
    }
}
