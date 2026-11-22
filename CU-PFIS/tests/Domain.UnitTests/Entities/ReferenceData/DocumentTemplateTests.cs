using Chula.Domain.Entities.ReferenceData;
using Chula.Domain.Enums.ReferenceData;
using NUnit.Framework;
using Shouldly;

namespace Chula.Domain.UnitTests.Entities.ReferenceData;

[TestFixture]
public class DocumentTemplateTests
{
    [Test]
    public void Constructor_AndReplaceTemplateFile_ShouldIncrementVersion()
    {
        var template = new DocumentTemplate(
            "tpl-faculty-cv",
            "แม่แบบ CV อาจารย์",
            "Faculty CV Template",
            DocumentTemplateType.FacultyCv,
            "faculty-cv-v1.docx",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            10240,
            "ABCDEF1234567890",
            "templates/faculty-cv-v1.docx");

        template.Code.ShouldBe("TPL-FACULTY-CV");
        template.Version.ShouldBe(1);
        template.Sha256Hash.ShouldBe("abcdef1234567890");

        template.ReplaceTemplateFile(
            "faculty-cv-v2.docx",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            20480,
            "9999991234567890",
            "templates/faculty-cv-v2.docx");

        template.Version.ShouldBe(2);
        template.FileName.ShouldBe("faculty-cv-v2.docx");
        template.FileSize.ShouldBe(20480);
    }

    [Test]
    public void Constructor_WhenFileSizeIsZeroOrNegative_ShouldThrowArgumentException()
    {
        Should.Throw<ArgumentException>(() =>
            new DocumentTemplate(
                "TPL-ABSTRACT",
                "แม่แบบเล่ม Abstract",
                "Abstract Book Template",
                DocumentTemplateType.SeniorProjectAbstractBook,
                "abstract.pdf",
                "application/pdf",
                0,
                "hash",
                "templates/abstract.pdf"));
    }
}
