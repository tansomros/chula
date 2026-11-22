using Chula.Domain.Entities.Students;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.Students;

[TestFixture]
public class StudentTests
{
    [Test]
    public void Constructor_WithValidFields_ShouldInitializeWithCuppepProvenance()
    {
        var importedAt = DateTimeOffset.UtcNow;

        var student = new Student(
            "6433012533",
            "กิตติภพ",
            "รัตนเภสัช",
            "Kittiphop",
            "Rattanaphesat",
            1,
            6,
            1,
            departmentId: 1,
            admissionRoundId: 2,
            academicAdvisorId: 1,
            highSchoolTh: "โรงเรียนเตรียมอุดมศึกษา",
            highSchoolEn: "Triam Udom Suksa School",
            sourceSystem: "CUPPEP",
            sourceRecordId: "CUPPEP-STU-6433012533",
            importedAt: importedAt);

        student.ExternalId.ShouldNotBe(Guid.Empty);
        student.StudentCode.ShouldBe("6433012533");
        student.SourceSystem.ShouldBe("CUPPEP");
        student.SourceRecordId.ShouldBe("CUPPEP-STU-6433012533");
        student.IsStaffVerifiedEdit.ShouldBeFalse();
        student.IsActive.ShouldBeTrue();
    }

    [Test]
    public void Constructor_WithOutOfRangeStudyYear_ShouldThrowArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() =>
            new Student(
                "6433012533",
                "กิตติภพ",
                "รัตนเภสัช",
                "Kittiphop",
                "Rattanaphesat",
                1,
                0,
                1));
    }

    [Test]
    public void UpdateProfileByStaff_ShouldUpdateFieldsAndSetStaffVerifiedEditFlag()
    {
        var student = new Student(
            "6433012533",
            "กิตติภพ",
            "รัตนเภสัช",
            "Kittiphop",
            "Rattanaphesat",
            1,
            5,
            1);

        student.UpdateProfileByStaff(
            "กิตติภพ",
            "รัตนเภสัช",
            "Kittiphop",
            "Rattanaphesat",
            6,
            2,
            1,
            3,
            1,
            "โรงเรียนเตรียมอุดมศึกษา",
            "Triam Udom Suksa School");

        student.StudyYear.ShouldBe(6);
        student.MajorId.ShouldBe(2);
        student.AdmissionRoundId.ShouldBe(3);
        student.IsStaffVerifiedEdit.ShouldBeTrue();
    }

    [Test]
    public void AddInternship_WithValidFields_ShouldAddToCollection()
    {
        var student = new Student(
            "6433012533",
            "กิตติภพ",
            "รัตนเภสัช",
            "Kittiphop",
            "Rattanaphesat",
            1,
            6,
            1);

        var internship = student.AddInternship(
            "โรงพยาบาลจุฬาลงกรณ์",
            "บริบาลผู้ป่วยใน",
            "พ.ค. – มิ.ย. 2569",
            "King Chulalongkorn Memorial Hospital",
            "Inpatient Clinical Pharmacy",
            "ROT-101");

        internship.ExternalId.ShouldNotBe(Guid.Empty);
        student.Internships.Count.ShouldBe(1);
    }
}
