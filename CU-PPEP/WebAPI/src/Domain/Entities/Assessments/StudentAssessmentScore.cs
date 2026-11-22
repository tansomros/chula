using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class StudentAssessmentScore
{
    public long Uid { get; set; }

    public int EduYear { get; set; }

    public int EvaluationGroupUid { get; set; }

    public int AssessorUid { get; set; }

    public string StudentCode { get; set; } = null!;

    public string SubjectCode { get; set; } = null!;

    public double? BaseScore { get; set; }

    public double? ExcludeScore { get; set; }

    public double? Score { get; set; }

    public double? WeightScore { get; set; }

    public double? NetScore { get; set; }

    public string? Muser { get; set; }

    public DateTime? Mwhen { get; set; }

    public string? AssessorRoleId { get; set; }

    public int? AssessorLocationId { get; set; }

    public int? EvaluationFormUid { get; set; }

    public string? AssessorName { get; set; }

    public string? Comments { get; set; }
}
