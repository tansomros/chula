using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Documents;

public partial class DocumentPrint
{
    public int Uid { get; set; }

    public int? DocumentUid { get; set; }

    public string? EduYear { get; set; }

    public string? DocumentDate { get; set; }

    public int? RunningNumber { get; set; }

    public string? LocationId { get; set; }

    public string? LetterTo { get; set; }

    public string? LocationName { get; set; }

    public string? LevelClass { get; set; }

    public int? PhaseCount { get; set; }

    public int? PhaseStart { get; set; }

    public int? PhaseEnd { get; set; }

    public int? Cuser { get; set; }

    public DateTime? Cwhen { get; set; }

    public string? PersonName { get; set; }
}
