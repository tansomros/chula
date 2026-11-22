
Public Class Login
    Inherits Page
    Dim dt As New DataTable
    Dim acc As New UserController
    Dim enc As New CryptographyEngine
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not Request("logout") Is Nothing Then
            If Request("logout").ToLower() = "yes" Or Request("logout").ToLower() = "y" Then
                Session.Contents.RemoveAll()
                Session.Abandon()
                Session.RemoveAll()
                Response.Cookies.Clear()
                Request.Cookies.Clear()
                'Dim delCookie1 As HttpCookie
                'delCookie1 = New HttpCookie("ChulaBCP")
                'delCookie1.Expires = DateTime.Now.AddDays(-1D)
                'Response.Cookies.Add(delCookie1)
            End If
        End If

        If Not IsPostBack Then
            txtUsername.Focus()
        End If
        Session.Timeout = 360
    End Sub

    Private Sub CheckUser()
        dt = acc.User_Login(txtUsername.Text, enc.EncryptString(txtPassword.Text, True))

        If dt.Rows.Count > 0 Then

            If dt.Rows(0).Item("StatusFlag") <> "A" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ID ของท่านไม่สามารถใช้งานได้ชั่วคราว');", True)
                Exit Sub
            End If

            '--------------------------------------------------------------------------------------------
            Response.Cookies.Clear()
            Request.Cookies.Clear()
            Dim ckLoginID As New HttpCookie("UserLoginID")
            Dim ckUserLogin As New HttpCookie("UserLogin")
            'Dim ckUserProfile As New HttpCookie("UserProfile")
            Dim ckPassword As New HttpCookie("Password")
            Dim ckNameOfUser As New HttpCookie("NameOfUser")
            'Dim ckLastLogin As New HttpCookie("LastLogin")
            Dim ckRoleID As New HttpCookie("RoleID")
            Dim ckPersonID As New HttpCookie("PersonID")
            Dim ckUserProfileID As New HttpCookie("UserProfileID")
            Dim ckProfileID As New HttpCookie("ProfileID")
            Dim ckLocationID As New HttpCookie("LocationID")

            Dim ckROLE_SPA As New HttpCookie("ROLE_SPA")
            Dim ckROLE_ADM As New HttpCookie("ROLE_ADM")
            Dim ckROLE_STD As New HttpCookie("ROLE_STD")
            Dim ckROLE_TCH As New HttpCookie("ROLE_TCH")
            Dim ckROLE_USR As New HttpCookie("ROLE_USR")
            Dim ckROLE_PCT As New HttpCookie("ROLE_PCT")

            Dim ckEDUYEAR As New HttpCookie("EDUYEAR")
            Dim ckASSMYEAR As New HttpCookie("ASSMYEAR")


            'Dim BCPCookie As HttpCookie = New HttpCookie("ChulaBCP")
            ckLoginID.Value = dt.Rows(0).Item("UserID")
            ckUserLogin.Value = String.Concat(dt.Rows(0).Item("Username")).ToString()
            ckPassword.Value = enc.DecryptString(dt.Rows(0).Item("Password"), True)
            'ckLastLogin.value =  String.Concat(dt.Rows(0).Item("LastLogin"))
            ckNameOfUser.Value = String.Concat(dt.Rows(0).Item("Name"))
            ckRoleID.Value = String.Concat(dt.Rows(0).Item("RoleID"))
            ckPersonID.Value = String.Concat(dt.Rows(0).Item("PersonID"))
            ckLocationID.Value = String.Concat(dt.Rows(0).Item("LocationID"))

            ckROLE_STD.Value = False
            ckROLE_TCH.Value = False
            ckROLE_ADM.Value = False
            ckROLE_USR.Value = False
            ckROLE_SPA.Value = False
            ckROLE_PCT.Value = False

            Session("userid") = dt.Rows(0).Item("UserID")
            Session("uname") = String.Concat(dt.Rows(0).Item("Name"))

            Dim rd As New UserRoleController
            dt = rd.UserRole_GetActiveRoleByUID(ckLoginID.Value)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Select Case dt.Rows(i)("RoleID")
                        Case 1 'Administrator
                            ckROLE_ADM.Value = True
                        Case 2  'นิสิต/เภสัชกรประจำบ้าน
                            ckROLE_STD.Value = True
                        Case 3 'อาจารย์ประจำหลักสูตร
                            ckROLE_TCH.Value = True
                        Case 4 'General User
                            ckROLE_USR.Value = True
                        Case 9
                            ckROLE_SPA.Value = True
                            ckROLE_ADM.Value = True
                            'Response.Cookies("ROLE_SPA").Value = True
                            'Response.Cookies("ROLE_ADM").Value = True
                    End Select
                Next
            End If
            Dim ctlCfg As New SystemConfigController()
            ckEDUYEAR.Value = ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR)
            ckASSMYEAR.Value = ctlCfg.SystemConfig_GetByCode(CFG_ASSMYEAR)

            ckUserLogin.Expires = acc.GET_DATE_SERVER().AddMinutes(60)


            Response.Cookies.Add(ckLoginID)
            Response.Cookies.Add(ckUserLogin)
            Response.Cookies.Add(ckPassword)
            Response.Cookies.Add(ckNameOfUser)
            Response.Cookies.Add(ckRoleID)
            Response.Cookies.Add(ckPersonID)
            Response.Cookies.Add(ckUserProfileID)
            Response.Cookies.Add(ckProfileID)
            Response.Cookies.Add(ckLocationID)
            Response.Cookies.Add(ckROLE_STD)
            Response.Cookies.Add(ckROLE_TCH)
            Response.Cookies.Add(ckROLE_ADM)
            Response.Cookies.Add(ckROLE_SPA)
            Response.Cookies.Add(ckROLE_USR)
            Response.Cookies.Add(ckROLE_PCT)
            Response.Cookies.Add(ckEDUYEAR)
            Response.Cookies.Add(ckASSMYEAR)


            Response.Redirect("Default")


        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','Username  หรือ Password ไม่ถูกต้อง');", True)
            Exit Sub
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        'txtUsername.Text = "admin"
        'txtPassword.Text = "1234"
        'Session("userid") = 1
        'Request.Cookies("UserLoginID").Value = 1
        'Request.Cookies("ROLE_ADM").Value = True
        'Response.Redirect("Default")
        If txtUsername.Text = "" Or txtPassword.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Warning!','กรุณาป้อน Username  หรือ Password ให้ครบถ้วนก่อน');", True)
            txtUsername.Focus()
            Exit Sub
        End If

        Session.Contents.RemoveAll()
        CheckUser()
    End Sub

    Private Sub genLastLog() ' เก็บวันเวลาที่ User เข้าใช้ระบบครั้งล่าสุด
        acc.User_LastLog_Update(txtUsername.Text)
    End Sub

End Class