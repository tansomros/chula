namespace Chula.Infrastructure.Integration;

public sealed class CuppepIntegrationOptions
{
    public const string SectionName = "CuppepIntegration";

    public string BaseUrl { get; set; } = "https://localhost:7002";

    public string? ApiKey { get; set; }

    public int PageSize { get; set; } = 200;

    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Allows connecting to localhost HTTPS dev certificates when talking to CU-PPEP/WebAPI in local development.
    /// Must be false in production.
    /// </summary>
    public bool AllowUntrustedDevCertificate { get; set; }
}
