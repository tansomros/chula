Imports Microsoft.ApplicationBlocks.Data
Public Class SurveyController
    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function ProblemGroup_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("ProblemGroup_Get"))
        Return ds.Tables(0)
    End Function

    Public Function ProblemItem_Get(pGroupUID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("ProblemItem_Get"), pGroupUID)
        Return ds.Tables(0)
    End Function

    Public Function BehaviorProblem_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("BehaviorRelate_Get"))
        Return ds.Tables(0)
    End Function
    Public Function Survey_Save(SurveyUID As Integer, Sexx As String, AssessorGroup As String, Department As String, Remark As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("Survey_Save"), SurveyUID, Sexx, AssessorGroup, Department, Remark)
    End Function
    Public Function SurveyScore_Save(SurveyUID As Integer, SurveyTopicUID As Integer, Score As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("SurveyScore_Save"), SurveyUID, SurveyTopicUID, Score)
    End Function

    Public Function Survey_GetLastUID(SurveyUID As Integer, Sexx As String, AssessorGroup As String, Department As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("Survey_GetLastUID"), SurveyUID, Sexx, AssessorGroup, Department)
        Return ds.Tables(0).Rows(0)(0)
    End Function

    Public Function RPT_Survey_SummaryBySex(FormUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("RPT_Survey_SummaryBySex"), FormUID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Survey_SummaryByAssessorGroup(FormUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("RPT_Survey_SummaryByAssessorGroup"), FormUID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Survey_ScoreSummary(FormUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, ("RPT_Survey_ScoreSummary"), FormUID)
        Return ds.Tables(0)
    End Function


End Class
