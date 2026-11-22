using Chula.Domain.Entities.Security;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Security;

public sealed class PdpaConsentConfiguration : IEntityTypeConfiguration<PdpaConsent>
{
    public void Configure(EntityTypeBuilder<PdpaConsent> builder)
    {
        builder.ConfigureEntityBase();

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.LoginName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.PolicyVersion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.AcceptedAt)
            .IsRequired();

        builder.Property(x => x.IpAddress)
            .HasMaxLength(100);

        builder.Property(x => x.UserAgent)
            .HasMaxLength(500);

        builder.HasIndex(x => new { x.UserId, x.PolicyVersion })
            .IsUnique();
    }
}
