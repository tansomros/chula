using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Reporting;

public partial class TmStudentRegisterSummary
{
    public int? Reqyear { get; set; }

    public int? LocationGroupId { get; set; }

    public string? LocationGroup { get; set; }

    public int LocationId { get; set; }

    public string? LocationName { get; set; }

    public string? PhaseName1 { get; set; }

    public string? PhaseName2 { get; set; }

    public int? M1 { get; set; }

    public int? F1 { get; set; }

    public int? N1 { get; set; }

    public int? M2 { get; set; }

    public int? F2 { get; set; }

    public int? N2 { get; set; }

    public string? Remark { get; set; }
}
