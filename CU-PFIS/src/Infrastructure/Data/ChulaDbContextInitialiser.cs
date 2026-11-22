using Chula.Domain.Entities.Faculty;
using Chula.Domain.Entities.ReferenceData;
using Chula.Domain.Entities.Students;
using Chula.Domain.Enums.ReferenceData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chula.Infrastructure.Data;

public sealed class ChulaDbContextInitialiser
{
    private readonly ChulaDbContext _context;
    private readonly ILogger<ChulaDbContextInitialiser> _logger;

    public ChulaDbContextInitialiser(
        ChulaDbContext context,
        ILogger<ChulaDbContextInitialiser> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitialiseAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Database.MigrateAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while applying database migrations.");
            throw;
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await SeedReferenceDataAsync(cancellationToken);
        await SeedSampleFacultyAndStudentsAsync(cancellationToken);
    }

    private async Task SeedReferenceDataAsync(CancellationToken cancellationToken)
    {
        if (!await _context.AcademicYears.AnyAsync(cancellationToken))
        {
            for (var yearBe = 2560; yearBe <= 2569; yearBe++)
            {
                var yearCe = yearBe - 543;
                _context.AcademicYears.Add(new AcademicYear(
                    yearBe,
                    new DateOnly(yearCe, 8, 1),
                    new DateOnly(yearCe + 1, 5, 31),
                    isCurrent: yearBe == 2569));
            }
        }

        if (!await _context.Departments.AnyAsync(cancellationToken))
        {
            _context.Departments.AddRange(
                new Department("PP", "ภาควิชาเภสัชกรรมปฏิบัติ", "Department of Pharmacy Practice"),
                new Department("PI", "ภาควิชาวิทยาการเภสัชกรรมและเภสัชอุตสาหกรรม", "Department of Pharmaceutics and Industrial Pharmacy"),
                new Department("PH", "ภาควิชาเภสัชวิทยาและสรีรวิทยา", "Department of Pharmacology and Physiology"),
                new Department("PG", "ภาควิชาเภสัชเวทและเภสัชพฤกษศาสตร์", "Department of Pharmacognosy and Pharmaceutical Botany"),
                new Department("SA", "ภาควิชาเภสัชกรรมสังคมและบริหาร", "Department of Social and Administrative Pharmacy"));
        }

        if (!await _context.Majors.AnyAsync(cancellationToken))
        {
            _context.Majors.AddRange(
                new Major("PC", "สาขาวิชาการบริบาลทางเภสัชกรรม", "Pharmaceutical Care"),
                new Major("IP", "สาขาวิชาเภสัชกรรมอุตสาหการ", "Industrial Pharmacy"));
        }

        if (!await _context.AcademicPositions.AnyAsync(cancellationToken))
        {
            _context.AcademicPositions.AddRange(
                new AcademicPosition("PROF", "ศาสตราจารย์", "Professor", "ศ.", "Prof.", 1),
                new AcademicPosition("ASSOC", "รองศาสตราจารย์", "Associate Professor", "รศ.", "Assoc. Prof.", 2),
                new AcademicPosition("ASST", "ผู้ช่วยศาสตราจารย์", "Assistant Professor", "ผศ.", "Asst. Prof.", 3),
                new AcademicPosition("LECT", "อาจารย์", "Lecturer", "อ.", "Lect.", 4));
        }

        await _context.SaveChangesAsync(cancellationToken);

        if (!await _context.AdmissionRounds.AnyAsync(cancellationToken))
        {
            var portfolio = new AdmissionRound("PORTFOLIO", "รอบ Portfolio", "Portfolio Round", AdmissionRoundTier.Tier1Category, null, 1);
            var quota = new AdmissionRound("QUOTA", "รอบ Quota", "Quota Round", AdmissionRoundTier.Tier1Category, null, 2);
            var cotmes = new AdmissionRound("COTMES", "รอบ กสพท.", "COTMES / National Admission Round", AdmissionRoundTier.Tier1Category, null, 3);

            _context.AdmissionRounds.AddRange(portfolio, quota, cotmes);
            await _context.SaveChangesAsync(cancellationToken);

            _context.AdmissionRounds.AddRange(
                new AdmissionRound("PORT_OLYMPIAD", "Port โอลิมปิกวิชาการ", "Portfolio — Academic Olympiad", AdmissionRoundTier.Tier2SubRound, portfolio.Id, 1),
                new AdmissionRound("PORT_ENGLISH", "Port ภาษาอังกฤษ", "Portfolio — English Proficiency", AdmissionRoundTier.Tier2SubRound, portfolio.Id, 2),
                new AdmissionRound("QUOTA_SATIT", "Quota สาธิตจุฬาฯ", "Quota — Chulalongkorn Demonstration School", AdmissionRoundTier.Tier2SubRound, quota.Id, 1),
                new AdmissionRound("QUOTA_SPORT", "Quota กีฬาชาติ", "Quota — National Athlete", AdmissionRoundTier.Tier2SubRound, quota.Id, 2),
                new AdmissionRound("QUOTA_RURAL", "Quota จุฬาชนบท", "Quota — Chula Rural", AdmissionRoundTier.Tier2SubRound, quota.Id, 3),
                new AdmissionRound("QUOTA_INTERIOR", "Quota มหาดไทย", "Quota — Ministry of Interior", AdmissionRoundTier.Tier2SubRound, quota.Id, 4),
                new AdmissionRound("QUOTA_KVIS", "Quota กำเนิดวิทย์", "Quota — Kamnoetvidya (KVIS)", AdmissionRoundTier.Tier2SubRound, quota.Id, 5));

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task SeedSampleFacultyAndStudentsAsync(CancellationToken cancellationToken)
    {
        if (!await _context.FacultyMembers.AnyAsync(cancellationToken))
        {
            var deptPp = await _context.Departments.FirstAsync(x => x.Code == "PP", cancellationToken);
            var deptPi = await _context.Departments.FirstAsync(x => x.Code == "PI", cancellationToken);
            var posAssoc = await _context.AcademicPositions.FirstAsync(x => x.Code == "ASSOC", cancellationToken);
            var posProf = await _context.AcademicPositions.FirstAsync(x => x.Code == "PROF", cancellationToken);

            var somchai = new FacultyMember(
                "PHARM-1001",
                "สมชาย",
                "เภสัชศาสตร์",
                "Somchai",
                "Phesatchasat",
                posAssoc.Id,
                deptPp.Id,
                "somchai.p@pharm.chula.ac.th",
                "ดร.ภก.",
                "Dr. Pharm.",
                "รองคณบดีฝ่ายวิชาการ",
                "Associate Dean for Academic Affairs",
                "02-218-8401",
                "57191234501",
                "0000-0002-1825-0097");

            somchai.AddEducation(
                "ปร.ด. (เภสัชกรรมคลินิก)",
                "จุฬาลงกรณ์มหาวิทยาลัย",
                2016,
                "Ph.D. (Clinical Pharmacy)",
                "Chulalongkorn University",
                1);

            somchai.AddPublication(
                "Clinical Pharmacist Interventions in Antimicrobial Stewardship Across Thai University Hospitals",
                "International Journal of Clinical Pharmacy",
                2025,
                "10.1007/s11096-025-01842-x",
                "2-s2.0-85210045123",
                "Scopus",
                "2-s2.0-85210045123",
                DateTimeOffset.UtcNow);

            var napawan = new FacultyMember(
                "PHARM-1002",
                "นภาวรรณ",
                "วิทยาการยา",
                "Napawan",
                "Wittayakanya",
                posProf.Id,
                deptPi.Id,
                "napawan.w@pharm.chula.ac.th",
                "ดร.ภญ.",
                "Dr. Pharm.",
                "หัวหน้าภาควิชาวิทยาการเภสัชกรรมและเภสัชอุตสาหกรรม",
                "Head, Department of Pharmaceutics and Industrial Pharmacy",
                "02-218-8392",
                "57191234502",
                "0000-0003-2190-4412");

            napawan.AddEducation(
                "Ph.D. (Pharmaceutics)",
                "University of Wisconsin-Madison, USA",
                2012,
                "Ph.D. (Pharmaceutics)",
                "University of Wisconsin-Madison, USA",
                1);

            _context.FacultyMembers.AddRange(somchai, napawan);
            await _context.SaveChangesAsync(cancellationToken);
        }

        if (!await _context.Students.AnyAsync(cancellationToken))
        {
            var ay2564 = await _context.AcademicYears.FirstAsync(x => x.YearBe == 2564, cancellationToken);
            var ay2565 = await _context.AcademicYears.FirstAsync(x => x.YearBe == 2565, cancellationToken);
            var majorPc = await _context.Majors.FirstAsync(x => x.Code == "PC", cancellationToken);
            var majorIp = await _context.Majors.FirstAsync(x => x.Code == "IP", cancellationToken);
            var deptPp = await _context.Departments.FirstAsync(x => x.Code == "PP", cancellationToken);
            var deptPi = await _context.Departments.FirstAsync(x => x.Code == "PI", cancellationToken);
            var roundOlympiad = await _context.AdmissionRounds.FirstAsync(x => x.Code == "PORT_OLYMPIAD", cancellationToken);
            var roundSatit = await _context.AdmissionRounds.FirstAsync(x => x.Code == "QUOTA_SATIT", cancellationToken);
            var somchai = await _context.FacultyMembers.FirstAsync(x => x.PersonnelCode == "PHARM-1001", cancellationToken);
            var napawan = await _context.FacultyMembers.FirstAsync(x => x.PersonnelCode == "PHARM-1002", cancellationToken);

            var student1 = new Student(
                "6433012533",
                "กิตติภพ",
                "รัตนเภสัช",
                "Kittiphop",
                "Rattanaphesat",
                ay2564.Id,
                6,
                majorPc.Id,
                deptPp.Id,
                roundOlympiad.Id,
                somchai.Id,
                "โรงเรียนเตรียมอุดมศึกษา",
                "Triam Udom Suksa School",
                "CUPPEP",
                "CUPPEP-STU-6433012533",
                DateTimeOffset.UtcNow);

            student1.AddInternship(
                "โรงพยาบาลจุฬาลงกรณ์ สภากาชาดไทย",
                "ฝึกปฏิบัติงานบริบาลทางเภสัชกรรมผู้ป่วยใน",
                "พ.ค. – มิ.ย. 2569",
                "King Chulalongkorn Memorial Hospital",
                "Inpatient Clinical Pharmacy Clerkship",
                "ROT-101");

            var student2 = new Student(
                "6533048233",
                "ชนิกานต์",
                "อุตสาหกรรมยา",
                "Chanikan",
                "Utsahakamya",
                ay2565.Id,
                5,
                majorIp.Id,
                deptPi.Id,
                roundSatit.Id,
                napawan.Id,
                "โรงเรียนสาธิตจุฬาลงกรณ์มหาวิทยาลัย ฝ่ายมัธยม",
                "Chulalongkorn University Demonstration Secondary School",
                "CUPPEP",
                "CUPPEP-STU-6533048233",
                DateTimeOffset.UtcNow);

            _context.Students.AddRange(student1, student2);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
