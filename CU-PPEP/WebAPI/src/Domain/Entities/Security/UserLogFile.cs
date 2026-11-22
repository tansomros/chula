using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Security;

public partial class UserLogFile
{
    public long LogId { get; set; }

    public int? UserId { get; set; }

    public DateTime? WorkDate { get; set; }

    public string? ActType { get; set; }

    public string? DbEffective { get; set; }

    public string? Descrp { get; set; }

    public string? Remark { get; set; }
}
