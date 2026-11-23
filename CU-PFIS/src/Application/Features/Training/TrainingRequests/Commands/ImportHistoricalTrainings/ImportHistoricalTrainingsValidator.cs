using FluentValidation;

namespace Chula.Application.Features.Training.TrainingRequests.Commands.ImportHistoricalTrainings;

public sealed class ImportHistoricalTrainingsValidator : AbstractValidator<ImportHistoricalTrainingsCommand>
{
    public ImportHistoricalTrainingsValidator()
    {
        RuleFor(x => x.Items).NotEmpty();
        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.SourceRecordId).NotEmpty().MaximumLength(150);
            item.RuleFor(i => i.PersonnelCode).NotEmpty().MaximumLength(50);
            item.RuleFor(i => i.TitleTh).NotEmpty().MaximumLength(500);
            item.RuleFor(i => i.OrganizerAndVenueTh).NotEmpty().MaximumLength(500);
            item.RuleFor(i => i.ApprovedAmount).GreaterThanOrEqualTo(0m);
        });
    }
}
