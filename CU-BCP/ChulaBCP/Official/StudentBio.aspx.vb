Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Subgurim.Controles


Public Class StudentBio
    Inherits System.Web.UI.Page
    Dim dt As New DataTable

    Dim ctlFct As New MasterController
    Dim ctlbase As New ApplicationBaseClass

    Dim ctlStd As New StudentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadStudentData()

            'If Request.Cookies("ROLE_STD").Value = True Then
            '    hlnkBack.Visible = True
            'Else
            '    hlnkBack.Visible = False
            'End If

        End If

        'picStudent.ImageUrl = "~/" & StudentPic & "/" & Request.Cookies("PersonID").Value  & ".jpg"


        '  cmdPrint.Attributes.Add("onClick", "window.open('" + ResolveUrl("ReportViewerStudentBio.aspx?id=" + lblStdCode.Text) + "', 'windowname', 'width=800,height=600,scrollbars=yes')")


    End Sub

    Private Sub LoadStudentData()
        If Not Request("std") Is Nothing Then
            hlnkPrint.NavigateUrl = "ReportViewerStudentBio.aspx?ActionType=std&id=" & Request("std")
            dt = ctlStd.GetStudent_ByID(Request("std"))
        Else
            hlnkPrint.NavigateUrl = "ReportViewerStudentBio.aspx?ActionType=std&id=" & Request.Cookies("PersonID").Value
            dt = ctlStd.GetStudent_ByID(Request.Cookies("PersonID").Value)
        End If

        ClearData()
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)

                hlnkBack.NavigateUrl = "StudentReg.aspx?m=pm&p=101&std=" & String.Concat(.Item("StudentID"))
                hlnkPrint.NavigateUrl = "ReportViewerStudentBio.aspx?ActionType=std&id=" & String.Concat(.Item("StudentID"))

                hdStudentID.Value = String.Concat(.Item("StudentID"))
                lblStdCode.Text = .Item("StudentCode")

                If Not IsDBNull(.Item("PrefixName")) Then
                    lblPrefix.Text = .Item("PrefixName")
                End If

                lblFirstName.Text = DBNull2Str(.Item("FirstName"))
                lblLastName.Text = DBNull2Str(.Item("LastName"))
                lblNickName.Text = DBNull2Str(.Item("NickName"))

                If String.Concat(.Item("Gender")) = "M" Then
                    lblGender.Text = "ชาย"
                ElseIf String.Concat(.Item("Gender")) = "F" Then

                    lblGender.Text = "หญิง"
                End If

                lblEmail.Text = DBNull2Str(.Item("Email"))
                lblTelephone.Text = DBNull2Str(.Item("Telephone"))
                lblMobile.Text = DBNull2Str(.Item("MobilePhone"))


                If DBNull2Str(.Item("BirthDate")) <> "" Then
                    lblBirthDate.Text = DisplayStr2DateTH(.Item("BirthDate"))
                End If

                If Not IsDBNull(.Item("BloodGroup")) Then
                    lblBloodGroup.Text = .Item("BloodGroup")
                End If

                lblMajorName.Text = String.Concat(.Item("MajorName"))
                lblAdvisorName.Text = String.Concat(.Item("Ad_PrefixName")) & String.Concat(.Item("Ad_Fname")) & " " & String.Concat(.Item("Ad_Lname"))
                lblFather_FirstName.Text = DBNull2Str(.Item("Father_FirstName"))
                lblFather_LastName.Text = DBNull2Str(.Item("Father_LastName"))
                lblFather_Career.Text = DBNull2Str(.Item("Father_Career"))
                lblFather_Tel.Text = DBNull2Str(.Item("Father_Tel"))
                lblMother_FirstName.Text = DBNull2Str(.Item("Mother_FirstName"))
                lblMother_LastName.Text = DBNull2Str(.Item("Mother_LastName"))
                lblMother_Career.Text = DBNull2Str(.Item("Mother_Career"))
                lblMother_Tel.Text = DBNull2Str(.Item("Mother_Tel"))
                lblSibling.Text = DBNull2Str(.Item("Sibling"))
                lblChildNo.Text = DBNull2Str(.Item("ChildNo"))

                lblAddressLive.Text = DBNull2Str(.Item("AddressLive"))
                lblTelLive.Text = DBNull2Str(.Item("TelLive"))

                lblAddress.Text = DBNull2Str(.Item("Address"))
                lblDistrict.Text = DBNull2Str(.Item("District"))
                lblCity.Text = DBNull2Str(.Item("City"))

                If Not IsDBNull(.Item("ProvinceID")) Then
                    lblProvince.Text = String.Concat(.Item("ProvinceName"))
                End If

                lblZipCode.Text = DBNull2Str(.Item("ZipCode"))
                lblCongenitalDisease.Text = DBNull2Str(.Item("CongenitalDisease"))
                lblMedicineUsually.Text = DBNull2Str(.Item("MedicineUsually"))
                lblMedicalHistory.Text = DBNull2Str(.Item("MedicalHistory"))
                lblHobby.Text = DBNull2Str(.Item("Hobby"))
                lblTalent.Text = DBNull2Str(.Item("Talent"))
                lblExpectations.Text = DBNull2Str(.Item("Expectations"))
                lblPrimarySchool.Text = DBNull2Str(.Item("PrimarySchool"))
                lblPrimaryProvince.Text = DBNull2Str(.Item("PrimaryProvince"))

                lblPrimaryYear.Text = Zero2StrNull(.Item("PrimaryYear"))

                lblSecondarySchool.Text = DBNull2Str(.Item("SecondarySchool"))
                lblSecondaryProvince.Text = DBNull2Str(.Item("SecondaryProvince"))
                lblSecondaryYear.Text = Zero2StrNull(.Item("SecondaryYear"))
                lblHighSchool.Text = DBNull2Str(.Item("HighSchool"))
                lblHighProvince.Text = DBNull2Str(.Item("HighProvince"))
                lblHighYear.Text = Zero2StrNull(.Item("HighYear"))
                lblContactName.Text = DBNull2Str(.Item("ContactName"))
                lblContactAddress.Text = DBNull2Str(.Item("ContactAddress"))
                lblContactTel.Text = DBNull2Str(.Item("ContactTel"))

                lblGPAX.Text = DBNull2Str(.Item("GPAX"))

                lblContactRelation.Text = DBNull2Str(.Item("ContactRelation"))


                If DBNull2Str(.Item("PicturePath")) <> "" Then
                    Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & StudentPic & "/" & .Item("PicturePath")))

                    If objfile.Exists Then
                        picStudent.ImageUrl = "~/" & StudentPic & "/" & .Item("PicturePath")
                    Else
                        picStudent.ImageUrl = "~/" & StudentPic & "/" & String.Concat(.Item("Gender")) & ".jpg"
                    End If
                Else
                    picStudent.ImageUrl = "~/" & StudentPic & "/" & String.Concat(.Item("Gender")) & ".jpg"
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
                    hlnkCV.Visible = True
                Else
                    hlnkCV.Visible = False
                End If
                If String.Concat(.Item("LineID")) <> "" Then
                    hplLineID.Visible = True
                    hplLineID.ToolTip = String.Concat(.Item("LineID"))
                    hplLineID.NavigateUrl = "line://ti/p/" & String.Concat(.Item("LineID"))
                Else
                    hplLineID.Visible = False
                End If

                lblBank.Text = String.Concat(.Item("BankName"))
                lblAccount.Text = String.Concat(.Item("BankAccount"))
                lblWorkPlace.Text = String.Concat(.Item("WorkPlace"))
                lblSize.Text = String.Concat(.Item("GownSize"))
                lblCovid.Text = String.Concat(.Item("CovidInsurance"))


                LoadEducation()

            End With
        Else

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่พบข้อมูล');", True)
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

        lblAdvisorName.Text = ""
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