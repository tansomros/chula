using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Teachers;

public partial class Person
{
    public int PersonId { get; set; }

    public int? PrefixId { get; set; }

    public string? PrefixName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? NickName { get; set; }

    public string? Gender { get; set; }

    public int? PositionId { get; set; }

    public string? PositionName { get; set; }

    public int? DepartmentUid { get; set; }

    public string? DepartmentName { get; set; }

    public string? OrganizationName { get; set; }

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

    public string? Cvpath { get; set; }

    public string? PersonType { get; set; }

    public int? LocationId { get; set; }

    public int? WorkId { get; set; }

    public string? LicenseNo { get; set; }

    public double? Credits { get; set; }

    public string? IsTraining { get; set; }

    public string? IsMain { get; set; }

    public int? UserId { get; set; }

    public string? StatusFlag { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? UpdateBy { get; set; }

    public string? ConfirmStatus { get; set; }
}
