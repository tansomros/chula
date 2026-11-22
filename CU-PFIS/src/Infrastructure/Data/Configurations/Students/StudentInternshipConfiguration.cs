using Chula.Domain.Entities.Students;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Students;

public sealed class StudentInternshipConfiguration : IEntityTypeConfiguration<StudentInternship>
{
    public void Configure(EntityTypeBuilder<StudentInternship> builder)
    {
        builder.ConfigureEntityBase();

        builder.Property(x => x.SiteTh).IsRequired().HasMaxLength(300);
        builder.Property(x => x.SiteEn).HasMaxLength(300);
        builder.Property(x => x.RotationTypeTh).IsRequired().HasMaxLength(250);
        builder.Property(x => x.RotationTypeEn).HasMaxLength(250);
        builder.Property(x => x.PeriodLabel).IsRequired().HasMaxLength(100);
        builder.Property(x => x.SourceRecordId).HasMaxLength(150);
    }
}
