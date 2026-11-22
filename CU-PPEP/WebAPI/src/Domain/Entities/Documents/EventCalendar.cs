using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Documents;

public partial class EventCalendar
{
    public int Uid { get; set; }

    public string? Title { get; set; }

    public string? Startdate { get; set; }

    public string? Enddate { get; set; }

    public string? StatusFlag { get; set; }

    public string? Details { get; set; }

    public int? Muser { get; set; }

    public DateTime? Mwhen { get; set; }

    public string? Barcolor { get; set; }
}
