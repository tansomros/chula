using Ardalis.GuardClauses;

namespace Chula.Domain.Common;

/// <summary>
/// Append-only audit log recording property-level diffs.
/// Never soft-deletable and never has IsActive (per coding-rules.md).
/// </summary>
public sealed class AuditLog
{
    private AuditLog()
    {
    }

    public AuditLog(
        string entityName,
        Guid entityExternalId,
        AuditAction action,
        string? changedBy,
        DateTimeOffset changedOn,
        string? oldValuesJson,
        string? newValuesJson)
    {
        EntityName = Guard.Against.NullOrWhiteSpace(entityName, nameof(entityName));
        EntityExternalId = Guard.Against.Default(entityExternalId, nameof(entityExternalId));
        Action = Guard.Against.EnumOutOfRange(action, nameof(action));
        ChangedBy = changedBy;
        ChangedOn = changedOn;
        OldValuesJson = oldValuesJson;
        NewValuesJson = newValuesJson;
    }

    public long Id { get; private set; }

    public Guid ExternalId { get; private set; } = Guid.CreateVersion7();

    public string EntityName { get; private set; } = string.Empty;

    public Guid EntityExternalId { get; private set; }

    public AuditAction Action { get; private set; }

    public string? ChangedBy { get; private set; }

    public DateTimeOffset ChangedOn { get; private set; }

    public string? OldValuesJson { get; private set; }

    public string? NewValuesJson { get; private set; }
}
