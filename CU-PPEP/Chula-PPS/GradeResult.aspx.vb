Public Class GradeResult
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim ctlCs As New CourseController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            LoadYearToDDL()
            LoadCourseToDDL()
            LoadAssesseeListToGrid()

        End If
        UpdateProgress1.DisplayAfter = 0
        UpdateProgress1.Visible = True

    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlCs.Courses_GetYear
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

        If Request.Cookies("ROLE_ADM").Value = True Then
            dt = ctlCs.Courses_GetByYear(StrNull2Zero(ddlYear.SelectedValue))
        ElseIf Request.Cookies("ROLE_ADV").Value = True Then
            dt = ctlCs.Courses_GetByCoordinator(ddlYear.SelectedValue, DBNull2Zero(Request.Cookies("ProfileID").Value))
        ElseIf Request.Cookies("ROLE_PCT").Value = True Then
            dt = ctlCs.Courses_GetByLocation(StrNull2Zero(ddlYear.SelectedValue), Request.Cookies("LocationID").Value)
        End If

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
        'System.Threading.Thread.Sleep(1000)
        UpdateProgress1.DisplayAfter = 0
        UpdateProgress1.Visible = True
        'If Request.Cookies("ROLE_ADM").Value = True Then
        dt = ctlLG.AssessmentResult_Get(StrNull2Zero(ddlYear.SelectedValue), ddlCourse.SelectedValue, txtSearch.Text)
        'Else
        '    dt = ctlLG.AssessmentResult_GetByPersonID(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue), DBNull2Zero(Request.Cookies("ProfileID").Value), txtSearch.Text)
        'End If

        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With
        dt = Nothing
        UpdateProgress1.Visible = False

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

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        LoadAssesseeListToGrid()
    End Sub
    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        LoadAssesseeListToGrid()
    End Sub

    Protected Sub cmdSearchAll_Click(sender As Object, e As EventArgs) Handles cmdSearchAll.Click
        txtSearch.Text = ""
        LoadAssesseeListToGrid()
    End Sub
    Private Sub UpdateGrade()
        System.Threading.Thread.Sleep(1000)
        UpdateProgress1.DisplayAfter = 0
        UpdateProgress1.Visible = True

        Dim dtStd As New DataTable
        dtStd = ctlLG.StudentAssessmentGrade_Get(StrNull2Zero(ddlYear.SelectedValue), ddlCourse.SelectedValue)
        If dtStd.Rows.Count > 0 Then
            For i = 0 To dtStd.Rows.Count - 1
                ctlLG.StudentAssessmentGrade_Save(StrNull2Zero(ddlYear.SelectedValue), dtStd.Rows(i)("SubjectCode"), dtStd.Rows(i)("StudentCode"), Request.Cookies("UserID").Value)
            Next
        End If
        LoadAssesseeListToGrid()
        UpdateProgress1.Visible = False
    End Sub
End Class

