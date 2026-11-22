Imports Microsoft.ApplicationBlocks.Data
Public Class MasterController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function GetEduYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "GetEduYear")
        Return ds.Tables(0)
    End Function


#Region "Province"


    Public Function LoadProvince() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Province_Get")
        Return ds.Tables(0)

    End Function
    Public Function Province_GetActive() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Province_GetActive")
        Return ds.Tables(0)

    End Function

    Public Function Province_GetByID(pid As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Province_GetByID", pid)
        Return ds.Tables(0)

    End Function
    Public Function Province_GetFromAssessment(pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Province_GetFromAssessment", pYear)
        Return ds.Tables(0)
    End Function
    Public Function Province_GetFromAssessment(pYear As Integer, CID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Province_GetFromCourseAssessment", pYear, CID)
        Return ds.Tables(0)
    End Function

    Public Function Province_Add(pName As String)
        SQL = ""
        SQL = "Insert into Province(ProvinceName) values ('" & pName & "') "
        ExecuteDataQuery(SQL)
    End Function

    Public Function Province_Update(pid As Integer, pName As String)
        SQL = ""
        SQL = "Update Province set ProvinceName='" & pName & "' where provinceID=" & pid
        ExecuteDataQuery(SQL)
    End Function

    Public Function Province_Delete(pid As Integer)
        SQL = ""
        SQL = "delete from  Province where provinceID=" & pid
        ExecuteDataQuery(SQL)
    End Function

#End Region
#Region "Prefix"

    Public Function Prefix_GetByID(pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Prefix_GetByID", pUID)
        Return ds.Tables(0)
    End Function
    Public Function Prefix_GetBySearch(pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Prefix_GetByID", pUID)
        Return ds.Tables(0)
    End Function


    Public Function Prefix_Save(ByVal PrefixID As Integer,
           ByVal PrefixName As String,
           ByVal isStudent As String,
           ByVal isTeacher As String,
           ByVal isPreceptor As String,
           ByVal isActive As String,
           ByVal CUser As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Prefix_Save", PrefixID, PrefixName, isStudent, isTeacher, isPreceptor, isActive, CUser)

    End Function
    Public Function Prefix_Delete(ByVal UID As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Prefix_Delete", UID)

    End Function
    Public Function Prefix_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Prefix_Get")
        Return ds.Tables(0)
    End Function
    Public Function Prefix_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Prefix_Get")
        Return ds.Tables(0)
    End Function


    Public Function Prefix_CheckDuplicate(name As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Prefix_CheckDuplicate", name)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) = 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

#End Region
#Region "Prefix Position"
    Public Function PrefixPosition_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PrefixPosition_Get")
        Return ds.Tables(0)
    End Function
    Public Function PrefixPosition_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "PrefixPosition_GetAll")
        Return ds.Tables(0)
    End Function
    Public Function PrefixPosition_Add(
            ByVal MTMUID As Integer, ServiceTypeID As String _
          , ByVal DeseaseUID As Integer _
          , ByVal DeseaseOther As String _
          , ByVal CreateBy As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PrefixPosition_Add", MTMUID, ServiceTypeID, DeseaseUID, DeseaseOther, CreateBy)

    End Function
    Public Function PrefixPosition_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PrefixPosition_Delete", UID)
    End Function

#End Region
#Region "Position"
    Public Function Position_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Positions_Get")
        Return ds.Tables(0)
    End Function
    Public Function Position_GetActive() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Positions_GetActive")
        Return ds.Tables(0)
    End Function

    Public Function Position_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Positions_GetAll")
        Return ds.Tables(0)
    End Function
    Public Function Position_GetSearch(Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Position_GetSearch", Search)
        Return ds.Tables(0)
    End Function
    Public Function Position_GetByGroup(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Positions_GetByGroup", UID)
        Return ds.Tables(0)
    End Function
    Public Function Position_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Positions_GetByUID", UID)
        Return ds.Tables(0)
    End Function
    Public Function Position_CheckDuplicate(pName As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Positions_CheckDuplicate", pName)
        If String.Concat(ds.Tables(0).Rows(0)(0)) <> "0" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function Position_Save(pid As Integer, Name As String, StatusFlag As String)
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Positions_Save", pid, Name, StatusFlag)
    End Function


    Public Function Position_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Positions_Delete", UID)
    End Function

#End Region

#Region "Major"
    Public Function Major_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Major_GetAll")
        Return ds.Tables(0)

    End Function
    Public Function Major_GetActive() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Major_GetActive")
        Return ds.Tables(0)
    End Function
    Public Function Major_GetSearch(pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Major_GetSearch", pSearch)
        Return ds.Tables(0)
    End Function

    'Public Function GetMinor_ByMajorID(id As Integer) As DataTable
    '    ds = SqlHelper.ExecuteDataset(ConnectionString, "Minor_GetByMajor", id)
    '    Return ds.Tables(0)
    'End Function


    'Public Function GetSubMinor_ByMinorID(id As Integer) As DataTable

    '    ds = SqlHelper.ExecuteDataset(ConnectionString, "SubMinor_GetByMinor", id)
    '    Return ds.Tables(0)
    'End Function

    Public Function Major_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Major_Get")
        Return ds.Tables(0)
    End Function

    Public Function Major_CheckDuplicate(pName As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Major_CheckDuplicate", pName)
        If String.Concat(ds.Tables(0).Rows(0)(0)) <> "0" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Major_GetByID(pid As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Major_GetByID", pid)

        Return ds.Tables(0)

    End Function

    Public Function Major_Add(NameTH As String, NameEN As String, StatusFlag As String)
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Major_Add", NameTH, NameEN, StatusFlag)
    End Function

    Public Function Major_Update(pid As Integer, NameTH As String, NameEN As String, StatusFlag As String)
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Major_Update", pid, NameTH, NameEN, StatusFlag)
    End Function

    Public Function Major_Delete(pid As Integer)
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Major_Delete", pid)
    End Function

#End Region
#Region "Department"
    Public Function Department_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Department_Get")
        Return ds.Tables(0)
    End Function
    Public Function Department_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Department_GetAll")
        Return ds.Tables(0)

    End Function
    Public Function Department_GetActive() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Department_GetActive")
        Return ds.Tables(0)

    End Function
    Public Function Department_GetSearch(pSearch As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Department_GetSearch", pSearch)

        Return ds.Tables(0)


    End Function
    Public Function Department_CheckDuplicate(pName As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Department_CheckDuplicate", pName)
        If String.Concat(ds.Tables(0).Rows(0)(0)) <> "0" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Department_GetByID(pid As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Department_GetByID", pid)

        Return ds.Tables(0)

    End Function

    Public Function Department_Add(Name As String, StatusFlag As String)
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Department_Add", Name, StatusFlag)
    End Function

    Public Function Department_Update(pid As Integer, Name As String, StatusFlag As String)
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Department_Update", pid, Name, StatusFlag)
    End Function

    Public Function Department_Delete(pid As Integer)
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Department_Delete", pid)
    End Function
#End Region
#Region "Payor"
    Public Function Payor_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Payor_Get")
        Return ds.Tables(0)
    End Function
#End Region

#Region "Bank"

    Public Function Bank_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Bank_Get")
        Return ds.Tables(0)
    End Function

    Public Function Bank_GetByID(pid As String) As DataTable

        Dim dt As New DataTable

        SQL = ""
        SQL = "select * from  Banks  where  BankID='" & pid & "'"
        dt = ExecuteDataTable(SQL)

        Return dt

        dt = Nothing

    End Function


    Public Sub Bank_Add(pcode As String, pName As String)
        SQL = ""
        SQL = "Insert into Banks(BankID,BankName) values ('" & pcode & "','" & pName & "') "
        ExecuteDataQuery(SQL)
    End Sub

    Public Sub Bank_Update(pid As String, pidnew As String, pName As String)
        SQL = ""
        SQL = "Update Banks set BankID='" & pidnew & "' , BankName='" & pName & "' where BankID='" & pid & "'"
        ExecuteDataQuery(SQL)
    End Sub

    Public Sub Bank_Delete(pid As String)
        SQL = ""
        SQL = "delete from  Banks where BankID='" & pid & "'"
        ExecuteDataQuery(SQL)
    End Sub
#End Region
End Class
