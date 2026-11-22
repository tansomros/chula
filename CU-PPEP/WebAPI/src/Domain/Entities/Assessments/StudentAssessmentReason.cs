using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class StudentAssessmentReason
{
    public int Uid { get; set; }

    public int? EduYear { get; set; }

    public int? EvaluationGroupUid { get; set; }

    public string? StudentCode { get; set; }

    public string? SubjectCode { get; set; }

    public int? LocationId { get; set; }

    public double? Score { get; set; }

    public DateTime? SubmitDate { get; set; }

    public string? SubmitBy { get; set; }

    public string? Reason { get; set; }
}
