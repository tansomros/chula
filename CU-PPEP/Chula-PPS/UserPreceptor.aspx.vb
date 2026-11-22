
Public Class UserPreceptor
    Inherits System.Web.UI.Page

    Dim ctlSubj As New SubjectController
    Dim dt As New DataTable
    Dim ctlCs As New Coursecontroller
    Dim acc As New UserController
    Dim ctlP As New PreceptorController
    Dim ctlPsn As New PersonController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            grdData.PageIndex = 0
            LoadYearToDDL()
            LoadLocationToGrid()
            LoadTeacher()
            If Not Request("y") Is Nothing Then
                ddlYear.SelectedValue = Request("y")
            End If

            LoadLocationToGrid()
            LoadCoordinatorToGrid()

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
            End With

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

            ddlYear.SelectedIndex = 0
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
    Private Sub LoadLocationToGrid()
        Dim ctlL As New LocationController
        dt = ctlL.Location_GetBySearch(StrNull2Zero(ddlYear.SelectedValue), txtFindLocation.Text)
        grdLocation.DataSource = dt
        grdLocation.DataBind()
    End Sub

    Private Sub LoadCoordinatorToGrid()

        dt = ctlP.PreceptorLocation_Get(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(lblLocationID.Text))
        If dt.Rows.Count > 0 Then
            lblNo.Visible = False
            With grdCoor
                .Visible = True
                .DataSource = dt
                .DataBind()

                For i = 0 To dt.Rows.Count - 1
                    .Rows(i).Cells(0).Text = i + 1
                    '.Rows(i).Cells(1).Text = dt.Rows(i)("PrefixName") & dt.Rows(i)("FirstName") & " " & dt.Rows(i)("LastName")

                Next

            End With
        Else
            lblNo.Visible = True
            grdCoor.Visible = False
        End If
    End Sub
    Private Sub LoadTeacher()

        If Trim(txtSearch.Text) <> "" Then
            dt = ctlPsn.Person_GetSearch("T", txtSearch.Text)
        Else
            dt = ctlPsn.Person_GetByType("T")
        End If


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

    Private Sub grdCourse_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdLocation.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    'ddlYear.SelectedValue = grdLocation.Rows(e.CommandArgument()).Cells(1).Text
                    lblLocationName.Text = grdLocation.Rows(e.CommandArgument()).Cells(0).Text
                    lblLocationID.Text = grdLocation.DataKeys(e.CommandArgument()).Value
                    LoadCoordinatorToGrid()
            End Select

        End If
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLocation.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub
    Private Sub AddTeacherToCourse(pID As Integer)

        Dim item As Integer

        item = ctlP.PreceptorLocation_Add(ddlYear.SelectedValue, pID, StrNull2Zero(lblLocationID.Text))
        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "CourseCoordinator", "เพิ่ม อ.แหล่งฝึก:" & ddlYear.SelectedValue & ">>" & lblLocationName.Text & ">>PersonID:" & pID, "")

        LoadCoordinatorToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'ผลการตรวจสอบ','เพิ่มอาจารย์เรียบร้อย');", True)
    End Sub

    Private Sub grdCoor_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdCoor.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    If ctlP.PreceptorLocation_Delete(e.CommandArgument) Then

                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "PreceptorLocation", "ลบ อ.แหล่งฝึก:" & ddlYear.SelectedValue & ">>" & lblLocationName.Text, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadCoordinatorToGrid()

                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If

            End Select


        End If
    End Sub

    Private Sub grdCoor_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCoor.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(2).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadLocationToGrid()
        LoadCoordinatorToGrid()
    End Sub



    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadTeacher()
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgSelect"
                    AddTeacherToCourse(e.CommandArgument())
            End Select


        End If
    End Sub

    Private Sub grdData_RowDataBound1(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadTeacher()
    End Sub

    Protected Sub lnkFind_Click(sender As Object, e As EventArgs) Handles lnkFind.Click
        LoadLocationToGrid()
    End Sub

End Class

