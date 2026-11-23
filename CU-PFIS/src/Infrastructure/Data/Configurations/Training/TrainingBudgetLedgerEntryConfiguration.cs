using Chula.Domain.Entities.Training;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Training;

public sealed class TrainingBudgetLedgerEntryConfiguration : IEntityTypeConfiguration<TrainingBudgetLedgerEntry>
{
    public void Configure(EntityTypeBuilder<TrainingBudgetLedgerEntry> builder)
    {
        // Append-only financial ledger (never soft-deleted, no IsActive)
        builder.ConfigureEntityBase();

        builder.Property(x => x.FiscalYearBe).IsRequired();
        builder.Property(x => x.EntryType).HasConversion<string>().IsRequired().HasMaxLength(40);
        builder.Property(x => x.Amount).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.EffectiveAt).IsRequired();
        builder.Property(x => x.RecordedBy).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(500);

        builder.HasIndex(x => new { x.FacultyMemberId, x.FiscalYearBe });
        builder.HasIndex(x => x.TrainingRequestId);

        builder.HasOne(x => x.FacultyMember)
            .WithMany()
            .HasForeignKey(x => x.FacultyMemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
