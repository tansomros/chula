Imports Microsoft.ApplicationBlocks.Data

Public Class KPIController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function QA_Year_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "QA_Year_Get")
        Return ds.Tables(0)
    End Function
    Public Function KPI_Item_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "KPI_Item_Get")
        Return ds.Tables(0)
    End Function
    Public Function KPI_Users_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "KPI_Users_Get")
        Return ds.Tables(0)
    End Function
    Public Function KPI_Users_GetByUserID(UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "KPI_Users_GetByUserID", UserID)
        Return ds.Tables(0)
    End Function
    Public Function KPISetFiles_Get(KPIYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "KPISetFiles_Get", KPIYear)
        Return ds.Tables(0)
    End Function
    Public Function KPISetLog_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "KPISetLog_Get")
        Return ds.Tables(0)
    End Function
    Public Function KPISet_Update(ByVal kpi_id As Integer, ByVal kpi_year As Integer, ByVal kpi_no As String, ByVal med As String, ByVal menv As String, ByVal kpi_avg As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "KPISet_Update", kpi_id, kpi_year, kpi_no, med, menv, kpi_avg)
    End Function



End Class