using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Teachers;

public partial class Supervisor
{
    public long? Uid { get; set; }

    public int Pyear { get; set; }

    public int CourseId { get; set; }

    public int PersonId { get; set; }

    public int? LocationId { get; set; }

    public string? SubjectCode { get; set; }

    public int? TimePhaseId { get; set; }

    public DateOnly WorkDate { get; set; }

    public int WorkTime { get; set; }

    public string? TravelBy { get; set; }

    public DateTime? UpdDate { get; set; }

    public string? UpdBy { get; set; }

    public string? IsSend { get; set; }
}
