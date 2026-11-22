using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class EvaluationGroup
{
    public int Uid { get; set; }

    public string? Name { get; set; }

    public string? AliasName { get; set; }

    public int? FullScore { get; set; }

    public string? IsAdvisor { get; set; }

    public string? IsPreceptor { get; set; }

    public string? IsCompare { get; set; }

    public string? IsNoScore { get; set; }

    public string? StatusFlag { get; set; }

    public int? ScoreMin { get; set; }

    public int? ScoreMax { get; set; }

    public string? Remark { get; set; }

    public int? Sort { get; set; }
}
