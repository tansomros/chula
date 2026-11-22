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
