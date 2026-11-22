using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Requirements;

public partial class Requirements2
{
    public long Reqid { get; set; }

    public int Reqyear { get; set; }

    public int LocationId { get; set; }

    public string SubjectCode { get; set; } = null!;

    public int? LocationGroupId { get; set; }

    public string? IsSameGender { get; set; }

    public int? WorkDayId { get; set; }

    public string? WorkDayDesc { get; set; }

    public int? WorkTimeId { get; set; }

    public string? WorkTimeDesc { get; set; }

    public int? HasResidence { get; set; }

    public int? HasCost { get; set; }

    public double? PayAmount { get; set; }

    public int? IsForce { get; set; }

    public string? ConfirmHold { get; set; }

    public string? ApartmentName { get; set; }

    public string? ApartmentTel { get; set; }

    public string? ApartmentAddr { get; set; }

    public string? ApartmentTravel { get; set; }

    public string? ApartmentDistance { get; set; }

    public int? IsBranch { get; set; }

    public string? BranchRemark { get; set; }

    public string? WorkList { get; set; }

    public string? WorkSpec { get; set; }

    public string? WorkTop { get; set; }

    public string? Remark { get; set; }

    public string? Informant { get; set; }

    public string? InfoPosition { get; set; }

    public string? InfoDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? UpdBy { get; set; }

    public int? IsPublic { get; set; }
}
