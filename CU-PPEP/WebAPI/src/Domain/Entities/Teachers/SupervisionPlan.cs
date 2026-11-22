using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Teachers;

public partial class SupervisionPlan
{
    public long? Uid { get; set; }

    public string Code { get; set; } = null!;

    public int? Seq { get; set; }

    public int EduYear { get; set; }

    public int PersonId { get; set; }

    public DateOnly? WorkDate { get; set; }

    public int? WorkTime { get; set; }

    public string? TravelBy { get; set; }

    public string? Remark { get; set; }

    public DateTime? UpdDate { get; set; }

    public string? UpdBy { get; set; }

    public string? ApproveStatus { get; set; }

    public int? ApprovalId { get; set; }

    public DateTime? ApproveDate { get; set; }
}
