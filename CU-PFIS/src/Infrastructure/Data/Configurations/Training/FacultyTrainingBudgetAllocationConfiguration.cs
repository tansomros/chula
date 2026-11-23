using Chula.Domain.Entities.Training;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Training;

public sealed class FacultyTrainingBudgetAllocationConfiguration
    : IEntityTypeConfiguration<FacultyTrainingBudgetAllocation>
{
    public void Configure(EntityTypeBuilder<FacultyTrainingBudgetAllocation> builder)
    {
        builder.ConfigureEntityBase();

        builder.Property(x => x.FiscalYearBe).IsRequired();
        builder.Property(x => x.AnnualQuotaAmount).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.MaxBorrowAmount).HasPrecision(18, 2);
        builder.Property(x => x.Note).HasMaxLength(500);

        builder.HasIndex(x => new { x.FacultyMemberId, x.FiscalYearBe }).IsUnique();

        builder.HasOne(x => x.FacultyMember)
            .WithMany()
            .HasForeignKey(x => x.FacultyMemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("ck_faculty_budget_alloc_quota_nonneg", "annual_quota_amount >= 0");
            t.HasCheckConstraint(
                "ck_faculty_budget_alloc_borrow_nonneg",
                "max_borrow_amount IS NULL OR max_borrow_amount >= 0");
        });
    }
}
