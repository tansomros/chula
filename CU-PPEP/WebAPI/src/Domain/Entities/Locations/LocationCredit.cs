using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Locations;

public partial class LocationCredit
{
    public int Uid { get; set; }

    public int? Byear { get; set; }

    public int? LocationId { get; set; }

    public double? CreditBalance { get; set; }

    public string? StatusFlag { get; set; }

    public int? Muser { get; set; }

    public DateTime? Mwhen { get; set; }
}
