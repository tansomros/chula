using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Teachers;

public partial class SupervisionPlanDetail
{
    public long Uid { get; set; }

    public int PlanUid { get; set; }

    public int? LocationId { get; set; }

    public string? Cuser { get; set; }

    public DateTime? Cwhen { get; set; }

    public string? Muser { get; set; }

    public DateTime? Mwhen { get; set; }
}
