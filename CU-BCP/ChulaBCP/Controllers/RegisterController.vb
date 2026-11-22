Imports Microsoft.ApplicationBlocks.Data

Public Class RegisterController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Register_GetYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Register_GetYear")
        Return ds.Tables(0)
    End Function
    Public Function Register_GetRegisterID(TermNo As Integer, EduYear As Integer, StudentID As Integer, RegDate As String, TopicUID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Register_GetRegisterID"), TermNo, EduYear, StudentID, RegDate, TopicUID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function


    Public Function GetRegister_BySearch(pYear As Integer, SubjectCode As String, pkey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Register_GetBySearch", pYear, SubjectCode, pkey)
        Return ds.Tables(0)
    End Function

    Public Function Register_GetByID(RegID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Register_GetByID", RegID)
        Return ds.Tables(0)
    End Function
    Public Function Register_GetByStudent(EduYear As Integer, StudentID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Register_GetByStudent", EduYear, StudentID)
        Return ds.Tables(0)
    End Function
    Public Function Register_GetByYear(EduYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Register_GetByYear", EduYear)
        Return ds.Tables(0)
    End Function
    Public Function Register_GetBySearch(EduYear As Integer, TopicUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Register_GetBySearch", EduYear, TopicUID)
        Return ds.Tables(0)
    End Function
    Public Function Register_Delete(RegID As Integer, ByVal pID As Integer, TopicID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Register_Delete", RegID, pID, TopicID)
    End Function
    Public Function Register_GetStudentBySex(RegYear As String, LocationID As Integer, Phase As Integer, DegreeNo As Integer, sSex As String, SubjectCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Register_GetStudentBySex"), RegYear, LocationID, Phase, DegreeNo, sSex, SubjectCode)

        Return ds.Tables(0)

    End Function

    Public Function Register_CheckDup(RegYear As String, stdCode As String, Phase As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Register_CheckDup"), RegYear, stdCode, Phase)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function
    Public Function Register_CheckAssessment(RID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Register_CheckAssessment"), RID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function

    Public Function Register_GetStudentNoAssessment(RegYear As Integer, SubjCode As String, Optional sKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Register_NotAssessment"), RegYear, SubjCode, sKey)
        Return ds.Tables(0)

    End Function
    Public Function Register_GetStudentNoRegister(RegYear As Integer, SubjCode As String, CID As Integer, Optional sKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Register_NotRegister"), RegYear, SubjCode, CID, sKey)
        Return ds.Tables(0)

    End Function

    Public Function Register_Save(UID As Integer, TermNo As Integer, EduYear As Integer, StudentID As Integer, RegDate As String, TopicUID As Integer, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Register_Save"), UID, TermNo, EduYear, StudentID, RegDate, TopicUID, UpdBy)
    End Function
    Public Function Register_ConfirmStatus(UID As Integer, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Register_ConfirmStatus"), UID,UpdBy)
    End Function

    Public Function Register_Update(ByVal pID_old As Integer, ByVal pID_new As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Register_Update", pID_old, pID_new, pName, desc, pStatus)
    End Function

    Public Function Register_UpdatePriority(ByVal ItemID As Integer, ByVal flage As String, updBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Register_UpdatePriority", ItemID, flage, updBy)
    End Function

    Public Function Register_UpdatePriorityItem(ByVal pID As Integer, ByVal pDegree As Integer, updBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Register_UpdatePriorityItem", pID, pDegree, updBy)

    End Function

#Region "Register Subject"
    Public Function RegisterSubject_Get(RegID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RegisterSubject_Get", RegID)
        Return ds.Tables(0)
    End Function

    Public Function RegisterSubject_GetCount(RegisID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RegisterSubject_GetCount"), RegisID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function

    Public Function RegisterSubjectTemplate_Clone(RegisterUID As Integer, TopicUID As Integer, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "RegisterSubjectTemplate_Clone", RegisterUID, TopicUID, CUser)
    End Function
    Public Function RegisterSubjectMaintain_Get(TopicUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RegisterSubjectMaintain_Get", TopicUID)
        Return ds.Tables(0)
    End Function

    Public Function RegisterSubject_Add(ByVal RegID As Integer, SubjectID As Integer, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "RegisterSubject_Add", RegID, SubjectID, CUser)
    End Function

    Public Function RegisterSubject_Delete(ByVal pID As Integer, RegID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "RegisterSubject_Delete", pID, RegID)
    End Function

    Public Function RegisterDocument_Get(ByVal RegID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RegisterDocument_Get", RegID)
        Return ds.Tables(0)
    End Function
    Public Function RegisterDocument_Add(ByVal RegID As Integer, DocumentUID As String, DocName As String, FilePath As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "RegisterDocument_Add", RegID, DocumentUID, DocName, FilePath, CUser)
    End Function

    Public Function RegisterDocument_Delete(ByVal pID As Integer, RegID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "RegisterDocument_Delete", pID, RegID)
    End Function
#End Region

    Public Function RegisterTopic_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RegisterTopic_Get")
        Return ds.Tables(0)
    End Function
    Public Function RegisterTopic_GetName(TopicID As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RegisterTopic_GetName"), TopicID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return String.Concat(ds.Tables(0).Rows(0)(0))
        Else
            Return ""
        End If

    End Function
End Class
