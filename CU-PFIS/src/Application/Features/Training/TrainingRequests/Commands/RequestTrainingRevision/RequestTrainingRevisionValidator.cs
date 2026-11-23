using FluentValidation;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.RequestTrainingRevision;

public sealed class RequestTrainingRevisionValidator : AbstractValidator<RequestTrainingRevisionCommand>
{
    public RequestTrainingRevisionValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty();
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(1000);
    }
}
