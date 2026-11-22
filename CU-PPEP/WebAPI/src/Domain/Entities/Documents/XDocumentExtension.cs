using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Documents;

public partial class XDocumentExtension
{
    public int Uid { get; set; }

    public int? DocumentTemplateUid { get; set; }

    public string? Descriptions { get; set; }

    public string? Qty { get; set; }

    public string? Uom { get; set; }

    public int? RefType { get; set; }
}
