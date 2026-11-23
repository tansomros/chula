using Chula.Application.Common.Interfaces;
using Chula.Application.Features.Integration.CuppepSync.Commands.ResolveDataConflict;
using Chula.Application.Features.Integration.CuppepSync.Commands.SyncCuppepStudents;
using Chula.Application.Features.Integration.CuppepSync.Queries.GetDataConflicts;
using Chula.Application.Features.Integration.CuppepSync.Queries.GetImportBatches;
using Chula.Application.Features.Students.Students.Commands.UpdateStudent;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Chula.Application.FunctionalTests.Integration;

[TestFixture]
public sealed class CuppepSyncTests : FunctionalTestBase
{
    [Test]
    public async Task SyncCuppepStudents_ShouldCreateUpdateSkipAndProtectStaffVerifiedRecords()
    {
        var now = DateTimeOffset.UtcNow;

        // 1. First pull creates 2 new students (including a legacy cohort year 2559 + advisor + internship rotations!)
        CuppepDataSource.NextStudents.AddRange(
        [
            new CuppepStudentSnapshot(
                "CUPPEP-SYNC-1",
                "6733090133",
                "ปิยะพงษ์",
                "ซิงก์ทดสอบ",
                "Piyapong",
                "SyncTest",
                2567,
                2,
                "PC",
                "โรงเรียนสวนกุหลาบวิทยาลัย",
                null,
                now,
                AcademicAdvisorNameTh: "รศ.ดร.ภก.สมชาย เภสัชศาสตร์",
                AcademicAdvisorFirstNameTh: "สมชาย",
                AcademicAdvisorLastNameTh: "เภสัชศาสตร์",
                Internships:
                [
                    new CuppepInternshipSnapshot(
                        "CUPPEP-ROT-9001",
                        "บริษัท สยามเภสัช จำกัด (กรุงเทพฯ)",
                        null,
                        "การปฏิบัติงานวิชาชีพในโรงงานอุตสาหกรรมยา 6",
                        "Professional Practice in Pharmaceutical Industry VI",
                        "ผลัด 8 · 18 มกราคม - 26 กุมภาพันธ์ 2570")
                ]),
            new CuppepStudentSnapshot(
                "CUPPEP-SYNC-2",
                "5933090233",
                "สุพัตรา",
                "รุ่นเก่า",
                "Supattra",
                "LegacyCohort",
                2559,
                6,
                "IP",
                "โรงเรียนบดินทรเดชา",
                null,
                now,
                AcademicAdvisorNameTh: "อ.ดร.ภญ.ปรรณพัชร์ ลักษมีเศรษฐ์")
        ]);

        var batch1 = await SendAsync(new SyncCuppepStudentsCommand());
        batch1.Status.ShouldBe("Completed");
        batch1.TotalPulledCount.ShouldBe(2);
        batch1.CreatedCount.ShouldBe(2);
        batch1.UpdatedCount.ShouldBe(0);
        batch1.SkippedUnchangedCount.ShouldBe(0);
        batch1.ConflictCount.ShouldBe(0);

        // Verify Student #1 auto-linked to seeded FacultyMember (สมชาย) + synced 1 internship,
        // and Student #2 preserved fallback AcademicAdvisorNameTh from CUPPEP
        var syncedStudent1 = await WithDbContextAsync(db =>
            db.Students
                .Include(x => x.AcademicAdvisor)
                .Include(x => x.Internships)
                .FirstAsync(x => x.StudentCode == "6733090133"));
        syncedStudent1.AcademicAdvisor.ShouldNotBeNull();
        syncedStudent1.AcademicAdvisor!.PersonnelCode.ShouldBe("PHARM-1001");
        syncedStudent1.Internships.Count.ShouldBe(1);
        syncedStudent1.Internships.First().RotationTypeEn.ShouldBe("Professional Practice in Pharmaceutical Industry VI");

        var syncedStudent2 = await WithDbContextAsync(db =>
            db.Students.FirstAsync(x => x.StudentCode == "5933090233"));
        syncedStudent2.AcademicAdvisorId.ShouldBeNull();
        syncedStudent2.AcademicAdvisorNameTh.ShouldBe("อ.ดร.ภญ.ปรรณพัชร์ ลักษมีเศรษฐ์");

        // 2. Re-pulling exact same data is idempotent
        var batch2 = await SendAsync(new SyncCuppepStudentsCommand());
        batch2.Status.ShouldBe("Completed");
        batch2.CreatedCount.ShouldBe(0);
        batch2.UpdatedCount.ShouldBe(0);
        batch2.SkippedUnchangedCount.ShouldBe(2);
        batch2.ConflictCount.ShouldBe(0);

        // 3. Staff edits student #1 in CU-PFIS (marks IsStaffVerifiedEdit = true)
        var student1 = await WithDbContextAsync(db =>
            db.Students.Include(x => x.Major).FirstAsync(x => x.StudentCode == "6733090133"));

        await SendAsync(new UpdateStudentCommand(
            student1.ExternalId,
            "ปิยะพงษ์ (ยืนยันแล้ว)",
            "ซิงก์ทดสอบ",
            "Piyapong",
            "SyncTest",
            2,
            student1.Major.ExternalId,
            HighSchoolTh: "โรงเรียนสวนกุหลาบวิทยาลัย (ตรวจสอบแล้ว)"));

        // 4. External CUPPEP changes BOTH students:
        //    - Student #1 (staff-verified) MUST NOT be overwritten -> routed to DataConflictQueue
        //    - Student #2 (unverified) updates cleanly
        CuppepDataSource.Reset();
        CuppepDataSource.NextStudents.AddRange(
        [
            new CuppepStudentSnapshot(
                "CUPPEP-SYNC-1",
                "6733090133",
                "ปิยะพงษ์ (จาก CUPPEP)",
                "ซิงก์ทดสอบ",
                "Piyapong",
                "SyncTest",
                2567,
                3,
                "PC",
                "โรงเรียนสวนกุหลาบฯ",
                null,
                now.AddMinutes(5)),
            new CuppepStudentSnapshot(
                "CUPPEP-SYNC-2",
                "5933090233",
                "สุพัตรา",
                "รุ่นเก่าอัปเดต",
                "Supattra",
                "LegacyUpdated",
                2559,
                6,
                "IP",
                "โรงเรียนบดินทรเดชา",
                null,
                now.AddMinutes(5))
        ]);

        var batch3 = await SendAsync(new SyncCuppepStudentsCommand());
        batch3.Status.ShouldBe("CompletedWithConflicts");
        batch3.UpdatedCount.ShouldBe(1);
        batch3.ConflictCount.ShouldBe(1);

        // Verify Student #1 was NOT overwritten, while Student #2 WAS updated
        var reloaded1 = await WithDbContextAsync(db =>
            db.Students.FirstAsync(x => x.StudentCode == "6733090133"));
        reloaded1.FirstNameTh.ShouldBe("ปิยะพงษ์ (ยืนยันแล้ว)");
        reloaded1.IsStaffVerifiedEdit.ShouldBeTrue();

        var reloaded2 = await WithDbContextAsync(db =>
            db.Students.FirstAsync(x => x.StudentCode == "5933090233"));
        reloaded2.LastNameTh.ShouldBe("รุ่นเก่าอัปเดต");

        // 5. Inspect Pending Conflicts & Resolve with AcceptIncoming
        var pendingConflicts = await SendAsync(new GetDataConflictsQuery("Pending"));
        var targetConflict = pendingConflicts.FirstOrDefault(c => c.BusinessKey == "6733090133");
        targetConflict.ShouldNotBeNull();
        targetConflict.ChangedFieldsSummary.ShouldContain("FirstNameTh");

        await SendAsync(new ResolveDataConflictCommand(
            targetConflict.ExternalId,
            "AcceptIncoming",
            "Confirmed latest CUPPEP name"));

        var afterAccept = await WithDbContextAsync(db =>
            db.Students.FirstAsync(x => x.StudentCode == "6733090133"));
        afterAccept.FirstNameTh.ShouldBe("ปิยะพงษ์ (จาก CUPPEP)");
        afterAccept.StudyYear.ShouldBe(3);
        afterAccept.IsStaffVerifiedEdit.ShouldBeTrue();

        var batches = await SendAsync(new GetImportBatchesQuery(10));
        batches.Count.ShouldBeGreaterThanOrEqualTo(3);
    }
}
