using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Students;

public partial class Education
{
    public int Uid { get; set; }

    public int? StudentId { get; set; }

    public string? CourseName { get; set; }

    public string? Institute { get; set; }

    public string? ProvinceName { get; set; }

    public string? EduYear { get; set; }
}
