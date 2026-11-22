using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Teachers;

public partial class Coordinator
{
    public int Uid { get; set; }

    public int? LocationId { get; set; }

    public string? Name { get; set; }

    public string? PositionName { get; set; }

    public int? WorkId { get; set; }

    public string? Email { get; set; }

    public string? Tel { get; set; }

    public int? SubjectId { get; set; }

    public int? IsDefault { get; set; }

    public string? Remark { get; set; }

    public string? StatusFlag { get; set; }
}
