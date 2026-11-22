using Chula.Domain.Entities.ReferenceData;
using Chula.Infrastructure.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chula.Infrastructure.Data.Configurations.ReferenceData;

public sealed class DocumentTemplateConfiguration : IEntityTypeConfiguration<DocumentTemplate>
{
    public void Configure(EntityTypeBuilder<DocumentTemplate> builder)
    {
        builder.ConfigureEntityBase();
        builder.ConfigureSoftDelete();
        builder.ConfigureActiveFlag();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(80);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.NameTh)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.NameEn)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.TemplateType)
            .IsRequired();

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(260);

        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.Sha256Hash)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.StoragePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Version)
            .IsRequired()
            .HasDefaultValue(1);
    }
}
