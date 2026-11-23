using Chula.Domain.Entities.Students;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Students;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ConfigureEntityBase();
        builder.ConfigureSoftDelete();
        builder.ConfigureActiveFlag();

        builder.Property(x => x.StudentCode)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasIndex(x => x.StudentCode)
            .IsUnique();

        builder.Property(x => x.FirstNameTh).IsRequired().HasMaxLength(150);
        builder.Property(x => x.LastNameTh).IsRequired().HasMaxLength(150);
        builder.Property(x => x.FirstNameEn).IsRequired().HasMaxLength(150);
        builder.Property(x => x.LastNameEn).IsRequired().HasMaxLength(150);

        builder.Property(x => x.StudyYear).IsRequired();
        builder.Property(x => x.AcademicAdvisorNameTh).HasMaxLength(250);
        builder.Property(x => x.AcademicAdvisorNameEn).HasMaxLength(250);
        builder.Property(x => x.HighSchoolTh).HasMaxLength(300);
        builder.Property(x => x.HighSchoolEn).HasMaxLength(300);

        builder.Property(x => x.SourceSystem).IsRequired().HasMaxLength(50);
        builder.Property(x => x.SourceRecordId).HasMaxLength(150);
        builder.Property(x => x.IsStaffVerifiedEdit).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.CohortAcademicYear)
            .WithMany()
            .HasForeignKey(x => x.CohortAcademicYearId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Major)
            .WithMany()
            .HasForeignKey(x => x.MajorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Department)
            .WithMany()
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AdmissionRound)
            .WithMany()
            .HasForeignKey(x => x.AdmissionRoundId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AcademicAdvisor)
            .WithMany()
            .HasForeignKey(x => x.AcademicAdvisorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Internships)
            .WithOne(i => i.Student)
            .HasForeignKey(i => i.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Internships).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.ToTable(t => t.HasCheckConstraint(
            "ck_students_study_year_range",
            "study_year >= 1 AND study_year <= 8"));
    }
}
