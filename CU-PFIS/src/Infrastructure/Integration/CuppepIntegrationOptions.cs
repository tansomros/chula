namespace Chula.Infrastructure.Integration;

public sealed class CuppepIntegrationOptions
{
    public const string SectionName = "CuppepIntegration";

    /// <summary>
    /// Name of the read-only MS SQL Server connection string under ConnectionStrings (default: CuppepSqlServer).
    /// </summary>
    public string ConnectionStringName { get; set; } = "CuppepSqlServer";

    /// <summary>
    /// SQL command timeout in seconds for read-only pulls from chula_pps.
    /// </summary>
    public int CommandTimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// When true, queries the dedicated read-only integration view dbo.vw_pfis_students (recommended for least-privilege DBA setups).
    /// When false, queries dbo.Students LEFT JOIN dbo.Major directly in read-only mode.
    /// </summary>
    public bool UseIntegrationView { get; set; }
}
