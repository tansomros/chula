using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Pc2;

public partial class Pc2Quotum
{
    public long Reqid { get; set; }

    public int EduYear { get; set; }

    public int SubjectUid { get; set; }

    public int? Qty { get; set; }

    public int? Muser { get; set; }

    public DateTime? Mwhen { get; set; }
}
