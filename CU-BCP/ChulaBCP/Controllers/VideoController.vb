Imports Microsoft.ApplicationBlocks.Data

Public Class VideoController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Video_GetFirstPage() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Video_GetFirstPage"))
        Return ds.Tables(0)
    End Function
    Public Function Video_GetList() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Video_GetList"))
        Return ds.Tables(0)
    End Function


    Public Function Knowledge_Save(ByVal ServiceOrderID As Integer _
              , ByVal ServiceTypeID As String, ByVal KnowledgeCode As String, ByVal KnowledgeValue As Integer, ByVal KnowledgeDescription As String, ByVal KNTYPEID As Integer, ByVal UpdBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Knowledge_Save", ServiceOrderID, ServiceTypeID, KnowledgeCode, KnowledgeValue, KnowledgeDescription, KNTYPEID, UpdBy)

    End Function

    Public Function Knowledge_GetByID(SID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Knowledge_GetByID"), SID)
        Return ds.Tables(0)
    End Function

    Public Function Knowledge_Delete(ByVal pID As Integer, tID As String) As Integer
        SQL = "delete from Knowledge  where ServiceOrderID =" & pID & " AND ServiceTypeID='" & tID & "'"
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

End Class
