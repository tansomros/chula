Imports System.IO
Public Class SurveyReport
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlS As New SurveyController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("../Login.aspx?logout=y")
        End If

        If Not IsPostBack Then
            LoadDataSex(1)
            LoadDataAssessor(1)
            LoadDataScore(1)
            lblSubjectName.Text = "แบบสำรวจความพึงพอใจเว็บไซต์สำนักวิชาแพทยศาสตร์"
        End If
    End Sub

    Private Sub LoadDataSex(ByVal tid As Integer)
        dt = ctlS.RPT_Survey_SummaryBySex(tid)
        If dt.Rows.Count > 0 Then
            With grdSex
                .DataSource = dt
                .DataBind()
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadDataAssessor(ByVal tid As Integer)
        dt = ctlS.RPT_Survey_SummaryByAssessorGroup(tid)
        If dt.Rows.Count > 0 Then
            lblCount1.Text = DBNull2Str(dt.Rows(0)("GroupCount1"))
            lblCount2.Text = DBNull2Str(dt.Rows(0)("GroupCount2"))
            lblCount3.Text = DBNull2Str(dt.Rows(0)("GroupCount3"))
            lblCount4.Text = DBNull2Str(dt.Rows(0)("GroupCount4"))
            lblCount5.Text = DBNull2Str(dt.Rows(0)("GroupCount5"))

            lblResult1.Text = DBNull2Dbl(dt.Rows(0)("Result1")).ToString("##.##")
            lblResult2.Text = DBNull2Dbl(dt.Rows(0)("Result2")).ToString("##.##")
            lblResult3.Text = DBNull2Dbl(dt.Rows(0)("Result3")).ToString("##.##")
            lblResult4.Text = DBNull2Dbl(dt.Rows(0)("Result4")).ToString("##.##")
            lblResult5.Text = DBNull2Dbl(dt.Rows(0)("Result5")).ToString("##.##")


            lblExamCount.Text = DBNull2Dbl(dt.Rows(0)("ExamCount")).ToString("##.##")

        End If
        dt = Nothing
    End Sub
    Private Sub LoadDataScore(ByVal tid As Integer)
        dt = ctlS.RPT_Survey_ScoreSummary(tid)
        If dt.Rows.Count > 0 Then
            With grdScore
                .DataSource = dt
                .DataBind()
            End With
        End If
        dt = Nothing
    End Sub


End Class