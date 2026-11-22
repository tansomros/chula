using Chula.Domain.Entities.Integration;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Integration;

public sealed class ImportBatchConfiguration : IEntityTypeConfiguration<ImportBatch>
{
    public void Configure(EntityTypeBuilder<ImportBatch> builder)
    {
        builder.ConfigureEntityBase();

        builder.Property(x => x.SourceSystem).IsRequired().HasMaxLength(50);
        builder.Property(x => x.EntityType).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Status).HasConversion<string>().IsRequired().HasMaxLength(40);
        builder.Property(x => x.ErrorMessage).HasMaxLength(2000);
        builder.Property(x => x.TriggeredBy).HasMaxLength(150);

        builder.HasIndex(x => new { x.SourceSystem, x.EntityType, x.StartedAt });

        builder.HasMany(x => x.Conflicts)
            .WithOne(c => c.ImportBatch)
            .HasForeignKey(c => c.ImportBatchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Conflicts).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
