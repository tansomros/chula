Imports Microsoft.ApplicationBlocks.Data

Public Class EventController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Event_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Event_Get"))
        Return ds.Tables(0)
    End Function

    Public Function Event_Add(ByVal Title As String, FirstPage As Integer, ByVal EventType As String, ByVal LinkPath As String, ByVal isPublic As Integer, ByVal ContentEvent As String, orderNo As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Event_Add"), Title, FirstPage, EventType, LinkPath, isPublic, ContentEvent, orderNo)
    End Function

    Public Function Event_Update(ByVal EventID As Integer, ByVal Title As String, FirstPage As Integer, ByVal EventType As String, ByVal LinkPath As String, ByVal isPublic As Integer, ByVal ContentEvent As String, orderNo As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Event_Update", EventID, Title, FirstPage, EventType, LinkPath, isPublic, ContentEvent, orderNo)
    End Function



End Class
