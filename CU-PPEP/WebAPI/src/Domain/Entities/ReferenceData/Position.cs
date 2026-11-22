using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.ReferenceData;

public partial class Position
{
    public int PositionId { get; set; }

    public string PositionName { get; set; } = null!;

    public int? IsActive { get; set; }

    public int? OrderSort { get; set; }
}
