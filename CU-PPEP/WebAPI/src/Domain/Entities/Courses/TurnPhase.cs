using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Courses;

public partial class TurnPhase
{
    public int PhaseId { get; set; }

    public int EduYear { get; set; }

    public int PhaseNo { get; set; }

    public string? Name { get; set; }

    public string? Descriptions { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? DayCount { get; set; }

    public string? StatusFlag { get; set; }
}
