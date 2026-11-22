Imports System
Imports System.Configuration
Imports System.Data


Public Class RoleInfo
    Inherits ApplicationBaseClass
    Private Const intFieldNum As Integer = 4 'กำหนด จำนวนฟิลด์ที่มี
    Public Enum fldPos As Integer 'ประกาศตำแหน่งของฟิลด์
        f00_RoleID = 0
        f01_RoleName = 1
        f02_Descriptions = 2
        f03_IsPublic = 3
    End Enum

    Sub init() 'ใส่ชื่อ table, field name และ ค่า default
        strTableName = "ROLES"
        intTotalField = intFieldNum

        ReDim tblField(intFieldNum)
        Array.Clear(tblField, 0, tblField.Length)
        tblField(fldPos.f00_RoleID).fldName = "RoleID"
        tblField(fldPos.f00_RoleID).fldValue = "0"
        tblField(fldPos.f01_RoleName).fldName = "RoleName"
        tblField(fldPos.f01_RoleName).fldValue = ""
        tblField(fldPos.f02_Descriptions).fldName = "Descriptions"
        tblField(fldPos.f02_Descriptions).fldValue = ""
        tblField(fldPos.f03_IsPublic).fldName = "IsPublic"
        tblField(fldPos.f03_IsPublic).fldValue = ""

    End Sub

    Sub New()
        MyBase.New()
        Me.init()

    End Sub

#Region "Property"

    Public Property RoleID() As Integer
        Get
            Return tblField(fldPos.f00_RoleID).fldValue
        End Get
        Set(ByVal Value As Integer)
            tblField(fldPos.f00_RoleID).fldValue = Value
            tblField(fldPos.f00_RoleID).fldAffect = False
        End Set
    End Property
    Public Property RoleName() As String
        Get
            Return tblField(fldPos.f01_RoleName).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f01_RoleName).fldValue = Value
            tblField(fldPos.f01_RoleName).fldAffect = True
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

