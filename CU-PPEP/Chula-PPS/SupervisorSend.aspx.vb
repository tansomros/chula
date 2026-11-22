
Public Class SupervisorSend
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlC As New Coursecontroller
    Dim ctlSpv As New SupervisorController

    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then

            LoadYearToDDL()
            LoadCourseToDDL()

            LoadTeacherInSendToGrid()
            LoadTeacherNotSendToGrid()

        End If
    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlC.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlC.Courses_GetYear
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
    Private Sub LoadTeacherNotSendToGrid()
        If StrNull2Zero(ddlCourse.SelectedValue) <> 0 Then
            dt = ctlSpv.Supervisor_GetNotSendJob(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue), Trim(txtSearch.Text))
            If dt.Rows.Count > 0 Then

                With grdData
                    .Visible = True
                    .DataSource = dt
                    .DataBind()
                End With
                cmdSave.Visible = True
                cmdClear.Visible = True
            Else
                grdData.Visible = False
                cmdSave.Visible = False
                cmdClear.Visible = False
            End If
        Else
            grdData.Visible = False
            cmdSave.Visible = False
            cmdClear.Visible = False
        End If

    End Sub

    Private Sub LoadTeacherInSendToGrid()

        If StrNull2Zero(ddlCourse.SelectedValue) <> 0 Then
            dt = ctlSpv.Supervisor_GetSendJob(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue), Trim(txtSearchStd.Text))


            If dt.Rows.Count > 0 Then
                lblCount.Text = dt.Rows.Count
                lblNo.Visible = False
                With grdSend
                    .Visible = True
                    .DataSource = dt
                    .DataBind()
                End With
            Else
                lblCount.Text = 0
                lblNo.Visible = True
                grdSend.Visible = False
            End If
        Else
            lblCount.Text = 0
            lblNo.Visible = True
            grdSend.Visible = False
        End If
    End Sub

    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()
        dt = ctlC.Courses_GetByYear(ddlYear.SelectedValue)
        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With ddlCourse
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("NameTH"))
                    .Items(i).Value = dt.Rows(i)("CourseID")
                End With
            Next
            lblNot.Visible = False
        Else
            ddlCourse.Items.Clear()
            lblNot.Visible = True
        End If

    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadTeacherNotSendToGrid()
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub
    Private Sub UpdateStatusSendJob()

        Dim item As Integer

        For i = 0 To grdData.Rows.Count - 1
            With grdData
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                If chkS.Checked Then
                    item = ctlSpv.Supervisor_UpdateSendJob(.DataKeys(i).Value, "Y", Request.Cookies("Username").Value)
                End If


            End With
        Next
        LoadTeacherInSendToGrid()
        LoadTeacherNotSendToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Private Sub grdSend_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSend.PageIndexChanging
        grdSend.PageIndex = e.NewPageIndex
        LoadTeacherInSendToGrid()
    End Sub

    Private Sub grdCourse_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdSend.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    If ctlSpv.Supervisor_UpdateSendJob(e.CommandArgument, "N", Request.Cookies("Username").Value) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadTeacherInSendToGrid()
                        LoadTeacherNotSendToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub

    Private Sub grdCourse_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSend.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(5).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        UpdateStatusSendJob()
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

        grdSend.PageIndex = 0
        grdData.PageIndex = 0
        LoadCourseToDDL()
        LoadTeacherInSendToGrid()
        LoadTeacherNotSendToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged

        grdSend.PageIndex = 0
        grdData.PageIndex = 0
        LoadTeacherInSendToGrid()
        LoadTeacherNotSendToGrid()
    End Sub

    Protected Sub cmdFindStd_Click(sender As Object, e As EventArgs) Handles cmdFindStd.Click
        grdSend.PageIndex = 0
        LoadTeacherInSendToGrid()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadTeacherNotSendToGrid()
    End Sub
End Class

