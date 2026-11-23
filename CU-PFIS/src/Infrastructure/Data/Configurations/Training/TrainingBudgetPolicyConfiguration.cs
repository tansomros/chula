using Chula.Domain.Entities.Training;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.Training;

public sealed class TrainingBudgetPolicyConfiguration : IEntityTypeConfiguration<TrainingBudgetPolicy>
{
    public void Configure(EntityTypeBuilder<TrainingBudgetPolicy> builder)
    {
        builder.ConfigureEntityBase();
        builder.ConfigureActiveFlag();

        builder.Property(x => x.FiscalYearBe).IsRequired();
        builder.Property(x => x.FiscalYearCe).IsRequired();
        builder.HasIndex(x => x.FiscalYearBe).IsUnique();

        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.AnnualQuotaAmount).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.AllowFutureQuotaBorrowing).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.MaxBorrowAmount).HasPrecision(18, 2).IsRequired();

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("ck_training_budget_policies_quota_nonneg", "annual_quota_amount >= 0");
            t.HasCheckConstraint("ck_training_budget_policies_borrow_nonneg", "max_borrow_amount >= 0");
        });
    }
}
