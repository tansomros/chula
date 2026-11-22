using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.ReferenceData;

public partial class Major
{
    public int MajorId { get; set; }

    public string? MajorName { get; set; }

    public string? MajorNameEn { get; set; }

    public string? AliasName { get; set; }
}
