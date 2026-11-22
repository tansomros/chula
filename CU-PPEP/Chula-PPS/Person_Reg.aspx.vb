Imports System.IO
Imports Subgurim.Controles


Public Class Person_Reg
    Inherits System.Web.UI.Page
    Dim dt As New DataTable

    Dim ctlFct As New FacultyController
    Dim ctlbase As New ApplicationBaseClass

    Dim ctlTch As New PersonController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            LoadPositionToDDL()
            LoadProvinceToDDL()
            LoadPrefixToDDL()
            LoadPersonData()

        End If

        If (FileUploaderAJAX1.IsPosting) Then
            UploadFile()
        End If

    End Sub

    Private Sub LoadPersonData()
        dt = ctlTch.Person_GetByID(Request.Cookies("ProfileID").Value)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)

                hdPersonID.Value = .Item("PersonID")

                If Not IsDBNull(.Item("PrefixID")) Then
                    ddlPrefix.SelectedValue = .Item("PrefixID")
                End If

                txtFirstName.Text = DBNull2Str(.Item("FirstName"))
                txtLastName.Text = DBNull2Str(.Item("LastName"))
                ddlPosition.SelectedValue = DBNull2Str(.Item("PositionID"))
                txtOrganizationName.Text = DBNull2Str(.Item("OrganizationName"))

                'If Not IsDBNull(.Item("Gender")) Then
                '    optGender.SelectedValue = .Item("Gender")
                'End If
                txtEmail.Text = DBNull2Str(.Item("Email"))
                txtTelephone.Text = DBNull2Str(.Item("Telephone"))
                txtMobile.Text = DBNull2Str(.Item("MobilePhone"))
                txtNickName.Text = DBNull2Str(.Item("NickName"))

                txtAddress.Text = DBNull2Str(.Item("Address"))
                txtDistrict.Text = DBNull2Str(.Item("District"))
                txtCity.Text = DBNull2Str(.Item("City"))

                If Not IsDBNull(.Item("ProvinceID")) Then
                    ddlProvince.SelectedValue = .Item("ProvinceID")
                End If

                txtZipCode.Text = DBNull2Str(.Item("ZipCode"))

                If DBNull2Str(.Item("PicturePath")) <> "" Then
                    picStudent.ImageUrl = "~/" & personPic & "/" & .Item("PicturePath")
                End If


            End With

        End If


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

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        hdPersonID.Value = ""
        hdUserID.Value = ""
        ddlPrefix.SelectedIndex = 0
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtNickName.Text = ""
        txtNickName.Text = ""
        txtEmail.Text = ""
        txtTelephone.Text = ""
        txtMobile.Text = ""
        ddlPosition.SelectedIndex = 0

        txtAddress.Text = ""
        txtDistrict.Text = ""
        txtCity.Text = ""
        ddlProvince.SelectedIndex = 0
        txtZipCode.Text = ""


    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        ctlTch.Person_UpdateByUser(StrNull2Zero(hdPersonID.Value), ddlPrefix.SelectedValue, txtFirstName.Text, txtLastName.Text, txtNickName.Text, StrNull2Zero(ddlPosition.SelectedValue), ddlPosition.SelectedItem.Text, 0, txtOrganizationName.Text, txtEmail.Text, txtTelephone.Text, txtMobile.Text, txtAddress.Text, txtDistrict.Text, txtCity.Text, StrNull2Zero(ddlProvince.SelectedValue), ddlProvince.SelectedItem.Text, txtZipCode.Text, Request.Cookies("Username").Value & ".jpg", Request.Cookies("Username").Value)


        Dim objuser As New UserController
        If txtEmail.Text <> "" Then
            objuser.User_UpdateMail(Request.Cookies("Username").Value, txtEmail.Text)
        End If

        objuser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Person", "บันทึก/แก้ไข ประวัติอาจารย์ :" & hdPersonID.Value, "")

        'Response.Redirect("ResultPage.aspx?t=p")

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย/Completed successfully.');", True)

    End Sub

    Private Sub UploadFile()

        Dim pf As HttpPostedFileAJAX = FileUploaderAJAX1.PostedFile


        'กรณีต้องการต้องสอบชนิดและขนาดไฟล์
        If ((pf.ContentType.Equals("image/jpeg") Or pf.ContentType.Equals("image/jpg")) And pf.ContentLength <= 500 * 1024) Then


            Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & personPic & "/" & Request.Cookies("Username").Value & ".jpg"))

            If objfile.Exists Then
                objfile.Delete()
            End If

            FileUploaderAJAX1.SaveAs("~/" & personPic, Request.Cookies("Username").Value & ".jpg")


        Else
            DisplayMessage(Me.Page, "ไม่สามารถอัปโหลดรูปท่านได้ เนื่องจากขนาดรูปท่านใหญ่เกิน 500K กรุณาลองใหม่ภายหลัง")
        End If

    End Sub

End Class