using Chula.Domain.Entities.Security;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Security;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ConfigureEntityBase();
        builder.ConfigureSoftDelete();
        builder.ConfigureActiveFlag();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(120);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.Module)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(x => x.NameTh)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.NameEn)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(500);
    }
}
