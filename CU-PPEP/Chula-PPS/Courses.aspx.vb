
Public Class Courses
    Inherits System.Web.UI.Page

    Dim ctlSubj As New SubjectController
    Dim dt As New DataTable
    Dim ctlCs As New courseController
    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            grdCourse.PageIndex = 0

            LoadYearToDDL()
            LoadSubjectToGrid()
            LoadCourseToGrid()
        End If

        lblYear.Text = ddlYear.SelectedValue
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

    Private Sub LoadSubjectToGrid()

        dt = ctlSubj.Subject_GetNoCourse(StrNull2Zero(ddlYear.SelectedValue))
        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        Else
            grdData.Visible = False
        End If
    End Sub

    Private Sub LoadCourseToGrid()
        dt = ctlCs.Courses_GetByYear(StrNull2Zero(ddlYear.SelectedValue))
        If dt.Rows.Count > 0 Then
            lblNo.Visible = False
            With grdCourse
            .Visible = True
            .DataSource = dt
                .DataBind()
            End With
        Else
            lblNo.Visible = True
            grdCourse.Visible = False
        End If
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub
    Private Sub AddSubjectToCourse()

        Dim item As Integer

        For i = 0 To grdData.Rows.Count - 1
            With grdData
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                If chkS.Checked Then
                    item = ctlCs.Courses_Add(ddlYear.SelectedValue, .Rows(i).Cells(1).Text, Request.Cookies("Username").Value)
                    acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Courses", "เพิ่ม รายวิชาเปิดฝึก:" & ddlYear.SelectedValue & ">>" & .Rows(i).Cells(1).Text, "")

                End If


            End With
        Next
        grdCourse.PageIndex = 0
        LoadCourseToGrid()
        LoadSubjectToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub
     
    Private Sub grdCourse_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdCourse.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgView"
                    Response.Redirect("CourseCoordinator.aspx?ActionType=setup&ItemType=y2&y=" & ddlYear.SelectedValue & "&id=" & e.CommandArgument())
                Case "imgDel"
                    If ctlCs.Courses_Delete(ddlYear.SelectedValue, e.CommandArgument) Then

                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "Courses", "ลบ รายวิชาเปิดฝึก:" & ddlYear.SelectedValue & ">>" & e.CommandArgument, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadSubjectToGrid()

                        grdCourse.PageIndex = 0

                        LoadCourseToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub

    Private Sub grdCourse_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCourse.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(4).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        AddSubjectToCourse()
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
        grdCourse.PageIndex = 0
        LoadCourseToGrid()
        LoadSubjectToGrid() 
    End Sub

    Protected Sub grdCourse_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdCourse.PageIndexChanging
        grdCourse.PageIndex = e.NewPageIndex
        LoadCourseToGrid()
    End Sub
End Class

