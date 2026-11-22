using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Courses;

public partial class Course
{
    public int? CourseId { get; set; }

    public int Cyear { get; set; }

    public string SubjectCode { get; set; } = null!;

    public DateTime? LastUpdate { get; set; }

    public string? UpdBy { get; set; }

    public string? StatusFlag { get; set; }

    public int? SubjectId { get; set; }
}
