using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Security;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string? DisplayName { get; set; }

    public string? XFirstName { get; set; }

    public string? XLastName { get; set; }

    public string? XEmail { get; set; }

    public int? IsSuperUser { get; set; }

    public int? IsPublic { get; set; }

    public int? UserProfileId { get; set; }

    public string? ProfileId { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? PolicyAccept { get; set; }

    public DateTime? AcceptDttm { get; set; }

    public string? StatusFlag { get; set; }
}
