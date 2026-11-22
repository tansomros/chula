using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Students;

public partial class StudentLevel
{
    public int Code { get; set; }

    public int? LevelClass { get; set; }

    public string? StudentStatus { get; set; }
}
