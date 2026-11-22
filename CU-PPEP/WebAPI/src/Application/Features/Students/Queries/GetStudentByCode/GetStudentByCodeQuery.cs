using CUPPEP.Application.Common.Exceptions;
using CUPPEP.Application.Common.Interfaces;
using CUPPEP.Application.Features.Students.Queries.GetStudentsIncremental;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CUPPEP.Application.Features.Students.Queries.GetStudentByCode;

public sealed record GetStudentByCodeQuery(string StudentCode) : IRequest<CuppepStudentDetailResponse>;

public sealed record CuppepPracticeHistoryDto(
    int PYear,
    int LocationId,
    string StudentCode,
    string SubjectCode,
    string? LocationName,
    string? SubjectName);

public sealed record CuppepStudentDetailResponse(
    CuppepStudentDto Student,
    IReadOnlyList<CuppepPracticeHistoryDto> PracticeHistories);

public sealed class GetStudentByCodeQueryHandler
    : IRequestHandler<GetStudentByCodeQuery, CuppepStudentDetailResponse>
{
    private readonly ICuPpepDbContext _context;

    public GetStudentByCodeQueryHandler(ICuPpepDbContext context)
    {
        _context = context;
    }

    public async Task<CuppepStudentDetailResponse> Handle(
        GetStudentByCodeQuery request,
        CancellationToken cancellationToken)
    {
        var normalizedCode = request.StudentCode.Trim();

        var row = await (
            from st in _context.Students.AsNoTracking()
            where st.StudentCode == normalizedCode
            join m in _context.Majors.AsNoTracking() on st.MajorId equals m.MajorId into mj
            from major in mj.DefaultIfEmpty()
            join mn in _context.Minors.AsNoTracking() on st.MinorId equals mn.MinorId into mnj
            from minor in mnj.DefaultIfEmpty()
            join adv in _context.Persons.AsNoTracking() on st.AdvisorId equals adv.PersonId into advj
            from advisor in advj.DefaultIfEmpty()
            join sp in _context.Persons.AsNoTracking() on st.SeniorProjectId equals sp.PersonId into spj
            from seniorProject in spj.DefaultIfEmpty()
            join ss in _context.StudentStatuses.AsNoTracking() on st.StudentStatus equals ss.StatusCode into ssj
            from status in ssj.DefaultIfEmpty()
            select new
            {
                st.StudentId,
                st.StudentCode,
                st.Prefix,
                st.FirstName,
                st.LastName,
                st.FirstNameEn,
                st.LastNameEn,
                st.Email,
                st.MajorId,
                MajorName = major != null ? major.MajorName : null,
                st.MinorId,
                MinorName = minor != null ? minor.MinorName : null,
                st.AdvisorId,
                AdvisorPrefix = advisor != null ? advisor.PrefixName : null,
                AdvisorFirstName = advisor != null ? advisor.FirstName : null,
                AdvisorLastName = advisor != null ? advisor.LastName : null,
                SeniorProjectPrefix = seniorProject != null ? seniorProject.PrefixName : null,
                SeniorProjectFirstName = seniorProject != null ? seniorProject.FirstName : null,
                SeniorProjectLastName = seniorProject != null ? seniorProject.LastName : null,
                st.HighSchool,
                st.HighProvince,
                st.HighYear,
                st.LevelClass,
                st.Gpax,
                st.StudentStatus,
                StatusName = status != null ? status.StatusName : null,
                UpdatedAt = st.LastUpdate ?? st.CreateDate
            })
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(Students), normalizedCode);

        var student = new CuppepStudentDto(
            row.StudentId.ToString(),
            row.StudentCode.Trim(),
            row.Prefix,
            (row.FirstName ?? string.Empty).Trim(),
            (row.LastName ?? string.Empty).Trim(),
            (row.FirstNameEn ?? string.Empty).Trim(),
            (row.LastNameEn ?? string.Empty).Trim(),
            row.Email,
            row.MajorId,
            row.MajorName,
            row.MinorId,
            row.MinorName,
            row.AdvisorId,
            FormatFullName(row.AdvisorPrefix, row.AdvisorFirstName, row.AdvisorLastName),
            FormatFullName(row.SeniorProjectPrefix, row.SeniorProjectFirstName, row.SeniorProjectLastName),
            row.HighSchool,
            row.HighProvince,
            row.HighYear,
            row.LevelClass ?? 1,
            row.Gpax,
            row.StudentStatus,
            row.StatusName,
            row.UpdatedAt);

        var histories = await _context.GetPracticeHistoryByStudentCodeAsync(normalizedCode, cancellationToken);

        return new CuppepStudentDetailResponse(student, histories);
    }

    private static string? FormatFullName(string? prefix, string? firstName, string? lastName)
    {
        var fullName = $"{prefix ?? string.Empty}{firstName ?? string.Empty} {lastName ?? string.Empty}".Trim();
        return string.IsNullOrEmpty(fullName) ? null : fullName;
    }
}
