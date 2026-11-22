using Ardalis.GuardClauses;
using Chula.Domain.Common;

namespace Chula.Domain.Entities.Security;

/// <summary>
/// Append-only record of a user's First-Login PDPA Consent acceptance (A18).
/// Never soft-deleted or mutated after creation.
/// </summary>
public sealed class PdpaConsent : EntityBase
{
    private PdpaConsent()
    {
    }

    public PdpaConsent(
        string userId,
        string loginName,
        string policyVersion,
        DateTimeOffset acceptedAt,
        string? ipAddress = null,
        string? userAgent = null)
    {
        UserId = Guard.Against.NullOrWhiteSpace(userId, nameof(userId)).Trim();
        LoginName = Guard.Against.NullOrWhiteSpace(loginName, nameof(loginName)).Trim();
        PolicyVersion = Guard.Against.NullOrWhiteSpace(policyVersion, nameof(policyVersion)).Trim();
        AcceptedAt = acceptedAt;
        IpAddress = string.IsNullOrWhiteSpace(ipAddress) ? null : ipAddress.Trim();
        UserAgent = string.IsNullOrWhiteSpace(userAgent) ? null : userAgent.Trim();
    }

    public string UserId { get; private set; } = string.Empty;

    public string LoginName { get; private set; } = string.Empty;

    public string PolicyVersion { get; private set; } = string.Empty;

    public DateTimeOffset AcceptedAt { get; private set; }

    public string? IpAddress { get; private set; }

    public string? UserAgent { get; private set; }
}
