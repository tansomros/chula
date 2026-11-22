using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Documents;

public partial class News
{
    public int NewsId { get; set; }

    public DateTime? NewsDate { get; set; }

    public int? NewsOrder { get; set; }

    public string? Title { get; set; }

    public int? FirstPage { get; set; }

    public string? NewsType { get; set; }

    public string? LinkPath { get; set; }

    public int? IsPublic { get; set; }

    public string? ContentNews { get; set; }
}
