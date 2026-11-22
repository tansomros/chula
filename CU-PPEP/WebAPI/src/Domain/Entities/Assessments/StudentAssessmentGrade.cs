using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class StudentAssessmentGrade
{
    public long Uid { get; set; }

    public int EduYear { get; set; }

    public string StudentCode { get; set; } = null!;

    public string SubjectCode { get; set; } = null!;

    public int? StudentId { get; set; }

    public int? SubjectId { get; set; }

    public double? TotalScore { get; set; }

    public string? Grade { get; set; }

    public string? Cuser { get; set; }

    public DateTime? Cwhen { get; set; }

    public string? Muser { get; set; }

    public DateTime? Mwhen { get; set; }
}
