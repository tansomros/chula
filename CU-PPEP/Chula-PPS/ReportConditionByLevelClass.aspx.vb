
Public Class ReportConditionByLevelClass
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlAss As New AssessmentController
    Dim ctlCs As New Coursecontroller

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            Select Case Request("m")
                Case "hour"
                    lblReportHeader.Text = "รายงานสรุปจำนวนชั่วโมงฝึกปฏิบัติงานวิชาชีพ"
                    ReportName = "StudentWorkHour"
                    FagRPT = "HOUR"
                    Reportskey = "XLS"

            End Select
        End If
    End Sub
    Protected Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click

        Response.Redirect("ReportServerViewer.aspx?lv=" & ddlLevel.SelectedValue & "&c=0")

    End Sub
End Class

