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
    Public Function Subject_GetForSearch() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_GetForSearch")
        Return ds.Tables(0)
    End Function
    Public Function Subject_GetFromRecommend() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_GetFromRecommend")
        Return ds.Tables(0)
    End Function


    Public Function Subject_GetByCode(Code As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_GetByCode", Code)
        Return ds.Tables(0)
    End Function

    Public Function Subject_Duplicate(Code As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_CheckDuplicate", Code)
        If ds.Tables(0).Rows.Count > 0 Then
            If DBNull2Zero(ds.Tables(0).Rows(0)(0)) > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function
    Public Function Subject_GetBySearch(Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_GetBySearch", Search)
        Return ds.Tables(0)
    End Function

    Public Function Subject_GetNoCourse(pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Subject_GetNoCourse", pYear)
        Return ds.Tables(0)
    End Function

    Public Function Subject_Add(ByVal SubjectCode As String, ByVal NameTH As String, ByVal NameEN As String, ByVal SubjectUnit As Double, ByVal SubjectGroup As String, ByVal UpdBy As String, ByVal isPublic As Integer, ByVal AliasName As String, ByVal LevelClass As Integer, ByVal NameCert1TH As String, ByVal NameCert2TH As String, ByVal NameCert1EN As String, ByVal NameCert2EN As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Subject_Add"), SubjectCode _
  , NameTH _
  , NameEN _
  , SubjectUnit _
  , StrNull2Zero(SubjectGroup) _
  , isPublic _
  , UpdBy, AliasName, LevelClass, NameCert1TH, NameCert2TH, NameCert1EN, NameCert2EN)
    End Function

    Public Function Subject_Update(ByVal SubjectCode_Old As String, ByVal SubjectCode_New As String, ByVal NameTH As String, ByVal NameEN As String, ByVal SubjectUnit As Double, ByVal SubjectGroup As String, ByVal UpdBy As String, ByVal isPublic As Integer, ByVal AliasName As String, ByVal LevelClass As Integer, ByVal NameCert1TH As String, ByVal NameCert2TH As String, ByVal NameCert1EN As String, ByVal NameCert2EN As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Subject_Update", SubjectCode_Old, SubjectCode_New _
  , NameTH _
  , NameEN _
  , SubjectUnit _
  , StrNull2Zero(SubjectGroup) _
  , isPublic _
  , UpdBy, AliasName, LevelClass, NameCert1TH, NameCert2TH, NameCert1EN, NameCert2EN)

    End Function

    Public Function Subject_Delete(ByVal pID As String) As Integer
        SQL = "delete from Subjects where SubjectCode ='" & pID & "'"
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function
End Class