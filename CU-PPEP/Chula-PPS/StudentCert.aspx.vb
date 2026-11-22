Public Class StudentCert
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlCfg As New SystemConfigController
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            txtEduYear.Text = ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR)
            txtAnnouncedDate.Text = ctlCfg.SystemConfig_GetByCode(CFG_CERTDATE)
            txtCertDate.Text = ctlCfg.SystemConfig_GetByCode(CFG_CERTDATE)
            LoadCertSettingToGrid()
        End If
    End Sub

    Private Sub grdData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand

        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument)
                Case "imgDel"
                    ctlCfg.Certification_Delete(e.CommandArgument)
                    LoadCertSettingToGrid()
            End Select
        End If
    End Sub
    Private Sub EditData(pIndex As Integer)
        txtEduYear.Text = grdData.Rows(pIndex).Cells(0).Text
        txtCertDate.Text = grdData.Rows(pIndex).Cells(1).Text
    End Sub
    Private Sub LoadCertSettingToGrid()
        Dim dt As New DataTable
        dt = ctlCfg.Certification_Get()
        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        Else
            grdData.DataSource = Nothing
            grdData.Visible = False
        End If
        dt = Nothing
    End Sub

    Protected Sub cmdSaveCert_Click(sender As Object, e As EventArgs) Handles cmdSaveCert.Click
        If txtAnnouncedDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านไม่ได้ระบุวันที่ให้ครบถ้วน');", True)
            Exit Sub
        End If
        If Not isStringFormatDate(txtAnnouncedDate.Text, "/") Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านระบุวันที่ไม่ถูกต้อง (รูปแบบวันที่ วว/ดด/ปปปป) กรุณาตรวจสอบ');", True)
            Exit Sub
        End If

        ctlCfg.Certification_Save(txtEduYear.Text, ParseDateToSQL(txtCertDate.Text))
        LoadCertSettingToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtAnnouncedDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านไม่ได้ระบุวันที่ให้ครบถ้วน');", True)
            Exit Sub
        End If
        If Not isStringFormatDate(txtAnnouncedDate.Text, "/") Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านระบุวันที่ไม่ถูกต้อง (รูปแบบวันที่ วว/ดด/ปปปป) กรุณาตรวจสอบ');", True)
            Exit Sub
        End If

        ctlCfg.DataConfig_UpdateValue(CFG_CERTDATE, txtAnnouncedDate.Text)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบข้อมูลนี้?"");"
            Dim imgD As ImageButton = e.Row.Cells(3).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)
        End If
    End Sub
End Class