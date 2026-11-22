Imports System.Net.Mail
Public Class _Default
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim acc As New UserController
    Dim enc As New CryptographyEngine
    Dim ctlNews As New NewsController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jsString As String = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document.forms[0].all['" + cmdLogin.ClientID + "'].click();return false;} else return true; "
        txtPassword.Attributes.Add("onkeydown", jsString)

        If Request("logout") = "YES" Then
            Session.Contents.RemoveAll()

            'For Each key As String In Request.Cookies.AllKeys
            '    Dim cookie As New HttpCookie(key)
            '    cookie.Expires = DateTime.Now.AddDays(-1)
            '    Response.Cookies.Add(cookie)
            'Next

            Session.Clear()
            Session.Abandon()


        End If

        If Not IsPostBack Then
            ' ModalPopupExtender1.Hide()
            lblVersion.Text = "3.0.0"
            If Request("logout") = "YES" Then
                'For Each key As String In Request.Cookies.AllKeys
                '    Dim cookie As New HttpCookie(key)
                '    cookie.Expires = DateTime.Now.AddDays(-1)
                '    Response.Cookies.Add(cookie)
                'Next

                Session.Clear()
                Session.Abandon()
            End If
            LoadNewsToGrid()
            'SendAlertPreceptor()
            LoadUserOnline()
            txtUserName.Focus()
        End If
    End Sub

    Private Sub SendAlertPreceptor()
        dt = acc.SendAlert_Get
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                acc.SendAlert_Save(dt.Rows(i)("LocationID"), dt.Rows(i)("TimePhaseID"), dt.Rows(i)("Email"), "N")
            Next
        End If

        dt = acc.SendAlert_GetMail
        For i = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                SendMailAlert(.Item("TimePhaseID"), .Item("LocationID"), .Item("LocationName"), .Item("StartDate"), .Item("Email"))
            End With

        Next

    End Sub

    Private Sub SendMailAlert(TimePhaseID As Integer, LocationID As Integer, LocationName As String, StartDate As String, ByVal sTo As String)
        Try

            Dim MySubject As String = "ขอส่งตัวนิสิตเข้าฝึกปฏิบัติวิชาชีพ"
            Dim MyMessageBody As String = ""

            MyMessageBody = "<p>เรียน อาจารย์แหล่งฝึก " & LocationName & "<br />    ศูนย์ฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย ขอส่งนิสิตคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัง เข้าฝึกปฏิบัติวิชาชีพทางเภสัชศาสตร์ ในหน่วยงานของท่าน ในวันที่ " & StartDate & " <br />"

            MyMessageBody &= "  รายละเอียดเพิ่มเติมสามารถดูได้ที่เว็บไซต์ระบบฝึกปฏิบัติวิชาชีพ www.chulappep.com <br/> "
            MyMessageBody &= "  หากมีปัญหาการใช้งานหรือข้อสงสัยใดๆ ติดต่อแจ้งเรื่องได้ที่ info.opep@pharm.chula.ac.th หรือ 02-218-8450-1 </p> <br />"
            MyMessageBody &= "<p> หน่วยฝึกปฏิบัติงานวิชาชีพ  <br />คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย  <br />254 ถนนพญาไท เขตปทุมวัน กรุงเทพมหานคร 10330  <br />โทร 02-218-8450-1 โทรสาร 02-218-8450</p>"

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


            acc.SendAlert_UpdateStatus(LocationID, TimePhaseID, "Y")
        Catch ex As Exception
            DisplayMessage(Me.Page, ex.Message)

        End Try
        '(4) Send the MailMessage (will use the Web.config settings)



    End Sub


    Private Sub LoadNewsToGrid()

        dt = ctlNews.News_GetFirstPage

        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()

            For i = 0 To .Rows.Count - 1
                Dim img1 As Image = .Rows(i).Cells(0).FindControl("imgNews")

                Dim hlnkN As HyperLink = .Rows(i).Cells(0).FindControl("hlnkNews")
                hlnkN.Text = dt.Rows(i)("Title")

                Select Case dt.Rows(i)("NewsType")
                    Case "URL"
                        img1.ImageUrl = "images/www.png"
                        hlnkN.NavigateUrl = String.Concat(dt.Rows(i)("LinkPath"))
                    Case "UPL"
                        Dim str As String()
                        str = Split(Right(String.Concat(dt.Rows(i)("LinkPath")), 6), ".")
                        If str.Length > 1 Then
                            Select Case str(1)
                                Case "pdf"
                                    img1.ImageUrl = "images/pdf.png"
                                Case "doc", "docx"
                                    img1.ImageUrl = "images/word.png"
                                Case "ppt", "pptx"
                                    img1.ImageUrl = "images/ppt.png"
                                Case "xls", "xlsx"
                                    img1.ImageUrl = "images/xls.png"
                                Case Else
                                    img1.ImageUrl = "images/pdf.png"
                            End Select

                            hlnkN.NavigateUrl = tmpUpload & "/" & String.Concat(dt.Rows(i)("LinkPath"))
                        Else
                            hlnkN.NavigateUrl = tmpUpload & "/" & String.Concat(dt.Rows(i)("LinkPath"))
                        End If
                    Case "CON"
                        img1.ImageUrl = "images/newspaper.jpg"
                        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
                    Case Else
                        img1.ImageUrl = "images/newspaper.jpg"
                        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
                End Select

                'Dim img2 As Image = .Rows(i).Cells(0).FindControl("imgNew")
                'img2.Visible = False
                'If DateDiff(DateInterval.Day, Today.Date, dt.Rows(i)("NewsDate")) <= 5 Then
                '    img2.Visible = True
                'End If



                '.Rows(i).Cells(2).Text = "Update เมื่อ" & DisplayDateTH(dt.Rows(i)("NewsDate"))
            Next

        End With

    End Sub


    Private Sub CheckUser()
        dt = acc.User_CheckLogin(txtUserName.Text, enc.EncryptString(txtPassword.Text, True))

        If dt.Rows.Count > 0 Then

            If dt.Rows(0).Item("isPublic") = 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ID ของท่านไม่สามารถใช้งานได้ชั่วคราว');", True)
                Exit Sub
            End If



            Dim ckUserID As New HttpCookie("UserID")
            Dim ckUsername As New HttpCookie("Username")
            'Dim ckPassword As New HttpCookie("Password")

            Dim ckNameOfUser As New HttpCookie("NameOfUser")
            Dim ckUserProfileID As New HttpCookie("UserProfileID")

            Dim ckProfileID As New HttpCookie("ProfileID")
            Dim ckLocationID As New HttpCookie("LocationID")

            Dim ckROLE_STD As New HttpCookie("ROLE_STD")
            Dim ckROLE_TCH As New HttpCookie("ROLE_TCH")

            Dim ckROLE_ADV As New HttpCookie("ROLE_ADV")
            Dim ckROLE_PCT As New HttpCookie("ROLE_PCT")
            Dim ckROLE_ADM As New HttpCookie("ROLE_ADM")
            Dim ckROLE_SPA As New HttpCookie("ROLE_SPA")
            Dim ckROLE_CO1 As New HttpCookie("ROLE_CO1")

            Dim ckEDUYEAR As New HttpCookie("EDUYEAR")
            Dim ckASSMYEAR As New HttpCookie("ASSMYEAR")

            Dim ckLANG As New HttpCookie("LANG")

            'Dim ckAssessorUID As New HttpCookie("AssessorUID")
            'Dim ckLocationAssessment As New HttpCookie("LocationAssessment")
            'Dim ckAssessorRoleID As New HttpCookie("AssessorRoleID")
            'Dim ckCourseAssessment As New HttpCookie("CourseAssessment")


            ckUserID.Value = dt.Rows(0).Item("UserID")
            ckUsername.Value = dt.Rows(0).Item("USERNAME")
            'ckPassword.Value = enc.DecryptString(dt.Rows(0).Item("PASSWORD"), True)
            ckNameOfUser.Value = String.Concat(dt.Rows(0).Item("DisplayName"))
            ckUserProfileID.Value = dt.Rows(0).Item("UserProfileID")
            ckProfileID.Value = dt.Rows(0).Item("ProfileID")
            ckLocationID.Value = String.Concat(dt.Rows(0).Item("LocationID"))
            ckLANG.Value = String.Concat(dt.Rows(0).Item("Lang"))

            ckROLE_STD.Value = False
            ckROLE_TCH.Value = False
            ckROLE_ADV.Value = False
            ckROLE_PCT.Value = False
            ckROLE_ADM.Value = False
            ckROLE_SPA.Value = False
            ckROLE_CO1.Value = False

            'ckAssessorUID.Value = dt.Rows(0).Item("ProfileID")
            'ckAssessorRoleID.Value = ""
            'ckUserID.Value = dt.Rows(0).Item("UserID")
            'ckUsername.Value = dt.Rows(0).Item("USERNAME")
            'ckPassword.Value = enc.DecryptString(dt.Rows(0).Item("PASSWORD"), True)
            'ckNameOfUser.Value = String.Concat(dt.Rows(0).Item("FirstName")) & " " & String.Concat(dt.Rows(0).Item("LastName"))
            'ckUserProfileID.Value  = dt.Rows(0).Item("UserProfileID")
            'ckProfileID.Value = dt.Rows(0).Item("ProfileID")
            'ckLocationID.Value = dt.Rows(0).Item("LocationID")

            'ckROLE_STD.Value = False
            'ckROLE_TCH.Value = False
            'ckROLE_ADV.Value = False
            'ckROLE_PCT.Value = False
            'ckROLE_ADM.Value = False
            'ckROLE_SPA.Value = False

            'ckROLE_CO1.Value = False


            If ckUserProfileID.Value = 2 Then
                ckROLE_TCH.Value = True
            End If

            If ckUserProfileID.Value <> 1 Then
                Dim ctlC As New CourseController
                ckROLE_ADV.Value = ctlC.CoursesCoordinator_CheckStatus(StrNull2Zero(ckProfileID.Value))
            End If

            Dim ctlCfg As New SystemConfigController()
            ckEDUYEAR.Value = ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR)
            ckASSMYEAR.Value = ctlCfg.SystemConfig_GetByCode(CFG_ASSMYEAR)

            genLastLog()

            acc.User_GenLogfile(txtUserName.Text, ACTTYPE_LOG, "Users", "", "")

            Dim rd As New UserRoleController

            dt = rd.UserRole_GetActiveRoleByUID(ckUserID.Value)
            If dt.Rows.Count > 0 Then

                For i = 0 To dt.Rows.Count - 1

                    Select Case dt.Rows(i)("RoleID")
                        Case 1
                            ckROLE_STD.Value = True
                        Case 2
                            ckROLE_TCH.Value = True
                            ' ckROLE_ADV.Value = True
                        Case 3
                            ckROLE_ADV.Value = True
                        Case 4
                            ckROLE_PCT.Value = True
                        Case 5
                            ckROLE_ADM.Value = True
                        Case 6
                            ckROLE_CO1.Value = True
                        Case 9
                            ckROLE_SPA.Value = True
                    End Select

                Next

            End If


            Response.Cookies.Add(ckUserID)
            Response.Cookies.Add(ckUsername)
            'Response.Cookies.Add(ckPassword)

            Response.Cookies.Add(ckNameOfUser)
            Response.Cookies.Add(ckUserProfileID)

            Response.Cookies.Add(ckProfileID)
            Response.Cookies.Add(ckLocationID)

            Response.Cookies.Add(ckROLE_STD)
            Response.Cookies.Add(ckROLE_TCH)

            Response.Cookies.Add(ckROLE_ADV)
            Response.Cookies.Add(ckROLE_PCT)
            Response.Cookies.Add(ckROLE_ADM)
            Response.Cookies.Add(ckROLE_SPA)
            Response.Cookies.Add(ckROLE_CO1)

            Response.Cookies.Add(ckEDUYEAR)
            Response.Cookies.Add(ckASSMYEAR)
            Response.Cookies.Add(ckLANG)
            'Response.Cookies.Add(ckAssessorUID)
            'Response.Cookies.Add(ckLocationAssessment)
            'Response.Cookies.Add(ckAssessorRoleID)
            'Response.Cookies.Add(ckCourseAssessment)


            'Select Case ckUserProfileID.Value
            '    Case 1 'students
            '        Response.Redirect("Default.aspx")
            '    Case 2, 3 'Teacher
            '        Response.Redirect("Default.aspx")
            '    Case 4 'Admin
            'Response.Redirect("Home.aspx")

            If acc.User_CheckPolicyAccept(StrNull2Zero(Request.Cookies("userid").Value)) = "Y" Then
                If Not Request("p") Is Nothing Then
                    Session("AssessorRoleID") = "P"
                    Session("AssessorUID") = Request.Cookies("ProfileID").Value
                    Response.Redirect("AssesseeList.aspx?ActionType=3&p=a&ph=" & Request("ph") & "&lid=" & Request("lid") & "&u=" & Request("u"))
                Else
                    Response.Redirect("Home.aspx?m=h")
                End If
            Else
                If Not Request("p") Is Nothing Then
                    Session("AssessorRoleID") = "P"
                    Session("AssessorUID") = Request.Cookies("ProfileID").Value
                    Response.Redirect("AssesseeList.aspx?ActionType=3&p=a&ph=" & Request("ph") & "&lid=" & Request("lid") & "&u=" & Request("u"))
                Else
                    Response.Redirect("PrivacyPolicy.aspx?p=log")
                End If
            End If

            'End Select

        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','Username  หรือ Password ไม่ถูกต้อง');", True)

            Exit Sub
        End If
    End Sub
    Private Sub LoadUserOnline()

        lblUserOnlineCount.Text = Application("OnlineNow")
        Dim ctlU As New UserController
        dt = ctlU.Users_Online
        lblOnline.Text = "Last online: "
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1

                Select Case i Mod 5
                    Case 0
                        lblOnline.Text &= " <span class='label label-success'>" & dt.Rows(i)("Name") & "</span>"
                    Case 1
                        lblOnline.Text &= " <span class='label label-info'>" & dt.Rows(i)("Name") & "</span>"
                    Case 2
                        lblOnline.Text &= " <span class='label label-danger'>" & dt.Rows(i)("Name") & "</span>"
                    Case 3
                        lblOnline.Text &= " <span class='label label-primary'>" & dt.Rows(i)("Name") & "</span>"
                    Case Else
                        lblOnline.Text &= " <span class='label label-warning'>" & dt.Rows(i)("Name") & "</span>"
                End Select
                'If i Mod 2 = 0 Then
                '    lblOnline.Text &= " <span class='label label-success'>" & dt.Rows(i)("Name") & " : " & dt.Rows(i)("Name") & "</span>"
                '    'lblOnline.Text &= "<font color=#1878ca>" & "[" & dt.Rows(i)("Name") & " : " & dt.Rows(i)("Name") & "]" & "</font>"
                'Else
                '    lblOnline.Text &= " <span class='label label-warning'>" & dt.Rows(i)("Name") & " : " & dt.Rows(i)("Name") & "</span>"
                'End If

            Next

        Else
            lblOnline.Text = "ไม่มี User Online ในขณะนี้"
        End If

    End Sub


    Private Sub genLastLog() ' เก็บวันเวลาที่ User เข้าใช้ระบบครั้งล่าสุด
        acc.User_LastLog_Update(txtUserName.Text)
    End Sub

    Protected Sub cmdLogin_Click(sender As Object, e As EventArgs) Handles cmdLogin.Click

        'txtUserName.Text = "admin"
        'txtPassword.Text = "112233"

        If txtUserName.Text = "" Or txtPassword.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาป้อน Username  หรือ Password ให้ครบถ้วนก่อน');", True)
            txtUserName.Focus()
            Exit Sub
        End If

        Session.Contents.RemoveAll()
        CheckUser()

    End Sub
End Class
