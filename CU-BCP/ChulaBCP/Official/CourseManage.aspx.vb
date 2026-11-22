Public Class CourseManage
    Inherits System.Web.UI.Page
    Dim ctlC As New CourseController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LoadCourse()
        End If
    End Sub

    Private Sub LoadCourse()
        dt = ctlC.Course_GetAll
        If dt.Rows.Count > 0 Then
            grdData.DataSource = dt
            grdData.DataBind()
        End If
    End Sub
    Private Sub EditCourse(CourseID As Integer)
        dt = ctlC.Course_GetByID(CourseID)
        If dt.Rows.Count > 0 Then
            lblCourseID.Text = CourseID
            txtNameTH.Text = String.Concat(dt.Rows(0)("Name_TH"))
            txtNameEN.Text = String.Concat(dt.Rows(0)("Name_EN"))
            txtShortNameTH.Text = String.Concat(dt.Rows(0)("ShortName_TH"))
            txtShortNameEN.Text = String.Concat(dt.Rows(0)("ShortName_EN"))
            txtDetailTH.Html = String.Concat(dt.Rows(0)("CourseDetail_TH"))
            txtDetailEN.Html = String.Concat(dt.Rows(0)("CourseDetail_EN"))
            ddlType.SelectedValue = dt.Rows(0)("CourseTypeID")
        End If
        dt = Nothing
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditCourse(e.CommandArgument())
                Case "imgDel"
                    ctlC.Course_Delete(e.CommandArgument())
                    LoadCourse()
            End Select
        End If
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(4).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#c1ffc2';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        ctlC.Course_Save(StrNull2Zero(lblCourseID.Text), StrNull2Zero(ddlType.SelectedValue), txtNameTH.Text, txtNameEN.Text, txtShortNameTH.Text, txtShortNameEN.Text, txtDetailTH.Html, txtDetailEN.Html, Session("userid"))

        LoadCourse()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกเรียบร้อย');", True)

        lblCourseID.Text = ""
        txtNameTH.Text = ""
        txtNameEN.Text = ""
        txtShortNameTH.Text = ""
        txtShortNameEN.Text = ""
        txtDetailTH.Html = ""
        txtDetailEN.Html = ""
    End Sub
End Class