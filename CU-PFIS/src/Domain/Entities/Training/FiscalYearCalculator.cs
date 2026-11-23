namespace Chula.Domain.Entities.Training;

/// <summary>
/// Thai Fiscal Year helper (1 Oct – 30 Sep, per Decision A06).
/// Example: 2026-10-01 through 2027-09-30 belongs to Fiscal Year 2570 BE (2027 CE).
/// </summary>
public static class FiscalYearCalculator
{
    public static int FromDate(DateOnly date)
    {
        var fiscalYearCe = date.Month >= 10 ? date.Year + 1 : date.Year;
        return fiscalYearCe + 543;
    }

    public static (DateOnly StartDate, DateOnly EndDate) GetDateRange(int fiscalYearBe)
    {
        var fiscalYearCe = fiscalYearBe - 543;
        return (new DateOnly(fiscalYearCe - 1, 10, 1), new DateOnly(fiscalYearCe, 9, 30));
    }
}
