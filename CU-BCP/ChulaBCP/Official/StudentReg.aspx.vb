Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Subgurim.Controles

Public Class StudentReg
    Inherits System.Web.UI.Page
    Dim dt As New DataTable

    Dim ctlM As New MasterController
    Dim ctlStd As New StudentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            cmdPrint.Visible = False
            LoadPrefix()
            LoadBank()
            BindDayToDDL()
            BindMonthToDDL()
            BindYearToDDL()
            LoadPayorToDDL()
            LoadProvinceToDDL()
            If Request("std") = Nothing Then
                If Not Request("c") = Nothing Then
                    LoadStudentData(Request.Cookies("PersonID").Value)
                End If
            Else
                LoadStudentData(Request("std"))
            End If

        End If

        If (FileUploaderAJAX1.IsPosting) Then
            UploadFile()
        End If

        'If (FileUploaderAJAX2.IsPosting) Then
        '    UploadSignature()
        'End If


        'picStudent.ImageUrl = "~/" & StudentPic & "/" & lblstdcode.text  & ".jpg"

        cmdPrint.Attributes.Add("onClick", "window.open('" + ResolveUrl("ReportViewerStudentBio.aspx?id=" + lblStdCode.Text) + "', 'windowname', 'width=800,height=600,scrollbars=yes')")

        txtGPAX.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
    End Sub

    Private Sub LoadStudentData(PersonID As String)
        dt = ctlStd.GetStudent_ByID(PersonID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)

                hdStudentID.Value = String.Concat(.Item("StudentID"))
                lblStdCode.Text = .Item("StudentCode")

                If Not IsDBNull(.Item("PrefixID")) Then
                    ddlPrefix.SelectedValue = .Item("PrefixID")
                End If

                txtFirstNameTH.Text = DBNull2Str(.Item("FirstName"))
                txtLastNameTH.Text = DBNull2Str(.Item("LastName"))
                txtNickName.Text = DBNull2Str(.Item("NickName"))

                If Not IsDBNull(.Item("Gender")) Then
                    optGender.SelectedValue = .Item("Gender")
                End If

                txtEmail.Text = DBNull2Str(.Item("Email"))
                txtTelephone.Text = DBNull2Str(.Item("Telephone"))
                txtMobile.Text = DBNull2Str(.Item("MobilePhone"))

                If DBNull2Str(.Item("BirthDate")) <> "" Then
                    ddlDay.SelectedValue = Right(.Item("BirthDate"), 2)
                    ddlMonth.SelectedValue = Mid(.Item("BirthDate"), 5, 2)
                    ddlYear.SelectedValue = Left(.Item("BirthDate"), 4)
                End If


                If Not IsDBNull(.Item("BloodGroup")) Then
                    ddlBloodGroup.SelectedValue = .Item("BloodGroup")
                End If

                lblMajorName.Text = String.Concat(.Item("MajorName"))
                'lblMinorName.Text = String.Concat(.Item("MinorName"))
                'lblSubMinorName.Text = String.Concat(.Item("SubMinorName"))


                lblAdvisorName.Text = String.Concat(.Item("Ad_PrefixName")) & String.Concat(.Item("Ad_Fname")) & " " & String.Concat(.Item("Ad_Lname"))


                txtFather_FirstName.Text = DBNull2Str(.Item("Father_FirstName"))
                txtFather_LastName.Text = DBNull2Str(.Item("Father_LastName"))
                txtFather_Career.Text = DBNull2Str(.Item("Father_Career"))
                txtFather_Tel.Text = DBNull2Str(.Item("Father_Tel"))
                txtMother_FirstName.Text = DBNull2Str(.Item("Mother_FirstName"))
                txtMother_LastName.Text = DBNull2Str(.Item("Mother_LastName"))
                txtMother_Career.Text = DBNull2Str(.Item("Mother_Career"))
                txtMother_Tel.Text = DBNull2Str(.Item("Mother_Tel"))
                txtSibling.Text = DBNull2Str(.Item("Sibling"))
                txtChildNo.Text = DBNull2Str(.Item("ChildNo"))

                txtAddressLive.Text = DBNull2Str(.Item("AddressLive"))
                txtTelLive.Text = DBNull2Str(.Item("TelLive"))


                txtAddressCard.Text = DBNull2Str(.Item("Address"))
                txtDistrict.Text = DBNull2Str(.Item("District"))
                txtCity.Text = DBNull2Str(.Item("City"))

                If Not IsDBNull(.Item("ProvinceID")) Then
                    ddlProvince.SelectedValue = .Item("ProvinceID")
                End If

                txtZipCode.Text = DBNull2Str(.Item("ZipCode"))
                txtCongenitalDisease.Text = DBNull2Str(.Item("CongenitalDisease"))
                txtMedicineUsually.Text = DBNull2Str(.Item("MedicineUsually"))
                txtMedicalHistory.Text = DBNull2Str(.Item("MedicalHistory"))
                txtHobby.Text = DBNull2Str(.Item("Hobby"))
                'txtTalent.Text = DBNull2Str(.Item("Talent"))
                'txtExpectations.Text = DBNull2Str(.Item("Expectations"))
                txtPrimarySchool.Text = DBNull2Str(.Item("PrimarySchool"))
                txtPrimaryProvince.Text = DBNull2Str(.Item("PrimaryProvince"))
                txtPrimaryYear.Text = Zero2StrNull(.Item("PrimaryYear"))
                txtSecondarySchool.Text = DBNull2Str(.Item("SecondarySchool"))
                txtSecondaryProvince.Text = DBNull2Str(.Item("SecondaryProvince"))
                txtSecondaryYear.Text = Zero2StrNull(.Item("SecondaryYear"))
                txtHighSchool.Text = DBNull2Str(.Item("HighSchool"))
                txtHighProvince.Text = DBNull2Str(.Item("HighProvince"))
                txtHighYear.Text = Zero2StrNull(.Item("HighYear"))
                txtContactName.Text = DBNull2Str(.Item("ContactName"))
                txtContactAddress.Text = DBNull2Str(.Item("ContactAddress"))
                txtContactTel.Text = DBNull2Str(.Item("ContactTel"))

                txtGPAX.Text = DBNull2Str(.Item("GPAX"))
                lblLevelClass.Text = DBNull2Str(.Item("LevelClass"))
                txtContactRelation.Text = DBNull2Str(.Item("ContactRelation"))

                Session("picname") = ""
                'Session("picsign") = ""

                If DBNull2Str(.Item("PicturePath")) <> "" Then
                    Session("picname") = .Item("PicturePath")
                    picStudent.ImageUrl = "~/" & StudentPic & "/" & .Item("PicturePath")
                Else
                    picStudent.ImageUrl = "~/" & StudentPic & "/user_blank.jpg"
                End If


                'If DBNull2Str(.Item("SignaturePath")) <> "" Then
                '    Session("picsign") = .Item("SignaturePath")
                '    imgSign.ImageUrl = "~/" & StudentPic & "signature/" & .Item("SignaturePath")
                'Else
                '    imgSign.ImageUrl = "~/" & StudentPic & "signature/nopic.jpg"
                'End If

                If .Item("StudentStatus") = "40" Then
                    lblLevelClass.Text = String.Concat(.Item("StatusName"))

                End If

                txtRegion.Text = String.Concat(.Item("Religion"))
                txtFirstNameEN.Text = String.Concat(.Item("FirstNameEN"))
                txtLastNameEN.Text = String.Concat(.Item("LastNameEN"))
                txtMedicalRecommend.Text = String.Concat(.Item("MedicalRecommend"))
                txtHospital.Text = String.Concat(.Item("HospitalName"))
                txtDoctor.Text = String.Concat(.Item("DoctorName"))
                txtCharacter.Text = String.Concat(.Item("Characteristic"))
                'txtFavoriteSubject.Text = String.Concat(.Item("FavoriteSubject"))
                'txtExperience.Text = String.Concat(.Item("EduExperience"))
                ddlPayor.SelectedValue = String.Concat(.Item("PayorID"))
                txtPayorDesc.Text = String.Concat(.Item("PayorDetail"))

                txtLinkCV.Text = String.Concat(.Item("CVLink"))
                txtLineID.Text = String.Concat(.Item("LineID"))

                txtAccount.Text = String.Concat(.Item("BankAccount"))
                ddlBank.SelectedValue = String.Concat(.Item("BankCode"))

                txtSize.Text = String.Concat(.Item("GownSize"))
                optCovid.SelectedValue = String.Concat(.Item("CovidInsurance"))

                txtWorkPlace.Text = String.Concat(.Item("WorkPlace"))

                LoadEducation()
            End With

        End If


    End Sub
    Private Sub LoadPrefix()
        dt = ctlM.Prefix_Get
        ddlPrefix.DataSource = dt
        ddlPrefix.DataValueField = "PrefixID"
        ddlPrefix.DataTextField = "PrefixName"
        ddlPrefix.DataBind()
        dt = Nothing
    End Sub

    Private Sub LoadBank()
        dt = ctlM.bank_get
        ddlBank.DataSource = dt
        ddlBank.DataValueField = "BankCode"
        ddlBank.DataTextField = "BankName"
        ddlBank.DataBind()
        dt = Nothing
    End Sub

    Private Sub LoadPayorToDDL()
        dt = ctlM.Payor_Get
        If dt.Rows.Count > 0 Then
            With ddlPayor
                .Enabled = True
                .DataSource = dt
                .DataTextField = "PayorName"
                .DataValueField = "PayorID"
                .DataBind()
                '.SelectedIndex = 0
            End With
        End If
        dt = Nothing
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

    Private Sub BindDayToDDL()
        Dim sD As String = ""
        For i = 1 To 31
            If i < 10 Then
                sD = "0" & i
            Else
                sD = i
            End If

            With ddlDay
                .Items.Add(i)
                .Items(i - 1).Value = sD
                .SelectedIndex = 0
            End With
        Next
    End Sub
    Private Sub BindMonthToDDL()
        Dim sD As String = ""

        For i = 1 To 12
            If i < 10 Then
                sD = "0" & i
            Else
                sD = i
            End If

            With ddlMonth
                .Items.Add(DisplayNumber2Month(i))
                .Items(i - 1).Value = sD
                .SelectedIndex = 0
            End With
        Next
    End Sub

    Private Sub BindYearToDDL()
        Dim ctlb As New ApplicationBaseClass
        Dim sDate As Date
        Dim y As Integer

        sDate = ctlb.GET_DATE_SERVER()

        If sDate.Year < 2500 Then
            y = (sDate.Year + 543)
        Else
            y = sDate.Year
        End If
        Dim i As Integer = 0
        Dim n As Integer = y

        For i = 0 To y - 15
            With ddlYear
                .Items.Add(n)
                .Items(i).Value = n
                .SelectedIndex = 0
            End With
            n = n - 1
            If n < 2530 Then
                Exit For
            End If
        Next
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        lblStdCode.Text = ""
        ddlPrefix.SelectedIndex = 0
        txtFirstNameTH.Text = ""
        txtLastNameTH.Text = ""
        txtNickName.Text = ""
        optGender.SelectedIndex = 0
        lblMajorName.Text = ""

        lblAdvisorName.Text = ""
        txtGPAX.Text = ""
        lblLevelClass.Text = ""
        txtEmail.Text = ""
        ddlDay.SelectedIndex = 0
        ddlMonth.SelectedIndex = 0
        ddlYear.SelectedIndex = 0
        ddlBloodGroup.SelectedIndex = 0
        txtTelephone.Text = ""
        txtMobile.Text = ""
        txtCongenitalDisease.Text = ""
        txtMedicineUsually.Text = ""
        txtMedicalHistory.Text = ""
        txtHobby.Text = ""
        'txtTalent.Text = ""
        'txtExpectations.Text = ""
        txtFather_FirstName.Text = ""
        txtFather_LastName.Text = ""
        txtFather_Career.Text = ""
        txtFather_Tel.Text = ""
        txtMother_FirstName.Text = ""
        txtMother_LastName.Text = ""
        txtMother_Career.Text = ""
        txtMother_Tel.Text = ""
        txtSibling.Text = ""
        txtChildNo.Text = ""
        txtAddressCard.Text = ""
        txtDistrict.Text = ""
        txtCity.Text = ""
        ddlProvince.SelectedIndex = 0
        txtZipCode.Text = ""
        txtPrimarySchool.Text = ""
        txtPrimaryProvince.Text = ""
        txtPrimaryYear.Text = ""
        txtSecondarySchool.Text = ""
        txtSecondaryProvince.Text = ""
        txtSecondaryYear.Text = ""
        txtHighSchool.Text = ""
        txtHighProvince.Text = ""
        txtHighYear.Text = ""
        txtContactName.Text = ""
        txtContactTel.Text = ""
        txtContactAddress.Text = ""
        txtContactRelation.Text = ""
        hdEduUID.Value = "0"
        cmdPrint.Visible = False
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        Dim BDate As String = ddlDay.SelectedValue
        BDate = ddlYear.SelectedValue & ddlMonth.SelectedValue & BDate
        'If txtGPAX.Text <> "" Then
        '    If Not IsNumeric(txtGPAX.Text) Or StrNull2Zero(txtGPAX.Text) <= 0 Then

        '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','GPAX ไม่ถูกต้อง');", True)
        '        Exit Sub
        '    End If
        'End If

        If txtEmail.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณากรอกอีเมล์ก่อน');", True)
            Exit Sub
        End If

        If StrNull2Zero(ddlDay.SelectedValue) <= 0 Or StrNull2Zero(ddlMonth.SelectedValue) = 0 Or StrNull2Zero(ddlYear.SelectedValue) = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาตรวจสอบวันเกิด');", True)
            Exit Sub
        End If

        If txtMobile.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุเบอร์มือถือก่อน');", True)
            Exit Sub
        End If
        If txtAddressLive.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุทีอยู่ระหว่างศึกษาก่อน');", True)
            Exit Sub
        End If

        If txtAddressCard.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุทีอยู่ตามบัตรประชาชนก่อน');", True)
            Exit Sub
        End If

        If txtDistrict.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุตำบลตามบัตรประชาชนก่อน');", True)
            Exit Sub
        End If

        If txtCity.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุอำเภอตามบัตรประชาชนก่อน');", True)
            Exit Sub
        End If


        If txtPrimarySchool.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุสถานศึกษาระดับประถมก่อน');", True)
            Exit Sub
        End If
        If txtPrimaryProvince.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุจังหวัดที่ศึกษาระดับประถมก่อน');", True)
            Exit Sub
        End If
        If txtPrimaryYear.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุปีที่จบการศึกษาระดับประถมก่อน');", True)
            Exit Sub
        End If


        If txtSecondarySchool.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุสถานศึกษาระดับมัธยมตอนต้นก่อน');", True)
            Exit Sub
        End If
        If txtSecondaryProvince.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุจังหวัดที่ศึกษาระดับมัธยมตอนต้นก่อน');", True)
            Exit Sub
        End If
        If txtSecondaryYear.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุปีที่จบการศึกษาระดับมัธยมตอนต้นก่อน');", True)
            Exit Sub
        End If


        If txtHighSchool.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุสถานศึกษาระดับมัธยมตอนปลายก่อน');", True)
            Exit Sub
        End If
        If txtHighProvince.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุจังหวัดที่ศึกษาระดับมัธยมตอนปลายก่อน');", True)
            Exit Sub
        End If
        If txtHighYear.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุปีที่จบการศึกษาระดับมัธยมตอนปลายก่อน');", True)
            Exit Sub
        End If


        ctlStd.Student_Update(lblStdCode.Text, ddlPrefix.SelectedValue, txtFirstNameTH.Text, txtLastNameTH.Text, txtNickName.Text, optGender.SelectedValue, txtEmail.Text, BDate, ddlBloodGroup.SelectedValue, txtTelephone.Text, txtMobile.Text, txtCongenitalDisease.Text, txtMedicineUsually.Text, txtMedicalHistory.Text, txtHobby.Text, "", "", txtFather_FirstName.Text, txtFather_LastName.Text, txtFather_Career.Text, txtFather_Tel.Text, txtMother_FirstName.Text, txtMother_LastName.Text, txtMother_Career.Text, txtMother_Tel.Text, StrNull2Zero(txtSibling.Text), StrNull2Zero(txtChildNo.Text), txtAddressCard.Text, txtDistrict.Text, txtCity.Text, StrNull2Zero(ddlProvince.SelectedValue), ddlProvince.SelectedItem.Text, txtZipCode.Text, txtPrimarySchool.Text, txtPrimaryProvince.Text, StrNull2Zero(txtPrimaryYear.Text), txtSecondarySchool.Text, txtSecondaryProvince.Text, StrNull2Zero(txtSecondaryYear.Text), txtHighSchool.Text, txtHighProvince.Text, StrNull2Zero(txtHighYear.Text), txtContactName.Text, txtContactTel.Text, txtContactAddress.Text, txtContactRelation.Text, Session("picname"), Request.Cookies("UserLoginID").Value, StrNull2Double(txtGPAX.Text), txtAddressLive.Text, txtTelLive.Text, txtFirstNameEN.Text, txtLastNameEN.Text, txtRegion.Text, txtMedicalRecommend.Text, txtHospital.Text, txtDoctor.Text, txtCharacter.Text, "", "", ddlPayor.SelectedValue, txtPayorDesc.Text, txtLinkCV.Text, txtLineID.Text, ddlBank.SelectedValue, txtAccount.Text, txtWorkPlace.Text, txtSize.Text, optCovid.SelectedValue)

        Dim objuser As New UserController
        If txtEmail.Text <> "" Then
            objuser.User_UpdateMail(lblStdCode.Text, txtEmail.Text)

        End If


        objuser.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_UPD, "Student", "บันทึก/แก้ไข ประวัตินิสิต :" & lblStdCode.Text, "")
        cmdPrint.Visible = True

        Response.Redirect("ResultPage.aspx?p=complete")

    End Sub

    Private Sub UploadFile()

        Dim pf As HttpPostedFileAJAX = FileUploaderAJAX1.PostedFile

        'กรณีต้องการต้องสอบชนิดและขนาดไฟล์
        If ((pf.ContentType.Equals("image/jpeg") Or pf.ContentType.Equals("image/jpg")) And pf.ContentLength <= 500 * 1024) Then

            Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & StudentPic & lblStdCode.Text & ".jpg"))

            If objfile.Exists Then
                objfile.Delete()
            End If
            FileUploaderAJAX1.SaveAs("~/" & StudentPic, lblStdCode.Text & ".jpg")
            Session("picname") = lblStdCode.Text & ".jpg"
        Else

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถอัปโหลดรูปท่านได้ เนื่องจากขนาดรูปท่านใหญ่เกิน 500K กรุณาลองใหม่ภายหลัง');", True)

        End If

    End Sub
    'Private Sub UploadSignature()

    '    Dim pf As HttpPostedFileAJAX = FileUploaderAJAX2.PostedFile

    '    'กรณีต้องการต้องสอบชนิดและขนาดไฟล์
    '    If ((pf.ContentType.Equals("image/jpeg") Or pf.ContentType.Equals("image/jpg")) And pf.ContentLength <= 500 * 1024) Then

    '        Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & StudentPic & "signature/" & lblStdCode.Text & ".jpg"))

    '        If objfile.Exists Then
    '            objfile.Delete()
    '        End If
    '        FileUploaderAJAX2.SaveAs("~/" & StudentPic & "signature/", lblStdCode.Text & ".jpg")
    '        Session("picsign") = lblStdCode.Text & ".jpg"
    '    Else

    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถอัปโหลดรูปท่านได้ เนื่องจากขนาดรูปท่านใหญ่เกิน 500K กรุณาลองใหม่ภายหลัง');", True)

    '    End If

    'End Sub

    Dim ctlEdu As New EducationController

    Private Sub LoadEducation()
        dt = ctlEdu.Education_Get(StrNull2Zero(hdStudentID.Value))
        grdEdu.DataSource = dt
        grdEdu.DataBind()
    End Sub

    Protected Sub cmdAddEdu_Click(sender As Object, e As EventArgs) Handles cmdAddEdu.Click
        If txtCourse.Text = "" Or txtSchool.Text = "" Or txtProvinceEdu.Text = "" Or txtYearEdu.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุข้อมูลให้ครบถ้วนก่อน');", True)
            Exit Sub
        End If

        ctlEdu.Education_Save(StrNull2Zero(hdEduUID.Value), StrNull2Zero(hdStudentID.Value), txtCourse.Text, txtSchool.Text, txtProvinceEdu.Text, txtYearEdu.Text)

        LoadEducation()
        hdEduUID.Value = "0"
        txtCourse.Text = ""
        txtSchool.Text = ""
        txtProvinceEdu.Text = ""
        txtYearEdu.Text = ""
        txtCourse.Focus()
    End Sub
End Class