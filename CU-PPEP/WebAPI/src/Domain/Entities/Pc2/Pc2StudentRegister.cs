using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Pc2;

public partial class Pc2StudentRegister
{
    public int Uid { get; set; }

    public int EduYear { get; set; }

    public int DegreeNo { get; set; }

    public string StudentCode { get; set; } = null!;

    public int SubjectUid { get; set; }

    public string? StatusFlag { get; set; }

    public DateTime? Mwhen { get; set; }

    public string? Muser { get; set; }
}
