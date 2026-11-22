Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Subgurim.Controles


Public Class Teacher_Reg
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlDept As New DepartmentController
    Dim ctlFct As New FacultyController
    Dim ctlM As New MasterController

    Dim ctlTch As New PersonController
    Dim objTch As New PersonInfo


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

                lblStdCode.Text = .Item("00_PersonID")

                If Not IsDBNull(.Item("01_Prefix")) Then
                    ddlPrefix.SelectedValue = .Item("01_Prefix")
                End If

                txtFirstName.Text = DBNull2Str(.Item("02_FirstName"))
                txtLastName.Text = DBNull2Str(.Item("03_LastName"))
                txtNickName.Text = DBNull2Str(.Item("10_NickName"))

                If Not IsDBNull(.Item("06_Gender")) Then
                    optGender.SelectedValue = .Item("06_Gender")
                End If

                txtEmail.Text = DBNull2Str(.Item("07_Email"))
                txtTelephone.Text = DBNull2Str(.Item("08_Telephone"))
                txtMobile.Text = DBNull2Str(.Item("09_MobilePhone"))

                ddlPosition.SelectedValue = .Item("PositionID")
                ddlDepartment.SelectedValue = .Item("DepartmentUID")

                txtAddress.Text = DBNull2Str(.Item("11_Address"))
                txtDistrict.Text = DBNull2Str(.Item("12_District"))
                txtCity.Text = DBNull2Str(.Item("13_City"))

                If Not IsDBNull(.Item("14_ProvinceID")) Then
                    ddlProvince.SelectedValue = .Item("14_ProvinceID")
                End If

                txtZipCode.Text = DBNull2Str(.Item("16_ZipCode"))

                If DBNull2Str(.Item("17_PicturePath")) <> "" Then
                    picStudent.ImageUrl = "~/" & PersonPic & "/" & .Item("17_PicturePath")
                End If


            End With

        End If


    End Sub
    
    Private Sub LoadProvinceToDDL()
        dt = ctlM.Province_GetActive
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
        dt = ctlm.Prefix_GetAll
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
        dt = ctlM.Position_GetActive
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

            FileUploaderAJAX1.SaveAs("~/" & PersonPic, Request.Cookies("Username").Value & ".jpg")


        Else
            DisplayMessage(Me.Page, "ไม่สามารถอัปโหลดรูปท่านได้ เนื่องจากขนาดรูปท่านใหญ่เกิน 500K กรุณาลองใหม่ภายหลัง")
        End If

    End Sub

End Class