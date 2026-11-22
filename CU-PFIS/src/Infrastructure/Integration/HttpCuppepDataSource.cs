using System.Net.Http.Json;
using System.Text.Json;
using Chula.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Chula.Infrastructure.Integration;

/// <summary>
/// HTTP adapter implementing <see cref="ICuppepDataSource"/> against the standalone .NET 10 CU-PPEP WebAPI (Decision D11).
/// Performs paginated, incremental one-way pulls via GET /api/students?updatedSince=...&page=...&pageSize=...
/// </summary>
public sealed class HttpCuppepDataSource : ICuppepDataSource
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly HttpClient _httpClient;
    private readonly CuppepIntegrationOptions _options;
    private readonly ILogger<HttpCuppepDataSource> _logger;

    public HttpCuppepDataSource(
        HttpClient httpClient,
        IOptions<CuppepIntegrationOptions> options,
        ILogger<HttpCuppepDataSource> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<CuppepHealthStatus> CheckHealthAsync(CancellationToken cancellationToken)
    {
        var baseUrl = _options.BaseUrl.TrimEnd('/');
        try
        {
            using var response = await _httpClient.GetAsync("/api/health", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return new CuppepHealthStatus(
                    false,
                    baseUrl,
                    $"CU-PPEP API responded with HTTP {(int)response.StatusCode}.");
            }

            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            return new CuppepHealthStatus(true, baseUrl, string.IsNullOrWhiteSpace(body) ? "Healthy" : body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "CU-PPEP Integration API health check failed for {BaseUrl}", baseUrl);
            return new CuppepHealthStatus(false, baseUrl, ex.Message);
        }
    }

    public async Task<IReadOnlyList<CuppepStudentSnapshot>> PullStudentsAsync(
        DateTimeOffset? updatedSince,
        int? maxRecords = null,
        CancellationToken cancellationToken = default)
    {
        var configuredPageSize = Math.Clamp(_options.PageSize, 1, 500);
        var effectivePageSize = maxRecords.HasValue
            ? Math.Min(configuredPageSize, Math.Max(1, maxRecords.Value))
            : configuredPageSize;

        var results = new List<CuppepStudentSnapshot>();
        var page = 1;

        while (true)
        {
            var queryParts = new List<string>
            {
                $"page={page}",
                $"pageSize={effectivePageSize}"
            };

            if (updatedSince.HasValue)
            {
                queryParts.Add($"updatedSince={Uri.EscapeDataString(updatedSince.Value.UtcDateTime.ToString("O"))}");
            }

            var requestUri = $"/api/students?{string.Join("&", queryParts)}";
            using var response = await _httpClient.GetAsync(requestUri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var detail = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException(
                    $"CU-PPEP API GET {requestUri} failed with HTTP {(int)response.StatusCode}: {detail}");
            }

            var payload = await response.Content.ReadFromJsonAsync<CuppepIncrementalStudentsResponse>(
                JsonOptions,
                cancellationToken);

            if (payload is null || payload.Items.Count == 0)
            {
                break;
            }

            foreach (var item in payload.Items)
            {
                if (string.IsNullOrWhiteSpace(item.StudentCode))
                {
                    continue;
                }

                results.Add(MapToSnapshot(item));

                if (maxRecords.HasValue && results.Count >= maxRecords.Value)
                {
                    return results;
                }
            }

            if (payload.Items.Count < effectivePageSize)
            {
                break;
            }

            page++;
        }

        return results;
    }

    private static CuppepStudentSnapshot MapToSnapshot(CuppepApiStudentDto dto)
    {
        var studentCode = dto.StudentCode.Trim();
        var firstNameTh = string.IsNullOrWhiteSpace(dto.FirstNameTh) ? studentCode : dto.FirstNameTh.Trim();
        var lastNameTh = string.IsNullOrWhiteSpace(dto.LastNameTh) ? "-" : dto.LastNameTh.Trim();
        var firstNameEn = string.IsNullOrWhiteSpace(dto.FirstNameEn) ? firstNameTh : dto.FirstNameEn.Trim();
        var lastNameEn = string.IsNullOrWhiteSpace(dto.LastNameEn) ? lastNameTh : dto.LastNameEn.Trim();

        var cohortYearBe = DeriveCohortYearBe(studentCode);
        var studyYear = Math.Clamp(dto.StudyYear <= 0 ? 1 : dto.StudyYear, 1, 8);
        var majorCode = MapMajorCode(dto.MajorId, dto.MajorNameTh);

        var highSchoolTh = string.IsNullOrWhiteSpace(dto.HighSchoolTh)
            ? null
            : string.IsNullOrWhiteSpace(dto.HighSchoolProvinceTh)
                ? dto.HighSchoolTh.Trim()
                : $"{dto.HighSchoolTh.Trim()} ({dto.HighSchoolProvinceTh.Trim()})";

        var updatedAt = dto.UpdatedAt.HasValue
            ? new DateTimeOffset(DateTime.SpecifyKind(dto.UpdatedAt.Value, DateTimeKind.Utc))
            : DateTimeOffset.UtcNow;

        return new CuppepStudentSnapshot(
            dto.SourceRecordId,
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
            updatedAt);
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

    private sealed record CuppepIncrementalStudentsResponse(
        int Page,
        int PageSize,
        int Count,
        DateTimeOffset PulledAtUtc,
        IReadOnlyList<CuppepApiStudentDto> Items);

    private sealed record CuppepApiStudentDto(
        string SourceRecordId,
        string StudentCode,
        string? PrefixTh,
        string? FirstNameTh,
        string? LastNameTh,
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
}
