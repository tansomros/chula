using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using Chula.Application.Features.Training.TrainingBudgetPolicies.Queries.GetTrainingBudgetPolicies;
using Chula.Domain.Entities.Training;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingBudgetPolicies.Commands.UpsertTrainingBudgetPolicy;

[RequirePermission(PermissionConstants.TrainingBudgetPolicies.Manage)]
public sealed record UpsertTrainingBudgetPolicyCommand(
    int FiscalYearBe,
    decimal AnnualQuotaAmount,
    bool AllowFutureQuotaBorrowing = true,
    decimal MaxBorrowAmount = 10_000m,
    bool ResetAllCustomAllocations = false) : IRequest<TrainingBudgetPolicyDto>;

public sealed class UpsertTrainingBudgetPolicyCommandHandler
    : IRequestHandler<UpsertTrainingBudgetPolicyCommand, TrainingBudgetPolicyDto>
{
    private readonly IChulaDbContext _context;

    public UpsertTrainingBudgetPolicyCommandHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task<TrainingBudgetPolicyDto> Handle(
        UpsertTrainingBudgetPolicyCommand request,
        CancellationToken cancellationToken)
    {
        var policy = await _context.TrainingBudgetPolicies
            .FirstOrDefaultAsync(x => x.FiscalYearBe == request.FiscalYearBe, cancellationToken);

        if (policy is null)
        {
            policy = new TrainingBudgetPolicy(
                request.FiscalYearBe,
                request.AnnualQuotaAmount,
                request.AllowFutureQuotaBorrowing,
                request.MaxBorrowAmount);
            _context.TrainingBudgetPolicies.Add(policy);
        }
        else
        {
            policy.UpdatePolicy(
                request.AnnualQuotaAmount,
                request.AllowFutureQuotaBorrowing,
                request.MaxBorrowAmount);
        }

        if (request.ResetAllCustomAllocations)
        {
            var customAllocations = await _context.FacultyTrainingBudgetAllocations
                .Where(x => x.FiscalYearBe == request.FiscalYearBe)
                .ToListAsync(cancellationToken);

            if (customAllocations.Count > 0)
            {
                _context.FacultyTrainingBudgetAllocations.RemoveRange(customAllocations);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new TrainingBudgetPolicyDto(
            policy.ExternalId,
            policy.FiscalYearBe,
            policy.FiscalYearCe,
            policy.StartDate,
            policy.EndDate,
            policy.AnnualQuotaAmount,
            policy.AllowFutureQuotaBorrowing,
            policy.MaxBorrowAmount,
            policy.IsActive);
    }
}
