using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class Assessment
{
    public int AssessmentId { get; set; }

    public int Pyear { get; set; }

    public int? CourseId { get; set; }

    public int? SubjectId { get; set; }

    public string SubjectCode { get; set; } = null!;

    public string StudentCode { get; set; } = null!;

    public int TimePhaseId { get; set; }

    public int LocationId { get; set; }

    public double? TeacherScore { get; set; }

    public double? PreceptorScore { get; set; }

    public string? Grade { get; set; }

    public int? Status { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? UpdBy { get; set; }
}
