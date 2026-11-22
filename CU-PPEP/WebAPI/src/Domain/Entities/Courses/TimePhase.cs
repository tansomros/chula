using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Courses;

public partial class TimePhase
{
    public int Uid { get; set; }

    public int? TimePhaseId { get; set; }

    public int EduYear { get; set; }

    public int? CourseId { get; set; }

    public string SubjectCode { get; set; } = null!;

    public int? IsPublic { get; set; }

    public int? PhaseId { get; set; }
}
