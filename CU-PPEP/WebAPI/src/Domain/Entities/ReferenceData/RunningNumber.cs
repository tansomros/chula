using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.ReferenceData;

public partial class RunningNumber
{
    public int Uid { get; set; }

    public int? Cyear { get; set; }

    public string Code { get; set; } = null!;

    public int? RunningValue { get; set; }
}
