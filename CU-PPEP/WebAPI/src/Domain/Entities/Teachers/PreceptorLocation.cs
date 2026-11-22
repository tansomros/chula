using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Teachers;

public partial class PreceptorLocation
{
    public int Uid { get; set; }

    public int? EduYear { get; set; }

    public int PersonId { get; set; }

    public int? LocationId { get; set; }

    public int? UserId { get; set; }

    public string? StatusFlag { get; set; }
}
