Public Class RequirementSearch
    Inherits System.Web.UI.Page

    Dim ctlReq As New REQcontroller

    Dim dt As New DataTable
    Dim acc As New UserController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            grdData.PageIndex = 0
            LoadYearToDDL()
            LoadRequirementToGrid()
        End If

    End Sub
    Private Sub LoadYearToDDL()

        dt = ctlReq.REQ_GetYear(Request.Cookies("LANG").Value)
        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "YearText"
                .DataValueField = "YearValue"
                .DataBind()
                .SelectedIndex = 1
            End With
        Else
            ddlYear.Items.Add("2557")
            ddlYear.Items(0).Value = "2557"
            ddlYear.SelectedIndex = 0
        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        dt = Nothing
    End Sub
    Private Sub LoadRequirementToGrid()

        dt = ctlReq.REQ_GetBySearch(ddlYear.SelectedValue, txtSearch.Text)


        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()

            Try


                Dim nrow As Integer = dt.Rows.Count
                If .PageCount > 1 Then
                    If .PageIndex > 0 Then
                        If (.PageSize) < nrow - (.PageSize * .PageIndex) Then
                            For i = 0 To .PageSize - 1
                                .Rows(i).Cells(0).Text = (.PageSize * .PageIndex) + (i + 1)
                            Next
                        Else
                            For i = 0 To nrow - (.PageSize * .PageIndex) - 1
                                .Rows(i).Cells(0).Text = (.PageSize * .PageIndex) + (i + 1)
                            Next
                        End If
                    Else
                        For i = 0 To .PageSize - 1
                            .Rows(i).Cells(0).Text = i + 1
                        Next
                    End If
                Else
                    For i = 0 To nrow - 1
                        .Rows(i).Cells(0).Text = i + 1
                    Next
                End If
            Catch ex As Exception
                'DisplayMessage(Me.Page, ex.Message)
            End Try
        End With

    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadRequirementToGrid()
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
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
    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    Response.Redirect("RequirementsReg.aspx?ActionType=setup&ItemType=y5&y=" & ddlYear.SelectedValue & "&id=" & grdData.DataKeys(e.CommandArgument).Value)
                Case "imgDel"
                    If ctlReq.Requirements_Delete(ddlYear.SelectedValue, grdData.DataKeys(e.CommandArgument).Value, grdData.Rows(e.CommandArgument).Cells(1).Text) Then

                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "Requirement", "ลบความต้องการ:" & ddlYear.SelectedValue & ">>" & e.CommandArgument, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadRequirementToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub


    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadRequirementToGrid()

    End Sub
End Class