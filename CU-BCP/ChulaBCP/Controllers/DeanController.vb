Imports Microsoft.ApplicationBlocks.Data
Public Class DeanController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function DeanMessage_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "DeanMessage_Get")
        Return ds.Tables(0)
    End Function
    Public Function DeanMessage_GetLast() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "DeanMessage_GetLast")
        Return ds.Tables(0)
    End Function
    Public Function DeanMessage_GetList() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "DeanMessage_GetList")
        Return ds.Tables(0)
    End Function
    Public Function DeanMessage_GetByID(MsgID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "DeanMessage_GetByID", MsgID)
        Return ds.Tables(0)
    End Function

    Public Function DeanMessage_Save(ByVal lang As String, ByVal MsgID As Integer _
           , ByVal MsgHead As String _
           , ByVal MsgTitle As String _
           , ByVal MsgDetail As String _
           , ByVal IsActive As Integer _
           , ByVal CUser As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "DeanMessage_Save", lang, MsgID, MsgHead, MsgTitle, MsgDetail, IsActive, CUser)

    End Function


End Class
