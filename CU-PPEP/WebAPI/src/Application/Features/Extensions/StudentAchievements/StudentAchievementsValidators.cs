using FluentValidation;

namespace CUPPEP.Application.Features.Extensions.StudentAchievements;

public sealed class CreateStudentAchievementValidator : AbstractValidator<CreateStudentAchievementCommand>
{
    public CreateStudentAchievementValidator()
    {
        RuleFor(x => x.StudentCode).NotEmpty().MaximumLength(30);
        RuleFor(x => x.AcademicYearBe).InclusiveBetween(2500, 2700);
        RuleFor(x => x.StudyYear).InclusiveBetween(1, 8);
        RuleFor(x => x.EventName).NotEmpty().MaximumLength(500);
        RuleFor(x => x.AwardName).NotEmpty().MaximumLength(300);
        RuleFor(x => x.GrantAmount).GreaterThanOrEqualTo(0).When(x => x.GrantAmount.HasValue);
    }
}

public sealed class UpdateStudentAchievementValidator : AbstractValidator<UpdateStudentAchievementCommand>
{
    public UpdateStudentAchievementValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty();
        RuleFor(x => x.AcademicYearBe).InclusiveBetween(2500, 2700);
        RuleFor(x => x.StudyYear).InclusiveBetween(1, 8);
        RuleFor(x => x.EventName).NotEmpty().MaximumLength(500);
        RuleFor(x => x.AwardName).NotEmpty().MaximumLength(300);
        RuleFor(x => x.GrantAmount).GreaterThanOrEqualTo(0).When(x => x.GrantAmount.HasValue);
    }
}
