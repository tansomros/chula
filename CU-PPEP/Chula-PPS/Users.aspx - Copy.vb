
Public Class Users
    Inherits System.Web.UI.Page
     
    Dim dt As New DataTable
  
    Dim objUser As New UserController
    Dim ctlRole As New RoleController
    Dim ctlStd As New StudentController

    Dim ctlPsn As New PersonController

    Dim ctlPct As New PreceptorController
    Dim objLct As New LocationInfo
    Dim ctlLct As New LocationController
    Dim objEn As New CryptographyEngine

    Dim objRole As New RoleInfo
    Dim objStd As New StudentInfo
    Dim objPsn As New PersonInfo
    'Dim ctlPsn As New PreceptorInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            grdData.PageIndex = 0

            txtUsername.ReadOnly = False
            lblID.Text = "0"
            isAdd = True

            LoadUserAccountToGrid()

            LoadRolsToCheckList()
            LoadStudentToDDL()
            LoadLocationToDDL()


            'txtFindSpec.Visible = False
            'txtFindLocation.Visible = False
            '' txtFindPreceptor.visible = False

            'ddlLocation.Visible = False
            'ddlUserID.Visible = False
            'ddlPreceptor.Visible = False

            'lnkFind.Visible = False
            'lnkFindLocation.Visible = False
            ''  lnkFindPreceptor.visible = False
            'lblselect.Visible = False
            'lblPreceptor.Visible = False
            'lblLocation.Visible = False
            'imgArrowLocation.Visible = False
            'imgArrowUser.Visible = False

            ClearData()

        End If
    End Sub

    Private Sub LoadRolsToCheckList()
        dt = ctlRole.GetRoles
        With chkRoles
            .Visible = True
            .DataSource = dt
            .DataTextField = objRole.tblField(objRole.fldPos.f01_RoleName).fldName
            .DataValueField = objRole.tblField(objRole.fldPos.f00_RoleID).fldName
            .DataBind()
        End With
        dt = Nothing
    End Sub

    Private Sub LoadStudentToDDL()
        If lblID.Text = "" Then
            dt = ctlStd.Student_NoUserBySearch(txtFindSpec.Text)
        Else
            dt = ctlStd.Student_GetAll
        End If

        ddlUserID.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlUserID
                .Visible = True
                .DataSource = dt
                .DataTextField = "StudentName"
                .DataValueField = "Student_Code"
                .DataBind()
                .SelectedIndex = 0
                ShowNameStudent()

            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadTeacherToDDL()

        dt = ctlPsn.Person_GetNoUserByType("T", txtFindSpec.Text)

        ddlUserID.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlUserID
                .Visible = True
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f02_FirstName).fldName) & " " & dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f03_LastName).fldName))
                    .Items(i).Value = dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f00_PersonID).fldName)
                Next
                .SelectedIndex = 0

            End With

        End If
        dt = Nothing
    End Sub

    Private Sub LoadPersonToDDL(pType As String)

        dt = ctlPsn.Person_GetNoUserByType(pType, txtFindSpec.Text)

        ddlUserID.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlUserID
                .Visible = True
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f02_FirstName).fldName) & " " & dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f03_LastName).fldName))
                    .Items(i).Value = dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f00_PersonID).fldName)
                Next
                .SelectedIndex = 0

            End With

        End If
        dt = Nothing
    End Sub

    Private Sub LoadLocationToDDL()
        If txtFindLocation.Text = "" Then
            dt = ctlLct.Location_Get
        Else
            dt = ctlLct.Location_GetBySearch(txtFindLocation.Text)
        End If

        ddlLocation.Items.Clear()

        If dt.Rows.Count > 0 Then
            With ddlLocation
                .Visible = True
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add(dt.Rows(i)(objLct.tblField(objLct.fldPos.f01_LocationName).fldName))
                    .Items(i).Value = dt.Rows(i)(objLct.tblField(objLct.fldPos.f00_LocationID).fldName)
                Next
                .SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadPreceptorToDDL()
        dt = ctlPsn.Preceptor_NoUserGetByLocation(ddlLocation.SelectedValue)
        ddlPreceptor.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlPreceptor
                .Visible = True
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & RTrim(LTrim(dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f02_FirstName).fldName))) & " " & RTrim(LTrim(dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f03_LastName).fldName))))
                    .Items(i).Value = dt.Rows(i)(objPsn.tblField(objPsn.fldPos.f00_PersonID).fldName)
                Next
                .SelectedIndex = 0

                ShowNamePreceptor()
            End With
        Else


        End If
        dt = Nothing
    End Sub

    Private Sub LoadUserAccountToGrid()
        Dim dtU As New DataTable

        dtU = objUser.User_GetBySearch(ddlGroupFind.SelectedValue, txtSearch.Text)

        If dtU.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dtU
                .DataBind()

                'Try


                '    Dim nrow As Integer = dtU.Rows.Count


                '    If .PageCount > 1 Then
                '        If .PageIndex > 0 Then
                '            If (.PageSize) < nrow - (.PageSize * .PageIndex) Then
                '                For i = 0 To .PageSize - 1
                '                    .Rows(i).Cells(0).Text = (.PageSize * .PageIndex) + (i + 1)

                '                    'If dtU.Rows(i)("UserProfileID") = 1 Then
                '                    '    .Rows(i).Cells(2).Text = ctlStd.GetStudentName(String.Concat(dtU.Rows((.PageSize * .PageIndex) + (i + 1))("ProfileID")))
                '                    'Else
                '                    '    .Rows(i).Cells(2).Text = ctlPsn.GetPersonName(DBNull2Zero(dtU.Rows((.PageSize * .PageIndex) + (i + 1))("ProfileID")))
                '                    'End If


                '                Next
                '            Else
                '                For i = 0 To nrow - (.PageSize * .PageIndex) - 1
                '                    .Rows(i).Cells(0).Text = (.PageSize * .PageIndex) + (i + 1)
                '                    'If dtU.Rows(i)("UserProfileID") = 1 Then
                '                    '    .Rows(i).Cells(2).Text = ctlStd.GetStudentName(String.Concat(dtU.Rows((.PageSize * .PageIndex) + (i))("ProfileID")))
                '                    'Else
                '                    '    .Rows(i).Cells(2).Text = ctlPsn.GetPersonName(DBNull2Zero(dtU.Rows((.PageSize * .PageIndex) + (i))("ProfileID")))
                '                    'End If

                '                Next
                '            End If
                '        Else
                '            For i = 0 To .PageSize - 1
                '                .Rows(i).Cells(0).Text = i + 1

                '                'If dtU.Rows(i)("UserProfileID") = 1 Then
                '                '    .Rows(i).Cells(2).Text = ctlStd.GetStudentName(String.Concat(dtU.Rows(i)("ProfileID")))
                '                'Else
                '                '    .Rows(i).Cells(2).Text = ctlPsn.GetPersonName(DBNull2Zero(dtU.Rows(i)("ProfileID")))
                '                'End If
                '            Next
                '        End If
                '    Else
                '        For i = 0 To nrow - 1
                '            .Rows(i).Cells(0).Text = i + 1

                '            'If dtU.Rows(i)("UserProfileID") = 1 Then
                '            '    .Rows(i).Cells(2).Text = ctlStd.GetStudentName(String.Concat(dtU.Rows(i)("ProfileID")))
                '            'Else
                '            '    .Rows(i).Cells(2).Text = ctlPsn.GetPersonName(DBNull2Zero(dtU.Rows(i)("ProfileID")))
                '            'End If

                '        Next
                '    End If


                'Catch ex As Exception
                '    'DisplayMessage(Me.Page, ex.Message)
                'End Try


            End With
        Else
            grdData.DataSource = Nothing
            grdData.Visible = False
        End If
        dt = Nothing

    End Sub
    Protected Function validateData() As Boolean
        Dim result As Boolean = True
        lblvalidate1.Text = ""
        lblvalidate1.Text = ""
        lblvalidate1.Text = ""


        If lblID.Text = "0" Then


            If ddlUserGroup.SelectedIndex = 0 Then
                result = False
                lblvalidate1.Text = "กรุณาเลือกกลุ่มผู้ใช้"
                lblvalidate1.Visible = True
            Else

                Select Case ddlUserGroup.SelectedValue
                    Case 1 'Student
                        If ddlUserID.SelectedValue = 0 Or ddlUserID.SelectedValue Is Nothing Then
                            result = False
                            lblvalidate1.Text = "กรุณาเลือกนิสิต"
                            lblvalidate1.Visible = True
                        End If

                    Case 2
                        If ddlUserID.SelectedValue = 0 Or ddlUserID.SelectedValue Is Nothing Then
                            result = False
                            lblvalidate1.Text = "กรุณาเลือกอาจารย์"
                            lblvalidate1.Visible = True
                        End If
                    Case 3 'Preceptor
                        If ddlPreceptor.SelectedValue = 0 Or ddlPreceptor.SelectedValue Is Nothing Then
                            result = False
                            lblvalidate1.Text = "กรุณาเลือกอาจารย์แหล่งฝึก"
                            lblvalidate1.Visible = True
                        End If

                End Select
            End If


        End If

        If txtUsername.Text = "" Or txtPassword.Text = "" Then
            result = False
            lblvalidate2.Text = "กรุณากรอก Username และ Password ก่อน"
            lblvalidate2.Visible = True

        Else

            If lblID.Text = "0" Then
                dt = objUser.GetUsers_ByUsername(txtUsername.Text)
                If dt.Rows.Count > 0 Then
                    result = False
                    lblvalidate2.Text = "Username นี้ซ้ำ กรุณาตั้ง Username ใหม่"
                    lblvalidate2.Visible = True
                    imgAlert.Visible = True
                Else
                    imgAlert.Visible = False
                End If

            End If

        End If





        Dim n As Integer = 0
        For i = 0 To chkRoles.Items.Count - 1
            If chkRoles.Items(i).Selected Then
                n = n + 1
            End If
        Next

        If n = 0 Then
            lblvalidate3.Text = "กรุณากำหนดสิทธิ์ให้ผู้ใช้ก่อน"
            lblvalidate3.Visible = True
        End If

        Return result
    End Function


    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSave.Click

        If validateData() Then


            Dim item As Integer
            Dim sProfileID As String = ""

            Dim status As Integer = 0
            If chkStatus.Checked Then
                status = 1
            End If


            Select Case ddlUserGroup.SelectedValue
                Case 1 'Student
                    sProfileID = ddlUserID.SelectedValue

                    If sProfileID = "" Then
                        sProfileID = txtUsername.Text
                    End If
                Case 2, 4
                    If lblID.Text <> "0" Then
                        sProfileID = ctlPsn.Person_GetPersonIDByUserID(lblID.Text).ToString()
                    Else
                        sProfileID = ddlUserID.SelectedValue
                    End If

                Case 3 'Preceptor
                    sProfileID = ddlPreceptor.SelectedValue
                Case Else
                    sProfileID = ""
            End Select

            'sProfileID = lblProfileID.Text

            'Dim userid As Integer


            If lblID.Text = "0" Then 'add new 

                item = objUser.User_Add(txtUsername.Text, objEn.EncryptString(txtPassword.Text, True), txtFirstName.Text, txtLastName.Text, txtMail.Text, 0, status, ddlUserGroup.SelectedValue, sProfileID)

                'userid = objUser.GetUsersID_ByUsername(txtUsername.Text)

                If ddlUserGroup.SelectedValue = 4 Then
                    ctlPsn.Person_AddUser(txtUsername.Text, txtFirstName.Text, txtLastName.Text, txtMail.Text, "A", 1)
                    'objUser.User_UpdateProfileID(txtUsername.Text)
                End If

                objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Persons", "add new Person :" & txtFirstName.Text, "")
                objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Users", "add new user :" & txtUsername.Text, "Result:" & item)

                objUser.User_UpdateProfileID(txtUsername.Text, sProfileID)
            Else 'Update user

                item = objUser.User_UpdateDatail(txtUsername.Text, objEn.EncryptString(txtPassword.Text, True), txtFirstName.Text, txtLastName.Text, txtMail.Text, 0, status)

                If ddlUserGroup.SelectedValue <> 1 Then
                    ctlPsn.Person_UpdateUserIDandMail(StrNull2Zero(sProfileID), lblID.Text, txtMail.Text)
                End If

                objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Users", "update user :" & txtUsername.Text, "Result:" & item)

            End If

            'ctlPsn.Person_UpdateUserID(sProfileID)

            For i = 0 To chkRoles.Items.Count - 1
                If IsHasRole(chkRoles.Items(i).Value) Then 'มีสิทธิ์อยู่แล้ว
                    If chkRoles.Items(i).Selected Then
                        'เลือก
                        item = objUser.UserRole_UpdateStatus(txtUsername.Text, chkRoles.Items(i).Value, bActive, Request.Cookies("UserID").Value)

                        objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "UserRole", "เพิ่มสิทธิ์ : " & chkRoles.Items(i).Text & " ให้ :" & txtUsername.Text, "Result:" & item)
                    Else 'ไม่เลือก ทำการลบสิทธิ์โดย set สถานะไม่ใช้งาน
                        item = objUser.UserRole_UpdateStatus(txtUsername.Text, chkRoles.Items(i).Value, binActive, Request.Cookies("UserID").Value)

                        objUser.User_GenLogfile(Request.Cookies("Username").Value, "DEL", "UserRole", "ยกเลิกสิทธิ์ : " & chkRoles.Items(i).Text & " ของ :" & txtUsername.Text, "Result:" & item)

                    End If
                Else 'ไม่มีสิทธิ์อยู่แล้ว และถ้าเลือกทำการเพิ่มใหม่
                    If chkRoles.Items(i).Selected Then
                        item = objUser.UserRole_Add(txtUsername.Text, chkRoles.Items(i).Value, Request.Cookies("UserID").Value)

                        objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "UserRole", "เพิ่มสิทธิ์ : " & chkRoles.Items(i).Text & " ให้ :" & txtUsername.Text, "Result:" & item)

                    End If
                End If
            Next

            'If item = 1 Then

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)

            LoadUserAccountToGrid()
            ClearData()
            'End If

        Else

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง!');", True)
        End If
    End Sub

    Function IsHasRole(rid As Integer) As Boolean
        dt = ctlRole.UserRole_chkHasRole(txtUsername.Text, rid)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadUserAccountToGrid()
    End Sub
    Private Sub grdData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand

        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If objUser.User_Delete(e.CommandArgument) Then

                        objUser.User_GenLogfile(Request.Cookies("Username").Value, "DEL", "User", "ลบ user :" & txtUsername.Text, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ลบข้อมูลเรียบร้อย');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If

                    LoadUserAccountToGrid()

            End Select

        End If

    End Sub
    Private Sub EditData(ByVal pID As String)
        ClearData()
        Dim dtE As New DataTable

        dtE = objUser.User_GetByID(pID)

        If dtE.Rows.Count > 0 Then
            With dtE.Rows(0)
                isAdd = False

                Me.lblID.Text = .Item("UserID")

                txtUsername.Text = .Item("Username")

                Me.txtFirstName.Text = String.Concat(.Item("FirstName"))
                txtLastName.Text = String.Concat(.Item("LastName"))

                txtMail.Text = String.Concat(.Item("Email"))
                ddlUserGroup.SelectedValue = StrNull2Zero(.Item("UserProfileID"))

                'SetUserGroup()
                'lblProfileID.Text = String.Concat(.Item("ProfileID"))

                'txtFindSpec.Visible = False
                'txtFindLocation.Visible = False
                ' txtFindPreceptor.visible = False

                'txtFirstName.Enabled = False
                'txtLastName.Enabled = False

                txtFirstName.BackColor = Drawing.Color.AliceBlue
                txtLastName.BackColor = Drawing.Color.AliceBlue


                'ddlLocation.Visible = False
                'ddlUserID.Visible = False
                'ddlPreceptor.Visible = False

                'lnkFind.Visible = False
                'lnkFindLocation.Visible = False
                ' lnkFindPreceptor.visible = False
                ddlLocation.Items.Clear()
                ddlPreceptor.Items.Clear()
                ddlUserID.Items.Clear()


                'lblLocation.Visible = False
                'lblPreceptor.Visible = False
                'imgArrowLocation.Visible = False
                'imgArrowUser.Visible = False

                'Dim str As String()

                'Select Case DBNull2Zero(.Item("UserProfileID"))

                '    Case 1 'Student
                '        'LoadStudentToDDL()
                '        lblselect.Text = "นิสิต"
                '        lblselect.Visible = True
                '        'imgArrowUser.Visible = True

                '        ddlUserID.Visible = True
                '        ddlUserID.Enabled = False

                '        'lnkFind.Visible = True
                '        'txtFindSpec.Visible = True

                '        'ddlUserID.SelectedValue = DBNull2Str(.Item("ProfileID"))

                '        'str = Split(ddlUserID.SelectedItem.Text, " ")


                '    Case 2 'teacher
                '        lblselect.Text = "อาจารย์"
                '        LoadTeacherToDDL()

                '        lblselect.Visible = True
                '        'imgArrowUser.Visible = True

                '        ddlUserID.Visible = True
                '        ddlUserID.Enabled = False

                '        'lnkFind.Visible = True
                '        'txtFindSpec.Visible = True

                '        ddlUserID.SelectedValue = DBNull2Str(.Item("ProfileID"))
                '        str = Split(ddlUserID.SelectedItem.Text, " ")

                '    Case 3 'Preceptor
                '        LoadLocationToDDL()


                '        ddlPreceptor.Visible = True
                '        ddlLocation.Visible = True

                '        ddlPreceptor.Enabled = False

                '        'lnkFindLocation.Visible = True
                '        'txtFindLocation.Visible = True
                '        lblLocation.Visible = True
                '        'imgArrowLocation.Visible = True

                '        ddlLocation.SelectedValue = ctlPsn.Location_GetByPersonID(DBNull2Zero(.Item("ProfileID")))

                '        LoadPreceptorToDDL()
                '        ddlPreceptor.SelectedValue = DBNull2Str(.Item("ProfileID"))
                '        str = Split(ddlPreceptor.SelectedItem.Text, " ")
                '    Case Else

                '        LoadPersonToDDL("A")
                '        lblselect.Text = "จนท./อื่นๆ"

                '        'imgArrowUser.Visible = True

                '        lblselect.Visible = True

                '        'txtFirstName.Enabled = True
                '        'txtLastName.Enabled = True
                '        'txtFirstName.BackColor = Drawing.Color.White
                '        'txtLastName.BackColor = Drawing.Color.White

                '        'txtFindSpec.Visible = True
                '        txtFindLocation.Visible = False
                '        '  txtFindPreceptor.visible = False

                '        ddlLocation.Visible = False
                '        ddlUserID.Visible = True
                '        ddlUserID.Enabled = False

                '        ddlPreceptor.Visible = False

                '        'lnkFind.Visible = True
                '        lnkFindLocation.Visible = False
                '        ' lnkFindPreceptor.visible = False

                '        ddlUserID.SelectedValue = DBNull2Str(.Item("ProfileID"))
                '        str = Split(ddlUserID.SelectedItem.Text, " ")


                'End Select

                ddlUserGroup.Enabled = False

                'txtFirstName.Text = str(0)
                'txtLastName.Text = str(1)


                If .Item("isPublic") = 0 Then
                    chkStatus.Checked = False
                Else
                    chkStatus.Checked = True
                End If

                txtPassword.Text = objEn.DecryptString(.Item("Password"), True)

                txtUsername.Text = .Item("username")

            End With

            txtUsername.ReadOnly = True

        End If

        dt = Nothing

        Dim ctlur As New UserRoleController
        Dim dtR As New DataTable

        dtR = ctlur.UserRole_GetByUserID(pID)
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

    End Sub

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdClear.Click
        ClearData()
        ddlUserGroup.SelectedIndex = 0

    End Sub

    Private Sub ClearData()
        Me.txtUsername.Text = ""
        txtPassword.Text = ""
        Me.txtFirstName.Text = ""
        txtLastName.Text = ""
        lblID.Text = "0"
        'lblProfileID.Text = ""
        chkStatus.Checked = True
        txtUsername.ReadOnly = False
        txtMail.Text = ""
        isAdd = True
        imgAlert.Visible = False

        lblvalidate1.Visible = False
        lblvalidate2.Visible = False
        lblvalidate3.Visible = False



        'txtFindSpec.Visible = False
        'txtFindLocation.Visible = False
        ' txtFindPreceptor.visible = False

        ddlUserGroup.Enabled = True
        'ddlLocation.Visible = False
        'ddlUserID.Visible = False
        'ddlPreceptor.Visible = False

        'lnkFind.Visible = False
        'lnkFindLocation.Visible = False
        ''  lnkFindPreceptor.visible = False
        'lblselect.Visible = False
        'lblPreceptor.Visible = False
        'lblLocation.Visible = False
        'imgArrowLocation.Visible = False
        'imgArrowUser.Visible = False

        pnStudent.Visible = False
        pnLocation.Visible = False
        pnPreceptor.Visible = False

        txtFirstName.Enabled = True
        txtLastName.Enabled = True
        txtUsername.Enabled = True

        txtFirstName.BackColor = Drawing.Color.White
        txtLastName.BackColor = Drawing.Color.White
        txtUsername.BackColor = Drawing.Color.White
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(9).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub

    Protected Sub ddlUserGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUserGroup.SelectedIndexChanged
        ClearData()
        SetUserGroup()
    End Sub
    Private Sub SetUserGroup()

        'txtFindSpec.Visible = False
        'txtFindLocation.Visible = False
        ' txtFindPreceptor.visible = False

        'txtFirstName.Enabled = False
        'txtLastName.Enabled = False
        txtFirstName.BackColor = Drawing.Color.AliceBlue
        txtLastName.BackColor = Drawing.Color.AliceBlue


        'ddlLocation.Visible = False
        'ddlUserID.Visible = False
        'ddlPreceptor.Visible = False

        'lnkFind.Visible = False
        'lnkFindLocation.Visible = False
        ' lnkFindPreceptor.visible = False
        ddlLocation.Items.Clear()
        ddlPreceptor.Items.Clear()
        ddlUserID.Items.Clear()

        'lblselect.Visible = False
        'lblLocation.Visible = False
        'lblPreceptor.Visible = False
        'imgArrowLocation.Visible = False
        'imgArrowUser.Visible = False

        pnStudent.Visible = False
        pnLocation.Visible = False
        pnPreceptor.Visible = False

        Select Case ddlUserGroup.SelectedValue
            Case 1 'Student
                pnStudent.Visible = True
                LoadStudentToDDL()
                lblselect.Text = "นิสิต"
                'lblselect.Visible = True
                'ddlUserID.Visible = True
                ddlUserID.Enabled = True
                lnkFind.Visible = True
                'txtFindSpec.Visible = True
                'imgArrowUser.Visible = True

                ShowNameStudent()
            Case 2 'teacher
                pnStudent.Visible = True
                lblselect.Text = "อาจารย์"
                LoadTeacherToDDL()
                'lblselect.Visible = True
                'ddlUserID.Visible = True
                'lnkFind.Visible = True
                'txtFindSpec.Visible = True
                'imgArrowUser.Visible = True
                ddlUserID.Enabled = True

            Case 3 'Preceptor
                pnLocation.Visible = True
                pnPreceptor.Visible = True
                LoadLocationToDDL()
                LoadPreceptorToDDL()

                'ddlLocation.Visible = True
                'lnkFindLocation.Visible = True
                'txtFindLocation.Visible = True
                'lblLocation.Visible = True
                'ddlPreceptor.Visible = True
                'lblPreceptor.Visible = True
                'imgArrowLocation.Visible = True

            Case Else 'เจ้าหน้าที่อื่นๆ
                pnStudent.Visible = True
                LoadPersonToDDL("A")
                lblselect.Text = "จนท./อื่นๆ"
                txtFirstName.Enabled = True
                txtLastName.Enabled = True
                txtFirstName.BackColor = Drawing.Color.White
                txtLastName.BackColor = Drawing.Color.White
                'txtFindSpec.Visible = True
                'ddlUserID.Visible = True
                'lnkFind.Visible = True
                'imgArrowUser.Visible = True
                'lblselect.Visible = True
        End Select

    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        LoadPreceptorToDDL()
    End Sub

    Protected Sub lnkFind_Click(sender As Object, e As EventArgs) Handles lnkFind.Click

        If ddlUserGroup.SelectedValue = 1 Then
            LoadStudentToDDL()
        ElseIf ddlUserGroup.SelectedValue = 2 Then
            LoadTeacherToDDL()
        End If

    End Sub

    Protected Sub lnkFindLocation_Click(sender As Object, e As EventArgs) Handles lnkFindLocation.Click
        LoadLocationToDDL()
        LoadPreceptorToDDL()
    End Sub

    Protected Sub ddlUserID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUserID.SelectedIndexChanged
        ShowNameStudent()
        lblID.Text = "0"
    End Sub
    Private Sub ShowNameStudent()

        If ddlUserGroup.SelectedValue = 1 Then
            txtUsername.Text = ddlUserID.SelectedValue
            txtUsername.ReadOnly = True
            txtUsername.BackColor = Drawing.Color.AliceBlue
        Else
            txtUsername.ReadOnly = False
            txtUsername.BackColor = Drawing.Color.White
        End If

        'Dim str As String()
        'str = Split(ddlUserID.SelectedItem.Text, " ")
        'txtFirstName.Text = str(0)
        'txtLastName.Text = str(1)


    End Sub

    Protected Sub ddlPreceptor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPreceptor.SelectedIndexChanged
        ShowNamePreceptor()
        lblID.Text = "0"
    End Sub

    Private Sub ShowNamePreceptor()
        Dim str As String()
        str = Split(ddlPreceptor.SelectedItem.Text, " ")
        txtFirstName.Text = str(0)
        txtLastName.Text = str(1)
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadUserAccountToGrid()
    End Sub

    Protected Sub ddlGroupFind_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGroupFind.SelectedIndexChanged
        grdData.PageIndex = 0
        LoadUserAccountToGrid()
    End Sub
End Class

