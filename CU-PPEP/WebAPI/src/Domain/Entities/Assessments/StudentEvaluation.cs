using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class StudentEvaluation
{
    public long Uid { get; set; }

    public int EduYear { get; set; }

    public string StudentCode { get; set; } = null!;

    public string SubjectCode { get; set; } = null!;

    public int? TimePhaseId { get; set; }

    public string AssessorRoleId { get; set; } = null!;

    public int AssessorLocationId { get; set; }

    public int AssessorUid { get; set; }

    public string? AssessorName { get; set; }

    public double? FullScore { get; set; }

    public double? Score { get; set; }

    public string? EstimateGrade { get; set; }

    public string? Comments { get; set; }

    public string? ConfirmStatus { get; set; }

    public DateTime? ConfirmDate { get; set; }

    public string? Muser { get; set; }

    public DateTime? Mwhen { get; set; }
}
