using FluentValidation;

namespace Chula.Application.Features.Training.TrainingBudgetPolicies.Commands.UpsertTrainingBudgetPolicy;

public sealed class UpsertTrainingBudgetPolicyValidator : AbstractValidator<UpsertTrainingBudgetPolicyCommand>
{
    public UpsertTrainingBudgetPolicyValidator()
    {
        RuleFor(x => x.FiscalYearBe).InclusiveBetween(2500, 2700);
        RuleFor(x => x.AnnualQuotaAmount).GreaterThanOrEqualTo(0m);
        RuleFor(x => x.MaxBorrowAmount).GreaterThanOrEqualTo(0m);
    }
}
