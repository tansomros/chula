namespace Chula.Domain.Enums.Training;

public enum BudgetLedgerEntryType
{
    /// <summary>Deducts approved spend from the current fiscal year's quota (triggered after Dean approval per A05).</summary>
    Consume = 1,

    /// <summary>Audit entry on the borrowing fiscal year recording advance borrowed from next fiscal year (A06).</summary>
    BorrowAdvance = 2,

    /// <summary>Deducts quota on the next fiscal year (FiscalYear + 1) to settle an advance borrowed in the prior year (A06).</summary>
    CarryForwardDeduction = 3,

    /// <summary>Explicit administrative adjustment (never mutating past ledger entries).</summary>
    Adjustment = 4
}
