Imports Microsoft.ApplicationBlocks.Data
Public Class RecommendController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function LocationRecommend_GetByID(id As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationRecommend_GetByID", id)
        Return ds.Tables(0)
    End Function

    Public Function LocationRecommend_Save(ByVal RID As Integer, ByVal StudentCode As String, ByVal LocationID As Integer, ByVal SubjectID As Integer, ByVal Title As String, ByVal Recommend As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("LocationRecommend_Save"), RID, StudentCode, LocationID, SubjectID, Title, Recommend)
    End Function
    Public Function LocationRecommend_UpdateByAdmin(ByVal RID As Integer, ByVal Title As String, ByVal Recommend As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("LocationRecommend_UpdateByAdmin"), RID, Title, Recommend)
    End Function

    Public Function LocationRecommend_UpdateStatus(ByVal RID As Integer, ByVal Status As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("LocationRecommend_UpdateStatus"), RID, Status)
    End Function
    Public Function LocationRecommend_Delete(ByVal RID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("LocationRecommend_Delete"), RID)
    End Function
    Public Function LocationRecommend_GetSearch(Type As String, Status As String, StudentCode As String, SubjectCode As String, LocationId As String, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationRecommend_GetSearch", Type, Status, StudentCode, SubjectCode, LocationId, Search)
        Return ds.Tables(0)
    End Function

    Public Function LocationRecommend_GetByLocation(LocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "LocationRecommend_GetByLocation", LocationID)
        Return ds.Tables(0)
    End Function
#Region "Room"
    Public Function RoomRecommend_GetByID(id As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RoomRecommend_GetByID", id)
        Return ds.Tables(0)
    End Function

    Public Function RoomRecommend_Save(ByVal RID As Integer, ByVal StudentCode As String, ByVal LocationID As Integer, ByVal Title As String, ByVal Recommend As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("RoomRecommend_Save"), RID, StudentCode, LocationID, Title, Recommend)
    End Function
    Public Function RoomRecommend_UpdateByAdmin(ByVal RID As Integer, ByVal Title As String, ByVal Recommend As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("RoomRecommend_UpdateByAdmin"), RID, Title, Recommend)
    End Function

    Public Function RoomRecommend_UpdateStatus(ByVal RID As Integer, ByVal Status As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("RoomRecommend_UpdateStatus"), RID, Status)
    End Function
    Public Function RoomRecommend_Delete(ByVal RID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("RoomRecommend_Delete"), RID)
    End Function
    Public Function RoomRecommend_GetSearch(Type As String, Status As String, StudentCode As String, LocationID As String, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RoomRecommend_GetSearch", Type, Status, StudentCode, LocationID, Search)
        Return ds.Tables(0)
    End Function
    Public Function RoomRecommend_GetByStudent(StudentCode As String, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RoomRecommend_GetByStudent", StudentCode, Search)
        Return ds.Tables(0)
    End Function
    Public Function RoomRecommend_GetByLocation(LocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RoomRecommend_GetByLocation", LocationID)
        Return ds.Tables(0)
    End Function
#End Region


End Class
