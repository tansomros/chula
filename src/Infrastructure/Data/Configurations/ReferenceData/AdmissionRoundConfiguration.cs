using Chula.Domain.Entities.ReferenceData;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.ReferenceData;

public sealed class AdmissionRoundConfiguration : IEntityTypeConfiguration<AdmissionRound>
{
    public void Configure(EntityTypeBuilder<AdmissionRound> builder)
    {
        builder.ConfigureEntityBase();
        builder.ConfigureSoftDelete();
        builder.ConfigureActiveFlag();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.NameTh)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.NameEn)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Tier)
            .IsRequired();

        builder.Property(x => x.SortOrder)
            .IsRequired()
            .HasDefaultValue(0);

        builder.HasOne(x => x.ParentRound)
            .WithMany(x => x.SubRounds)
            .HasForeignKey(x => x.ParentRoundId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
