using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Courses;

public partial class CourseStudent
{
    public int ItemId { get; set; }

    public int CourseId { get; set; }

    public string StudentCode { get; set; } = null!;

    public DateTime? LastUpdate { get; set; }

    public string? UpdBy { get; set; }

    public int? Status { get; set; }
}
