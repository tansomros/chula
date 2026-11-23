using Chula.Domain.Entities.Training;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Training;

public sealed class TrainingApprovalHistoryConfiguration : IEntityTypeConfiguration<TrainingApprovalHistory>
{
    public void Configure(EntityTypeBuilder<TrainingApprovalHistory> builder)
    {
        // Append-only audit trail (never soft-deleted, no IsActive)
        builder.ConfigureEntityBase();

        builder.Property(x => x.FromStatus).HasConversion<string>().IsRequired().HasMaxLength(40);
        builder.Property(x => x.ToStatus).HasConversion<string>().IsRequired().HasMaxLength(40);
        builder.Property(x => x.ActorLogin).IsRequired().HasMaxLength(150);
        builder.Property(x => x.ActionAt).IsRequired();
        builder.Property(x => x.Note).HasMaxLength(1000);

        builder.HasIndex(x => new { x.TrainingRequestId, x.ActionAt });
    }
}
