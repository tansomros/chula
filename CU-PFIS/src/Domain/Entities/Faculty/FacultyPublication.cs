using Ardalis.GuardClauses;
using Chula.Domain.Common;

namespace Chula.Domain.Entities.Faculty;

public sealed class FacultyPublication : EntityBase
{
    private FacultyPublication()
    {
    }

    public FacultyPublication(
        string title,
        string? journalOrVenue,
        int publishedYear,
        string? doi = null,
        string? scopusId = null,
        string sourceSystem = "CU-PFIS",
        string? sourceRecordId = null,
        DateTimeOffset? importedAt = null)
    {
        Title = Guard.Against.NullOrWhiteSpace(title, nameof(title)).Trim();
        JournalOrVenue = string.IsNullOrWhiteSpace(journalOrVenue) ? null : journalOrVenue.Trim();
        PublishedYear = Guard.Against.OutOfRange(publishedYear, nameof(publishedYear), 1950, 2600);
        Doi = string.IsNullOrWhiteSpace(doi) ? null : doi.Trim();
        ScopusId = string.IsNullOrWhiteSpace(scopusId) ? null : scopusId.Trim();
        SourceSystem = Guard.Against.NullOrWhiteSpace(sourceSystem, nameof(sourceSystem)).Trim();
        SourceRecordId = string.IsNullOrWhiteSpace(sourceRecordId) ? null : sourceRecordId.Trim();
        ImportedAt = importedAt;
    }

    public int FacultyMemberId { get; private set; }

    public FacultyMember FacultyMember { get; private set; } = null!;

    public string Title { get; private set; } = string.Empty;

    public string? JournalOrVenue { get; private set; }

    public int PublishedYear { get; private set; }

    public string? Doi { get; private set; }

    public string? ScopusId { get; private set; }

    public string SourceSystem { get; private set; } = "CU-PFIS";

    public string? SourceRecordId { get; private set; }

    public DateTimeOffset? ImportedAt { get; private set; }
}
