Imports Microsoft.ApplicationBlocks.Data
Public Class SlideController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function SlideShow_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "SlideShow_Get")
        Return ds.Tables(0)
    End Function
    Public Function SlideShow_GetByID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "SlideShow_GetByID", UID)
        Return ds.Tables(0)
    End Function

    Public Function SlideShow_GetFirstPage() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "SlideShow_GetFirstPage")
        Return ds.Tables(0)
    End Function
    Public Function SlideShow_ActiveSlide(pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "SlideShow_ActiveSlide", pID)
    End Function
    Public Function SlideShow_InActiveSlide(pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "SlideShow_InActiveSlide", pID)
    End Function

    Public Function SlideShow_Delete(pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "SlideShow_Delete", pID)
    End Function
    Public Function SlideShow_Add(ByVal img_path As String,
        ByVal title As String,
        ByVal desc As String,
        ByVal StatusFlag As String,
        ByVal ImgStatus As String,
        ByVal url As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "SlideShow_Add", img_path, title, desc, StatusFlag, ImgStatus, url)
    End Function
    Public Function SlideShow_Update(ByVal UID As String, ByVal title As String, ByVal url As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "SlideShow_Update", UID, title, url)
    End Function

End Class
