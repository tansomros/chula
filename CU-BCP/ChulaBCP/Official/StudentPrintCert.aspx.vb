Imports System.IO
Imports System.Data.OleDb
Imports System.Data
Imports Subgurim.Controles

Public Class StudentPrintCert
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlStd As New StudentController
    Dim objUser As New UserController
    Dim ctlFct As New FacultyController
    Dim ctlPsn As New PersonController

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            grdData.PageIndex = 0
            isAdd = True
            lnkPrintAll.Visible = False
            lnkPrintSelect.Visible = False

            Dim ctlCfg As New SystemConfigController

            txtEduYear.Text = ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR)

            LoadMajorToDDL()
            LoadAdvisorToDDL()
            'LoadStudent()
        End If


    End Sub
    Dim objPsn As New PersonInfo

    Private Sub LoadAdvisorToDDL()

        dt = ctlPsn.GetPerson_ByType("T")

        ddlAdvisor.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlAdvisor
                .Enabled = True
                .Items.Add("--ไม่ระบุ--")
                .Items(0).Value = 0
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & dt.Rows(i)("FirstName") & " " & dt.Rows(i)("LastName"))
                    .Items(i + 1).Value = dt.Rows(i)("PersonID")
                Next

                .SelectedIndex = 0

            End With

        End If
    End Sub


    Private Sub LoadMajorToDDL()
        dt = ctlFct.GetMajor
        ddlMajor.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlMajor
                .Enabled = True
                .Items.Add("--ไม่ระบุ--")
                .Items(0).Value = 0
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add(dt.Rows(i)("MajorName"))
                    .Items(i + 1).Value = dt.Rows(i)("MajorID")
                Next
                '.DataSource = dt
                '.DataTextField = "MajorName"
                '.DataValueField = "MajorID"
                '.DataBind()
                .SelectedIndex = 0
            End With
        Else

        End If
        dt = Nothing
    End Sub

    Private Sub LoadMinorToDDL()
        dt = ctlFct.GetMinor_ByMajorID(ddlMajor.SelectedValue)
        ddlMinor.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlMinor
                .Enabled = True
                .Items.Add("--ไม่ระบุ--")
                .Items(0).Value = 0
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add(dt.Rows(i)("MinorName"))
                    .Items(i + 1).Value = dt.Rows(i)("MinorID")
                Next

                '.DataSource = dt
                '.DataTextField = "MinorName"
                '.DataValueField = "MinorID"
                '.DataBind()
                .SelectedIndex = 0
            End With
        Else
            ddlMinor.Items.Clear()
            ddlSubMinor.Items.Clear()
            ddlMinor.DataSource = Nothing
            ddlMinor.Enabled = False
            ddlSubMinor.Enabled = False
        End If
        dt = Nothing
    End Sub

    Private Sub LoadSubMinorToDDL()
        dt = ctlFct.GetSubMinor_ByMinorID(ddlMinor.SelectedValue)
        ddlSubMinor.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlSubMinor
                .Enabled = True
                .Items.Add("--ไม่ระบุ--")
                .Items(0).Value = 0
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add(dt.Rows(i)("SubMinorName"))
                    .Items(i + 1).Value = dt.Rows(i)("SubMinorID")
                Next

                '.DataSource = dt
                '.DataTextField = "SubMinorName"
                '.DataValueField = "SubMinorID"
                '.DataBind()
                .SelectedIndex = 0
            End With
        Else
            ddlSubMinor.Items.Clear()
            ddlSubMinor.DataSource = Nothing
            ddlSubMinor.Enabled = False
        End If
        dt = Nothing
    End Sub


    Private Sub ddlMajor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMajor.SelectedIndexChanged
        LoadMinorToDDL()

        If ddlMinor.SelectedIndex = 0 Then
            LoadSubMinorToDDL()
        End If
    End Sub

    Private Sub ddlMinor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMinor.SelectedIndexChanged
        LoadSubMinorToDDL()
    End Sub

    Private Sub LoadStudent()
        If Not paramValidate() Then
            DisplayMessage(Me.Page, "ท่านต้องกำหนดค่าพารามิเตอร์ให้ครบถ้วนก่อน")
            Exit Sub
        End If

        dt = ctlStd.GetStudent_BySearch4Print(ddlMajor.SelectedValue, ddlMinor.SelectedValue, ddlSubMinor.SelectedValue, ddlAdvisor.SelectedValue, ddlLevel.SelectedValue, txtSearch.Text)

        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With

            lnkPrintAll.Visible = True
            lnkPrintSelect.Visible = True
        Else
            grdData.Visible = False
            lnkPrintAll.Visible = False
            lnkPrintSelect.Visible = False
        End If


    End Sub
    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadStudent()
    End Sub
    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound

        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            'e.Row.Cells(8).Text = "<a target='_blank' href='ReportViewerStudentBio.aspx?ActionType=mnuPrintBio&id=" & e.Row.Cells(1).Text & "'><img src='images/printer.png'></a>"
                    e.Row.Cells(8).Text = "<a target='_blank' href='ReportViewerStudentBio.aspx?m=cert&y=" + txtEduYear.Text + "&d=" + txtCertDate.Text + "&id=" & grdData.DataKeys(e.Row.RowIndex).Value & "'><img src='images/printer.png'></a>"



        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Function paramValidate() As Boolean
        If txtEduYear.Text = "" Then
            Return False
            Exit Function
        End If
        If txtCertDate.Text = "" Then
            Return False
            Exit Function
        End If

        Return True

    End Function


    Private Sub lnkPrintAll_Click(sender As Object, e As EventArgs) Handles lnkPrintAll.Click
        Dim str As String = ""

        If Not paramValidate() Then
            DisplayMessage(Me.Page, "ท่านต้องกำหนดค่าพารามิเตอร์ให้ครบถ้วนก่อน")
            Exit Sub
        End If

        'str = str & " (Student_Code Like ''%" & txtSearch.Text & "%'' OR FirstName like  ''%" & txtSearch.Text & "%''   OR  LastName like  ''%" & txtSearch.Text & "%'') "
        str = " Student_Code<>'' "

        If StrNull2Zero(ddlMajor.SelectedValue) <> 0 Then
            str &= " AND MajorID=" & StrNull2Zero(ddlMajor.SelectedValue)
        End If

        If StrNull2Zero(ddlMinor.SelectedValue) <> 0 Then
            str &= " And MinorID=" & StrNull2Zero(ddlMinor.SelectedValue)
        End If

        If StrNull2Zero(ddlSubMinor.SelectedValue) <> 0 Then
            str &= " And SubMinorID=" & StrNull2Zero(ddlSubMinor.SelectedValue)
        End If

        If StrNull2Zero(ddlAdvisor.SelectedValue) <> 0 Then
            str &= " And AdvisorID=" & StrNull2Zero(ddlAdvisor.SelectedValue)
        End If

        If StrNull2Zero(ddlLevel.SelectedValue) <> 0 Then
            str &= " And LevelClass=" & StrNull2Zero(ddlLevel.SelectedValue)
        End If

        If txtSearch.Text.TrimStart().TrimEnd() <> "" Then
            str &= " And (Student_Code like '%" & txtSearch.Text & "%' OR FirstName like '%" & txtSearch.Text & "%' OR  LastName like '%" & txtSearch.Text & "%')   "
        End If

        ReportViewerStudentBio.ReportFormula = str

        Response.Redirect("ReportViewerStudentBio.aspx?m=cert&y=" + txtEduYear.Text + "&d=" + txtCertDate.Text)
    End Sub

    Private Sub lnkPrintSelect_Click(sender As Object, e As EventArgs) Handles lnkPrintSelect.Click

        If Not paramValidate() Then
            DisplayMessage(Me.Page, "ท่านต้องกำหนดค่าพารามิเตอร์ให้ครบถ้วนก่อน")
            Exit Sub
        End If

        Dim str As String = ""
        Dim n As Integer = 0

        For i = 0 To grdData.Rows.Count - 1
            With grdData
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                If chkS.Checked Then
                    n = n + 1
                    'str = str & "'" & .Rows(i).Cells(1).Text & "'" & ","
                    str = str & .DataKeys(i).Value & ","

                End If
            End With
        Next
        str = str & "0"
        str = "(" & str & ")"
        If n <> 0 Then
            ReportViewerStudentBio.ReportFormula = "Student_ID in  " & str
            Response.Redirect("ReportViewerStudentBio.aspx?m=cert&y=" + txtEduYear.Text + "&d=" + txtCertDate.Text)
        Else
            ReportViewerStudentBio.ReportFormula = ""
            DisplayMessage(Me.Page, "ท่านยังไม่ได้เลือกนิสิต")

        End If

    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        grdData.PageIndex = 0
        LoadStudent()
    End Sub
End Class