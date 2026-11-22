using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Locations;

public partial class LocationWorkDetail
{
    public int LocationId { get; set; }

    public int WorkId { get; set; }

    public string? StatusFlag { get; set; }

    public DateTime? Cwhen { get; set; }

    public DateTime? Mwhen { get; set; }
}
