Imports Microsoft.ApplicationBlocks.Data

Public Class SubjectController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function Subject_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_Get")
        Return ds.Tables(0)
    End Function

    Public Function Subject_GetActive() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_GetActive")
        Return ds.Tables(0)
    End Function
    Public Function Subject_GetUnit(Code As String) As Double
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_GetUnit", Code)
        If ds.Tables(0).Rows.Count > 0 Then
            If IsDBNull(ds.Tables(0).Rows(0)(0)) Then
                Return 0
            Else
                Return ds.Tables(0).Rows(0)(0)
            End If
        Else
            Return 0
        End If

    End Function

    Public Function Subject_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_GetByUID", UID)
        Return ds.Tables(0)
    End Function
    Public Function Subject_GetByCode(Code As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_GetByCode", Code)
        Return ds.Tables(0)
    End Function

    Public Function Subject_GetBySearch(Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_GetBySearch", Search)
        Return ds.Tables(0)
    End Function

    Public Function Subject_GetNoCourse(pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_GetNoCourse", pYear)
        Return ds.Tables(0)
    End Function
    Public Function Subject_Save(ByVal SubjectID As Integer, ByVal SubjectCode As String, ByVal NameTH As String, ByVal NameEN As String, ByVal SubjectUnit As Double, SubjectType As String, ByVal StatusFlag As String, FileName As String, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Subject_Save"), SubjectID, SubjectCode, NameTH, NameEN, SubjectUnit, SubjectType, StatusFlag, FileName, UpdBy)
    End Function

    '  Public Function Subject_Save(ByVal SubjectID As Integer, ByVal SubjectCode As String, ByVal NameTH As String, ByVal NameEN As String, ByVal SubjectUnit As Double, ByVal SubjectGroup As String, ByVal UpdBy As String, ByVal StatusFlag as string, ByVal AliasName As String, ByVal LevelClass As Integer, ByVal NameCert1TH As String, ByVal NameCert2TH As String, ByVal NameCert1EN As String, ByVal NameCert2EN As String) As Integer

    '      Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Subject_Save"), SubjectCode _
    ', NameTH _
    ', NameEN _
    ', SubjectUnit _
    ', StrNull2Zero(SubjectGroup) _
    ', StatusFlag _
    ', UpdBy, AliasName, LevelClass, NameCert1TH, NameCert2TH, NameCert1EN, NameCert2EN)
    '  End Function

    Public Function Subject_Delete(ByVal pID As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Subject_Delete", pID)
    End Function
End Class