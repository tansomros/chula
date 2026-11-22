Imports Microsoft.ApplicationBlocks.Data

Public Class ResultController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

#Region "Student Grade"
    Public Function StudentGrade_GetYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "StudentGrade_GetYear")
        Return ds.Tables(0)
    End Function

    Public Function StudentGrade_GetBySearch(EduYear As String, TermNo As String, StudentCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "StudentGrade_GetBySearch", EduYear, TermNo, StudentCode)
        Return ds.Tables(0)
    End Function
    Public Function StudentGrade_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "StudentGrade_GetByUID", UID)
        Return ds.Tables(0)
    End Function
    Public Function StudentGrade_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "StudentGrade_Delete", UID)
    End Function
    Public Function StudentGrade_Save(ByVal UID As Integer, ByVal StudentID As Integer, StudentCode As String, SubjectCode As String, ByVal EduYear As Integer, ByVal TermNo As Integer, ByVal Unit As Integer, Grade As String, Cuser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "StudentGrade_Save", UID, StudentID, StudentCode, SubjectCode, EduYear, TermNo, Unit, Grade, Cuser)
    End Function


#End Region
    Public Function StudentResult_GetByStudentSearch(pYear As Integer, SubjCode As String, stdkey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_StudentResult", pYear, SubjCode, stdkey)
        Return ds.Tables(0)
    End Function

    Public Function StudentResult_GetBySearch(pYear As Integer, SubjCode As String, LocationID As Integer, TimePhaseID As Integer, stdkey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Student_Assessment", pYear, SubjCode, LocationID, TimePhaseID, stdkey)
        Return ds.Tables(0)
    End Function


    Public Function StudentResult_GetStudentSex(RegYear As String, LocationID As Integer, Phase As Integer, SubjectCode As String) As String

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentResult_GetStudentSex"), RegYear, LocationID, Phase, SubjectCode)

        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Str(ds.Tables(0).Rows(0)(0))
        Else
            Return ""
        End If
    End Function

    Public Function StudentResult_GetStudentCountBySex(RegYear As String, LocationID As Integer, Phase As Integer, sSex As String, SubjectCode As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentResult_GetStudentCountBySex"), RegYear, LocationID, Phase, sSex, SubjectCode)

        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))

    End Function
    Public Function StudentResult_GetCheckDup(RegYear As String, Phase As Integer, stdcode As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentResult_CheckDup"), RegYear, stdcode, Phase)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function
    Public Function StudentResult_GetResultByStudent(RegYear As String, StdCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentResult_GetResultByStudent"), RegYear, StdCode)

        Return ds.Tables(0)

    End Function

    Public Function StudentResult_GetResultByLocation(RegYear As String, SubjCode As String, TimePhaseID As Integer, LID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentResult_GetResultByLocation"), RegYear, SubjCode, TimePhaseID, LID)
        Return ds.Tables(0)

    End Function
    Public Function StudentResult_Add(RegYear As String, SubjectCode As String, Student_Code As String, LocationID As Integer, Phase As Integer, CourseID As Integer, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("StudentResult_Add"), RegYear, SubjectCode, Student_Code, LocationID, Phase, CourseID, UpdBy)
    End Function

    Public Function StudentResult_DeleteByID(ByVal pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "StudentResult_DeleteByID", pID)
    End Function
    Public Function EvaluationGroup_Get(ByVal pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationGroup_Get"), pSearch)
        Return ds.Tables(0)
    End Function
    Public Function EvaluationGroup_GetActive() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationGroup_GetActive"))
        Return ds.Tables(0)
    End Function
    Public Function EvaluationGroup_GetByUID(ByVal PUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationGroup_GetByUID"), PUID)
        Return ds.Tables(0)
    End Function

    Public Function EvaluationTopic_GetWeightRate(ByVal PUID As Integer) As Double
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationTopic_GetWeightRate"), PUID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Dbl(ds.Tables(0).Rows(0)(0))
        Else
            Return 1
        End If

    End Function
    Public Function EvaluationGroup_GetBaseScore(ByVal PUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationGroup_GetBaseScore"), PUID)
        Return ds.Tables(0)
    End Function
    Public Function EvaluationGroup_GetName(ByVal PUID As Integer) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationGroup_GetName"), PUID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Str(ds.Tables(0).Rows(0)(0))
        Else
            Return ""
        End If
    End Function

    Public Function EvaluationGroup_GetCompare(ByVal PUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationGroup_GetCompare"), PUID)

        Return ds.Tables(0)

    End Function

    Public Function EvaluationGroup_Delete(ByVal pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "EvaluationGroup_Delete", pID)
    End Function
    Public Function EvaluationGroup_Save(ByVal pID As Integer, Name As String, AliasName As String, FullScore As Integer, statusflag As String, isAdvisor As String, isPreceptor As String, isCompare As String, isNoScore As String, Min As Integer, Max As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "EvaluationGroup_Save", pID, Name, AliasName, FullScore, statusflag, isAdvisor, isPreceptor, isCompare, isNoScore, Min, Max)
    End Function
    Public Function EvaluationGroup_GetSearch(ByVal pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationGroup_GetSearch"), pSearch)
        Return ds.Tables(0)
    End Function
    Public Function EvaluationGroup_GetAssessment(ByVal pYear As Integer, SubjectCode As String, UserGroup As String, StudentCode As String, AssessorUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationGroup_GetAssessment"), pYear, SubjectCode, UserGroup, StudentCode, AssessorUID)
        Return ds.Tables(0)
    End Function



    Public Function EvaluationTopic_Get(ByVal pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationTopic_Get"), pSearch)
        Return ds.Tables(0)
    End Function

    Public Function EvaluationTopicNotSetForm_Get(ByVal EvaGroup As Integer, ByVal pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationTopicNotSetForm_Get"), EvaGroup, pSearch)
        Return ds.Tables(0)
    End Function

    Public Function EvaluationTopic_GetByGroup(ByVal GroupUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationTopic_GetByGroup"), GroupUID)
        Return ds.Tables(0)
    End Function

    Public Function EvaluationTopic_GetByUID(ByVal PUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationTopic_GetByUID"), PUID)
        Return ds.Tables(0)
    End Function
    Public Function EvaluationTopic_Delete(ByVal pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "EvaluationTopic_Delete", pID)
    End Function
    Public Function EvaluationTopic_Save(ByVal pID As Integer, Name As String, Weight As Integer, statusflag As String, Remark As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "EvaluationTopic_Save", pID, Name, Weight, statusflag, Remark)
    End Function


#Region "Eva Form"
    Public Function EvaluationForm_Add(ByVal SEQNO As Integer, ByVal EvaluationTopicUID As Integer, EvaluationGroupUID As Integer, StatusFlag As String, Remark As String, Cuser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "EvaluationForm_Add", SEQNO, EvaluationTopicUID, EvaluationGroupUID, StatusFlag, Remark, Cuser)
    End Function
    Public Function EvaluationForm_UpdateSEQ(UID As Integer, ByVal SEQNO As Integer, MUser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "EvaluationForm_UpdateSEQ", UID, SEQNO, MUser)
    End Function
    Public Function EvaluationForm_Delete(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "EvaluationForm_Delete", UID)
    End Function
#End Region

    Public Function Subject_GetNoEvaluationSearch(ByVal pYear As Integer, ByVal pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Subject_GetNoEvaluationSearch"), pYear, pSearch)
        Return ds.Tables(0)
    End Function
    Public Function Subject_GetNoEvaluationSearch2(ByVal pYear As Integer, ByVal pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Subject_GetNoEvaluationSearch2"), pYear, pSearch)
        Return ds.Tables(0)
    End Function

    Public Function AssessmentSubject_Add(ByVal EduYear As Integer, ByVal SubjectID As Integer, SubjectCode As String, EvaGroup As Integer, Cuser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "AssessmentSubject_Add", EduYear, SubjectID, SubjectCode, EvaGroup, Cuser)
    End Function
    Public Function AssessmentSubject_Copy(ByVal EduYear1 As Integer, EduYear2 As Integer, Cuser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "AssessmentSubject_Copy", EduYear1, EduYear2, Cuser)
    End Function
    Public Function AssessmentSubject_GetSearch(ByVal pYear As Integer, ByVal pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AssessmentSubject_GetSearch"), pYear, pSearch)
        Return ds.Tables(0)
    End Function
    Public Function AssessmentSubject_DeleteByUID(UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "AssessmentSubject_DeleteByUID", UID)
    End Function
    Public Function AssessmentSubject_Delete(ByVal EduYear As Integer, ByVal SubjectID As Integer, EvaGroup As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "AssessmentSubject_Delete", EduYear, SubjectID, EvaGroup)
    End Function

    Public Function AssessmentSubject_DeleteBySubject(ByVal EduYear As Integer, ByVal SubjectID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "AssessmentSubject_DeleteBySubjectID", EduYear, SubjectID)
    End Function
    Public Function AssessmentSubject_GetYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AssessmentSubject_GetYear"))
        Return ds.Tables(0)
    End Function

    Public Function AssessmentSubject_GetByUID(ByVal UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AssessmentSubject_GetByUID"), UID)
        Return ds.Tables(0)
    End Function

    Public Function Assessee_GetByCoordinator(ByVal PYear As Integer, ByVal PersonID As Integer, ByVal CourseID As Integer, ByVal pSearch As String, Status As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Assessee_GetByCoordinator"), PYear, PersonID, CourseID, pSearch, Status)
        Return ds.Tables(0)
    End Function
    Public Function Assessee_GetByPrecepter(ByVal PYear As Integer, ByVal PersonID As Integer, ByVal CourseID As Integer, ByVal pSearch As String, Status As String, ByVal LocationID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Assessee_GetByPrecepter"), PYear, PersonID, CourseID, pSearch, Status, LocationID)
        Return ds.Tables(0)
    End Function

    Public Function Assessee_GetBySearch(ByVal PYear As Integer, ByVal CourseID As Integer, ByVal LocationID As Integer, ByVal pSearch As String, Status As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Assessee_GetBySearch"), PYear, CourseID, LocationID, pSearch, Status)
        Return ds.Tables(0)
    End Function

    Public Function Assessee_Get4Prominent(ByVal PYear As Integer, ByVal LocationID As Integer, ByVal PhaseID As Integer, ByVal pSearch As String, Status As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Assessee_Get4Prominent"), PYear, LocationID, PhaseID, pSearch, Status)
        Return ds.Tables(0)
    End Function

    Public Function StudentStudentResult_GetBySearch(ByVal PYear As Integer, ByVal CourseID As Integer, ByVal LocationID As Integer, ByVal pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentStudentResult_GetBySearch"), PYear, CourseID, LocationID, pSearch)
        Return ds.Tables(0)
    End Function

    Public Function StudentStudentResult_GetSearch4Admin(ByVal PYear As Integer, ByVal CourseID As Integer, ByVal LocationID As Integer, ByVal pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentStudentResult_GetSearch4Admin"), PYear, CourseID, LocationID, pSearch)
        Return ds.Tables(0)
    End Function
    Public Function StudentAssessmentGrade_GetByUID(ByVal UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentAssessmentGrade_GetByUID"), UID)
        Return ds.Tables(0)
    End Function
    Public Function StudentAssessmentGrade_Get(ByVal PYear As Integer, ByVal SubjectCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentAssessmentGrade_Get"), PYear, SubjectCode)
        Return ds.Tables(0)
    End Function
    Public Function StudentAssessmentGrade_Get(ByVal pYear As Integer, SubjectCode As String, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentAssessmentGrade_GetSearch"), pYear, SubjectCode, Search)
        Return ds.Tables(0)
    End Function

    Public Function Student_GetStudentNoAssessment(EduYear As Integer, SubjCode As String, Optional sKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Student_NotAssessment"), EduYear, SubjCode, sKey)
        Return ds.Tables(0)

    End Function

    Public Function AssessmentStudent_Get(ByVal pYear As Integer, SubjectCode As String, StdCode As String, guid As Integer, AssessorUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AssessmentStudent_Get"), pYear, SubjectCode, StdCode, guid, AssessorUID)
        Return ds.Tables(0)
    End Function

    Public Function AssessmentStudent_Save(ByVal pYear As Integer, SubjectCode As String, StdCode As String, AssessorUID As Integer, GroupUID As Integer, TopicUID As Integer, Score1 As Double, Score2 As Double, CUser As String, AssessorRoleID As String, AssessorLocationID As Integer, isExclude As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AssessmentStudent_Save"), pYear, SubjectCode, StdCode, AssessorUID, GroupUID, TopicUID, Score1, Score2, CUser, AssessorRoleID, AssessorLocationID, isExclude)
    End Function

    Public Function AssessmentStudent_SaveScore(ByVal pYear As Integer, SubjectCode As String, StdCode As String, AssessorUID As Integer, GroupUID As Integer, Score As Double, NetScore As Double, CUser As String, AssessorRoleID As String, AssessorLocationID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AssessmentStudent_SaveScore"), pYear, SubjectCode, StdCode, AssessorUID, GroupUID, Score, NetScore, CUser, AssessorRoleID, AssessorLocationID)
    End Function
    Public Function StudentAssessmentGrade_Save(ByVal pYear As Integer, SubjectCode As String, StdCode As String, CUser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("StudentAssessmentGrade_Save"), pYear, SubjectCode, StdCode, CUser)
    End Function

    Public Function StudentAssessmentGrade_SaveByUID(ByVal UID As Integer, ByVal Score As Double, Grade As String, CUser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("StudentAssessmentGrade_SaveByUID"), UID, Score, Grade, CUser)
    End Function
    Public Function StudentAssessmentGrade_SaveReason(ByVal UID As Integer, Score_Old As Double, Score_New As Double, Grade_Old As String, Grade_New As String, Comment As String, EditorUID As Integer, CUser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("StudentAssessmentGrade_SaveReason"), UID, Score_Old, Score_New, Grade_Old, Grade_New, Comment, EditorUID, CUser)
    End Function


    Public Function StudentResult_GetByAdvisor(ByVal pYear As Integer, SubjectCode As String, AssessorUID As Integer, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentResult_GetByAdvisor"), pYear, SubjectCode, AssessorUID, Search)
        Return ds.Tables(0)
    End Function
    Public Function StudentResult_Get(ByVal pYear As Integer, SubjectCode As String, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentResult_Get"), pYear, SubjectCode, Search)
        Return ds.Tables(0)
    End Function
    Public Function StudentResult_Preceptor_Get(ByVal pYear As Integer, SubjectCode As String, LocationID As Integer, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentResult_Preceptor_Get"), pYear, SubjectCode, LocationID, Search)
        Return ds.Tables(0)
    End Function
    Public Function EvaluationFullScore_Get4Preceptor(ByVal pYear As Integer, SubjectCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("EvaluationFullScore_Get4Preceptor"), pYear, SubjectCode)
        Return ds.Tables(0)
    End Function
    Public Function StudentResult_GetByPersonID(ByVal pYear As Integer, SubjectCode As String, PersonID As Integer, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentResult_GetByPersonID"), pYear, SubjectCode, PersonID, Search)
        Return ds.Tables(0)
    End Function

    Public Function StudentResult_Update(AssessmentID As Integer, EduYear As String, SubjectCode As String, Student_Code As String, LocationID As Integer, TimePhaseID As Integer, CourseID As Integer, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("StudentResult_Update"), AssessmentID, EduYear, SubjectCode, Student_Code, LocationID, TimePhaseID, CourseID, UpdBy)
    End Function


#Region "Grade"
    Public Function Grade_Get(ByVal pYear As Integer, ByVal pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Grade_Get"), pYear, pSearch)
        Return ds.Tables(0)
    End Function
    Public Function Grade_GetByID(ByVal PID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Grade_GetByID"), PID)
        Return ds.Tables(0)
    End Function
    Public Function Grade_Save(ByVal EduYear As Integer, SubjectCode As String, Grade As String, MinScore As Double, MaxScore As Double) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Grade_Save", EduYear, SubjectCode, Grade, MinScore, MaxScore)
    End Function



    Public Function Grade_GetSubjectNoGrade(ByVal pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Grade_GetSubjectNoGrade"), pYear)
        Return ds.Tables(0)
    End Function
    Public Function Grade_Copy(ByVal EduYear As Integer, SubjectCodeSource As String, SubjectCodeDestination As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Grade_Copy", EduYear, SubjectCodeSource, SubjectCodeDestination)
    End Function

#End Region

#Region "Grade Preceptor"
    Public Function GradePreceptor_Get(pYear As Integer, ByVal pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Grade_Preceptor_Get"), pYear, pSearch)
        Return ds.Tables(0)
    End Function
    Public Function GradePreceptor_GetByID(ByVal PID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Grade_Preceptor_GetByID"), PID)
        Return ds.Tables(0)
    End Function
    Public Function GradePreceptor_Save(ByVal EduYear As Integer, SubjectCode As String, Grade As String, MinScore As Double, MaxScore As Double) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Grade_Preceptor_Save", EduYear, SubjectCode, Grade, MinScore, MaxScore)
    End Function

    Public Function GradePreceptor_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Grade_Preceptor_Delete", UID)
    End Function

    Public Function GradePreceptor_GetSubjectNoGrade(ByVal pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Grade_Preceptor_GetSubjectNoGrade"), pYear)
        Return ds.Tables(0)
    End Function
    Public Function GradePreceptor_Copy(ByVal EduYear As Integer, SubjectCodeSource As String, SubjectCodeDestination As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Grade_Preceptor_Copy", EduYear, SubjectCodeSource, SubjectCodeDestination)
    End Function


#End Region

    Public Function StudentAssessmentScore_GetByUID(ByVal UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentAssessmentScore_GetByUID"), UID)
        Return ds.Tables(0)
    End Function

    Public Function StudentStudentResult_GetByStudent(ByVal stdcode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentStudentResult_GetByStudent"), stdcode)
        Return ds.Tables(0)
    End Function

    Public Function AssessmentStudentCommentText_Get(ByVal EduYear As Integer, StudentCode As String, LocationID As Integer, UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AssessmentStudentCommentText_Get"), EduYear, StudentCode, LocationID, UserID)
        Return ds.Tables(0)
    End Function


    Public Function AssessmentStudentReason_Save(ByVal pYear As Integer, EvaGroupUID As Integer, StdCode As String, SubjectCode As String, Score As Double, CUser As String, Comment As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AssessmentStudentReason_Save"), pYear, EvaGroupUID, StdCode, SubjectCode, Score, CUser, Comment)
    End Function

    Public Function AssessmentStudentComment_Save(EduYear As Integer, StudentCode As String, LocationID As Integer, UserID As Integer, ProsID As Integer, CUser As String, Comment As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AssessmentStudentComment_Save"), EduYear, StudentCode, LocationID, UserID, ProsID, CUser, Comment)
    End Function

    Public Function AssessmentStudentComment_Delete(EduYear As Integer, StudentCode As String, LocationID As Integer, UserID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AssessmentStudentComment_Delete"), EduYear, StudentCode, LocationID, UserID)
    End Function


    Public Function AssessmentStudentCommentText_Save(EduYear As Integer, StudentCode As String, LocationID As Integer, UserID As Integer, WorkDetail As String, Comment1 As String, Comment2 As String, StandingGrade As String, IsSure As String, Sure As Integer, Reason As String, Comments As String, CUser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AssessmentStudentCommentText_Save"), EduYear, StudentCode, LocationID, UserID, WorkDetail, Comment1, Comment2, StandingGrade, IsSure, Sure, Reason, Comments, CUser)
    End Function

    Public Function StudentResult_GetCount(RegYear As String, LocationID As Integer, SkillPhaseID As Integer, SkillUID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentResult_GetStudentCount"), RegYear, LocationID, SkillPhaseID, SkillUID)

        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))

    End Function

    Public Function StudentResult_GetCountBySex(RegYear As String, LocationID As Integer, SkillPhaseID As Integer, sSex As String, SkillUID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("StudentResult_GetStudentCountBySex"), RegYear, LocationID, SkillPhaseID, sSex, SkillUID)

        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))

    End Function
End Class
