Imports Microsoft.ApplicationBlocks.Data
#Region "Reseach About"
Public Class ResearchAboutController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function ResearchAbout_Get(Code As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "ResearchAbout_Get", Code)
        Return ds.Tables(0)
    End Function
    Public Function ResearchNews_Get(Code As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "ResearchNews_Get", Code)
        Return ds.Tables(0)
    End Function

    Public Function ResearchAbout_Get(lang As String, page As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("ResearchAbout_GetByPage"), lang, page)
        Return ds.Tables(0)
    End Function
    Public Function ResearchAbout_Update(ByVal Code As String, ByVal configPage As String, ByVal Detail As String, MUser As Integer) As Integer
        If configPage <> "" Then
            Return SqlHelper.ExecuteNonQuery(ConnectionString, "ResearchAbout_Update", Code, configPage, Detail, MUser)
        Else
            Return 0
        End If

    End Function
    Public Function ResearchNews_Update(ByVal Code As String, ByVal Detail As String, MUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "ResearchNews_Update", Code, Detail, MUser)
    End Function

    Public Function ResearchAbout_CountRead(ByVal configPage As String, ByVal lang As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "ResearchAbout_CountRead", configPage, lang)
    End Function
    Public Function ResearchNews_CountRead(ByVal lang As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "ResearchNews_CountRead", lang)
    End Function

End Class
#End Region

Public Class ResearchController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Research_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Research_GetAll")
        Return ds.Tables(0)
    End Function
    Public Function Research_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Research_Get")
        Return ds.Tables(0)
    End Function

    Public Function Research_GetYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Research_GetYear")
        Return ds.Tables(0)
    End Function

    Public Function Research_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Research_GetByUID", UID)
        Return ds.Tables(0)
    End Function

    Public Function Research_GetByPersonID(PersonID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Research_GetByPersonID", PersonID)
        Return ds.Tables(0)
    End Function
    Public Function Research_GetBySearch(Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Research_GetBySearchAll", Search)
        Return ds.Tables(0)
    End Function

    Public Function Research_GetBySearch(Title As String, Author As String, Year As String, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Research_GetBySearch", Title, Author, Year, Search)
        Return ds.Tables(0)
    End Function

    Public Function Research_GetName(ProjID As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Research_GetName", ProjID)

        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Str(ds.Tables(0).Rows(0)(0))
        Else
            Return ""
        End If
    End Function
    Public Function Research_Save(UID As Integer, RYear As Integer, RESTYPE As String, RESGROUPUID As String, PRESENTUID As String, PersonID As Integer, TitleTH As String, TitleEN As String, StatusFlag As String, FileAbstract As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Research_Save", UID, RYear, RESTYPE, RESGROUPUID, PRESENTUID, PersonID, TitleTH, TitleEN, StatusFlag, FileAbstract)
    End Function
    Public Function Research_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Research_Delete", UID)
    End Function

End Class

