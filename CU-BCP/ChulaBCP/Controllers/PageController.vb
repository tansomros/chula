Imports Microsoft.ApplicationBlocks.Data
Public Class PageController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

#Region "Page ID"

#End Region
#Region "Page Content"
    Public Function PageContent_Get(PageID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("PageContent_Get"), PageID)
        Return ds.Tables(0)
    End Function
    'Public Function PageContent_Get(pageID As String) As DataTable
    '    ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("PageContent_GetByPage"), pageID)
    '    Return ds.Tables(0)
    'End Function
    Public Function PageContent_Update(ByVal pageID As String, ByVal Detail As String, MUser As Integer) As Integer
        If pageID <> "" Then
            Return SqlHelper.ExecuteNonQuery(ConnectionString, "PageContent_Update", pageID, Detail, MUser)
        Else
            Return 0
        End If

    End Function

    Public Function PageContent_Counting(PageID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "PageContent_Counting", PageID)
    End Function
#End Region

End Class
