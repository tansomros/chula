using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Courses;

public partial class Subject
{
    public int? SubjectId { get; set; }

    public string SubjectCode { get; set; } = null!;

    public string? NameTh { get; set; }

    public string? NameEn { get; set; }

    public double? SubjectUnit { get; set; }

    /// <summary>
    /// วิชาเลือก/บังคับ
    /// </summary>
    public int? MajorId { get; set; }

    public int? IsPublic { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? UpdBy { get; set; }

    public string? AliasName { get; set; }

    public string? PharmSci { get; set; }

    public string? PharmCare { get; set; }

    public int? LevelClass { get; set; }

    public string? NameCert1Th { get; set; }

    public string? NameCert2Th { get; set; }

    public string? NameCert1En { get; set; }

    public string? NameCert2En { get; set; }

    public int? SubjectGroupId { get; set; }
}
