using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Students;

public partial class StudentStatus
{
    public string StatusCode { get; set; } = null!;

    public string? StatusName { get; set; }

    public string? StatusFlag { get; set; }
}
