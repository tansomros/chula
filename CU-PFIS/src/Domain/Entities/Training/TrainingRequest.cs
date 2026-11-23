using Ardalis.GuardClauses;
using Chula.Domain.Common;
using Chula.Domain.Entities.Faculty;
using Chula.Domain.Enums.Training;

namespace Chula.Domain.Entities.Training;

/// <summary>
/// Faculty training request / attendance record across all 4 categories (A04, A05, A06, A15, A18).
/// </summary>
public sealed class TrainingRequest : SoftDeletableEntityBase
{
    private readonly List<TrainingApprovalHistory> _approvalHistory = new();
    private readonly List<TrainingBudgetLedgerEntry> _budgetLedgerEntries = new();

    private TrainingRequest()
    {
    }

    public TrainingRequest(
        int facultyMemberId,
        TrainingCategory category,
        TrainingWorkType workType,
        string titleTh,
        string organizerAndVenueTh,
        DateOnly startDate,
        DateOnly endDate,
        decimal requestedAmount = 0m,
        string? titleEn = null,
        string? sessionTopicTh = null,
        string? sessionTopicEn = null,
        string? organizerAndVenueEn = null,
        string? bankName = null,
        string? bankAccountNumber = null,
        string? bankAccountHolderName = null,
        int? fiscalYearBe = null,
        string sourceSystem = "CU-PFIS",
        string? sourceRecordId = null,
        DateTimeOffset? importedAt = null)
    {
        if (endDate < startDate)
        {
            throw new DomainException("Training end date cannot be earlier than start date.");
        }

        FacultyMemberId = Guard.Against.NegativeOrZero(facultyMemberId, nameof(facultyMemberId));
        Category = category;
        WorkType = workType;
        TitleTh = Guard.Against.NullOrWhiteSpace(titleTh, nameof(titleTh)).Trim();
        TitleEn = NormalizeOptional(titleEn);
        SessionTopicTh = NormalizeOptional(sessionTopicTh);
        SessionTopicEn = NormalizeOptional(sessionTopicEn);
        OrganizerAndVenueTh = Guard.Against.NullOrWhiteSpace(organizerAndVenueTh, nameof(organizerAndVenueTh)).Trim();
        OrganizerAndVenueEn = NormalizeOptional(organizerAndVenueEn);
        StartDate = startDate;
        EndDate = endDate;
        FiscalYearBe = fiscalYearBe ?? FiscalYearCalculator.FromDate(startDate);
        RequestedAmount = decimal.Round(Guard.Against.Negative(requestedAmount, nameof(requestedAmount)), 2, MidpointRounding.AwayFromZero);
        ApprovedAmount = 0m;
        BankName = NormalizeOptional(bankName);
        BankAccountNumber = NormalizeOptional(bankAccountNumber);
        BankAccountHolderName = NormalizeOptional(bankAccountHolderName);
        SourceSystem = Guard.Against.NullOrWhiteSpace(sourceSystem, nameof(sourceSystem)).Trim();
        SourceRecordId = NormalizeOptional(sourceRecordId);
        ImportedAt = importedAt;
        Status = TrainingRequestStatus.Draft;
    }

    public int FacultyMemberId { get; private set; }

    public FacultyMember FacultyMember { get; private set; } = null!;

    public int FiscalYearBe { get; private set; }

    public TrainingCategory Category { get; private set; }

    public TrainingWorkType WorkType { get; private set; }

    public string TitleTh { get; private set; } = string.Empty;

    public string? TitleEn { get; private set; }

    public string? SessionTopicTh { get; private set; }

    public string? SessionTopicEn { get; private set; }

    public string OrganizerAndVenueTh { get; private set; } = string.Empty;

    public string? OrganizerAndVenueEn { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public decimal RequestedAmount { get; private set; }

    public decimal ApprovedAmount { get; private set; }

    // PDPA Sensitive fields — strictly gated by training-requests.view-bank-account or record owner (A18)
    public string? BankName { get; private set; }

    public string? BankAccountNumber { get; private set; }

    public string? BankAccountHolderName { get; private set; }

    public TrainingRequestStatus Status { get; private set; }

    public string? RevisionNote { get; private set; }

    public DateTimeOffset? SubmittedAt { get; private set; }

    public DateTimeOffset? VerifiedAt { get; private set; }

    public DateTimeOffset? ApprovedAt { get; private set; }

    public string SourceSystem { get; private set; } = "CU-PFIS";

    public string? SourceRecordId { get; private set; }

    public DateTimeOffset? ImportedAt { get; private set; }

    public IReadOnlyCollection<TrainingApprovalHistory> ApprovalHistory => _approvalHistory.AsReadOnly();

    public IReadOnlyCollection<TrainingBudgetLedgerEntry> BudgetLedgerEntries => _budgetLedgerEntries.AsReadOnly();

    public bool RequiresApprovalWorkflow =>
        Category is TrainingCategory.AcademicFunded or TrainingCategory.NonAcademicFunded;

    public void UpdateDetails(
        TrainingWorkType workType,
        string titleTh,
        string organizerAndVenueTh,
        DateOnly startDate,
        DateOnly endDate,
        decimal requestedAmount,
        string? titleEn = null,
        string? sessionTopicTh = null,
        string? sessionTopicEn = null,
        string? organizerAndVenueEn = null,
        string? bankName = null,
        string? bankAccountNumber = null,
        string? bankAccountHolderName = null)
    {
        if (RequiresApprovalWorkflow &&
            Status is not (TrainingRequestStatus.Draft or TrainingRequestStatus.RevisionRequested))
        {
            throw new DomainException(
                $"Cannot edit training request in status '{Status}'. Only Draft or RevisionRequested requests can be edited.");
        }

        if (endDate < startDate)
        {
            throw new DomainException("Training end date cannot be earlier than start date.");
        }

        WorkType = workType;
        TitleTh = Guard.Against.NullOrWhiteSpace(titleTh, nameof(titleTh)).Trim();
        TitleEn = NormalizeOptional(titleEn);
        SessionTopicTh = NormalizeOptional(sessionTopicTh);
        SessionTopicEn = NormalizeOptional(sessionTopicEn);
        OrganizerAndVenueTh = Guard.Against.NullOrWhiteSpace(organizerAndVenueTh, nameof(organizerAndVenueTh)).Trim();
        OrganizerAndVenueEn = NormalizeOptional(organizerAndVenueEn);
        StartDate = startDate;
        EndDate = endDate;
        FiscalYearBe = FiscalYearCalculator.FromDate(startDate);
        RequestedAmount = decimal.Round(Guard.Against.Negative(requestedAmount, nameof(requestedAmount)), 2, MidpointRounding.AwayFromZero);
        BankName = NormalizeOptional(bankName);
        BankAccountNumber = NormalizeOptional(bankAccountNumber);
        BankAccountHolderName = NormalizeOptional(bankAccountHolderName);
    }

    public void Submit(string actorLogin, DateTimeOffset now, string? note = null)
    {
        if (!RequiresApprovalWorkflow)
        {
            RecordDirectlyAsApproved(actorLogin, RequestedAmount, now, note);
            return;
        }

        if (Status is not (TrainingRequestStatus.Draft or TrainingRequestStatus.RevisionRequested))
        {
            throw new DomainException($"Cannot submit training request from status '{Status}'.");
        }

        var from = Status;
        Status = TrainingRequestStatus.Submitted;
        SubmittedAt = now;
        RevisionNote = null;
        AppendHistory(from, Status, actorLogin, now, note);
    }

    /// <summary>
    /// Categories 3 (FacultyOrganized) &amp; 4 (ShortTermOrOverseas) or historical Excel migrations do not require Dean approval.
    /// </summary>
    public void RecordDirectlyAsApproved(
        string actorLogin,
        decimal approvedAmount,
        DateTimeOffset now,
        string? note = null)
    {
        var from = Status;
        ApprovedAmount = decimal.Round(Guard.Against.Negative(approvedAmount, nameof(approvedAmount)), 2, MidpointRounding.AwayFromZero);
        Status = TrainingRequestStatus.Approved;
        ApprovedAt = now;
        AppendHistory(from, Status, actorLogin, now, note ?? "Recorded directly (no Dean approval workflow required).");
    }

    public void RequestRevision(string actorLogin, string reason, DateTimeOffset now)
    {
        if (Status is not (TrainingRequestStatus.Submitted or TrainingRequestStatus.VerifiedInProgress))
        {
            throw new DomainException($"Cannot request revision from status '{Status}'.");
        }

        var normalizedReason = Guard.Against.NullOrWhiteSpace(reason, nameof(reason)).Trim();
        var from = Status;
        Status = TrainingRequestStatus.RevisionRequested;
        RevisionNote = normalizedReason;
        AppendHistory(from, Status, actorLogin, now, normalizedReason);
    }

    public void MarkVerified(string actorLogin, DateTimeOffset now, string? note = null)
    {
        if (Status != TrainingRequestStatus.Submitted)
        {
            throw new DomainException($"Only Submitted requests can be marked Verified. Current status: '{Status}'.");
        }

        var from = Status;
        Status = TrainingRequestStatus.VerifiedInProgress;
        VerifiedAt = now;
        RevisionNote = null;
        AppendHistory(from, Status, actorLogin, now, note ?? "Verified accurate by staff; ready for Dean signature.");
    }

    public void ApproveByDean(
        string actorLogin,
        decimal approvedAmount,
        DateTimeOffset now,
        string? note = null)
    {
        if (Status != TrainingRequestStatus.VerifiedInProgress)
        {
            throw new DomainException(
                $"Only VerifiedInProgress requests can be approved by Dean. Current status: '{Status}'.");
        }

        var from = Status;
        ApprovedAmount = Category == TrainingCategory.AcademicFunded
            ? decimal.Round(Guard.Against.Negative(approvedAmount, nameof(approvedAmount)), 2, MidpointRounding.AwayFromZero)
            : 0m;
        Status = TrainingRequestStatus.Approved;
        ApprovedAt = now;
        AppendHistory(from, Status, actorLogin, now, note ?? "Approved by Dean.");
    }

    public void Cancel(string actorLogin, DateTimeOffset now, string? reason = null)
    {
        if (Status is TrainingRequestStatus.Approved or TrainingRequestStatus.Cancelled)
        {
            throw new DomainException($"Cannot cancel a training request that is already '{Status}'.");
        }

        var from = Status;
        Status = TrainingRequestStatus.Cancelled;
        AppendHistory(from, Status, actorLogin, now, reason ?? "Cancelled before approval.");
    }

    private void AppendHistory(
        TrainingRequestStatus fromStatus,
        TrainingRequestStatus toStatus,
        string actorLogin,
        DateTimeOffset actionAt,
        string? note)
    {
        _approvalHistory.Add(new TrainingApprovalHistory(fromStatus, toStatus, actorLogin, actionAt, note));
    }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
