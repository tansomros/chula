using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Reporting;

public partial class ReportGroupLink
{
    public int Rptgrpuid { get; set; }

    public string SubjectCode { get; set; } = null!;
}
