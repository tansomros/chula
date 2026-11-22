using Ardalis.GuardClauses;
using Chula.Domain.Common;
using Chula.Domain.Enums.Integration;

namespace Chula.Domain.Entities.Integration;

/// <summary>
/// Non-destructive conflict queue entry when an external import (e.g. CUPPEP) differs from a staff-verified CU-PFIS record (P01).
/// </summary>
public sealed class DataConflictQueueItem : EntityBase
{
    private DataConflictQueueItem()
    {
    }

    public DataConflictQueueItem(
        int importBatchId,
        string sourceSystem,
        string entityType,
        Guid targetExternalId,
        string businessKey,
        string sourceRecordId,
        string currentValuesJson,
        string incomingValuesJson,
        string changedFieldsSummary)
    {
        ImportBatchId = Guard.Against.NegativeOrZero(importBatchId, nameof(importBatchId));
        SourceSystem = Guard.Against.NullOrWhiteSpace(sourceSystem, nameof(sourceSystem)).Trim();
        EntityType = Guard.Against.NullOrWhiteSpace(entityType, nameof(entityType)).Trim();
        TargetExternalId = Guard.Against.Default(targetExternalId, nameof(targetExternalId));
        BusinessKey = Guard.Against.NullOrWhiteSpace(businessKey, nameof(businessKey)).Trim();
        SourceRecordId = Guard.Against.NullOrWhiteSpace(sourceRecordId, nameof(sourceRecordId)).Trim();
        CurrentValuesJson = Guard.Against.NullOrWhiteSpace(currentValuesJson, nameof(currentValuesJson));
        IncomingValuesJson = Guard.Against.NullOrWhiteSpace(incomingValuesJson, nameof(incomingValuesJson));
        ChangedFieldsSummary = Guard.Against.NullOrWhiteSpace(changedFieldsSummary, nameof(changedFieldsSummary)).Trim();
        Status = ConflictResolutionStatus.Pending;
    }

    public int ImportBatchId { get; private set; }

    public ImportBatch ImportBatch { get; private set; } = null!;

    public string SourceSystem { get; private set; } = string.Empty;

    public string EntityType { get; private set; } = string.Empty;

    public Guid TargetExternalId { get; private set; }

    public string BusinessKey { get; private set; } = string.Empty;

    public string SourceRecordId { get; private set; } = string.Empty;

    public string CurrentValuesJson { get; private set; } = string.Empty;

    public string IncomingValuesJson { get; private set; } = string.Empty;

    public string ChangedFieldsSummary { get; private set; } = string.Empty;

    public ConflictResolutionStatus Status { get; private set; }

    public string? ResolvedBy { get; private set; }

    public DateTimeOffset? ResolvedAt { get; private set; }

    public string? ResolutionNote { get; private set; }

    public void RefreshIncomingSnapshot(
        int importBatchId,
        string currentValuesJson,
        string incomingValuesJson,
        string changedFieldsSummary)
    {
        if (Status != ConflictResolutionStatus.Pending)
        {
            throw new DomainException("Cannot refresh a conflict item that has already been resolved.");
        }

        ImportBatchId = Guard.Against.NegativeOrZero(importBatchId, nameof(importBatchId));
        CurrentValuesJson = Guard.Against.NullOrWhiteSpace(currentValuesJson, nameof(currentValuesJson));
        IncomingValuesJson = Guard.Against.NullOrWhiteSpace(incomingValuesJson, nameof(incomingValuesJson));
        ChangedFieldsSummary = Guard.Against.NullOrWhiteSpace(changedFieldsSummary, nameof(changedFieldsSummary)).Trim();
    }

    public void ResolveKeepCuPfis(string resolvedBy, DateTimeOffset resolvedAt, string? note = null)
    {
        EnsurePending();
        Status = ConflictResolutionStatus.KeepCuPfis;
        ResolvedBy = Guard.Against.NullOrWhiteSpace(resolvedBy, nameof(resolvedBy)).Trim();
        ResolvedAt = resolvedAt;
        ResolutionNote = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
    }

    public void ResolveAcceptIncoming(string resolvedBy, DateTimeOffset resolvedAt, string? note = null)
    {
        EnsurePending();
        Status = ConflictResolutionStatus.AcceptIncoming;
        ResolvedBy = Guard.Against.NullOrWhiteSpace(resolvedBy, nameof(resolvedBy)).Trim();
        ResolvedAt = resolvedAt;
        ResolutionNote = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
    }

    private void EnsurePending()
    {
        if (Status != ConflictResolutionStatus.Pending)
        {
            throw new DomainException($"Conflict item '{ExternalId}' has already been resolved with status '{Status}'.");
        }
    }
}
