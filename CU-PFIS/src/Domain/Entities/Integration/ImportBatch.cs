using Ardalis.GuardClauses;
using Chula.Domain.Common;
using Chula.Domain.Enums.Integration;

namespace Chula.Domain.Entities.Integration;

/// <summary>
/// Tracks each one-way incremental pull batch from an external system such as CU-PPEP (Decisions D11, P01).
/// </summary>
public sealed class ImportBatch : EntityBase
{
    private readonly List<DataConflictQueueItem> _conflicts = new();

    private ImportBatch()
    {
    }

    public ImportBatch(
        string sourceSystem,
        string entityType,
        DateTimeOffset startedAt,
        DateTimeOffset? updatedSinceUtc = null,
        string? triggeredBy = null)
    {
        SourceSystem = Guard.Against.NullOrWhiteSpace(sourceSystem, nameof(sourceSystem)).Trim();
        EntityType = Guard.Against.NullOrWhiteSpace(entityType, nameof(entityType)).Trim();
        StartedAt = startedAt;
        UpdatedSinceUtc = updatedSinceUtc;
        TriggeredBy = string.IsNullOrWhiteSpace(triggeredBy) ? null : triggeredBy.Trim();
        Status = ImportBatchStatus.Running;
    }

    public string SourceSystem { get; private set; } = string.Empty;

    public string EntityType { get; private set; } = string.Empty;

    public DateTimeOffset? UpdatedSinceUtc { get; private set; }

    public DateTimeOffset StartedAt { get; private set; }

    public DateTimeOffset? CompletedAt { get; private set; }

    public ImportBatchStatus Status { get; private set; }

    public int TotalPulledCount { get; private set; }

    public int CreatedCount { get; private set; }

    public int UpdatedCount { get; private set; }

    public int SkippedUnchangedCount { get; private set; }

    public int ConflictCount { get; private set; }

    public string? ErrorMessage { get; private set; }

    public string? TriggeredBy { get; private set; }

    public IReadOnlyCollection<DataConflictQueueItem> Conflicts => _conflicts.AsReadOnly();

    public void Complete(
        int totalPulled,
        int created,
        int updated,
        int skippedUnchanged,
        int conflicts,
        DateTimeOffset completedAt)
    {
        TotalPulledCount = Guard.Against.Negative(totalPulled, nameof(totalPulled));
        CreatedCount = Guard.Against.Negative(created, nameof(created));
        UpdatedCount = Guard.Against.Negative(updated, nameof(updated));
        SkippedUnchangedCount = Guard.Against.Negative(skippedUnchanged, nameof(skippedUnchanged));
        ConflictCount = Guard.Against.Negative(conflicts, nameof(conflicts));
        CompletedAt = completedAt;
        ErrorMessage = null;
        Status = conflicts > 0
            ? ImportBatchStatus.CompletedWithConflicts
            : ImportBatchStatus.Completed;
    }

    public void Fail(string errorMessage, DateTimeOffset completedAt)
    {
        ErrorMessage = Guard.Against.NullOrWhiteSpace(errorMessage, nameof(errorMessage)).Trim();
        CompletedAt = completedAt;
        Status = ImportBatchStatus.Failed;
    }
}
