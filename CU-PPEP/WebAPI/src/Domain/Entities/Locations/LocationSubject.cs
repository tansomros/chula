using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Locations;

public partial class LocationSubject
{
    public int Uid { get; set; }

    public int? LocationId { get; set; }

    public int? SubjectId { get; set; }
}
