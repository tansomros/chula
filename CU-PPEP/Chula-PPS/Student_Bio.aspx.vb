Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Subgurim.Controles


Public Class Student_Bio
    Inherits System.Web.UI.Page
    Dim dt As New DataTable

    Dim ctlFct As New FacultyController
    Dim ctlbase As New ApplicationBaseClass

    Dim ctlStd As New StudentController
    Dim objStd As New StudentInfo


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'hlnkBack.NavigateUrl = "Student_Reg.aspx?m=1&p=101"
            'hlnkPrint.NavigateUrl = "ReportViewerStudentBio.aspx?ActionType=std&id=" & Request.Cookies("ProfileID").Value
            hlnkBack.NavigateUrl = "Student_Reg.aspx?ActionType=std&ItemType=std&stdcode=" & Request("stdcode")
            hlnkPrint.NavigateUrl = "ReportViewerStudentBio.aspx?ActionType=std&ItemType=std&stdcode=" & Request("stdcode")

            LoadStudentData()

            If Request.Cookies("ROLE_STD").Value = True Or Request.Cookies("ROLE_ADM").Value = True Or Request.Cookies("ROLE_SPA").Value = True Then
                hlnkBack.Visible = True
            Else
                hlnkBack.Visible = False
            End If

        End If

            'picStudent.ImageUrl = "~/" & stdPic & "/" & Request.Cookies("ProfileID").Value & ".jpg"

            btnOK.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOK.UniqueID, "")


        '  cmdPrint.Attributes.Add("onClick", "window.open('" + ResolveUrl("ReportViewerStudentBio.aspx?id=" + lblStdCode.Text) + "', 'windowname', 'width=800,height=600,scrollbars=yes')")


    End Sub

    Private Sub LoadStudentData()
        If Not Request("stdcode") Is Nothing Then
            hlnkPrint.NavigateUrl = "ReportViewerStudentBio.aspx?ActionType=std&id=" & Request("stdcode") & "&stdcode=" & Request("stdcode")
            hlnkBack.NavigateUrl = "Student_Reg.aspx?m=std&stdcode=" & Request("stdcode")
            dt = ctlStd.Student_GetByCode(Request("stdcode"))
        Else
            hlnkPrint.NavigateUrl = "ReportViewerStudentBio.aspx?ActionType=std&id=" & Request.Cookies("ProfileID").Value & "&stdcode=" & Request.Cookies("ProfileID").Value
            hlnkBack.NavigateUrl = "Student_Reg.aspx?m=std&stdcode=" & Request.Cookies("ProfileID").Value
            dt = ctlStd.Student_GetByCode(Request.Cookies("ProfileID").Value)
        End If

        ClearData()
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdStudentID.Value = String.Concat(.Item("Student_ID"))
                lblStdCode.Text = .Item(objStd.tblField(objStd.fldPos.f01_Student_Code).fldName)

                If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f02_Prefix).fldName)) Then
                    lblPrefix.Text = .Item(objStd.tblField(objStd.fldPos.f02_Prefix).fldName)
                End If

                lblFirstName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f03_FirstName).fldName))
                lblLastName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f04_LastName).fldName))
                lblNickName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f05_NickName).fldName))

                If String.Concat(.Item(objStd.tblField(objStd.fldPos.f06_Gender).fldName)) = "M" Then
                    lblGender.Text = "ชาย"
                ElseIf String.Concat(.Item(objStd.tblField(objStd.fldPos.f06_Gender).fldName)) = "F" Then

                    lblGender.Text = "หญิง"
                End If

                lblEmail.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f07_Email).fldName))
                lblTelephone.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f08_Telephone).fldName))
                lblMobile.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f09_MobilePhone).fldName))


                If DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f10_BirthDate).fldName)) <> "" Then
                    lblBirthDate.Text = DisplayStr2DateTH(.Item(objStd.tblField(objStd.fldPos.f10_BirthDate).fldName))
                End If

                If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f11_BloodGroup).fldName)) Then
                    lblBloodGroup.Text = .Item(objStd.tblField(objStd.fldPos.f11_BloodGroup).fldName)
                End If

                lblMajorName.Text = String.Concat(.Item("MajorName"))
                lblMinorName.Text = String.Concat(.Item("MinorName"))
                'lblSubMinorName.Text = String.Concat(.Item("SubMinorName"))


                lblAdvisorName.Text = String.Concat(.Item("AdvisorName"))
                lblSeniorProject.Text = String.Concat(.Item("SeniorProjectName"))


                lblFather_FirstName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f16_Father_FirstName).fldName))
                lblFather_LastName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f17_Father_LastName).fldName))
                lblFather_Career.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f18_Father_Career).fldName))
                lblFather_Tel.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f19_Father_Tel).fldName))
                lblMother_FirstName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f20_Mother_FirstName).fldName))
                lblMother_LastName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f21_Mother_LastName).fldName))
                lblMother_Career.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f22_Mother_Career).fldName))
                lblMother_Tel.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f23_Mother_Tel).fldName))
                lblSibling.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f24_Sibling).fldName))
                lblChildNo.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f25_ChildNo).fldName))

                lblAddressLive.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f57_AddressLive).fldName))
                lblTelLive.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f58_TelLive).fldName))

                lblAddress.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f26_Address).fldName))
                lblDistrict.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f27_District).fldName))
                lblCity.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f28_City).fldName))

                If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f29_ProvinceID).fldName)) Then
                    lblProvince.Text = String.Concat(.Item(objStd.tblField(objStd.fldPos.f30_ProvinceName).fldName))
                End If

                lblZipCode.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f31_ZipCode).fldName))
                lblCongenitalDisease.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f32_CongenitalDisease).fldName))
                lblMedicineUsually.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f33_MedicineUsually).fldName))
                lblMedicalHistory.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f34_MedicalHistory).fldName))
                lblHobby.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f35_Hobby).fldName))
                lblTalent.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f36_Talent).fldName))
                lblExpectations.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f37_Expectations).fldName))
                lblPrimarySchool.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f38_PrimarySchool).fldName))
                lblPrimaryProvince.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f39_PrimaryProvince).fldName))

                lblPrimaryYear.Text = Zero2StrNull(.Item(objStd.tblField(objStd.fldPos.f40_PrimaryYear).fldName))

                lblSecondarySchool.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f41_SecondarySchool).fldName))
                lblSecondaryProvince.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f42_SecondaryProvince).fldName))
                lblSecondaryYear.Text = Zero2StrNull(.Item(objStd.tblField(objStd.fldPos.f43_SecondaryYear).fldName))
                lblHighSchool.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f44_HighSchool).fldName))
                lblHighProvince.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f45_HighProvince).fldName))
                lblHighYear.Text = Zero2StrNull(.Item(objStd.tblField(objStd.fldPos.f46_HighYear).fldName))
                lblContactName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f47_ContactName).fldName))
                lblContactAddress.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f48_ContactAddress).fldName))
                lblContactTel.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f49_ContactTel).fldName))

                lblGPAX.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f54_GPAX).fldName))

                lblContactRelation.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f55_ContactRelation).fldName))


                If DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f50_PicturePath).fldName)) <> "" Then
                    Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & stdPic & "/" & .Item("PicturePath")))

                    If objfile.Exists Then
                        picStudent.ImageUrl = "~/" & stdPic & "/" & .Item(objStd.tblField(objStd.fldPos.f50_PicturePath).fldName)
                    Else
                        picStudent.ImageUrl = "~/" & stdPic & "/nopic" & String.Concat(.Item(objStd.tblField(objStd.fldPos.f06_Gender).fldName)) & ".jpg"
                    End If
                Else

                End If

                lblStudentStatus.Text = String.Concat(.Item("StatusName"))
                lblReligion.Text = String.Concat(.Item("Religion"))
                lblFirstNameEN.Text = String.Concat(.Item("FirstNameEN"))
                lblLastNameEN.Text = String.Concat(.Item("LastNameEN"))
                lblMedicalRecommend.Text = String.Concat(.Item("MedicalRecommend"))
                lblHospitalName.Text = String.Concat(.Item("HospitalName"))
                lblDoctorName.Text = String.Concat(.Item("DoctorName"))
                lblCharacter.Text = String.Concat(.Item("Characteristic"))
                lblFavoriteSubject.Text = String.Concat(.Item("FavoriteSubject"))
                lblExperience.Text = String.Concat(.Item("EduExperience"))

                lblClaim.Text = String.Concat(.Item("PayorName"))
                If String.Concat(.Item("PayorDetail")) <> "" Then
                    lblClaim.Text &= " (" & String.Concat(.Item("PayorDetail")) & ")"
                End If

                If String.Concat(.Item("CVLink")) <> "" Then
                    hlnkCV.NavigateUrl = String.Concat(.Item("CVLink"))
                End If

                LoadEducation()

            End With
        Else
            lblAlert.Text = "ไม่พบข้อมูล"
            ModalPopupExtender1.Show()
        End If


    End Sub
    Dim ctlEdu As New EducationController
    Private Sub LoadEducation()
        dt = ctlEdu.Education_Get(StrNull2Zero(hdStudentID.Value))
        grdEdu.DataSource = dt
        grdEdu.DataBind()
    End Sub

    Private Sub ClearData()

        lblStdCode.Text = ""
        lblPrefix.Text = ""
        lblFirstName.Text = ""
        lblLastName.Text = ""
        lblNickName.Text = ""
        lblGender.Text = ""
        lblMajorName.Text = ""
        lblMinorName.Text = ""
        'lblSubMinorName.Text = ""
        lblAdvisorName.Text = ""
        lblSeniorProject.Text = ""
        lblGPAX.Text = ""
        lblEmail.Text = ""
        lblBirthDate.Text = ""
        lblBloodGroup.Text = ""
        lblTelephone.Text = ""
        lblMobile.Text = ""
        lblCongenitalDisease.Text = ""
        lblMedicineUsually.Text = ""
        lblMedicalHistory.Text = ""
        lblHobby.Text = ""
        lblTalent.Text = ""
        lblExpectations.Text = ""
        lblFather_FirstName.Text = ""
        lblFather_LastName.Text = ""
        lblFather_Career.Text = ""
        lblFather_Tel.Text = ""
        lblMother_FirstName.Text = ""
        lblMother_LastName.Text = ""
        lblMother_Career.Text = ""
        lblMother_Tel.Text = ""
        lblSibling.Text = ""
        lblChildNo.Text = ""
        lblAddressLive.Text = ""
        lblTelLive.Text = ""
        lblAddress.Text = ""
        lblDistrict.Text = ""
        lblCity.Text = ""
        lblProvince.Text = ""
        lblZipCode.Text = ""
        lblPrimarySchool.Text = ""
        lblPrimaryProvince.Text = ""
        lblPrimaryYear.Text = ""
        lblSecondarySchool.Text = ""
        lblSecondaryProvince.Text = ""
        lblSecondaryYear.Text = ""
        lblHighSchool.Text = ""
        lblHighProvince.Text = ""
        lblHighYear.Text = ""
        lblContactName.Text = ""
        lblContactTel.Text = ""
        lblContactAddress.Text = ""
        lblContactRelation.Text = ""

        ' cmdPrint.Visible = False
    End Sub


    'Protected Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
    '    Response.Redirect("Student_Reg.aspx?m=1&p=101")
    'End Sub
End Class