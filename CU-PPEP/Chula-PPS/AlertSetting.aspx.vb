Imports System.Net.Mail
Imports DevExpress.CodeParser
Imports DevExpress.Office.PInvoke

Public Class AlertSetting
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim ctlCs As New CourseController
    Dim ctlU As New UserController
    Dim dt As New DataTable
    Public dtMail As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            lblYear.Text = Request.Cookies("ASSMYEAR").Value

            LoadTimePhase()
            LoadCourseToDDL()
            If Request("t") = "send" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSend('ยืนยันการลบ','คุณต้องการส่งอีเมล์ให้แหล่งฝึกนี้ใช่หรือไม่?');", True)
            End If
            If Not Request("ph") Is Nothing Then
                ddlPhase.SelectedValue = Request("ph")
            End If

            LoadLocationToGrid()
        End If

    End Sub
    Private Sub LoadTimePhase()
        Dim ctlTP As New TimePhaseController
        dt = ctlTP.Phase_GetByYear(lblYear.Text)
        If dt.Rows.Count > 0 Then
            ddlPhase.Items.Clear()
            ddlPhase.Items.Add("---ทั้งหมด---")
            ddlPhase.Items(0).Value = 0

            For i = 1 To dt.Rows.Count
                With ddlPhase
                    .Items.Add(dt.Rows(i - 1)("Descriptions"))
                    .Items(i).Value = dt.Rows(i - 1)("PhaseID")
                End With
            Next
            ddlPhase.SelectedIndex = 0
        Else
            ddlPhase.DataSource = dt
            ddlPhase.DataBind()
        End If
    End Sub

    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()

        dt = ctlCs.Courses_GetFromAssessment(StrNull2Zero(lblYear.Text))

        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            With ddlCourse
                .Items.Add("---ทั้งหมด---")
                .Items(0).Value = ""

                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("SubjectName"))
                    .Items(i + 1).Value = dt.Rows(i)("SubjectCode")
                Next
            End With
        End If

    End Sub

    Private Sub LoadLocationToGrid()
        dtMail = ctlU.LocationAlertMail_Get(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlPhase.SelectedValue), ddlCourse.SelectedValue, ddlStatus.SelectedValue)
        dt = Nothing
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click

        LoadLocationToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        LoadLocationToGrid()
    End Sub

    Private Sub AssessmentAlertPreceptor()
        Dim enc As New CryptographyEngine
        Dim Password As String = ""
        'ครั้งละ 20 ฉบับ
        dt = ctlU.AssessmentAlert_GetMail(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlPhase.SelectedValue), ddlCourse.SelectedValue, ddlStatus.SelectedValue)
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                Password = ""
                Password = enc.DecryptString(dt.Rows(i)("Password"), True)
                ctlU.AssessmentAlert_Save(dt.Rows(i)("LocationID"), dt.Rows(i)("PhaseID"), dt.Rows(i)("Email"), "N", dt.Rows(i)("Username"), Password)

                SendEmail(dt.Rows(i)("Email"), dt.Rows(i)("Lang"), dt.Rows(i)("PhaseID"), dt.Rows(i)("LocationID"), dt.Rows(i)("LocationName"), dt.Rows(i)("Username"), Password, dt.Rows(i)("EndDate"))

            Next
        End If
    End Sub

    Private Sub SendEmail(Email As String, Lang As String, PhaseID As String, LocationID As Integer, LocationName As String, Username As String, Password As String, EndDate As String)
        Dim MySubject As String = ""
        Dim MyMessageBody As String = ""
        Dim SenderDisplayName As String = ""
        Try
            If Lang = "EN" Then
                SenderDisplayName = "Faculty of Pharmaceutical Sciences, Chulalongkorn University"
                MySubject = "Request for Evaluation of Professional Practice Performance"
                MyMessageBody = ""
                MyMessageBody = "<div>"

                MyMessageBody = "<p style=""font-size:16px;line-height:26px;""><strong> Dear Internship Supervisor,  " & LocationName & "</strong></p>"
                MyMessageBody &= "<p style=""font-size:16px;line-height:26px;"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Professional Practice Training Center, Faculty of Pharmaceutical Sciences, Chulalongkorn University, kindly requests your cooperation in evaluating the professional practice performance.<br />"
                MyMessageBody &= "You may complete the evaluation by clicking  <a href=""https://dev.chulappep.com/Default.aspx?logout=YES&p=ma&ph=" & PhaseID & "&lid=" & LocationID & "&u=" & Username & """ target=""_bank"">here</a>, and use the following credentials to log in:</p>
<ul>
        <li style=""font-size:16px;line-height:26px;"">
        <p> <strong> Username : </strong>  " & Username & " </p>
</li>
<li style=""font-size:16px;line-height:26px;"">
            <p> <strong> Password : </strong> " & Password & "</p>
</li>
</ul> "


                MyMessageBody &= "<p style=""font-size:16px;line-height:26px;"">The evaluation form will be accessible until  <strong>" & EndDate & "</strong> </p>"

                MyMessageBody &= " <p style=""font-size:16px;line-height:26px;""> For further details, please visit the Professional Practice System website at www.chulappep.com <br /> "
                MyMessageBody &= "Should you encounter any issues or have any inquiries, please contact us at info.opep@pharm.chula.ac.th  or call +66 2 218 8450-1. </p>"

                MyMessageBody &= "<p style=""font-size:16px;line-height:26px;""> Sincerely, <br />
Professional Practice Training Unit <br />
Faculty of Pharmaceutical Sciences, Chulalongkorn University <br />
254 Phayathai Road, Pathumwan District, Bangkok 10330
Tel: +66 2 218 8450-1 | Fax: +66 2 218 8450 <br /> </p></div>"

            Else
                SenderDisplayName = "หน่วยฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย"
                MySubject = "แบบประเมินผลการฝึกปฏิบัติวิชาชีพเภสัชกรรม จุฬาลงกรณ์มหาวิทยาลัย"
                MyMessageBody = ""

                MyMessageBody = "<div>"
                MyMessageBody = "<p style=""font-size:16px;line-height:26px;""><strong>เรียน อาจารย์แหล่งฝึก " & LocationName & "</strong></p>  "
                MyMessageBody &= "<p style=""font-size:16px;line-height:26px;"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ศูนย์ฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย ขอความร่วมมือให้ท่านทำการประเมินผลการปฏิบัติงานวิชาชีพ <br />"
                MyMessageBody &= "โดยประเมินได้โดย  <a href=""https://dev.chulappep.com/Default.aspx?logout=YES&p=ma&ph=" & PhaseID & "&lid=" & LocationID & "&u=" & Username & """ target=""_bank"">คลิกที่นี่</a> และป้อน Username และ Password ของท่านเพื่อเข้าสู่ระบบ :</p>
<ul>
        <li style=""font-size:16px;line-height:26px;"">
        <p> <strong> Username : </strong>  " & Username & " </p>
</li>
<li style=""font-size:16px;line-height:26px;"">
            <p> <strong> Password :  </strong> " & Password & "</p>
</li>
</ul> "

                MyMessageBody &= "<p style=""font-size:16px;line-height:26px;"">ซึ่งท่านสามารถเข้าระบบเพื่อทำแบบประเมินได้ถึงวันที่  <strong>" & EndDate & "</strong></p>"
                MyMessageBody &= "<p style=""font-size:16px;line-height:26px;"">รายละเอียดเพิ่มเติมสามารถดูได้ที่เว็บไซต์ระบบฝึกปฏิบัติวิชาชีพ www.chulappep.com  <br />"
                MyMessageBody &= "หากมีปัญหาการใช้งานหรือข้อสงสัยใดๆ ติดต่อแจ้งเรื่องได้ที่ info.opep@pharm.chula.ac.th หรือ 02-218-8450-1 </p>"

                MyMessageBody &= "<p style=""font-size:16px;line-height:26px;""> หน่วยฝึกปฏิบัติงานวิชาชีพ  <br />คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย  <br />254 ถนนพญาไท เขตปทุมวัน กรุงเทพมหานคร 10330  <br />โทร 02-218-8450-1 โทรสาร 02-218-8450</p></div>"

            End If

            Dim RecipientEmail As String = Email
            Dim SenderEmail As String = "ppepinfo@gmail.com"
            Dim SenderEmailPassword As String = "hpxqeawultgdhoim" ' "ppep@2018"

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


            ctlU.AssessmentAlert_UpdateStatus(LocationID, PhaseID, "Y", Email)
        Catch ex As Exception
            Exit Sub
        End Try
        '(4) Send the MailMessage (will use the Web.config settings)

    End Sub
    Private Sub ddlPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPhase.SelectedIndexChanged

        LoadLocationToGrid()
    End Sub

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged

        LoadLocationToGrid()
    End Sub

    Private Sub cmdSend_Click(sender As Object, e As EventArgs) Handles cmdSend.Click
        AssessmentAlertPreceptor()
        LoadLocationToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','Completed successfully.');", True)
    End Sub

    Private Sub cmdConfirm_Click(sender As Object, e As EventArgs) Handles cmdConfirm.Click

        Dim enc As New CryptographyEngine
        Dim Password As String = ""
        dt = ctlU.AssessmentAlert_GetMailByPerson(StrNull2Zero(lblYear.Text), Request("pid"), Request("ph"), Request("lid"))
        If dt.Rows.Count > 0 Then
            'For i = 0 To dt.Rows.Count - 1
            Password = ""
            Password = enc.DecryptString(dt.Rows(0)("Password"), True)
            ctlU.AssessmentAlert_Save(dt.Rows(0)("LocationID"), dt.Rows(0)("PhaseID"), dt.Rows(0)("Email"), "N", dt.Rows(0)("Username"), Password)

            SendEmail(dt.Rows(0)("Email"), dt.Rows(0)("Lang"), dt.Rows(0)("PhaseID"), dt.Rows(0)("LocationID"), dt.Rows(0)("LocationName"), dt.Rows(0)("Username"), Password, dt.Rows(0)("EndDate"))

            'Next
        End If

        LoadLocationToGrid()
        'Response.Redirect("RequestList?m=find&s=1")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','Completed successfully.');", True)
    End Sub
End Class

