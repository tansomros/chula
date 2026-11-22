using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.ReferenceData;

public partial class Prefix
{
    public int PrefixId { get; set; }

    public string? PrefixName { get; set; }

    public string? Remark { get; set; }

    public int? IsActive { get; set; }

    public string? IsStudent { get; set; }

    public string? IsTeacher { get; set; }

    public string? IsPreceptor { get; set; }

    public string? Cuser { get; set; }

    public DateTime? Cwhen { get; set; }

    public string? Muser { get; set; }

    public DateTime? Mwhen { get; set; }
}
