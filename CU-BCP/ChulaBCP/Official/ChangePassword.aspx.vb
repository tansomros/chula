Public Class ChangePassword
    Inherits System.Web.UI.Page
    Dim acc As New UserController
    Dim enc As New CryptographyEngine
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnResult.Visible = False
            pnForm.Visible = True
        End If
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If Request.Cookies("Password").Value <> txtOld.Text Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','รหัสผ่านเดิมท่านไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง');", True)
            Exit Sub
        End If
        acc.User_ChangePassword(Request.Cookies("UserLoginID").Value, enc.EncryptString(txtNew.Text, True))

        'acc.User_GenLogfile(Session("Username"), ACTTYPE_CHGP, "Change Password", txtOld.Text, txtNew.Text, "")

        pnForm.Visible = False
        pnResult.Visible = True
    End Sub
End Class