using Chula.Domain.Entities.Faculty;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Faculty;

public sealed class FacultyMemberConfiguration : IEntityTypeConfiguration<FacultyMember>
{
    public void Configure(EntityTypeBuilder<FacultyMember> builder)
    {
        builder.ConfigureEntityBase();
        builder.ConfigureSoftDelete();
        builder.ConfigureActiveFlag();

        builder.Property(x => x.PersonnelCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.PersonnelCode)
            .IsUnique();

        builder.Property(x => x.ProfessionalTitleTh).HasMaxLength(100);
        builder.Property(x => x.ProfessionalTitleEn).HasMaxLength(100);

        builder.Property(x => x.FirstNameTh).IsRequired().HasMaxLength(150);
        builder.Property(x => x.LastNameTh).IsRequired().HasMaxLength(150);
        builder.Property(x => x.FirstNameEn).IsRequired().HasMaxLength(150);
        builder.Property(x => x.LastNameEn).IsRequired().HasMaxLength(150);

        builder.Property(x => x.AdministrativePositionsTh).HasMaxLength(500);
        builder.Property(x => x.AdministrativePositionsEn).HasMaxLength(500);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
        builder.Property(x => x.PhoneNumber).HasMaxLength(50);
        builder.Property(x => x.ScopusAuthorId).HasMaxLength(50);
        builder.Property(x => x.OrcidId).HasMaxLength(50);

        builder.HasOne(x => x.AcademicPosition)
            .WithMany()
            .HasForeignKey(x => x.AcademicPositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Department)
            .WithMany()
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Educations)
            .WithOne(e => e.FacultyMember)
            .HasForeignKey(e => e.FacultyMemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Publications)
            .WithOne(p => p.FacultyMember)
            .HasForeignKey(p => p.FacultyMemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Educations).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(x => x.Publications).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
