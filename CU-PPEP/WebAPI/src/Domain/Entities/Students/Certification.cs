using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Students;

public partial class Certification
{
    public int? EduYear { get; set; }

    public DateOnly? CertDate { get; set; }
}
