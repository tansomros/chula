Imports System.Drawing
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Internal
Public Class PersonInfo
    Inherits System.Web.UI.Page
    Dim ctlP As New PersonController
    Dim ctlM As New MasterController
    Dim ctlR As New ResearchController
    Dim ctlT As New TrainingController

    Dim dt As New DataTable

    Dim dtR As New DataTable
    Dim dtT As New DataTable

    Dim sAlert As String
    Dim isValid As Boolean
    Private Const UploadDirectory As String = "~/images/persons/"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            pnAlert.Visible = False

            lblPersonID.Text = ""
            hdfEduID.Value = 0
            ctlP.PersonEducation_DeleteQ(Request.Cookies("UserLoginID").Value)

            ddlDepartment.Visible = False

            LoadPrefix()
            LoadPrefixPosition()
            LoadPosition()
            LoadMajor()
            LoadDepartment()

            LoadResearchType()

            If Not Request("pid") Is Nothing Then
                If Request("pid") = "00" Then
                    EditData(Session("pid"))
                Else
                    EditData(Request("pid"))
                End If

            End If

            If Session("RoleID") <> 0 Then
                'ddlDepartment.Visible = False
                'ddlMajor.Visible = False
                ddlPositionPrefix.Visible = False
                ddlPosition.Visible = False

                lblMajorName.Visible = True
                lblPositionEduName.Visible = True
                lblPositionName.Visible = True

            Else
                'ddlDepartment.Visible = True
                'ddlMajor.Visible = True
                ddlPositionPrefix.Visible = True
                ddlPosition.Visible = True
                lblMajorName.Visible = False
                lblPositionEduName.Visible = False
                lblPositionName.Visible = False

            End If

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
    Private Sub LoadPrefixPosition()
        dt = ctlM.PrefixPosition_Get
        ddlPositionPrefix.DataSource = dt
        ddlPositionPrefix.DataValueField = "UID"
        ddlPositionPrefix.DataTextField = "Name_TH"
        ddlPositionPrefix.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadPosition()
        dt = ctlM.Position_GetByGroup(0)
        ddlPosition.DataSource = dt
        ddlPosition.DataValueField = "PositionID"
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
        ddlDepartment.DataValueField = "DepartmentUID"
        ddlDepartment.DataTextField = "DepartmentName"
        ddlDepartment.DataBind()
        dt = Nothing
    End Sub


    Private Sub EditData(ByVal pcode As Integer)
        Dim dtPs As New DataTable
        dtPs = ctlP.Person_GetByID(pcode)
        If dtPs.Rows.Count > 0 Then
            With dtPs.Rows(0)
                isAdd = False
                Me.lblPersonID.Text = DBNull2Str(dtPs.Rows(0)("PersonID"))
                txtEmployeeCode.Text = DBNull2Str(dtPs.Rows(0)("EmployeeCode"))


                ddlPrefix.SelectedValue = DBNull2Zero(dtPs.Rows(0)("PrefixID"))
                ddlPosition.SelectedValue = DBNull2Str(dtPs.Rows(0)("PositionID"))
                Session("pos") = DBNull2Str(dtPs.Rows(0)("PositionID"))
                Session("ptype") = DBNull2Str(dtPs.Rows(0)("PersonType"))
                ddlPositionPrefix.SelectedValue = DBNull2Str(dtPs.Rows(0)("PrefixPositionID"))

                txtEducated.Text = DBNull2Str(dtPs.Rows(0)("EducateName"))

                'txtStartDate.Text = DBNull2Str(dtPs.Rows(0)("StartWorkDate"))
                txtFnameTH.Text = DBNull2Str(dtPs.Rows(0)("Fname"))
                txtFnameEN.Text = DBNull2Str(dtPs.Rows(0)("Fname_EN"))
                txtLnameTH.Text = DBNull2Str(dtPs.Rows(0)("Lname"))
                txtLnameEN.Text = DBNull2Str(dtPs.Rows(0)("Lname_EN"))
                txtTel.Text = DBNull2Str(dtPs.Rows(0)("Contact"))

                txtMail.Text = DBNull2Str(dtPs.Rows(0)("Email"))
                ddlMajor.SelectedValue = DBNull2Str(dtPs.Rows(0)("MajorID"))
                ddlDepartment.SelectedValue = DBNull2Str(dtPs.Rows(0)("DepartmentUID"))

                ddlWorkStatus.SelectedValue = DBNull2Str(dtPs.Rows(0)("WorkStatus"))

                lblPositionEduName.Text = ddlPositionPrefix.SelectedItem.Text
                lblPositionName.Text = ddlPosition.SelectedItem.Text

                Session("personimg") = dtPs.Rows(0).Item("ImagePath")

                'If DBNull2Str(dtPs.Rows(0).Item("ImagePath")) <> "" Then
                '    Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & PersonPic & "/" & dtPs.Rows(0).Item("ImagePath")))

                '    'If objfile.Exists Then
                '    '    uploadedImage.ImageUrl = "~/" & PersonPic & "/" & dtPs.Rows(0).Item("ImagePath")
                '    'Else
                '    '    uploadedImage.ImageUrl = "~/" & PersonPic & "/nopic.jpg"

                '    'End If

                'End If

                chkExclusive.Checked = ConvertYN2Boolean(String.Concat(dtPs.Rows(0)("isExecutiveDirector")))
                chkFaculty.Checked = ConvertYN2Boolean(String.Concat(dtPs.Rows(0)("isFaculty")))
                chkTeacher.Checked = ConvertYN2Boolean(String.Concat(dtPs.Rows(0)("isTeacher")))
                chkStaff.Checked = ConvertYN2Boolean(String.Concat(dtPs.Rows(0)("isStaff")))
                chkAlumni.Checked = ConvertYN2Boolean(String.Concat(dtPs.Rows(0)("isAlumni")))

                chkStatus.Checked = ConvertStatusFlag2CHK(dtPs.Rows(0)("StatusFlag"))

                If DBNull2Zero(dtPs.Rows(0)("PersonType")) = 1 Then
                    'pnCV.Visible = True
                    lblMajorName.Text = ddlMajor.SelectedItem.Text

                    LoadPersonWork()


                Else
                    'pnCV.Visible = False
                    lblMajorName.Text = ddlDepartment.SelectedItem.Text
                End If
            End With
            LoadEducation()
            LoadResearch()
            LoadTraining()
        End If
        dt = Nothing
    End Sub
    Private Sub LoadPersonWork()
        Dim dtW As New DataTable

        dtW = ctlP.PersonWork_GetByID(StrNull2Zero(lblPersonID.Text))
        If dtW.Rows.Count > 0 Then
            With dtW.Rows(0)

                'txtProfessional_Research_Pub.Text = String.Concat(.Item("Professional_Research_Pub"))

                'txtProfessional_Research_Present.Text = String.Concat(.Item("Professional_Research_Present"))
                'txtMedical_Pub.Text = String.Concat(.Item("Medical_Pub"))
                'txtMedical_Present.Text = String.Concat(.Item("Medical_Present"))
                txtAward.Text = String.Concat(.Item("Award"))
                'txtProfessional_Training_In.Text = String.Concat(.Item("Professional_Training_In"))
                'txtProfessional_Training_Out.Text = String.Concat(.Item("Professional_Training_Out"))
                'txtMedical_Training_In1.Text = String.Concat(.Item("Medical_Training_In1"))
                'txtMedical_Training_In2.Text = String.Concat(.Item("Medical_Training_In2"))
                'txtMedical_Training_In3.Text = String.Concat(.Item("Medical_Training_In3"))
                'txtMedical_Training_In4.Text = String.Concat(.Item("Medical_Training_In4"))
                'txtMedical_Training_Out1.Text = String.Concat(.Item("Medical_Training_Out1"))
                'txtMedical_Training_Out2.Text = String.Concat(.Item("Medical_Training_Out2"))
                'txtMedical_Training_Out3.Text = String.Concat(.Item("Medical_Training_Out3"))
                'txtMedical_Training_Out4.Text = String.Concat(.Item("Medical_Training_Out4"))


                'chkQA_Training.ClearSelection()
                'Dim sQA() As String = Split(DBNull2Str(.Item("QA_Training")), "|")
                'For i = 0 To sQA.Length - 1
                '    Select Case sQA(i)
                '        Case "QA1"
                '            chkQA_Training.Items(0).Selected = True
                '        Case "QA2"
                '            chkQA_Training.Items(1).Selected = True
                '        Case "QA3"
                '            chkQA_Training.Items(2).Selected = True
                '        Case "QA4"
                '            chkQA_Training.Items(3).Selected = True
                '        Case "QA5"
                '            chkQA_Training.Items(4).Selected = True
                '    End Select
                'Next


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
        'Dim StartDate As String
        'If txtStartDate.Text = "" Then
        '    StartDate = ""
        'Else
        '    StartDate = ConvertStrDate2InformDateString(txtStartDate.Text)
        'End If

        ctlP.Person_Save(StrNull2Zero(lblPersonID.Text), txtEmployeeCode.Text, StrNull2Zero(ddlPrefix.SelectedValue), txtFnameTH.Text, txtLnameTH.Text, txtFnameEN.Text, txtLnameEN.Text, "", txtTel.Text, txtMail.Text, StrNull2Zero(ddlPosition.SelectedValue), StrNull2Zero(ddlDepartment.SelectedValue), StrNull2Zero(ddlMajor.SelectedValue), ddlPositionPrefix.SelectedValue, txtEducated.Text, ddlWorkStatus.SelectedValue, ConvertBoolean2YN(chkExclusive.Checked), ConvertBoolean2YN(chkFaculty.Checked), ConvertBoolean2YN(chkTeacher.Checked), ConvertBoolean2YN(chkStaff.Checked), ConvertBoolean2YN(chkAlumni.Checked), ConvertBoolean2StatusFlag(chkStatus.Checked), Request.Cookies("UserLoginID").Value, txtWorkplace.Text)

        If lblPersonID.Text = "" Then
            Dim PersonUID As Integer
            PersonUID = ctlP.Person_GetPersonID("", txtEmployeeCode.Text, txtFnameTH.Text.Trim + txtLnameTH.Text.Trim, txtFnameEN.Text.Trim + txtLnameEN.Text.Trim)
            lblPersonID.Text = PersonUID

            ctlP.PersonEducation_ActiveStatusQ(PersonUID, txtFnameTH.Text.Trim() + txtLnameTH.Text.Trim())
        End If

        If Not Session("picname") Is Nothing And Session("picname")<>"" Then
            ctlP.Person_UpdateFileName(StrNull2Zero(lblPersonID.Text), lblPersonID.Text & Path.GetExtension(Session("picname")))
            'UploadFile(FileUpload1, lblPersonID.Text & Path.GetExtension(FileUpload1.PostedFile.FileName))

            RenamePictureName()
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)

    End Sub
    Private Sub RenamePictureName()
        Dim Path As String = Server.MapPath(UploadDirectory)
        Dim Fromfile As String = Path + Session("picname")
        Dim Tofile As String = Path + lblPersonID.Text + ".jpg"

        Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & PersonPic & "/" & lblPersonID.Text + ".jpg"))

        If objfile.Exists Then
            objfile.Delete()
        End If

        File.Move(Fromfile, Tofile)

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
        lblPersonID.Text = ""
        txtFnameTH.Text = ""
        txtFnameEN.Text = ""
        txtLnameTH.Text = ""
        txtLnameEN.Text = ""
        txtAward.Text = ""

    End Sub

    Protected Sub cmdAddEdu_Click(sender As Object, e As EventArgs) Handles cmdAddEdu.Click
        If txtFnameTH.Text.Trim() = "" And txtLnameTH.Text.Trim() = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณากรอกชื่อ-นามสกุลให้ครบถ้วนก่อน');", True)
            Exit Sub
        End If

        ctlP.PersonEducation_Save(StrNull2Zero(hdfEduID.Value), StrNull2Zero(lblPersonID.Text), 0, txtDegreeTH.Text, txtDegreeEN.Text, txtMajorTH.Text, txtMajorEN.Text, txtUniversityTH.Text, txtUniversityEN.Text, txtEduYearTH.Text, txtEduYearEN.Text, Request.Cookies("UserLoginID").Value, txtFnameTH.Text.Trim() + txtLnameTH.Text.Trim())

        LoadEducation()
        ClearEduData()
        hdfEduID.Value = 0
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
        hdfEduID.Value = 0
        cmdAddEdu.Text = "เพิ่ม"
    End Sub

    Private Sub ClearTrainingData()
        txtTrainYear.Text = ""
        txtTrainTopic.Text = ""
        hdfTrainUID.Value = 0
        cmdSaveTrain.Text = "เพิ่ม"

    End Sub

    Private Sub ClearResearchData()
        txtRYear.Text = ""
        'ddlResearchType.SelectedIndex = 0
        'ddlResearchGroup.SelectedIndex = 0
        'ddlResearchPresented.SelectedIndex = 0
        'txtRTitleEN.Text = ""
        txtRTitle.Text = ""
        'txtRAbstract.Text = ""
        hdfResearchID.Value = 0
        cmdSaveResearch.Text = "เพิ่ม"
    End Sub

    Private Sub LoadResearchType()
        Dim ctlRf As New ReferenceValueController
        dt = ctlRf.ReferenceValue_GetByDomainCode("RESTYPE")
        ddlResearchType.DataSource = dt
        ddlResearchType.DataValueField = "Code"
        ddlResearchType.DataTextField = "Descriptions"
        ddlResearchType.DataBind()
    End Sub

    Private Sub LoadEducation()
        dt = ctlP.PersonEducation_Get(StrNull2Zero(lblPersonID.Text))
        grdEdu.DataSource = dt
        grdEdu.DataBind()
    End Sub

    Private Sub LoadResearch()
        dtR = ctlR.Research_GetByPersonID(StrNull2Zero(lblPersonID.Text))
        grdResearch.DataSource = dtR
        grdResearch.DataBind()
    End Sub

    Private Sub LoadTraining()
        dtT = ctlT.Training_GetByPersonID(StrNull2Zero(lblPersonID.Text))
        grdTrain.DataSource = dtT
        grdTrain.DataBind()
    End Sub

    Protected Sub grdEdu_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdEdu.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As System.Web.UI.WebControls.Image = e.Row.Cells(5).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#e0f3ff';")
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
                    hdfEduID.Value = 0
            End Select


        End If
    End Sub
    Private Sub EditEducation(UID As Integer)
        dt = ctlP.PersonEducation_GetByUID(UID)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdfEduID.Value = .Item("UID")
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
    Private Sub EditResearch(UID As Integer)
        dt = ctlR.Research_GetByUID(UID)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdfResearchID.Value = .Item("UID")
                txtRYear.Text = String.Concat(.Item("RYear"))
                ddlResearchType.SelectedValue = String.Concat(.Item("RESTYPEUID"))
                ddlResearchGroup.SelectedValue = String.Concat(.Item("RESGRPUID"))
                ddlResearchPresented.SelectedValue = String.Concat(.Item("PRESENTUID"))
                'txtRTitleEN.Text = String.Concat(.Item("TitleEN"))
                txtRTitle.Text = String.Concat(.Item("TitleTH"))
                'txtRAbstract.Text = String.Concat(.Item("Abstract"))

                If Convert.ToString(.Item("AttachFileName")) <> "" Then
                    hlnkAttachs.Text = .Item("AttachFileName").ToString()
                    hlnkAttachs.NavigateUrl = "../" + DocumentResearch + "/" + .Item("AttachFileName").ToString()
                    cmdDelFile.Visible = True
                Else
                    hlnkAttachs.Text = ""
                    cmdDelFile.Visible = False
                End If


                cmdSaveResearch.Text = "บันทึก"

            End With
        End If
        dt = Nothing
    End Sub
    Private Sub EditTrain(UID As Integer)
        dt = ctlT.Training_GetByUID(UID)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdfTrainUID.Value = .Item("UID")
                txtTrainYear.Text = String.Concat(.Item("RYear"))
                optTrainCate.SelectedValue = String.Concat(.Item("TRNCATEUID"))
                ddlTrainGroup.SelectedValue = String.Concat(.Item("TRNGRPUID"))
                ddlTrainType.SelectedValue = String.Concat(.Item("TRNTYPEUID"))
                txtTrainTopic.Text = String.Concat(.Item("TopicName"))
                cmdSaveTrain.Text = "บันทึก"

            End With
        End If
        dt = Nothing

    End Sub

    Protected Sub cmdDelFile_Click(sender As Object, e As EventArgs) Handles cmdDelFile.Click
        'ctlN.News_UpdateFileAttachName(StrNull2Zero(lblNewsID.Text), "")

        If chkFileExist(Server.MapPath("/" + DocumentResearch + hlnkAttachs.Text)) Then
            File.Delete(Server.MapPath("/" + DocumentResearch + hlnkAttachs.Text))
        End If

        hlnkAttachs.Text = ""
        cmdDelFile.Visible = False
    End Sub

    'Protected Sub cmdSavePicture_Click(sender As Object, e As EventArgs) Handles cmdSavePicture.Click
    '    If FileUpload1.HasFile Then
    '        ctlP.Person_UpdateFileName(StrNull2Zero(lblPersonID.Text), lblPersonID.Text & Path.GetExtension(FileUpload1.PostedFile.FileName))
    '        UploadFile(FileUpload1, lblPersonID.Text & Path.GetExtension(FileUpload1.PostedFile.FileName))
    '    End If

    '    Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & PersonPic & "/" & lblPersonID.Text & ".jpg"))

    '    If objfile.Exists Then
    '        imgPerson.ImageUrl = "~/" & PersonPic & "/" & lblPersonID.Text & ".jpg"
    '        lblNoSuccess.Visible = False
    '    Else
    '        'imgPerson.ImageUrl = "~/" & PersonPic & "/nopic.jpg"
    '        lblNoSuccess.Visible = True
    '    End If
    '    imgPerson.ImageUrl = "~/images/persons/" + Session("picname")

    'End Sub


    Protected Sub cmdSaveTrain_Click(sender As Object, e As EventArgs) Handles cmdSaveTrain.Click
        Dim tYear As Integer = StrNull2Zero(txtTrainYear.Text)

        If tYear > 2500 Then
            tYear = tYear - 543
        End If

        ctlT.Training_Save(StrNull2Zero(hdfTrainUID.Value), tYear, optTrainCate.SelectedValue, ddlTrainGroup.SelectedValue, ddlTrainType.SelectedValue, StrNull2Zero(lblPersonID.Text), txtTrainTopic.Text, "", "", "A")
        LoadTraining()
        ClearTrainingData()
    End Sub

    Protected Sub cmdSaveResearch_Click(sender As Object, e As EventArgs) Handles cmdSaveResearch.Click
        Dim RYear As Integer = StrNull2Zero(txtRYear.Text)

        If RYear > 2500 Then
            RYear = RYear - 543
        End If
        Dim AttachFileName As String = ""
        Dim UlFileName As String = ""
        If FileUploadFile.HasFile Then
            UlFileName = FileUploadFile.FileName
            AttachFileName = Path.GetFileName(UlFileName)
            FileUploadFile.SaveAs(Server.MapPath("~/" + DocumentResearch + UlFileName))
        Else
            AttachFileName = hlnkAttachs.Text
        End If


        ctlR.Research_Save(StrNull2Zero(hdfResearchID.Value), RYear, ddlResearchType.SelectedValue, ddlResearchGroup.SelectedValue, ddlResearchPresented.SelectedValue, StrNull2Zero(lblPersonID.Text), txtRTitle.Text, "", "A", AttachFileName)
        LoadResearch()
        ClearResearchData()
    End Sub

    Protected Sub grdTrain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdTrain.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As System.Web.UI.WebControls.Image = e.Row.Cells(5).FindControl("imgDelT")

            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#e0f3ff';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub grdTrain_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdTrain.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEditT"
                    EditTrain(e.CommandArgument())
                Case "imgDelT"
                    ctlT.Training_Delete(e.CommandArgument())
                    LoadTraining()
                    hdfTrainUID.Value = 0
            End Select


        End If
    End Sub

    Protected Sub grdResearch_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdResearch.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEditR"
                    EditResearch(e.CommandArgument())
                Case "imgDelR"
                    ctlR.research_Delete(e.CommandArgument())
                    LoadResearch()
                    hdfResearchID.Value = 0
            End Select
        End If
    End Sub

    Protected Sub grdResearch_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdResearch.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As System.Web.UI.WebControls.Image = e.Row.Cells(7).FindControl("imgDelR")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#e0f3ff';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub cmdResearchCancel_Click(sender As Object, e As EventArgs) Handles cmdResearchCancel.Click
        ClearResearchData()
    End Sub

    Protected Sub cmdTrainCancel_Click(sender As Object, e As EventArgs) Handles cmdTrainCancel.Click
        ClearTrainingData()
    End Sub

    Protected Sub UploadControl_FileUploadComplete(ByVal sender As Object, ByVal e As FileUploadCompleteEventArgs)

        If chkFileExist(Server.MapPath(UploadDirectory + Session("picname"))) Then
            File.Delete(Server.MapPath(UploadDirectory + Session("picname")))
        End If

        e.CallbackData = SavePostedFile(e.UploadedFile, UploadDirectory, Path.GetRandomFileName())
        Session("picname") = e.CallbackData
    End Sub
    Protected Function SavePostedFile(ByVal uploadedFile As UploadedFile, ByVal UploadPath As String, ByVal UploadFileName As String) As String
        If (Not uploadedFile.IsValid) Then
            Return String.Empty
        End If

        Dim fileName As String = Path.ChangeExtension(UploadFileName, ".jpg")

        Dim fullFileName As String = CombinePath(UploadPath, fileName)
        Using original As Image = Image.FromStream(uploadedFile.FileContent)
            Using thumbnail As Image = New ImageThumbnailCreator(original).CreateImageThumbnail(New Size(350, 350))
                ImageUtils.SaveToJpeg(CType(thumbnail, Bitmap), fullFileName)
            End Using
        End Using
        UploadingUtils.RemoveFileWithDelay(fileName, fullFileName, 5)
        Return fileName
    End Function
    Protected Function CombinePath(ByVal UploadPath As String, ByVal fileName As String) As String
        Return Path.Combine(Server.MapPath(UploadPath), fileName)
    End Function

End Class