Public Class EvaluationTopic
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            ClearData()
            lblID.Text = ""
            grdData.PageIndex = 0
            LoadEvaluationTopicToGrid()
        End If

    End Sub

    Private Sub LoadEvaluationTopicToGrid()
        dt = ctlLG.EvaluationTopic_Get(txtSearch.Text)
        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With
        dt = Nothing
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If ctlLG.EvaluationTopic_Delete(e.CommandArgument) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadEvaluationTopicToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub

    Private Sub EditData(ByVal pID As Integer)
        dt = ctlLG.EvaluationTopic_GetByUID(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.lblID.Text = DBNull2Str(.Item("UID"))
                txtName.Text = DBNull2Str(.Item("TopicName"))
                'ddlGroup.SelectedValue = DBNull2Str(.Item("EvaluationGroupUID"))
                ddlWeight.SelectedValue = DBNull2Str(.Item("Weight"))
                chkStatus.Checked = ConvertStatusFlag2Boolean(DBNull2Str(.Item("StatusFlag")))
                txtRemark.Text = DBNull2Str(.Item("Remark"))

            End With
        End If
        dt = Nothing
    End Sub
    Private Sub ClearData()
        Me.lblID.Text = ""
        txtName.Text = ""
        chkStatus.Checked = True
        ddlWeight.SelectedValue = "1"
        txtRemark.Text = ""
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


    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If

        Dim item As Integer
        item = ctlLG.EvaluationTopic_Save(StrNull2Zero(lblID.Text), txtName.Text, StrNull2Double(ddlWeight.SelectedValue), ConvertBoolean2StatusFlag(chkStatus.Checked), txtRemark.Text)

        grdData.PageIndex = 0
        LoadEvaluationTopicToGrid()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)


    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadEvaluationTopicToGrid()
    End Sub
    Protected Sub cmdAll_Click(sender As Object, e As EventArgs) Handles cmdAll.Click
        txtSearch.Text = ""
        grdData.PageIndex = 0
        LoadEvaluationTopicToGrid()
    End Sub

    Protected Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging

        grdData.PageIndex = e.NewPageIndex
        LoadEvaluationTopicToGrid()
    End Sub
End Class

