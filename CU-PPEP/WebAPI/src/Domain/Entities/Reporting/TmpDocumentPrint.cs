using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Reporting;

public partial class TmpDocumentPrint
{
    public int Uid { get; set; }

    public int? DocumentTemplateUid { get; set; }

    public int? RunningNumber { get; set; }

    public int? LocationId { get; set; }
}
