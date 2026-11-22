using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Reporting;

public partial class ReportGroup
{
    public int Rptgrpuid { get; set; }

    public string? Name { get; set; }

    public string? StatusFlag { get; set; }
}
