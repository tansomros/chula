using Chula.Application.Common.Interfaces;
using Chula.Application.Features.Integration.CuppepSync.Commands.ResolveDataConflict;
using Chula.Application.Features.Integration.CuppepSync.Commands.SyncCuppepStudents;
using Chula.Application.Features.Integration.CuppepSync.Queries.GetDataConflicts;
using Chula.Application.Features.Integration.CuppepSync.Queries.GetImportBatches;
using Microsoft.AspNetCore.Mvc;

namespace Chula.API.Controllers;

[Route("api/integrations/cuppep")]
public sealed class IntegrationsController : BaseController
{
    private readonly ICuppepDataSource _cuppepDataSource;

    public IntegrationsController(ICuppepDataSource cuppepDataSource)
    {
        _cuppepDataSource = cuppepDataSource;
    }

    [HttpGet("health")]
    public async Task<ActionResult<CuppepHealthStatus>> GetCuppepHealth(
        CancellationToken cancellationToken = default)
    {
        return Ok(await _cuppepDataSource.CheckHealthAsync(cancellationToken));
    }

    [HttpPost("sync-students")]
    public async Task<ActionResult<ImportBatchDto>> SyncStudents(
        [FromBody] SyncCuppepStudentsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(
            new SyncCuppepStudentsCommand(request?.UpdatedSince, request?.MaxRecords),
            cancellationToken);
        return Ok(result);
    }

    [HttpGet("batches")]
    public async Task<ActionResult<IReadOnlyList<ImportBatchDto>>> GetImportBatches(
        [FromQuery] int take = 20,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetImportBatchesQuery(take), cancellationToken));
    }

    [HttpGet("conflicts")]
    public async Task<ActionResult<IReadOnlyList<DataConflictQueueItemDto>>> GetConflicts(
        [FromQuery] string? status = "Pending",
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new GetDataConflictsQuery(status), cancellationToken));
    }

    [HttpPost("conflicts/{externalId:guid}/resolve")]
    public async Task<IActionResult> ResolveConflict(
        Guid externalId,
        [FromBody] ResolveConflictRequest request,
        CancellationToken cancellationToken = default)
    {
        await Mediator.Send(
            new ResolveDataConflictCommand(externalId, request.Action, request.Note),
            cancellationToken);
        return NoContent();
    }
}

public sealed record SyncCuppepStudentsRequest(
    DateTimeOffset? UpdatedSince = null,
    int? MaxRecords = null);

public sealed record ResolveConflictRequest(
    string Action,
    string? Note = null);
