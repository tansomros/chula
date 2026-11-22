using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Students;

public partial class PracticeHistory
{
    public long ItemId { get; set; }

    public string? StudentCode { get; set; }

    public string? SubjectCode { get; set; }

    public string? PhaseId { get; set; }

    public int? LocationId { get; set; }

    public string? Grade { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? UpdateBy { get; set; }
}
