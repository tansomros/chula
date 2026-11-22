Imports Microsoft.ApplicationBlocks.Data

Public Class DocumentController
    Inherits ApplicationBaseClass
    Public ds As New DataSet
    Public Function Document_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Document_Get"))
        Return ds.Tables(0)
    End Function

    Public Function Document_GetByUID(ByVal pID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Document_GetByUID"), pID)
        Return ds.Tables(0)
    End Function

    Public Function Document_Save(ByVal UID As Integer, DocumentCode As String, DocumentName As String, SubjectTitle As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Document_Save", UID, DocumentCode, DocumentName, SubjectTitle)

    End Function
    Public Function tmpDocumentPrint_Get(pUid As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("tmpDocumentPrint_Get"), pUid)
        Return ds.Tables(0)
    End Function
    Public Function DocumentPrint_Add(DocumentUID As Integer, EduYear As Integer, DocumentDate As String, StartNumber As Integer, LevelClase As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "DocumentPrint_Add", DocumentUID, EduYear, DocumentDate, StartNumber, LevelClase, CUser)

    End Function
    Public Function DocumentPrint_SendTeacher_Add(DocumentUID As Integer, EduYear As Integer, DocumentDate As String, StartNumber As Integer, LevelClase As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "DocumentPrint_SendTeacher_Add", DocumentUID, EduYear, DocumentDate, StartNumber, LevelClase, CUser)

    End Function
    Public Function DocumentTemplate_GetByUID(pYear As Integer, ByVal pID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("DocumentTemplate_GetByUID"), pYear, pID)
        Return ds.Tables(0)
    End Function

    Public Function DocumentPrint_Get(ByVal DocumentUID As Integer, EduYear As Integer, pSearch As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "DocumentPrint_GetSearch", DocumentUID, EduYear, pSearch)

        Return ds.Tables(0)
    End Function

    Public Function Document_SavePath(path As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Document_SavePath", path)
    End Function

    Public Function Document_GetPath() As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Document_GetPath")
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Str(ds.Tables(0).Rows(0)(0))
        Else
            Return ""
        End If

    End Function

End Class