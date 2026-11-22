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

        Dim sPass As String = enc.DecryptString(acc.User_GetPassword(Request.Cookies("iLaw")("username")), True)

        If sPass <> txtOld.Text Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','รหัสผ่านเดิมท่านไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง');", True)
        End If

        acc.User_ChangePassword(Request.Cookies("username").Value, enc.EncryptString(txtNew.Text, True))

        acc.User_GenLogfile(Request.Cookies("username").Value, ACTTYPE_UPD, "Users", "Change password", "")
        'ModalPopupExtender.Show()

        ' DisplayMessage(Me.Page, "ระบบเปลี่ยนรหัสผ่านให้ท่านใหม่เรียบร้อยแล้ว")

        pnForm.Visible = False
        pnResult.Visible = True
    End Sub
End Class