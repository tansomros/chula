Imports System.IO
Public Class PersonalInfo
    Inherits System.Web.UI.Page
    Dim ctlP As New PersonController
    Dim ctlM As New MasterController
    Dim dt As New DataTable
    Dim sAlert As String
    Dim isValid As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("../Login.aspx?logout=y")
        End If
        If Not IsPostBack Then
            pnAlert.Visible = False

            lblUID.Text = ""
            Session("eduuid") = 0
            ctlP.PersonEducation_DeleteQ(Session("userid"))
            chkStatus.Checked = True
            optPersonType.SelectedIndex = 0
            ddlDepartment.Visible = False

            LoadPrefix()
            LoadPrefixPosition()
            LoadPosition()
            LoadMajor()
            LoadDepartment()

            If Not Request("pid") Is Nothing Then
                EditData(Request("pid"))
            End If
        End If
    End Sub


    Private Sub LoadPrefix()
        dt = ctlM.Prefix_Get
        ddlPrefix.DataSource = dt
        ddlPrefix.DataValueField = "PrefixID"
        ddlPrefix.DataTextField = "PrefixName_TH"
        ddlPrefix.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadPrefixPosition()
        dt = ctlM.PrefixPosition_Get
        ddlPositionPrefix.DataSource = dt
        ddlPositionPrefix.DataValueField = "UID"
        ddlPositionPrefix.DataTextField = "Name_TH"
        ddlPositionPrefix.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadPosition()
        dt = ctlM.Position_GetByGroup(optPersonType.SelectedValue)
        ddlPosition.DataSource = dt
        ddlPosition.DataValueField = "PositionCode"
        ddlPosition.DataTextField = "PositionName"
        ddlPosition.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadMajor()
        dt = ctlM.Major_Get
        ddlMajor.DataSource = dt
        ddlMajor.DataValueField = "UID"
        ddlMajor.DataTextField = "NameTH"
        ddlMajor.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadDepartment()
        dt = ctlM.Department_Get
        ddlDepartment.DataSource = dt
        ddlDepartment.DataValueField = "UID"
        ddlDepartment.DataTextField = "NameTH"
        ddlDepartment.DataBind()
        dt = Nothing
    End Sub


    Private Sub EditData(ByVal pcode As Integer)
        Dim dtPs As New DataTable
        dtPs = ctlP.Person_GetByID(pcode)
        If dtPs.Rows.Count > 0 Then
            With dtPs.Rows(0)
                isAdd = False
                Me.lblUID.Text = DBNull2Str(dtPs.Rows(0)("PersonID"))
                txtEmployeeCode.Text = DBNull2Str(dtPs.Rows(0)("EmployeeCode"))
                optPersonType.SelectedValue = DBNull2Str(dtPs.Rows(0)("PersonType"))
                SwithPersonType()

                ddlPrefix.SelectedValue = DBNull2Zero(dtPs.Rows(0)("PrefixID"))
                ddlPosition.SelectedValue = DBNull2Str(dtPs.Rows(0)("PositionID"))
                ddlPositionPrefix.SelectedValue = DBNull2Str(dtPs.Rows(0)("PrefixPositionID"))

                txtEducated.Text = DBNull2Str(dtPs.Rows(0)("EduName"))

                txtStartDate.Text = DBNull2Str(dtPs.Rows(0)("StartWorkDate"))
                txtFnameTH.Text = DBNull2Str(dtPs.Rows(0)("Fname"))
                txtFnameEN.Text = DBNull2Str(dtPs.Rows(0)("Fname_EN"))
                txtLnameTH.Text = DBNull2Str(dtPs.Rows(0)("Lname"))
                txtLnameEN.Text = DBNull2Str(dtPs.Rows(0)("Lname_EN"))
                txtTel.Text = DBNull2Str(dtPs.Rows(0)("Contact"))
                txtMobile.Text = DBNull2Str(dtPs.Rows(0)("Tel"))
                txtMail.Text = DBNull2Str(dtPs.Rows(0)("Email"))
                ddlMajor.SelectedValue = DBNull2Str(dtPs.Rows(0)("MajorID"))
                ddlDepartment.SelectedValue = DBNull2Str(dtPs.Rows(0)("DepartmentUID"))

                If DBNull2Str(dtPs.Rows(0).Item("ImagePath")) <> "" Then
                    Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & PersonPic & "/" & dtPs.Rows(0).Item("ImagePath")))

                    If objfile.Exists Then
                        imgPerson.ImageUrl = "~/" & PersonPic & "/" & dtPs.Rows(0).Item("ImagePath")
                    Else
                        imgPerson.ImageUrl = "~/" & PersonPic & "/nopic.jpg"
                    End If

                End If

                chkStatus.Checked = ConvertStatusFlag2CHK(dtPs.Rows(0)("StatusFlag"))

                If DBNull2Zero(dtPs.Rows(0)("PersonType")) = 1 Then
                    pnCV.Visible = True
                    LoadPersonWork()
                Else
                    pnCV.Visible = False
                End If
            End With
            LoadEducation()
        End If
        dt = Nothing
    End Sub
    Private Sub LoadPersonWork()
        Dim dtW As New DataTable

        dtW = ctlP.PersonWork_GetByID(StrNull2Zero(lblUID.Text))
        If dtW.Rows.Count > 0 Then
            With dtW.Rows(0)

                txtProfessional_Research_Pub.Text = String.Concat(.Item("Professional_Research_Pub"))

                txtProfessional_Research_Present.Text = String.Concat(.Item("Professional_Research_Present"))
                txtMedical_Pub.Text = String.Concat(.Item("Medical_Pub"))
                txtMedical_Present.Text = String.Concat(.Item("Medical_Present"))
                txtAward.Text = String.Concat(.Item("Award"))
                txtProfessional_Training_In.Text = String.Concat(.Item("Professional_Training_In"))
                txtProfessional_Training_Out.Text = String.Concat(.Item("Professional_Training_Out"))
                txtMedical_Training_In1.Text = String.Concat(.Item("Medical_Training_In1"))
                txtMedical_Training_In2.Text = String.Concat(.Item("Medical_Training_In2"))
                txtMedical_Training_In3.Text = String.Concat(.Item("Medical_Training_In3"))
                txtMedical_Training_In4.Text = String.Concat(.Item("Medical_Training_In4"))
                txtMedical_Training_Out1.Text = String.Concat(.Item("Medical_Training_Out1"))
                txtMedical_Training_Out2.Text = String.Concat(.Item("Medical_Training_Out2"))
                txtMedical_Training_Out3.Text = String.Concat(.Item("Medical_Training_Out3"))
                txtMedical_Training_Out4.Text = String.Concat(.Item("Medical_Training_Out4"))


                chkQA_Training.ClearSelection()
                Dim sQA() As String = Split(DBNull2Str(.Item("QA_Training")), "|")
                For i = 0 To sQA.Length - 1
                    Select Case sQA(i)
                        Case "QA1"
                            chkQA_Training.Items(0).Selected = True
                        Case "QA2"
                            chkQA_Training.Items(1).Selected = True
                        Case "QA3"
                            chkQA_Training.Items(2).Selected = True
                        Case "QA4"
                            chkQA_Training.Items(3).Selected = True
                        Case "QA5"
                            chkQA_Training.Items(4).Selected = True
                    End Select
                Next

                txtQA_Training_Other.Text = String.Concat(.Item("QA_Training_Other"))
                txtCourse_Main.Text = String.Concat(.Item("Course_Main"))
                txtCourse_Coordinatior.Text = String.Concat(.Item("Course_Coordinator"))

            End With

        End If

    End Sub
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If ValidateData() = False Then
            pnAlert.Visible = True
            lblAlert.Text = sAlert
            Exit Sub
        Else
            pnAlert.Visible = False
        End If
        Dim StartDate As String
        If txtStartDate.Text = "" Then
            StartDate = ""
        Else
            StartDate = ConvertStrDate2InformDateString(txtStartDate.Text)
        End If


        ctlP.Person_Save(StrNull2Zero(lblUID.Text), txtEmployeeCode.Text, optPersonType.SelectedValue, StrNull2Zero(ddlPrefix.SelectedValue), txtFnameTH.Text, txtLnameTH.Text, txtFnameEN.Text, txtLnameEN.Text, StrNull2Zero(ddlPosition.SelectedValue), StrNull2Zero(ddlPositionPrefix.Text), StartDate, StrNull2Zero(ddlMajor.SelectedValue), StrNull2Zero(ddlDepartment.SelectedValue), ConvertBoolean2StatusFlag(chkStatus.Checked), Session("userid"), txtEducated.Text, txtTel.Text, txtMobile.Text, txtMail.Text)
        'ConvertBoolean2StatusFlag(chkStatus.Checked

        If lblUID.Text = "" Then
            Dim PersonUID As Integer
            PersonUID = ctlP.Person_GetPersonID(optPersonType.SelectedValue, txtEmployeeCode.Text, txtFnameTH.Text.Trim + txtLnameTH.Text.Trim, txtFnameEN.Text.Trim + txtLnameEN.Text.Trim)
            lblUID.Text = PersonUID

            ctlP.PersonEducation_ActiveStatusQ(PersonUID, txtFnameTH.Text.Trim() + txtLnameTH.Text.Trim())
        End If

        If FileUpload1.HasFile Then
            ctlP.Person_UpdateFileName(StrNull2Zero(lblUID.Text), lblUID.Text & Path.GetExtension(FileUpload1.PostedFile.FileName))
            UploadFile(FileUpload1, lblUID.Text & Path.GetExtension(FileUpload1.PostedFile.FileName))
        End If

        If optPersonType.SelectedValue = 1 Then
            Dim QA_Training As String = ""
            For i = 0 To chkQA_Training.Items.Count - 1
                If chkQA_Training.Items(i).Selected Then
                    QA_Training = QA_Training + chkQA_Training.Items(i).Value + "|"
                End If
            Next

            If QA_Training.Length > 0 Then
                QA_Training = Left(QA_Training, QA_Training.Length - 1)
            End If

            ctlP.PersonWork_Save(StrNull2Zero(lblUID.Text), txtProfessional_Research_Pub.Text,
        txtProfessional_Research_Present.Text,
        txtMedical_Pub.Text,
        txtMedical_Present.Text,
        txtAward.Text,
        txtProfessional_Training_In.Text,
        txtProfessional_Training_Out.Text,
        txtMedical_Training_In1.Text,
        txtMedical_Training_In2.Text,
        txtMedical_Training_In3.Text,
        txtMedical_Training_In4.Text,
        txtMedical_Training_Out1.Text,
        txtMedical_Training_Out2.Text,
        txtMedical_Training_Out3.Text,
        txtMedical_Training_Out4.Text,
        QA_Training,
        txtQA_Training_Other.Text,
        txtCourse_Main.Text,
        txtCourse_Coordinatior.Text, Session("userid"))
        End If

        DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")

        Response.Redirect("#?pid=" & lblUID.Text)

    End Sub

    Sub UploadFile(ByVal Fileupload As Object, sName As String)
        Dim FileFullName As String = Fileupload.PostedFile.FileName
        Dim FileNameInfo As String = Path.GetFileName(FileFullName)
        Dim filename As String = Path.GetFileName(Fileupload.PostedFile.FileName)
        Dim objfile As FileInfo = New FileInfo(Server.MapPath("../" & PersonPic & "/"))
        If FileNameInfo <> "" Then
            'Fileupload.PostedFile.SaveAs(objfile.DirectoryName & "\" & filename)
            Fileupload.PostedFile.SaveAs(objfile.DirectoryName & "\" & sName)
        End If
        objfile = Nothing
    End Sub

    Function ValidateData() As Boolean
        sAlert = "กรุณาตรวจสอบ <br />"
        isValid = True

        'If txtStartDate.Text = "" Then
        '    sAlert &= "- ระบุวันที่ให้ครบถ้วน <br/>"
        '    isValid = False
        'End If

        'If Not CheckDateValid(txtStartDate.Text) Then
        '    sAlert &= "- ตรวจสอบวันที่ให้ถูกต้อง <br/>"
        '    isValid = False
        'End If


        'If txtPresentType.Text = "" Then
        '    sAlert &= "- ประเภทผลงาน <br/>"
        '    isValid = False
        'End If


        If txtFnameTH.Text.Trim = "" Then
            sAlert &= "- ชื่อ <br/>"
            isValid = False

        End If
        If txtLnameTH.Text.Trim = "" Then
            sAlert &= "- นามสกุล <br/>"
            isValid = False
        End If

        Return isValid

    End Function


    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearData()
    End Sub
    Private Sub ClearData()
        lblUID.Text = ""
        txtFnameTH.Text = ""
        txtFnameEN.Text = ""
        txtLnameTH.Text = ""
        txtLnameEN.Text = ""

        chkStatus.Checked = True


        txtProfessional_Research_Pub.Text = ""
        txtProfessional_Research_Present.Text = ""
        txtMedical_Pub.Text = ""
        txtMedical_Present.Text = ""
        txtAward.Text = ""
        txtProfessional_Training_In.Text = ""
        txtProfessional_Training_Out.Text = ""
        txtMedical_Training_In1.Text = ""
        txtMedical_Training_In2.Text = ""
        txtMedical_Training_In3.Text = ""
        txtMedical_Training_In4.Text = ""
        txtMedical_Training_Out1.Text = ""
        txtMedical_Training_Out2.Text = ""
        txtMedical_Training_Out3.Text = ""
        txtMedical_Training_Out4.Text = ""
        chkQA_Training.ClearSelection()
        txtQA_Training_Other.Text = ""
        txtCourse_Main.Text = ""
        txtCourse_Coordinatior.Text = ""

    End Sub

    Protected Sub cmdAddEdu_Click(sender As Object, e As EventArgs) Handles cmdAddEdu.Click
        If txtFnameTH.Text.Trim() = "" And txtLnameTH.Text.Trim() = "" Then
            DisplayMessage(Me.Page, "กรุณากรอกชื่อ-นามสกุลให้ครบถ้วนก่อน")
            Exit Sub
        End If

        ctlP.PersonEducation_Save(Session("eduuid"), StrNull2Zero(lblUID.Text), 0, txtDegreeTH.Text, txtDegreeEN.Text, txtMajorTH.Text, txtMajorEN.Text, txtUniversityTH.Text, txtUniversityEN.Text, txtEduYearTH.Text, txtEduYearEN.Text, Session("userid"), txtFnameTH.Text.Trim() + txtLnameTH.Text.Trim())

        LoadEducation()
        ClearEduData()
        Session("eduuid") = 0
    End Sub
    Private Sub ClearEduData()
        txtDegreeTH.Text = ""
        txtDegreeEN.Text = ""
        txtMajorEN.Text = ""
        txtMajorTH.Text = ""
        txtUniversityEN.Text = ""
        txtUniversityTH.Text = ""
        txtEduYearEN.Text = ""
        txtEduYearTH.Text = ""
        Session("eduuid") = 0
        cmdAddEdu.Text = "เพิ่ม"
    End Sub

    Private Sub LoadEducation()
        dt = ctlP.PersonEducation_Get(StrNull2Zero(lblUID.Text))
        grdEdu.DataSource = dt
        grdEdu.DataBind()
    End Sub

    Protected Sub grdEdu_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdEdu.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(5).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#c1ffc2';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub grdEdu_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdEdu.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditEducation(e.CommandArgument())
                Case "imgDel"
                    ctlP.PersonEducation_Delete(e.CommandArgument())
                    LoadEducation()
                    Session("eduuid") = 0
            End Select


        End If
    End Sub
    Private Sub EditEducation(UID As Integer)
        dt = ctlP.PersonEducation_GetByUID(UID)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                Session("eduuid") = .Item("UID")
                txtDegreeTH.Text = String.Concat(.Item("DegreeTH"))
                txtDegreeEN.Text = String.Concat(.Item("DegreeEN"))
                txtMajorEN.Text = String.Concat(.Item("MajorEN"))
                txtMajorTH.Text = String.Concat(.Item("MajorTH"))
                txtUniversityEN.Text = .Item("UniversityEN")
                txtUniversityTH.Text = .Item("UniversityTH")
                txtEduYearEN.Text = String.Concat(.Item("YearEN"))
                txtEduYearTH.Text = String.Concat(.Item("YearTH"))

                cmdAddEdu.Text = "บันทึก"

            End With
        End If


    End Sub

    Protected Sub optPersonType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optPersonType.SelectedIndexChanged
        SwithPersonType()
    End Sub

    Private Sub SwithPersonType()
        If DBNull2Zero(optPersonType.SelectedValue) = 1 Then
            pnCV.Visible = True
            lblMajor_Label.Text = "สาขาวิชา"
            ddlMajor.Visible = True
            ddlDepartment.Visible = False
            'ddlDepartment.SelectedIndex = 0
            lblPositionPrefix_Label.Visible = True
            ddlPositionPrefix.Visible = True

        Else
            pnCV.Visible = False
            lblMajor_Label.Text = "ส่วนงาน"
            ddlDepartment.Visible = True
            ddlMajor.Visible = False
            lblPositionPrefix_Label.Visible = False
            ddlPositionPrefix.Visible = False

            'ddlMajor.SelectedIndex = 0
        End If

        LoadPosition()

    End Sub

    Protected Sub cmdSavePicture_Click(sender As Object, e As EventArgs) Handles cmdSavePicture.Click
        If FileUpload1.HasFile Then
            ctlP.Person_UpdateFileName(StrNull2Zero(lblUID.Text), lblUID.Text & Path.GetExtension(FileUpload1.PostedFile.FileName))
            UploadFile(FileUpload1, lblUID.Text & Path.GetExtension(FileUpload1.PostedFile.FileName))
        End If

        Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & PersonPic & "/" & lblUID.Text & ".jpg"))

        If objfile.Exists Then
            imgPerson.ImageUrl = "~/" & PersonPic & "/" & lblUID.Text & ".jpg"
            lblNoSuccess.Visible = False
        Else
            'imgPerson.ImageUrl = "~/" & PersonPic & "/nopic.jpg"
            lblNoSuccess.Visible = True
        End If

    End Sub
End Class