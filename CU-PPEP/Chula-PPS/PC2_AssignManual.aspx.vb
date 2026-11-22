
Public Class PC2_AssignManual
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlA As New PC2Controller
    Dim acc As New UserController

    'Dim strSubj() As String
    'Dim gSubjCode As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then

            LoadYearToDDL()
            LoadSubjectToDDL()

            ddlSubject.SelectedIndex = 0
            grdStudent.PageIndex = 0
            grdData.PageIndex = 0
            LoadStudentNoAssessmentToGrid()
            LoadStudentAssessmentToGrid()

        End If
    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlA.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlA.PC2_GetYear
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "EduYear"
                .DataValueField = "EduYear"
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

    Private Sub LoadStudentNoAssessmentToGrid()

        dt = ctlA.PC2_Student_GetStudentNoAssessment(ddlYear.SelectedValue, Trim(txtSearchStd.Text))

        If dt.Rows.Count > 0 Then
            lblCount.Text = dt.Rows.Count
            lblNo.Visible = False
            With grdStudent
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        Else
            lblCount.Text = 0
            lblNo.Visible = True
            grdStudent.Visible = False
            grdStudent.DataSource = Nothing
        End If

        dt = Nothing
    End Sub

    Private Sub LoadStudentAssessmentToGrid()
        Dim ctlREQ As New PC2Controller

        dt = ctlREQ.PC2_Assessment_Get(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlSubject.SelectedValue), Trim(txtSearch.Text))
        If dt.Rows.Count > 0 Then

            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
            lblAssmCount.Text = dt.Rows.Count.ToString
        Else
            grdData.Visible = False
        End If
        dt = Nothing
    End Sub
    Private Sub LoadSubjectToDDL()
        ddlSubject.Items.Clear()
        dt = ctlA.PC2_Subject_GetByYear(ddlYear.SelectedValue)
        If dt.Rows.Count > 0 Then
            With ddlSubject
                .Enabled = True
                .DataSource = dt
                .DataTextField = "NameTH"
                .DataValueField = "UID"
                .DataBind()
            End With
            ddlSubject.SelectedIndex = 0
        End If
    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadStudentAssessmentToGrid()
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    ctlA.PC2_Assessment_Delete(e.CommandArgument)
                    LoadStudentAssessmentToGrid()
                    LoadStudentNoAssessmentToGrid()
            End Select
        End If
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub
    Private Sub AddStudentToAssessment()

        Dim item, RYear, SubjectUID As Integer
        Dim sStdCode, strA As String
        Dim bDup As Boolean = False

        RYear = StrNull2Zero(ddlYear.SelectedValue)
        SubjectUID = StrNull2Zero(ddlSubject.SelectedValue)

        For i = 0 To grdStudent.Rows.Count - 1
            With grdStudent
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkStd")
                If chkS.Checked Then

                    sStdCode = .Rows(i).Cells(1).Text
                    If ctlA.PC2_Assessment_GetCheckDup(RYear, sStdCode) <= 0 Then

                        item = ctlA.PC2_Assessment_Add(RYear, SubjectUID, sStdCode, Request.Cookies("UserID").Value)

                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "PC2Assessment", "คัดเลือกนิสิตด้วยวิธี Manual :" & ddlYear.SelectedValue & ">>" & ddlSubject.SelectedItem.Text, "")

                    Else
                        bDup = True
                    End If
                End If

            End With
        Next
        LoadStudentNoAssessmentToGrid()
        LoadStudentAssessmentToGrid()
        If bDup = True Then
            strA = "และ มีนิสิตบางคนไม่สามารถกำหนดให้ได้ เนื่องจากผลัดฝึกซ้ำ"
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย" & strA & "');", True)
    End Sub

    Private Sub grdStudent_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdStudent.PageIndexChanging
        grdStudent.PageIndex = e.NewPageIndex
        LoadStudentNoAssessmentToGrid()
    End Sub
    Private Sub grdStudent_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdStudent.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        AddStudentToAssessment()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
               For i = 0 To grdStudent.Rows.Count - 1
            With grdStudent
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkStd")
                chkS.Checked = False
            End With
        Next
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged

        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadSubjectToDDL()


        grdStudent.PageIndex = 0
        grdData.PageIndex = 0

        LoadStudentNoAssessmentToGrid()
        LoadStudentAssessmentToGrid()
    End Sub

    Protected Sub ddlSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject.SelectedIndexChanged

        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadStudentNoAssessmentToGrid()
        LoadStudentAssessmentToGrid()
    End Sub

    Protected Sub cmdFindStd_Click(sender As Object, e As EventArgs) Handles cmdFindStd.Click
        grdStudent.PageIndex = 0
        LoadStudentNoAssessmentToGrid()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadStudentAssessmentToGrid()
    End Sub

End Class

