using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class StudentAssessmentCommentText
{
    public int Uid { get; set; }

    public int? EduYear { get; set; }

    public string? StudentCode { get; set; }

    public int? LocationId { get; set; }

    public string? SubjectCode { get; set; }

    public int? TimePhaseId { get; set; }

    public int? PhaseId { get; set; }

    public int? UserId { get; set; }

    public string? WorkDetail { get; set; }

    public string? Comment1 { get; set; }

    public string? Comment2 { get; set; }

    public string? StandingGrade { get; set; }

    public string? IsSure { get; set; }

    public int? Sure { get; set; }

    public string? Reason { get; set; }

    public string? Comments { get; set; }

    public DateTime? Mwhen { get; set; }

    public string? Muser { get; set; }
}
