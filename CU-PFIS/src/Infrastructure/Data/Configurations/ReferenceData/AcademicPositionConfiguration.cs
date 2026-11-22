using Chula.Domain.Entities.ReferenceData;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.ReferenceData;

public sealed class AcademicPositionConfiguration : IEntityTypeConfiguration<AcademicPosition>
{
    public void Configure(EntityTypeBuilder<AcademicPosition> builder)
    {
        builder.ConfigureEntityBase();
        builder.ConfigureSoftDelete();
        builder.ConfigureActiveFlag();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.TitleTh)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.TitleEn)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.ShortTitleTh)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ShortTitleEn)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.SortOrder)
            .IsRequired()
            .HasDefaultValue(0);
    }
}
