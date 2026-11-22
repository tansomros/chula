using CUPPEP.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CUPPEP.Infrastructure.Data.Configurations.Common;

public static class EntityConfigurationExtensions
{
    public static void ConfigureEntityBase<T>(this EntityTypeBuilder<T> builder)
        where T : EntityBase
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ExternalId)
            .IsRequired();

        builder.HasIndex(e => e.ExternalId)
            .IsUnique();

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(150);

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.LastModifiedBy)
            .HasMaxLength(150);

        // SQL Server optimistic concurrency token (equivalent of PostgreSQL xmin in CU-PFIS)
        builder.Property(e => e.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        builder.Ignore(e => e.EffectiveUpdatedAt);
    }

    public static void ConfigureSoftDelete<T>(this EntityTypeBuilder<T> builder)
        where T : SoftDeletableEntityBase
    {
        builder.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.DeletedBy)
            .HasMaxLength(150);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
