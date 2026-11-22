Public Class VdoEdit
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlG As New GalleryController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("RoleID") <> "0" Then
            Response.Redirect("../Login.aspx?logout=y")
        End If
        Page.Title = "จัดการข้อมูล รายการ VDO"
        If Not Page.IsPostBack Then
            GetVDOList()
        End If
    End Sub

    Private Sub GetVDOList()
        dt = ctlG.Video_Get()
        grdVDO.DataSource = dt
        grdVDO.DataBind()

        dt = Nothing
    End Sub

    Protected Sub bttSave_Click(sender As Object, e As System.EventArgs) Handles bttSave.Click
        ctlG.Video_Save(txtCode.Text, txtTitle.Text, ConvertCheckedStatus2Number(chkFirstPage.Checked), "Images/icon/ok.png")
        txtCode.Text = ""
        txtTitle.Text = ""
        chkFirstPage.Checked = True
        GetVDOList()
        ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('บันทึกเรียบร้อยแล้ว!')", True)

    End Sub

    Protected Sub grdVDO_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdVDO.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    ctlG.Video_Delete(e.CommandArgument())
                    GetVDOList()
            End Select
        End If
    End Sub
    Private Sub grdVDO_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdVDO.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(2).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

            e.Row.Cells(0).Text = e.Row.RowIndex + 1

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#c1ffc2';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub
End Class