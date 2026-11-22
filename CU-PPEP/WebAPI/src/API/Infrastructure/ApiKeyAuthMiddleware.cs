using System.Security.Cryptography;
using System.Text;

namespace CUPPEP.API.Infrastructure;

/// <summary>
/// Service-to-service API Key gate (X-Api-Key) protecting CU-PPEP API endpoints.
/// Always enforced in Staging/Production; optional in Development via IntegrationAuth:RequireApiKeyInDevelopment
/// so developers can test endpoints directly in Swagger UI.
/// </summary>
public sealed class ApiKeyAuthMiddleware
{
    public const string HeaderName = "X-Api-Key";
    public const string ConfigSectionKey = "IntegrationAuth:ApiKey";
    public const string RequireInDevConfigKey = "IntegrationAuth:RequireApiKeyInDevelopment";

    private readonly RequestDelegate _next;
    private readonly string _expectedApiKey;
    private readonly bool _bypassInDevelopment;

    public ApiKeyAuthMiddleware(
        RequestDelegate next,
        IConfiguration configuration,
        IWebHostEnvironment? environment = null)
    {
        _next = next;
        _expectedApiKey = configuration[ConfigSectionKey] ?? string.Empty;
        var requireInDev = configuration.GetValue(RequireInDevConfigKey, true);
        _bypassInDevelopment = environment is not null && environment.IsDevelopment() && !requireInDev;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (_bypassInDevelopment ||
            context.Request.Path.StartsWithSegments("/api/health", StringComparison.OrdinalIgnoreCase) ||
            context.Request.Path.StartsWithSegments("/openapi", StringComparison.OrdinalIgnoreCase) ||
            context.Request.Path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        if (string.IsNullOrWhiteSpace(_expectedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("API key is not configured.");
            return;
        }

        if (!context.Request.Headers.TryGetValue(HeaderName, out var providedValues))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing X-Api-Key header.");
            return;
        }

        var provided = providedValues.ToString();
        var expectedBytes = Encoding.UTF8.GetBytes(_expectedApiKey);
        var providedBytes = Encoding.UTF8.GetBytes(provided);

        if (expectedBytes.Length != providedBytes.Length ||
            !CryptographicOperations.FixedTimeEquals(expectedBytes, providedBytes))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid X-Api-Key.");
            return;
        }

        await _next(context);
    }
}
