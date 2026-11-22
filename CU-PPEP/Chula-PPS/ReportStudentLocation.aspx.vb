Imports Microsoft.Reporting.WebForms
Public Class ReportStudentLocation
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
            LoadYearToDDL()
            LoadCourseToDDL()
        End If
    End Sub
    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()

        If Request.Cookies("ROLE_STD").Value = True Then
            dt = ctlCs.Courses_GetByStudent(ddlYear.SelectedValue, Request.Cookies("ProfileID").Value)
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
            Else
                ddlCourse.Items.Add("---ทั้งหมด---")
                ddlCourse.Items(0).Value = 0
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
        FagRPT = ""

        'Dim fRptView As New ReportViewer

        'fRptView.FileName = "Reports/rptResultStudentLocation.rpt"
        'If ddlCourse.SelectedValue = 0 Then
        '    fRptView.SelectionFomula = "{View_Student_Assessment.PYear}=" & ddlYear.SelectedValue
        'Else
        '    fRptView.SelectionFomula = "{View_Student_Assessment.SubjectCode}='" & ddlCourse.SelectedValue & "' AND {View_Student_Assessment.PYear}=" & ddlYear.SelectedValue
        'End If

        'Response.Redirect("ReportViewer.aspx")

        FagRPT = "STDLOCATION"
        Reportskey = "XLS"
        ReportName = "StudentSelectedLocation"

        'Dim dStart As String = ConvertStrDate2DBString(txtStartDate.Text)
        'Dim dEnd As String = ConvertStrDate2DBString(txtEndDate.Text)
        UpdateProgress1.Visible = False
        Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&c=" & ddlCourse.SelectedValue)

    End Sub

    Protected Sub cmdPrint2_Click(sender As Object, e As EventArgs) Handles cmdPrint2.Click
        FagRPT = ""
        FagRPT = "STDLOCATION"
        Reportskey = "XLS"
        ReportName = "StudentSelectedLocationTable"

        UpdateProgress1.Visible = False
        Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&c=" & ddlCourse.SelectedValue)

    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadCourseToDDL()
        UpdatePanel1.Visible = False
    End Sub


    Private Sub LoadReport()

        System.Threading.Thread.Sleep(1000)
        UpdateProgress1.Visible = True
        UpdatePanel1.Visible = True

        Dim credential As New ReportServerCredentials
        ReportViewer1.Reset()
        ReportViewer1.ServerReport.ReportServerCredentials = credential
        ReportViewer1.ServerReport.ReportServerUrl = credential.ReportServerUrl
        ReportViewer1.ShowPrintButton = True
        ReportViewer1.Height = 500
        'ReportViewer1.ZoomPercent = 100


        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("th-TH")
        Dim xParam As New List(Of ReportParameter)
        Dim ReportName As String = ""
        ReportName = "StudentSelectedLocationByStudentCode"

        ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)


        'Select Case FagRPT

        '    Case "REQACCOUNT"

        xParam.Add(New ReportParameter("Year", ddlYear.SelectedValue))
        If Request.Cookies("ROLE_STD").Value = True Then
            xParam.Add(New ReportParameter("StudentCode", Request.Cookies("Username").Value.ToString()))
        Else
            xParam.Add(New ReportParameter("StudentCode", "0"))
        End If


        'End Select


        ReportViewer1.ServerReport.SetParameters(xParam)
        ReportViewer1.ServerReport.Refresh()

        UpdateProgress1.Visible = False
    End Sub


    Protected Sub cmdPrint3_Click(sender As Object, e As EventArgs) Handles cmdPrint3.Click
        LoadReport()
        UpdateProgress1.Visible = False

    End Sub
End Class

