using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Documents;

public partial class XDocumentTemplate
{
    public int Uid { get; set; }

    public int? DocumentUid { get; set; }

    public string? DocumentName { get; set; }

    public string? DocumentDate { get; set; }

    public int? StartNumber { get; set; }

    public int? EndNumber { get; set; }

    public string? SubjectTitle { get; set; }

    public string? EduYear { get; set; }

    public string? StdLevel { get; set; }

    public string? PhaseName { get; set; }

    public string? Content1 { get; set; }

    public string? Content2 { get; set; }

    public string? Cuser { get; set; }

    public DateTime? Cwhen { get; set; }
}
