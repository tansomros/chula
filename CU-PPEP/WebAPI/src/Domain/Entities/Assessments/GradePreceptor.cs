using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class GradePreceptor
{
    public int Uid { get; set; }

    public int EduYear { get; set; }

    public string SubjectCode { get; set; } = null!;

    public string Grade { get; set; } = null!;

    public double? MinScore { get; set; }

    public double? MaxScore { get; set; }

    public int? Sort { get; set; }
}
