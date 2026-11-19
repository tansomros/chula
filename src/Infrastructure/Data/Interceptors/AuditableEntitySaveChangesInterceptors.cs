using System.Text.Json;
using Chula.Application.Common.Interfaces;
using Chula.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Chula.Infrastructure.Data.Interceptors;

public sealed class AuditableEntitySaveChangesInterceptors : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _dateTime;

    public AuditableEntitySaveChangesInterceptors(
        ICurrentUserService currentUserService,
        TimeProvider dateTime)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
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
        if (context == null)
        {
            return;
        }

        var now = _dateTime.GetUtcNow();
        var actor = _currentUserService.LoginName ?? _currentUserService.UserId ?? "system";
        var auditLogs = new List<AuditLog>();

        foreach (var entry in context.ChangeTracker.Entries<EntityBase>().ToList())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreatedAudit(actor, now);

                var newValues = new Dictionary<string, object?>();
                foreach (var prop in entry.Properties)
                {
                    if (!prop.Metadata.IsPrimaryKey() && prop.Metadata.Name != "xmin")
                    {
                        newValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                auditLogs.Add(new AuditLog(
                    entry.Entity.GetType().Name,
                    entry.Entity.ExternalId,
                    AuditAction.Create,
                    actor,
                    now,
                    null,
                    JsonSerializer.Serialize(newValues)));
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetModifiedAudit(actor, now);

                var oldValues = new Dictionary<string, object?>();
                var newValues = new Dictionary<string, object?>();

                foreach (var prop in entry.Properties.Where(p => p.IsModified && p.Metadata.Name != "xmin"))
                {
                    oldValues[prop.Metadata.Name] = prop.OriginalValue;
                    newValues[prop.Metadata.Name] = prop.CurrentValue;
                }

                if (newValues.Count > 0)
                {
                    auditLogs.Add(new AuditLog(
                        entry.Entity.GetType().Name,
                        entry.Entity.ExternalId,
                        AuditAction.Update,
                        actor,
                        now,
                        JsonSerializer.Serialize(oldValues),
                        JsonSerializer.Serialize(newValues)));
                }
            }
            else if (entry.State == EntityState.Deleted && entry.Entity is SoftDeletableEntityBase softDeletable)
            {
                entry.State = EntityState.Modified;
                softDeletable.MarkDeleted(actor, now);
                entry.Entity.SetModifiedAudit(actor, now);

                auditLogs.Add(new AuditLog(
                    entry.Entity.GetType().Name,
                    entry.Entity.ExternalId,
                    AuditAction.Delete,
                    actor,
                    now,
                    JsonSerializer.Serialize(new { IsDeleted = false }),
                    JsonSerializer.Serialize(new { IsDeleted = true, DeletedOn = now })));
            }
        }

        if (auditLogs.Count > 0)
        {
            context.Set<AuditLog>().AddRange(auditLogs);
        }
    }
}
