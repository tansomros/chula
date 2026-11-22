Imports Microsoft.ApplicationBlocks.Data

Public Class RequestController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet
    Public Function Request_GetYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Request_GetYear")
        Return ds.Tables(0)
    End Function
    Public Function Request_GetRequestID(StudentID As Integer, ReqDate As String, TopicUID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Request_GetRequestID"), StudentID, ReqDate, TopicUID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function

    Public Function Request_GetByID(ReqID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Request_GetByID", ReqID)
        Return ds.Tables(0)
    End Function
    Public Function Request_GetByStudent(TopicUID As Integer, StudentID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Request_GetByStudent", TopicUID, StudentID)
        Return ds.Tables(0)
    End Function
    Public Function Request_GetByTopic(TopicUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Request_GetByTopic", TopicUID)
        Return ds.Tables(0)
    End Function
    Public Function Request_GetBySearch(TopicUID As Integer, StudentID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Request_GetBySearch", TopicUID, StudentID)
        Return ds.Tables(0)
    End Function
    Public Function Request_Delete(ReqID As Integer, ByVal pID As Integer, TopicID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Request_Delete", ReqID, pID, TopicID)
    End Function

    Public Function Request_CheckDup(RegYear As String, stdCode As String, Phase As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Request_CheckDup"), RegYear, stdCode, Phase)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function
    Public Function Request_CheckAssessment(RID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Request_CheckAssessment"), RID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function

    Public Function Request_GetStudentNoAssessment(RegYear As Integer, SubjCode As String, Optional sKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Request_NotAssessment"), RegYear, SubjCode, sKey)
        Return ds.Tables(0)

    End Function
    Public Function Request_GetStudentNoRequest(RegYear As Integer, SubjCode As String, CID As Integer, Optional sKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Request_NotRequest"), RegYear, SubjCode, CID, sKey)
        Return ds.Tables(0)

    End Function

    Public Function Request_Save(RequestID As Integer _
      , RequestDate As String _
      , StudentID As Integer _
      , TopicUID As Integer _
      , ThesisName As String _
      , AdmitDate As String _
      , AdmitTimeStart As String _
      , AdmitTimeEnd As String _
      , RoomNo As String _
      , DepartmentName As String _
      , FloorNo As String _
      , Building As String _
      , LocationName As String _
      , Road As String _
      , Subdistrict As String _
      , District As String _
      , ProvinceID As Integer _
      , ZipCode As String _
      , CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Request_Save"), RequestID _
      , RequestDate _
      , StudentID _
      , TopicUID _
      , ThesisName _
      , AdmitDate _
      , AdmitTimeStart _
      , AdmitTimeEnd _
      , RoomNo _
      , DepartmentName _
      , FloorNo _
      , Building _
      , LocationName _
      , Road _
      , Subdistrict _
      , District _
      , ProvinceID _
      , ZipCode _
      , CUser)
    End Function

    Public Function RequestExam_Save(RequestID As Integer _
      , RequestDate As String _
      , StudentID As Integer _
      , TopicUID As Integer _
      , IsExam As String _
      , ReasonExam As String _
      , Emphasize As String _
      , ExamType As Integer _
      , ObjectiveNo As Integer _
      , RoundNo As Integer _
      , LocationName As String _
      , CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("RequestExam_Save"), RequestID, RequestDate, StudentID, TopicUID, IsExam, ReasonExam, Emphasize, ExamType, ObjectiveNo, RoundNo, LocationName, CUser)
    End Function



    Public Function Request_Update(ByVal pID_old As Integer, ByVal pID_new As Integer, ByVal pName As String, desc As String, ByVal pStatus As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Request_Update", pID_old, pID_new, pName, desc, pStatus)
    End Function

    Public Function Request_UpdatePriority(ByVal ItemID As Integer, ByVal flage As String, updBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Request_UpdatePriority", ItemID, flage, updBy)
    End Function

    Public Function Request_UpdatePriorityItem(ByVal pID As Integer, ByVal pDegree As Integer, updBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Request_UpdatePriorityItem", pID, pDegree, updBy)

    End Function

#Region "Request Committee"
    Public Function RequestCommittee_Get(ReqID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RequestCommittee_Get", ReqID)
        Return ds.Tables(0)
    End Function

    Public Function RequestCommittee_Add(ByVal ReqID As Integer, PersonID As Integer, Name As String, Department As String, PositionID As Integer, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "RequestCommittee_Add", ReqID, PersonID, Name, Department, PositionID)
    End Function

    Public Function RequestCommittee_Delete(ByVal pID As Integer, ReqID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "RequestCommittee_Delete", pID, ReqID)
    End Function

    Public Function RequestDocument_Get(ByVal ReqID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RequestDocument_Get", ReqID)
        Return ds.Tables(0)
    End Function
    Public Function RequestDocument_Add(ByVal ReqID As Integer, DocumentUID As String, DocName As String, FilePath As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "RequestDocument_Add", ReqID, DocumentUID, DocName, FilePath, CUser)
    End Function

    Public Function RequestDocument_Delete(ByVal pID As Integer, ReqID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "RequestDocument_Delete", pID, ReqID)
    End Function
#End Region

    Public Function RequestTopic_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RequestTopic_Get")
        Return ds.Tables(0)
    End Function

End Class
