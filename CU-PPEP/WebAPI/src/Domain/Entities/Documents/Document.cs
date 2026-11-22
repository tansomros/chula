using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Documents;

public partial class Document
{
    public int Uid { get; set; }

    public string? DocumentCode { get; set; }

    public string? DocumentName { get; set; }

    public string? SubjectTitle { get; set; }

    public int? DocumentType { get; set; }

    public string? StatusFlag { get; set; }

    public int? Sort { get; set; }

    public string? MailMergeName { get; set; }

    public string? FileDataName { get; set; }

    public string? DocumentPath { get; set; }
}
