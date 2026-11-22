using FluentValidation;

namespace Chula.Application.Features.Security.PdpaConsents.Commands.RecordPdpaConsent;

public sealed class RecordPdpaConsentValidator : AbstractValidator<RecordPdpaConsentCommand>
{
    public RecordPdpaConsentValidator()
    {
        RuleFor(x => x.PolicyVersion)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.IpAddress)
            .MaximumLength(100);

        RuleFor(x => x.UserAgent)
            .MaximumLength(500);
    }
}
