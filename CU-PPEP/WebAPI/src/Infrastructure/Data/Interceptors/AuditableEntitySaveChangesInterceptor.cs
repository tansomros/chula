using CUPPEP.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CUPPEP.Infrastructure.Data.Interceptors;

public sealed class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly TimeProvider _timeProvider;

    public AuditableEntitySaveChangesInterceptor(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        var now = _timeProvider.GetUtcNow();
        const string actor = "cuppep-api";

        foreach (var entry in context.ChangeTracker.Entries<EntityBase>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreatedAudit(actor, now);
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetModifiedAudit(actor, now);
            }
            else if (entry.State == EntityState.Deleted && entry.Entity is SoftDeletableEntityBase softDeletable)
            {
                entry.State = EntityState.Modified;
                softDeletable.MarkDeleted(actor, now);
                entry.Entity.SetModifiedAudit(actor, now);
            }
        }
    }
}
