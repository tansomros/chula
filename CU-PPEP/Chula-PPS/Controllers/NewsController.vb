Imports Microsoft.ApplicationBlocks.Data

Public Class NewsController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function GetNews() As DataTable
        SQL = "select * from News  Order By  NewsOrder,NewsDate desc"

        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function GetNews_ByID(id As Integer) As DataTable
        SQL = "select * from News where Newsid=" & id
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function News_Add(ByVal Title As String, FirstPage As Integer, ByVal NewsType As String, ByVal LinkPath As String, ByVal isPublic As Integer, ByVal ContentNews As String, orderNo As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("News_Add"), Title, FirstPage, NewsType, LinkPath, isPublic, ContentNews, orderNo)
    End Function

    Public Function News_Update(ByVal NewsID As Integer, ByVal Title As String, FirstPage As Integer, ByVal NewsType As String, ByVal LinkPath As String, ByVal isPublic As Integer, ByVal ContentNews As String, orderNo As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "News_Update", NewsID, Title, FirstPage, NewsType, LinkPath, isPublic, ContentNews, orderNo)
    End Function
    Public Function News_SetFirstPage(ByVal pID As Integer) As Integer
        SQL = "update News  set FirstPage=1 where Newsid =" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function News_Delete(ByVal pID As Integer) As Integer
        SQL = "delete from News where Newsid =" & pID
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function News_GetFirstPage() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("News_GetFirstPage"))
        Return ds.Tables(0)
    End Function
    Public Function News_GetByStatus() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("News_GetByStatus"))
        Return ds.Tables(0)
    End Function
End Class
