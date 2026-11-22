using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.ReferenceData;

public partial class Department
{
    public int DepartmentUid { get; set; }

    public string? DepartmentName { get; set; }

    public string? StatusFlag { get; set; }
}
