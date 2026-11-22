using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.ReferenceData;

public partial class SubMinor
{
    public int SubMinorId { get; set; }

    public string? SubMinorName { get; set; }

    public int? MinorId { get; set; }
}
