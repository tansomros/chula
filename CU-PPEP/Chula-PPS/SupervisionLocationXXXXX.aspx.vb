Public Class SupervisionLocationXXXXX
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlSpv As New SupervisorController
    Dim ctlCs As New Coursecontroller
    Dim ctlL As New LocationController

    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then

            LoadYearToDDL()
            LoadCourseToDDL()
            LoadLocationToDDL()
            LoadTimePhase()

            LoadProvinceToDDL()
            LoadLocationInSupervisorToGrid()
            LoadLocationNotSupervisorToGrid()
            'cboLocation.DropDownWidth = cboLocation.Width

        End If
    End Sub
    Private Sub LoadProvinceToDDL()
        dt = ctlCs.LoadProvince
        If dt.Rows.Count > 0 Then
            ddlProvince.Items.Add("---ทั้งหมด---")
            ddlProvince.Items(0).Value = "0"

            For i = 1 To dt.Rows.Count
                With ddlProvince
                    .Items.Add(dt.Rows(i - 1)("ProvinceName"))
                    .Items(i).Value = dt.Rows(i - 1)("ProvinceID")

                End With
            Next
            ddlProvince.SelectedIndex = 0
        End If
        dt = Nothing
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
                .DataTextField = "CYear"
                .DataValueField = "CYear"
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
        dt = Nothing
    End Sub
    Private Sub LoadTimePhase()
        Dim ctlTP As New TimePhaseController

        dt = ctlTP.TimePhase_GetByCourse(ddlCourse.SelectedValue)

        If dt.Rows.Count > 0 Then
            With optTimePhase
                .Visible = True
                .DataSource = dt
                .TextField = "Descriptions"
                .ValueField = "TimePhaseID"
                .DataBind()
                .SelectedIndex = 0
            End With
        Else
            optTimePhase.DataSource = Nothing
        End If
    End Sub

    Private Sub LoadLocationNotSupervisorToGrid()

        'dt = ctlSpv.Supervisor_GetLocation(StrNull2Zero(ddlYear.SelectedValue), ddlCourse.SelectedValue, ddlProvince.SelectedValue, Trim(txtSearch.Text))
        'If dt.Rows.Count > 0 Then

        '    With grdData
        '        .Visible = True
        '        .DataSource = dt
        '        .DataBind()
        '    End With
        'Else
        '    grdData.Visible = False
        'End If
        'dt = Nothing
    End Sub

    Private Sub LoadLocationInSupervisorToGrid()

        'dt = ctlSpv.Supervisor_GetLocationInSupervisor(StrNull2Zero(ddlYear.SelectedValue), ddlCourse.SelectedValue, Session("ProfileID"), Trim(txtSearchStd.Text))

        'If dt.Rows.Count > 0 Then
        '    lblCount.Text = dt.Rows.Count
        '    lblNo.Visible = False
        '    With grdStudent
        '        .Visible = True
        '        .DataSource = dt
        '        .DataBind()
        '    End With
        'Else
        '    lblCount.Text = 0
        '    lblNo.Visible = True
        '    grdStudent.Visible = False
        'End If
    End Sub

    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()

        dt = ctlCs.Courses_GetByYear(ddlYear.SelectedValue)

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

    Private Sub LoadLocationToDDL()
        cboLocation.Items.Clear()
        Dim dtL As New DataTable
        dtL = ctlCs.CourseLocation_GetLocationInCourse(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue))

        If dtL.Rows.Count > 0 Then
            cboLocation.Items.Clear()

            With cboLocation
                .DataSource = dtL
                .TextField = "LocationName"
                .ValueField = "LocationID"
                .DataBind()
            End With


        End If

    End Sub


    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadLocationNotSupervisorToGrid()
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub
    Private Sub AddSupervisor()

        Dim item As Integer

        For i = 0 To grdData.Rows.Count - 1
            With grdData
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                If chkS.Checked Then
                    If Not ctlSpv.Supervisor_CheckDup(ddlYear.SelectedValue, Session("ProfileID"), .DataKeys(i).Value, StrNull2Zero(.Rows(i).Cells(1).Text)) Then

                        item = ctlSpv.Supervisor_Add(ddlYear.SelectedValue, Session("ProfileID"), .DataKeys(i).Value, ddlCourse.SelectedValue, StrNull2Zero(.Rows(i).Cells(1).Text), Request.Cookies("Username").Value)
                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Supervisor", "เลือกสถานที่นิเทศ ปี " & ddlYear.SelectedValue & " วิชา " & ddlCourse.SelectedItem.Text & "ผลัด " & .Rows(i).Cells(1).Text & " " & .DataKeys(i).Value, "")

                    End If

                End If
            End With
        Next
        LoadLocationInSupervisorToGrid()
        LoadLocationNotSupervisorToGrid()
        DisplayMessage(Me, "บันทึกข้อมูลเรียบร้อย")
    End Sub

    Private Sub grdStudent_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdStudent.PageIndexChanging
        grdStudent.PageIndex = e.NewPageIndex
        LoadLocationInSupervisorToGrid()
    End Sub

    Private Sub grdCourse_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdStudent.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    If ctlSpv.Supervisor_Delete(ddlYear.SelectedValue, grdStudent.DataKeys(e.CommandArgument).Value) Then
                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "Supervisor", "ลบ แหล่งฝึกในรายวิชาฝึก:" & ddlCourse.SelectedItem.Text & ">>" & grdStudent.Rows(e.CommandArgument).Cells(0).Text, "")
                        DisplayMessage(Me, "ลบข้อมูลเรียบร้อย")
                        LoadLocationInSupervisorToGrid()
                        LoadLocationNotSupervisorToGrid()
                    Else
                        DisplayMessage(Me, "ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง")
                    End If


            End Select


        End If
    End Sub

    Private Sub grdCourse_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdStudent.RowDataBound
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

    Protected Sub cmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdSave.Click
        AddSupervisor()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdClear.Click
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
        LoadLocationInSupervisorToGrid()
        LoadLocationNotSupervisorToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged

        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadLocationInSupervisorToGrid()
        LoadLocationNotSupervisorToGrid()
    End Sub

    Protected Sub cmdFindStd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdFindStd.Click
        grdStudent.PageIndex = 0
        LoadLocationInSupervisorToGrid()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadLocationNotSupervisorToGrid()
    End Sub




End Class

