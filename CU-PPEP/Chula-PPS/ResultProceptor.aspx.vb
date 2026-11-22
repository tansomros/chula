Public Class ResultProceptor
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlAss As New AssessmentController
    Dim ctlCs As New CourseController
    Dim ctlPs As New PersonController
    Dim ctlP As New PreceptorController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            If Request.Cookies("LANG").Value.ToLower() = "en" Then
                cmdFind.Text = "Search"
            End If
            lblResult.Visible = False
            LoadYearToDDL()
            LoadLocationToDDL()
            LoadTimePhase()
            LoadCourseToDDL()
            LoadResultProcess()
        End If
    End Sub
    Private Sub LoadLocationToDDL()
        ddlLocation.Items.Clear()
        dt = ctlP.PreceptorLocation_GetByUser(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(Request.Cookies("UserID").Value))
        If dt.Rows.Count > 0 Then
            With ddlLocation
                .DataSource = dt
                .DataTextField = "LocationName"
                .DataValueField = "LocationID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadTimePhase()
        Dim ctlTP As New TimePhaseController

        dt = ctlTP.TimePhase_GetByLocation(ddlYear.SelectedValue, StrNull2Zero(ddlLocation.SelectedValue))

        If dt.Rows.Count > 0 Then

            ddlPhase.Items.Clear()
            If Request.Cookies("LANG").Value.ToLower() = "en" Then
                ddlPhase.Items.Add("---All---")
            Else
                ddlPhase.Items.Add("---All---")
            End If

            ddlPhase.Items(0).Value = 0

            For i = 1 To dt.Rows.Count
                With ddlPhase
                    .Items.Add(dt.Rows(i - 1)("Descriptions"))
                    .Items(i).Value = dt.Rows(i - 1)("PhaseID")
                End With
            Next

            With ddlPhase
                '.Visible = True
                '.DataSource = dt
                '.DataTextField = "Name"
                '.DataValueField = "Code"
                '.DataBind()
                .SelectedIndex = 0
            End With
        Else
            ddlPhase.DataSource = dt
            ddlPhase.DataBind()
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
    Private Sub LoadResultProcess()
        dt = ctlAss.Assessment_GetByLocation(ddlYear.SelectedValue, ddlSubject.SelectedValue, StrNull2Zero(ddlPhase.SelectedValue), StrNull2Zero(ddlLocation.SelectedValue))
        If dt.Rows.Count > 0 Then

            'cmdPrint.Visible = True
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
                For i = 0 To dt.Rows.Count - 1
                    .Rows(i).Cells(0).Text = i + 1
                    '.Rows(i).Cells(2).Text = dt.Rows(i)("Prefix") & dt.Rows(i)("FirstName") & " " & dt.Rows(i)("LastName")
                    'Dim lblTH As Label = .Rows(i).Cells(3).FindControl("lblTH")
                    'lblTH.Text = dt.Rows(i)("NameTH")
                Next
            End With
        Else
            'cmdPrint.Visible = False
            grdData.Visible = False
        End If
        dt = Nothing
    End Sub
    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound

        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            'Dim sSubjCode As String
            'sSubjCode = e.Row.Cells(1).Text
            'Dim lnkV As HyperLink = e.Row.Cells(6).FindControl("hlnkView")
            'lnkV.NavigateUrl = "ReportViewerStudentBio.aspx?ActionType=mnuPrintBio&id=" & grdData.DataKeys(e.Row.RowIndex).Value


            e.Row.Cells(6).Text = "<a target='_blank' href='ReportViewerStudentBio.aspx?ActionType=mnuPrintBio&id=" & grdData.DataKeys(e.Row.RowIndex).Value & "'><img src='images/printer.png'></a>"


        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If

    End Sub

    'Private Sub cmdPrint_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdPrint.Click

    '    FagRPT = ""

    '    'Dim fRptView As New ReportViewer

    '    'fRptView.FileName = "Reports/rptAssessmentByLocation.rpt"


    '    'fRptView.SelectionFomula = "{View_Student_Assessment.LocationID}=" & ctlPs.Person_GetLocationByUserID(Request.Cookies("UserID").Value) & " AND {View_Student_Assessment.PYear}=" & ddlYear.SelectedValue

    '    'If ddlSubject.SelectedValue <> "0" Then
    '    '    fRptView.SelectionFomula &= " AND {View_Student_Assessment.SubjectCode}='" & ddlSubject.SelectedValue & "'"
    '    'End If

    '    'If ddlPhase.SelectedValue <> "0" Then
    '    '    fRptView.SelectionFomula &= " AND {View_Student_Assessment.TimePhaseID}=" & ddlPhase.SelectedValue
    '    'End If

    '    'Response.Redirect("ReportViewer.aspx")


    'End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadCourseToDDL()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadResultProcess()
    End Sub

    Private Sub LoadCourseToDDL()
        ddlSubject.Items.Clear()
        dt = ctlCs.Courses_GetByLocation(ddlYear.SelectedValue, StrNull2Zero(ddlLocation.SelectedValue))
        If dt.Rows.Count > 0 Then
            ddlSubject.Items.Clear()
            ddlSubject.Items.Add("---All---")
            ddlSubject.Items(0).Value = 0

            For i = 1 To dt.Rows.Count
                With ddlSubject
                    .Items.Add("" & dt.Rows(i - 1)("SubjectCode") & " : " & dt.Rows(i - 1)("SubjectName"))
                    .Items(i).Value = dt.Rows(i - 1)("SubjectCode")
                End With
            Next
            ddlSubject.SelectedIndex = 0
        End If
    End Sub

    Protected Sub ddlSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject.SelectedIndexChanged
        LoadResultProcess()
    End Sub

    Protected Sub ddlPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPhase.SelectedIndexChanged
        LoadResultProcess()
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        LoadCourseToDDL()
    End Sub
End Class

