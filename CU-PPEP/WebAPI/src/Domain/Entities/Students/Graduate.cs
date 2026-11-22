using System;
using System.Collections.Generic;

namespace CUPPEP.Domain.Entities.Students;

public partial class Graduate
{
    public int StudentId { get; set; }

    public string StudentCode { get; set; } = null!;

    public string? Prefix { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? NickName { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public string? MobilePhone { get; set; }

    public string? BirthDate { get; set; }

    public string? BloodGroup { get; set; }

    public int? MajorId { get; set; }

    public int? MinorId { get; set; }

    public int? SubMinorId { get; set; }

    public int? AdvisorId { get; set; }

    public double? Gpax { get; set; }

    public string? FatherFirstName { get; set; }

    public string? FatherLastName { get; set; }

    public string? FatherCareer { get; set; }

    public string? FatherTel { get; set; }

    public string? MotherFirstName { get; set; }

    public string? MotherLastName { get; set; }

    public string? MotherCareer { get; set; }

    public string? MotherTel { get; set; }

    public int? Sibling { get; set; }

    public int? ChildNo { get; set; }

    public string? Address { get; set; }

    public string? District { get; set; }

    public string? City { get; set; }

    public int? ProvinceId { get; set; }

    public string? ProvinceName { get; set; }

    public string? ZipCode { get; set; }

    public string? CongenitalDisease { get; set; }

    public string? MedicineUsually { get; set; }

    public string? MedicalHistory { get; set; }

    public string? Hobby { get; set; }

    public string? Talent { get; set; }

    public string? Expectations { get; set; }

    public string? PrimarySchool { get; set; }

    public string? PrimaryProvince { get; set; }

    public int? PrimaryYear { get; set; }

    public string? SecondarySchool { get; set; }

    public string? SecondaryProvince { get; set; }

    public int? SecondaryYear { get; set; }

    public string? HighSchool { get; set; }

    public string? HighProvince { get; set; }

    public int? HighYear { get; set; }

    public string? ContactName { get; set; }

    public string? ContactAddress { get; set; }

    public string? ContactTel { get; set; }

    public string? ContactRelation { get; set; }

    public string? PicturePath { get; set; }

    public DateTime? LastUpdate { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public int? LevelClass { get; set; }

    public string? StatusFlag { get; set; }

    public string? StudentStatus { get; set; }

    public string? AddressLive { get; set; }

    public string? TelLive { get; set; }

    public string? FirstNameEn { get; set; }

    public string? LastNameEn { get; set; }

    public string? Religion { get; set; }

    public string? MedicalRecommend { get; set; }

    public string? HospitalName { get; set; }

    public string? DoctorName { get; set; }

    public string? Characteristic { get; set; }

    public string? FavoriteSubject { get; set; }

    public string? EduExperience { get; set; }

    public int? PayorId { get; set; }

    public string? PayorDetail { get; set; }

    public string? Cvlink { get; set; }
}
