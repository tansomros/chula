using Chula.Application.Common.Interfaces;
using Chula.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chula.Application.Features.Training.TrainingBudgetPolicies.Queries.GetTrainingBudgetPolicies;

[RequirePermission(PermissionConstants.ReferenceData.View)]
public sealed record GetTrainingBudgetPoliciesQuery : IRequest<IReadOnlyList<TrainingBudgetPolicyDto>>;

public sealed record TrainingBudgetPolicyDto(
    Guid ExternalId,
    int FiscalYearBe,
    int FiscalYearCe,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal AnnualQuotaAmount,
    bool AllowFutureQuotaBorrowing,
    decimal MaxBorrowAmount,
    bool IsActive);

public sealed class GetTrainingBudgetPoliciesQueryHandler
    : IRequestHandler<GetTrainingBudgetPoliciesQuery, IReadOnlyList<TrainingBudgetPolicyDto>>
{
    private readonly IChulaDbContext _context;

    public GetTrainingBudgetPoliciesQueryHandler(IChulaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TrainingBudgetPolicyDto>> Handle(
        GetTrainingBudgetPoliciesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.TrainingBudgetPolicies
            .AsNoTracking()
            .OrderByDescending(x => x.FiscalYearBe)
            .Select(x => new TrainingBudgetPolicyDto(
                x.ExternalId,
                x.FiscalYearBe,
                x.FiscalYearCe,
                x.StartDate,
                x.EndDate,
                x.AnnualQuotaAmount,
                x.AllowFutureQuotaBorrowing,
                x.MaxBorrowAmount,
                x.IsActive))
            .ToListAsync(cancellationToken);
    }
}
