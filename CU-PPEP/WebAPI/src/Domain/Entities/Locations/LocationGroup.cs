using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Locations;

public partial class LocationGroup
{
    public int Code { get; set; }

    public string? Name { get; set; }

    public string? Descriptions { get; set; }

    public int? IsPublic { get; set; }
}
