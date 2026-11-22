using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Reporting;

public partial class TmpStudent
{
    public string StudentCode { get; set; } = null!;

    public string? Prefix { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? MajorId { get; set; }

    public int? MinorId { get; set; }

    public int? SubMinorId { get; set; }

    public int? AdvisorId { get; set; }

    public double? Gpax { get; set; }
}
