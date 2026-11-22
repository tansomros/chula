using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Locations;

public partial class Prominent
{
    public int Uid { get; set; }

    public string? Descriptions { get; set; }

    public string? ItemType { get; set; }

    public string? StatusFlag { get; set; }
}
