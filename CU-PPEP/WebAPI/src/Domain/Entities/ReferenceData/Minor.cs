using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.ReferenceData;

public partial class Minor
{
    public int MinorId { get; set; }

    public string? MinorName { get; set; }

    public int? MajorId { get; set; }
}
