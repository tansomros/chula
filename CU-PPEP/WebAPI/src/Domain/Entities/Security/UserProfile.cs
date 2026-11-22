using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Security;

public partial class UserProfile
{
    public int UserProfileId { get; set; }

    public string? ProfileName { get; set; }
}
