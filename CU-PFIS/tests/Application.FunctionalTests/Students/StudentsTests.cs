using Chula.Application.Common.Exceptions;
using Chula.Application.Features.Students.Students.Commands.UpdateStudent;
using Chula.Application.Features.Students.Students.Queries.GetStudentById;
using Chula.Application.Features.Students.Students.Queries.GetStudents;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Chula.Application.FunctionalTests.Students;

[TestFixture]
public sealed class StudentsTests : FunctionalTestBase
{
    [Test]
    public async Task UpdateStudentByStaff_ShouldSetStaffVerifiedEditFlagAndEnforceDeptScope()
    {
        var (deptPpId, student1Id, student2Id, majorPcId, cotmesId) = await WithDbContextAsync(async db =>
        {
            var deptPp = await db.Departments.FirstAsync(x => x.Code == "PP");
            var s1 = await db.Students.FirstAsync(x => x.StudentCode == "6433012533");
            var s2 = await db.Students.FirstAsync(x => x.StudentCode == "6533048233");
            var majorPc = await db.Majors.FirstAsync(x => x.Code == "PC");
            var cotmes = await db.AdmissionRounds.FirstAsync(x => x.Code == "COTMES");
            return (deptPp.ExternalId, s1.ExternalId, s2.ExternalId, majorPc.ExternalId, cotmes.ExternalId);
        });

        CurrentUser.RunAsDepartmentStaff(deptPpId);

        var list = await SendAsync(new GetStudentsQuery());
        list.ShouldAllBe(x => x.DepartmentExternalId == deptPpId);

        await SendAsync(new UpdateStudentCommand(
            student1Id,
            "กิตติภพ",
            "รัตนเภสัช",
            "Kittiphop",
            "Rattanaphesat",
            6,
            majorPcId,
            deptPpId,
            cotmesId,
            null,
            "โรงเรียนเตรียมอุดมศึกษา",
            "Triam Udom Suksa School"));

        var updated = await SendAsync(new GetStudentByIdQuery(student1Id));
        updated.IsStaffVerifiedEdit.ShouldBeTrue();
        updated.AdmissionRoundExternalId.ShouldBe(cotmesId);

        // Staff in PP cannot access/edit student in PI
        await Should.ThrowAsync<ForbiddenAccessException>(async () =>
            await SendAsync(new GetStudentByIdQuery(student2Id)));
    }

    [Test]
    public async Task GetStudentById_WhenNotFound_ShouldThrowNotFoundException()
    {
        await Should.ThrowAsync<NotFoundException>(async () =>
            await SendAsync(new GetStudentByIdQuery(Guid.CreateVersion7())));
    }
}
