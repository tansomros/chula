using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Assessments;

public partial class AssessmentMailAlert
{
    public int Uid { get; set; }

    public int? LocationId { get; set; }

    public int? PhaseId { get; set; }

    public int? TimePhaseId { get; set; }

    public string? Email { get; set; }

    public DateTime? SendDttm { get; set; }

    public string? SendStatus { get; set; }

    public string? StudentCode { get; set; }

    public string? SubjectCode { get; set; }

    public string? Lang { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
}
