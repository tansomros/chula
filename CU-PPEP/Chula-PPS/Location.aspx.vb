Imports DevExpress.Web
Imports DevExpress.Web.ASPxHtmlEditor.Internal
'Imports DevExpress.WebUtils
Public Class Location
    Inherits System.Web.UI.Page

    Dim ctlL As New LocationController
    Dim dt As New DataTable
    Dim ds As New DataSet
    Dim ctlP As New PersonController
    Dim acc As New UserController
    Dim ctlS As New SubjectController
    Dim ctlLS As New LocationSubjectController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If


        If Not IsPostBack Then
            'ApplyLayout(0)

            lblID.Text = ""
            ClearData()
            LoadProvinceToDDL()
            LoadLocationDepartmentToDDL()
            LoadLocationGroupToDDL()
            LoadZoneToDDL()
            LoadOfficeToDDL()
            LoadSubjectToDDL()
            LoadTeacherToDDL()

            If Request.Cookies("ROLE_PCT").Value = True And Request.Cookies("ROLE_ADM").Value <> True Then
                EditData(ctlP.Location_GetByPersonID(DBNull2Zero(Request.Cookies("ProfileID").Value)))
                chkStatus.Visible = False
                lblStatus.Visible = False
                chkStatus.Checked = True
            ElseIf Request.Cookies("ROLE_ADM").Value = True Then
                EditData(DBNull2Zero(Request("lid")))
                chkStatus.Visible = True
                lblStatus.Visible = True
            End If
            LoadPrefix()
            LoadLocationWork()
            LoadWorkDetail(StrNull2Zero(lblID.Text))
            LoadPreceptorToGrid()
            LoadSubjectToGrid()
            If Request.Cookies("ROLE_PCT").Value = True Then
                ddlPerson.Enabled = False
                grdPreceptor.Columns(0).Visible = False
                grdPreceptor.Columns(5).Visible = False
                grdPreceptor.Columns(6).Visible = False
                grdSubject.Columns(0).Visible = False
            End If
        End If
        txtPay.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        txtZipCode.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")

    End Sub
    Protected Sub grdPreceptor_CustomButtonCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomButtonCallbackEventArgs)
        If e.ButtonID = "DelPreceptor" Then
            ctlP.Person_Delete(grdPreceptor.GetRowValues(e.VisibleIndex, "PersonID"))
            LoadPreceptorToGrid()
        End If
    End Sub
    Protected Sub grdSubject_CustomButtonCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomButtonCallbackEventArgs)
        If e.ButtonID = "DelSubject" Then
            ctlLS.LocationSubject_Delete(grdSubject.GetRowValues(e.VisibleIndex, "UID"))
            LoadSubjectToGrid()
        End If
    End Sub
    Private Sub LoadProvinceToDDL()
        Dim ctlbase As New ApplicationBaseClass
        dt = ctlbase.Province_GetActive
        If dt.Rows.Count > 0 Then
            With ddlProvince
                .Enabled = True
                .DataSource = dt
                .DataTextField = "ProvinceName"
                .DataValueField = "ProvinceID"
                .DataBind()
                .SelectedIndex = 1
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadLocationGroupToDDL()

        Dim ctlL As New LocationGroupController
        dt = ctlL.LocationGroup_Get
        If dt.Rows.Count > 0 Then
            With ddlLocationGroup
                .Enabled = True
                .DataSource = dt
                .DataTextField = "Name"
                .DataValueField = "Code"
                .DataBind()
                .SelectedIndex = 0
                LoadGroupSelected()
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadTeacherToDDL()
        dt = ctlP.Person_GetByType("T")
        If dt.Rows.Count > 0 Then
            With ddlPerson
                .Enabled = True
                .DataSource = dt
                .DataTextField = "PersonName"
                .DataValueField = "PersonID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadZoneToDDL()
        Dim ctlZ As New LocationZoneController
        dt = ctlZ.LocationZone_GetWithBlank
        If dt.Rows.Count > 0 Then
            With ddlZone
                .Enabled = True
                .DataSource = dt
                .DataTextField = "ZoneName"
                .DataValueField = "ZoneID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadOfficeToDDL()
        Dim ctlZ As New LocationOfficeController
        dt = ctlZ.LocationOffice_GetWithBlank
        If dt.Rows.Count > 0 Then
            With ddlOfficeGroup
                .Enabled = True
                .DataSource = dt
                .DataTextField = "OfficeName"
                .DataValueField = "OfficeID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadLocationTypeToDDL()

        Dim ctlLT As New LocationTypeController
        ddlType.Items.Clear()
        dt = ctlLT.LocationType_ByGroupID(ddlLocationGroup.SelectedValue)
        If dt.Rows.Count > 0 Then
            With ddlType
                .Enabled = True
                .DataSource = dt
                .DataTextField = "Name"
                .DataValueField = "Code"
                .DataBind()
                .SelectedIndex = 0
            End With
            lblType.Visible = True
            ddlType.Visible = True

        Else
            lblType.Visible = False
            ddlType.Visible = False
            ddlType.DataSource = Nothing

        End If
        dt = Nothing
    End Sub

    Private Sub LoadLocationDepartmentToDDL()
        Dim ctlLT As New LocationDepartmentController
        dt = ctlLT.LocationDepartment_Get
        With ddlDepartment
            .Enabled = True
            .DataSource = dt
            .DataTextField = "Name"
            .DataValueField = "UID"
            .DataBind()
            .SelectedIndex = 0
        End With
        dt = Nothing
    End Sub

    Private Sub EditData(ByVal pID As String)
        Dim dtE As New DataTable

        dtE = ctlL.Location_GetByID(pID)
        Dim objList As New LocationInfo
        If dtE.Rows.Count > 0 Then
            With dtE.Rows(0)
                isAdd = False

                Me.lblID.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f00_LocationID).fldName))
                txtName.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f01_LocationName).fldName))

                ddlLocationGroup.SelectedValue = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f02_LocationGroupID).fldName))
                LoadGroupSelected()

                If ddlLocationGroup.SelectedValue = 1 Then
                    Try
                        ddlDepartment.SelectedValue = String.Concat(dtE.Rows(0)("DepartmentName"))
                    Catch ex As Exception
                        ddlDepartment.SelectedValue = "อื่นๆ"
                        ddlDepartment.Visible = True
                        txtDepartment.Visible = True
                        txtDepartment.Text = String.Concat(dtE.Rows(0)("DepartmentName"))
                    End Try

                End If

                If String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f03_LocationTypeID).fldName)) <> "0" Then
                    ddlType.SelectedValue = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f03_LocationTypeID).fldName))
                End If


                ddlDepartment.SelectedValue = DBNull2Zero(dtE.Rows(0)("DepartmentID"))
                txtDepartment.Text = String.Concat(dtE.Rows(0)("DepartmentName"))
                ddlLevel.SelectedValue = DBNull2Zero(dtE.Rows(0)("LevelID"))
                txtBed.Text = String.Concat(dtE.Rows(0)("Bed"))
                txtBranch.Text = String.Concat(dtE.Rows(0)("Branch"))

                txtLat.Text = String.Concat(dtE.Rows(0)("Lat"))
                txtLng.Text = String.Concat(dtE.Rows(0)("Lng"))

                txtAddress.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f06_Address).fldName))
                If DBNull2Zero(dtE.Rows(0)(objList.tblField(objList.fldPos.f07_ProvinceID).fldName)) = 99 Then
                    ddlProvince.Visible = False
                Else
                    ddlProvince.Visible = True
                    ddlProvince.SelectedValue = DBNull2Zero(dtE.Rows(0)(objList.tblField(objList.fldPos.f07_ProvinceID).fldName))
                End If

                Me.txtZipCode.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f09_ZipCode).fldName))
                txtStat.Text = String.Concat(dtE.Rows(0)("ProvinceName"))
                txtCountry.Text = String.Concat(dtE.Rows(0)("Country"))

                txtTel.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f10_Office_Tel).fldName))
                txtFax.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f11_Office_Fax).fldName))

                'txtCoName.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f12_Co_Name).fldName))
                'txtCoPosition.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f13_Co_Position).fldName))
                'txtCoTel.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f15_Co_Tel).fldName))
                'txtCoMail.Text = String.Concat(dtE.Rows(0)("Co_Mail"))

                'txtCuName.Text = String.Concat(dtE.Rows(0)("CU_Name"))
                ddlPerson.SelectedValue = String.Concat(dtE.Rows(0)("CU_Coordinator"))

                'txtCuPosition.Text = String.Concat(dtE.Rows(0)("CU_Position"))
                'txtCuTel.Text = String.Concat(dtE.Rows(0)("CU_Tel"))
                'txtCuMail.Text = String.Concat(dtE.Rows(0)("CU_Mail"))

                txtMail.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f14_Co_Mail).fldName))


                txtOfficeHour.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f16_Office_hours).fldName))
                txtOfficerNo.Text = String.Concat(dtE.Rows(0)(objList.tblField(objList.fldPos.f17_Officer_Count).fldName))

                chkStatus.Checked = CBool(dtE.Rows(0)(objList.tblField(objList.fldPos.f20_isPublic).fldName))
                optLang.SelectedValue = String.Concat(.Item("Lang"))

                txtReceiveName.Text = String.Concat(dtE.Rows(0)("Bill_Name"))
                lblAuto.Visible = False
                lblMode.Text = "Mode : Edit"

                optDay.SelectedValue = String.Concat(.Item("WorkDayID"))
                txtDay.Text = String.Concat(.Item("WorkDayDesc"))
                optTime.SelectedValue = String.Concat(.Item("WorkTimeID"))
                txtOfficeTime.Text = String.Concat(.Item("WorkTimeDesc"))

                CheckResidence(DBNull2Zero(.Item("HasResidence")))
                Select Case DBNull2Zero(.Item("HasResidence"))
                    Case 1
                        optResidence1.Checked = True
                    Case 2
                        optResidence2.Checked = True
                    Case 3
                        optResidence3.Checked = True
                End Select

                optIsPay.SelectedValue = DBNull2Zero(.Item("HasCost"))
                txtPay.Text = String.Concat(.Item("PayAmount"))
                optForce.SelectedValue = DBNull2Zero(.Item("isForce"))
                txtConfirm.Text = String.Concat(.Item("ConfirmHold"))

                txtApartmentName1.Text = String.Concat(.Item("ApartmentName1"))
                txtApartmentTel1.Text = String.Concat(.Item("ApartmentTel1"))
                txtApartmentAddress1.Text = String.Concat(.Item("ApartmentAddr1"))
                txtTravel1.Text = String.Concat(.Item("ApartmentTravel1"))
                txtDistance1.Text = String.Concat(.Item("ApartmentDistance1"))

                txtApartmentName2.Text = String.Concat(.Item("ApartmentName2"))
                txtApartmentTel2.Text = String.Concat(.Item("ApartmentTel2"))
                txtApartmentAddress2.Text = String.Concat(.Item("ApartmentAddr2"))
                txtTravel2.Text = String.Concat(.Item("ApartmentTravel2"))
                txtDistance2.Text = String.Concat(.Item("ApartmentDistance2"))

                txtApartmentName3.Text = String.Concat(.Item("ApartmentName3"))
                txtApartmentTel3.Text = String.Concat(.Item("ApartmentTel3"))
                txtApartmentAddress3.Text = String.Concat(.Item("ApartmentAddr3"))
                txtTravel3.Text = String.Concat(.Item("ApartmentTravel3"))
                txtDistance3.Text = String.Concat(.Item("ApartmentDistance3"))

                txtLetterName.Text = String.Concat(.Item("LetterTo"))

                If DBNull2Zero(.Item("isBranch")) = 0 Then
                    optWorkBrunch.SelectedValue = "2"
                Else
                    optWorkBrunch.SelectedValue = DBNull2Zero(.Item("isBranch"))
                End If

                txtBrunchDetail.Text = String.Concat(.Item("BranchRemark"))

                'Dim WorkList As String() = Split(String.Concat(.Item("WorkList")), "|")
                'chkWorkList.ClearSelection()
                'For i = 0 To WorkList.Length - 1
                '    For n = 0 To chkWorkList.Items.Count - 1
                '        If WorkList(i) = chkWorkList.Items(n).Value Then
                '            chkWorkList.Items(n).Selected = True
                '        End If
                '    Next
                'Next

                'txtWorkOther.Text = String.Concat(.Item("WorkSpec"))

                'If txtWorkOther.Text.Trim <> "" Then
                '    chkWorkOther.Checked = True
                'Else
                '    chkWorkOther.Checked = False
                'End If

                txtTopWork.Text = String.Concat(.Item("WorkTop"))
                txtRemark.Text = String.Concat(.Item("Remark"))

                txtWebsite.Text = String.Concat(.Item("Website"))
                txtFacebook.Text = String.Concat(.Item("Facebook"))

                optReceive.SelectedValue = String.Concat(.Item("isReceive"))
                optIsPay.SelectedValue = String.Concat(.Item("isPay"))

                ddlZone.SelectedValue = String.Concat(.Item("ZoneID"))
                ddlOfficeGroup.SelectedValue = String.Concat(.Item("OfficeID"))

                LoadPersonDetail()
                LoadWorkDetail(pID)
                LoadPreceptorToGrid()
                LoadSubjectToGrid()
            End With
        End If
        dtE = Nothing
        objList = Nothing
    End Sub
    Private Sub LoadWorkDetail(LocationID As Integer)
        chkWorkList.ClearSelection()
        Dim ctlLW As New LocationWorkController
        dt = ctlLW.LocationWorkDetail_Get(LocationID)
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                For n = 0 To chkWorkList.Items.Count - 1
                    If dt.Rows(i)("WorkID") = chkWorkList.Items(n).Value Then
                        chkWorkList.Items(n).Selected = True
                    End If
                Next
            Next
        End If
    End Sub

    Protected Function validateData() As Boolean
        Dim result As Boolean = True
        lblValidate.Text = ""


        If lblID.Text = "" Then
            If ddlLocationGroup.SelectedIndex = -1 Then
                result = False
                lblValidate.Text &= "- กรุณาเลือกประเภทแหล่งฝึก  <br />"
                lblValidate.Visible = True
            Else
                'If ddlType.Items.Count > 0 And ddlType.Visible = True Then
                '    result = False
                '    lblValidate.Text &= "- กรุณาเลือกประเภท" & ddlLocationGroup.SelectedItem.Text & "  <br />"
                '    lblValidate.Visible = True
                'Else
                If ddlDepartment.Visible = True Then
                    If ddlDepartment.SelectedIndex = 4 Then
                        If txtDepartment.Text = "" Then
                            result = False
                            lblValidate.Text &= "- กรุณาระบุสังกัด  <br />"
                            lblValidate.Visible = True
                        End If
                    End If
                End If
                'End If
            End If
        End If

        If txtName.Text = "" Then
            result = False
            lblValidate.Text &= "- กรุณาระบุชื่อแหล่งฝึก  <br />"
            lblValidate.Visible = True

        End If



        'Dim n As Integer = 0
        'For i = 0 To ddlLocationGroup.Items.Count - 1
        '    If ddlLocationGroup.Items(i).Selected Then
        '        n = n + 1
        '    End If
        'Next

        'If n = 0 Then
        '    lblValidate.Text = "กรุณาเลือกประเภทแหล่งฝึก"
        '    lblValidate.Visible = True
        'End If

        Return result
    End Function

    Private Sub ClearData()
        lblMode.Text = "Mode : Add New"
        Me.lblID.Text = ""
        lblAuto.Visible = True
        txtName.Text = ""
        txtAddress.Text = ""
        ddlProvince.SelectedIndex = 1
        ddlZone.SelectedIndex = 0
        ddlOfficeGroup.SelectedIndex = 0

        txtFacebook.Text = ""
        txtWebsite.Text = ""

        Me.txtZipCode.Text = ""
        chkStatus.Checked = True
        ddlPerson.SelectedIndex = 0
        txtMail.Text = ""
        txtCuPosition.Text = ""
        txtCuTel.Text = ""
        txtTel.Text = ""
        txtFax.Text = ""
        txtDepartment.Text = ""
        txtBed.Text = ""
        txtOfficeHour.Text = ""
        txtOfficerNo.Text = ""
        'txtWorkOther.Text = ""
        txtTopWork.Text = ""
        txtRemark.Text = ""

        txtApartmentName1.Text = ""
        txtTravel1.Text = ""
        txtDistance1.Text = ""
        txtApartmentTel1.Text = ""
        txtApartmentAddress1.Text = ""
        txtApartmentName2.Text = ""
        txtTravel2.Text = ""
        txtDistance2.Text = ""
        txtApartmentTel2.Text = ""
        txtApartmentAddress2.Text = ""
        txtApartmentName3.Text = ""
        txtTravel3.Text = ""
        txtDistance3.Text = ""
        txtApartmentTel3.Text = ""
        txtApartmentAddress3.Text = ""

        txtDay.Text = ""
        txtBrunchDetail.Text = ""
        txtOfficeTime.Text = ""

        txtBrunchDetail.Visible = False
        txtDay.Visible = True
        txtOfficeTime.Visible = True

        pnRes1.Visible = False
        pnRes3.Visible = False
        pnWorkBrunch.Visible = False
        pnWorkList.Visible = True


        chkWorkList.ClearSelection()

        txtLetterName.Text = ""

        ddlProvince.Visible = True
        txtStat.Text = ""
        txtStat.Visible = False
        txtCountry.Text = "ประเทศไทย"

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If validateData() Then

            lblValidate.Visible = False

            Dim item As Integer
            Dim HasResidence As Integer = 2

            If optResidence1.Checked Then
                HasResidence = 1
            ElseIf optResidence2.Checked Then
                HasResidence = 2
            ElseIf optResidence3.Checked Then
                HasResidence = 3
            End If

            Dim WorkList As String
            WorkList = ""
            For i = 0 To chkWorkList.Items.Count - 1
                If chkWorkList.Items(i).Selected Then
                    If i <> chkWorkList.Items.Count - 1 Then
                        WorkList &= chkWorkList.Items(i).Value & "|"
                    Else
                        WorkList &= chkWorkList.Items(i).Value
                    End If

                End If
            Next



            Dim sDept As String
            If ddlLocationGroup.SelectedValue = 1 Then
                If ddlDepartment.SelectedIndex <> 4 Then
                    sDept = ddlDepartment.SelectedItem.Text
                Else
                    sDept = txtDepartment.Text
                End If
            Else
                sDept = ddlLevel.SelectedValue
            End If

            Dim ProvID, LocType As Integer
            Dim ProvName As String = ""

            If ddlLocationGroup.SelectedValue = 7 Then
                ProvID = 99
                ProvName = txtStat.Text
            Else
                ProvID = ddlProvince.SelectedValue
                ProvName = ddlProvince.SelectedItem.Text
            End If

            If ddlType.Items.Count = 0 Then
                LocType = 0
            Else
                LocType = ddlType.SelectedValue
            End If

            'Dim CoMail As String

            'If txtMail.Text = "" Then
            '    CoMail = txtPmail.Text
            'Else
            '    CoMail = txtMail.Text
            'End If

            If lblID.Text = "" Then
                If ctlL.Location_CheckDuplicate(txtName.Text) Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','แหล่งฝึกนี้มีในฐานข้อมูลแล้ว กรุณาตรวจสอบชื่อแหล่งฝึกอีกครั้ง');", True)
                    Exit Sub
                End If
                acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Locations", "เพิ่มใหม่ แหล่งฝึก:" & txtName.Text, "")
            Else
                acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Locations", "แก้ไข แหล่งฝึก:" & txtName.Text, "")
            End If

            item = ctlL.Location_Save(StrNull2Zero(lblID.Text), txtName.Text, ddlLocationGroup.SelectedValue, LocType, StrNull2Zero(ddlDepartment.SelectedValue), sDept, ddlLevel.SelectedValue, StrNull2Zero(txtBed.Text), StrNull2Zero(txtBranch.Text), txtAddress.Text, ProvID, ProvName, txtZipCode.Text, txtTel.Text, txtFax.Text, "", "", "", "", txtOfficeHour.Text, StrNull2Zero(txtOfficerNo.Text), Request.Cookies("Username").Value, Boolean2Decimal(chkStatus.Checked), txtReceiveName.Text _
           , StrNull2Zero(optDay.SelectedValue) _
           , txtDay.Text _
           , StrNull2Zero(optTime.SelectedValue) _
           , txtOfficeTime.Text _
           , HasResidence _
           , StrNull2Zero(optIsPay.SelectedValue) _
           , StrNull2Double(txtPay.Text) _
           , StrNull2Zero(optForce.SelectedValue) _
           , txtConfirm.Text _
           , txtApartmentName1.Text _
           , txtApartmentTel1.Text _
           , txtApartmentAddress1.Text _
           , txtTravel1.Text _
           , txtDistance1.Text _
           , StrNull2Zero(optWorkBrunch.SelectedValue) _
           , txtBrunchDetail.Text _
           , WorkList _
           , "" _
           , txtTopWork.Text _
           , txtRemark.Text, "", "", "" _
           , txtApartmentName2.Text _
           , txtApartmentTel2.Text _
           , txtApartmentAddress2.Text _
           , txtTravel2.Text _
           , txtDistance2.Text _
           , txtApartmentName3.Text _
           , txtApartmentTel3.Text _
           , txtApartmentAddress3.Text _
           , txtTravel3.Text _
           , txtDistance3.Text, txtLetterName.Text, txtCountry.Text, txtWebsite.Text, txtFacebook.Text _
           , ddlZone.SelectedValue _
           , ddlOfficeGroup.SelectedValue _
           , optReceive.SelectedValue _
           , optIsPay.SelectedValue, txtLat.Text, txtLng.Text, optLang.SelectedValue _
           , StrNull2Zero(ddlPerson.SelectedValue), ddlPerson.SelectedItem.Text, txtCuPosition.Text, txtCuMail.Text, txtCuTel.Text)


            SaveWorkList(StrNull2Zero(lblID.Text))

            'ClearData()
            'LoadGroupSelected()

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','Your data has been saved successfully.');", True)

            If lblID.Text = "" Then
                Response.Redirect("LocationList.aspx")
            Else

            End If

        Else

        End If

    End Sub

    Private Sub SaveWorkList(LocationID As Integer)
        Dim CtlLW As New LocationWorkController
        CtlLW.LocationWorkDetail_DeleteByLocation(LocationID)

        For i = 0 To chkWorkList.Items.Count - 1
            If chkWorkList.Items(i).Selected Then
                CtlLW.LocationWorkDetail_Save(LocationID, StrNull2Zero(chkWorkList.Items(i).Value))
            End If
        Next
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
        LoadGroupSelected()
    End Sub
    Private Sub LoadPrefix()
        Dim ctlM As New MasterController
        dt = ctlM.Prefix_GetActive()
        With ddlPPrefix
            .DataSource = dt
            .DataTextField = "PrefixName"
            .DataValueField = "PrefixID"
            .DataBind()
        End With
        dt = Nothing
    End Sub
    Private Sub LoadLocationWork()
        chkWorkList.Items.Clear()
        Dim ctlL As New LocationWorkController
        dt = ctlL.LocationWork_GetByGroup(ddlLocationGroup.SelectedValue)
        With chkWorkList
            .Enabled = True
            .DataSource = dt
            .DataTextField = "WorkName"
            .DataValueField = "WorkID"
            .DataBind()
            .Visible = True
        End With

        With ddlWork
            .DataSource = dt
            .DataTextField = "WorkName"
            .DataValueField = "WorkID"
            .DataBind()
        End With

        If dt.Rows.Count > 0 Then
            lblNoWork.Visible = False
        Else
            lblNoWork.Visible = True
        End If
        dt = Nothing
    End Sub
    Private Sub LoadGroupSelected()

        lblType.Visible = True
        ddlType.Visible = True
        LoadLocationTypeToDDL()

        ddlDepartment.Visible = False
        lblSpecDept.Visible = False
        lblDepartment.Visible = False
        txtDepartment.Visible = False

        lblLevel.Visible = False
        ddlLevel.Visible = False

        Select Case ddlLocationGroup.SelectedValue.ToString()
            Case "1" 'โรงพยาบาล
                lblDepartment.Visible = True
                ddlDepartment.Visible = True
            Case "2" 'ร้านยา
                lblLevel.Visible = True
                ddlLevel.Visible = True
            Case "3"   'ศูนย์บริการสาธารณสุข
                ddlDepartment.SelectedValue = "1"
                ddlDepartment.Visible = True
                lblDepartment.Visible = True
            Case "4" 'สถาบันวิจัย 
                ddlDepartment.Visible = True
                ddlDepartment.SelectedValue = "99"

                txtBed.Text = 1
                txtDepartment.Visible = True
                lblDepartment.Visible = True
                lblSpecDept.Visible = True
            Case "5", "6" 'บริษัท,โรงงาน
                ddlDepartment.Visible = False
                ddlDepartment.SelectedValue = "99"
                lblLevel.Visible = False
                txtDepartment.Visible = False
                lblSpecDept.Visible = False
            Case "7" 'มหาวิทยาลัยต่างประเทศ
                ddlDepartment.SelectedValue = "2"
                ddlDepartment.Visible = True
                lblDepartment.Visible = True
            Case "99" 'อื่นๆ
                ddlDepartment.Visible = True
                ddlDepartment.SelectedValue = "99"
                lblDepartment.Visible = True
                txtDepartment.Visible = True
                lblSpecDept.Visible = True
        End Select


    End Sub

    Protected Sub ddlDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepartment.SelectedIndexChanged

        If ddlDepartment.SelectedIndex = 4 Then
            lblSpecDept.Visible = True
            txtDepartment.Visible = True
        Else
            lblSpecDept.Visible = False
            txtDepartment.Visible = False
        End If
    End Sub


    Protected Sub cmdAddPreceptor_Click(sender As Object, e As EventArgs) Handles cmdAddPreceptor.Click

        If txtPFname.Text = "" Or txtPLname.Text = "" Or txtPposition.Text = "" Or txtPmail.Text = "" Or txtPtel.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Warning!", "openModalWarningInfo(this,'Warning','กรุณาระบุข้อมูลให้ครบถ้วน.');", True)
            Exit Sub
        End If

        ctlP.Preceptor_Add(StrNull2Zero(ddlPPrefix.SelectedValue), txtPFname.Text, txtPLname.Text, txtPposition.Text, StrNull2Zero(ddlWork.SelectedValue), txtPmail.Text, txtPtel.Text, "P", StrNull2Zero(lblID.Text), Request.Cookies("Username").Value)

        LoadPreceptorToGrid()

        txtPFname.Text = ""
        txtPLname.Text = ""
        txtPmail.Text = ""
        txtPposition.Text = ""
        txtPtel.Text = ""
    End Sub

    Private Sub LoadSubjectToDDL()
        Dim ctlSj As New SubjectController
        dt = ctlSj.Subject_GetActive
        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With ddlCourse
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("SubjectName"))
                    .Items(i).Value = dt.Rows(i)("SubjectID")
                End With
            Next

        End If
        dt = Nothing
    End Sub
    Private Sub LoadPreceptorToGrid()
        dt = ctlP.Preceptor_GetByLocation(StrNull2Zero(lblID.Text))
        grdPreceptor.DataSource = dt
        grdPreceptor.DataBind()
        dt = Nothing
    End Sub

    Private Sub LoadSubjectToGrid()
        dt = ctlLS.LocationSubject_Get(StrNull2Zero(lblID.Text))
        grdSubject.DataSource = dt
        grdSubject.DataBind()
        dt = Nothing
    End Sub

    Private Sub CheckResidence(pID As Integer)

        pnRes1.Visible = False
        pnRes3.Visible = False

        optResidence1.Checked = False
        optResidence2.Checked = False
        optResidence3.Checked = False

        Select Case pID
            Case 1
                pnRes1.Visible = True
            Case 3
                pnRes3.Visible = True
            Case Else
                pnRes1.Visible = False
                pnRes3.Visible = False
        End Select
    End Sub
    Protected Sub optResidence1_CheckedChanged(sender As Object, e As EventArgs) Handles optResidence1.CheckedChanged
        CheckResidence(1)
        optResidence1.Checked = True
    End Sub
    Protected Sub optResidence2_CheckedChanged(sender As Object, e As EventArgs) Handles optResidence2.CheckedChanged
        CheckResidence(2)
        optResidence2.Checked = True
    End Sub

    Protected Sub optResidence3_CheckedChanged(sender As Object, e As EventArgs) Handles optResidence3.CheckedChanged
        CheckResidence(3)
        optResidence3.Checked = True
    End Sub

    Protected Sub ddlLocationGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocationGroup.SelectedIndexChanged
        LoadGroupSelected()
        LoadLocationWork()
        LoadWorkDetail(StrNull2Zero(lblID.Text))
    End Sub

    Private Sub cmdAddSubject_Click(sender As Object, e As EventArgs) Handles cmdAddSubject.Click
        ctlLS.LocationSubject_Save(StrNull2Zero(lblID.Text), StrNull2Zero(ddlCourse.SelectedValue))
        LoadSubjectToGrid()
    End Sub

    Private Sub ddlPerson_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPerson.SelectedIndexChanged
        LoadPersonDetail()
    End Sub
    Private Sub LoadPersonDetail()
        dt = ctlP.Person_GetByID(StrNull2Zero(ddlPerson.SelectedValue))
        If dt.Rows.Count > 0 Then
            txtCuPosition.Text = String.Concat(dt.Rows(0)("PositionName"))
            txtCuTel.Text = String.Concat(dt.Rows(0)("Telephone"))
            txtCuMail.Text = String.Concat(dt.Rows(0)("Email"))
        End If
    End Sub
End Class

