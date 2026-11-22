
Public Class Login
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim acc As New UserController
    Dim enc As New CryptographyEngine

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim jsString As String = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document.forms[0].all['" + cmdLogin.ClientID + "'].click();return false;} else return true; "
        txtPassword.Attributes.Add("onkeydown", jsString)

        If Request("logout") = "YES" Then
            Session.Contents.RemoveAll()
        End If

        If Not IsPostBack Then
            ' ModalPopupExtender1.Hide()

            If Request("logout") = "YES" Then
                Session.Abandon()
                Request.Cookies("Username").Value = Nothing
            End If

            txtUserName.Focus()
        End If


        btnOK.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOK.UniqueID, "")

    End Sub

    Private Sub CheckUser()
        dt = acc.User_CheckLogin(txtUserName.Text, enc.EncryptString(txtPassword.Text, True))

        If dt.Rows.Count > 0 Then

            If dt.Rows(0).Item("isPublic") = 0 Then

                lblAlert.Text = "ID ของท่านไม่สามารถใช้งานได้ชั่วคราว"
                ModalPopupExtender1.Show()

                ' DisplayMessage(Me.Page, "ID ของท่านไม่สามารถใช้งานได้ชั่วคราว")
                Exit Sub
            End If

            Session("userid") = dt.Rows(0).Item("UserID")
            Request.Cookies("Username").Value = dt.Rows(0).Item("USERNAME")
            Session("Password") = enc.DecryptString(dt.Rows(0).Item("PASSWORD"), True)
            'Session("UserRole") = dt.Rows(0).Item("USERROLE")
            Session("LastLog") = String.Concat(dt.Rows(0).Item("LastLogin"))
            Session("NameOfUser") = String.Concat(dt.Rows(0).Item("FirstName")) & " " & String.Concat(dt.Rows(0).Item("LastName"))
            Session("Email") = String.Concat(dt.Rows(0).Item("Email"))
            Session("UserProfileID") = dt.Rows(0).Item("UserProfileID")
            Session("ProfileID") = dt.Rows(0).Item("ProfileID")

            Session("ROLE_STD") = False
            Session("ROLE_TCH") = False
            Session("ROLE_ADV") = False
            Session("ROLE_PCT") = False
            Session("ROLE_ADM") = False



            genLastLog()

            acc.User_GenLogfile(txtUserName.Text, ACTTYPE_LOG, "Users", "", "")

            Dim rd As New UserRoleController

            dt = rd.UserRole_GetActiveRoleByUID(Session("userid"))
            If dt.Rows.Count > 0 Then

                For i = 0 To dt.Rows.Count - 1

                    Select Case dt.Rows(i)("RoleID")
                        Case 1
                            Session("ROLE_STD") = True
                        Case 2
                            Session("ROLE_TCH") = True
                        Case 3
                            Session("ROLE_ADV") = True
                        Case 4
                            Session("ROLE_PCT") = True
                        Case 5
                            Session("ROLE_ADM") = True
                    End Select

                Next

            End If


            'Select Case Session("UserProfileID")
            '    Case 1 'students
            '        Response.Redirect("Default.aspx")
            '    Case 2, 3 'Teacher
            '        Response.Redirect("Default.aspx")
            '    Case 4 'Admin
            Response.Redirect("Home.aspx")
            'End Select

        Else

            lblAlert.Text = "Username  หรือ Password ไม่ถูกต้อง"
            ModalPopupExtender1.Show()


            ' DisplayMessage(Me.Page, "Username  หรือ Password ไม่ถูกต้อง")
            Exit Sub
        End If
    End Sub

    Private Sub genLastLog() ' เก็บวันเวลาที่ User เข้าใช้ระบบครั้งล่าสุด
        acc.User_LastLog_Update(txtUserName.Text)
    End Sub

    Protected Sub cmdLogin_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdLogin.Click

        If txtUserName.Text = "" Or txtPassword.Text = "" Then

            lblAlert.Text = "กรุณาป้อน Username  หรือ Password ให้ครบถ้วนก่อน"
            ModalPopupExtender1.Show()
            ' DisplayMessage(Me.Page, "กรุณาป้อน Username  หรือ Password ให้ครบถ้วนก่อน ")
            txtUserName.Focus()
            Exit Sub
        End If

        Session.Contents.RemoveAll()
        CheckUser()

    End Sub
End Class


