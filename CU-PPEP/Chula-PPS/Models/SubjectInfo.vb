Imports System
Imports System.Configuration
Imports System.Data

Public Class SubjectInfo
    Inherits ApplicationBaseClass
    Private Const intFieldNum As Integer = 21 'กำหนด จำนวนฟิลด์ที่มี
    Public Enum fldPos As Integer 'ประกาศตำแหน่งของฟิลด์
        f00_SubjectID = 0
        f01_SubjectCode = 1
        f02_NameTH = 2
        f03_NameEN = 3
        f04_SubjectUnit = 4
        f05_MajorID = 5
        f06_LastUpdate = 6
        f07_UpdBy = 7
        f08_isPublic = 8


    End Enum

    Sub init() 'ใส่ชื่อ tablef_ field name และ ค่า default
        strTableName = "Subjects"
        intTotalField = intFieldNum

        ReDim tblField(intFieldNum)
        Array.Clear(tblField, 0, tblField.Length)
        tblField(fldPos.f00_SubjectID).fldName = "itemID"
        tblField(fldPos.f00_SubjectID).fldValue = "0"
        tblField(fldPos.f01_SubjectCode).fldName = "SubjectCode"
        tblField(fldPos.f01_SubjectCode).fldValue = ""
        tblField(fldPos.f02_NameTH).fldName = "NameTH"
        tblField(fldPos.f02_NameTH).fldValue = ""
        tblField(fldPos.f03_NameEN).fldName = "NameEN"
        tblField(fldPos.f04_SubjectUnit).fldName = "SubjectUnit"
        tblField(fldPos.f05_MajorID).fldName = "MajorID"
        tblField(fldPos.f06_LastUpdate).fldName = "LastUpdate"
        tblField(fldPos.f07_UpdBy).fldName = "UpdBy"
        tblField(fldPos.f08_isPublic).fldName = "IsPublic"


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
