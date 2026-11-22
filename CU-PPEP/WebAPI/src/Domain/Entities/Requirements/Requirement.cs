using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Requirements;

public partial class Requirement
{
    public long Reqid { get; set; }

    public int Reqyear { get; set; }

    public int LocationId { get; set; }

    public int TimePhaseId { get; set; }

    public string? SubjectCode { get; set; }

    public string? IsSameGender { get; set; }

    public int? Man { get; set; }

    public int? Women { get; set; }

    public int? NoSpec { get; set; }
}
