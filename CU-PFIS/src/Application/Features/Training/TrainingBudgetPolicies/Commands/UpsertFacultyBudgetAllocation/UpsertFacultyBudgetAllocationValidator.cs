using FluentValidation;

namespace Chula.Application.Features.Training.TrainingBudgetPolicies.Commands.UpsertFacultyBudgetAllocation;

public sealed class UpsertFacultyBudgetAllocationValidator : AbstractValidator<UpsertFacultyBudgetAllocationCommand>
{
    public UpsertFacultyBudgetAllocationValidator()
    {
        RuleFor(x => x.FiscalYearBe).InclusiveBetween(2500, 2700);
        RuleFor(x => x.FacultyMemberExternalId).NotEmpty();
        RuleFor(x => x.AnnualQuotaAmount).GreaterThanOrEqualTo(0m);
        RuleFor(x => x.MaxBorrowAmount).GreaterThanOrEqualTo(0m).When(x => x.MaxBorrowAmount.HasValue);
        RuleFor(x => x.Note).MaximumLength(500);
    }
}
