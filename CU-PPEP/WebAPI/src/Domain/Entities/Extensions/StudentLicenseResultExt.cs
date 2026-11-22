using Ardalis.GuardClauses;
using CUPPEP.Domain.Common;

namespace CUPPEP.Domain.Entities.Extensions;

/// <summary>
/// Professional license self-confirmed exam checklist in CU-PPEP (A08: CC1, CC2, IP1, IP2, PC1, PC2 — no file attachment).
/// </summary>
public sealed class StudentLicenseResultExt : EntityBase
{
    private StudentLicenseResultExt()
    {
    }

    public StudentLicenseResultExt(
        string studentCode,
        bool passedCc1,
        bool passedCc2,
        bool passedIp1,
        bool passedIp2,
        bool passedPc1,
        bool passedPc2)
    {
        StudentCode = Guard.Against.NullOrWhiteSpace(studentCode, nameof(studentCode)).Trim();
        Update(passedCc1, passedCc2, passedIp1, passedIp2, passedPc1, passedPc2);
    }

    public string StudentCode { get; private set; } = string.Empty;

    public bool PassedCc1 { get; private set; }

    public bool PassedCc2 { get; private set; }

    public bool PassedIp1 { get; private set; }

    public bool PassedIp2 { get; private set; }

    public bool PassedPc1 { get; private set; }

    public bool PassedPc2 { get; private set; }

    public void Update(
        bool passedCc1,
        bool passedCc2,
        bool passedIp1,
        bool passedIp2,
        bool passedPc1,
        bool passedPc2)
    {
        PassedCc1 = passedCc1;
        PassedCc2 = passedCc2;
        PassedIp1 = passedIp1;
        PassedIp2 = passedIp2;
        PassedPc1 = passedPc1;
        PassedPc2 = passedPc2;
    }
}
