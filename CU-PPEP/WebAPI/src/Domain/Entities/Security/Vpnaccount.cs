using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Security;

public partial class Vpnaccount
{
    public int LocationId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? StatusFlag { get; set; }
}
