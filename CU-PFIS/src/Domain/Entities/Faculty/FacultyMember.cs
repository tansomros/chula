using Ardalis.GuardClauses;
using Chula.Domain.Common;
using Chula.Domain.Entities.ReferenceData;

namespace Chula.Domain.Entities.Faculty;

public sealed class FacultyMember : SoftDeletableEntityBase, IHasActiveFlag
{
    private readonly List<FacultyEducation> _educations = new();
    private readonly List<FacultyPublication> _publications = new();

    private FacultyMember()
    {
    }

    public FacultyMember(
        string personnelCode,
        string firstNameTh,
        string lastNameTh,
        string firstNameEn,
        string lastNameEn,
        int academicPositionId,
        int departmentId,
        string email,
        string? professionalTitleTh = null,
        string? professionalTitleEn = null,
        string? administrativePositionsTh = null,
        string? administrativePositionsEn = null,
        string? phoneNumber = null,
        string? scopusAuthorId = null,
        string? orcidId = null)
    {
        PersonnelCode = Guard.Against.NullOrWhiteSpace(personnelCode, nameof(personnelCode)).Trim();
        FirstNameTh = Guard.Against.NullOrWhiteSpace(firstNameTh, nameof(firstNameTh)).Trim();
        LastNameTh = Guard.Against.NullOrWhiteSpace(lastNameTh, nameof(lastNameTh)).Trim();
        FirstNameEn = Guard.Against.NullOrWhiteSpace(firstNameEn, nameof(firstNameEn)).Trim();
        LastNameEn = Guard.Against.NullOrWhiteSpace(lastNameEn, nameof(lastNameEn)).Trim();
        AcademicPositionId = Guard.Against.NegativeOrZero(academicPositionId, nameof(academicPositionId));
        DepartmentId = Guard.Against.NegativeOrZero(departmentId, nameof(departmentId));
        Email = ValidateEmail(email);
        ProfessionalTitleTh = NormalizeOptional(professionalTitleTh);
        ProfessionalTitleEn = NormalizeOptional(professionalTitleEn);
        AdministrativePositionsTh = NormalizeOptional(administrativePositionsTh);
        AdministrativePositionsEn = NormalizeOptional(administrativePositionsEn);
        PhoneNumber = NormalizeOptional(phoneNumber);
        ScopusAuthorId = NormalizeOptional(scopusAuthorId);
        OrcidId = NormalizeOptional(orcidId);
        IsActive = true;
    }

    public string PersonnelCode { get; private set; } = string.Empty;

    public string? ProfessionalTitleTh { get; private set; }

    public string? ProfessionalTitleEn { get; private set; }

    public string FirstNameTh { get; private set; } = string.Empty;

    public string LastNameTh { get; private set; } = string.Empty;

    public string FirstNameEn { get; private set; } = string.Empty;

    public string LastNameEn { get; private set; } = string.Empty;

    public int AcademicPositionId { get; private set; }

    public AcademicPosition AcademicPosition { get; private set; } = null!;

    public string? AdministrativePositionsTh { get; private set; }

    public string? AdministrativePositionsEn { get; private set; }

    public int DepartmentId { get; private set; }

    public Department Department { get; private set; } = null!;

    public string Email { get; private set; } = string.Empty;

    public string? PhoneNumber { get; private set; }

    public string? ScopusAuthorId { get; private set; }

    public string? OrcidId { get; private set; }

    public DateTimeOffset? LastCvConfirmedAt { get; private set; }

    public bool IsActive { get; private set; } = true;

    public IReadOnlyCollection<FacultyEducation> Educations => _educations.AsReadOnly();

    public IReadOnlyCollection<FacultyPublication> Publications => _publications.AsReadOnly();

    public void UpdateProfile(
        string firstNameTh,
        string lastNameTh,
        string firstNameEn,
        string lastNameEn,
        int academicPositionId,
        int departmentId,
        string email,
        string? professionalTitleTh = null,
        string? professionalTitleEn = null,
        string? administrativePositionsTh = null,
        string? administrativePositionsEn = null,
        string? phoneNumber = null,
        string? scopusAuthorId = null,
        string? orcidId = null)
    {
        FirstNameTh = Guard.Against.NullOrWhiteSpace(firstNameTh, nameof(firstNameTh)).Trim();
        LastNameTh = Guard.Against.NullOrWhiteSpace(lastNameTh, nameof(lastNameTh)).Trim();
        FirstNameEn = Guard.Against.NullOrWhiteSpace(firstNameEn, nameof(firstNameEn)).Trim();
        LastNameEn = Guard.Against.NullOrWhiteSpace(lastNameEn, nameof(lastNameEn)).Trim();
        AcademicPositionId = Guard.Against.NegativeOrZero(academicPositionId, nameof(academicPositionId));
        DepartmentId = Guard.Against.NegativeOrZero(departmentId, nameof(departmentId));
        Email = ValidateEmail(email);
        ProfessionalTitleTh = NormalizeOptional(professionalTitleTh);
        ProfessionalTitleEn = NormalizeOptional(professionalTitleEn);
        AdministrativePositionsTh = NormalizeOptional(administrativePositionsTh);
        AdministrativePositionsEn = NormalizeOptional(administrativePositionsEn);
        PhoneNumber = NormalizeOptional(phoneNumber);
        ScopusAuthorId = NormalizeOptional(scopusAuthorId);
        OrcidId = NormalizeOptional(orcidId);
    }

    public FacultyEducation AddEducation(
        string degreeTh,
        string institutionTh,
        int graduationYear,
        string? degreeEn = null,
        string? institutionEn = null,
        int sortOrder = 0)
    {
        var education = new FacultyEducation(
            degreeTh,
            institutionTh,
            graduationYear,
            degreeEn,
            institutionEn,
            sortOrder);
        _educations.Add(education);
        return education;
    }

    public void ClearEducations()
    {
        _educations.Clear();
    }

    public FacultyPublication AddPublication(
        string title,
        string? journalOrVenue,
        int publishedYear,
        string? doi = null,
        string? scopusId = null,
        string sourceSystem = "CU-PFIS",
        string? sourceRecordId = null,
        DateTimeOffset? importedAt = null)
    {
        var publication = new FacultyPublication(
            title,
            journalOrVenue,
            publishedYear,
            doi,
            scopusId,
            sourceSystem,
            sourceRecordId,
            importedAt);
        _publications.Add(publication);
        return publication;
    }

    public void ClearPublications()
    {
        _publications.Clear();
    }

    /// <summary>
    /// Idempotent Scopus/external publication import (A16): skips duplicates matched by ScopusId (EID) or DOI.
    /// </summary>
    public bool TryImportPublication(
        string title,
        string? journalOrVenue,
        int publishedYear,
        string? doi,
        string? scopusId,
        string sourceSystem,
        DateTimeOffset importedAt)
    {
        var normalizedScopusId = NormalizeOptional(scopusId);
        var normalizedDoi = NormalizeOptional(doi);

        var isDuplicate = _publications.Any(existing =>
            (normalizedScopusId is not null &&
             string.Equals(existing.ScopusId, normalizedScopusId, StringComparison.OrdinalIgnoreCase)) ||
            (normalizedDoi is not null &&
             string.Equals(existing.Doi, normalizedDoi, StringComparison.OrdinalIgnoreCase)));

        if (isDuplicate)
        {
            return false;
        }

        var sourceRecordId = normalizedScopusId ?? normalizedDoi ?? $"{publishedYear}:{title.Trim()}";
        AddPublication(
            title,
            journalOrVenue,
            publishedYear,
            normalizedDoi,
            normalizedScopusId,
            sourceSystem,
            sourceRecordId,
            importedAt);
        return true;
    }

    public void ConfirmCvUpdated(DateTimeOffset confirmedAt)
    {
        LastCvConfirmedAt = confirmedAt;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    private static string ValidateEmail(string email)
    {
        var trimmed = Guard.Against.NullOrWhiteSpace(email, nameof(email)).Trim();
        if (!trimmed.Contains('@') || trimmed.StartsWith('@') || trimmed.EndsWith('@'))
        {
            throw new DomainException("Invalid email address format.");
        }

        return trimmed;
    }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
