using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.ReferenceData;

public partial class RunningConfig
{
    public string Code { get; set; } = null!;

    public string? Descriptions { get; set; }

    public string? IsCode { get; set; }

    public string? IsYear { get; set; }

    public int? NumberDigit { get; set; }

    public string? TemplateCode { get; set; }
}
