using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Reporting;

public partial class TmpReport
{
    public string? Code { get; set; }

    public int? StudentId { get; set; }

    public string StudentCode { get; set; } = null!;

    public string Username { get; set; } = null!;
}
