using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Reporting;

public partial class TmpStudentCert
{
    public int StudentId { get; set; }

    public string Username { get; set; } = null!;

    public string? DocumentDate { get; set; }
}
