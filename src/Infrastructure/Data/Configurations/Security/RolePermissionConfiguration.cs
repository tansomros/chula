using Chula.Domain.Entities.Security;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Security;

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ConfigureEntityBase();

        builder.Property(x => x.RoleName)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(x => x.PermissionId)
            .IsRequired();

        builder.HasIndex(x => new { x.RoleName, x.PermissionId })
            .IsUnique();

        builder.HasOne(x => x.Permission)
            .WithMany(x => x.RolePermissions)
            .HasForeignKey(x => x.PermissionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
