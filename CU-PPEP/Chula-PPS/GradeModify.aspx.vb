Public Class GradeModify
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim ctlCs As New CourseController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If SystemOnlineTime() Then
            Response.Redirect("ErrorPage500.aspx?p=grademod")
        End If
        If Not IsPostBack Then
            lblYear.Text = Request.Cookies("ASSMYEAR").Value

            LoadCourseToDDL()
            LoadAssesseeListToGrid()

        End If
        UpdateProgress1.DisplayAfter = 0
        UpdateProgress1.Visible = True

    End Sub
    Private Function SystemOnlineTime() As Boolean
        Dim ctlCfg As New SystemConfigController

        Dim Bdate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_STARTASSM)))
        Dim Edate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_ENDASSM)))


        Dim Edate2 As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_ENDASSM_LOC)))

        Dim sToday As Integer = StrNull2Zero(ConvertDate2DBString(ctlCfg.GET_DATE_SERVER))
        Dim bAvailable As Boolean
        If sToday < Bdate Then
            bAvailable = False
        ElseIf sToday > Edate Then
            bAvailable = False
        ElseIf sToday > Edate2 Then
            bAvailable = False
        Else
            bAvailable = True
        End If

        Return bAvailable
    End Function

    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()

        If Request.Cookies("ROLE_ADM").Value = True Then
            dt = ctlCs.Courses_GetByYear(StrNull2Zero(lblYear.Text))
        ElseIf Request.Cookies("ROLE_ADV").Value = True Then
            dt = ctlCs.Courses_GetByCoordinator(lblYear.Text, DBNull2Zero(Request.Cookies("ProfileID").Value))
        ElseIf Request.Cookies("ROLE_PCT").Value = True Then
            dt = ctlCs.Courses_GetByLocation(StrNull2Zero(lblYear.Text), Request.Cookies("LocationID").Value)
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
        dt = ctlLG.StudentAssessmentGrade_Get(StrNull2Zero(lblYear.Text), ddlCourse.SelectedValue, txtSearch.Text)
        'Else
        '    dt = ctlLG.AssessmentResult_GetByPersonID(StrNull2Zero(lblYear.text), StrNull2Zero(ddlCourse.SelectedValue), DBNull2Zero(Request.Cookies("ProfileID").Value), txtSearch.Text)
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
                    Response.Redirect("GradeEdit.aspx?uid=" & e.CommandArgument())
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
        dtStd = ctlLG.StudentAssessmentGrade_Get(StrNull2Zero(lblYear.Text), ddlCourse.SelectedValue)
        If dtStd.Rows.Count > 0 Then
            For i = 0 To dtStd.Rows.Count - 1
                ctlLG.StudentAssessmentGrade_Save(StrNull2Zero(lblYear.Text), dtStd.Rows(i)("SubjectCode"), dtStd.Rows(i)("StudentCode"), Request.Cookies("UserID").Value)
            Next
        End If
        LoadAssesseeListToGrid()
        UpdateProgress1.Visible = False
    End Sub
End Class

