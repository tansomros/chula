using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class StudentAssessmentComment
{
    public int Uid { get; set; }

    public int? EduYear { get; set; }

    public string? StudentCode { get; set; }

    public int? LocationId { get; set; }

    public int? TimePhaseId { get; set; }

    public int? PhaseId { get; set; }

    public int? UserId { get; set; }

    public int? ProsId { get; set; }

    public string? Comment { get; set; }

    public DateTime? Mwhen { get; set; }

    public string? Muser { get; set; }
}
