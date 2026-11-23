using Chula.Domain.Common;
using Chula.Domain.Entities.Integration;
using Chula.Domain.Entities.Students;
using Chula.Domain.Enums.Integration;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.Integration;

[TestFixture]
public sealed class CuppepSyncDomainTests
{
    [Test]
    public void ImportBatch_Complete_SetsCompletedWhenZeroConflicts()
    {
        var now = DateTimeOffset.UtcNow;
        var batch = new ImportBatch("CUPPEP", "Student", now, null, "admin.pfis");

        batch.Complete(10, created: 4, updated: 3, skippedUnchanged: 3, conflicts: 0, now.AddSeconds(2));

        batch.Status.ShouldBe(ImportBatchStatus.Completed);
        batch.TotalPulledCount.ShouldBe(10);
        batch.CreatedCount.ShouldBe(4);
        batch.UpdatedCount.ShouldBe(3);
        batch.SkippedUnchangedCount.ShouldBe(3);
        batch.ConflictCount.ShouldBe(0);
    }

    [Test]
    public void ImportBatch_Complete_SetsCompletedWithConflictsWhenConflictsPresent()
    {
        var now = DateTimeOffset.UtcNow;
        var batch = new ImportBatch("CUPPEP", "Student", now);

        batch.Complete(5, created: 1, updated: 0, skippedUnchanged: 3, conflicts: 1, now.AddSeconds(1));

        batch.Status.ShouldBe(ImportBatchStatus.CompletedWithConflicts);
        batch.ConflictCount.ShouldBe(1);
    }

    [Test]
    public void DataConflictQueueItem_CannotBeResolvedTwice()
    {
        var conflict = new DataConflictQueueItem(
            1,
            "CUPPEP",
            "Student",
            Guid.CreateVersion7(),
            "6633000133",
            "1001",
            "{}",
            "{}",
            "FirstNameTh");

        conflict.ResolveKeepCuPfis("staff.pp", DateTimeOffset.UtcNow, "Verified by staff");
        conflict.Status.ShouldBe(ConflictResolutionStatus.KeepCuPfis);

        Should.Throw<DomainException>(() =>
            conflict.ResolveAcceptIncoming("staff.pp", DateTimeOffset.UtcNow));
    }

    [Test]
    public void Student_ApplyExternalSyncUpdate_ThrowsWhenStaffVerifiedEdit()
    {
        var student = new Student(
            "6633009933",
            "กนกพร",
            "เภสัชดี",
            "Kanokporn",
            "Phesatdee",
            cohortAcademicYearId: 1,
            studyYear: 3,
            majorId: 1);

        student.UpdateProfileByStaff(
            "กนกพร (แก้ไข)",
            "เภสัชดี",
            "Kanokporn",
            "Phesatdee",
            3,
            1,
            null,
            null,
            null,
            "โรงเรียนเตรียมอุดมศึกษา",
            null);

        student.IsStaffVerifiedEdit.ShouldBeTrue();

        Should.Throw<DomainException>(() =>
            student.ApplyExternalSyncUpdate(
                "กนกพร",
                "เภสัชดี",
                "Kanokporn",
                "Phesatdee",
                3,
                1,
                "โรงเรียนอื่น",
                null,
                "CUPPEP-99",
                DateTimeOffset.UtcNow));
    }

    [Test]
    public void Student_ApplyConflictResolutionAcceptExternal_UpdatesWhileKeepingStaffVerified()
    {
        var student = new Student(
            "6633009934",
            "ธนวัฒน์",
            "ยาไทย",
            "Thanawat",
            "Yathai",
            cohortAcademicYearId: 1,
            studyYear: 2,
            majorId: 1);

        student.UpdateProfileByStaff(
            "ธนวัฒน์",
            "ยาไทย",
            "Thanawat",
            "Yathai",
            2,
            1,
            null,
            null,
            null,
            "โรงเรียนเดิม",
            null);

        student.ApplyConflictResolutionAcceptExternal(
            "ธนวัฒน์",
            "ยาไทยใหม่",
            "Thanawat",
            "YathaiNew",
            3,
            2,
            "โรงเรียนใหม่",
            null,
            "CUPPEP-100",
            DateTimeOffset.UtcNow);

        student.LastNameTh.ShouldBe("ยาไทยใหม่");
        student.StudyYear.ShouldBe(3);
        student.MajorId.ShouldBe(2);
        student.HighSchoolTh.ShouldBe("โรงเรียนใหม่");
        student.IsStaffVerifiedEdit.ShouldBeTrue();
    }

    [Test]
    public void Student_UpsertExternalInternship_IsIdempotentAndUpdatesOnChange()
    {
        var student = new Student(
            "6633009935",
            "พิมพา",
            "ฝึกงานดี",
            "Pimpa",
            "InternGood",
            cohortAcademicYearId: 1,
            studyYear: 6,
            majorId: 1);

        student.UpsertExternalInternship(
            "CUPPEP-ROT-101",
            "บริษัท สยามเภสัช จำกัด",
            "การปฏิบัติงานวิชาชีพ 1",
            "ผลัด 1 · พ.ค. 2569",
            rotationTypeEn: "Professional Practice I").ShouldBeTrue();

        // Exact same payload -> idempotent (false)
        student.UpsertExternalInternship(
            "CUPPEP-ROT-101",
            "บริษัท สยามเภสัช จำกัด",
            "การปฏิบัติงานวิชาชีพ 1",
            "ผลัด 1 · พ.ค. 2569",
            rotationTypeEn: "Professional Practice I").ShouldBeFalse();

        student.Internships.Count.ShouldBe(1);

        // Updated period -> updates in place without duplicating
        student.UpsertExternalInternship(
            "CUPPEP-ROT-101",
            "บริษัท สยามเภสัช จำกัด",
            "การปฏิบัติงานวิชาชีพ 1",
            "ผลัด 2 · มิ.ย. 2569",
            rotationTypeEn: "Professional Practice I").ShouldBeTrue();

        student.Internships.Count.ShouldBe(1);
        student.Internships.First().PeriodLabel.ShouldBe("ผลัด 2 · มิ.ย. 2569");
    }
}
