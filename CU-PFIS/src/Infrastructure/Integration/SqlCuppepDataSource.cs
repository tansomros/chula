using Chula.Application.Common.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Chula.Infrastructure.Integration;

/// <summary>
/// Direct read-only MS SQL Server adapter implementing <see cref="ICuppepDataSource"/> (Decision D11 — Option 1B).
/// Runs inside the single CU-PFIS API on the same server, reading legacy chula_pps students, advisors,
/// and professional practice rotations via a read-only connection string without adding a second EF Core DbContext.
/// </summary>
public sealed class SqlCuppepDataSource : ICuppepDataSource
{
    private const int SqlInClauseChunkSize = 500;

    private readonly IConfiguration _configuration;
    private readonly CuppepIntegrationOptions _options;
    private readonly ILogger<SqlCuppepDataSource> _logger;

    public SqlCuppepDataSource(
        IConfiguration configuration,
        IOptions<CuppepIntegrationOptions> options,
        ILogger<SqlCuppepDataSource> logger)
    {
        _configuration = configuration;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<CuppepHealthStatus> CheckHealthAsync(CancellationToken cancellationToken)
    {
        var connectionString = ResolveConnectionString();
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return new CuppepHealthStatus(
                false,
                "(not configured)",
                $"ConnectionStrings:{_options.ConnectionStringName} is not configured.");
        }

        var safeTarget = FormatSafeTarget(connectionString);
        var commandTimeout = Math.Clamp(_options.CommandTimeoutSeconds, 5, 300);

        try
        {
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);

            var countSql = _options.UseIntegrationView
                ? "SELECT COUNT(1) FROM dbo.vw_pfis_students;"
                : "SELECT COUNT(1) FROM dbo.Students WHERE Student_Code IS NOT NULL AND LTRIM(RTRIM(Student_Code)) <> '';";

            var count = await connection.ExecuteScalarAsync<int>(
                new CommandDefinition(countSql, commandTimeout: commandTimeout, cancellationToken: cancellationToken));

            var sourceMode = _options.UseIntegrationView ? "view dbo.vw_pfis_students" : "dbo.Students";
            return new CuppepHealthStatus(
                true,
                safeTarget,
                $"Connected to CU-PPEP SQL Server ({sourceMode}, {count:N0} students available).");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "CU-PPEP SQL Server health check failed for {Target}", safeTarget);
            return new CuppepHealthStatus(false, safeTarget, ex.Message);
        }
    }

    public async Task<IReadOnlyList<CuppepStudentSnapshot>> PullStudentsAsync(
        DateTimeOffset? updatedSince,
        int? maxRecords = null,
        CancellationToken cancellationToken = default)
    {
        var connectionString = ResolveConnectionString();
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"ConnectionStrings:{_options.ConnectionStringName} is not configured.");
        }

        var commandTimeout = Math.Clamp(_options.CommandTimeoutSeconds, 5, 300);
        var topClause = maxRecords.HasValue && maxRecords.Value > 0 ? "TOP (@MaxRecords) " : string.Empty;

        var sql = _options.UseIntegrationView
            ? BuildViewQuery(topClause, updatedSince.HasValue)
            : BuildTablesQuery(topClause, updatedSince.HasValue);

        var parameters = new DynamicParameters();
        if (maxRecords.HasValue && maxRecords.Value > 0)
        {
            parameters.Add("MaxRecords", maxRecords.Value);
        }

        if (updatedSince.HasValue)
        {
            parameters.Add("UpdatedSince", updatedSince.Value.UtcDateTime);
        }

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        var rows = (await connection.QueryAsync<CuppepStudentRow>(
            new CommandDefinition(
                sql,
                parameters,
                commandTimeout: commandTimeout,
                cancellationToken: cancellationToken)))
            .Where(r => !string.IsNullOrWhiteSpace(r.StudentCode))
            .ToList();

        if (rows.Count == 0)
        {
            return Array.Empty<CuppepStudentSnapshot>();
        }

        var studentCodes = rows
            .Select(r => r.StudentCode.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var internshipsByCode = await LoadInternshipsByStudentCodesAsync(
            connection,
            studentCodes,
            commandTimeout,
            cancellationToken);

        var results = new List<CuppepStudentSnapshot>(rows.Count);
        foreach (var row in rows)
        {
            var code = row.StudentCode.Trim();
            internshipsByCode.TryGetValue(code, out var internships);
            results.Add(MapToSnapshot(row, internships));
        }

        return results;
    }

    private async Task<Dictionary<string, List<CuppepInternshipSnapshot>>> LoadInternshipsByStudentCodesAsync(
        SqlConnection connection,
        IReadOnlyList<string> studentCodes,
        int commandTimeout,
        CancellationToken cancellationToken)
    {
        var result = new Dictionary<string, List<CuppepInternshipSnapshot>>(StringComparer.OrdinalIgnoreCase);

        const string sql = """
            SELECT
                sr.itemID                        AS ItemId,
                LTRIM(RTRIM(sr.Student_Code))    AS StudentCode,
                sr.REGYEAR                       AS RegYear,
                sr.LocationName                  AS LocationName,
                sr.LocationGroupName             AS LocationGroupName,
                sr.ProvinceName                  AS ProvinceName,
                sr.PhaseNo                       AS PhaseNo,
                sr.Name                          AS PhaseName,
                sr.SubjectCode                   AS SubjectCode,
                sil.NameTH                       AS SubjectNameTh,
                sil.NameEN                       AS SubjectNameEn
            FROM dbo.View_StudentRegister AS sr
            LEFT JOIN dbo.View_StudentRegister_InLocation AS sil
                ON sil.itemID = sr.itemID
            WHERE LTRIM(RTRIM(sr.Student_Code)) IN @StudentCodes
            ORDER BY sr.REGYEAR DESC, sr.PhaseNo ASC, sr.itemID ASC;
            """;

        try
        {
            for (var i = 0; i < studentCodes.Count; i += SqlInClauseChunkSize)
            {
                var chunk = studentCodes.Skip(i).Take(SqlInClauseChunkSize).ToArray();
                var rows = await connection.QueryAsync<CuppepInternshipRow>(
                    new CommandDefinition(
                        sql,
                        new { StudentCodes = chunk },
                        commandTimeout: commandTimeout,
                        cancellationToken: cancellationToken));

                foreach (var row in rows)
                {
                    if (string.IsNullOrWhiteSpace(row.StudentCode) || string.IsNullOrWhiteSpace(row.LocationName))
                    {
                        continue;
                    }

                    var code = row.StudentCode.Trim();
                    if (!result.TryGetValue(code, out var list))
                    {
                        list = new List<CuppepInternshipSnapshot>();
                        result[code] = list;
                    }

                    var siteTh = FormatSiteName(row.LocationName, row.ProvinceName);
                    var rotationTypeTh = FirstNonEmpty(row.SubjectNameTh, row.LocationGroupName, row.SubjectCode, "ฝึกปฏิบัติงานวิชาชีพ");
                    var rotationTypeEn = string.IsNullOrWhiteSpace(row.SubjectNameEn) ? null : Truncate(row.SubjectNameEn.Trim(), 250);
                    var periodLabel = FormatPeriodLabel(row.PhaseNo, row.PhaseName, row.RegYear);

                    list.Add(new CuppepInternshipSnapshot(
                        $"CUPPEP-ROT-{row.ItemId}",
                        Truncate(siteTh, 300),
                        SiteEn: null,
                        Truncate(rotationTypeTh, 250),
                        rotationTypeEn,
                        Truncate(periodLabel, 100)));
                }
            }
        }
        catch (SqlException ex)
        {
            // Keep student sync resilient if a custom SQL environment lacks View_StudentRegister
            _logger.LogWarning(ex, "Could not query dbo.View_StudentRegister for internship rotations; continuing with base student sync.");
        }

        return result;
    }

    private string? ResolveConnectionString()
    {
        var key = string.IsNullOrWhiteSpace(_options.ConnectionStringName)
            ? "CuppepSqlServer"
            : _options.ConnectionStringName.Trim();

        return _configuration.GetConnectionString(key);
    }

    private static string BuildTablesQuery(string topClause, bool filterUpdatedSince)
    {
        var sinceClause = filterUpdatedSince
            ? " AND ISNULL(st.LastUpdate, st.CreateDate) >= @UpdatedSince"
            : string.Empty;

        return $"""
            SELECT {topClause}
                CAST(st.Student_ID AS NVARCHAR(50))                    AS SourceRecordId,
                LTRIM(RTRIM(st.Student_Code))                          AS StudentCode,
                LTRIM(RTRIM(ISNULL(st.FirstName, N'')))                AS FirstNameTh,
                LTRIM(RTRIM(ISNULL(st.LastName, N'')))                 AS LastNameTh,
                LTRIM(RTRIM(ISNULL(st.FirstNameEN, N'')))              AS FirstNameEn,
                LTRIM(RTRIM(ISNULL(st.LastNameEN, N'')))               AS LastNameEn,
                st.MajorID                                             AS MajorId,
                m.MajorName                                            AS MajorNameTh,
                st.HighSchool                                          AS HighSchoolTh,
                st.HighProvince                                        AS HighSchoolProvinceTh,
                ISNULL(st.LevelClass, 1)                               AS StudyYear,
                ISNULL(st.LastUpdate, st.CreateDate)                   AS UpdatedAt,
                LTRIM(RTRIM(ISNULL(adv.PrefixName, '')))               AS AdvisorPrefixTh,
                LTRIM(RTRIM(ISNULL(adv.FirstName, '')))                AS AdvisorFirstNameTh,
                LTRIM(RTRIM(ISNULL(adv.LastName, '')))                 AS AdvisorLastNameTh,
                LTRIM(RTRIM(ISNULL(adv.Email, '')))                    AS AdvisorEmail
            FROM dbo.Students AS st
            LEFT JOIN dbo.Major AS m
                ON m.MajorID = st.MajorID
            LEFT JOIN dbo.Persons AS adv
                ON adv.PersonID = st.AdvisorID
            WHERE st.Student_Code IS NOT NULL
              AND LTRIM(RTRIM(st.Student_Code)) <> ''{sinceClause}
            ORDER BY ISNULL(st.LastUpdate, st.CreateDate) DESC, st.Student_Code DESC;
            """;
    }

    private static string BuildViewQuery(string topClause, bool filterUpdatedSince)
    {
        var sinceClause = filterUpdatedSince
            ? " WHERE UpdatedAt >= @UpdatedSince"
            : string.Empty;

        return $"""
            SELECT {topClause}
                SourceRecordId,
                StudentCode,
                FirstNameTh,
                LastNameTh,
                FirstNameEn,
                LastNameEn,
                MajorId,
                MajorNameTh,
                HighSchoolTh,
                HighSchoolProvinceTh,
                StudyYear,
                UpdatedAt,
                AdvisorNameTh
            FROM dbo.vw_pfis_students{sinceClause}
            ORDER BY UpdatedAt DESC, StudentCode DESC;
            """;
    }

    private static string FormatSafeTarget(string connectionString)
    {
        try
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            var server = string.IsNullOrWhiteSpace(builder.DataSource) ? "sql-server" : builder.DataSource;
            var database = string.IsNullOrWhiteSpace(builder.InitialCatalog) ? "chula_pps" : builder.InitialCatalog;
            return $"mssql://{server}/{database}";
        }
        catch
        {
            return "mssql://chula_pps";
        }
    }

    private static CuppepStudentSnapshot MapToSnapshot(
        CuppepStudentRow row,
        IReadOnlyList<CuppepInternshipSnapshot>? internships)
    {
        var studentCode = row.StudentCode.Trim();
        var firstNameTh = string.IsNullOrWhiteSpace(row.FirstNameTh) ? studentCode : row.FirstNameTh.Trim();
        var lastNameTh = string.IsNullOrWhiteSpace(row.LastNameTh) ? "-" : row.LastNameTh.Trim();
        var firstNameEn = string.IsNullOrWhiteSpace(row.FirstNameEn) ? firstNameTh : row.FirstNameEn.Trim();
        var lastNameEn = string.IsNullOrWhiteSpace(row.LastNameEn) ? lastNameTh : row.LastNameEn.Trim();

        var cohortYearBe = DeriveCohortYearBe(studentCode);
        var studyYear = Math.Clamp(row.StudyYear <= 0 ? 1 : row.StudyYear, 1, 8);
        var majorCode = MapMajorCode(row.MajorId, row.MajorNameTh);

        var highSchoolTh = string.IsNullOrWhiteSpace(row.HighSchoolTh)
            ? null
            : string.IsNullOrWhiteSpace(row.HighSchoolProvinceTh)
                ? row.HighSchoolTh.Trim()
                : $"{row.HighSchoolTh.Trim()} ({row.HighSchoolProvinceTh.Trim()})";

        var updatedAt = row.UpdatedAt.HasValue
            ? new DateTimeOffset(DateTime.SpecifyKind(row.UpdatedAt.Value, DateTimeKind.Utc))
            : DateTimeOffset.UtcNow;

        var advisorFullNameTh = FormatAdvisorName(
            row.AdvisorNameTh,
            row.AdvisorPrefixTh,
            row.AdvisorFirstNameTh,
            row.AdvisorLastNameTh);

        return new CuppepStudentSnapshot(
            row.SourceRecordId,
            studentCode,
            firstNameTh,
            lastNameTh,
            firstNameEn,
            lastNameEn,
            cohortYearBe,
            studyYear,
            majorCode,
            highSchoolTh,
            HighSchoolEn: null,
            updatedAt,
            AcademicAdvisorNameTh: advisorFullNameTh is null ? null : Truncate(advisorFullNameTh, 250),
            AcademicAdvisorFirstNameTh: string.IsNullOrWhiteSpace(row.AdvisorFirstNameTh) ? null : row.AdvisorFirstNameTh.Trim(),
            AcademicAdvisorLastNameTh: string.IsNullOrWhiteSpace(row.AdvisorLastNameTh) ? null : row.AdvisorLastNameTh.Trim(),
            AcademicAdvisorEmail: string.IsNullOrWhiteSpace(row.AdvisorEmail) ? null : row.AdvisorEmail.Trim(),
            Internships: internships ?? Array.Empty<CuppepInternshipSnapshot>());
    }

    private static string? FormatAdvisorName(string? fullName, string? prefix, string? firstName, string? lastName)
    {
        if (!string.IsNullOrWhiteSpace(fullName))
        {
            return fullName.Trim();
        }

        var parts = new[] { prefix?.Trim(), firstName?.Trim(), lastName?.Trim() }
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .ToArray();

        return parts.Length == 0 ? null : string.Join(" ", parts);
    }

    private static string FormatSiteName(string locationName, string? provinceName)
    {
        var site = locationName.Trim();
        var prov = provinceName?.Trim();
        if (string.IsNullOrWhiteSpace(prov) || site.Contains(prov, StringComparison.OrdinalIgnoreCase))
        {
            return site;
        }

        return $"{site} ({prov})";
    }

    private static string FormatPeriodLabel(int? phaseNo, string? phaseName, int? regYear)
    {
        var cleanPhase = phaseName?.Trim();
        if (!string.IsNullOrWhiteSpace(cleanPhase))
        {
            return phaseNo.HasValue && phaseNo.Value > 0
                ? $"ผลัด {phaseNo.Value} · {cleanPhase}"
                : cleanPhase;
        }

        if (phaseNo.HasValue && phaseNo.Value > 0 && regYear.HasValue)
        {
            return $"ปีการศึกษา {regYear.Value} ผลัด {phaseNo.Value}";
        }

        return regYear.HasValue ? $"ปีการศึกษา {regYear.Value}" : "-";
    }

    private static string FirstNonEmpty(params string?[] candidates)
    {
        foreach (var value in candidates)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }
        }

        return "-";
    }

    private static string Truncate(string value, int maxLength)
    {
        return value.Length <= maxLength ? value : value[..maxLength];
    }

    private static int DeriveCohortYearBe(string studentCode)
    {
        // Chula student codes start with 2-digit Buddhist Era admission year (e.g. 6533048233 -> 2565)
        if (studentCode.Length >= 2 &&
            int.TryParse(studentCode[..2], out var twoDigitYear) &&
            twoDigitYear is >= 40 and <= 99)
        {
            return 2500 + twoDigitYear;
        }

        return DateTime.UtcNow.Year + 543;
    }

    private static string MapMajorCode(int? majorId, string? majorNameTh)
    {
        if (!string.IsNullOrWhiteSpace(majorNameTh))
        {
            if (majorNameTh.Contains("อุตสาหการ", StringComparison.OrdinalIgnoreCase) ||
                majorNameTh.Contains("Industrial", StringComparison.OrdinalIgnoreCase) ||
                majorNameTh.Contains("IP", StringComparison.OrdinalIgnoreCase))
            {
                return "IP";
            }

            if (majorNameTh.Contains("บริบาล", StringComparison.OrdinalIgnoreCase) ||
                majorNameTh.Contains("Care", StringComparison.OrdinalIgnoreCase) ||
                majorNameTh.Contains("PC", StringComparison.OrdinalIgnoreCase))
            {
                return "PC";
            }
        }

        return majorId == 2 ? "IP" : "PC";
    }

    private sealed class CuppepStudentRow
    {
        public string SourceRecordId { get; init; } = string.Empty;
        public string StudentCode { get; init; } = string.Empty;
        public string? FirstNameTh { get; init; }
        public string? LastNameTh { get; init; }
        public string? FirstNameEn { get; init; }
        public string? LastNameEn { get; init; }
        public int? MajorId { get; init; }
        public string? MajorNameTh { get; init; }
        public string? HighSchoolTh { get; init; }
        public string? HighSchoolProvinceTh { get; init; }
        public int StudyYear { get; init; }
        public DateTime? UpdatedAt { get; init; }
        public string? AdvisorNameTh { get; init; }
        public string? AdvisorPrefixTh { get; init; }
        public string? AdvisorFirstNameTh { get; init; }
        public string? AdvisorLastNameTh { get; init; }
        public string? AdvisorEmail { get; init; }
    }

    private sealed class CuppepInternshipRow
    {
        public long ItemId { get; init; }
        public string StudentCode { get; init; } = string.Empty;
        public int? RegYear { get; init; }
        public string? LocationName { get; init; }
        public string? LocationGroupName { get; init; }
        public string? ProvinceName { get; init; }
        public int? PhaseNo { get; init; }
        public string? PhaseName { get; init; }
        public string? SubjectCode { get; init; }
        public string? SubjectNameTh { get; init; }
        public string? SubjectNameEn { get; init; }
    }
}
