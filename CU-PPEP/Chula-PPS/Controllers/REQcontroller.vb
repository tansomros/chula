Imports Microsoft.ApplicationBlocks.Data
Public Class REQcontroller

    Inherits ApplicationBaseClass
    Dim ds As New DataSet

    Public Function REQ_GetYear(lang As String) As DataTable
        'SQL = "select * from  View_ReqYear "
        'ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Requirements_GetYear", lang)
        Return ds.Tables(0)
    End Function
    Public Function REQ_GetByPKKey(pYear As Integer, pLID As Integer, pSubj As String) As DataTable
        SQL = "select * from  Requirements Where REQYEAR=" & pYear & " And LocationID=" & pLID & " And SubjectCode='" & pSubj & "'"
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function Requirements_GetLocationByYearCourse(pYear As Integer, CourseID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Requirements_GetLocationByYearCourse", pYear, CourseID)
        Return ds.Tables(0)
    End Function
    Public Function REQ_GetDetail(pYear As Integer, PLID As Integer) As DataTable
        SQL = "select * from  View_LocationREQ_Detail   Where REQYEAR=" & pYear & " And LocationID=" & PLID
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function REQ_GetByLG(pLG As Integer) As DataTable
        SQL = "select * from  View_Requirements  Where LocationGroupID=" & pLG
        ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
        Return ds.Tables(0)
    End Function

    Public Function REQ_GetBySearch(pYear As Integer, pKeys As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Requirements_GetBySearch", pYear, pKeys)
        Return ds.Tables(0)
    End Function
    Public Function Requirements_Delete(pYear As Integer, pLID As Integer, pSubj As String) As Integer
        SQL = "delete from  Requirements Where REQYEAR=" & pYear & " And LocationID=" & pLID & " And SubjectCode='" & pSubj & "'"
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function
    Public Function Requirements_GetByLocationCourse(pYear As Integer, pLID As Integer, CourseID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Requirements_GetByLocationCourse", pYear, pLID, CourseID)
        Return ds.Tables(0)
    End Function
    Public Function Requirements_GetByLocationSubjectCode(pYear As Integer, pLID As Integer, SubjectCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Requirements_GetByLocationSubjectCode", pYear, pLID, SubjectCode)
        Return ds.Tables(0)
    End Function

    Public Function Requirements_Get(pYear As Integer, pLID As Integer, TimeP As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Requirements_Get", pYear, pLID, TimeP)
        Return ds.Tables(0)
    End Function
    Public Function Requirements_GetLocation(pYear As Integer, pLIDGRP As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Requirements_GetLocation", pYear, pLIDGRP)
        Return ds.Tables(0)
    End Function

    Public Function REQ_GetLocationNotFull(pYear As Integer, CourseID As Integer, Optional pKey As String = "") As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Location_GetAssessmentNotFull"), pYear, CourseID, pKey)

        Return ds.Tables(0)
    End Function

    Public Function REQACC_GetBalanceCount(pYear As Integer, CourseID As Integer, TimePhaseID As Integer, LID As Integer, Gender As String) As Integer

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RequirementAccount_GetBalanceCountBySex"), pYear, CourseID, TimePhaseID, LID, Gender)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function
    Public Function Requirements_Get4Report(pYear As Integer, pLID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RPT_Requirements_Get", pYear, pLID)
        Return ds.Tables(0)
    End Function


    Public Function Tmp4ReportLocation_Add(ByVal REQYEAR As Integer, ByVal LCID As Integer _
    , ByVal LocationName As String _
    , ByVal LGRP As Integer _
    , ByVal Remark As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Tmp4ReportLocation_Add"), REQYEAR, LCID, LocationName, LGRP, Remark)
    End Function

    Public Function Tmp4ReportLocation_Update(ByVal REQYEAR As Integer, TimePhaseID As Integer, ByVal LCID As Integer _
   , ByVal LCGRPName As String _
   , ByVal PhaseName As String _
   , ByVal M As Integer, ByVal F As Integer, ByVal N As Integer) As Integer

        Select Case TimePhaseID
            Case 1
                Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Tmp4ReportLocation_UpdatePhase1"), REQYEAR, LCID, LCGRPName, PhaseName, M, F, N)
            Case 2
                Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Tmp4ReportLocation_UpdatePhase2"), REQYEAR, LCID, LCGRPName, PhaseName, M, F, N)
        End Select
    End Function



    Public Function tm_StudentRegister_Summary_Delete() As Integer
        SQL = "delete from  tm_StudentRegister_Summary  "
        Return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL)
    End Function

    Public Function Requirements_GetByCourse(pYear As Integer, pLID As Integer, CourseID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Requirements_GetByCourse", pYear, pLID, CourseID)
        Return ds.Tables(0)
    End Function
    Public Function Requirements_Add(ByVal REQYEAR As Integer, ByVal LCID As Integer _
      , ByVal PhaseID As Integer _
      , ByVal isSameGender As String _
      , ByVal Man As Integer _
      , ByVal Women As Integer _
      , ByVal NoSpec As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Requirements_Add"), REQYEAR, LCID, PhaseID, isSameGender, Man, Women, NoSpec)
    End Function
    Public Function Requirements_Update(ByVal REQYEAR As Integer, ByVal LCID As Integer _
       , ByVal PhaseID As Integer _
       , ByVal isSameGender As String _
       , ByVal Man As Integer _
       , ByVal Women As Integer _
       , ByVal NoSpec As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Requirements_Update"), REQYEAR, LCID, PhaseID, isSameGender, Man, Women, NoSpec)
    End Function


    Public Function Location_GetSubjectRequirement(pYear As Integer, pLID As Integer) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "Location_GetSubjectRequirement", pYear, pLID)
        Return ds.Tables(0)
    End Function


    Public Function RequirementEstimate_GetBySearch(pYear As Integer, pKeys As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, "RequirementEstimate_GetBySearch", pYear, pKeys)
        Return ds.Tables(0)
    End Function

    Public Function RequirementEstimate_GetByLocation(pYear As Integer, pLID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, "RequirementEstimate_GetByLocation", pYear, pLID)
        Return ds.Tables(0)
    End Function

    Public Function RequirementEstimate_Save(ByVal REQYEAR As Integer, ByVal LCID As Integer _
     , ByVal WorkID As Integer _
     , ByVal QTY1 As Integer _
     , ByVal QTY2 As Integer _
     , ByVal OptionSex As String _
     , ByVal isSameGender As String, MUser As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("RequirementEstimate_Save"), REQYEAR, LCID, WorkID, QTY1, QTY2, OptionSex, isSameGender, MUser)
    End Function

    Public Function REQACC_GetCount(pYear As Integer, SkillUID As Integer, SkillPhaseID As Integer, LocationID As Integer, Sex As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RequirementAccount_GetCountBySex"), pYear, SkillUID, SkillPhaseID, LocationID, Sex)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function
    Public Function REQACC_GetCountByNoSpecSex(pYear As Integer, SkillUID As Integer, SkillPhaseID As Integer, LocationID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RequirementAccount_GetCountByNoSpecSex"), pYear, SkillUID, SkillPhaseID, LocationID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If

    End Function


End Class
