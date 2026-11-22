Public Class AssessmentResultPreceptor
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim ctlCs As New CourseController
    Dim dt As New DataTable

    Public dtScore As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            LoadYearToDDL()
            LoadCourseToDDL()
            LoadAssesseeListToGrid()

            dtScore = ctlLG.EvaluationFullScore_Get4Preceptor(ddlYear.SelectedValue, ddlCourse.SelectedValue)


            If Request.Cookies("LANG").Value.ToLower() = "en" Then
                cmdView.Text = "View"
                cmdExportScore.Text = "Export"
                cmdExportScoreAll.Text = "Export for Verify Score(s)"
                cmdFind.Text = "Search"
                cmdAll.Text = "View All"
            Else
                cmdView.Text = "View"
                cmdExportScore.Text = "Export"
                cmdExportScoreAll.Text = "Export สำหรับตรวจสอบคะแนน"
                cmdFind.Text = "ค้นหา"
                cmdAll.Text = "ดูทั้งหมด"
            End If
        End If

    End Sub


    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlCs.Courses_GetYearByLang(Request.Cookies("LANG").Value)
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "YearText"
                .DataValueField = "YearValue"
                .DataBind()

                If dt.Rows(LastRow)(0) = y Then
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 1).Value = y + 1
                ElseIf dt.Rows(LastRow)(0) > y Then
                    'ddlYear.Items.Add(y + 2)
                    'ddlYear.Items(LastRow + 1).Value = y + 2
                ElseIf dt.Rows(LastRow)(0) < y Then
                    ddlYear.Items.Add(y)
                    ddlYear.Items(LastRow + 1).Value = y
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 2).Value = y + 1
                End If
                .SelectedIndex = 0
            End With
        Else
            ddlYear.Items.Add(y)
            ddlYear.Items(0).Value = y
            ddlYear.Items.Add(y + 1)
            ddlYear.Items(1).Value = y + 1
            ddlYear.SelectedIndex = 0
        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        dt = Nothing
    End Sub

    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()
        dt = ctlCs.Courses_GetByLocation(StrNull2Zero(ddlYear.SelectedValue), Request.Cookies("LocationID").Value)
        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With ddlCourse
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("SubjectName"))
                    .Items(i).Value = dt.Rows(i)("SubjectCode")
                End With
            Next

        End If

    End Sub

    Private Sub LoadAssesseeListToGrid()

        dt = ctlLG.AssessmentResult_Preceptor_Get(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue), Request.Cookies("LocationID").Value, txtSearch.Text)
        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
            lblResult.Visible = False
        Else
            lblResult.Visible = True
        End If


        dt = Nothing
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    Dim strS() As String
                    strS = Split(ddlCourse.SelectedItem.Text, ":")

                    Response.Redirect("AssesseeEvaluationGroup.aspx?p=a&std=" & e.CommandArgument() & "&sj=" & RTrim(strS(0)))
            End Select
        End If
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadAssesseeListToGrid()
    End Sub
    Protected Sub cmdAll_Click(sender As Object, e As EventArgs) Handles cmdAll.Click
        txtSearch.Text = ""
        LoadAssesseeListToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        LoadAssesseeListToGrid()
    End Sub

    Protected Sub cmdView_Click(sender As Object, e As EventArgs) Handles cmdView.Click
        LoadAssesseeListToGrid()
    End Sub

    Protected Sub cmdExportScore_Click(sender As Object, e As EventArgs) Handles cmdExportScore.Click
        System.Threading.Thread.Sleep(1000)
        ReportName = "StudentAssessmentScoreByPreceptor"
        FagRPT = "PRECEPTOR_SCORE"
        Reportskey = "XLS"
        Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&c=" & ddlCourse.SelectedValue, True)
        'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Download", "window.location='ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&c=" & ddlCourse.SelectedValue & "';", True)

    End Sub

    Protected Sub cmdExportScoreAll_Click(sender As Object, e As EventArgs) Handles cmdExportScoreAll.Click
        ReportName = "StudentAssessmentScoreAll"
        FagRPT = "PRECEPTOR_SCORE_ALL"
        Reportskey = "XLS"
        Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&c=" & ddlCourse.SelectedValue, True)
    End Sub
End Class

