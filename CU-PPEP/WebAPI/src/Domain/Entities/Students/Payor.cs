using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Students;

public partial class Payor
{
    public int PayorId { get; set; }

    public string? PayorName { get; set; }

    public string? StatusFlag { get; set; }
}
