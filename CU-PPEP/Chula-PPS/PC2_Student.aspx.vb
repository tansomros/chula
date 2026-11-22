
Public Class PC2_Student
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlCs As New PC2Controller
    'Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblNotic.Visible = False
            LoadYearToDDL()
            LoadMajorToDDL()

            LoadStudentInPC2ToGrid()
            LoadStudentNotPC2ToGrid()

        End If
    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlCs.PC2_GetYear
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
    Private Sub LoadMajorToDDL()
        Dim ctlFct As New FacultyController
        dt = ctlFct.GetMajor
        If dt.Rows.Count > 0 Then
            With ddlMajorAdd
                .Enabled = True
                .DataSource = dt
                .DataTextField = "MajorName"
                .DataValueField = "MajorID"
                .DataBind()
                .SelectedIndex = 0
            End With

            With ddlMajorDel
                .Enabled = True
                .DataSource = dt
                .DataTextField = "MajorName"
                .DataValueField = "MajorID"
                .DataBind()
                .SelectedIndex = 0
            End With

            With ddlMajorSearch
                .Enabled = True
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

    Private Sub LoadStudentNotPC2ToGrid()
        If StrNull2Zero(ddlYear.SelectedValue) <> 0 Or StrNull2Zero(ddlLevel.SelectedValue) > 0 Then
            dt = ctlCs.PC2_Student_GetStudentNoPC2(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlMajorSearch.SelectedValue), StrNull2Zero(ddlLevelSearch.SelectedValue), Trim(txtSearch.Text))
            If dt.Rows.Count > 0 Then

                With grdData
                    .Visible = True
                    .DataSource = dt
                    .DataBind()
                End With
                lblNotic.Visible = False
            Else
                grdData.Visible = False
                lblNotic.Visible = True
            End If
        Else
            grdData.Visible = False
        End If

    End Sub

    Private Sub LoadStudentInPC2ToGrid()

        If StrNull2Zero(ddlYear.SelectedValue) <> 0 Or StrNull2Zero(ddlLevel.SelectedValue) > 0 Then
            dt = ctlCs.PC2_Student_GetStudentInPC2(StrNull2Zero(ddlYear.SelectedValue), Trim(txtSearchStd.Text))


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
            End If
        Else
            lblCount.Text = 0
            lblNo.Visible = True
            grdStudent.Visible = False
        End If
    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadStudentNotPC2ToGrid()
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub
    Private Sub AddStudentToPC2()
        Dim item As Integer
        For i = 0 To grdData.Rows.Count - 1
            With grdData
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                If chkS.Checked Then
                    item = ctlCs.PC2_Student_Add(StrNull2Zero(ddlYear.SelectedValue), .Rows(i).Cells(1).Text)
                End If
            End With
        Next
        LoadStudentInPC2ToGrid()
        LoadStudentNotPC2ToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Private Sub grdStudent_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdStudent.PageIndexChanging
        grdStudent.PageIndex = e.NewPageIndex
        LoadStudentInPC2ToGrid()
    End Sub

    Private Sub grdPC2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdStudent.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    If ctlCs.PC2_Student_Delete(e.CommandArgument) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ลบข้อมูลเรียบร้อย');", True)
                        LoadStudentInPC2ToGrid()
                        LoadStudentNotPC2ToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub

    Private Sub grdPC2_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdStudent.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(6).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        AddStudentToPC2()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        For i = 0 To grdData.Rows.Count - 1
            With grdData
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                chkS.Checked = False
            End With
        Next
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged

        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadStudentInPC2ToGrid()
        LoadStudentNotPC2ToGrid()
    End Sub

    Protected Sub cmdFindStd_Click(sender As Object, e As EventArgs) Handles cmdFindStd.Click
        grdStudent.PageIndex = 0
        LoadStudentInPC2ToGrid()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadStudentNotPC2ToGrid()
    End Sub

    Protected Sub lnkSubmitDel_Click(sender As Object, e As EventArgs) Handles lnkSubmitDel.Click
        ctlCs.PC2_Student_DeleteByMajorAndLevel(ddlYear.SelectedValue, ddlMajorDel.SelectedValue, ddlLevelDel.SelectedValue)

        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadStudentInPC2ToGrid()
        LoadStudentNotPC2ToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ลบเรียบร้อย');", True)
    End Sub

    Protected Sub lnkSubmitAdd_Click(sender As Object, e As EventArgs) Handles lnkSubmitAdd.Click
        Dim ctlStd As New StudentController
        dt = ctlStd.GetStudent_ByMajorAndLevel(ddlMajorAdd.SelectedValue, ddlLevelAdd.SelectedValue)
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                ctlCs.PC2_Student_Add(ddlYear.SelectedValue, dt.Rows(i)("Student_Code"))
            Next
        End If


        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadStudentInPC2ToGrid()
        LoadStudentNotPC2ToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกเรียบร้อย');", True)
    End Sub

End Class

