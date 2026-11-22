using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Locations;

public partial class LocationRecommend
{
    public int Uid { get; set; }

    public string? StudentCode { get; set; }

    public int? LocationId { get; set; }

    public string? ApproveStatus { get; set; }

    public string? StatusFlag { get; set; }

    public string? Title { get; set; }

    public string? Recommend { get; set; }

    public DateTime? Mwhen { get; set; }

    public int? SubjectId { get; set; }
}
