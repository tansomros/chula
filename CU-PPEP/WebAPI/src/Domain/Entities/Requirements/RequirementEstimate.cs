using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Requirements;

public partial class RequirementEstimate
{
    public int Uid { get; set; }

    public int? EduYear { get; set; }

    public int? WorkId { get; set; }

    public int? LocationId { get; set; }

    public int? Qty1 { get; set; }

    public int? Qty2 { get; set; }

    public string? ReqOption { get; set; }

    public string? IsSameGender { get; set; }

    public string? Cuser { get; set; }

    public DateTime? Cwhen { get; set; }

    public string? Muser { get; set; }

    public DateTime? Mwhen { get; set; }
}
