using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class StudentAssessmentGradeModify
{
    public int Uid { get; set; }

    public int? StudentAssessmentGradeUid { get; set; }

    public double? ScoreOld { get; set; }

    public double? ScoreNew { get; set; }

    public string? GradeOld { get; set; }

    public string? GradeNew { get; set; }

    public string? Reason { get; set; }

    public int? UserId { get; set; }

    public string? Muser { get; set; }

    public DateTime? Mwhen { get; set; }
}
