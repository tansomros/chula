using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class StudentAssessment
{
    public long Uid { get; set; }

    public int EduYear { get; set; }

    public int? EvaluationGroupUid { get; set; }

    public int EvaluationTopicUid { get; set; }

    public int AssessorUid { get; set; }

    public string SubjectCode { get; set; } = null!;

    public string StudentCode { get; set; } = null!;

    public double? Score1 { get; set; }

    public double? Score2 { get; set; }

    public string? AssessorRoleId { get; set; }

    public int? AssessorLocationId { get; set; }

    public string? IsExclude { get; set; }

    public string? Cuser { get; set; }

    public DateTime? Cwhen { get; set; }

    public string? Muser { get; set; }

    public DateTime? Mwhen { get; set; }

    public int? XEvaluationFormUid { get; set; }
}
