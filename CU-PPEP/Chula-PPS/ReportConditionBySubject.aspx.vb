
Public Class ReportConditionBySubject
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlAss As New AssessmentController
    Dim ctlCs As New CourseController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblResult.Visible = False
            LoadYearToDDL()
            LoadCourseToDDL()
        End If
        Select Case Request("ItemType")
            Case "rpct1"
                lblReportHeader.Text = "Evaluation Item Scores"
                lblSubheading.Text = "รายงานสรุปคะแนนการประเมินตามหัวข้อ"
                ReportName = "StudentAssessmentScoreAll"
                FagRPT = "PRECEPTOR_SCORE_ALL"
                Reportskey = "XLS"
            Case "rpct2"

                lblReportHeader.Text = "Evaluation Summary"
                lblSubheading.Text = "รายงานสรุปคะแนนประเมินตามกลุ่มแบบประเมิน"
                ReportName = "StudentAssessmentScoreByPreceptor"
                FagRPT = "PRECEPTOR_SCORE"
                Reportskey = "XLS"
            Case "rpsum"
                lblReportHeader.Text = "Evaluation Summary"
                lblSubheading.Text = "รายงานสรุปคะแนนประเมิน"
                ReportName = "StudentAssessmentSummary"
                FagRPT = "ASSMSUMMARY"
                Reportskey = "XLS"
        End Select

    End Sub
    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()

        If Request.Cookies("ROLE_STD").Value = True Then
            dt = ctlCs.Courses_GetByStudent(ddlYear.SelectedValue, Request.Cookies("ProfileID").Value)
        ElseIf Request.Cookies("ROLE_ADM").Value = True Then
            dt = ctlCs.Courses_GetByYear(StrNull2Zero(ddlYear.SelectedValue))
        ElseIf Request.Cookies("ROLE_ADV").Value = True Then
            dt = ctlCs.Courses_GetByCoordinator(ddlYear.SelectedValue, DBNull2Zero(Request.Cookies("ProfileID").Value))
        ElseIf Request.Cookies("ROLE_PCT").Value = True Then
            dt = ctlCs.Courses_GetByLocation(StrNull2Zero(ddlYear.SelectedValue), Request.Cookies("LocationID").Value)
        Else
            dt = ctlCs.Courses_GetByYear(ddlYear.SelectedValue)
        End If


        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()

            If Request.Cookies("ROLE_STD").Value = True Then
                For i = 0 To dt.Rows.Count - 1
                    With ddlCourse
                        .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("SubjectName"))
                        .Items(i).Value = dt.Rows(i)("SubjectCode")
                    End With
                Next
            ElseIf Request.Cookies("ROLE_PCT").Value = True Then
                For i = 0 To dt.Rows.Count - 1
                    With ddlCourse
                        .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("SubjectName"))
                        .Items(i).Value = dt.Rows(i)("SubjectCode")
                    End With
                Next
            Else
                ddlCourse.Items.Add("")
                ddlCourse.Items(0).Value = "0"
                For i = 1 To dt.Rows.Count
                    With ddlCourse
                        .Items.Add("" & dt.Rows(i - 1)("SubjectCode") & " : " & dt.Rows(i - 1)("SubjectName"))
                        .Items(i).Value = dt.Rows(i - 1)("SubjectCode")
                    End With
                Next

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

    Protected Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        If ddlCourse.SelectedValue = "0" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','กรุณาเลือกรายวิชาก่อน');", True)
        Else
            Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&c=" & ddlCourse.SelectedValue, True)
        End If
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadCourseToDDL()
    End Sub
End Class

