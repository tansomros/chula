namespace CUPPEP.Domain.Common;

public abstract class EntityBase
{
    public int Id { get; private set; }

    public Guid ExternalId { get; private set; } = Guid.CreateVersion7();

    public string? CreatedBy { get; private set; }

    public DateTimeOffset CreatedOn { get; private set; } = DateTimeOffset.UtcNow;

    public string? LastModifiedBy { get; private set; }

    public DateTimeOffset? LastModified { get; private set; }

    public byte[] RowVersion { get; private set; } = Array.Empty<byte>();

    public DateTimeOffset EffectiveUpdatedAt => LastModified ?? CreatedOn;

    public void SetCreatedAudit(string? createdBy, DateTimeOffset createdOn)
    {
        CreatedBy = createdBy;
        CreatedOn = createdOn;
    }

    public void SetModifiedAudit(string? modifiedBy, DateTimeOffset modifiedOn)
    {
        LastModifiedBy = modifiedBy;
        LastModified = modifiedOn;
    }
}
