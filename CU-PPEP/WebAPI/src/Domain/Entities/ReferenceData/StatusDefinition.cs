using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.ReferenceData;

public partial class StatusDefinition
{
    public string StatusGroup { get; set; } = null!;

    public string StatusCode { get; set; } = null!;

    public string? StatusName { get; set; }
}
