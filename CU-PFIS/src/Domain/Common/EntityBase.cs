namespace Chula.Domain.Common;

public abstract class EntityBase
{
    private readonly List<BaseEvent> _domainEvents = new();

    public int Id { get; private set; }

    public Guid ExternalId { get; private set; } = Guid.CreateVersion7();

    public string? CreatedBy { get; private set; }

    public DateTimeOffset CreatedOn { get; private set; } = DateTimeOffset.UtcNow;

    public string? LastModifiedBy { get; private set; }

    public DateTimeOffset? LastModified { get; private set; }

    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

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
