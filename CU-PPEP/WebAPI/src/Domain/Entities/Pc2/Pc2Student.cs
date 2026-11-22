using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Pc2;

public partial class Pc2Student
{
    public int Uid { get; set; }

    public int EduYear { get; set; }

    public string StudentCode { get; set; } = null!;
}
