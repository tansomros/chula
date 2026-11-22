
Public Class LocationList
    Inherits System.Web.UI.Page

    Dim ctlLG As New LocationController
    Dim dt As New DataTable
    Dim ds As New DataSet
    Dim ctlPsn As New PersonController
    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            grdData.PageIndex = 0
            LoadLocationGroupToDDL()
            If Request.Cookies("ROLE_ADM").Value = True Then

            Else
                grdData.Columns(5).Visible = False
                grdData.Columns(7).Visible = False
                grdData.Columns(8).Visible = False
            End If
            LoadLocationToGrid()
        End If

    End Sub
    Private Sub LoadLocationToGrid()
        dt = ctlLG.Location_GetBySearch(txtSearch.Text, StrNull2Zero(ddlType.SelectedValue))
        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With

    End Sub

    Private Sub LoadLocationGroupToDDL()

        Dim ctlLG As New LocationGroupController
        dt = ctlLG.LocationGroup_Get
        If dt.Rows.Count > 0 Then
            With ddlType
                .Items.Add("---ทั้งหมด---")
                .Items(0).Value = 0
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add(dt.Rows(i)("Name"))
                    .Items(i + 1).Value = dt.Rows(i)("Code")
                Next
                .SelectedIndex = 0
                .Visible = True
            End With

        End If
        dt = Nothing
    End Sub


    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadLocationToGrid()
    End Sub


    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    Response.Redirect("Location.aspx?ActionType=loc&ItemType=l&lid=" & e.CommandArgument())
                Case "imgView"
                    Response.Redirect("LocationDetail.aspx?ActionType=loc&ItemType=l&lid=" & e.CommandArgument())
                Case "imgDel"
                    If ctlLG.Location_Delete(e.CommandArgument) Then

                        acc.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_DEL, "Location", "ลบแหล่งฝึก:" & e.CommandArgument, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ลบข้อมูลเรียบร้อย');", True)
                        LoadLocationToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If

            End Select


        End If
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(8).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadLocationToGrid()

    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        grdData.PageIndex = 0
        LoadLocationToGrid()
    End Sub

End Class

