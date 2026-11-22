
Public Class ReportConditionByYear
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlAss As New AssessmentController
    Dim ctlCs As New Coursecontroller

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblResult.Visible = False
            cmdPrint2.Visible = False
            LoadYearToDDL()

            Select Case Request("m")
                Case "manage"
                    cmdPrint2.Visible = True
                    lblReportHeader.Text = "รายงานสรุปข้อมูลการจัดสรรนิสิตเข้าฝึกปฏิบัติงานวิชาชีพ"
                    ReportName = "StudentAssessmentByYear"
                    FagRPT = "STDASSESSMENT"
                    Reportskey = "XLS"
                Case "stdassessment"
                    lblReportHeader.Text = "รายงานแหล่งฝึกแยกตามรายชื่อ"
                    ReportName = "StudentAssessmentByStudent"
                    FagRPT = "STDASSBYSTD"
                    Reportskey = "XLS"

                Case "payamount"
                    lblReportHeader.Text = "รายงานสรุปข้อมูลค่าตอบแทน"
                    ReportName = "StudentSummaryCount"
                    FagRPT = "PAYAMOUNT"
                    Reportskey = "XLS"
                Case "docstdlist"
                    lblReportHeader.Text = "รายชื่อนิสิตแต่ละผลัดฝึก (เอกสารแนบใบส่งตัว)"
                    ReportName = "docStudentList"
                    FagRPT = "DOCSTDLIST"
                    Reportskey = "PDF"
                Case "timephase"
                    lblReportHeader.Text = "รายงานสรุปข้อมูลช่วงเวลาที่มีนิสิตเข้าฝึกปฏิบัติงาน"
                    ReportName = "LocationTimePhasePractice2"
                    FagRPT = "TIMEPHASE"
                    Reportskey = "XLS"
                Case "phasecount"
                    lblReportHeader.Text = "รายงานสรุปจำนวนนิสิตแต่ละผลัดฝึก"
                    ReportName = "StudentAssessmentCountByPhase"
                    FagRPT = "PHASECOUNT"
                    Reportskey = "XLS"


            End Select
        End If
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

    Protected Sub cmdPrint1_Click(sender As Object, e As EventArgs) Handles cmdPrint1.Click

        Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&c=0")

    End Sub

    Protected Sub cmdPrint2_Click(sender As Object, e As EventArgs) Handles cmdPrint2.Click

        lblReportHeader.Text = "รายงานสรุปข้อมูลการจัดสรรนิสิตเข้าฝึกปฏิบัติงานวิชาชีพ"
        ReportName = "StudentAssessmentTable"
        FagRPT = "STDASSESSMENT2"
        Reportskey = "XLS"

        Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&c=0")

    End Sub
End Class

