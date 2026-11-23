using Chula.Application.Features.Training.TrainingBudgetPolicies.Commands.DeleteFacultyBudgetAllocation;
using Chula.Application.Features.Training.TrainingBudgetPolicies.Commands.UpsertFacultyBudgetAllocation;
using Chula.Application.Features.Training.TrainingBudgetPolicies.Commands.UpsertTrainingBudgetPolicy;
using Chula.Application.Features.Training.TrainingBudgetPolicies.Queries.GetTrainingBudgetPolicies;
using Microsoft.AspNetCore.Mvc;

namespace Chula.API.Controllers;

public sealed class TrainingBudgetPoliciesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TrainingBudgetPolicyDto>>> GetPolicies(
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetTrainingBudgetPoliciesQuery(), cancellationToken));
    }

    [HttpPut("{fiscalYearBe:int}")]
    public async Task<ActionResult<TrainingBudgetPolicyDto>> UpsertPolicy(
        int fiscalYearBe,
        [FromBody] UpsertTrainingBudgetPolicyRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(
            new UpsertTrainingBudgetPolicyCommand(
                fiscalYearBe,
                request.AnnualQuotaAmount,
                request.AllowFutureQuotaBorrowing,
                request.MaxBorrowAmount,
                request.ResetAllCustomAllocations),
            cancellationToken);
        return Ok(result);
    }

    [HttpPut("{fiscalYearBe:int}/allocations/{facultyMemberExternalId:guid}")]
    public async Task<ActionResult<Guid>> UpsertFacultyAllocation(
        int fiscalYearBe,
        Guid facultyMemberExternalId,
        [FromBody] UpsertFacultyBudgetAllocationRequest request,
        CancellationToken cancellationToken = default)
    {
        var externalId = await Mediator.Send(
            new UpsertFacultyBudgetAllocationCommand(
                fiscalYearBe,
                facultyMemberExternalId,
                request.AnnualQuotaAmount,
                request.MaxBorrowAmount,
                request.Note),
            cancellationToken);
        return Ok(externalId);
    }

    [HttpDelete("{fiscalYearBe:int}/allocations/{facultyMemberExternalId:guid}")]
    public async Task<IActionResult> DeleteFacultyAllocation(
        int fiscalYearBe,
        Guid facultyMemberExternalId,
        CancellationToken cancellationToken = default)
    {
        await Mediator.Send(
            new DeleteFacultyBudgetAllocationCommand(fiscalYearBe, facultyMemberExternalId),
            cancellationToken);
        return NoContent();
    }
}

public sealed record UpsertTrainingBudgetPolicyRequest(
    decimal AnnualQuotaAmount,
    bool AllowFutureQuotaBorrowing = true,
    decimal MaxBorrowAmount = 10_000m,
    bool ResetAllCustomAllocations = false);

public sealed record UpsertFacultyBudgetAllocationRequest(
    decimal AnnualQuotaAmount,
    decimal? MaxBorrowAmount = null,
    string? Note = null);
