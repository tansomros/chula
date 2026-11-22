using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class AssessmentSubject
{
    public int Uid { get; set; }

    public int EduYear { get; set; }

    public int SubjectId { get; set; }

    public int EvaluationGroupUid { get; set; }

    public string? SubjectCode { get; set; }

    public string? Cuser { get; set; }

    public DateTime? Cwhen { get; set; }
}
