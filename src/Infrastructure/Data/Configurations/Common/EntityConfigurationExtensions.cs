using Chula.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Common;

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

        // PostgreSQL xmin optimistic concurrency token (per coding-rules.md)
        builder.Property<uint>("xmin")
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();

        builder.Ignore(e => e.DomainEvents);
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

    public static void ConfigureActiveFlag<T>(this EntityTypeBuilder<T> builder)
        where T : class, IHasActiveFlag
    {
        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
    }
}
