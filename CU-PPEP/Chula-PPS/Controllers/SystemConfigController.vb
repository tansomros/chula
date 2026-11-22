Imports System.Runtime.ConstrainedExecution
Imports Microsoft.ApplicationBlocks.Data
Public Class SystemConfigController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function SystemConfig_Get() As DataTable
        SQL = "select * from SystemConfig order by itemID "
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function SystemConfig_GetAll(Code As String) As DataTable
        SQL = "select *  from SystemConfig where CodeConfig='" & Code & "'"
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function


    Public Function SystemConfig_GetByCode(id As String) As String
        SQL = "select ValueConfig  from SystemConfig where CodeConfig='" & id & "'"
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)(0).ToString
        Else
            Return ""
        End If
    End Function

    Public Function DataConfig_Add(code As String, pValue As String, desc As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "DataConfig_Add", code, pValue, desc)
    End Function
    Public Function DataConfig_Update(code As String, pValue As String, desc As String) As Integer
        SQL = "Update SystemConfig set ValueConfig='" & pValue & "',[Description]='" & desc & "'  Where CodeConfig='" & code & "'"
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function DataConfig_UpdateValue(code As String, pValue As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "DataConfig_UpdateValue", code, pValue)
    End Function
    Public Function Certification_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Certification_Get")
        Return ds.Tables(0)
    End Function
    Public Function Certification_GetYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Certification_GetYear")
        Return ds.Tables(0)
    End Function
    Public Function Certification_Delete(eduYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Certification_Delete", eduYear)
        Return ds.Tables(0)
    End Function

    Public Function Certification_Save(eduYear As Integer, certDate As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Certification_Save", eduYear, certDate)
    End Function

End Class
 