using Ardalis.GuardClauses;
using Chula.Domain.Common;
using Chula.Domain.Entities.Faculty;
using Chula.Domain.Entities.ReferenceData;

namespace Chula.Domain.Entities.Students;

public sealed class Student : SoftDeletableEntityBase, IHasActiveFlag
{
    private readonly List<StudentInternship> _internships = new();

    private Student()
    {
    }

    public Student(
        string studentCode,
        string firstNameTh,
        string lastNameTh,
        string firstNameEn,
        string lastNameEn,
        int cohortAcademicYearId,
        int studyYear,
        int majorId,
        int? departmentId = null,
        int? admissionRoundId = null,
        int? academicAdvisorId = null,
        string? highSchoolTh = null,
        string? highSchoolEn = null,
        string sourceSystem = "CUPPEP",
        string? sourceRecordId = null,
        DateTimeOffset? importedAt = null)
    {
        StudentCode = Guard.Against.NullOrWhiteSpace(studentCode, nameof(studentCode)).Trim();
        FirstNameTh = Guard.Against.NullOrWhiteSpace(firstNameTh, nameof(firstNameTh)).Trim();
        LastNameTh = Guard.Against.NullOrWhiteSpace(lastNameTh, nameof(lastNameTh)).Trim();
        FirstNameEn = Guard.Against.NullOrWhiteSpace(firstNameEn, nameof(firstNameEn)).Trim();
        LastNameEn = Guard.Against.NullOrWhiteSpace(lastNameEn, nameof(lastNameEn)).Trim();
        CohortAcademicYearId = Guard.Against.NegativeOrZero(cohortAcademicYearId, nameof(cohortAcademicYearId));
        StudyYear = Guard.Against.OutOfRange(studyYear, nameof(studyYear), 1, 8);
        MajorId = Guard.Against.NegativeOrZero(majorId, nameof(majorId));
        DepartmentId = departmentId;
        AdmissionRoundId = admissionRoundId;
        AcademicAdvisorId = academicAdvisorId;
        HighSchoolTh = NormalizeOptional(highSchoolTh);
        HighSchoolEn = NormalizeOptional(highSchoolEn);
        SourceSystem = Guard.Against.NullOrWhiteSpace(sourceSystem, nameof(sourceSystem)).Trim();
        SourceRecordId = NormalizeOptional(sourceRecordId) ?? StudentCode;
        ImportedAt = importedAt;
        IsStaffVerifiedEdit = false;
        IsActive = true;
    }

    public string StudentCode { get; private set; } = string.Empty;

    public string FirstNameTh { get; private set; } = string.Empty;

    public string LastNameTh { get; private set; } = string.Empty;

    public string FirstNameEn { get; private set; } = string.Empty;

    public string LastNameEn { get; private set; } = string.Empty;

    public int CohortAcademicYearId { get; private set; }

    public AcademicYear CohortAcademicYear { get; private set; } = null!;

    public int StudyYear { get; private set; }

    public int MajorId { get; private set; }

    public Major Major { get; private set; } = null!;

    public int? DepartmentId { get; private set; }

    public Department? Department { get; private set; }

    public int? AdmissionRoundId { get; private set; }

    public AdmissionRound? AdmissionRound { get; private set; }

    public int? AcademicAdvisorId { get; private set; }

    public FacultyMember? AcademicAdvisor { get; private set; }

    public string? HighSchoolTh { get; private set; }

    public string? HighSchoolEn { get; private set; }

    public string SourceSystem { get; private set; } = "CUPPEP";

    public string? SourceRecordId { get; private set; }

    public DateTimeOffset? ImportedAt { get; private set; }

    /// <summary>
    /// Marks that staff reviewed/edited this record in CU-PFIS; external re-imports must not silently overwrite it (P01).
    /// </summary>
    public bool IsStaffVerifiedEdit { get; private set; }

    public bool IsActive { get; private set; } = true;

    public IReadOnlyCollection<StudentInternship> Internships => _internships.AsReadOnly();

    public void UpdateProfileByStaff(
        string firstNameTh,
        string lastNameTh,
        string firstNameEn,
        string lastNameEn,
        int studyYear,
        int majorId,
        int? departmentId,
        int? admissionRoundId,
        int? academicAdvisorId,
        string? highSchoolTh,
        string? highSchoolEn)
    {
        FirstNameTh = Guard.Against.NullOrWhiteSpace(firstNameTh, nameof(firstNameTh)).Trim();
        LastNameTh = Guard.Against.NullOrWhiteSpace(lastNameTh, nameof(lastNameTh)).Trim();
        FirstNameEn = Guard.Against.NullOrWhiteSpace(firstNameEn, nameof(firstNameEn)).Trim();
        LastNameEn = Guard.Against.NullOrWhiteSpace(lastNameEn, nameof(lastNameEn)).Trim();
        StudyYear = Guard.Against.OutOfRange(studyYear, nameof(studyYear), 1, 8);
        MajorId = Guard.Against.NegativeOrZero(majorId, nameof(majorId));
        DepartmentId = departmentId;
        AdmissionRoundId = admissionRoundId;
        AcademicAdvisorId = academicAdvisorId;
        HighSchoolTh = NormalizeOptional(highSchoolTh);
        HighSchoolEn = NormalizeOptional(highSchoolEn);
        IsStaffVerifiedEdit = true;
    }

    /// <summary>
    /// Detects which sync-managed fields differ between this CU-PFIS student and an incoming external snapshot.
    /// </summary>
    public IReadOnlyList<string> GetExternalSyncDifferences(
        string firstNameTh,
        string lastNameTh,
        string firstNameEn,
        string lastNameEn,
        int studyYear,
        int majorId,
        string? highSchoolTh,
        string? highSchoolEn)
    {
        var diffs = new List<string>();
        if (!string.Equals(FirstNameTh, firstNameTh.Trim(), StringComparison.Ordinal))
            diffs.Add(nameof(FirstNameTh));
        if (!string.Equals(LastNameTh, lastNameTh.Trim(), StringComparison.Ordinal))
            diffs.Add(nameof(LastNameTh));
        if (!string.Equals(FirstNameEn, firstNameEn.Trim(), StringComparison.Ordinal))
            diffs.Add(nameof(FirstNameEn));
        if (!string.Equals(LastNameEn, lastNameEn.Trim(), StringComparison.Ordinal))
            diffs.Add(nameof(LastNameEn));
        if (StudyYear != studyYear)
            diffs.Add(nameof(StudyYear));
        if (MajorId != majorId)
            diffs.Add(nameof(MajorId));
        if (!string.Equals(HighSchoolTh, NormalizeOptional(highSchoolTh), StringComparison.Ordinal))
            diffs.Add(nameof(HighSchoolTh));
        if (!string.Equals(HighSchoolEn, NormalizeOptional(highSchoolEn), StringComparison.Ordinal))
            diffs.Add(nameof(HighSchoolEn));

        return diffs;
    }

    /// <summary>
    /// Applies an external sync update (e.g. from CUPPEP) ONLY if staff has not verified/edited this record (P01).
    /// </summary>
    public void ApplyExternalSyncUpdate(
        string firstNameTh,
        string lastNameTh,
        string firstNameEn,
        string lastNameEn,
        int studyYear,
        int majorId,
        string? highSchoolTh,
        string? highSchoolEn,
        string sourceRecordId,
        DateTimeOffset importedAt)
    {
        if (IsStaffVerifiedEdit)
        {
            throw new DomainException(
                $"Cannot silently overwrite staff-verified student '{StudentCode}' from external sync. Route to DataConflictQueue.");
        }

        FirstNameTh = Guard.Against.NullOrWhiteSpace(firstNameTh, nameof(firstNameTh)).Trim();
        LastNameTh = Guard.Against.NullOrWhiteSpace(lastNameTh, nameof(lastNameTh)).Trim();
        FirstNameEn = Guard.Against.NullOrWhiteSpace(firstNameEn, nameof(firstNameEn)).Trim();
        LastNameEn = Guard.Against.NullOrWhiteSpace(lastNameEn, nameof(lastNameEn)).Trim();
        StudyYear = Guard.Against.OutOfRange(studyYear, nameof(studyYear), 1, 8);
        MajorId = Guard.Against.NegativeOrZero(majorId, nameof(majorId));
        HighSchoolTh = NormalizeOptional(highSchoolTh);
        HighSchoolEn = NormalizeOptional(highSchoolEn);
        SourceRecordId = NormalizeOptional(sourceRecordId) ?? SourceRecordId;
        ImportedAt = importedAt;
    }

    /// <summary>
    /// Applies incoming external values ONLY after staff explicitly accepts the conflict resolution in DataConflictQueue.
    /// </summary>
    public void ApplyConflictResolutionAcceptExternal(
        string firstNameTh,
        string lastNameTh,
        string firstNameEn,
        string lastNameEn,
        int studyYear,
        int majorId,
        string? highSchoolTh,
        string? highSchoolEn,
        string sourceRecordId,
        DateTimeOffset importedAt)
    {
        FirstNameTh = Guard.Against.NullOrWhiteSpace(firstNameTh, nameof(firstNameTh)).Trim();
        LastNameTh = Guard.Against.NullOrWhiteSpace(lastNameTh, nameof(lastNameTh)).Trim();
        FirstNameEn = Guard.Against.NullOrWhiteSpace(firstNameEn, nameof(firstNameEn)).Trim();
        LastNameEn = Guard.Against.NullOrWhiteSpace(lastNameEn, nameof(lastNameEn)).Trim();
        StudyYear = Guard.Against.OutOfRange(studyYear, nameof(studyYear), 1, 8);
        MajorId = Guard.Against.NegativeOrZero(majorId, nameof(majorId));
        HighSchoolTh = NormalizeOptional(highSchoolTh);
        HighSchoolEn = NormalizeOptional(highSchoolEn);
        SourceRecordId = NormalizeOptional(sourceRecordId) ?? SourceRecordId;
        ImportedAt = importedAt;
        IsStaffVerifiedEdit = true;
    }

    public StudentInternship AddInternship(
        string siteTh,
        string rotationTypeTh,
        string periodLabel,
        string? siteEn = null,
        string? rotationTypeEn = null,
        string? sourceRecordId = null)
    {
        var internship = new StudentInternship(
            siteTh,
            rotationTypeTh,
            periodLabel,
            siteEn,
            rotationTypeEn,
            sourceRecordId);
        _internships.Add(internship);
        return internship;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
