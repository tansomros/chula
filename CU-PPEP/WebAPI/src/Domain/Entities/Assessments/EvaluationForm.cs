using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class EvaluationForm
{
    public int? Uid { get; set; }

    public int? Seqno { get; set; }

    public int? EvaluationTopicUid { get; set; }

    public int? EvaluationGroupUid { get; set; }

    public int? Weight { get; set; }

    public string? StatusFlag { get; set; }

    public string? Remark { get; set; }

    public int? Cuser { get; set; }

    public DateTime? Cwhen { get; set; }

    public int? Muser { get; set; }

    public DateTime? Mwhen { get; set; }
}
