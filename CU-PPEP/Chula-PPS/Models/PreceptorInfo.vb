Imports System
Imports System.Configuration
Imports System.Data


Public Class PreceptorInfo
    Inherits ApplicationBaseClass
    Private Const intFieldNum As Integer = 5 'กำหนด จำนวนฟิลด์ที่มี
    Public Enum fldPos As Integer 'ประกาศตำแหน่งของฟิลด์
        f00_Preceptor_ID = 0
        f01_FirstName = 2
        f02_LastName = 3
        f03_LocationID = 4
    End Enum

    Sub init() 'ใส่ชื่อ table, field name และ ค่า default
        strTableName = "PRECEPTORS"
        intTotalField = intFieldNum

        ReDim tblField(intFieldNum)
        Array.Clear(tblField, 0, tblField.Length)
        tblField(fldPos.f00_Preceptor_ID).fldName = "Preceptor_ID"
        tblField(fldPos.f00_Preceptor_ID).fldValue = 0
        tblField(fldPos.f01_FirstName).fldName = "FirstName"
        tblField(fldPos.f02_LastName).fldName = "LastName"
        tblField(fldPos.f03_LocationID).fldName = "LocationID"

    End Sub

    Sub New()
        MyBase.New()
        Me.init()

    End Sub

#Region "Property"

    Public Property Preceptor_ID() As Integer
        Get
            Return tblField(fldPos.f00_Preceptor_ID).fldValue
        End Get
        Set(ByVal Value As Integer)
            tblField(fldPos.f00_Preceptor_ID).fldValue = Value
            tblField(fldPos.f00_Preceptor_ID).fldAffect = False
        End Set
    End Property
    Public Property FirstName() As String
        Get
            Return tblField(fldPos.f01_FirstName).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f01_FirstName).fldValue = Value
            tblField(fldPos.f01_FirstName).fldAffect = True
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return tblField(fldPos.f02_LastName).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(fldPos.f02_LastName).fldValue = Value
            tblField(fldPos.f02_LastName).fldAffect = True
        End Set
    End Property

    Public Property LocationID() As Integer
        Get
            Return tblField(fldPos.f03_LocationID).fldValue
        End Get
        Set(ByVal Value As Integer)
            tblField(fldPos.f03_LocationID).fldValue = Value
            tblField(fldPos.f03_LocationID).fldAffect = True
        End Set
    End Property

     


    Public ReadOnly Property TableName() As String
        Get
            Return strTableName
        End Get
    End Property


#End Region

End Class

