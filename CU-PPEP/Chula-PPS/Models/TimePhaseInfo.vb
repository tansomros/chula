Public Class TimePhaseInfo
    Inherits ApplicationBaseClass
    Private Const intFieldNum As Integer = 5 'กำหนด จำนวนฟิลด์ที่มี
    Public Enum fldPos As Integer 'ประกาศตำแหน่งของฟิลด์
        f00_Code = 0
        f01_Name = 1
        f02_Descriptions = 2
        f03_IsPublic = 3
        f04_Year = 4
    End Enum

    Sub init() 'ใส่ชื่อ table, field name และ ค่า default
        strTableName = "TimePhase"
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
        tblField(fldPos.f04_Year).fldName = "EduYear"

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
