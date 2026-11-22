Imports System
Imports System.Configuration
Imports System.Data


Public Class StudentInfo
    Inherits ApplicationBaseClass
    Private Const intFieldNum As Integer = 59 'กำหนด จำนวนฟิลด์ที่มี
    Public Enum fldPos As Integer 'ประกาศตำแหน่งของฟิลด์
        f00_Student_ID = 0
        f01_Student_Code = 1
        f02_Prefix = 2
        f03_FirstName = 3
        f04_LastName = 4
        f05_NickName = 5
        f06_Gender = 6
        f07_Email = 7
        f08_Telephone = 8
        f09_MobilePhone = 9
        f10_BirthDate = 10
        f11_BloodGroup = 11
        f12_MajorID = 12
        f13_MinorID = 13
        f14_SubMinorID = 14
        f15_AdvisorID = 15
        f16_Father_FirstName = 16
        f17_Father_LastName = 17
        f18_Father_Career = 18
        f19_Father_Tel = 19
        f20_Mother_FirstName = 20
        f21_Mother_LastName = 21
        f22_Mother_Career = 22
        f23_Mother_Tel = 23
        f24_Sibling = 24
        f25_ChildNo = 25
        f26_Address = 26
        f27_District = 27
        f28_City = 28
        f29_ProvinceID = 29
        f30_ProvinceName = 30
        f31_ZipCode = 31
        f32_CongenitalDisease = 32
        f33_MedicineUsually = 33
        f34_MedicalHistory = 34
        f35_Hobby = 35
        f36_Talent = 36
        f37_Expectations = 37
        f38_PrimarySchool = 38
        f39_PrimaryProvince = 39
        f40_PrimaryYear = 40
        f41_SecondarySchool = 41
        f42_SecondaryProvince = 42
        f43_SecondaryYear = 43
        f44_HighSchool = 44
        f45_HighProvince = 45
        f46_HighYear = 46
        f47_ContactName = 47
        f48_ContactAddress = 48
        f49_ContactTel = 49
        f50_PicturePath = 50
        f51_LastUpdate = 51
        f52_CreateDate = 52
        f53_UpdateBy = 53
        f54_GPAX = 54
        f55_ContactRelation = 55
        f56_LevelClass = 56
        f57_AddressLive = 57
        f58_TelLive = 58
        f59_StudentStatus = 59

    End Enum

    Sub init() 'ใส่ชื่อ table, field name และ ค่า default
        strTableName = "STUDENTS"
        intTotalField = intFieldNum

        ReDim tblField(intFieldNum)
        Array.Clear(tblField, 0, tblField.Length)
        tblField(fldPos.f00_Student_ID).fldName = "Student_ID"
        tblField(fldPos.f00_Student_ID).fldValue = 0

        tblField(fldPos.f01_Student_Code).fldName = "Student_Code"
        tblField(fldPos.f02_Prefix).fldName = "Prefix"
        tblField(fldPos.f03_FirstName).fldName = "FirstName"
        tblField(fldPos.f04_LastName).fldName = "LastName"
        tblField(fldPos.f05_NickName).fldName = "NickName"
        tblField(fldPos.f06_Gender).fldName = "Gender"
        tblField(fldPos.f07_Email).fldName = "Email"
        tblField(fldPos.f08_Telephone).fldName = "Telephone"
        tblField(fldPos.f09_MobilePhone).fldName = "MobilePhone"
        tblField(fldPos.f10_BirthDate).fldName = "BirthDate"
        tblField(fldPos.f11_BloodGroup).fldName = "BloodGroup"
        tblField(fldPos.f12_MajorID).fldName = "MajorID"
        tblField(fldPos.f13_MinorID).fldName = "MinorID"
        tblField(fldPos.f14_SubMinorID).fldName = "SubMinorID"
        tblField(fldPos.f15_AdvisorID).fldName = "AdvisorID"
        tblField(fldPos.f16_Father_FirstName).fldName = "Father_FirstName"
        tblField(fldPos.f17_Father_LastName).fldName = "Father_LastName"
        tblField(fldPos.f18_Father_Career).fldName = "Father_Career"
        tblField(fldPos.f19_Father_Tel).fldName = "Father_Tel"
        tblField(fldPos.f20_Mother_FirstName).fldName = "Mother_FirstName"
        tblField(fldPos.f21_Mother_LastName).fldName = "Mother_LastName"
        tblField(fldPos.f22_Mother_Career).fldName = "Mother_Career"
        tblField(fldPos.f23_Mother_Tel).fldName = "Mother_Tel"
        tblField(fldPos.f24_Sibling).fldName = "Sibling"
        tblField(fldPos.f25_ChildNo).fldName = "ChildNo"
        tblField(fldPos.f26_Address).fldName = "Address"
        tblField(fldPos.f27_District).fldName = "District"
        tblField(fldPos.f28_City).fldName = "City"
        tblField(fldPos.f29_ProvinceID).fldName = "ProvinceID"
        tblField(fldPos.f30_ProvinceName).fldName = "ProvinceName"
        tblField(fldPos.f31_ZipCode).fldName = "ZipCode"
        tblField(fldPos.f32_CongenitalDisease).fldName = "CongenitalDisease"
        tblField(fldPos.f33_MedicineUsually).fldName = "MedicineUsually"
        tblField(fldPos.f34_MedicalHistory).fldName = "MedicalHistory"
        tblField(fldPos.f35_Hobby).fldName = "Hobby"
        tblField(fldPos.f36_Talent).fldName = "Talent"
        tblField(fldPos.f37_Expectations).fldName = "Expectations"
        tblField(fldPos.f38_PrimarySchool).fldName = "PrimarySchool"
        tblField(fldPos.f39_PrimaryProvince).fldName = "PrimaryProvince"
        tblField(fldPos.f40_PrimaryYear).fldName = "PrimaryYear"
        tblField(fldPos.f41_SecondarySchool).fldName = "SecondarySchool"
        tblField(fldPos.f42_SecondaryProvince).fldName = "SecondaryProvince"
        tblField(fldPos.f43_SecondaryYear).fldName = "SecondaryYear"
        tblField(fldPos.f44_HighSchool).fldName = "HighSchool"
        tblField(fldPos.f45_HighProvince).fldName = "HighProvince"
        tblField(fldPos.f46_HighYear).fldName = "HighYear"
        tblField(fldPos.f47_ContactName).fldName = "ContactName"
        tblField(fldPos.f48_ContactAddress).fldName = "ContactAddress"
        tblField(fldPos.f49_ContactTel).fldName = "ContactTel"
        tblField(fldPos.f50_PicturePath).fldName = "PicturePath"
        tblField(fldPos.f51_LastUpdate).fldName = "LastUpdate"
        tblField(fldPos.f52_CreateDate).fldName = "CreateDate"
        tblField(fldPos.f53_UpdateBy).fldName = "UpdateBy"
        tblField(fldPos.f54_GPAX).fldName = "GPAX"
        tblField(fldPos.f55_ContactRelation).fldName = "ContactRelation"
        tblField(fldPos.f56_LevelClass).fldName = "LevelClass"

        tblField(fldPos.f57_AddressLive).fldName = "AddressLive"
        tblField(fldPos.f58_TelLive).fldName = "TelLive"
        tblField(fldPos.f59_StudentStatus).fldName = "StudentStatus"


    End Sub

    Sub New()
        MyBase.New()
        Me.init()

    End Sub

#Region "Property"

    Public Property RoleID() As Integer
        Get
            Return tblField(fldPos.f00_Student_ID).fldValue
        End Get
        Set(ByVal Value As Integer)
            tblField(fldPos.f00_Student_ID).fldValue = Value
            tblField(fldPos.f00_Student_ID).fldAffect = False
        End Set
    End Property
    Public Property RoleName() As String
        Get
            Return tblField(fldPos.f01_Student_Code).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f01_Student_Code).fldValue = Value
            tblField(fldPos.f01_Student_Code).fldAffect = True
        End Set
    End Property

    Public Property Descriptions() As String
        Get
            Return tblField(fldPos.f02_Prefix).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f02_Prefix).fldValue = Value
            tblField(fldPos.f02_Prefix).fldAffect = True
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return tblField(fldPos.f03_FirstName).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f03_FirstName).fldValue = Value
            tblField(fldPos.f03_FirstName).fldAffect = True
        End Set
    End Property



    Public ReadOnly Property TableName() As String
        Get
            Return strTableName
        End Get
    End Property


#End Region

End Class

