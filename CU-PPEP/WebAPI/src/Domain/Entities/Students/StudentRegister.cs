using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Students;

public partial class StudentRegister
{
    public int ItemId { get; set; }

    public int Regyear { get; set; }

    public int DegreeNo { get; set; }

    public string StudentCode { get; set; } = null!;

    public string SubjectCode { get; set; } = null!;

    public int LocationId { get; set; }

    public int? TimePhaseId { get; set; }

    public int? Status { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? UpdBy { get; set; }
}
