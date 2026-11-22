Imports Subgurim.Controles


Public Class Teacher_Reg
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlDept As New DepartmentController
    Dim ctlFct As New FacultyController
    Dim ctlbase As New ApplicationBaseClass

    Dim ctlTch As New PersonController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            LoadPrefixToDDL()
            LoadDepartmentToDDL()
            LoadPositionToDDL()
            LoadProvinceToDDL()
            LoadTeacherData()

        End If

        If (FileUploaderAJAX1.IsPosting) Then
            UploadFile()
        End If

    End Sub

    Private Sub LoadTeacherData()
        dt = ctlTch.Person_GetByID(Request.Cookies("ProfileID").Value)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)

                lblStdCode.Text = .Item("PersonID")

                If Not IsDBNull(.Item("Prefix")) Then
                    ddlPrefix.SelectedValue = .Item("Prefix")
                End If

                txtFirstName.Text = DBNull2Str(.Item("FirstName"))
                txtLastName.Text = DBNull2Str(.Item("LastName"))
                txtNickName.Text = DBNull2Str(.Item("NickName"))

                If Not IsDBNull(.Item("Gender")) Then
                    optGender.SelectedValue = .Item("Gender")
                End If

                txtEmail.Text = DBNull2Str(.Item("Email"))
                txtTelephone.Text = DBNull2Str(.Item("Telephone"))
                txtMobile.Text = DBNull2Str(.Item("MobilePhone"))

                ddlPosition.SelectedValue = .Item("PositionID")
                ddlDepartment.SelectedValue = .Item("DepartmentUID")

                txtAddress.Text = DBNull2Str(.Item("Address"))
                txtDistrict.Text = DBNull2Str(.Item("District"))
                txtCity.Text = DBNull2Str(.Item("City"))

                If Not IsDBNull(.Item("ProvinceID")) Then
                    ddlProvince.SelectedValue = .Item("ProvinceID")
                End If

                txtZipCode.Text = DBNull2Str(.Item("ZipCode"))

                If DBNull2Str(.Item("PicturePath")) <> "" Then
                    picStudent.ImageUrl = "~/" & stdPic & "/" & .Item("PicturePath")
                End If


            End With

        End If


    End Sub
    
    Private Sub LoadProvinceToDDL()
        dt = ctlbase.Province_GetActive
        If dt.Rows.Count > 0 Then
            With ddlProvince
                .Enabled = True
                .DataSource = dt
                .DataTextField = "ProvinceName"
                .DataValueField = "ProvinceID"
                .DataBind()
                '.SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadPrefixToDDL()
        dt = ctlbase.Prefix_GetAll
        If dt.Rows.Count > 0 Then
            With ddlPrefix
                .Enabled = True
                .DataSource = dt
                .DataTextField = "PrefixName"
                .DataValueField = "PrefixID"
                .DataBind()
                '.SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub LoadPositionToDDL()
        dt = ctlbase.Position_GetActive
        If dt.Rows.Count > 0 Then
            With ddlPosition
                .Enabled = True
                .DataSource = dt
                .DataTextField = "PositionName"
                .DataValueField = "PositionID"
                .DataBind()
                '.SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadDepartmentToDDL()
        dt = ctlDept.Department_GetActive
        If dt.Rows.Count > 0 Then
            With ddlDepartment
                .Enabled = True
                .DataSource = dt
                .DataTextField = "DepartmentName"
                .DataValueField = "DepartmentUID"
                .DataBind()
                '.SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        lblStdCode.Text = ""
        ddlPrefix.SelectedIndex = 0
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtNickName.Text = ""
        optGender.SelectedIndex = 0
        txtNickName.Text = ""
        txtEmail.Text = ""
        txtTelephone.Text = ""
        txtMobile.Text = ""

        txtAddress.Text = ""
        txtDistrict.Text = ""
        txtCity.Text = ""
        ddlProvince.SelectedIndex = 0
        txtZipCode.Text = ""


    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click


        ctlTch.Person_Update(lblStdCode.Text, ddlPrefix.SelectedValue, txtFirstName.Text, txtLastName.Text, txtNickName.Text, optGender.SelectedValue, StrNull2Zero(ddlPosition.SelectedValue), ddlPosition.SelectedItem.Text, StrNull2Zero(ddlDepartment.SelectedValue), ddlDepartment.SelectedItem.Text, txtEmail.Text, txtTelephone.Text, txtMobile.Text, txtAddress.Text, txtDistrict.Text, txtCity.Text, StrNull2Zero(ddlProvince.SelectedValue), ddlProvince.SelectedItem.Text, txtZipCode.Text, Request.Cookies("Username").Value & ".jpg", Request.Cookies("Username").Value)


        Dim objuser As New UserController
        If txtEmail.Text <> "" Then
            objuser.User_UpdateMail(Request.Cookies("Username").Value, txtEmail.Text)
        End If

        objuser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Teacher", "บันทึก/แก้ไข ประวัติอาจารย์ :" & lblStdCode.Text, "")

        DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")
    End Sub

    Private Sub UploadFile()

        Dim pf As HttpPostedFileAJAX = FileUploaderAJAX1.PostedFile


        'กรณีต้องการต้องสอบชนิดและขนาดไฟล์
        If ((pf.ContentType.Equals("image/jpeg") Or pf.ContentType.Equals("image/jpg")) And pf.ContentLength <= 500 * 1024) Then

            FileUploaderAJAX1.SaveAs("~/" & stdPic, Request.Cookies("Username").Value & ".jpg")


        Else
            DisplayMessage(Me.Page, "ไม่สามารถอัปโหลดรูปท่านได้ เนื่องจากขนาดรูปท่านใหญ่เกิน 500K กรุณาลองใหม่ภายหลัง")
        End If

    End Sub

End Class