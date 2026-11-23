using Chula.Application.Common.Exceptions;
using Chula.Application.Features.Faculty.FacultyMembers.Queries.GetFacultyMemberById;
using Chula.Application.Features.Training.TrainingBudgetPolicies.Commands.DeleteFacultyBudgetAllocation;
using Chula.Application.Features.Training.TrainingBudgetPolicies.Commands.UpsertFacultyBudgetAllocation;
using Chula.Application.Features.Training.TrainingBudgetPolicies.Commands.UpsertTrainingBudgetPolicy;
using Chula.Application.Features.Training.TrainingRequests.Commands.ApproveTrainingRequest;
using Chula.Application.Features.Training.TrainingRequests.Commands.CancelTrainingRequest;
using Chula.Application.Features.Training.TrainingRequests.Commands.CreateTrainingRequest;
using Chula.Application.Features.Training.TrainingRequests.Commands.ImportHistoricalTrainings;
using Chula.Application.Features.Training.TrainingRequests.Commands.RequestTrainingRevision;
using Chula.Application.Features.Training.TrainingRequests.Commands.VerifyTrainingRequest;
using Chula.Application.Features.Training.TrainingRequests.Queries.GetFacultyBudgetSummaries;
using Chula.Application.Features.Training.TrainingRequests.Queries.GetTrainingEventSummaries;
using Chula.Application.Features.Training.TrainingRequests.Queries.GetTrainingRequestById;
using Chula.Domain.Common;
using Chula.Domain.Enums.Training;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Chula.Application.FunctionalTests.Training;

[TestFixture]
public sealed class TrainingWorkflowAndBudgetLedgerTests : FunctionalTestBase
{
    [Test]
    public async Task ApproveAcademicFundedTraining_WithFutureQuotaBorrowing_ShouldDeductNextFiscalYearAndLinkToFacultyProfile()
    {
        // Configure clean FY 2572 & FY 2573 policies (10,000 THB each, borrowing enabled up to 10,000 THB)
        await SendAsync(new UpsertTrainingBudgetPolicyCommand(2572, 10_000m, true, 10_000m));
        await SendAsync(new UpsertTrainingBudgetPolicyCommand(2573, 10_000m, true, 10_000m));

        var napawan = await WithDbContextAsync(db =>
            db.FacultyMembers.Include(f => f.Department).FirstAsync(x => x.PersonnelCode == "PHARM-1002"));

        // 1. Faculty submits Request #1 in FY 2572 (Nov 2028 -> FY 2572) for 10,000 THB
        CurrentUser.RunAsFaculty(napawan.ExternalId, "test-fac-napawan", "napawan.w");
        var ids1 = await SendAsync(new CreateTrainingRequestCommand(
            new[] { napawan.ExternalId },
            TrainingCategory.AcademicFunded,
            TrainingWorkType.Research,
            "การประชุมวิจัยเภสัชศาสตร์ 2572",
            "กรุงเทพฯ",
            new DateOnly(2028, 11, 10),
            new DateOnly(2028, 11, 12),
            RequestedAmount: 10_000m,
            TitleEn: "Pharmacy Research Conference 2028",
            BankName: "SCB",
            BankAccountNumber: "111-2-33333-4",
            BankAccountHolderName: "ศ.ดร.ภญ.นภาวรรณ วิทยาการยา",
            SubmitImmediately: true));
        var req1Id = ids1.Single();

        // Owner CAN view own bank account; Staff WITHOUT training-requests.view-bank-account CANNOT!
        var detailAsOwner = await SendAsync(new GetTrainingRequestByIdQuery(req1Id));
        detailAsOwner.CanViewBankAccount.ShouldBeTrue();
        detailAsOwner.BankAccountNumber.ShouldBe("111-2-33333-4");

        CurrentUser.RunAsDepartmentStaff(napawan.Department.ExternalId, "test-staff-pi", "staff.pi");
        var detailAsStaffNoBankPerm = await SendAsync(new GetTrainingRequestByIdQuery(req1Id));
        detailAsStaffNoBankPerm.CanViewBankAccount.ShouldBeFalse();
        detailAsStaffNoBankPerm.BankAccountNumber.ShouldBeNull();

        // 2. Staff verifies -> Budget is NOT deducted yet (A05)
        await SendAsync(new VerifyTrainingRequestCommand(req1Id, "เอกสารครบถ้วน"));
        var budgetBeforeApproval = await SendAsync(new GetFacultyBudgetSummariesQuery(2572));
        var napawanBefore = budgetBeforeApproval.Items.First(x => x.FacultyMemberExternalId == napawan.ExternalId);
        napawanBefore.ConsumedInYear.ShouldBe(0m);
        napawanBefore.RemainingInYear.ShouldBe(10_000m);

        // 3. Staff records Dean Approval -> Consumes 10,000 THB in FY 2572
        var approve1 = await SendAsync(new ApproveTrainingRequestCommand(req1Id, 10_000m, "คณบดีลงนามแล้ว"));
        approve1.ConsumedFromCurrentYear.ShouldBe(10_000m);
        approve1.BorrowedFromNextYear.ShouldBe(0m);

        // 4. Faculty submits Request #2 in FY 2572 for 5,000 THB -> Must borrow 5,000 THB from FY 2573 (A06)!
        CurrentUser.RunAsFaculty(napawan.ExternalId, "test-fac-napawan", "napawan.w");
        var ids2 = await SendAsync(new CreateTrainingRequestCommand(
            new[] { napawan.ExternalId },
            TrainingCategory.AcademicFunded,
            TrainingWorkType.Academic,
            "สัมมนาวิชาการต่อเนื่อง 2572",
            "จุฬาฯ",
            new DateOnly(2029, 3, 5),
            new DateOnly(2029, 3, 6),
            RequestedAmount: 5_000m,
            SubmitImmediately: true));
        var req2Id = ids2.Single();

        CurrentUser.RunAsDepartmentStaff(napawan.Department.ExternalId, "test-staff-pi", "staff.pi");
        await SendAsync(new VerifyTrainingRequestCommand(req2Id));
        var approve2 = await SendAsync(new ApproveTrainingRequestCommand(req2Id, 5_000m));
        approve2.ConsumedFromCurrentYear.ShouldBe(0m);
        approve2.BorrowedFromNextYear.ShouldBe(5_000m);

        // Verify FY 2572 & FY 2573 derived balances match A06 example:
        // FY 2572: Consumed=10,000, BorrowedFromNextYear=5,000, Remaining=0, Borrowable=5,000
        // FY 2573: CarryForwardDeducted=5,000, Remaining=5,000!
        var summary2572 = await SendAsync(new GetFacultyBudgetSummariesQuery(2572));
        var row2572 = summary2572.Items.First(x => x.FacultyMemberExternalId == napawan.ExternalId);
        row2572.ConsumedInYear.ShouldBe(10_000m);
        row2572.BorrowedFromNextYear.ShouldBe(5_000m);
        row2572.RemainingInYear.ShouldBe(0m);
        row2572.BorrowableFromNextYear.ShouldBe(5_000m);

        var summary2573 = await SendAsync(new GetFacultyBudgetSummariesQuery(2573));
        var row2573 = summary2573.Items.First(x => x.FacultyMemberExternalId == napawan.ExternalId);
        row2573.CarryForwardDeductedInYear.ShouldBe(5_000m);
        row2573.RemainingInYear.ShouldBe(5_000m);

        // 5. Request #3 in FY 2572 for 6,000 THB exceeds remaining borrowable 5,000 THB -> MUST FAIL!
        CurrentUser.RunAsAdmin();
        var ids3 = await SendAsync(new CreateTrainingRequestCommand(
            new[] { napawan.ExternalId },
            TrainingCategory.AcademicFunded,
            TrainingWorkType.Academic,
            "อบรมเกินวงเงินยืม",
            "กรุงเทพฯ",
            new DateOnly(2029, 5, 1),
            new DateOnly(2029, 5, 2),
            RequestedAmount: 6_000m,
            SubmitImmediately: true));
        await SendAsync(new VerifyTrainingRequestCommand(ids3.Single()));
        await Should.ThrowAsync<DomainException>(() =>
            SendAsync(new ApproveTrainingRequestCommand(ids3.Single(), 6_000m)));

        // Cancel Request #3 cleanly before approval
        await SendAsync(new CancelTrainingRequestCommand(ids3.Single(), "เกินวงเงิน"));

        // 6. Verify auto-linked Approved trainings appear in Faculty Profile Section 8 + Event Summary
        var facultyProfile = await SendAsync(new GetFacultyMemberByIdQuery(napawan.ExternalId));
        facultyProfile.ApprovedTrainings.ShouldContain(t => t.ExternalId == req1Id);
        facultyProfile.ApprovedTrainings.ShouldContain(t => t.ExternalId == req2Id);
        facultyProfile.ApprovedTrainings.ShouldNotContain(t => t.ExternalId == ids3.Single());

        var events2572 = await SendAsync(new GetTrainingEventSummariesQuery(2572));
        events2572.ShouldContain(e => e.TitleTh == "การประชุมวิจัยเภสัชศาสตร์ 2572" && e.AverageAmountPerAttendee == 10_000m);
    }

    [Test]
    public async Task HistoricalImport_AndDepartmentScoping_ShouldBeIdempotentAndEnforced()
    {
        CurrentUser.RunAsAdmin();
        var items = new[]
        {
            new HistoricalTrainingImportItem(
                "HIST-2568-001",
                "PHARM-1001",
                TrainingCategory.AcademicFunded,
                TrainingWorkType.Academic,
                "การอบรมย้อนหลังปี 2568",
                "Historical Training 2025",
                "คณะเภสัชศาสตร์",
                "Faculty of Pharmacy",
                new DateOnly(2025, 3, 10),
                new DateOnly(2025, 3, 11),
                3_500m)
        };

        var res1 = await SendAsync(new ImportHistoricalTrainingsCommand(items, DeductFromBudgetLedger: true));
        res1.ImportedCount.ShouldBe(1);
        res1.SkippedDuplicateCount.ShouldBe(0);

        // Re-importing same SourceRecordId must be idempotent
        var res2 = await SendAsync(new ImportHistoricalTrainingsCommand(items, DeductFromBudgetLedger: true));
        res2.ImportedCount.ShouldBe(0);
        res2.SkippedDuplicateCount.ShouldBe(1);

        // Revision request by Staff outside department must throw ForbiddenAccessException (A03)
        var somchai = await WithDbContextAsync(db =>
            db.FacultyMembers.Include(f => f.Department).FirstAsync(x => x.PersonnelCode == "PHARM-1001"));
        var deptPi = await WithDbContextAsync(db =>
            db.Departments.FirstAsync(x => x.Code == "PI"));

        var reqIds = await SendAsync(new CreateTrainingRequestCommand(
            new[] { somchai.ExternalId },
            TrainingCategory.NonAcademicFunded,
            TrainingWorkType.Academic,
            "งานประชุมนอกสังกัดทดสอบสิทธิ์",
            "กรุงเทพฯ",
            new DateOnly(2027, 4, 1),
            new DateOnly(2027, 4, 2),
            0m,
            SubmitImmediately: true));

        CurrentUser.RunAsDepartmentStaff(deptPi.ExternalId, "staff-pi", "staff.pi");
        await Should.ThrowAsync<ForbiddenAccessException>(() =>
            SendAsync(new RequestTrainingRevisionCommand(reqIds.Single(), "ไม่มีสิทธิ์ข้ามภาควิชา")));
    }

    [Test]
    public async Task PerFacultyBudgetAllocation_ShouldOverrideGlobalPolicyAndSupportRevertAndGlobalReset()
    {
        CurrentUser.RunAsAdmin();

        // Global policy for FY 2574 & 2575 = 10,000 THB/year
        await SendAsync(new UpsertTrainingBudgetPolicyCommand(2574, 10_000m, true, 10_000m));
        await SendAsync(new UpsertTrainingBudgetPolicyCommand(2575, 10_000m, true, 10_000m));

        var somchai = await WithDbContextAsync(db =>
            db.FacultyMembers.FirstAsync(x => x.PersonnelCode == "PHARM-1001"));
        var napawan = await WithDbContextAsync(db =>
            db.FacultyMembers.FirstAsync(x => x.PersonnelCode == "PHARM-1002"));

        // 1. Set individual custom quota for PHARM-1002 = 15,000 THB in FY 2574
        await SendAsync(new UpsertFacultyBudgetAllocationCommand(
            2574,
            napawan.ExternalId,
            AnnualQuotaAmount: 15_000m,
            MaxBorrowAmount: 4_000m,
            Note: "งบพิเศษโครงการวิจัย"));

        var summary1 = await SendAsync(new GetFacultyBudgetSummariesQuery(2574));
        var napawanRow1 = summary1.Items.First(x => x.FacultyMemberExternalId == napawan.ExternalId);
        var somchaiRow1 = summary1.Items.First(x => x.FacultyMemberExternalId == somchai.ExternalId);

        napawanRow1.AnnualQuota.ShouldBe(15_000m);
        napawanRow1.HasCustomAllocation.ShouldBeTrue();
        napawanRow1.CustomMaxBorrowAmount.ShouldBe(4_000m);
        napawanRow1.AllocationNote.ShouldBe("งบพิเศษโครงการวิจัย");
        napawanRow1.BorrowableFromNextYear.ShouldBe(4_000m);

        somchaiRow1.AnnualQuota.ShouldBe(10_000m);
        somchaiRow1.HasCustomAllocation.ShouldBeFalse();

        // 2. Approving a 14,000 THB training in FY 2574 (Nov 2030 -> FY 2574) consumes 14,000 from current year with 0 borrowed!
        var reqIds = await SendAsync(new CreateTrainingRequestCommand(
            new[] { napawan.ExternalId },
            TrainingCategory.AcademicFunded,
            TrainingWorkType.Research,
            "อบรมงบเฉพาะรายบุคคล 2574",
            "จุฬาฯ",
            new DateOnly(2030, 11, 15),
            new DateOnly(2030, 11, 16),
            RequestedAmount: 14_000m,
            SubmitImmediately: true));
        await SendAsync(new VerifyTrainingRequestCommand(reqIds.Single()));
        var approveRes = await SendAsync(new ApproveTrainingRequestCommand(reqIds.Single(), 14_000m));
        approveRes.ConsumedFromCurrentYear.ShouldBe(14_000m);
        approveRes.BorrowedFromNextYear.ShouldBe(0m);

        // 3. Revert PHARM-1002 back to global policy via DeleteFacultyBudgetAllocationCommand
        await SendAsync(new DeleteFacultyBudgetAllocationCommand(2574, napawan.ExternalId));
        var summaryAfterRevert = await SendAsync(new GetFacultyBudgetSummariesQuery(2574));
        var napawanReverted = summaryAfterRevert.Items.First(x => x.FacultyMemberExternalId == napawan.ExternalId);
        napawanReverted.AnnualQuota.ShouldBe(10_000m);
        napawanReverted.HasCustomAllocation.ShouldBeFalse();
        napawanReverted.AllocationNote.ShouldBeNull();

        // 4. Set custom override on PHARM-1001, then apply global policy with ResetAllCustomAllocations = true
        await SendAsync(new UpsertFacultyBudgetAllocationCommand(2574, somchai.ExternalId, 0m, null, "ลาศึกษาต่อ"));
        await SendAsync(new UpsertTrainingBudgetPolicyCommand(2574, 12_000m, true, 12_000m, ResetAllCustomAllocations: true));

        var summaryAfterGlobalReset = await SendAsync(new GetFacultyBudgetSummariesQuery(2574));
        summaryAfterGlobalReset.Items.ShouldAllBe(x => !x.HasCustomAllocation && x.AnnualQuota == 12_000m);
    }
}
