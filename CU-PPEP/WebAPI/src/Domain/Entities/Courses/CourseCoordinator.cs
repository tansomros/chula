using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Courses;

public partial class CourseCoordinator
{
    public int ItemId { get; set; }

    public int Cyear { get; set; }

    public string SubjectCode { get; set; } = null!;

    public int PersonId { get; set; }

    public int? CourseId { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? UpdBy { get; set; }
}
