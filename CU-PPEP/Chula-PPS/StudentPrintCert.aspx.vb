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
    Dim ctlCfg As New SystemConfigController
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If


        If Not IsPostBack Then
            FagRPT = "cert"
            grdData.PageIndex = 0
            isAdd = True
            lnkPrintAll.Visible = False
            lnkPrintSelect.Visible = False

            LoadYearToDDL()
            LoadMajorToDDL()
            'LoadAdvisorToDDL()
            LoadStudent()
        End If


    End Sub
    Private Sub LoadYearToDDL()
        dt = ctlCfg.Certification_GetYear()
        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "EduYear"
                .DataValueField = "EduYear"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        dt = Nothing
    End Sub

    'Private Sub LoadAdvisorToDDL()

    '    dt = ctlPsn.Persons_GetByType("T")

    '    ddlAdvisor.Items.Clear()
    '    If dt.Rows.Count > 0 Then
    '        With ddlAdvisor
    '            .Enabled = True
    '            '.Items.Add("--ไม่ระบุ--")
    '            '.Items(0).Value = 0
    '            'For i = 0 To dt.Rows.Count - 1
    '            '    .Items.Add("" & dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f02_FirstName).fldName) & " " & dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f03_LastName).fldName))
    '            '    .Items(i + 1).Value = dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f00_PersonID).fldName)
    '            'Next
    '            .DataSource = dt
    '            .DataTextField = "PersonName"
    '            .DataValueField = "PersonID"
    '            .DataBind()
    '            .SelectedIndex = 0

    '        End With

    '    End If
    'End Sub


    Private Sub LoadMajorToDDL()
        dt = ctlFct.GetMajor
        ddlMajor.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlMajor
                .Enabled = True
                '.Items.Add("--ไม่ระบุ--")
                '.Items(0).Value = 0
                'For i = 0 To dt.Rows.Count - 1
                '    .Items.Add(dt.Rows(i)("MajorName"))
                '    .Items(i + 1).Value = dt.Rows(i)("MajorID")
                'Next
                .DataSource = dt
                .DataTextField = "MajorName"
                .DataValueField = "MajorID"
                .DataBind()
                .SelectedIndex = 0
            End With
        Else

        End If
        dt = Nothing
    End Sub

    Private Sub LoadStudent()

        dt = ctlStd.GetStudent_BySearch4Print(ddlMajor.SelectedValue, "0", "0", ddlLevel.SelectedValue, txtSearch.Text)

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
            e.Row.Cells(8).Text = "<a target='_blank' href='ReportViewerStudentBio.aspx?ActionType=std&ItemType=cert&m=cert&t=pdf&y=" + ddlYear.SelectedValue.ToString() & "&id=" & grdData.DataKeys(e.Row.RowIndex).Value & "'><img src='images/pdffile.png?v=1' width='28px'></a>"
            e.Row.Cells(9).Text = "<a target='_blank' href='ReportStudentBioView.aspx?ActionType=std&ItemType=cert&m=cert&t=wrd&y=" + ddlYear.SelectedValue.ToString() & "&id=" & grdData.DataKeys(e.Row.RowIndex).Value & "'><img src='images/word32.png' width='24px'></a>"
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If

    End Sub

    Private Sub lnkPrintAll_Click(sender As Object, e As EventArgs) Handles lnkPrintAll.Click
        ExportAll("pdf")
    End Sub

    Private Sub ExportAll(flag As String)

        Dim str As String = ""

        'str = str & " (Student_Code Like ''%" & txtSearch.Text & "%'' OR FirstName like  ''%" & txtSearch.Text & "%''   OR  LastName like  ''%" & txtSearch.Text & "%'') "
        str = " Student_Code<>'' "

        If StrNull2Zero(ddlMajor.SelectedValue) <> 0 Then
            str &= " AND MajorID=" & StrNull2Zero(ddlMajor.SelectedValue)
        End If


        'If StrNull2Zero(ddlSubMinor.SelectedValue) <> 0 Then
        '    str &= " And SubMinorID=" & StrNull2Zero(ddlSubMinor.SelectedValue)
        'End If

        'If StrNull2Zero(ddlAdvisor.SelectedValue) <> 0 Then
        '    str &= " And AdvisorID=" & StrNull2Zero(ddlAdvisor.SelectedValue)
        'End If

        If StrNull2Zero(ddlLevel.SelectedValue) <> 0 Then
            If StrNull2Zero(ddlLevel.SelectedValue) > 6 Then
                str &= " And LevelClass>6 AND StudentStatus=40"
            Else
                str &= " And LevelClass=" & StrNull2Zero(ddlLevel.SelectedValue)
            End If

        End If

        If txtSearch.Text.TrimStart().TrimEnd() <> "" Then
            str &= " And (Student_Code like '%" & txtSearch.Text & "%' OR FirstName like '%" & txtSearch.Text & "%' OR  LastName like '%" & txtSearch.Text & "%')   "
        End If

        ReportViewerStudentBio.ReportFormula = str
        'ReportServerViewer.ReportFormula = str
        If flag = "pdf" Then
            Response.Redirect("ReportViewerStudentBio.aspx?ActionType=std&ItemType=cert&t=pdf&m=cert&y=" + ddlYear.SelectedValue)
        Else
            Response.Redirect("ReportStudentBioView.aspx?ActionType=std&ItemType=cert&t=wrd&m=cert&y=" + ddlYear.SelectedValue)
        End If

    End Sub

    Private Sub lnkPrintSelect_Click(sender As Object, e As EventArgs) Handles lnkPrintSelect.Click
        ExportBySelected("pdf")
    End Sub
    Private Sub ExportBySelected(flag As String)
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

            If flag = "pdf" Then
                Response.Redirect("ReportViewerStudentBio.aspx?ActionType=std&ItemType=cert&t=pdf&m=cert&y=" + ddlYear.SelectedValue)
            Else
                GenerateData("", "Student_ID in  " & str)
                Response.Redirect("ReportStudentBioView.aspx?ActionType=std&ItemType=cert&m=cert&t=wrd&y=" + ddlYear.SelectedValue)
            End If
        Else
            ReportViewerStudentBio.ReportFormula = ""
            DisplayMessage(Me.Page, "ท่านยังไม่ได้เลือกนิสิต")
        End If

    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        grdData.PageIndex = 0
        LoadStudent()
    End Sub

    Private Sub lnkExportSelect_Click(sender As Object, e As EventArgs) Handles lnkExportSelect.Click
        ExportBySelected("word")
    End Sub

    Private Sub lnkExportAll_Click(sender As Object, e As EventArgs) Handles lnkExportAll.Click
        ExportAll("word")
    End Sub
    Private Function CertificateOnlineTime() As Boolean
        Dim ctlCfg As New SystemConfigController
        Dim StartDate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_CERTDATE)))
        Dim sToday As Integer = StrNull2Zero(ConvertDate2DBString(ctlCfg.GET_DATE_SERVER))
        Dim bAvailable As Boolean
        If sToday < StartDate Then
            bAvailable = False
        Else
            bAvailable = True
        End If
        Return bAvailable
    End Function
    Private Sub GenerateData(pID As String, ReportFormula As String)

        Dim ctlstd As New StudentController

        Select Case Request("m")
            Case "cert"
                'lblReportTitle.Text = "พิมพ์ใบรับรองการผ่านการฝึกปฏิบัติงาน"
                Session("rptyear") = ddlYear.SelectedValue
                'Session("rptcertdate") = Request("d")
                Session("m") = "cert"

                If pID <> "" Then
                    ctlstd.TMP_Student_SET2CERTBySTDID(Request("id"), Request.Cookies("Username").Value)
                Else
                    If ReportFormula <> "" Then
                        ctlstd.TMP_Student_SET2CERT(ReportFormula, Request.Cookies("Username").Value)
                    End If
                End If
            Case "certregis"
                'lblReportTitle.Text = "พิมพ์แบบรายงานการฝึกปฏิบัติงานสำหรับผู้สมัครสอบความรู้เพื่อขึ้นทะเบียนเป็นผู้ประกอบวิชาชีพเภสัชกรรม"
                Session("m") = "certregis"
                If pID <> "" Then
                    ctlstd.TMP_Student_SET2CERTBySTDID(Request("id"), Request.Cookies("Username").Value)
                Else
                    If ReportFormula <> "" Then
                        ctlstd.TMP_Student_SET2CERT(ReportFormula, Request.Cookies("Username").Value)
                    End If
                End If
            Case Else
                Session("m") = "cert"
                'lblReportTitle.Text = "พิมพ์ประวัตินิสิต (Student Bio)"
                If pID <> "" Then
                    ctlstd.TMP_Student_SET2BIOBySTDID(Request("id"), Request.Cookies("Username").Value)
                Else
                    If ReportFormula <> "" Then
                        ctlstd.TMP_Student_SET2BIO(ReportFormula, Request.Cookies("Username").Value)
                    End If
                End If
        End Select



    End Sub

End Class