
Public Class Courses_Reg
    Inherits System.Web.UI.Page

    Dim ctlSubj As New SubjectController
    Dim dt As New DataTable
    Dim ctlCs As New courseController
    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        'If Request.Cookies("ROLE_STD").Value = True Then
        '    If Not SystemOnlineTime() Then
        '        Response.Redirect("ResultPage.aspx?t=closed&p=select")
        '    End If
        'End If

        If Not IsPostBack Then
            LoadYearToDDL()
            LoadCourseToGrid()
        End If


    End Sub
    Private Function SystemOnlineTime() As Boolean
        Dim ctlCfg As New SystemConfigController
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
    Private Sub LoadYearToDDL()
        dt = ctlCs.Year_GetByStudent(Request.Cookies("ProfileID").Value)
        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "CYear"
                .DataValueField = "CYear"
                .DataBind()
            End With
            ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value

        Else

        End If


        dt = Nothing
    End Sub

    Private Sub LoadCourseToGrid()
        dt = ctlCs.Course4Student_GetByStdICode(StrNull2Zero(ddlYear.SelectedValue), Request.Cookies("ProfileID").Value)
        If dt.Rows.Count > 0 Then
            lblNo.Visible = False

            With grdCourse
                .Visible = True
                .DataSource = dt
                .DataBind()

                lblYear.Text = ddlYear.SelectedValue

                For i = 0 To .Rows.Count - 1

                    .Rows(i).Cells(0).Text = i + 1
                    'Dim lblTH As Label = .Rows(i).Cells(2).FindControl("lblTH0")
                    'Dim lblEN As Label = .Rows(i).Cells(2).FindControl("lblEN0")

                    'lblTH.Text = dt.Rows(i)("NameTH")
                    'lblEN.Text = dt.Rows(i)("NameEN")

                    If dt.Rows(i)("Status") = 1 Then
                        .Rows(i).Cells(4).Text = "ผ่าน"
                        Dim lnkS As LinkButton = .Rows(i).Cells(5).FindControl("lnkSelect")
                        lnkS.Visible = False
                    Else
                        .Rows(i).Cells(4).Text = "ยังไม่ฝึก"
                    End If

                Next

                If Request.Cookies("ROLE_STD").Value = True Then
                    If Not SystemOnlineTime() Then
                        .Columns(5).Visible = False
                    Else
                        If ddlYear.SelectedValue <> Request.Cookies("EDUYEAR").Value Then
                            .Columns(5).Visible = False
                        Else
                            .Columns(5).Visible = True
                        End If
                    End If
                End If
                

            End With
        Else
            lblNo.Visible = True
            grdCourse.DataSource = Nothing
            grdCourse.Visible = False
        End If
        dt = Nothing
    End Sub
    Private Sub grdCourse_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdCourse.RowCommand
        If TypeOf e.CommandSource Is WebControls.LinkButton Then
            Dim ButtonPressed As WebControls.LinkButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "lnkSelect"
                    Response.Redirect("StudentLocation_Manage.aspx?m=2&p=202&y=" & ddlYear.SelectedValue & "&id=" & e.CommandArgument())
            End Select


        End If
    End Sub

    Private Sub grdCourse_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCourse.RowDataBound

        'If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

        '    Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
        '    Dim imgD As Image = e.Row.Cells(5).FindControl("imgDel")
        '    imgD.Attributes.Add("onClick", scriptString)

        'End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub
    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadCourseToGrid()
    End Sub
End Class

