Public Class DataConfig
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlCfg As New SystemConfigController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            isAdd = True
            txtCode.ReadOnly = True
            LoadCategoryToGrid()
        End If

    End Sub
    Private Sub LoadCategoryToGrid()
        dt = ctlCfg.SystemConfig_Get
        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()

                For i = 0 To .Rows.Count - 1
                    .Rows(i).Cells(0).Text = i + 1
                Next

            End With
        Else
            grdData.DataSource = Nothing
            grdData.Visible = False
        End If
        dt = Nothing
    End Sub

    Private Sub grdData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand

        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument)
            End Select

        End If


    End Sub
    Private Sub EditData(pIndex As Integer)

        isAdd = False
        txtCode.ReadOnly = True
        Me.txtCode.Text = grdData.Rows(pIndex).Cells(4).Text
        txtValues.Text = grdData.Rows(pIndex).Cells(3).Text
        txtDesc.Text = grdData.Rows(pIndex).Cells(2).Text
        txtName.Text = grdData.Rows(pIndex).Cells(1).Text

    End Sub

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub
    Private Sub ClearData()
        txtCode.ReadOnly = True
        Me.txtCode.Text = ""
        txtValues.Text = ""
        txtDesc.Text = ""
        txtName.Text = ""
        isAdd = True
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSave.Click

        If txtValues.Text = "" Or txtDesc.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If

        If txtCode.Text = CFG_EDUYEAR Then
            If Not IsNumeric(txtValues.Text) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ถูกต้อง');", True)
                Exit Sub
            End If
            Request.Cookies("EDUYEAR").Value = StrNull2Zero(txtValues.Text)
        End If

        Dim item As Integer

        'If txtCode.Text = "" Then
        '    item = ctlCfg.DataConfig_Add(txtCode.Text, txtName.Text, txtFee.Text)
        'Else
        item = ctlCfg.DataConfig_Update(txtCode.Text, txtValues.Text, txtDesc.Text)
        'End If

        If item Then
            LoadCategoryToGrid()
            ClearData()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
        End If



    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub
End Class