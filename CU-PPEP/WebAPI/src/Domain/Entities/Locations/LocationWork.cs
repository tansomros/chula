using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Locations;

public partial class LocationWork
{
    public int WorkId { get; set; }

    public string? LocationGroup { get; set; }

    public string? WorkName { get; set; }

    public string? StatusFlag { get; set; }

    public DateTime? Cwhen { get; set; }

    public DateTime? Mwhen { get; set; }
}
