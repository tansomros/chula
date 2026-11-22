
Public Class CourseLocation
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlCs As New courseController
    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then

            LoadYearToDDL()
            LoadCourseToDDL()

            LoadGroupToDDL()

            LoadLocationInCourseToGrid()
            LoadLocationNotCourseToGrid()

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
    Private Sub LoadGroupToDDL()
        Dim ctlFct As New LocationGroupController
        dt = ctlFct.LocationGroup_Get
        If dt.Rows.Count > 0 Then
            With ddlLocationTypeAdd
                .Enabled = True
                .DataSource = dt
                .DataTextField = "Name"
                .DataValueField = "Code"
                .DataBind()
                .SelectedIndex = 0
            End With

            With ddlLocationTypeDel
                .Enabled = True
                .DataSource = dt
                .DataTextField = "Name"
                .DataValueField = "Code"
                .DataBind()
                .SelectedIndex = 0
            End With


        End If
        dt = Nothing
    End Sub

    Private Sub LoadLocationNotCourseToGrid()

        dt = ctlCs.CourseLocation_GetLocationNoCourse(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue), Trim(txtSearch.Text))
        If dt.Rows.Count > 0 Then

            With grdLocation
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        Else
            grdLocation.Visible = False
        End If
    End Sub

    Private Sub LoadLocationInCourseToGrid()

        dt = ctlCs.CourseLocation_GetLocationInCourse(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue), Trim(txtSearchLocation.Text))


        If dt.Rows.Count > 0 Then
            lblCount.Text = dt.Rows.Count
            lblNo.Visible = False
            With grdCourseLocation
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        Else
            lblCount.Text = 0
            lblNo.Visible = True
            grdCourseLocation.Visible = False
        End If
    End Sub

    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()

        If Request.Cookies("ROLE_ADM").Value = True Then
            dt = ctlCs.Courses_GetByYear(ddlYear.SelectedValue)
        Else
            If Request.Cookies("ROLE_ADV").Value = True Then
                dt = ctlCs.Courses_GetByCoordinator(ddlYear.SelectedValue, DBNull2Zero(Request.Cookies("ProfileID").Value))
            Else
                Exit Sub
            End If
        End If

        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With ddlCourse
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("NameTH"))
                    .Items(i).Value = dt.Rows(i)("CourseID")
                End With
            Next

        End If

    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLocation.PageIndexChanging
        grdLocation.PageIndex = e.NewPageIndex
        LoadLocationNotCourseToGrid()
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLocation.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub
    Private Sub AddLocationToCourse()

        Dim item As Integer

        For i = 0 To grdLocation.Rows.Count - 1
            With grdLocation
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                If chkS.Checked Then
                    item = ctlCs.CourseLocation_Add(ddlCourse.SelectedValue, .DataKeys(i).Value, "Y", "Y", Request.Cookies("Username").Value)
                    acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "CourseLocation", "เพิ่ม แหล่งฝึกในรายวิชาฝึก:" & ddlCourse.SelectedItem.Text & ">>" & .Rows(i).Cells(1).Text, "")

                End If


            End With
        Next
        LoadLocationInCourseToGrid()
        LoadLocationNotCourseToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Private Sub grdCourseLocation_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCourseLocation.PageIndexChanging
        grdCourseLocation.PageIndex = e.NewPageIndex
        LoadLocationInCourseToGrid()
    End Sub

    Private Sub grdCourse_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdCourseLocation.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    If ctlCs.CourseLocation_Delete(e.CommandArgument) Then
                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "CourseLocation", "ลบ แหล่งฝึกในรายวิชาฝึก:" & ddlCourse.SelectedItem.Text & ">>itemID:" & e.CommandArgument, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadLocationInCourseToGrid()
                        LoadLocationNotCourseToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub

    Private Sub grdCourse_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCourseLocation.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(3).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        AddLocationToCourse()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        For i = 0 To grdLocation.Rows.Count - 1
            With grdLocation
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                chkS.Checked = False
            End With
        Next
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged

        grdCourseLocation.PageIndex = 0
        grdLocation.PageIndex = 0
        LoadCourseToDDL()
        LoadLocationInCourseToGrid()
        LoadLocationNotCourseToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged

        grdCourseLocation.PageIndex = 0
        grdLocation.PageIndex = 0
        LoadLocationInCourseToGrid()
        LoadLocationNotCourseToGrid()
    End Sub

    Protected Sub cmdFindStd_Click(sender As Object, e As EventArgs) Handles cmdFindLocationInCourse.Click
        grdCourseLocation.PageIndex = 0
        LoadLocationInCourseToGrid()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdLocation.PageIndex = 0
        LoadLocationNotCourseToGrid()
    End Sub

    Protected Sub lnkSubmitDel_Click(sender As Object, e As EventArgs) Handles lnkSubmitDel.Click
        ctlCs.CourseLocation_DeleteByGroup(ddlLocationTypeDel.SelectedValue, ddlCourse.SelectedValue)

        grdCourseLocation.PageIndex = 0
        grdLocation.PageIndex = 0
        LoadLocationInCourseToGrid()
        LoadLocationNotCourseToGrid()

        DisplayMessage(Me.Page, "ลบเรียบร้อย")
    End Sub

    Protected Sub lnkSubmitAdd_Click(sender As Object, e As EventArgs) Handles lnkSubmitAdd.Click
        Dim ctlL As New LocationController

        dt = ctlL.Location_GetByGroupID(ddlLocationTypeAdd.SelectedValue)

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                ctlCs.CourseLocation_Add(ddlCourse.SelectedValue, dt.Rows(i)("LocationID"), "Y", "Y", Request.Cookies("Username").Value)
            Next
        End If

        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "CourseLocation", "เพิ่ม แหล่งฝึกในรายวิชาฝึกทั้งประเภท:" & ddlLocationTypeAdd.SelectedValue & ">>" & ddlCourse.SelectedItem.Text, "")

        grdCourseLocation.PageIndex = 0
        grdLocation.PageIndex = 0
        LoadLocationInCourseToGrid()
        LoadLocationNotCourseToGrid()

        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")

    End Sub
End Class

