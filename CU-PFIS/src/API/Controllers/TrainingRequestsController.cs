using Chula.Application.Features.Training.TrainingRequests.Commands.ApproveTrainingRequest;
using Chula.Application.Features.Training.TrainingRequests.Commands.CancelTrainingRequest;
using Chula.Application.Features.Training.TrainingRequests.Commands.CreateTrainingRequest;
using Chula.Application.Features.Training.TrainingRequests.Commands.ImportHistoricalTrainings;
using Chula.Application.Features.Training.TrainingRequests.Commands.RequestTrainingRevision;
using Chula.Application.Features.Training.TrainingRequests.Commands.SubmitTrainingRequest;
using Chula.Application.Features.Training.TrainingRequests.Commands.UpdateTrainingRequest;
using Chula.Application.Features.Training.TrainingRequests.Commands.VerifyTrainingRequest;
using Chula.Application.Features.Training.TrainingRequests.Queries.GetFacultyBudgetSummaries;
using Chula.Application.Features.Training.TrainingRequests.Queries.GetTrainingEventSummaries;
using Chula.Application.Features.Training.TrainingRequests.Queries.GetTrainingRequestById;
using Chula.Application.Features.Training.TrainingRequests.Queries.GetTrainingRequests;
using Chula.Domain.Enums.Training;
using Microsoft.AspNetCore.Mvc;

namespace Chula.API.Controllers;

public sealed class TrainingRequestsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TrainingRequestListItemDto>>> GetTrainingRequests(
        [FromQuery] int? fiscalYearBe = null,
        [FromQuery] TrainingCategory? category = null,
        [FromQuery] TrainingRequestStatus? status = null,
        [FromQuery] Guid? facultyMemberExternalId = null,
        [FromQuery] string? search = null,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(
            new GetTrainingRequestsQuery(fiscalYearBe, category, status, facultyMemberExternalId, search),
            cancellationToken);
        return Ok(result);
    }

    [HttpGet("{externalId:guid}")]
    public async Task<ActionResult<TrainingRequestDetailDto>> GetTrainingRequestById(
        Guid externalId,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetTrainingRequestByIdQuery(externalId), cancellationToken));
    }

    [HttpGet("faculty-budgets")]
    public async Task<ActionResult<FacultyBudgetSummaryResponseDto>> GetFacultyBudgets(
        [FromQuery] int? fiscalYearBe = null,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetFacultyBudgetSummariesQuery(fiscalYearBe), cancellationToken));
    }

    [HttpGet("event-summaries")]
    public async Task<ActionResult<IReadOnlyList<TrainingEventSummaryDto>>> GetEventSummaries(
        [FromQuery] int? fiscalYearBe = null,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetTrainingEventSummariesQuery(fiscalYearBe), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<IReadOnlyList<Guid>>> CreateTrainingRequest(
        [FromBody] CreateTrainingRequestCommand command,
        CancellationToken cancellationToken = default)
    {
        var createdIds = await Mediator.Send(command, cancellationToken);
        return Ok(createdIds);
    }

    [HttpPut("{externalId:guid}")]
    public async Task<IActionResult> UpdateTrainingRequest(
        Guid externalId,
        [FromBody] UpdateTrainingRequestCommand command,
        CancellationToken cancellationToken = default)
    {
        await Mediator.Send(command with { ExternalId = externalId }, cancellationToken);
        return NoContent();
    }

    [HttpPost("{externalId:guid}/submit")]
    public async Task<IActionResult> Submit(
        Guid externalId,
        [FromBody] TrainingWorkflowNoteRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        await Mediator.Send(new SubmitTrainingRequestCommand(externalId, request?.Note), cancellationToken);
        return NoContent();
    }

    [HttpPost("{externalId:guid}/verify")]
    public async Task<IActionResult> Verify(
        Guid externalId,
        [FromBody] TrainingWorkflowNoteRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        await Mediator.Send(new VerifyTrainingRequestCommand(externalId, request?.Note), cancellationToken);
        return NoContent();
    }

    [HttpPost("{externalId:guid}/request-revision")]
    public async Task<IActionResult> RequestRevision(
        Guid externalId,
        [FromBody] TrainingRevisionRequest request,
        CancellationToken cancellationToken = default)
    {
        await Mediator.Send(new RequestTrainingRevisionCommand(externalId, request.Reason), cancellationToken);
        return NoContent();
    }

    [HttpPost("{externalId:guid}/approve")]
    public async Task<ActionResult<ApproveTrainingResultDto>> Approve(
        Guid externalId,
        [FromBody] TrainingApproveRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(
            new ApproveTrainingRequestCommand(externalId, request?.ApprovedAmount, request?.Note),
            cancellationToken);
        return Ok(result);
    }

    [HttpPost("{externalId:guid}/cancel")]
    public async Task<IActionResult> Cancel(
        Guid externalId,
        [FromBody] TrainingWorkflowNoteRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        await Mediator.Send(new CancelTrainingRequestCommand(externalId, request?.Note), cancellationToken);
        return NoContent();
    }

    [HttpPost("import-historical")]
    public async Task<ActionResult<ImportHistoricalTrainingsResultDto>> ImportHistorical(
        [FromBody] ImportHistoricalTrainingsCommand command,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
}

public sealed record TrainingWorkflowNoteRequest(string? Note = null);

public sealed record TrainingRevisionRequest(string Reason);

public sealed record TrainingApproveRequest(decimal? ApprovedAmount = null, string? Note = null);
