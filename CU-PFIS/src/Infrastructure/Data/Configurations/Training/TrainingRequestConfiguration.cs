using Chula.Domain.Entities.Training;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Training;

public sealed class TrainingRequestConfiguration : IEntityTypeConfiguration<TrainingRequest>
{
    public void Configure(EntityTypeBuilder<TrainingRequest> builder)
    {
        builder.ConfigureEntityBase();
        builder.ConfigureSoftDelete();

        builder.Property(x => x.FiscalYearBe).IsRequired();
        builder.Property(x => x.Category).HasConversion<string>().IsRequired().HasMaxLength(40);
        builder.Property(x => x.WorkType).HasConversion<string>().IsRequired().HasMaxLength(30);
        builder.Property(x => x.Status).HasConversion<string>().IsRequired().HasMaxLength(40);

        builder.Property(x => x.TitleTh).IsRequired().HasMaxLength(500);
        builder.Property(x => x.TitleEn).HasMaxLength(500);
        builder.Property(x => x.SessionTopicTh).HasMaxLength(500);
        builder.Property(x => x.SessionTopicEn).HasMaxLength(500);
        builder.Property(x => x.OrganizerAndVenueTh).IsRequired().HasMaxLength(500);
        builder.Property(x => x.OrganizerAndVenueEn).HasMaxLength(500);

        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.RequestedAmount).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.ApprovedAmount).HasPrecision(18, 2).IsRequired();

        builder.Property(x => x.BankName).HasMaxLength(150);
        builder.Property(x => x.BankAccountNumber).HasMaxLength(50);
        builder.Property(x => x.BankAccountHolderName).HasMaxLength(200);
        builder.Property(x => x.RevisionNote).HasMaxLength(1000);

        builder.Property(x => x.SourceSystem).IsRequired().HasMaxLength(50);
        builder.Property(x => x.SourceRecordId).HasMaxLength(150);

        builder.HasIndex(x => new { x.FiscalYearBe, x.Status });
        builder.HasIndex(x => new { x.FacultyMemberId, x.FiscalYearBe });
        builder.HasIndex(x => x.SourceRecordId);

        builder.HasOne(x => x.FacultyMember)
            .WithMany()
            .HasForeignKey(x => x.FacultyMemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ApprovalHistory)
            .WithOne(h => h.TrainingRequest)
            .HasForeignKey(h => h.TrainingRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.BudgetLedgerEntries)
            .WithOne(l => l.TrainingRequest)
            .HasForeignKey(l => l.TrainingRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(x => x.ApprovalHistory).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(x => x.BudgetLedgerEntries).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("ck_training_requests_date_range", "end_date >= start_date");
            t.HasCheckConstraint("ck_training_requests_requested_nonneg", "requested_amount >= 0");
            t.HasCheckConstraint("ck_training_requests_approved_nonneg", "approved_amount >= 0");
        });
    }
}
