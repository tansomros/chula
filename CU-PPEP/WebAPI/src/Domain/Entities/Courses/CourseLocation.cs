using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Courses;

public partial class CourseLocation
{
    public int ItemId { get; set; }

    public int CourseId { get; set; }

    public int LocationId { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? UpdBy { get; set; }

    public string? IsTrain { get; set; }

    public string? IsSupervision { get; set; }

    public string? IsForceSupervision { get; set; }
}
