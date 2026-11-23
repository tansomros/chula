using FluentValidation;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.ApproveTrainingRequest;

public sealed class ApproveTrainingRequestValidator : AbstractValidator<ApproveTrainingRequestCommand>
{
    public ApproveTrainingRequestValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty();
        RuleFor(x => x.ApprovedAmount).GreaterThanOrEqualTo(0m).When(x => x.ApprovedAmount.HasValue);
        RuleFor(x => x.Note).MaximumLength(1000);
    }
}
