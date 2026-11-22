using Chula.Domain.Common;
using Chula.Domain.Entities.ReferenceData;
using Chula.Domain.Enums.ReferenceData;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.ReferenceData;

[TestFixture]
public class AdmissionRoundTests
{
    [Test]
    public void Constructor_ForTier1CategoryWithoutParent_ShouldSucceed()
    {
        var quotaRound = new AdmissionRound(
            "quota",
            "รอบ Quota",
            "Quota Round",
            AdmissionRoundTier.Tier1Category);

        quotaRound.Code.ShouldBe("QUOTA");
        quotaRound.Tier.ShouldBe(AdmissionRoundTier.Tier1Category);
        quotaRound.ParentRoundId.ShouldBeNull();
    }

    [Test]
    public void Constructor_ForTier1CategoryWithParent_ShouldThrowDomainException()
    {
        Should.Throw<DomainException>(() =>
            new AdmissionRound(
                "QUOTA",
                "รอบ Quota",
                "Quota Round",
                AdmissionRoundTier.Tier1Category,
                parentRoundId: 1));
    }

    [Test]
    public void Constructor_ForTier2SubRoundWithValidParent_ShouldSucceed()
    {
        var subRound = new AdmissionRound(
            "quota-satit",
            "Quota สาธิตจุฬาฯ",
            "Chula Demonstration School Quota",
            AdmissionRoundTier.Tier2SubRound,
            parentRoundId: 2,
            sortOrder: 1);

        subRound.Code.ShouldBe("QUOTA-SATIT");
        subRound.Tier.ShouldBe(AdmissionRoundTier.Tier2SubRound);
        subRound.ParentRoundId.ShouldBe(2);
    }

    [Test]
    public void Constructor_ForTier2SubRoundWithoutParent_ShouldThrowDomainException()
    {
        Should.Throw<DomainException>(() =>
            new AdmissionRound(
                "PORT-OLYMPIC",
                "Port โอลิมปิกวิชาการ",
                "Academic Olympiad Portfolio",
                AdmissionRoundTier.Tier2SubRound,
                parentRoundId: null));
    }
}
