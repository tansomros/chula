
Public Class UserStudent
    Inherits System.Web.UI.Page

    Dim dt As New DataTable

    Dim objUser As New UserController
    Dim ctlRole As New RoleController
    Dim ctlStd As New StudentController
    Dim ctlPsn As New PersonController
    'Dim ctlPct As New PreceptorController
    Dim objLct As New LocationInfo
    Dim ctlLct As New LocationController
    Dim objEn As New CryptographyEngine

    'Dim objRole As New RoleInfo
    'Dim objStd As New StudentInfo
    'Dim ctlPsn As New PreceptorInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            grdData.PageIndex = 0

            txtUsername.ReadOnly = False
            lblID.Text = 0
            isAdd = True


            LoadUserAccountToGrid()

            lblNoUserCount.Text = objUser.User_GetCountNoUser(1).ToString()
            If lblNoUserCount.Text = "0" Then
                cmdGenUser.Visible = False
            Else
                cmdGenUser.Visible = True
            End If

            ClearData()

        End If
    End Sub

    Private Sub LoadUserAccountToGrid()
        Dim dtU As New DataTable

        dtU = objUser.User_GetBySearch(1, txtSearch.Text)

        If dtU.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dtU
                .DataBind()

                Try


                    Dim nrow As Integer = dtU.Rows.Count


                    If .PageCount > 1 Then
                        If .PageIndex > 0 Then
                            If (.PageSize) < nrow - (.PageSize * .PageIndex) Then
                                For i = 0 To .PageSize - 1
                                    .Rows(i).Cells(0).Text = (.PageSize * .PageIndex) + (i + 1)
                                Next
                            Else
                                For i = 0 To nrow - (.PageSize * .PageIndex) - 1
                                    .Rows(i).Cells(0).Text = (.PageSize * .PageIndex) + (i + 1)
                                Next
                            End If
                        Else
                            For i = 0 To .PageSize - 1
                                .Rows(i).Cells(0).Text = i + 1
                            Next
                        End If
                    Else
                        For i = 0 To nrow - 1
                            .Rows(i).Cells(0).Text = i + 1
                        Next
                    End If


                Catch ex As Exception
                    '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ',e.message);", True)
                End Try


            End With
        Else
            grdData.DataSource = Nothing
            grdData.Visible = False
        End If
        dt = Nothing

    End Sub
    Protected Function validateData() As Boolean
        Dim result As Boolean = True

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

        Return result
    End Function


    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSave.Click

        If validateData() Then


            Dim item As Integer
            Dim sProfileID As String = txtUsername.Text

            Dim status As Integer = 0
            Dim statusFlag As String = "D"
            If chkStatus.Checked Then
                status = 1
                statusFlag = "A"
            End If

            Dim ctlPsn As New PersonController

            If lblID.Text = 0 Then

                item = objUser.User_Add(txtUsername.Text, objEn.EncryptString(txtPassword.Text, True), txtFirstName.Text & " " & txtLastName.Text, 0, statusFlag, 1, sProfileID)

                'userid = ctlA.GetUsersID_ByUsername(txtUsername.Text)



                objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Persons", "add new Person :" & txtFirstName.Text, "")
                objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Users", "add new user :" & txtUsername.Text, "Result:" & item)

            Else
                item = objUser.User_Update(txtUsername.Text, objEn.EncryptString(txtPassword.Text, True), txtFirstName.Text, txtLastName.Text, "", 0, statusFlag, 1, sProfileID)


                objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Users", "update user :" & txtUsername.Text, "Result:" & item)

            End If

            objUser.User_UpdateProfileID(txtUsername.Text, sProfileID)
            'ctlPsn.Person_UpdateUserID(sProfileID)

            item = objUser.UserRole_Save(txtUsername.Text, 1, Request.Cookies("UserID").Value)

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

        dtE = objUser.User_GetByUserID(pID)

        If dtE.Rows.Count > 0 Then
            With dtE.Rows(0)
                isAdd = False

                Me.lblID.Text = .Item("UserID")

                txtUsername.Text = .Item("Username")


                txtFirstName.Enabled = False
                txtLastName.Enabled = False

                txtFirstName.BackColor = Drawing.Color.AliceBlue
                txtLastName.BackColor = Drawing.Color.AliceBlue

                Dim sName() As String = Split(String.Concat(.Item("PersonName")), "|")
                txtFirstName.Text = sName(0)
                txtLastName.Text = sName(1)

                'txtFirstName.Text = dtE.Rows(0)("FirstName")
                'txtLastName.Text = dtE.Rows(0)("LastName")


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

    End Sub

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Private Sub ClearData()
        Me.txtUsername.Text = ""
        txtPassword.Text = ""
        Me.txtFirstName.Text = ""
        txtLastName.Text = ""
        lblID.Text = 0
        'lblProfileID.Text = ""
        chkStatus.Checked = True
        txtUsername.ReadOnly = False

        isAdd = True
        imgAlert.Visible = False


        lblvalidate2.Visible = False


        txtFirstName.Enabled = True
        txtLastName.Enabled = True
        txtUsername.Enabled = True

        txtFirstName.BackColor = Drawing.Color.White
        txtLastName.BackColor = Drawing.Color.White
        txtUsername.BackColor = Drawing.Color.White





    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(8).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub
    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadUserAccountToGrid()
    End Sub

    Protected Sub cmdGenUser_Click(sender As Object, e As EventArgs) Handles cmdGenUser.Click
        Dim dtS As New DataTable

        dtS = ctlStd.Student_GetNoUser
        If dtS.Rows.Count > 0 Then
            For i = 0 To dtS.Rows.Count - 1
                objUser.User_Save(dtS.Rows(i)("Student_Code") _
                                , objEn.EncryptString("password", True) _
                                , dtS.Rows(i)("FirstName") _
                                , dtS.Rows(i)("LastName") _
                                , "" _
                                , 0 _
                                , 1 _
                                , 1 _
                                , dtS.Rows(i)("Student_Code"))

                objUser.UserRole_Add(dtS.Rows(i)("Student_Code"), 1, Request.Cookies("UserID").Value)
            Next
        End If

        lblNoUserCount.Text = objUser.User_GetCountNoUser(1).ToString()
        If lblNoUserCount.Text = "0" Then
            cmdGenUser.Visible = False
        Else
            cmdGenUser.Visible = True
        End If
    End Sub
End Class

