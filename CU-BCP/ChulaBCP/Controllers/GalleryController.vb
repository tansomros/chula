Imports Microsoft.ApplicationBlocks.Data

Public Class GalleryController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Gallery_GetFirstPage(lang As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Gallery_GetFirstPage", lang)
        Return ds.Tables(0)
    End Function
    Public Function Gallery_GetBySearch(pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Gallery_GetBySearch", pSearch)
        Return ds.Tables(0)
    End Function
    Public Function Gallery_GetByAlbumID(PID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Gallery_GetByAlbumID", PID)
        Return ds.Tables(0)
    End Function
    Public Function Gallery_GetMaxAlbumID() As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Gallery_GetMaxAlbumID")
        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
    End Function

    Public Function Gallery_GetByPageIndex(lang As String, pIndex As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Gallery_GetByPageIndex", lang, pIndex)
        Return ds.Tables(0)
    End Function
    Public Function Gallery_GetPage(lang As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Gallery_GetPage", lang)
        Return ds.Tables(0)
    End Function

    Public Function Gallery_Delete(pid As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Gallery_Delete", pid)

    End Function
    Public Function Gallery_Save(ByVal AlbumID As Integer _
           , ByVal AlbumName_th As String _
           , ByVal AlbumTitle_th As String _
           , ByVal AlbumName_en As String _
           , ByVal AlbumTitle_en As String _
           , ByVal IsActive As Integer _
           , ByVal ReadCount As Integer _
           , ByVal IsActiveIcon As String _
           , ByVal imgCover As String _
           , ByVal isTH As String _
           , ByVal isEN As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Gallery_Save", AlbumID _
           , AlbumName_th _
           , AlbumTitle_th _
           , AlbumName_en _
           , AlbumTitle_en _
           , IsActive _
           , ReadCount _
           , IsActiveIcon _
           , imgCover, isTH, isEN)

    End Function

#Region "VDO"
    Public Function Video_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Video_Get")
        Return ds.Tables(0)
    End Function
    Public Function Video_Delete(pid As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Video_Delete", pid)

    End Function
    Public Function Video_Save(ByVal vdoCode As String _
           , ByVal vdoName As String _
           , ByVal vdoIndex As Integer _
           , ByVal vodIndexImg As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Video_Save", vdoCode, vdoName, vdoIndex, vodIndexImg)

    End Function


#End Region
End Class
