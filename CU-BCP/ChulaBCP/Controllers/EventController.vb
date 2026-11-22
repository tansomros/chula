Imports Microsoft.ApplicationBlocks.Data
Public Class EventController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function Events_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("Events_GetAll"))
        Return ds.Tables(0)
    End Function
    Public Function Events_GetBySearch(CATUID As Integer, StartDate As String, EndDate As String, pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("Events_GetBySearch"), CATUID, StartDate, EndDate, pKey)
        Return ds.Tables(0)
    End Function

    Public Function Events_GetByLocation(LID As String, pKey As String, Optional isLocation As Integer = 0) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("Events_GetByLocation"), LID, pKey, isLocation)
        Return ds.Tables(0)
    End Function
    Public Function Events_GetByID(PID As Long) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("Events_GetByUID"), PID)
        Return ds.Tables(0)
    End Function
    Public Function Events_GetByCategoryUID(CID As Long) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("Events_GetByCategoryUID"), CID)
        Return ds.Tables(0)
    End Function

    Public Function Events_GetByDate(pDate As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("Events_GetByDate"), pDate)
        Return ds.Tables(0)
    End Function

    Public Function EventCategory_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("EventCategory_GetAll"))
        Return ds.Tables(0)
    End Function

    Public Function Events_Save(ByVal UID As Integer _
       , ByVal EventCategoryUID As Integer _
       , ByVal StartDate As String _
       , ByVal EndDate As String _
       , ByVal StartTime As String _
       , ByVal EndTime As String _
       , ByVal EventName As String _
       , ByVal Venue As String _
       , ByVal Organizer As String _
       , ByVal EventDetail As String) As Integer


        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Events_Save", UID, EventName, StartDate, EndDate, StartTime, EndTime, Venue, Organizer, EventCategoryUID, EventDetail)

    End Function

    Public Function Events_Delete(ByVal UID As Integer) As Boolean
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Events_Delete", UID)
    End Function

End Class
