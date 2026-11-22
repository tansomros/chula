using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Security;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public int? IsActive { get; set; }

    public int? UpdBy { get; set; }

    public DateTime? UpdDate { get; set; }
}
