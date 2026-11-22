using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class EvaluationTopic
{
    public int Uid { get; set; }

    public string? TopicName { get; set; }

    public double? Weight { get; set; }

    public string? StatusFlag { get; set; }

    public string? Remark { get; set; }

    public string? Cuser { get; set; }

    public DateTime? Cwhen { get; set; }

    public string? Muser { get; set; }

    public DateTime? Mwhen { get; set; }

    public int? XEvaluationGroupUid { get; set; }
}
