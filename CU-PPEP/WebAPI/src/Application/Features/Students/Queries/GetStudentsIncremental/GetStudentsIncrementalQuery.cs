using CUPPEP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CUPPEP.Application.Features.Students.Queries.GetStudentsIncremental;

public sealed record GetStudentsIncrementalQuery(
    DateTime? UpdatedSince = null,
    int Page = 1,
    int PageSize = 200) : IRequest<IncrementalStudentsResponse>;

public sealed record CuppepStudentDto(
    string SourceRecordId,
    string StudentCode,
    string? PrefixTh,
    string FirstNameTh,
    string LastNameTh,
    string? FirstNameEn,
    string? LastNameEn,
    string? Email,
    int? MajorId,
    string? MajorNameTh,
    int? MinorId,
    string? MinorNameTh,
    int? AdvisorId,
    string? AdvisorNameTh,
    string? SeniorProjectAdvisorNameTh,
    string? HighSchoolTh,
    string? HighSchoolProvinceTh,
    int? HighSchoolGradYear,
    int StudyYear,
    double? Gpax,
    string? StudentStatusCode,
    string? StudentStatusNameTh,
    DateTime? UpdatedAt);

public sealed record IncrementalStudentsResponse(
    int Page,
    int PageSize,
    int Count,
    DateTimeOffset PulledAtUtc,
    IReadOnlyList<CuppepStudentDto> Items);

public sealed class GetStudentsIncrementalQueryHandler
    : IRequestHandler<GetStudentsIncrementalQuery, IncrementalStudentsResponse>
{
    private readonly ICuPpepDbContext _context;

    public GetStudentsIncrementalQueryHandler(ICuPpepDbContext context)
    {
        _context = context;
    }

    public async Task<IncrementalStudentsResponse> Handle(
        GetStudentsIncrementalQuery request,
        CancellationToken cancellationToken)
    {
        var studentsQuery = _context.Students
            .AsNoTracking()
            .Where(x => x.StudentCode != null && x.StudentCode.Trim() != string.Empty);

        if (request.UpdatedSince.HasValue)
        {
            var since = request.UpdatedSince.Value;
            studentsQuery = studentsQuery.Where(x => (x.LastUpdate ?? x.CreateDate) >= since);
        }

        var pagedStudents = studentsQuery
            .OrderBy(x => x.StudentCode)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);

        var rows = await (
            from st in pagedStudents
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
            orderby st.StudentCode
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
            .ToListAsync(cancellationToken);

        var items = rows
            .Select(row => new CuppepStudentDto(
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
                row.UpdatedAt))
            .ToList();

        return new IncrementalStudentsResponse(
            request.Page,
            request.PageSize,
            items.Count,
            DateTimeOffset.UtcNow,
            items);
    }

    private static string? FormatFullName(string? prefix, string? firstName, string? lastName)
    {
        var fullName = $"{prefix ?? string.Empty}{firstName ?? string.Empty} {lastName ?? string.Empty}".Trim();
        return string.IsNullOrEmpty(fullName) ? null : fullName;
    }
}
