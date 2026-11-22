using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Locations;

public partial class LocationZone
{
    public string ZoneId { get; set; } = null!;

    public string? ZoneName { get; set; }
}
