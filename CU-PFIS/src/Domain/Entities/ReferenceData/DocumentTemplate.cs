using Ardalis.GuardClauses;
using Chula.Domain.Common;
using Chula.Domain.Enums.ReferenceData;

namespace Chula.Domain.Entities.ReferenceData;

public sealed class DocumentTemplate : SoftDeletableEntityBase, IHasActiveFlag
{
    private DocumentTemplate()
    {
    }

    public DocumentTemplate(
        string code,
        string nameTh,
        string nameEn,
        DocumentTemplateType templateType,
        string fileName,
        string contentType,
        long fileSize,
        string sha256Hash,
        string storagePath)
    {
        Code = Guard.Against.NullOrWhiteSpace(code, nameof(code)).Trim().ToUpperInvariant();
        TemplateType = Guard.Against.EnumOutOfRange(templateType, nameof(templateType));
        UpdateMetadata(nameTh, nameEn);
        UpdateFile(
            fileName,
            contentType,
            fileSize,
            sha256Hash,
            storagePath);
        Version = 1;
        IsActive = true;
    }

    public string Code { get; private set; } = string.Empty;

    public string NameTh { get; private set; } = string.Empty;

    public string NameEn { get; private set; } = string.Empty;

    public DocumentTemplateType TemplateType { get; private set; }

    public string FileName { get; private set; } = string.Empty;

    public string ContentType { get; private set; } = string.Empty;

    public long FileSize { get; private set; }

    public string Sha256Hash { get; private set; } = string.Empty;

    public string StoragePath { get; private set; } = string.Empty;

    public int Version { get; private set; }

    public bool IsActive { get; private set; } = true;

    public void UpdateMetadata(string nameTh, string nameEn)
    {
        NameTh = Guard.Against.NullOrWhiteSpace(nameTh, nameof(nameTh)).Trim();
        NameEn = Guard.Against.NullOrWhiteSpace(nameEn, nameof(nameEn)).Trim();
    }

    public void ReplaceTemplateFile(
        string fileName,
        string contentType,
        long fileSize,
        string sha256Hash,
        string storagePath)
    {
        UpdateFile(
            fileName,
            contentType,
            fileSize,
            sha256Hash,
            storagePath);
        Version++;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private void UpdateFile(
        string fileName,
        string contentType,
        long fileSize,
        string sha256Hash,
        string storagePath)
    {
        FileName = Guard.Against.NullOrWhiteSpace(fileName, nameof(fileName)).Trim();
        ContentType = Guard.Against.NullOrWhiteSpace(contentType, nameof(contentType)).Trim();
        FileSize = Guard.Against.NegativeOrZero(fileSize, nameof(fileSize));
        Sha256Hash = Guard.Against.NullOrWhiteSpace(sha256Hash, nameof(sha256Hash)).Trim().ToLowerInvariant();
        StoragePath = Guard.Against.NullOrWhiteSpace(storagePath, nameof(storagePath)).Trim();
    }
}
