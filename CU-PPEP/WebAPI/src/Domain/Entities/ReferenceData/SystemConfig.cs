using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.ReferenceData;

public partial class SystemConfig
{
    public int ItemId { get; set; }

    public string CodeConfig { get; set; } = null!;

    public string? ValueConfig { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Sort { get; set; }
}
