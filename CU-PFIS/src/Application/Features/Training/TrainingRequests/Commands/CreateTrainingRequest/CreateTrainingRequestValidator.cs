using FluentValidation;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.CreateTrainingRequest;

public sealed class CreateTrainingRequestValidator : AbstractValidator<CreateTrainingRequestCommand>
{
    public CreateTrainingRequestValidator()
    {
        RuleFor(x => x.TitleTh).NotEmpty().MaximumLength(500);
        RuleFor(x => x.TitleEn).MaximumLength(500);
        RuleFor(x => x.OrganizerAndVenueTh).NotEmpty().MaximumLength(500);
        RuleFor(x => x.OrganizerAndVenueEn).MaximumLength(500);
        RuleFor(x => x.SessionTopicTh).MaximumLength(500);
        RuleFor(x => x.SessionTopicEn).MaximumLength(500);
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("End date must be on or after start date.");
        RuleFor(x => x.RequestedAmount).GreaterThanOrEqualTo(0m);
        RuleFor(x => x.BankName).MaximumLength(150);
        RuleFor(x => x.BankAccountNumber).MaximumLength(50);
        RuleFor(x => x.BankAccountHolderName).MaximumLength(200);
    }
}
