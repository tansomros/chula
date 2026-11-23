using FluentValidation;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.UpdateTrainingRequest;

public sealed class UpdateTrainingRequestValidator : AbstractValidator<UpdateTrainingRequestCommand>
{
    public UpdateTrainingRequestValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty();
        RuleFor(x => x.TitleTh).NotEmpty().MaximumLength(500);
        RuleFor(x => x.TitleEn).MaximumLength(500);
        RuleFor(x => x.OrganizerAndVenueTh).NotEmpty().MaximumLength(500);
        RuleFor(x => x.OrganizerAndVenueEn).MaximumLength(500);
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate);
        RuleFor(x => x.RequestedAmount).GreaterThanOrEqualTo(0m);
        RuleFor(x => x.BankName).MaximumLength(150);
        RuleFor(x => x.BankAccountNumber).MaximumLength(50);
        RuleFor(x => x.BankAccountHolderName).MaximumLength(200);
    }
}
