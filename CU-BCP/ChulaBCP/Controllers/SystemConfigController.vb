Imports Microsoft.ApplicationBlocks.Data
Public Class SystemConfigController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function SystemConfig_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "SystemConfig_Get")
        Return ds.Tables(0)
    End Function
    Public Function SystemConfig_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "SystemConfig_GetByUID", UID)
        Return ds.Tables(0)
    End Function

    Public Function SystemConfig_GetByCode(Code As String) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, "SystemConfig_GetByCode", Code)
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
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "DataConfig_Update", code, pValue, desc)
    End Function


    Public Function ControlPanel_Get(UserRoleID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("ControlPanel_Get"), UserRoleID)
        Return ds.Tables(0)
    End Function

End Class
 