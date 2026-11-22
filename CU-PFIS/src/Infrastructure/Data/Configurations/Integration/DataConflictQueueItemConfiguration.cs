using Chula.Domain.Entities.Integration;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Integration;

public sealed class DataConflictQueueItemConfiguration : IEntityTypeConfiguration<DataConflictQueueItem>
{
    public void Configure(EntityTypeBuilder<DataConflictQueueItem> builder)
    {
        builder.ConfigureEntityBase();

        builder.Property(x => x.SourceSystem).IsRequired().HasMaxLength(50);
        builder.Property(x => x.EntityType).IsRequired().HasMaxLength(50);
        builder.Property(x => x.TargetExternalId).IsRequired();
        builder.Property(x => x.BusinessKey).IsRequired().HasMaxLength(100);
        builder.Property(x => x.SourceRecordId).IsRequired().HasMaxLength(150);
        builder.Property(x => x.CurrentValuesJson).IsRequired();
        builder.Property(x => x.IncomingValuesJson).IsRequired();
        builder.Property(x => x.ChangedFieldsSummary).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Status).HasConversion<string>().IsRequired().HasMaxLength(40);
        builder.Property(x => x.ResolvedBy).HasMaxLength(150);
        builder.Property(x => x.ResolutionNote).HasMaxLength(1000);

        builder.HasIndex(x => new { x.EntityType, x.Status });
        builder.HasIndex(x => x.TargetExternalId);
    }
}
