using Chula.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Common;

/// <summary>
/// Append-only AuditLog configuration — never soft-deletable and never has IsActive.
/// </summary>
public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalId)
            .IsRequired();

        builder.HasIndex(x => x.ExternalId)
            .IsUnique();

        builder.Property(x => x.EntityName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.EntityExternalId)
            .IsRequired();

        builder.HasIndex(x => new { x.EntityName, x.EntityExternalId });

        builder.Property(x => x.Action)
            .IsRequired();

        builder.Property(x => x.ChangedBy)
            .HasMaxLength(150);

        builder.Property(x => x.ChangedOn)
            .IsRequired();

        builder.Property(x => x.OldValuesJson)
            .HasColumnType("jsonb");

        builder.Property(x => x.NewValuesJson)
            .HasColumnType("jsonb");
    }
}
