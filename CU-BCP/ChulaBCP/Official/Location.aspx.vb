Imports DevExpress.Web
'Imports DevExpress.WebUtils
Public Class Location
    Inherits System.Web.UI.Page

    Dim ctlL As New LocationController
    Dim dt As New DataTable
    Dim ds As New DataSet
    Dim ctlPsn As New PersonController
    Dim acc As New UserController
    Dim ctlM As New MasterController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        'GridViewFeaturesHelper.SetupGlobalGridViewBehavior(grdCoordinator2)

        If Not IsPostBack Then
            ApplyLayout(0)

            lblID.Text = ""
            ClearData()
            ctlL.Coordinator_DeleteQ(StrNull2Zero(lblID.Text))
            LoadProvinceToDDL()

            LoadLocationDepartmentToDDL()
            LoadLocationGroupToDDL()


            LoadZoneToDDL()
            LoadOfficeToDDL()

            LoadCourseToDDL()

            If Request.Cookies("ROLE_PCT").Value = True And Request.Cookies("ROLE_ADM").Value <> True Then
                EditData(ctlPsn.Location_GetByPersonID(DBNull2Zero(Request.Cookies("PersonID").Value)))
            ElseIf Request.Cookies("ROLE_ADM").Value = True Then
                EditData(DBNull2Zero(Request("lid")))
            End If
            LoadLocationWork()
            LoadWorkDetail(StrNull2Zero(lblID.Text))
        End If
        txtPay.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        txtZipCode.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")

        LoadCoordinatorToGrid()
    End Sub

    Protected Sub grdCoordinator2_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        ApplyLayout(Int32.Parse(e.Parameters))
    End Sub

    Private Sub ApplyLayout(ByVal layoutIndex As Integer)
        grdCoordinator2.BeginUpdate()
        Try
            'grdCoordinator2.ClearSort()
            'grdCoordinator2.SortBy(grdCoordinator2.Columns("SubjectName"), SortDirection.Descending)
            grdCoordinator2.GroupBy(grdCoordinator2.Columns("SubjectName"))
        Finally
            grdCoordinator2.EndUpdate()
        End Try
        grdCoordinator2.ExpandAll()
    End Sub

    Protected Sub grdCoordinator2_CustomButtonCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomButtonCallbackEventArgs)
        If e.ButtonID = "Del" Then
            ctlL.Coordinator_Delete(grdCoordinator2.GetRowValues(e.VisibleIndex, "UID"))
            LoadCoordinatorToGrid()
        End If
    End Sub

    Private Sub LoadProvinceToDDL()

        dt = ctlm.Province_GetActive
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
        If dtE.Rows.Count > 0 Then
            With dtE.Rows(0)
                isAdd = False

                Me.lblID.Text = DBNull2Str(dtE.Rows(0)("LocationID"))
                txtName.Text = DBNull2Str(dtE.Rows(0)("LocationName"))

                ddlLocationGroup.SelectedValue = DBNull2Str(dtE.Rows(0)("LocationGroupID"))
                LoadGroupSelected()

                If ddlLocationGroup.SelectedValue = 1 Then
                    Try
                        ddlDepartment.SelectedValue = DBNull2Str(dtE.Rows(0)("DepartmentName"))
                    Catch ex As Exception
                        ddlDepartment.SelectedValue = "อื่นๆ"
                        ddlDepartment.Enabled = True
                        txtDepartment.Text = DBNull2Str(dtE.Rows(0)("DepartmentName"))
                    End Try

                End If

                If DBNull2Str(dtE.Rows(0)("LocationTypeID")) <> "0" Then
                    ddlType.SelectedValue = DBNull2Str(dtE.Rows(0)("LocationTypeID"))
                End If


                ddlDepartment.SelectedValue = DBNull2Zero(dtE.Rows(0)("DepartmentID"))
                txtDepartment.Text = DBNull2Str(dtE.Rows(0)("DepartmentName"))
                ddlLevel.SelectedValue = DBNull2Zero(dtE.Rows(0)("LevelID"))
                txtBed.Text = DBNull2Str(dtE.Rows(0)("Bed"))
                txtBranch.Text = DBNull2Str(dtE.Rows(0)("Branch"))

                txtLat.Text = DBNull2Str(dtE.Rows(0)("Lat"))
                txtLng.Text = DBNull2Str(dtE.Rows(0)("Lng"))

                txtAddress.Text = DBNull2Str(dtE.Rows(0)("Address"))
                If DBNull2Zero(dtE.Rows(0)("ProvinceID")) = 99 Then
                    ddlProvince.Visible = False
                Else
                    ddlProvince.Visible = True
                    ddlProvince.SelectedValue = DBNull2Zero(dtE.Rows(0)("ProvinceID"))
                End If

                Me.txtZipCode.Text = DBNull2Str(dtE.Rows(0)("ZipCode"))
                txtStat.Text = DBNull2Str(dtE.Rows(0)("ProvinceName"))
                txtCountry.Text = DBNull2Str(dtE.Rows(0)("Country"))

                txtTel.Text = DBNull2Str(dtE.Rows(0)("Office_Tel"))
                txtFax.Text = DBNull2Str(dtE.Rows(0)("Office_Fax"))

                txtCoName.Text = DBNull2Str(dtE.Rows(0)("Co_Name"))
                txtCoPosition.Text = DBNull2Str(dtE.Rows(0)("Co_Position"))
                txtCoTel.Text = DBNull2Str(dtE.Rows(0)("Co_Tel"))
                txtCoMail.Text = DBNull2Str(dtE.Rows(0)("Co_Mail"))
                txtMail.Text = DBNull2Str(dtE.Rows(0)("Co_Mail"))


                txtOfficeHour.Text = DBNull2Str(dtE.Rows(0)("Office_hours"))
                txtOfficerNo.Text = DBNull2Str(dtE.Rows(0)("Officer_Count"))

                chkStatus.Checked = ConvertStatusFlag2CHK(dtE.Rows(0)("StatusFlag"))

                txtReceiveName.Text = DBNull2Str(dtE.Rows(0)("Bill_Name"))


                optDay.SelectedValue = DBNull2Str(.Item("WorkDayID"))
                txtDay.Text = DBNull2Str(.Item("WorkDayDesc"))
                optTime.SelectedValue = DBNull2Str(.Item("WorkTimeID"))
                txtOfficeTime.Text = DBNull2Str(.Item("WorkTimeDesc"))

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
                txtPay.Text = DBNull2Str(.Item("PayAmount"))
                optForce.SelectedValue = DBNull2Zero(.Item("isForce"))
                txtConfirm.Text = DBNull2Str(.Item("ConfirmHold"))

                txtApartmentName1.Text = DBNull2Str(.Item("ApartmentName1"))
                txtApartmentTel1.Text = DBNull2Str(.Item("ApartmentTel1"))
                txtApartmentAddress1.Text = DBNull2Str(.Item("ApartmentAddr1"))
                txtTravel1.Text = DBNull2Str(.Item("ApartmentTravel1"))
                txtDistance1.Text = DBNull2Str(.Item("ApartmentDistance1"))

                txtApartmentName2.Text = DBNull2Str(.Item("ApartmentName2"))
                txtApartmentTel2.Text = DBNull2Str(.Item("ApartmentTel2"))
                txtApartmentAddress2.Text = DBNull2Str(.Item("ApartmentAddr2"))
                txtTravel2.Text = DBNull2Str(.Item("ApartmentTravel2"))
                txtDistance2.Text = DBNull2Str(.Item("ApartmentDistance2"))

                txtApartmentName3.Text = DBNull2Str(.Item("ApartmentName3"))
                txtApartmentTel3.Text = DBNull2Str(.Item("ApartmentTel3"))
                txtApartmentAddress3.Text = DBNull2Str(.Item("ApartmentAddr3"))
                txtTravel3.Text = DBNull2Str(.Item("ApartmentTravel3"))
                txtDistance3.Text = DBNull2Str(.Item("ApartmentDistance3"))

                txtLetterName.Text = DBNull2Str(.Item("LetterTo"))

                If DBNull2Zero(.Item("isBranch")) = 0 Then
                    optWorkBrunch.SelectedValue = "2"
                Else
                    optWorkBrunch.SelectedValue = DBNull2Zero(.Item("isBranch"))
                End If

                txtBrunchDetail.Text = DBNull2Str(.Item("BranchRemark"))

                'Dim WorkList As String() = Split(DBNull2Str(.Item("WorkList")), "|")
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
                txtRemark.Text = DBNull2Str(.Item("Remark"))

                txtWebsite.Text = String.Concat(.Item("Website"))
                txtFacebook.Text = String.Concat(.Item("Facebook"))

                optReceive.SelectedValue = String.Concat(.Item("isReceive"))
                optIsPay.SelectedValue = String.Concat(.Item("isPay"))

                ddlZone.SelectedValue = String.Concat(.Item("ZoneID"))
                ddlOfficeGroup.SelectedValue = String.Concat(.Item("OfficeID"))

                LoadWorkDetail(pID)
                LoadCoordinatorToGrid()

            End With
        End If
        dtE = Nothing
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
                If ddlDepartment.Enabled = True Then
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
        Me.lblID.Text = ""
        txtName.Text = ""
        txtAddress.Text = ""
        ddlProvince.SelectedIndex = 1
        ddlZone.SelectedIndex = 0
        ddlOfficeGroup.SelectedIndex = 0

        txtFacebook.Text = ""
        txtWebsite.Text = ""

        Me.txtZipCode.Text = ""
        chkStatus.Checked = True
        txtCoName.Text = ""
        txtMail.Text = ""
        txtCoPosition.Text = ""
        txtCoTel.Text = ""
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
        lblStep5.Text = "งานที่สามารถให้นิสิตฝึกปฏิบัติได้"

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

            Dim CoMail As String

            If txtMail.Text = "" Then
                CoMail = txtCoMail.Text
            Else
                CoMail = txtMail.Text
            End If


            If lblID.Text = "" Then

                If ctlL.Location_CheckDuplicate(txtName.Text) Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','แหล่งฝึกนี้มีในฐานข้อมูลแล้ว กรุณาตรวจสอบชื่อแหล่งฝึกอีกครั้ง');", True)
                    Exit Sub
                End If

                item = ctlL.Location_Add(txtName.Text, ddlLocationGroup.SelectedValue, LocType, StrNull2Zero(ddlDepartment.SelectedValue), sDept, ddlLevel.SelectedValue, StrNull2Zero(txtBed.Text), StrNull2Zero(txtBranch.Text), txtAddress.Text, ProvID, ProvName, txtZipCode.Text, txtTel.Text, txtFax.Text, txtCoName.Text, txtCoPosition.Text, CoMail, txtCoTel.Text, txtOfficeHour.Text, StrNull2Zero(txtOfficerNo.Text), Request.Cookies("UserLogin").Value, Boolean2Decimal(chkStatus.Checked), txtReceiveName.Text _
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
           , optIsPay.SelectedValue, txtLat.Text, txtLng.Text)

                acc.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_ADD, "Locations", "เพิ่มใหม่ แหล่งฝึก:" & txtName.Text, "")

                UpdateCoordinatorStatus()

            Else
                item = ctlL.Location_Update(StrNull2Zero(lblID.Text), txtName.Text, ddlLocationGroup.SelectedValue, LocType, StrNull2Zero(ddlDepartment.SelectedValue), sDept, ddlLevel.SelectedValue, StrNull2Zero(txtBed.Text), StrNull2Zero(txtBranch.Text), txtAddress.Text, ProvID, ProvName, txtZipCode.Text, txtTel.Text, txtFax.Text, txtCoName.Text, txtCoPosition.Text, CoMail, txtCoTel.Text, txtOfficeHour.Text, StrNull2Zero(txtOfficerNo.Text), Request.Cookies("UserLogin").Value, Boolean2Decimal(chkStatus.Checked), txtReceiveName.Text _
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
           , optIsPay.SelectedValue, txtLat.Text, txtLng.Text)

                acc.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_UPD, "Locations", "แก้ไข แหล่งฝึก:" & txtName.Text, "")

            End If


            SaveWorkList(StrNull2Zero(lblID.Text))

            'ClearData()
            'LoadGroupSelected()

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)

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

    Private Sub UpdateCoordinatorStatus()

        Dim iLocationID As Integer

        iLocationID = ctlL.Location_GetIDByName(txtName.Text)

        If grdCoordinator.Rows.Count > 0 Then
            For i = 0 To grdCoordinator.Rows.Count - 1
                ctlL.Coordinator_UpdateStatus(iLocationID, StrNull2Zero(grdCoordinator.DataKeys(i).Value), grdCoordinator.Rows(i).Cells(5).Text)
            Next
        End If

    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
        LoadGroupSelected()
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

        ddlDepartment.Enabled = False
        lblDepartment.Visible = False

        ddlLevel.Enabled = False

        Select Case ddlLocationGroup.SelectedValue.ToString()
            Case "1" 'โรงพยาบาล
                lblDepartment.Visible = True
                ddlDepartment.Enabled = True
            Case "2" 'ร้านยา 
                ddlLevel.Enabled = True
            Case "3"   'ศูนย์บริการสาธารณสุข
                ddlDepartment.SelectedValue = "1"
                ddlDepartment.Enabled = True
                lblDepartment.Visible = True
            Case "4" 'สถาบันวิจัย 
                ddlDepartment.Enabled = True
                ddlDepartment.SelectedValue = "99"

                txtBed.Text = 1
                lblDepartment.Visible = True
            Case "5", "6" 'บริษัท,โรงงาน
                ddlDepartment.Enabled = False
                ddlDepartment.SelectedValue = "99"
            Case "7" 'มหาวิทยาลัยต่างประเทศ
                ddlDepartment.SelectedValue = "2"
                ddlDepartment.Enabled = True
                lblDepartment.Visible = True
            Case "99" 'อื่นๆ
                ddlDepartment.Enabled = True
                ddlDepartment.SelectedValue = "99"
                lblDepartment.Visible = True
        End Select
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click

        If txtCoName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ท่านยังไม่ได้ระบุชื่อ-สกุลผู้ประสานงาน');", True)
            Exit Sub
        End If
        Dim StatusFlag As String
        StatusFlag = "Q"
        If lblID.Text <> "" Then
            StatusFlag = "A"
        End If

        ctlL.Coordinator_Add(StrNull2Zero(lblID.Text), txtCoName.Text, txtCoPosition.Text, txtCoMail.Text, txtCoTel.Text, StrNull2Zero(ddlSubject.SelectedValue), ConvertBoolean2Integer(chkCoMain.Checked), StatusFlag, txtName.Text, ddlWork.SelectedValue)

        LoadCoordinatorToGrid()

        txtCoName.Text = ""
        txtCoMail.Text = ""
        txtCoPosition.Text = ""
        txtCoTel.Text = ""
        chkCoMain.Checked = False
    End Sub

    Private Sub LoadCourseToDDL()
        ddlSubject.Items.Clear()
        Dim ctlSj As New SubjectController

        dt = ctlSj.Subject_GetActive

        If dt.Rows.Count > 0 Then
            ddlSubject.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With ddlSubject
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("SubjectName"))
                    .Items(i).Value = dt.Rows(i)("SubjectID")
                End With
            Next

        End If
        dt = Nothing
    End Sub

    Private Sub LoadCoordinatorToGrid()

        dt = ctlL.Coordinator_GetSearch(StrNull2Zero(lblID.Text), txtName.Text)
        If dt.Rows.Count > 0 Then
            'With grdCoordinator
            '    .Visible = True
            '    .DataSource = dt
            '    .DataBind()
            'End With

            grdCoordinator2.DataSource = dt
            grdCoordinator2.DataBind()


        Else
            grdCoordinator.DataSource = Nothing
            grdCoordinator.Visible = False
        End If

        dt = Nothing
    End Sub

    Protected Sub grdCoordinator_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdCoordinator.RowCommand

        ctlL.Coordinator_Delete(e.CommandArgument)
        LoadCoordinatorToGrid()

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

    Private Sub grdCoordinator_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdCoordinator.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub ddlLocationGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocationGroup.SelectedIndexChanged
        LoadGroupSelected()
        LoadLocationWork()
        LoadWorkDetail(StrNull2Zero(lblID.Text))
    End Sub

End Class

