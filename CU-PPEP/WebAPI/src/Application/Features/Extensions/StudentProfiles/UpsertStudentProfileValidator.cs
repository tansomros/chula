using FluentValidation;

namespace CUPPEP.Application.Features.Extensions.StudentProfiles;

public sealed class UpsertStudentProfileValidator : AbstractValidator<UpsertStudentProfileCommand>
{
    public UpsertStudentProfileValidator()
    {
        RuleFor(x => x.StudentCode).NotEmpty().MaximumLength(30);
        RuleFor(x => x.AdmissionRoundCode).MaximumLength(50);
        RuleFor(x => x.SeniorProjectTitleTh).MaximumLength(500);
        RuleFor(x => x.SeniorProjectTitleEn).MaximumLength(500);
    }
}
