using Chula.Domain.Entities.Faculty;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Faculty;

public sealed class FacultyEducationConfiguration : IEntityTypeConfiguration<FacultyEducation>
{
    public void Configure(EntityTypeBuilder<FacultyEducation> builder)
    {
        builder.ConfigureEntityBase();

        builder.Property(x => x.DegreeTh).IsRequired().HasMaxLength(250);
        builder.Property(x => x.DegreeEn).HasMaxLength(250);
        builder.Property(x => x.InstitutionTh).IsRequired().HasMaxLength(300);
        builder.Property(x => x.InstitutionEn).HasMaxLength(300);
        builder.Property(x => x.GraduationYear).IsRequired();
        builder.Property(x => x.SortOrder).IsRequired().HasDefaultValue(0);
    }
}
