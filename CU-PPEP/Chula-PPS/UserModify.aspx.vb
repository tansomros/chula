Public Class UserModify
    Inherits System.Web.UI.Page
    Dim ctlU As New UserController
    Dim ctlR As New UserRoleController
    Dim enc As New CryptographyEngine
    Dim ctlRole As New RoleController

    Dim ctlStd As New StudentController
    Dim ctlPsn As New PersonController
    Dim ctlPct As New PreceptorController
    Dim ctlLct As New LocationController
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            chkStatus.Checked = True
            lblUserID.Text = "0"
            cmdDelete.Enabled = False
            pnLocation.Visible = False
            pnPerson.Visible = False
            BindUserGroup()
            LoadPersonToDDL()
            LoadPreceptor()

            If Not Request("uid") = Nothing Then
                EditUser()
            End If
        End If

        Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
        cmdDelete.Attributes.Add("onClick", scriptString)
        txtEmail.Attributes.Add("OnKeyPress", "return NotAllowThai();")
        txtUsername.Attributes.Add("OnKeyPress", "return AllowOnlyEnglishAndInteger();")

    End Sub
    Private Sub BindUserGroup()
        chkRoles.Items.Clear()
        chkRoles.Items.Add("Student")
        chkRoles.Items(0).Value = 1
        chkRoles.Items.Add("Faculty member")
        chkRoles.Items(1).Value = 2
        chkRoles.Items.Add("Course coordinator")
        chkRoles.Items(2).Value = 3
        chkRoles.Items.Add("Preceptor")
        chkRoles.Items(3).Value = 4
        chkRoles.Items.Add("Administrator")
        chkRoles.Items(4).Value = 5
        If Convert.ToBoolean(Request.Cookies("ROLE_SPA").Value) = True Then
            chkRoles.Items.Add("Super Admin")
            chkRoles.Items(5).Value = 9
        End If
    End Sub
    Private Sub LoadPersonToDDL()
        If lblUserID.Text = "0" Then
            If ddlUserGroup.SelectedValue = 2 Then
                dt = ctlPsn.Person_GetNoUserByType("T", "")
            Else
                dt = ctlPsn.Person_GetNoUserByType("A", "")
            End If
        Else
            If ddlUserGroup.SelectedValue = 2 Then
                dt = ctlPsn.Person_GetByType("T")
            Else
                dt = ctlPsn.Person_GetByType("A")
            End If
        End If

        ddlPerson.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlPerson
                .Visible = True
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & String.Concat(dt.Rows(i)("FirstName")) & " " & String.Concat(dt.Rows(i)("LastName")))
                    .Items(i).Value = dt.Rows(i)("PersonID")
                Next
                .SelectedIndex = 0
            End With

        End If
        dt = Nothing
    End Sub
    Private Sub LoadPreceptor()
        Dim dtPS As New DataTable
        If lblUserID.Text = "0" Then
            dtPS = ctlPsn.Preceptor_GetNoUser
        Else
            Dim lid As Integer = ctlU.User_GetLocationIdByUserID(lblUserID.Text)
            dtPS = ctlPsn.Preceptor_GetByLocation(lid)
        End If
        ddlLocation.DataSource = dtPS
        ddlLocation.DataTextField = "PreceptorName"
        ddlLocation.DataValueField = "PersonID"
        ddlLocation.DataBind()
    End Sub

    Private Sub EditUser()
        Dim dt As New DataTable
        dt = ctlU.User_GetByUserID(Request("uid"))

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                txtFirstName.ReadOnly = True
                txtLastName.ReadOnly = True
                lblUserID.Text = String.Concat(.Item("UserID"))
                txtUsername.Text = .Item("Username")
                txtPassword.Text = enc.DecryptString(.Item("Password"), True)

                Dim sName() As String = Split(String.Concat(.Item("PersonName")), "|")
                If sName.Length > 0 Then
                    If Not sName(0) Is Nothing Then
                        txtFirstName.Text = sName(0)
                    End If
                End If

                If sName.Length > 1 Then
                    If Not sName(1) Is Nothing Then
                        txtLastName.Text = sName(1)
                    End If
                End If


                txtDisplayName.Text = String.Concat(.Item("DisplayName"))
                txtEmail.Text = String.Concat(.Item("Email"))

                pnLocation.Visible = False
                pnPerson.Visible = False

                Select Case DBNull2Zero(.Item("UserProfileID"))
                    Case 1
                        ddlUserGroup.SelectedValue = 1
                    Case 2
                        ddlUserGroup.SelectedValue = 2
                        pnPerson.Visible = True
                        LoadPersonToDDL()
                        ddlPerson.SelectedValue = String.Concat(.Item("ProfileID"))
                    Case 3
                        ddlUserGroup.SelectedValue = 3
                        LoadPreceptor()
                        ddlLocation.SelectedValue = String.Concat(.Item("ProfileID"))
                        pnLocation.Visible = True
                    Case 4
                        ddlUserGroup.SelectedValue = 4
                        LoadPersonToDDL()
                    Case Else
                End Select
                txtUsername.Text = String.Concat(.Item("Username"))
                txtPassword.Text = enc.DecryptString(String.Concat(dt.Rows(0)("Password")), True)
                chkStatus.Checked = ConvertStatusFlag2Boolean(String.Concat(.Item("StatusFlag")))

                cmdDelete.Enabled = True

                Dim ctlur As New UserRoleController
                Dim dtR As New DataTable

                dtR = ctlur.UserRole_GetByUserID(DBNull2Zero(.Item("UserID")))
                chkRoles.ClearSelection()

                If dtR.Rows.Count > 0 Then
                    For i = 0 To dtR.Rows.Count - 1
                        For n = 0 To chkRoles.Items.Count - 1
                            If dtR.Rows(i)("RoleID") = chkRoles.Items(n).Value Then
                                If dtR.Rows(i)("isActive") = 1 Then
                                    chkRoles.Items(n).Selected = True
                                Else
                                    chkRoles.Items(n).Selected = False
                                End If
                            End If
                        Next
                    Next
                End If
            End With
        End If
    End Sub
    Function IsHasRole(rid As Integer) As Boolean
        Dim dtR As New DataTable
        dtR = ctlRole.UserRole_chkHasRole(txtUsername.Text, rid)
        If dtR.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Function Check_Email(str As String) As Boolean
        Return Regex.IsMatch(str, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
    End Function
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtDisplayName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกชื่อที่แสดง');", True)
            Exit Sub
        End If
        If txtEmail.Text <> "" Then
            If Check_Email(txtEmail.Text) = False Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','รูปแบบ E-mail ไม่ถูกต้อง กรุณาตรวจสอบ');", True)
                Exit Sub
            End If
        End If

        If txtUsername.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอก Username');", True)
            Exit Sub
        End If
        If txtPassword.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุ Password');", True)
            Exit Sub
        End If

        If chkRoles.SelectedValue = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุสิทธิ์ผู้ใช้งานก่อน');", True)
            Exit Sub
        End If

        If lblUserID.Text = "0" Then
            If ctlU.User_CheckDuplicate(txtUsername.Text) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','Username นี้มีในระบบแล้ว');", True)
                Exit Sub
            End If
        End If

        Dim Password As String = enc.EncryptString(txtPassword.Text, True)
        'Dim LocationUID As Integer
        Dim PersonID As String = ""
        Dim item As Integer
        Dim sProfileID As String = ""

        Dim IsPublic As Integer = 0
        Dim StatusFlag As String = "D"
        Dim IsSuperAdmin As Integer = 0
        If chkStatus.Checked Then
            IsPublic = 1
            StatusFlag = "A"
        End If

        If Convert.ToBoolean(Request.Cookies("ROLE_SPA").Value) = True Then
            If chkRoles.Items(5).Selected Then
                IsSuperAdmin = 1
            End If
        End If

        Select Case ddlUserGroup.SelectedValue
            Case 1 'Student
                sProfileID = txtUsername.Text
            Case 2, 4
                If lblUserID.Text <> "0" Then
                    sProfileID = ctlPsn.Person_GetPersonIDByUserID(lblUserID.Text).ToString()
                Else
                    sProfileID = ddlPerson.SelectedValue
                End If
            Case 3 'Preceptor
                sProfileID = ddlLocation.SelectedValue
            Case Else
                sProfileID = ""
        End Select


        If lblUserID.Text = "0" Then 'add new 

            item = ctlU.User_Add(txtUsername.Text, enc.EncryptString(txtPassword.Text, True), txtDisplayName.Text, 0, StatusFlag, ddlUserGroup.SelectedValue, sProfileID)

            'userid = ctlu.GetUsersID_ByUsername(txtUsername.Text)

            If ddlUserGroup.SelectedValue = 4 Then
                ctlPsn.Person_AddUser(txtUsername.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, "A", 1)
                'ctlu.User_UpdateProfileID(txtUsername.Text)
            End If

            ctlU.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Persons", "add new Person :" & txtFirstName.Text, "")
            ctlU.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Users", "add new user :" & txtUsername.Text, "Result:" & item)

            ctlU.User_UpdateProfileID(txtUsername.Text, sProfileID)
        Else 'Update user

            item = ctlU.User_Update(StrNull2Zero(lblUserID.Text), enc.EncryptString(txtPassword.Text, True), txtDisplayName.Text, txtEmail.Text, IsSuperAdmin, StatusFlag, sProfileID)

            If ddlUserGroup.SelectedValue <> 1 Then
                ctlPsn.Person_UpdateUserIDandMail(StrNull2Zero(sProfileID), lblUserID.Text, txtEmail.Text)
            End If

            ctlU.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Users", "update user :" & txtUsername.Text, "Result:" & item)

        End If

        If lblUserID.Text = "0" Then
            lblUserID.Text = ctlU.User_GetUID(txtUsername.Text)
        End If
        For i = 0 To chkRoles.Items.Count - 1
            If IsHasRole(chkRoles.Items(i).Value) Then 'มีสิทธิ์อยู่แล้ว
                If chkRoles.Items(i).Selected Then
                    'เลือก
                    item = ctlU.UserRole_UpdateStatus(txtUsername.Text, chkRoles.Items(i).Value, bActive, Request.Cookies("UserID").Value)

                    ctlU.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "UserRole", "เพิ่มสิทธิ์ : " & chkRoles.Items(i).Text & " ให้ :" & txtUsername.Text, "Result:" & item)
                Else 'ไม่เลือก ทำการลบสิทธิ์โดย set สถานะไม่ใช้งาน
                    item = ctlU.UserRole_UpdateStatus(txtUsername.Text, chkRoles.Items(i).Value, binActive, Request.Cookies("UserID").Value)

                    ctlU.User_GenLogfile(Request.Cookies("Username").Value, "DEL", "UserRole", "ยกเลิกสิทธิ์ : " & chkRoles.Items(i).Text & " ของ :" & txtUsername.Text, "Result:" & item)

                End If
            Else 'ไม่มีสิทธิ์อยู่แล้ว และถ้าเลือกทำการเพิ่มใหม่
                If chkRoles.Items(i).Selected Then
                    item = ctlU.UserRole_Add(txtUsername.Text, chkRoles.Items(i).Value, Request.Cookies("UserID").Value)

                    ctlU.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "UserRole", "เพิ่มสิทธิ์ : " & chkRoles.Items(i).Text & " ให้ :" & txtUsername.Text, "Result:" & item)

                End If
            End If
        Next

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)

    End Sub
    Private Sub ClearData()
        lblUserID.Text = "0"
        txtFirstName.ReadOnly = False
        txtLastName.ReadOnly = False
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtEmail.Text = ""
        txtDisplayName.Text = ""
        ddlUserGroup.SelectedIndex = 1
        pnLocation.Visible = False
        pnPerson.Visible = False
        cmdDelete.Enabled = False
    End Sub
    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        ctlU.User_Delete(lblUserID.Text)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'ผลการทำงาน','ลบข้อมูลเรียบร้อย');", True)

        Response.Redirect("Users?m=u&s=u")
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub ddlUserGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUserGroup.SelectedIndexChanged
        If ddlUserGroup.SelectedValue = "3" Then
            pnLocation.Visible = True
            pnPerson.Visible = False
        ElseIf ddlUserGroup.SelectedValue = "2" Then
            pnPerson.Visible = True
            pnLocation.Visible = False
        Else
            pnLocation.Visible = False
            pnPerson.Visible = False

        End If
        LoadPersonToDDL()
    End Sub

    Private Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged

    End Sub

    Private Sub ddlPerson_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPerson.SelectedIndexChanged

    End Sub
End Class