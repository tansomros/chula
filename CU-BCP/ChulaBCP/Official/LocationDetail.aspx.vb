Imports Newtonsoft.Json
Public Class LocationDetail
    Inherits System.Web.UI.Page

    'Dim ctlLG As New REQcontroller
    'Dim ctlReg As New RegisterController
    Dim ctlL As New LocationController

    Dim dt As New DataTable
    Dim ds As New DataSet

    Public Shared json As String
    Public Shared lat As Double
    Public Shared lng As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            pnSelectPhase.Visible = False
            hdLocationID.Value = ""
            ClearData()
            LoadLocationDetail(Request("lid"))
        End If

    End Sub
    Private Sub LoadMaps(LID As Integer)
        Dim dtMap As New DataTable
        dtMap = ctlL.Location_GetMapsByLocation(LID)
        If dtMap.Rows.Count > 0 Then
            lat = DBNull2Dbl(dtMap.Rows(0)("lat"))
            lng = DBNull2Dbl(dtMap.Rows(0)("lng"))
        End If
        json = JsonConvert.SerializeObject(dtMap, Formatting.Indented)
    End Sub

    Private Sub LoadLocationDetail(LID As Integer)
        Dim dtE As New DataTable

        dtE = ctlL.Location_GetByID(LID)
        If dtE.Rows.Count > 0 Then
            With dtE.Rows(0)
                isAdd = False

                LoadMaps(DBNull2Zero(dtE.Rows(0)("LocationID")))

                hdLocationID.Value = DBNull2Zero(dtE.Rows(0)("LocationID"))
                lblName.Text = DBNull2Str(dtE.Rows(0)("LocationName"))
                lblAddress.Text = DBNull2Str(dtE.Rows(0)("Address"))
                lblProvince.Text = DBNull2Str(dtE.Rows(0)("ProvinceName"))
                lblCountry.Text = DBNull2Str(dtE.Rows(0)("Country"))

                Me.lblZipCode.Text = DBNull2Str(dtE.Rows(0)("ZipCode"))
                lblTel.Text = DBNull2Str(dtE.Rows(0)("Office_Tel"))
                lblFax.Text = DBNull2Str(dtE.Rows(0)("Office_Fax"))
                lblCoName.Text = DBNull2Str(dtE.Rows(0)("Co_Name"))
                lblCoPosition.Text = DBNull2Str(dtE.Rows(0)("Co_Position"))
                lblCoMail.Text = DBNull2Str(dtE.Rows(0)("Co_Mail"))
                lblCoTel.Text = DBNull2Str(dtE.Rows(0)("Co_Tel"))

                hplWebsite.Text = String.Concat(dtE.Rows(0)("Website"))
                hplWebsite.NavigateUrl = String.Concat(dtE.Rows(0)("Website"))
                hplFacebook.Text = String.Concat(dtE.Rows(0)("Facebook"))
                hplFacebook.NavigateUrl = String.Concat(dtE.Rows(0)("Facebook"))

                lblGroup.Text = DBNull2Str(dtE.Rows(0)("LocationGroupName"))
                lblLocationType.Text = DBNull2Str(dtE.Rows(0)("LocationTypeName"))

                If DBNull2Str(dtE.Rows(0)("LocationGroupID")) = 1 Then

                    lblField.Text = "จำนวนเตียง :"
                    lblDepartment.Text = "สังกัด :"

                    lblOfficeHour.Visible = False
                    lblOfficer.Visible = False
                    lblOfficerNo.Visible = False
                    lblOpen.Visible = False

                    lblBranch.Text = "-"
                    lblBrunchDetail.Text = "-"
                    pnWorkBrunch.Visible = False

                    'lblStep5.Text = "งานที่สามารถให้นิสิตฝึกปฏิบัติได้"

                    'Dim str() As String
                    'str = Split(DBNull2Str(dtE.Rows(0)("WorkList")), "|")
                    'For i = 0 To str.Length - 1
                    '    Select Case str(i)
                    '        Case 1
                    '            lblWork.Text &= "- งานบริการผู้ป่วยนอกและผู้ป่วยใน <br/>"
                    '        Case 2
                    '            lblWork.Text &= "- งานด้านเภสัชกรรมคลีนิก <br/> "
                    '        Case 3
                    '            lblWork.Text &= "- งานด้านคลับเวชภัณฑ์ <br/> "
                    '        Case 4
                    '            lblWork.Text &= "-  งานบริการวิชาการ <br/> "
                    '        Case 5
                    '            lblWork.Text &= "-  งานการผลิตยา <br/> "
                    '        Case 6
                    '            lblWork.Text &= "-  งานคุ้มครองผู้บริโภคในหน่วยงาน <br/> "
                    '        Case 7
                    '            lblWork.Text &= "-  ศึกษาดูงานคุ้มครองผู้บริโภค ณ สำนักงานสาธารณสุขจังหวัด <br/> "
                    '        Case 8
                    '            lblWork.Text &= "-  " & DBNull2Str(dtE.Rows(0)("WorkSpec"))
                    '    End Select
                    'Next
                    'LoadWorkList(DBNull2Zero(dtE.Rows(0)("LocationID")))
                    lblOtherWork.Text = DBNull2Str(dtE.Rows(0)("WorkSpec"))
                Else
                    pnWorkList.Visible = False
                    'lblStep5.Text = "กรณีร้านยาหลายสาขา"
                    lblField.Text = "จำนวนสาขา :"
                    lblDepartment.Text = "ได้รับการรับรองเป็นร้านยาคุณภาพ"

                    lblOfficeHour.Text = DBNull2StrDash(dtE.Rows(0)("Office_hours"))
                    lblOfficerNo.Text = DBNull2Zero(dtE.Rows(0)("Officer_Count"))

                    If DBNull2Zero(dtE.Rows(0)("isBranch")) = 1 Then
                        lblBranch.Text = "- นิสิตต้องหมุนเวียนฝึกทุกสาขา"
                    Else
                        lblBranch.Text = "- อยู่ประจำสาขาเดียว"
                    End If

                    lblBrunchDetail.Text = "- " & DBNull2Str(dtE.Rows(0)("BranchRemark"))

                End If

                lblISO.Text = DBNull2Str(dtE.Rows(0)("DepartmentName"))
                lblFieldCount.Text = DBNull2Dbl(dtE.Rows(0)("Bed")).ToString("#,###")

                If DBNull2Str(dtE.Rows(0)("isSameGender")) = "Y" Then
                    lblSameGender.Text = "ใช่"
                Else
                    lblSameGender.Text = "ไม่ใช่"
                End If

                If DBNull2Str(dtE.Rows(0)("WorkDayDesc")) = "" Then
                    lblDay.Text = "ไม่ระบุ"
                Else
                    lblDay.Text = DBNull2Str(dtE.Rows(0)("WorkDayDesc"))
                End If
                If DBNull2Str(dtE.Rows(0)("WorkTimeDesc")) = "" Then
                    lblOfficeTime.Text = "ไม่ระบุ"
                Else
                    lblOfficeTime.Text = DBNull2Str(dtE.Rows(0)("WorkTimeDesc"))
                End If



                lblRoom.Text = ""
                pnRoom1.Visible = False
                pnRoom2.Visible = False
                pnRoom3.Visible = False

                Select Case DBNull2Zero(dtE.Rows(0)("HasResidence"))
                    Case 1
                        lblRoom.Text = "- แหล่งฝึกมีที่พักให้ <br/>"
                        If DBNull2Zero(dtE.Rows(0)("HasCost")) = 1 Then
                            lblRoom.Text &= "- เสียค่าเช่าโดยประมาณ เดือนละ " & DBNull2Dbl(dtE.Rows(0)("PayAmount")) & "บาท <br/>"
                        End If
                        If DBNull2Zero(dtE.Rows(0)("isForce")) = 1 Then
                            lblRoom.Text &= "- นิสิตต้องเข้าพัก ณ ที่พักที่แหล่งฝึกจัดหาให้ <br/> "
                        End If

                        If DBNull2Str(dtE.Rows(0)("ConfirmHold")) <> "" Then
                            lblRoom.Text &= "- นิสิตต้องติดต่อเพื่อเข้าพักที่แหล่งฝึกจัดหาให้ล่วงหน้า " & DBNull2Str(dtE.Rows(0)("ConfirmHold")) & " เดือน <br/> "
                        End If

                    Case 2
                        lblRoom.Text = "- แหล่งฝึกไม่มีที่พักให้ <br/>"
                    Case 3, 4
                        lblRoom.Text = "- แหล่งฝึกไม่มีที่พักให้ แต่สามารถแนะนำที่พักให้ได้ <br/>"
                        pnRoom1.Visible = True
                        lblApartmentName1.Text = DBNull2Str(dtE.Rows(0)("ApartmentName1"))
                        lblApartmentTel1.Text = DBNull2Str(dtE.Rows(0)("ApartmentTel1"))
                        lblApartmentAddress1.Text = DBNull2Str(dtE.Rows(0)("ApartmentAddr1"))
                        lblTravel1.Text = DBNull2Str(dtE.Rows(0)("ApartmentTravel1"))
                        lblDistance1.Text = DBNull2Str(dtE.Rows(0)("ApartmentDistance1"))

                        If DBNull2Str(dtE.Rows(0)("ApartmentName2")) <> "" Then
                            pnRoom2.Visible = True
                            lblApartmentName2.Text = DBNull2Str(dtE.Rows(0)("ApartmentName2"))
                            lblApartmentTel2.Text = DBNull2Str(dtE.Rows(0)("ApartmentTel2"))
                            lblApartmentAddress2.Text = DBNull2Str(dtE.Rows(0)("ApartmentAddr2"))
                            lblTravel2.Text = DBNull2Str(dtE.Rows(0)("ApartmentTravel2"))
                            lblDistance2.Text = DBNull2Str(dtE.Rows(0)("ApartmentDistance2"))
                        End If
                        If DBNull2Str(dtE.Rows(0)("ApartmentName3")) <> "" Then
                            pnRoom3.Visible = True
                            lblApartmentName3.Text = DBNull2Str(dtE.Rows(0)("ApartmentName3"))
                            lblApartmentTel3.Text = DBNull2Str(dtE.Rows(0)("ApartmentTel3"))
                            lblApartmentAddress3.Text = DBNull2Str(dtE.Rows(0)("ApartmentAddr3"))
                            lblTravel3.Text = DBNull2Str(dtE.Rows(0)("ApartmentTravel3"))
                            lblDistance3.Text = DBNull2Str(dtE.Rows(0)("ApartmentDistance3"))
                        End If

                End Select

                lblTopWork.Text = DBNull2Str(dtE.Rows(0)("WorkTop"))
                lblRemark.Text = DBNull2Str(dtE.Rows(0)("Remark"))

                'If Request.Cookies("ROLE_STD").Value.Value = True Then
                '    If Not SystemOnlineTime() Then
                '        pnSelectPhase.Visible = False
                '    Else

                '        If Request("y") <> Request.Cookies("EDUYEAR").Value.Value Then
                '            pnSelectPhase.Visible = False
                '        Else
                '            If Not Request("subid") Is Nothing Then
                '                pnSelectPhase.Visible = True
                '                LoadRequirement(Request("y"), Request("lid"), Request("subid"))
                '            Else
                '                pnSelectPhase.Visible = False
                '            End If
                '        End If
                '    End If
                'End If
                'LoadRecommend()
                'LoadRoomRecommend()
            End With
        End If
        dtE = Nothing
    End Sub
    'Private Function SystemOnlineTime() As Boolean
    '    Dim ctlCfg As New SystemConfigController
    '    Dim Bdate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_STARTDATE)))
    '    Dim Edate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_ENDDATE)))
    '    Dim sToday As Integer = StrNull2Zero(ConvertDate2DBString(ctlCfg.GET_DATE_SERVER))
    '    Dim bAvailable As Boolean
    '    If sToday < Bdate Then
    '        bAvailable = False
    '    ElseIf sToday > Edate Then
    '        bAvailable = False
    '    Else
    '        bAvailable = True
    '    End If
    '    Return bAvailable
    'End Function

    'Private Sub LoadWorkList(LocationID As Integer)
    '    lblWork.Text = ""
    '    Dim ctlLW As New LocationWorkController
    '    Dim dtW As New DataTable
    '    dtW = ctlLW.LocationWorkDetail_Get(LocationID)
    '    If dtW.Rows.Count > 0 Then
    '        For i = 0 To dtW.Rows.Count - 1
    '            lblWork.Text &= "- " & dtW.Rows(i)("WorkName") & " <br/>"
    '        Next
    '    End If
    'End Sub

    'Private Sub LoadRecommend()
    '    Dim ctlR As New RecommendController

    '    dt = ctlR.LocationRecommend_GetByLocation(hdLocationID.Value)
    '    If dt.Rows.Count > 0 Then
    '        With grdRecommend
    '            .Visible = True
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '    End If
    'End Sub
    'Private Sub LoadRoomRecommend()
    '    Dim ctlR As New RecommendController

    '    dt = ctlR.RoomRecommend_GetByLocation(hdLocationID.Value)
    '    If dt.Rows.Count > 0 Then
    '        With grdApartment
    '            .Visible = True
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '    End If
    'End Sub


    'Private Sub LoadRequirement(ByVal Year As Integer, LID As Integer, SubjectCode As String)
    '    Dim dtA As New DataTable
    '    Dim ctlStd As New StudentController
    '    'Dim hasReq As Boolean = False
    '    Dim sSex As String = ctlStd.GetStudentSex(Request.Cookies("ProfileID").Value.Value)
    '    Dim k As Integer = 0
    '    Dim isM, isF, isNo As Integer
    '    isM = 0
    '    isF = 0
    '    isNo = 0


    '    dt = ctlLG.Requirements_GetByLocationSubjectCode(Year, LID, SubjectCode)

    '    If dt.Rows.Count > 0 Then
    '        With grdREQ
    '            .Visible = True
    '            .DataSource = dt
    '            .DataBind()
    '        End With

    '        For i = 0 To dt.Rows.Count - 1
    '            If DBNull2Zero(dt.Rows(i)("NoSpec")) > 0 Then
    '                isNo = isNo + 1
    '            End If

    '            If DBNull2Zero(dt.Rows(i)("Man")) > 0 Then
    '                isM = isM + 1
    '            End If
    '            If DBNull2Zero(dt.Rows(i)("Women")) > 0 Then
    '                isF = isF + 1
    '            End If

    '            If (DBNull2Zero(dt.Rows(i)("NoSpec")) + DBNull2Zero(dt.Rows(i)("Man")) + DBNull2Zero(dt.Rows(i)("Women"))) > 0 Then
    '                ddlPhase.Items.Add(dt.Rows(i)("PhaseNo") & " : " & dt.Rows(i)("PhaseName"))
    '                ddlPhase.Items(k).Value = dt.Rows(i)("TimePhaseID")
    '                k = k + 1
    '            End If

    '        Next

    '        'If HasSelected() Then
    '        '    lnkSelect.Visible = False
    '        '    lblMsg.Text = "ท่านได้เลือกแหล่งฝึกนี้ ให้กับวิชาที่ท่านต้องการเรียบร้อยแล้ว"
    '        '    lblMsg.Visible = True
    '        '    ddlPhase.Visible = False
    '        '    lblselect.Visible = False
    '        'Else

    '        If (isM + isF + isNo) <= 0 Then
    '            ddlPhase.Visible = False
    '            lblselect.Visible = False
    '            lnkSelect.Visible = False
    '            lblMsg.Visible = True
    '            lblMsg.Text = "แหล่งฝึกนี้ไม่รับนิสิต"

    '            If sSex = "M" And isM <= 0 Then
    '                lblMsg.Text = "แหล่งฝึกนี้ไม่รับนิสิต เพศ" & "ชาย"

    '            ElseIf sSex = "F" And isF <= 0 Then
    '                lblMsg.Text = "แหล่งฝึกนี้ไม่รับนิสิต เพศ" & "หญิง"
    '            End If

    '        Else
    '            'มีการรับนิสิต
    '            ddlPhase.Visible = True
    '            lblselect.Visible = True
    '            lnkSelect.Visible = True
    '            lblMsg.Visible = False

    '            If isNo > 0 Then
    '                ddlPhase.Visible = True
    '                lblselect.Visible = True
    '                lnkSelect.Visible = True
    '                lblMsg.Visible = False
    '            ElseIf sSex = "M" And isM <= 0 Then
    '                lblMsg.Text = "แหล่งฝึกนี้ไม่รับนิสิต เพศ" & "ชาย"
    '                ddlPhase.Visible = False
    '                lblselect.Visible = False
    '                lnkSelect.Visible = False
    '                lblMsg.Visible = True
    '            ElseIf sSex = "F" And isF <= 0 Then
    '                lblMsg.Text = "แหล่งฝึกนี้ไม่รับนิสิต เพศ" & "หญิง"
    '                ddlPhase.Visible = False
    '                lblselect.Visible = False
    '                lnkSelect.Visible = False
    '                lblMsg.Visible = True
    '            End If
    '        End If

    '        'End If


    '        If Request("r") = "yes" Then
    '            ddlPhase.Visible = False
    '            lblselect.Visible = False
    '            lnkSelect.Visible = False
    '            lblMsg.Visible = False
    '        End If

    '    Else
    '        grdREQ.Visible = True

    '    End If

    '    dtA = Nothing
    'End Sub

    Private Sub ClearData()
        hdLocationID.Value = ""
        lblName.Text = ""
        lblAddress.Text = ""
        lblProvince.Text = ""
        Me.lblZipCode.Text = ""

        lblCoName.Text = ""
        lblCoMail.Text = ""
        lblCoPosition.Text = ""
        lblCoTel.Text = ""
        lblTel.Text = ""
        lblFax.Text = ""
        lblDepartment.Text = ""
        lblField.Text = ""
        lblOfficeHour.Text = ""
        lblOfficerNo.Text = ""



        lblDepartment.Text = ""
        lblISO.Text = ""



        'lblDepartment.Visible = False
        'lblField.Visible = False

        'lblType.Visible = False

        'lblField.Visible = False
        'lblDepartment.Visible = False

        'lblOpen.Visible = False
        'lblOfficer.Visible = False
        'lblOfficeHour.Visible = False
        'lblOfficerNo.Visible = False

    End Sub
    Dim acc As New UserController
    ' Dim ctlAss As New AssessmentController
    'Private Sub SelectLocationToBusket(RegYear, Student_Code, LocationID, SubjectCode)
    '    Dim iCount As Integer = 0
    '    Dim iQuota As Integer
    '    Dim ctlCfg As New SystemConfigController
    '    iCount = ctlReg.GetStudentRegister_Count(RegYear, Student_Code, SubjectCode)
    '    iQuota = ctlCfg.SystemConfig_GetByCode(CFG_MAXLOCATION)
    '    If iCount < iQuota Then
    '        ' If ctlReg.StudentRegister_CheckDup(RegYear, Student_Code, StrNull2Zero(ddlPhase.SelectedValue)) <= 0 Then
    '        ctlReg.StudentRegister_Add(RegYear, Student_Code, iCount + 1, LocationID, SubjectCode, StrNull2Zero(ddlPhase.SelectedValue), Request.Cookies("UserLogin").Value.Value)

    '        acc.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_ADD, "StudentRegister", "นิสิตเลือกแหล่งฝึก LID:" & LocationID & ">>" & Student_Code, "วิชา :" & SubjectCode)

    '        Response.Redirect("StudentLocation_Manage.aspx?m=2&p=202")
    '        ' ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกเรียบร้อยแล้ว")
    '        'LoadData(Request("y"), Request("lid"))
    '        'Else
    '        '     ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ท่านได้เลือกผลัดฝึกซ้ำ กับที่ท่านได้เลือกไว้ก่อนหน้านี้แล้ว")
    '        'End If
    '    Else
    '         ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ท่านได้เลือกแหล่งฝึกสำหรับวิชานี้ ครบตามจำนวนแล้ว ท่านไม่สามารถเลือกเพิ่มได้อีก")
    '    End If

    'End Sub

    Protected Sub lnkSelect_Click(sender As Object, e As EventArgs) Handles lnkSelect.Click
        'SelectLocationToBusket(Request("y"), Request.Cookies("ProfileID").Value, Request("lid"), Request("subid"))
    End Sub

    'Function HasSelected() As Boolean
    '    Dim iCount As Integer = 0
    '    iCount = ctlReg.GetStudentRegister_CountSelected(Request("y"), Request.Cookies("ProfileID").Value, Request("subid"), Request("lid"))

    '    If iCount > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
End Class

