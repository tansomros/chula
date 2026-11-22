using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Teachers;

public partial class Perceptor
{
    public int PerceptorId { get; set; }

    public string? Prefix { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? NickName { get; set; }

    public string? Gender { get; set; }

    public string? PositionName { get; set; }

    public string? DeptName { get; set; }

    public string? Prefix1 { get; set; }

    public string? Prefix2 { get; set; }

    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public string? MobilePhone { get; set; }

    public string? Address { get; set; }

    public string? District { get; set; }

    public string? City { get; set; }

    public int? ProvinceId { get; set; }

    public string? ProvinceName { get; set; }

    public string? ZipCode { get; set; }

    public string? PicturePath { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? UpdateBy { get; set; }

    public int? LocationId { get; set; }
}
