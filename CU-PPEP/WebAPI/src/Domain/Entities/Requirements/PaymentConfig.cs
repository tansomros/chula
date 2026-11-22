using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Requirements;

public partial class PaymentConfig
{
    public int Uid { get; set; }

    public string? SubjectCode { get; set; }

    public int? PaymentType { get; set; }

    public double? PayAmount { get; set; }

    public DateOnly? EffectiveTo { get; set; }

    public string? StatusFlag { get; set; }
}
