Public Class SupervisionLocation
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim acc As New UserController
    Dim ctlSpv As New SupervisionController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then

            LoadYearToDDL()
            LoadGroupToDDL()

            LoadSupervisionLocationToGrid()
            LoadLocationNotSupervisionToGrid()

        End If
    End Sub
    Private Sub LoadYearToDDL()
        Dim ctlCs As New CourseController
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlSpv.GET_DATE_SERVER))
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

            With ddlGroupFind
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
    Private Sub LoadSupervisionLocationToGrid()

        dt = ctlSpv.SupervisionLocation_Get(StrNull2Zero(ddlYear.SelectedValue), Trim(txtSearchLocation.Text))

        If dt.Rows.Count > 0 Then
            lblCount.Text = dt.Rows.Count
            lblNo.Visible = False
            pnAddByType.Visible = True
            With grdVisitLocation
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        Else
            pnAddByType.Visible = False
            lblCount.Text = 0
            lblNo.Visible = True
            grdVisitLocation.Visible = False
        End If
    End Sub
    Private Sub LoadLocationNotSupervisionToGrid()

        dt = ctlSpv.Location_GetNotSupervision(StrNull2Zero(ddlYear.SelectedValue), ddlGroupFind.SelectedValue, Trim(txtSearch.Text))
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

    Private Sub grdLocation_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLocation.PageIndexChanging
        grdLocation.PageIndex = e.NewPageIndex
        LoadLocationNotSupervisionToGrid()
    End Sub
    Private Sub grdLocation_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLocation.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub
    Private Sub AddLocationToVisitLocation()

        Dim item As Integer
        Dim sForce As String = "N"

        For i = 0 To grdLocation.Rows.Count - 1
            With grdLocation
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                Dim chkF As CheckBox = .Rows(i).Cells(0).FindControl("chkForce")
                If chkS.Checked Then

                    If chkF.Checked Then
                        sForce = "Y"
                    End If

                    item = ctlSpv.VisitLocation_Add(ddlYear.SelectedValue, .DataKeys(i).Value, sForce, Request.Cookies("Username").Value)

                End If


            End With
        Next
        LoadSupervisionLocationToGrid()
        LoadLocationNotSupervisionToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Private Sub grdCourseLocation_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdVisitLocation.PageIndexChanging
        grdVisitLocation.PageIndex = e.NewPageIndex
        LoadSupervisionLocationToGrid()
    End Sub

    Private Sub grdCourse_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdVisitLocation.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    If ctlSpv.VisitLocation_Delete(e.CommandArgument) Then
                        'acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "CourseLocation", "ลบ สถานที่นิเทศ:" & ddlCourse.SelectedItem.Text & ">>itemID:" & e.CommandArgument, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadSupervisionLocationToGrid()
                        LoadLocationNotSupervisionToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub
    Private Sub grdCourse_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdVisitLocation.RowDataBound
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
        AddLocationToVisitLocation()
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

        grdVisitLocation.PageIndex = 0
        grdLocation.PageIndex = 0

        LoadSupervisionLocationToGrid()
        LoadLocationNotSupervisionToGrid()
    End Sub

    Protected Sub cmdFindStd_Click(sender As Object, e As EventArgs) Handles cmdFindLocationInCourse.Click
        grdVisitLocation.PageIndex = 0
        LoadSupervisionLocationToGrid()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdLocation.PageIndex = 0
        LoadLocationNotSupervisionToGrid()
    End Sub

    Protected Sub lnkSubmitDel_Click(sender As Object, e As EventArgs) Handles lnkSubmitDel.Click
        ctlSpv.VisitLocation_DeleteByGroup(ddlYear.SelectedValue, ddlLocationTypeDel.SelectedValue)

        grdVisitLocation.PageIndex = 0
        grdLocation.PageIndex = 0
        LoadSupervisionLocationToGrid()
        LoadLocationNotSupervisionToGrid()

        DisplayMessage(Me.Page, "ลบเรียบร้อย")
    End Sub

    Protected Sub lnkSubmitAdd_Click(sender As Object, e As EventArgs) Handles lnkSubmitAdd.Click
        Dim ctlL As New LocationController

        dt = ctlL.Location_GetByGroupID(ddlLocationTypeAdd.SelectedValue)

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                ctlSpv.VisitLocation_Add(ddlYear.SelectedValue, dt.Rows(i)("LocationID"), "N", Request.Cookies("Username").Value)
            Next
        End If

        'acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "CourseLocation", "เพิ่ม แหล่งนิเทศทั้งประเภท:" & ddlLocationTypeAdd.SelectedValue & ">>" & ddlCourse.SelectedItem.Text, "")

        grdVisitLocation.PageIndex = 0
        grdLocation.PageIndex = 0
        LoadSupervisionLocationToGrid()
        LoadLocationNotSupervisionToGrid()

        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")

    End Sub
End Class

