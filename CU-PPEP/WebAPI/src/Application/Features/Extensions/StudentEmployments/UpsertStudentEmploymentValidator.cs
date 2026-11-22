using FluentValidation;

namespace CUPPEP.Application.Features.Extensions.StudentEmployments;

public sealed class UpsertStudentEmploymentValidator : AbstractValidator<UpsertStudentEmploymentCommand>
{
    public UpsertStudentEmploymentValidator()
    {
        RuleFor(x => x.StudentCode).NotEmpty().MaximumLength(30);
        RuleFor(x => x.GraduationYearBe).InclusiveBetween(2500, 2700);
        RuleFor(x => x.EmploymentStatus).NotEmpty().MaximumLength(100);
        RuleFor(x => x.JobTitle).MaximumLength(300);
        RuleFor(x => x.WorkplaceName).MaximumLength(300);
    }
}
