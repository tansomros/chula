Public Class AssesseeList
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim ctlCs As New CourseController
    Dim dt As New DataTable
    Public dtAssessee As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Request("p") Is Nothing Then
            Session("AssessorRoleID") = "P"
            Session("AssessorUID") = Request.Cookies("ProfileID").Value
            If Request.Cookies("Username").Value Is Nothing Then
                Response.Redirect("Default.aspx?p=a&ph=" & Request("ph") & "&lid=" & Request("lid") & "&u=" & Request("u") & "&logout=YES")
            Else
                If Request.Cookies("Username").Value <> Request("u") Then
                    Response.Redirect("Default.aspx?p=a&ph=" & Request("ph") & "&lid=" & Request("lid") & "&u=" & Request("u") & "&logout=YES")
                End If
            End If
            'If Session("AssessorRoleID") Is Nothing Then
            '    Response.Redirect("Default.aspx?p=a&ph=" & Request("ph") & "&lid=" & Request("lid") & "&logout=YES")
            'End If
        Else
            If Request.Cookies("Username").Value Is Nothing Then
                Response.Redirect("Default.aspx?logout=YES")
            End If
            If Session("AssessorRoleID") Is Nothing Then
                Response.Redirect("Default.aspx?logout=YES")
            End If
        End If


        If Not SystemOnlineTime() Then
            Response.Redirect("ResultPage.aspx?t=closed&p=assm")
        End If

        If Not IsPostBack Then
            lblYear.Text = Request.Cookies("ASSMYEAR").Value
            lblYearEn.Text = StrNull2Zero(Request.Cookies("ASSMYEAR").Value) - 543
            If Not Request("lid") Is Nothing Then
                Session("LocationAssessment") = Request("lid")
            End If

            LoadTimePhase()
            LoadCourseToDDL()

            If Not Request("ph") Is Nothing Then
                ddlPhase.SelectedValue = Request("ph")

            Else
                If Not Request("sj") Is Nothing Then
                    ddlCourse.SelectedValue = ctlCs.Courses_GetCourseIDBySubjectCode(Request.Cookies("ASSMYEAR").Value, Request("sj"))
                Else
                    If Not Session("CourseAssessment") = Nothing Then
                        ddlCourse.SelectedValue = Session("CourseAssessment")
                    End If
                End If
            End If

            If Request.Cookies("LANG").Value.ToLower() = "en" Then
                lblYearEn.Visible = True
                lblYear.Visible = False
                lblNote.Text = "Select a batch/course to view the list of students who wish to be evaluated and only the batch/courses for which you are trained/responsible will be displayed."
            Else
                lblYearEn.Visible = False
                lblYear.Visible = True
            End If

            If Session("AssessorRoleID") = "P" Then
                If Request.Cookies("LANG").Value.ToLower() = "en" Then
                    lblFor.Text = "Evaluation of internship results for preceptor"
                Else
                    lblFor.Text = "ประเมินผลการฝึกสำหรับอาจารย์แหล่งฝึก"
                End If
            Else
                If Request.Cookies("LANG").Value.ToLower() = "en" Then
                    lblFor.Text = "Evaluation of internship results for advisor"
                Else
                    lblFor.Text = "ประเมินผลการฝึกสำหรับอาจารย์ประจำรายวิชา"
                End If

            End If

            LoadAssesseeListToGrid()
        End If

    End Sub
    Private Function SystemOnlineTime() As Boolean
        Dim ctlCfg As New SystemConfigController

        Dim Bdate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_STARTASSM)))
        Dim Edate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_ENDASSM)))
        Dim sToday As Integer = StrNull2Zero(ConvertDate2DBString(ctlCfg.GET_DATE_SERVER))

        Dim Edate2 As Integer = sToday

        If Request.Cookies("ROLE_PCT").Value = True Then
            Edate2 = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_ENDASSM_LOC)))
            If Edate2 > Edate Then
                Edate = Edate2
            End If
        End If


        Dim bAvailable As Boolean
        If sToday < Bdate Then
            bAvailable = False
        ElseIf sToday > Edate Then
            bAvailable = False
        Else
            bAvailable = True
        End If
        Return bAvailable
    End Function

    Private Sub LoadTimePhase()
        Dim ctlTP As New TimePhaseController

        Dim LocationID As Integer

        If Session("AssessorRoleID") = "P" Then
            LocationID = Session("LocationAssessment")
            dt = ctlTP.TimePhase_GetByLocation(StrNull2Zero(lblYear.Text), LocationID)
        Else
            If Session("AssessorRoleID") = "A" Then
                dt = ctlTP.Phase_GetByYear(lblYear.Text)
            Else
                Exit Sub
            End If
        End If


        If dt.Rows.Count > 0 Then

            ddlPhase.Items.Clear()
            If Request.Cookies("LANG").Value.ToLower() = "en" Then
                ddlPhase.Items.Add("---All---")
            Else
                ddlPhase.Items.Add("---ทั้งหมด---")
            End If

            ddlPhase.Items(0).Value = 0

            For i = 1 To dt.Rows.Count
                With ddlPhase
                    .Items.Add(dt.Rows(i - 1)("Descriptions"))
                    .Items(i).Value = dt.Rows(i - 1)("PhaseID")
                End With
            Next

        Else
            ddlPhase.DataSource = dt
            ddlPhase.DataBind()
        End If
    End Sub

    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()
        Dim LocationID As Integer

        If Session("AssessorRoleID") = "P" Then
            LocationID = Session("LocationAssessment")
            dt = ctlCs.Courses_GetByLocation(StrNull2Zero(lblYear.Text), LocationID)
        Else
            If Session("AssessorRoleID") = "A" Then
                dt = ctlCs.Courses_GetByCoordinator(lblYear.Text, DBNull2Zero(Request.Cookies("ProfileID").Value))
            Else
                Exit Sub
            End If
        End If

        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            If Request.Cookies("LANG").Value.ToLower() = "en" Then
                ddlCourse.Items.Add("---All---")
            Else
                ddlCourse.Items.Add("---ทั้งหมด---")
            End If

            ddlCourse.Items(0).Value = 0

            For i = 1 To dt.Rows.Count
                With ddlCourse
                    .Items.Add("" & dt.Rows(i - 1)("SubjectCode") & " : " & dt.Rows(i - 1)("SubjectName"))
                    .Items(i).Value = dt.Rows(i - 1)("CourseID")
                End With
            Next

            If Not Request("sj") Is Nothing Then
                ddlCourse.SelectedValue = ctlCs.Courses_GetCourseIDBySubjectCode(Request.Cookies("ASSMYEAR").Value, Request("sj"))
            End If
        End If

    End Sub

    Private Sub LoadAssesseeListToGrid()
        If Session("AssessorRoleID") = "A" Then
            dtAssessee = ctlLG.Assessee_GetByCoordinator(StrNull2Zero(lblYear.Text), DBNull2Zero(Request.Cookies("ProfileID").Value), StrNull2Zero(ddlCourse.SelectedValue), txtSearch.Text, ddlStatus.SelectedValue)
        ElseIf Session("AssessorRoleID") = "P" Then
            dtAssessee = ctlLG.Assessee_GetByPrecepterPhase(lblYear.Text, Request.Cookies("ProfileID").Value, ddlPhase.SelectedValue, ddlCourse.SelectedValue, txtSearch.Text, ddlStatus.SelectedValue, StrNull2Zero(Session("LocationAssessment")))
        End If
    End Sub

    'Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
    '    If TypeOf e.CommandSource Is WebControls.ImageButton Then
    '        Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
    '        Select Case ButtonPressed.ID
    '            Case "imgEdit"
    '                Dim strS() As String
    '                strS = Split(ddlCourse.SelectedItem.Text, ":")

    '                Response.Redirect("AssesseeEvaluationGroup.aspx?p=a&std=" & e.CommandArgument() & "&sj=" & RTrim(strS(0)))
    '        End Select
    '    End If
    'End Sub
    'Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
    '        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
    '    End If
    'End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadAssesseeListToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        LoadAssesseeListToGrid()
    End Sub

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        LoadAssesseeListToGrid()
    End Sub

    Private Sub ddlPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPhase.SelectedIndexChanged
        LoadAssesseeListToGrid()
    End Sub
End Class

