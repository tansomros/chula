using Ardalis.GuardClauses;
using Chula.Domain.Common;
using Chula.Domain.Enums.Training;

namespace Chula.Domain.Entities.Training;

/// <summary>
/// Append-only audit trail recording every status transition of a TrainingRequest (coding-rules §Backend).
/// </summary>
public sealed class TrainingApprovalHistory : EntityBase
{
    private TrainingApprovalHistory()
    {
    }

    public TrainingApprovalHistory(
        TrainingRequestStatus fromStatus,
        TrainingRequestStatus toStatus,
        string actorLogin,
        DateTimeOffset actionAt,
        string? note = null)
    {
        FromStatus = fromStatus;
        ToStatus = toStatus;
        ActorLogin = Guard.Against.NullOrWhiteSpace(actorLogin, nameof(actorLogin)).Trim();
        ActionAt = actionAt;
        Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
    }

    public int TrainingRequestId { get; private set; }

    public TrainingRequest TrainingRequest { get; private set; } = null!;

    public TrainingRequestStatus FromStatus { get; private set; }

    public TrainingRequestStatus ToStatus { get; private set; }

    public string ActorLogin { get; private set; } = string.Empty;

    public DateTimeOffset ActionAt { get; private set; }

    public string? Note { get; private set; }
}
