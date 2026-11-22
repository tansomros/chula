Imports System.Net.Mail
Public Class ForgotPassword
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim acc As New UserController
    Dim enc As New CryptographyEngine

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim jsString As String = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document.forms[0].all['" + cmdSubmit.ClientID + "'].click();return false;} else return true; "

        txtUsername.Attributes.Add("onkeydown", jsString)

        If Not IsPostBack Then
            ' ModalPopupExtender1.Hide()
            pnResult.Visible = False
            pnRequest.Visible = True
            txtUsername.Focus()
        End If
        btnOK.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOK.UniqueID, "")
    End Sub
    Private Sub CheckUser()

        dt = acc.User_GetEmail(txtUsername.Text)  ' enc.EncryptString(txtPassword.Text, True)

        If dt.Rows.Count > 0 Then
            pnResult.Visible = True
            pnRequest.Visible = False

            acc.User_GenLogfile(txtUsername.Text, ACTTYPE_FGT, "Users", "ขอรหัสผ่านเนื่องจากลืมรหัสผ่าน : " & String.Concat(dt.Rows(0).Item("Email")), "ผ่าน")

            SendMailToUser(String.Concat(dt.Rows(0).Item("Email")), dt.Rows(0).Item("USERNAME"), enc.DecryptString(dt.Rows(0).Item("PASSWORD"), True))

        Else

            acc.User_GenLogfile(txtUsername.Text, ACTTYPE_FGT, "Users", "ขอรหัสผ่านเนื่องจากลืมรหัสผ่าน", "ไม่ผ่าน")
            lblAlert.Text = "Username or E-mail not found."
            ModalPopupExtender1.Show()


            ' DisplayMessage(Me.Page, "Username  หรือ E-mail ไม่ถูกต้อง")
            Exit Sub
        End If
    End Sub
    Private Sub SendMailToUser(ByVal sTo As String, username As String, pass As String)
        Try

            Dim MySubject As String = "Forgot Password PPEP"
            Dim MyMessageBody As String = ""

            If Request.Cookies("LANG").Value.ToLower() = "en" Then
                MyMessageBody = "<p>Hello, </p><p> The Professional Internship Database System of the Faculty of Pharmacy, Chulalongkorn University, has sent you your login credentials.<br />"
                MyMessageBody &= "Your login information is as follows: <br />"
                MyMessageBody &= "Username : <b>" & username & "</b><br />"
                MyMessageBody &= "Password : <b>" & pass & "</b><br />"
                MyMessageBody &= " - You can access the system using the credentials provided above to manage various sections. <br/> "
                MyMessageBody &= " - If you encounter any issues or have any questions, please contact us at info.opep@pharm.chula.ac.th or 02-218-8450-1 </p>"
            Else
                MyMessageBody = "<p>สวัสดีครับ ระบบฐานข้อมูลการฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย ได้ทำการส่งรหัสผ่านในการเข้าใช้งานระบบมาให้คุณ<br />"
                MyMessageBody &= "ข้อมูลการเข้าระบบ ของคุณ คือ <br />"
                MyMessageBody &= "Username : <b>" & username & "</b><br />"
                MyMessageBody &= "Password : <b>" & pass & "</b><br />"
                MyMessageBody &= " - คุณสามารถเข้าระบบได้โดยการใช้ข้อมูลดังกล่าวนี้ เพื่อจัดการส่วนต่างๆ <br/> "
                MyMessageBody &= " - หากมีปัญหาการใช้งานหรือข้อสงสัยใดๆ ติดต่อแจ้งเรื่องได้ที่ info.opep@pharm.chula.ac.th หรือ 02-218-8450-1 </p>"
            End If

            Dim RecipientEmail As String = sTo
            Dim SenderEmail As String = "ppepinfo@gmail.com"
            Dim SenderDisplayName As String = "หน่วยฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย"
            Dim SenderEmailPassword As String = "hpxqeawultgdhoim"

            Dim HOST = "smtp.gmail.com"
            Dim PORT = "587" 'TLS Port

            Dim mail As New MailMessage
            mail.Subject = MySubject
            mail.Body = MyMessageBody
            mail.IsBodyHtml = True
            mail.To.Add(RecipientEmail)
            mail.From = New MailAddress(SenderEmail, SenderDisplayName)

            Dim SMTP As New SmtpClient(HOST)
            SMTP.EnableSsl = True
            SMTP.Credentials = New System.Net.NetworkCredential(SenderEmail.Trim(), SenderEmailPassword.Trim())
            SMTP.DeliveryMethod = SmtpDeliveryMethod.Network
            SMTP.Port = PORT
            SMTP.Send(mail)
            'MsgBox("Sent Message To : " & RecipientEmail, MsgBoxStyle.Information, "Sent!")

        Catch ex As Exception
            lblAlert.Text = "Unable to send an E-mail. Please contact the system support team."
            ModalPopupExtender1.Show()
            pnResult.Visible = False
            pnRequest.Visible = True
        End Try
        '(4) Send the MailMessage (will use the Web.config settings)



    End Sub

    Protected Sub cmdSubmit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdSubmit.Click
        If txtUsername.Text = "" Then

            lblAlert.Text = "Please enter your Username or E-mail."
            ModalPopupExtender1.Show()
            ' DisplayMessage(Me.Page, "กรุณาป้อน Username  หรือ E-mail ให้ครบถ้วนก่อน ")
            txtUsername.Focus()
            Exit Sub
        End If

        Session.Contents.RemoveAll()
        CheckUser()
    End Sub
End Class
