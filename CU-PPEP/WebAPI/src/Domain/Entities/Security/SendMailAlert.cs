using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Security;

public partial class SendMailAlert
{
    public int Uid { get; set; }

    public int? LocationId { get; set; }

    public int? TimePhaseId { get; set; }

    public string? Email { get; set; }

    public DateTime? SendDttm { get; set; }

    public string? SendStatus { get; set; }
}
