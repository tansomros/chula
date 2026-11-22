Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Subgurim.Controles


Public Class Student_Reg
    Inherits System.Web.UI.Page
    Dim dt As New DataTable

    Dim ctlFct As New FacultyController
    Dim ctlbase As New ApplicationBaseClass

    Dim ctlStd As New StudentController
    Dim objStd As New StudentInfo


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            cmdPrint.Visible = False
            BindDayToDDL()
            BindMonthToDDL()
            BindYearToDDL()
            LoadStatusToDDL()
            LoadPayorToDDL()
            LoadProvinceToDDL()
            LoadMajorToDDL()
            LoadAdvisorToDDL()
            LoadStudentData()
        End If

        If (FileUploaderAJAX1.IsPosting) Then
            UploadFile()
        End If

        'picStudent.ImageUrl = "~/" & stdPic & "/" & Request.Cookies("ProfileID").Value & ".jpg"

        cmdPrint.Attributes.Add("onClick", "window.open('" + ResolveUrl("ReportViewerStudentBio.aspx?id=" + lblStdCode.Text) + "', 'windowname', 'width=800,height=600,scrollbars=yes')")

        txtGPAX.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
    End Sub
    Private Sub LoadStatusToDDL()

        dt = ctlStd.StudentStatus_GetAll()

        ddlStudentStatus.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlStudentStatus
                .Enabled = True
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & dt.Rows(i)("StatusCode") & " " & dt.Rows(i)("StatusName"))
                    .Items(i).Value = dt.Rows(i)("StatusCode")
                Next

                .SelectedIndex = 0

            End With

        End If
    End Sub
    Private Sub LoadStudentData()

        'dt = ctlStd.Student_GetByCode(Request.Cookies("ProfileID").Value)
        If Request("stdcode") Is Nothing Then
            dt = ctlStd.Student_GetByCode(Request.Cookies("ProfileID").Value)
        Else
            dt = ctlStd.Student_GetByCode(Request("stdcode"))
        End If

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)

                hdStudentID.Value = String.Concat(.Item("Student_ID"))

                lblStdCode.Text = .Item(objStd.tblField(objStd.fldPos.f01_Student_Code).fldName)

                If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f02_Prefix).fldName)) Then
                    ddlPrefix.SelectedValue = .Item(objStd.tblField(objStd.fldPos.f02_Prefix).fldName)
                End If

                txtFirstNameTH.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f03_FirstName).fldName))
                txtLastNameTH.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f04_LastName).fldName))
                txtNickName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f05_NickName).fldName))

                If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f06_Gender).fldName)) Then
                    optGender.SelectedValue = .Item(objStd.tblField(objStd.fldPos.f06_Gender).fldName)
                End If

                txtEmail.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f07_Email).fldName))
                txtTelephone.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f08_Telephone).fldName))
                txtMobile.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f09_MobilePhone).fldName))

                If DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f10_BirthDate).fldName)) <> "" Then
                    ddlDay.SelectedValue = Right(.Item(objStd.tblField(objStd.fldPos.f10_BirthDate).fldName), 2)
                    ddlMonth.SelectedValue = Mid(.Item(objStd.tblField(objStd.fldPos.f10_BirthDate).fldName), 5, 2)
                    ddlYear.SelectedValue = Left(.Item(objStd.tblField(objStd.fldPos.f10_BirthDate).fldName), 4)
                End If


                If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f11_BloodGroup).fldName)) Then
                    ddlBloodGroup.SelectedValue = .Item(objStd.tblField(objStd.fldPos.f11_BloodGroup).fldName)
                End If


                If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f12_MajorID).fldName)) Then
                    ddlMajor.SelectedValue = .Item(objStd.tblField(objStd.fldPos.f12_MajorID).fldName)
                End If
                If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f13_MinorID).fldName)) Then
                    LoadMinorToDDL()
                    If .Item(objStd.tblField(objStd.fldPos.f13_MinorID).fldName).ToString <> "0" Then
                        ddlMinor.SelectedValue = .Item(objStd.tblField(objStd.fldPos.f13_MinorID).fldName)
                    End If
                End If
                'If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f14_SubMinorID).fldName)) Then
                '    LoadSubMinorToDDL()
                '    If .Item(objStd.tblField(objStd.fldPos.f14_SubMinorID).fldName).ToString <> "0" Then
                '        ddlSubMinor.SelectedValue = .Item(objStd.tblField(objStd.fldPos.f14_SubMinorID).fldName)
                '    End If
                'End If

                'lblMajorName.Text = String.Concat(.Item("MajorName"))
                'lblMinorName.Text = String.Concat(.Item("MinorName"))
                'lblSubMinorName.Text = String.Concat(.Item("SubMinorName"))


                ddlAdvisor.SelectedValue = String.Concat(.Item("AdvisorID"))
                ddlSeniorProject.SelectedValue = String.Concat(.Item("SeniorProjectID"))


                txtFather_FirstName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f16_Father_FirstName).fldName))
                txtFather_LastName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f17_Father_LastName).fldName))
                txtFather_Career.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f18_Father_Career).fldName))
                txtFather_Tel.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f19_Father_Tel).fldName))
                txtMother_FirstName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f20_Mother_FirstName).fldName))
                txtMother_LastName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f21_Mother_LastName).fldName))
                txtMother_Career.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f22_Mother_Career).fldName))
                txtMother_Tel.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f23_Mother_Tel).fldName))
                txtSibling.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f24_Sibling).fldName))
                txtChildNo.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f25_ChildNo).fldName))

                txtAddressLive.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f57_AddressLive).fldName))
                txtTelLive.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f58_TelLive).fldName))


                txtAddressCard.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f26_Address).fldName))
                txtDistrict.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f27_District).fldName))
                txtCity.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f28_City).fldName))

                If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f29_ProvinceID).fldName)) Then
                    ddlProvince.SelectedValue = .Item(objStd.tblField(objStd.fldPos.f29_ProvinceID).fldName)
                End If

                txtZipCode.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f31_ZipCode).fldName))
                txtCongenitalDisease.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f32_CongenitalDisease).fldName))
                txtMedicineUsually.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f33_MedicineUsually).fldName))
                txtMedicalHistory.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f34_MedicalHistory).fldName))
                txtHobby.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f35_Hobby).fldName))
                txtTalent.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f36_Talent).fldName))
                txtExpectations.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f37_Expectations).fldName))
                txtPrimarySchool.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f38_PrimarySchool).fldName))
                txtPrimaryProvince.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f39_PrimaryProvince).fldName))
                txtPrimaryYear.Text = Zero2StrNull(.Item(objStd.tblField(objStd.fldPos.f40_PrimaryYear).fldName))
                txtSecondarySchool.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f41_SecondarySchool).fldName))
                txtSecondaryProvince.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f42_SecondaryProvince).fldName))
                txtSecondaryYear.Text = Zero2StrNull(.Item(objStd.tblField(objStd.fldPos.f43_SecondaryYear).fldName))
                txtHighSchool.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f44_HighSchool).fldName))
                txtHighProvince.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f45_HighProvince).fldName))
                txtHighYear.Text = Zero2StrNull(.Item(objStd.tblField(objStd.fldPos.f46_HighYear).fldName))
                txtContactName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f47_ContactName).fldName))
                txtContactAddress.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f48_ContactAddress).fldName))
                txtContactTel.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f49_ContactTel).fldName))

                txtGPAX.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f54_GPAX).fldName))

                txtLevelClass.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f56_LevelClass).fldName))
                txtContactRelation.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f55_ContactRelation).fldName))

                Session("picname") = ""
                If DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f50_PicturePath).fldName)) <> "" Then
                    Session("picname") = .Item(objStd.tblField(objStd.fldPos.f50_PicturePath).fldName)
                    picStudent.ImageUrl = "~/" & stdPic & "/" & .Item(objStd.tblField(objStd.fldPos.f50_PicturePath).fldName)
                End If

                If .Item("StudentStatus") = "40" Then
                    txtLevelClass.Text = String.Concat(.Item("StatusName"))
                End If

                ddlStudentStatus.SelectedValue = .Item("StudentStatus")

                txtRegion.Text = String.Concat(.Item("Religion"))
                txtFirstNameEN.Text = String.Concat(.Item("FirstNameEN"))
                txtLastNameEN.Text = String.Concat(.Item("LastNameEN"))
                txtMedicalRecommend.Text = String.Concat(.Item("MedicalRecommend"))
                txtHospital.Text = String.Concat(.Item("HospitalName"))
                txtDoctor.Text = String.Concat(.Item("DoctorName"))
                txtCharacter.Text = String.Concat(.Item("Characteristic"))
                txtFavoriteSubject.Text = String.Concat(.Item("FavoriteSubject"))
                txtExperience.Text = String.Concat(.Item("EduExperience"))
                ddlPayor.SelectedValue = String.Concat(.Item("PayorID"))
                txtPayorDesc.Text = String.Concat(.Item("PayorDetail"))

                txtLinkCV.Text = String.Concat(.Item("CVLink"))

                LoadEducation()
            End With

        End If


    End Sub
    Private Sub LoadPayorToDDL()
        dt = ctlbase.Payor_Get
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
        'lblMajorName.Text = ""
        'lblMinorName.Text = ""
        'lblSubMinorName.Text = ""
        ddlAdvisor.SelectedIndex = 0
        ddlSeniorProject.SelectedIndex = 0
        txtGPAX.Text = ""
        txtLevelClass.Text = ""
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
        txtTalent.Text = ""
        txtExpectations.Text = ""
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
        If txtGPAX.Text <> "" Then
            If Not IsNumeric(txtGPAX.Text) Or StrNull2Zero(txtGPAX.Text) <= 0 Then

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','GPAX ไม่ถูกต้อง');", True)
                Exit Sub
            End If
        End If

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

        Dim LevelClass As Integer
        If ddlStudentStatus.SelectedValue <> 10 Then
            LevelClass = 0
        Else
            LevelClass = StrNull2Zero(txtLevelClass.Text)
        End If

        ctlStd.Student_Update(lblStdCode.Text, ddlPrefix.SelectedValue, txtFirstNameTH.Text, txtLastNameTH.Text, txtNickName.Text, optGender.SelectedValue, txtEmail.Text, BDate, ddlBloodGroup.SelectedValue, txtTelephone.Text, txtMobile.Text, txtCongenitalDisease.Text, txtMedicineUsually.Text, txtMedicalHistory.Text, txtHobby.Text, txtTalent.Text, txtExpectations.Text, txtFather_FirstName.Text, txtFather_LastName.Text, txtFather_Career.Text, txtFather_Tel.Text, txtMother_FirstName.Text, txtMother_LastName.Text, txtMother_Career.Text, txtMother_Tel.Text, StrNull2Zero(txtSibling.Text), StrNull2Zero(txtChildNo.Text), txtAddressCard.Text, txtDistrict.Text, txtCity.Text, StrNull2Zero(ddlProvince.SelectedValue), ddlProvince.SelectedItem.Text, txtZipCode.Text, txtPrimarySchool.Text, txtPrimaryProvince.Text, StrNull2Zero(txtPrimaryYear.Text), txtSecondarySchool.Text, txtSecondaryProvince.Text, StrNull2Zero(txtSecondaryYear.Text), txtHighSchool.Text, txtHighProvince.Text, StrNull2Zero(txtHighYear.Text), txtContactName.Text, txtContactTel.Text, txtContactAddress.Text, txtContactRelation.Text, Session("picname"), Request.Cookies("UserID").Value, StrNull2Double(txtGPAX.Text), txtAddressLive.Text, txtTelLive.Text, txtFirstNameEN.Text, txtLastNameEN.Text, txtRegion.Text, txtMedicalRecommend.Text, txtHospital.Text, txtDoctor.Text, txtCharacter.Text, txtFavoriteSubject.Text, txtExperience.Text, ddlPayor.SelectedValue, txtPayorDesc.Text, txtLinkCV.Text, StrNull2Zero(ddlMajor.SelectedValue), StrNull2Zero(ddlMinor.SelectedValue), StrNull2Zero(ddlAdvisor.SelectedValue), StrNull2Zero(ddlSeniorProject.SelectedValue), StrNull2Zero(ddlStudentStatus.SelectedValue), LevelClass)

        Dim objuser As New UserController
        If txtEmail.Text <> "" Then
            objuser.User_UpdateMail(lblStdCode.Text, txtEmail.Text)

        End If

        objuser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Student", "บันทึก/แก้ไข ประวัตินิสิต :" & lblStdCode.Text, "")
        cmdPrint.Visible = True
        Response.Redirect("ResultPage.aspx?m=std&s=std&p=complete&stdcode=" & lblStdCode.Text)

    End Sub

    Private Sub UploadFile()

        Dim pf As HttpPostedFileAJAX = FileUploaderAJAX1.PostedFile

        'กรณีต้องการต้องสอบชนิดและขนาดไฟล์
        If ((pf.ContentType.Equals("image/jpeg") Or pf.ContentType.Equals("image/jpg")) And pf.ContentLength <= 500 * 1024) Then



            Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & stdPic & "/" & Request.Cookies("ProfileID").Value & ".jpg"))

            If objfile.Exists Then
                objfile.Delete()
            End If

            FileUploaderAJAX1.SaveAs("~/" & stdPic, Request.Cookies("ProfileID").Value & ".jpg")

            Session("picname") = Request.Cookies("ProfileID").Value & ".jpg"

            ' picStudent.ImageUrl = "~/" & stdPic & "/" & Request.Cookies("ProfileID").Value & ".jpg"

        Else

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถอัปโหลดรูปท่านได้ เนื่องจากขนาดรูปท่านใหญ่เกิน 500K กรุณาลองใหม่ภายหลัง');", True)

        End If

    End Sub
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
    Private Sub LoadAdvisorToDDL()
        Dim ctlP As New PersonController
        Dim dtPs As New DataTable
        dtPs = ctlP.Person_GetByType("T")
        If dtPs.Rows.Count > 0 Then
            With ddlAdvisor
                .DataSource = dtPs
                .DataTextField = "PersonName"
                .DataValueField = "PersonID"
                .DataBind()
                .SelectedIndex = 0
            End With
            With ddlSeniorProject
                .DataSource = dtPs
                .DataTextField = "PersonName"
                .DataValueField = "PersonID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        dtPs = Nothing
    End Sub
    Private Sub LoadMajorToDDL()
        Dim dtMajor As New DataTable
        dtMajor = ctlFct.GetMajor
        If dtMajor.Rows.Count > 0 Then
            With ddlMajor
                .Enabled = True
                .DataSource = dtMajor
                .DataTextField = "MajorName"
                .DataValueField = "MajorID"
                .DataBind()
                .SelectedIndex = 0
            End With
        Else

        End If
        dtMajor = Nothing
    End Sub

    Private Sub LoadMinorToDDL()
        Dim dtMinor As New DataTable
        dtMinor = ctlFct.GetMinor_ByMajorID(StrNull2Zero(ddlMajor.SelectedValue))
        If dtMinor.Rows.Count > 0 Then
            With ddlMinor
                .Enabled = True
                .DataSource = dtMinor
                .DataTextField = "MinorName"
                .DataValueField = "MinorID"
                .DataBind()
                .SelectedIndex = 0
            End With
        Else
            ddlMinor.Items.Clear()
            ddlMinor.DataSource = Nothing
            ddlMinor.Enabled = False
        End If
        dtMinor = Nothing
    End Sub

    'Private Sub LoadSubMinorToDDL()
    '    Dim dtSubMinor As New DataTable
    '    dtSubMinor = ctlFct.GetSubMinor_ByMinorID(StrNull2Zero(ddlMinor.SelectedValue))
    '    If dtSubMinor.Rows.Count > 0 Then
    '        With ddlSubMinor
    '            .Enabled = True
    '            .DataSource = dtSubMinor
    '            .DataTextField = "SubMinorName"
    '            .DataValueField = "SubMinorID"
    '            .DataBind()
    '            .SelectedIndex = 0
    '        End With
    '    Else
    '        ddlSubMinor.Items.Clear()
    '        ddlSubMinor.DataSource = Nothing
    '        ddlSubMinor.Enabled = False
    '    End If
    '    dtSubMinor = Nothing
    'End Sub


    Protected Sub ddlMajor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMajor.SelectedIndexChanged
        LoadMinorToDDL()
        'If ddlMinor.SelectedIndex = 0 Then
        '    LoadSubMinorToDDL()
        'End If
    End Sub

End Class
