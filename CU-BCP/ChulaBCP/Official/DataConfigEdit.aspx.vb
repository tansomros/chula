Public Class DataConfigEdit
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlCfg As New SystemConfigController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Request.Cookies("Username").Value Is Nothing Then
        '    Response.Redirect("Default.aspx?logout=YES")
        'End If
        If Not IsPostBack Then
            If Not Request("uid") Is Nothing Then
                EditData(Request("uid"))
            End If

        End If
    End Sub
    Private Sub EditData(pUID As Integer)
        dt = ctlCfg.SystemConfig_GetByUID(pUID)
        If dt.Rows.Count > 0 Then
            lblUID.Text = dt.Rows(0)("UID")
            lblCode.Text = dt.Rows(0)("CodeConfig")
            txtValues.Text = dt.Rows(0)("ValueConfig")
            txtDesc.Text = dt.Rows(0)("Description")
            lblName.Text = dt.Rows(0)("Name")
        End If
        dt = Nothing
    End Sub

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub
    Private Sub ClearData()
        txtValues.Text = ""
        txtDesc.Text = ""
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSave.Click

        If txtValues.Text = "" Or txtDesc.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If

        If lblCode.Text = CFG_EDUYEAR Then
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
        item = ctlCfg.DataConfig_Update(lblCode.Text, txtValues.Text, txtDesc.Text)
        'End If

        If item Then
            ClearData()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
        End If



    End Sub
End Class