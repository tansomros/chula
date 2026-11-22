
Public Class UserAdvisor
    Inherits System.Web.UI.Page

    Dim dt As New DataTable

    Dim objUser As New UserController
    Dim ctlRole As New RoleController
    Dim ctlPsn As New PersonController
    Dim objEn As New CryptographyEngine

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
            LoadPersonToGrid()


            ClearData()

        End If
    End Sub


    Private Sub LoadPersonToGrid()

        dt = ctlPsn.Person_GetNoUserByType("T", txtFindPerson.Text)

        grdPerson.DataSource = dt
        grdPerson.DataBind()
        dt = Nothing
    End Sub


    Private Sub LoadUserAccountToGrid()
        Dim dtU As New DataTable

        dtU = objUser.User_GetBySearch(2, txtSearch.Text)

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

                                    'If dtU.Rows(i)("UserProfileID") = 1 Then
                                    '    .Rows(i).Cells(2).Text = ctlStd.GetStudentName(String.Concat(dtU.Rows((.PageSize * .PageIndex) + (i + 1))("ProfileID")))
                                    'Else
                                    '    .Rows(i).Cells(2).Text = ctlPsn.GetPersonName(DBNull2Zero(dtU.Rows((.PageSize * .PageIndex) + (i + 1))("ProfileID")))
                                    'End If


                                Next
                            Else
                                For i = 0 To nrow - (.PageSize * .PageIndex) - 1
                                    .Rows(i).Cells(0).Text = (.PageSize * .PageIndex) + (i + 1)
                                    'If dtU.Rows(i)("UserProfileID") = 1 Then
                                    '    .Rows(i).Cells(2).Text = ctlStd.GetStudentName(String.Concat(dtU.Rows((.PageSize * .PageIndex) + (i))("ProfileID")))
                                    'Else
                                    '    .Rows(i).Cells(2).Text = ctlPsn.GetPersonName(DBNull2Zero(dtU.Rows((.PageSize * .PageIndex) + (i))("ProfileID")))
                                    'End If

                                Next
                            End If
                        Else
                            For i = 0 To .PageSize - 1
                                .Rows(i).Cells(0).Text = i + 1

                                'If dtU.Rows(i)("UserProfileID") = 1 Then
                                '    .Rows(i).Cells(2).Text = ctlStd.GetStudentName(String.Concat(dtU.Rows(i)("ProfileID")))
                                'Else
                                '    .Rows(i).Cells(2).Text = ctlPsn.GetPersonName(DBNull2Zero(dtU.Rows(i)("ProfileID")))
                                'End If
                            Next
                        End If
                    Else
                        For i = 0 To nrow - 1
                            .Rows(i).Cells(0).Text = i + 1

                            'If dtU.Rows(i)("UserProfileID") = 1 Then
                            '    .Rows(i).Cells(2).Text = ctlStd.GetStudentName(String.Concat(dtU.Rows(i)("ProfileID")))
                            'Else
                            '    .Rows(i).Cells(2).Text = ctlPsn.GetPersonName(DBNull2Zero(dtU.Rows(i)("ProfileID")))
                            'End If

                        Next
                    End If


                Catch ex As Exception
                    'DisplayMessage(Me.Page, ex.Message)
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

                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
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

                Dim sName() As String = Split(String.Concat(.Item("PersonName")), "|")
                txtFirstName.Text = sName(0)
                txtLastName.Text = sName(1)

                'Me.txtFirstName.Text = String.Concat(.Item("FirstName"))
                'txtLastName.Text = String.Concat(.Item("LastName"))

                txtMail.Text = String.Concat(.Item("Email"))


                'txtFirstName.Enabled = False
                'txtLastName.Enabled = False

                txtFirstName.BackColor = Drawing.Color.AliceBlue
                txtLastName.BackColor = Drawing.Color.AliceBlue


                


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
        txtMail.Text = ""
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

    Protected Sub lnkFindLocation_Click(sender As Object, e As EventArgs) Handles lnkFindPerson.Click
        LoadPersonToGrid()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadUserAccountToGrid()
    End Sub


    Protected Sub grdPerson_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdPerson.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Private Sub grdPerson_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdPerson.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgSelect"
                    ClearData()
                    txtFirstName.Text = grdPerson.Rows(e.CommandArgument()).Cells(1).Text
                    txtLastName.Text = grdPerson.Rows(e.CommandArgument()).Cells(2).Text
                    lblPersonID.Text = grdPerson.DataKeys(e.CommandArgument()).Value

            End Select

        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If validateData() Then


            Dim item As Integer

            Dim status As Integer = 0
            Dim statusFlag As String = "D"
            If chkStatus.Checked Then
                status = 1
                statusFlag = "A"
            End If


            Dim ctlPsn As New PersonController

            If lblID.Text = 0 Then

                item = objUser.User_Add(txtUsername.Text, objEn.EncryptString(txtPassword.Text, True), txtFirstName.Text & " " & txtLastName.Text, 0, statusFlag, 2, lblPersonID.Text)

                'userid = ctlA.GetUsersID_ByUsername(txtUsername.Text)

                If 2 = 4 Then
                    ctlPsn.Person_AddUser(txtUsername.Text, txtFirstName.Text, txtLastName.Text, txtMail.Text, "A", 1)
                    'ctlA.User_UpdateProfileID(txtUsername.Text)
                End If

                objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Persons", "add new Person :" & txtFirstName.Text, "")
                objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Users", "add new user :" & txtUsername.Text, "Result:" & item)

            Else
                item = objUser.User_Update(txtUsername.Text, objEn.EncryptString(txtPassword.Text, True), txtFirstName.Text, txtLastName.Text, txtMail.Text, 0, statusFlag, 2, lblPersonID.Text)

                If 2 <> 1 Then
                    ctlPsn.Person_UpdateUserIDandMail(StrNull2Zero(lblPersonID.Text), StrNull2Zero(lblID.Text), txtMail.Text)
                End If


                objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Users", "update user :" & txtUsername.Text, "Result:" & item)

            End If

            objUser.User_UpdateProfileID(txtUsername.Text, lblPersonID.Text)
            'ctlPsn.Person_UpdateUserID(sProfileID)



            If IsHasRole(2) Then 'มีสิทธิ์อยู่แล้ว
                item = objUser.UserRole_UpdateStatus(txtUsername.Text, 2, bActive, Request.Cookies("UserID").Value)
                objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "UserRole", "เพิ่มสิทธิ์อาจารย์ประจำคณะให้ :" & txtUsername.Text, "Result:" & item)

            Else 'ไม่มีสิทธิ์อยู่แล้ว และถ้าเลือกทำการเพิ่มใหม่
                item = objUser.UserRole_Add(txtUsername.Text, 2, Request.Cookies("UserID").Value)
                objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "UserRole", "เพิ่มสิทธิ์อาจารย์ประจำคณะให้ :" & txtUsername.Text, "Result:" & item)
            End If

            'If item = 1 Then
            DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")
            LoadUserAccountToGrid()
            ClearData()
            'End If

        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง!');", True)
        End If
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub
End Class

