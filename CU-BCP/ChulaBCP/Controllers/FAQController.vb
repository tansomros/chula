Imports Microsoft.ApplicationBlocks.Data
Public Class FAQController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

#Region "Category"
    Public Function FAQ_Category_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("FAQ_Category_Get"))
        Return ds.Tables(0)
    End Function
    Public Function FAQ_Category_GetBySearch(Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("FAQ_Category_GetBySearch"), Search)
        Return ds.Tables(0)
    End Function
    Public Function FAQ_Category_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("FAQ_Category_GetByUID"), UID)
        Return ds.Tables(0)
    End Function

    Public Function FAQ_Category_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("FAQ_Category_Delete"), UID)
    End Function

    Public Function FAQ_Category_Save(UID As Integer, Name As String, Sort As Integer, StatusFlag As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("FAQ_Category_Save"), UID, Name, Sort, StatusFlag)
    End Function

#End Region

#Region "FAQ"
    Public Function FAQ_GetByCategory(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("FAQ_GetByCategory"), UID)
        Return ds.Tables(0)
    End Function

    Public Function FAQ_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("FAQ_GetByUID"), UID)
        Return ds.Tables(0)
    End Function

    Public Function FAQ_GetBySearch(Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("FAQ_GetBySearch"), Search)
        Return ds.Tables(0)
    End Function
    Public Function FAQ_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("FAQ_Delete"), UID)
    End Function

    Public Function FAQ_Save(UID As Integer, CateUID As Integer, Q As String, A As String, StatusFlag As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("FAQ_Save"), UID, CateUID, Q, A, StatusFlag)
    End Function

#End Region

    Public Function FAQ_Mail_GetBySearch(Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("FAQ_Mail_GetBySearch"), Search)
        Return ds.Tables(0)
    End Function
    Public Function FAQ_Mail_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("FAQ_Mail_Delete"), UID)
    End Function

    Public Function FAQ_Mail_Save(CateUID As Integer, Name As String, Mail As String, Subject As String, Msg As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("FAQ_Mail_Save"), CateUID, Name, Mail, Subject, Msg)
    End Function

End Class
