using FluentValidation;

namespace CUPPEP.Application.Features.Extensions.StudentLicenseResults;

public sealed class UpsertStudentLicenseResultValidator : AbstractValidator<UpsertStudentLicenseResultCommand>
{
    public UpsertStudentLicenseResultValidator()
    {
        RuleFor(x => x.StudentCode).NotEmpty().MaximumLength(30);
    }
}
