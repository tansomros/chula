using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Locations;

public partial class Location
{
    public int LocationId { get; set; }

    public int? LocationIdOld { get; set; }

    public string? Code { get; set; }

    public string? LocationName { get; set; }

    public int? LocationGroupId { get; set; }

    public int? LocationTypeId { get; set; }

    public string? Department { get; set; }

    public int? DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public int? LevelId { get; set; }

    public int? Bed { get; set; }

    public int? Branch { get; set; }

    public int? FieldCount { get; set; }

    public string? Address { get; set; }

    public int? ProvinceId { get; set; }

    public string? ProvinceName { get; set; }

    public string? ZipCode { get; set; }

    public string? OfficeTel { get; set; }

    public string? OfficeFax { get; set; }

    public string? CoName { get; set; }

    public string? CoPosition { get; set; }

    public string? CoMail { get; set; }

    public string? CoTel { get; set; }

    public string? OfficeHours { get; set; }

    public int? OfficerCount { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? UpdBy { get; set; }

    public int? IsPublic { get; set; }

    public string? BillName { get; set; }

    public int? WorkDayId { get; set; }

    public string? WorkDayDesc { get; set; }

    public int? WorkTimeId { get; set; }

    public string? WorkTimeDesc { get; set; }

    public int? HasResidence { get; set; }

    public int? HasCost { get; set; }

    public double? PayAmount { get; set; }

    public int? IsForce { get; set; }

    public string? ConfirmHold { get; set; }

    public string? ApartmentName1 { get; set; }

    public string? ApartmentTel1 { get; set; }

    public string? ApartmentAddr1 { get; set; }

    public string? ApartmentTravel1 { get; set; }

    public string? ApartmentDistance1 { get; set; }

    public int? IsBranch { get; set; }

    public string? BranchRemark { get; set; }

    public string? WorkList { get; set; }

    public string? WorkSpec { get; set; }

    public string? WorkTop { get; set; }

    public string? Remark { get; set; }

    public string? Informant { get; set; }

    public string? InfoPosition { get; set; }

    public string? InfoDate { get; set; }

    public string? IsSameGender { get; set; }

    public string? LetterTo { get; set; }

    public string? ApartmentName2 { get; set; }

    public string? ApartmentTel2 { get; set; }

    public string? ApartmentAddr2 { get; set; }

    public string? ApartmentTravel2 { get; set; }

    public string? ApartmentDistance2 { get; set; }

    public string? ApartmentName3 { get; set; }

    public string? ApartmentTel3 { get; set; }

    public string? ApartmentAddr3 { get; set; }

    public string? ApartmentTravel3 { get; set; }

    public string? ApartmentDistance3 { get; set; }

    public string? Country { get; set; }

    public string? IsReceive { get; set; }

    public string? IsPay { get; set; }

    public string? ZoneId { get; set; }

    public string? OfficeId { get; set; }

    public string? Website { get; set; }

    public string? Facebook { get; set; }

    public string? Lat { get; set; }

    public string? Lng { get; set; }

    public string? LocationNameCert1 { get; set; }

    public string? LocationNameCert2 { get; set; }

    public string? Lang { get; set; }

    public int? CuCoordinator { get; set; }

    public string? CuName { get; set; }

    public string? CuPosition { get; set; }

    public string? CuMail { get; set; }

    public string? CuTel { get; set; }
}
