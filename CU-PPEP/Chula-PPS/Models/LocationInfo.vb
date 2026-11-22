Imports System
Imports System.Configuration
Imports System.Data


Public Class LocationInfo
    Inherits ApplicationBaseClass
    Private Const intFieldNum As Integer = 21 'กำหนด จำนวนฟิลด์ที่มี
    Public Enum fldPos As Integer 'ประกาศตำแหน่งของฟิลด์
        f00_LocationID = 0
        f01_LocationName = 1
        f02_LocationGroupID = 2
        f03_LocationTypeID = 3
        f04_Department = 4
        f05_Field_Count = 5
        f06_Address = 6
        f07_ProvinceID = 7
        f08_ProvinceName = 8
        f09_ZipCode = 9
        f10_Office_Tel = 10
        f11_Office_Fax = 11
        f12_Co_Name = 12
        f13_Co_Position = 13
        f14_Co_Mail = 14
        f15_Co_Tel = 15
        f16_Office_hours = 16
        f17_Officer_Count = 17
        f18_LastUpdate = 18
        f19_UpdBy = 19
        f20_isPublic = 20



    End Enum

    Sub init() 'ใส่ชื่อ tablef_ field name และ ค่า default
        strTableName = "LOCATIONS"
        intTotalField = intFieldNum

        ReDim tblField(intFieldNum)
        Array.Clear(tblField, 0, tblField.Length)
        tblField(fldPos.f00_LocationID).fldName = "LocationID"
        tblField(fldPos.f00_LocationID).fldValue = "0"
        tblField(fldPos.f01_LocationName).fldName = "LocationName"
        tblField(fldPos.f01_LocationName).fldValue = ""
        tblField(fldPos.f02_LocationGroupID).fldName = "LocationGroupID"
        tblField(fldPos.f02_LocationGroupID).fldValue = ""
        tblField(fldPos.f03_LocationTypeID).fldName = "LocationTypeID"
        tblField(fldPos.f04_Department).fldName = "Department"
        tblField(fldPos.f05_Field_Count).fldName = "Field_Count"
        tblField(fldPos.f06_Address).fldName = "Address"
        tblField(fldPos.f07_ProvinceID).fldName = "ProvinceID"
        tblField(fldPos.f08_ProvinceName).fldName = "ProvinceName"
        tblField(fldPos.f09_ZipCode).fldName = "ZipCode"
        tblField(fldPos.f10_Office_Tel).fldName = "Office_Tel"
        tblField(fldPos.f11_Office_Fax).fldName = "Office_Fax"
        tblField(fldPos.f12_Co_Name).fldName = "Co_Name"
        tblField(fldPos.f13_Co_Position).fldName = "Co_Position"
        tblField(fldPos.f14_Co_Mail).fldName = "Co_Mail"
        tblField(fldPos.f15_Co_Tel).fldName = "Co_Tel"
        tblField(fldPos.f16_Office_hours).fldName = "Office_hours"
        tblField(fldPos.f17_Officer_Count).fldName = "Officer_Count"
        tblField(fldPos.f18_LastUpdate).fldName = "LastUpdate"
        tblField(fldPos.f19_UpdBy).fldName = "UpdBy"
        tblField(fldPos.f20_isPublic).fldName = "IsPublic"


    End Sub

    Sub New()
        MyBase.New()
        Me.init()

    End Sub

#Region "Property"

    Public ReadOnly Property TableName() As String
        Get
            Return strTableName
        End Get
    End Property


#End Region

End Class

Public Class LocationGroupInfo
    Inherits ApplicationBaseClass
    Private Const intFieldNum As Integer = 5 'กำหนด จำนวนฟิลด์ที่มี
    Public Enum fldPos As Integer 'ประกาศตำแหน่งของฟิลด์
        f00_Code = 0
        f01_Name = 1
        f02_Descriptions = 2
        f03_IsPublic = 3
        f04_GroupID = 4
    End Enum

    Sub init() 'ใส่ชื่อ table, field name และ ค่า default
        strTableName = "LOCATIONGROUP"
        intTotalField = intFieldNum

        ReDim tblField(intFieldNum)
        Array.Clear(tblField, 0, tblField.Length)
        tblField(fldPos.f00_Code).fldName = "Code"
        tblField(fldPos.f00_Code).fldValue = "0"
        tblField(fldPos.f01_Name).fldName = "Name"
        tblField(fldPos.f01_Name).fldValue = ""
        tblField(fldPos.f02_Descriptions).fldName = "Descriptions"
        tblField(fldPos.f02_Descriptions).fldValue = ""
        tblField(fldPos.f03_IsPublic).fldName = "IsPublic"
        tblField(fldPos.f03_IsPublic).fldValue = ""
        tblField(fldPos.f04_GroupID).fldName = "GroupID"

    End Sub

    Sub New()
        MyBase.New()
        Me.init()

    End Sub

#Region "Property"

    Public Property RoleID() As Integer
        Get
            Return tblField(fldPos.f00_Code).fldValue
        End Get
        Set(ByVal Value As Integer)
            tblField(fldPos.f00_Code).fldValue = Value
            tblField(fldPos.f00_Code).fldAffect = False
        End Set
    End Property
    Public Property RoleName() As String
        Get
            Return tblField(fldPos.f01_Name).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f01_Name).fldValue = Value
            tblField(fldPos.f01_Name).fldAffect = True
        End Set
    End Property

    Public Property Descriptions() As String
        Get
            Return tblField(fldPos.f02_Descriptions).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f02_Descriptions).fldValue = Value
            tblField(fldPos.f02_Descriptions).fldAffect = True
        End Set
    End Property

    Public Property IsPublic() As Integer
        Get
            Return tblField(fldPos.f03_IsPublic).fldValue
        End Get
        Set(ByVal Value As Integer)
            tblField(fldPos.f03_IsPublic).fldValue = Value
            tblField(fldPos.f03_IsPublic).fldAffect = True
        End Set
    End Property



    Public ReadOnly Property TableName() As String
        Get
            Return strTableName
        End Get
    End Property


#End Region

End Class
