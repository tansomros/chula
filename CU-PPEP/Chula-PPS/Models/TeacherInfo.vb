Imports System
Imports System.Configuration
Imports System.Data


Public Class TeacherInfo
    Inherits ApplicationBaseClass
    Private Const intFieldNum As Integer = 20 'กำหนด จำนวนฟิลด์ที่มี
    Public Enum fldPos As Integer 'ประกาศตำแหน่งของฟิลด์

        f00_Teacher_ID = 0
        f01_Prefix = 1
        f02_FirstName = 2
        f03_LastName = 3
        f04_PositionName = 4
        f05_OrganizationName = 5
        f06_Gender = 6
        f07_Email = 7
        f08_Telephone = 8
        f09_MobilePhone = 9
        f10_NickName = 10
        f11_Address = 11
        f12_District = 12
        f13_City = 13
        f14_ProvinceID = 14
        f15_ProvinceName = 15
        f16_ZipCode = 16
        f17_PicturePath = 17
        f18_LastUpdate = 18
        f19_UpdateBy = 19


    End Enum

    Sub init() 'ใส่ชื่อ table, field name และ ค่า default
        strTableName = "TEACHERS"
        intTotalField = intFieldNum

        ReDim tblField(intFieldNum)
        Array.Clear(tblField, 0, tblField.Length)
        tblField(fldPos.f00_Teacher_ID).fldName = "Teacher_ID"
        tblField(fldPos.f00_Teacher_ID).fldValue = 0
        tblField(fldPos.f01_Prefix).fldName = "Prefix"
        tblField(fldPos.f02_FirstName).fldName = "FirstName"
        tblField(fldPos.f03_LastName).fldName = "LastName"
        tblField(fldPos.f04_PositionName).fldName = "PositionName"
        tblField(fldPos.f05_OrganizationName).fldName = "OrganizationName"
        tblField(fldPos.f06_Gender).fldName = "Gender"
        tblField(fldPos.f07_Email).fldName = "Email"
        tblField(fldPos.f08_Telephone).fldName = "Telephone"
        tblField(fldPos.f09_MobilePhone).fldName = "MobilePhone"
        tblField(fldPos.f10_NickName).fldName = "NickName"
        tblField(fldPos.f11_Address).fldName = "Address"
        tblField(fldPos.f12_District).fldName = "District"
        tblField(fldPos.f13_City).fldName = "City"
        tblField(fldPos.f14_ProvinceID).fldName = "ProvinceID"
        tblField(fldPos.f15_ProvinceName).fldName = "ProvinceName"
        tblField(fldPos.f16_ZipCode).fldName = "ZipCode"
        tblField(fldPos.f17_PicturePath).fldName = "PicturePath"
        tblField(fldPos.f18_LastUpdate).fldName = "LastUpdate"
        tblField(fldPos.f19_UpdateBy).fldName = "UpdateBy"


    End Sub

    Sub New()
        MyBase.New()
        Me.init()

    End Sub

#Region "Property"

    Public Property TeacherID() As Integer
        Get
            Return tblField(fldPos.f00_Teacher_ID).fldValue
        End Get
        Set(ByVal Value As Integer)
            tblField(fldPos.f00_Teacher_ID).fldValue = Value
            tblField(fldPos.f00_Teacher_ID).fldAffect = False
        End Set
    End Property
    Public Property Teacher_Code() As String
        Get
            Return tblField(fldPos.f01_Prefix).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f01_Prefix).fldValue = Value
            tblField(fldPos.f01_Prefix).fldAffect = True
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return tblField(fldPos.f02_FirstName).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f02_FirstName).fldValue = Value
            tblField(fldPos.f02_FirstName).fldAffect = True
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return tblField(fldPos.f03_LastName).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f03_LastName).fldValue = Value
            tblField(fldPos.f03_LastName).fldAffect = True
        End Set
    End Property


    Public Property PositionName() As String
        Get
            Return tblField(fldPos.f04_PositionName).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f04_PositionName).fldValue = Value
            tblField(fldPos.f04_PositionName).fldAffect = True
        End Set
    End Property

    Public Property OrganizationName() As String
        Get
            Return tblField(fldPos.f05_OrganizationName).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f05_OrganizationName).fldValue = Value
            tblField(fldPos.f05_OrganizationName).fldAffect = True
        End Set
    End Property


    Public ReadOnly Property TableName() As String
        Get
            Return strTableName
        End Get
    End Property


#End Region

End Class

