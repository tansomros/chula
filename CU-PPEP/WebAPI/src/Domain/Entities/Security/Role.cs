using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Security;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? Descriptions { get; set; }

    public int IsPublic { get; set; }
}
