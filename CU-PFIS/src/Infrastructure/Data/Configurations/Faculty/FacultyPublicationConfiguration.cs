using Chula.Domain.Entities.Faculty;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Faculty;

public sealed class FacultyPublicationConfiguration : IEntityTypeConfiguration<FacultyPublication>
{
    public void Configure(EntityTypeBuilder<FacultyPublication> builder)
    {
        builder.ConfigureEntityBase();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
        builder.Property(x => x.JournalOrVenue).HasMaxLength(300);
        builder.Property(x => x.PublishedYear).IsRequired();
        builder.Property(x => x.Doi).HasMaxLength(200);
        builder.Property(x => x.ScopusId).HasMaxLength(100);
        builder.Property(x => x.SourceSystem).IsRequired().HasMaxLength(50);
        builder.Property(x => x.SourceRecordId).HasMaxLength(200);

        builder.HasIndex(x => new { x.FacultyMemberId, x.ScopusId });
    }
}
