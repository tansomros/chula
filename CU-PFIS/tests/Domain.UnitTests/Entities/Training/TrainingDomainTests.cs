using Chula.Domain.Common;
using Chula.Domain.Entities.Training;
using Chula.Domain.Enums.Training;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.Training;

[TestFixture]
public sealed class TrainingDomainTests
{
    [Test]
    public void FiscalYearCalculator_FollowsThaiFiscalYearOctToSep()
    {
        FiscalYearCalculator.FromDate(new DateOnly(2026, 9, 30)).ShouldBe(2569);
        FiscalYearCalculator.FromDate(new DateOnly(2026, 10, 1)).ShouldBe(2570);
        FiscalYearCalculator.FromDate(new DateOnly(2027, 9, 30)).ShouldBe(2570);
        FiscalYearCalculator.FromDate(new DateOnly(2027, 10, 1)).ShouldBe(2571);
    }

    [Test]
    public void TrainingRequest_EndDateBeforeStartDate_ThrowsDomainException()
    {
        Should.Throw<DomainException>(() => new TrainingRequest(
            facultyMemberId: 1,
            TrainingCategory.AcademicFunded,
            TrainingWorkType.Academic,
            "การอบรมทดสอบ",
            "จุฬาฯ",
            new DateOnly(2026, 11, 15),
            new DateOnly(2026, 11, 10),
            requestedAmount: 3000m));
    }

    [Test]
    public void TrainingRequest_WorkflowStateTransitions_LogAppendOnlyHistory()
    {
        var now = DateTimeOffset.UtcNow;
        var req = new TrainingRequest(
            1,
            TrainingCategory.AcademicFunded,
            TrainingWorkType.Research,
            "งานประชุมวิจัย",
            "ศูนย์ประชุมฯ",
            new DateOnly(2026, 11, 1),
            new DateOnly(2026, 11, 2),
            requestedAmount: 4500m);

        // Cannot approve directly from Draft/Submitted before VerifiedInProgress
        Should.Throw<DomainException>(() => req.ApproveByDean("staff", 4500m, now));

        req.Submit("somchai.p", now);
        req.Status.ShouldBe(TrainingRequestStatus.Submitted);

        req.RequestRevision("staff.pp", "กรุณาระบุสถานที่จัดงานให้ชัดเจน", now.AddHours(1));
        req.Status.ShouldBe(TrainingRequestStatus.RevisionRequested);
        req.RevisionNote.ShouldBe("กรุณาระบุสถานที่จัดงานให้ชัดเจน");

        req.Submit("somchai.p", now.AddHours(2));
        req.MarkVerified("staff.pp", now.AddHours(3));
        req.Status.ShouldBe(TrainingRequestStatus.VerifiedInProgress);

        req.ApproveByDean("staff.pp", 4500m, now.AddHours(4));
        req.Status.ShouldBe(TrainingRequestStatus.Approved);
        req.ApprovedAmount.ShouldBe(4500m);
        req.ApprovalHistory.Count.ShouldBe(5);

        // Cannot cancel once Approved
        Should.Throw<DomainException>(() => req.Cancel("staff.pp", now.AddHours(5)));
    }

    [Test]
    public void TrainingRequest_CancelBeforeApproval_Succeeds()
    {
        var now = DateTimeOffset.UtcNow;
        var req = new TrainingRequest(
            1,
            TrainingCategory.AcademicFunded,
            TrainingWorkType.Academic,
            "งานประชุมวิชาการ",
            "กรุงเทพฯ",
            new DateOnly(2026, 12, 1),
            new DateOnly(2026, 12, 2),
            requestedAmount: 5000m);

        req.Submit("somchai.p", now);
        req.MarkVerified("staff.pp", now.AddHours(1));
        req.Cancel("somchai.p", now.AddHours(2), "ติดภารกิจสอน");

        req.Status.ShouldBe(TrainingRequestStatus.Cancelled);
        req.ApprovalHistory.Count.ShouldBe(3);
    }

    [Test]
    public void TrainingBudgetLedgerEntry_NonAdjustmentMustBePositive()
    {
        Should.Throw<DomainException>(() => new TrainingBudgetLedgerEntry(
            1, 1, 2570, BudgetLedgerEntryType.Consume, 0m, DateTimeOffset.UtcNow, "staff", "Invalid"));

        Should.Throw<DomainException>(() => new TrainingBudgetLedgerEntry(
            1, 1, 2570, BudgetLedgerEntryType.BorrowAdvance, -100m, DateTimeOffset.UtcNow, "staff", "Invalid"));
    }

    [Test]
    public void FacultyTrainingBudgetAllocation_ValidatesNonNegativeAmountsAndRounding()
    {
        Should.Throw<ArgumentException>(() => new FacultyTrainingBudgetAllocation(1, 2570, -1m));
        Should.Throw<ArgumentException>(() => new FacultyTrainingBudgetAllocation(1, 2570, 10_000m, maxBorrowAmount: -50m));
        Should.Throw<ArgumentOutOfRangeException>(() => new FacultyTrainingBudgetAllocation(1, 2400, 10_000m));

        var alloc = new FacultyTrainingBudgetAllocation(1, 2570, 15_000.556m, 5_000.124m, "  งบพิเศษ  ");
        alloc.AnnualQuotaAmount.ShouldBe(15_000.56m);
        alloc.MaxBorrowAmount.ShouldBe(5_000.12m);
        alloc.Note.ShouldBe("งบพิเศษ");

        alloc.UpdateAllocation(0m, null, "ลาศึกษาต่อ");
        alloc.AnnualQuotaAmount.ShouldBe(0m);
        alloc.MaxBorrowAmount.ShouldBeNull();
        alloc.Note.ShouldBe("ลาศึกษาต่อ");
    }
}
