
Public Class StudentLocation_Manage
    Inherits System.Web.UI.Page

    Dim ctlReg As New RegisterController

    Dim ctlCs As New Coursecontroller
    Dim acc As New UserController
    Dim ctlCfg As New SystemConfigController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            lblNone.Visible = False
            If Request.Cookies("ROLE_STD").Value = True Then
                If Not SystemOnlineTime() Then
                    Response.Redirect("ResultPage.aspx?t=closed&p=select")
                End If
            End If
            LoadYearToDDL()
            LoadCourseToDDL()

            If Not Request("y") Is Nothing Then
                ddlYear.SelectedValue = Request("y")
                LoadCourseToDDL()
                ddlCourse.SelectedValue = Request("id")
            End If

            LoadStudentLocationToGrid()

        End If

    End Sub
    Private Function SystemOnlineTime() As Boolean
        Dim Bdate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_STARTDATE)))
        Dim Edate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_ENDDATE)))
        Dim sToday As Integer = StrNull2Zero(ConvertDate2DBString(ctlCfg.GET_DATE_SERVER))
        Dim bAvailable As Boolean
        If sToday < Bdate Then
            bAvailable = False
        ElseIf sToday > Edate Then
            bAvailable = False
        Else
            bAvailable = True
        End If
        Return bAvailable
    End Function
    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()

        Dim dtC As New DataTable
        dtC = ctlCs.Courses_GetByStudent(ddlYear.SelectedValue, Request.Cookies("ProfileID").Value)
        If dtC.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            For i = 0 To dtC.Rows.Count - 1
                With ddlCourse
                    .Items.Add("" & dtC.Rows(i)("SubjectCode") & " : " & dtC.Rows(i)("NameTH"))
                    .Items(i).Value = dtC.Rows(i)("CourseID")
                End With
            Next
            ddlCourse.SelectedIndex = 0
        Else
            ddlCourse.Items.Clear()
        End If
        dtC = Nothing
    End Sub

    Private Sub LoadYearToDDL()
        Dim dtY As New DataTable
        'Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
        'Dim LastRow As Integer
        dtY = ctlCs.Year_GetByStudent(Request.Cookies("ProfileID").Value)
        If dtY.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dtY
                .DataTextField = "CYear"
                .DataValueField = "CYear"
                .DataBind()
            End With
            ddlYear.SelectedValue = ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR)
            pnYear.Visible = True
        Else
            lblNone.Visible = True
            pnYear.Visible = False
        End If
        dtY = Nothing
    End Sub

    Private Sub LoadStudentLocationToGrid()

        If ddlCourse.Items.Count > 0 Then
            Dim str() As String
            Dim dtSL As New DataTable
            Dim iQuota As Integer

            str = Split(ddlCourse.SelectedItem.Text, " : ")
            dtSL = ctlReg.GetStudentRegister_ByStudent(ddlYear.SelectedValue, Request.Cookies("ProfileID").Value, str(0))
            If dtSL.Rows.Count > 0 Then
                lblNo.Visible = False
                pnData.Visible = True

                With grdLocation
                    .Visible = True
                    .DataSource = dtSL
                    .DataBind()

                    For i = 0 To .Rows.Count - 1

                        '.Rows(i).Cells(0).Text = i + 1
                        Dim imgU As ImageButton = .Rows(i).Cells(5).FindControl("imgUp")
                        Dim imgD As ImageButton = .Rows(i).Cells(5).FindControl("imgDown")

                        If i = 0 Then
                            imgU.Visible = False
                        ElseIf i = .Rows.Count - 1 Then
                            imgD.Visible = False
                        End If
                        If .Rows.Count = 1 Then
                            imgU.Visible = False
                            imgD.Visible = False
                        End If

                    Next

                End With

                iQuota = StrNull2Zero(ctlCfg.SystemConfig_GetByCode(CFG_MAXLOCATION))

                If iQuota > dtSL.Rows.Count Then
                    lblFull.Text = "นิสิตสามารถเลือกแหล่งฝึกได้อีก " & iQuota - dtSL.Rows.Count & " แหล่ง (ไม่บังคับ)"
                    lnkSelect.Visible = True
                Else
                    lblFull.Text = "นิสิตเลือกแหล่งฝึกครบตามจำนวนที่กำหนดแล้ว"
                    lnkSelect.Visible = False
                End If

                lblFull.Visible = True
            Else
                'pnData.Visible = False
                'lblNo.Visible = True
                grdLocation.Visible = False
                grdLocation.DataSource = Nothing
                lblFull.Text = ""
                lblFull.Visible = False
            End If
            dtSL = Nothing
        Else
            lblNo.Visible = True
            grdLocation.Visible = False
            grdLocation.DataSource = Nothing
            lblFull.Text = ""
            lblFull.Visible = False
        End If

    End Sub
    Private Sub grdLocation_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdLocation.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Dim str() As String
            str = Split(ddlCourse.SelectedItem.Text, " : ")

            Select Case ButtonPressed.ID
                Case "imgUp"
                    UpdateRegisterPriority(CInt(DBNull2Zero(e.CommandArgument)), "U")
                Case "imgDown"
                    UpdateRegisterPriority(CInt(DBNull2Zero(e.CommandArgument)), "D")
                Case "imgDel"

                    If ctlReg.StudentRegister_CheckAssessment(StrNull2Zero(grdLocation.DataKeys(e.CommandArgument).Value)) Then
                        DisplayMessage(Me.Page, "ไม่สามารถลบได้ เนื่องจากท่านได้รับการคัดเลือกแหล่งฝึกในรายวิชานี้เรียบร้อยแล้ว")
                        Exit Sub
                    Else
                        ctlReg.StudentRegister_Delete(StrNull2Zero(grdLocation.DataKeys(e.CommandArgument).Value))
                        LoadStudentLocationToGrid()
                        UpdatePriority()
                    End If


                    'Response.Redirect("StudentLocation.aspx?m=7&p=703&y=" & ddlYear.SelectedValue & "&id=" & e.CommandArgument())

            End Select
            LoadStudentLocationToGrid()

        End If
    End Sub
    Private Sub UpdateRegisterPriority(row As Integer, flag As String)
        Dim str() As String
        str = Split(ddlCourse.SelectedItem.Text, " : ")

        If flag = "U" Then
            ctlReg.StudentRegister_UpdatePriority(StrNull2Zero(grdLocation.DataKeys(row).Value), "U", Request.Cookies("Username").Value)
            ctlReg.StudentRegister_UpdatePriority(StrNull2Zero(grdLocation.DataKeys(row - 1).Value), "D", Request.Cookies("Username").Value)

        Else
            ctlReg.StudentRegister_UpdatePriority(StrNull2Zero(grdLocation.DataKeys(row).Value), "D", Request.Cookies("Username").Value)
            ctlReg.StudentRegister_UpdatePriority(StrNull2Zero(grdLocation.DataKeys(row + 1).Value), "U", Request.Cookies("Username").Value)
        End If

    End Sub

    Private Sub UpdatePriority()
        Dim str() As String
        str = Split(ddlCourse.SelectedItem.Text, " : ")
        For i = 0 To grdLocation.Rows.Count - 1
            ctlReg.StudentRegister_UpdatePriorityItem(StrNull2Zero(grdLocation.DataKeys(i).Value), i + 1, Request.Cookies("Username").Value)
        Next
    End Sub
    Private Sub grdCourse_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLocation.RowDataBound

        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(6).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub
    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadCourseToDDL()
        LoadStudentLocationToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        LoadStudentLocationToGrid()
    End Sub

    Protected Sub lnkSelect_Click(sender As Object, e As EventArgs) Handles lnkSelect.Click
        Response.Redirect("StudentLocation.aspx?m=2&p=202&y=" & ddlYear.SelectedValue & "&id=" & ddlCourse.SelectedValue)
    End Sub
End Class

