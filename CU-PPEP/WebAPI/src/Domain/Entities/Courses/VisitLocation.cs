using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Courses;

public partial class VisitLocation
{
    public int Uid { get; set; }

    public int? EduYear { get; set; }

    public int? LocationId { get; set; }

    public string? IsForceVisit { get; set; }

    public string? Cuser { get; set; }

    public DateTime? Cwhen { get; set; }
}
