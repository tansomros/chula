using Chula.Domain.Entities.ReferenceData;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.ReferenceData;

public sealed class AcademicYearConfiguration : IEntityTypeConfiguration<AcademicYear>
{
    public void Configure(EntityTypeBuilder<AcademicYear> builder)
    {
        builder.ConfigureEntityBase();
        builder.ConfigureSoftDelete();
        builder.ConfigureActiveFlag();

        builder.Property(x => x.YearBe)
            .IsRequired();

        builder.Property(x => x.YearCe)
            .IsRequired();

        builder.HasIndex(x => x.YearBe)
            .IsUnique();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.IsCurrent)
            .IsRequired()
            .HasDefaultValue(false);

        builder.ToTable(t => t.HasCheckConstraint(
            "ck_academic_years_date_order",
            "end_date >= start_date"));
    }
}
