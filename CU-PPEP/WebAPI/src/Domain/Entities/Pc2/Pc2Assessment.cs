using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Pc2;

public partial class Pc2Assessment
{
    public int AssessmentId { get; set; }

    public int EduYear { get; set; }

    public int SubjectUid { get; set; }

    public string StudentCode { get; set; } = null!;

    public string? StatusFlag { get; set; }

    public DateTime? Mwhen { get; set; }

    public int? Muser { get; set; }
}
