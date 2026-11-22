using CUPPEP.Domain.Entities.Extensions;
using CUPPEP.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CUPPEP.Infrastructure.Data.Configurations.Extensions;

public sealed class StudentProfileExtConfiguration : IEntityTypeConfiguration<StudentProfileExt>
{
    public void Configure(EntityTypeBuilder<StudentProfileExt> builder)
    {
        builder.ToTable("StudentProfileExts", "dbo");
        builder.ConfigureEntityBase();
        builder.ConfigureSoftDelete();

        builder.Property(x => x.StudentCode).IsRequired().HasMaxLength(30);
        builder.HasIndex(x => x.StudentCode).IsUnique();
        builder.Property(x => x.AdmissionRoundCode).HasMaxLength(50);
        builder.Property(x => x.SeniorProjectTitleTh).HasMaxLength(500);
        builder.Property(x => x.SeniorProjectTitleEn).HasMaxLength(500);
    }
}

public sealed class StudentAchievementExtConfiguration : IEntityTypeConfiguration<StudentAchievementExt>
{
    public void Configure(EntityTypeBuilder<StudentAchievementExt> builder)
    {
        builder.ToTable("StudentAchievements", "dbo");
        builder.ConfigureEntityBase();
        builder.ConfigureSoftDelete();

        builder.Property(x => x.StudentCode).IsRequired().HasMaxLength(30);
        builder.HasIndex(x => x.StudentCode);
        builder.Property(x => x.EventName).IsRequired().HasMaxLength(500);
        builder.Property(x => x.AwardName).IsRequired().HasMaxLength(300);
        builder.Property(x => x.GrantAmount).HasPrecision(18, 2);
    }
}

public sealed class StudentLicenseResultExtConfiguration : IEntityTypeConfiguration<StudentLicenseResultExt>
{
    public void Configure(EntityTypeBuilder<StudentLicenseResultExt> builder)
    {
        builder.ToTable("StudentLicenseResults", "dbo");
        builder.ConfigureEntityBase();

        builder.Property(x => x.StudentCode).IsRequired().HasMaxLength(30);
        builder.HasIndex(x => x.StudentCode).IsUnique();
    }
}

public sealed class StudentEmploymentRound1ExtConfiguration : IEntityTypeConfiguration<StudentEmploymentRound1Ext>
{
    public void Configure(EntityTypeBuilder<StudentEmploymentRound1Ext> builder)
    {
        builder.ToTable("StudentEmploymentRound1s", "dbo");
        builder.ConfigureEntityBase();

        builder.Property(x => x.StudentCode).IsRequired().HasMaxLength(30);
        builder.HasIndex(x => x.StudentCode).IsUnique();
        builder.Property(x => x.EmploymentStatus).IsRequired().HasMaxLength(100);
        builder.Property(x => x.JobTitle).HasMaxLength(300);
        builder.Property(x => x.WorkplaceName).HasMaxLength(300);
    }
}
