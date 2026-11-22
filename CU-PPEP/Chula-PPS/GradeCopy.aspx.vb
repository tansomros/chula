
Public Class GradeCopy
    Inherits System.Web.UI.Page

    Dim ctlG As New AssessmentController
    Dim ctlCs As New CourseController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            LoadYearToDDL()
            LoadSubjectToDDL()
            LoadSubjectNoGradeToGrid()
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

            With ddlDesYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "YearText"
                .DataValueField = "YearValue"
                .DataBind()

                If dt.Rows(LastRow)(0) = y Then
                    ddlDesYear.Items.Add(y + 1)
                    ddlDesYear.Items(LastRow + 1).Value = y + 1
                ElseIf dt.Rows(LastRow)(0) > y Then
                    'ddlDesYear.Items.Add(y + 2)
                    'ddlDesYear.Items(LastRow + 1).Value = y + 2
                ElseIf dt.Rows(LastRow)(0) < y Then
                    ddlDesYear.Items.Add(y)
                    ddlDesYear.Items(LastRow + 1).Value = y
                    ddlDesYear.Items.Add(y + 1)
                    ddlDesYear.Items(LastRow + 2).Value = y + 1
                End If
                .SelectedIndex = 0
            End With

        Else
            ddlYear.Items.Add(y)
            ddlYear.Items(0).Value = y
            ddlYear.Items.Add(y + 1)
            ddlYear.Items(1).Value = y + 1
            ddlYear.SelectedIndex = 0

            ddlDesYear.Items.Add(y)
            ddlDesYear.Items(0).Value = y
            ddlDesYear.Items.Add(y + 1)
            ddlDesYear.Items(1).Value = y + 1
            ddlDesYear.SelectedIndex = 0
        End If
        ddlYear.SelectedValue = Request.Cookies("ASSMYEAR").Value
        ddlDesYear.SelectedValue = Request.Cookies("ASSMYEAR").Value
        dt = Nothing
    End Sub
    Private Sub LoadSubjectToDDL()
        Dim ctlSubj As New CourseController

        If Request.Cookies("ROLE_SPA").Value = True Or Request.Cookies("ROLE_ADM").Value = True Then
            dt = ctlSubj.Courses_Get4Selection(ddlYear.SelectedValue)
        Else
            dt = ctlSubj.Courses_GetByAdv4Selection(ddlYear.SelectedValue, Request.Cookies("ProfileID").Value)
        End If

        'dt = ctlSubj.Courses_GetByYear(ddlYear.SelectedValue)

        ddlSubject.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlSubject
                .DataSource = dt
                .DataTextField = "NameTH"
                .DataValueField = "SubjectCode"
                .DataBind()
            End With
        Else
            ddlSubject.Items.Clear()
            dt = Nothing
        End If

        'If dt.Rows.Count > 0 Then
        '    ddlSubject.Items.Clear()
        '    'With ddlSubject
        '    '    .Items.Add("-")
        '    '    .Items(0).Value = "-"
        '    'End With

        '    For i = 0 To dt.Rows.Count - 1
        '        With ddlSubject
        '            .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("NameTH"))
        '            .Items(i).Value = dt.Rows(i)("SubjectCode")
        '        End With
        '    Next
        'Else
        '    ddlSubject.Items.Clear()
        '    dt = Nothing
        'End If
    End Sub

    Private Sub LoadSubjectNoGradeToGrid()
        If Request.Cookies("ROLE_SPA").Value = True Or Request.Cookies("ROLE_ADM").Value = True Then
            dt = ctlG.Grade_GetSubjectNoGrade(StrNull2Zero(ddlDesYear.SelectedValue))
        Else
            dt = ctlG.Grade_GetSubjectNoGradeByAdv(StrNull2Zero(ddlDesYear.SelectedValue), Request.Cookies("ProfileID").Value)
        End If

        If dt.Rows.Count > 0 Then
            With grdSubject
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
            lblNo.Visible = False
            chkAll.Visible = True
            cmdSave.Visible = True
        Else
            lblNo.Visible = True
            chkAll.Visible = False
            cmdSave.Visible = False
        End If

        dt = Nothing
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubject.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim dtCs As New DataTable

        If chkAll.Checked Then
            If Request.Cookies("ROLE_SPA").Value = True Or Request.Cookies("ROLE_ADM").Value = True Then
                dtCs = ctlCs.Courses_GetByYear(ddlDesYear.SelectedValue)
            Else
                dtCs = ctlCs.Courses_GetByAdv(ddlDesYear.SelectedValue, Request.Cookies("ProfileID").Value)
            End If


            For i = 0 To dtCs.Rows.Count - 1
                ctlG.Grade_Copy(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlDesYear.SelectedValue), ddlSubject.SelectedValue, dtCs.Rows(i)("SubjectCode"))
            Next

        Else
            For i = 0 To grdSubject.Rows.Count - 1
                With grdSubject
                    Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                    If chkS.Checked Then
                        ctlG.Grade_Copy(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlDesYear.SelectedValue), ddlSubject.SelectedValue, .DataKeys(i).Value)
                    End If
                End With
            Next
        End If
        LoadSubjectNoGradeToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadSubjectToDDL()
    End Sub

    Private Sub ddlDesYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDesYear.SelectedIndexChanged
        LoadSubjectNoGradeToGrid()
    End Sub
End Class

