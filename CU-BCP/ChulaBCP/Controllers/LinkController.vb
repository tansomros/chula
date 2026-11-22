Imports Microsoft.ApplicationBlocks.Data

Public Class LinkController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Link_GetFirstPage(lang As String, seqno As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Link_GetFirstPage", "th", seqno)
        Return ds.Tables(0)
    End Function

End Class
Public Class VisitCounterController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Visit_Hit(SID As Integer, IP As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, "VisitHit", SID, IP)
        Return ds.Tables(0).Rows(0)(0)
    End Function
    Public Function VisitCounter_Get(d As Integer, m As Integer, y As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "VisitCounter_Get", d, m, y)
        Return ds.Tables(0)
    End Function
End Class

