using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Courses;

public partial class Subjects2
{
    public int SubjectId { get; set; }

    public string SubjectCode { get; set; } = null!;

    public string? NameTh { get; set; }

    public string? NameEn { get; set; }

    public double? SubjectUnit { get; set; }

    public int? MajorId { get; set; }

    public int? IsPublic { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? UpdBy { get; set; }

    public string? AliasName { get; set; }

    public string? PharmSci { get; set; }

    public string? PharmCare { get; set; }
}
