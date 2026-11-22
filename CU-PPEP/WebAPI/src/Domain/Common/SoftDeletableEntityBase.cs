namespace CUPPEP.Domain.Common;

public abstract class SoftDeletableEntityBase : EntityBase
{
    public bool IsDeleted { get; private set; }

    public string? DeletedBy { get; private set; }

    public DateTimeOffset? DeletedOn { get; private set; }

    public void MarkDeleted(string? deletedBy, DateTimeOffset deletedOn)
    {
        IsDeleted = true;
        DeletedBy = deletedBy;
        DeletedOn = deletedOn;
    }

    public void Restore()
    {
        IsDeleted = false;
        DeletedBy = null;
        DeletedOn = null;
    }
}
