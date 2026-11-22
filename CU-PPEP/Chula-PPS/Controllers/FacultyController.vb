Imports Microsoft.ApplicationBlocks.Data
Public Class FacultyController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function GetMajor() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Major_Get")
        Return ds.Tables(0)
    End Function
    Public Function Major_GetForSearch() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Major_GetForSearch")
        Return ds.Tables(0)
    End Function
    Public Function GetMinor_ByMajorID(id As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Minor_GetByMajor", id)
        Return ds.Tables(0)
    End Function

    'Public Function GetSubMinor_ByMinorID(id As Integer) As DataTable
    '    ds = SqlHelper.ExecuteDataset(ConnectionString, "SubMinor_GetByMinor", id)
    '    Return ds.Tables(0)
    'End Function

End Class

Public Class DepartmentController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
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

End Class

