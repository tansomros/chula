using Chula.Application.Common.Exceptions;
using Chula.Application.Features.Faculty.FacultyMembers.Commands.CreateFacultyMember;
using Chula.Application.Features.Faculty.FacultyMembers.Commands.ImportScopusPublications;
using Chula.Application.Features.Faculty.FacultyMembers.Commands.UpdateFacultyMember;
using Chula.Application.Features.Faculty.FacultyMembers.Queries.GetFacultyMemberById;
using Chula.Application.Features.Faculty.FacultyMembers.Queries.GetFacultyMembers;
using Chula.Domain.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Chula.Application.FunctionalTests.Faculty;

[TestFixture]
public sealed class FacultyMembersTests : FunctionalTestBase
{
    [Test]
    public async Task CreateAndGetFacultyMember_HappyPathAndDuplicateProtection()
    {
        var (deptId, posId) = await WithDbContextAsync(async db =>
        {
            var dept = await db.Departments.FirstAsync(x => x.Code == "PP");
            var pos = await db.AcademicPositions.FirstAsync(x => x.Code == "ASST");
            return (dept.ExternalId, pos.ExternalId);
        });

        var code = $"PHARM-{Guid.NewGuid().ToString("N")[..6]}";
        var externalId = await SendAsync(new CreateFacultyMemberCommand(
            code,
            "ทดสอบ",
            "อาจารย์",
            "Test",
            "Faculty",
            posId,
            deptId,
            "test.faculty@pharm.chula.ac.th",
            Educations: new[]
            {
                new CreateFacultyEducationItem("ปร.ด.", "Ph.D.", "จุฬาฯ", "Chula", 2021)
            }));

        var detail = await SendAsync(new GetFacultyMemberByIdQuery(externalId));
        detail.PersonnelCode.ShouldBe(code);
        detail.Educations.Count.ShouldBe(1);

        await Should.ThrowAsync<DomainException>(async () =>
            await SendAsync(new CreateFacultyMemberCommand(
                code,
                "ซ้ำ",
                "รหัส",
                "Dup",
                "Code",
                posId,
                deptId,
                "dup@pharm.chula.ac.th")));
    }

    [Test]
    public async Task AccessControl_DepartmentScopedStaffAndOwnCvFaculty_ShouldBeEnforced()
    {
        var (deptPpId, somchaiId, napawanId) = await WithDbContextAsync(async db =>
        {
            var deptPp = await db.Departments.FirstAsync(x => x.Code == "PP");
            var somchai = await db.FacultyMembers.FirstAsync(x => x.PersonnelCode == "PHARM-1001");
            var napawan = await db.FacultyMembers.FirstAsync(x => x.PersonnelCode == "PHARM-1002");
            return (deptPp.ExternalId, somchai.ExternalId, napawan.ExternalId);
        });

        // A03: Staff scoped to PP can see Somchai (PP) but NOT Napawan (PI)
        CurrentUser.RunAsDepartmentStaff(deptPpId);
        var staffList = await SendAsync(new GetFacultyMembersQuery());
        staffList.ShouldAllBe(x => x.DepartmentExternalId == deptPpId);

        await Should.ThrowAsync<ForbiddenAccessException>(async () =>
            await SendAsync(new GetFacultyMemberByIdQuery(napawanId)));

        // Faculty can view own CV, but NOT another faculty member's CV
        CurrentUser.RunAsFaculty(somchaiId);
        var ownDetail = await SendAsync(new GetFacultyMemberByIdQuery(somchaiId));
        ownDetail.ExternalId.ShouldBe(somchaiId);

        await Should.ThrowAsync<ForbiddenAccessException>(async () =>
            await SendAsync(new GetFacultyMemberByIdQuery(napawanId)));
    }

    [Test]
    public async Task ImportScopusPublications_ShouldDeduplicateByEidAndDoi()
    {
        var somchaiId = await WithDbContextAsync(async db =>
            (await db.FacultyMembers.FirstAsync(x => x.PersonnelCode == "PHARM-1001")).ExternalId);

        var uniqueEid = $"2-s2.0-{Guid.NewGuid().ToString("N")[..10]}";
        var uniqueDoi = $"10.1016/test.{Guid.NewGuid().ToString("N")[..8]}";

        var firstRun = await SendAsync(new ImportScopusPublicationsCommand(
            somchaiId,
            new[]
            {
                new ScopusPublicationImportItem("New Scopus Paper", "Pharm Journal", 2026, uniqueDoi, uniqueEid)
            }));

        firstRun.AddedCount.ShouldBe(1);
        firstRun.SkippedDuplicateCount.ShouldBe(0);

        var secondRun = await SendAsync(new ImportScopusPublicationsCommand(
            somchaiId,
            new[]
            {
                new ScopusPublicationImportItem("Duplicate By EID", "Pharm Journal", 2026, null, uniqueEid),
                new ScopusPublicationImportItem("Duplicate By DOI", "Pharm Journal", 2026, uniqueDoi.ToUpperInvariant(), "2-s2.0-other")
            }));

        secondRun.AddedCount.ShouldBe(0);
        secondRun.SkippedDuplicateCount.ShouldBe(2);
    }

    [Test]
    public async Task GetFacultyMemberById_WhenNotFound_ShouldThrowNotFoundException()
    {
        await Should.ThrowAsync<NotFoundException>(async () =>
            await SendAsync(new GetFacultyMemberByIdQuery(Guid.CreateVersion7())));
    }
}
