Public Class RequirementsReg
    Inherits System.Web.UI.Page

    Dim ctlLG As New LocationController
    Dim ctlReq As New REQcontroller

    Dim objLct As New LocationInfo
    Dim dt As New DataTable
    Dim ds As New DataSet

    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblID.Text = ""
            lblValidate.Visible = False

            LoadYearToDDL()
            LoadLocationToDDL()

            ClearData()

            If Not Request("y") Is Nothing Then
                ddlLocation.SelectedValue = Request("id")
                ddlYear.SelectedValue = Request("y")
                lblID.Text = ddlLocation.SelectedValue
                LoadRequirements(Request("y"), Request("id"))
            End If



        End If

        'txtPay.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        ' txtConfirm.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub

    Private Sub LoadRequirements(sYear As Integer, sLocationID As Integer)
        Dim ctlTP As New TimePhaseController
        Dim ctlC As New CourseController

        Dim dtReq As New DataTable

        dtReq = ctlC.CourseLocation_GetByLocation(sYear, sLocationID)

        If dtReq.Rows.Count > 0 Then
            With grdSubject
                .Visible = True
                .DataSource = dtReq
                .DataBind()
            End With
            lblValidate.Visible = False

            For i = 0 To grdSubject.Rows.Count - 1
                Dim grdT As New GridView
                grdT = grdSubject.Rows(i).Cells(0).FindControl("grdREQ")
                dt = ctlReq.Requirements_GetByLocationCourse(sYear, sLocationID, grdSubject.DataKeys(i).Value)

                If dt.Rows.Count > 0 Then
                    With grdT
                        .Visible = True
                        .DataSource = dt
                        .DataBind()
                    End With
                    lblValidate.Visible = False
                Else
                    grdT.Visible = False
                    lblValidate.Visible = True
                    lblValidate.Text = "กรุณากำหนดผลัดฝึกของรายวิชาที่เปิดฝึกก่อน"
                End If
            Next
        Else
            lblValidate.Visible = True
            lblValidate.Text = "กรุณากำหนดรายวิชาให้หน่วยงานก่อน"
            grdSubject.Visible = False
        End If

        optGender.SelectedValue = ctlLG.Location_GetIsSameGender(sLocationID)

    End Sub

    Private Sub LoadYearToDDL()

        Dim y As Integer = StrNull2Zero(DisplayYear(ctlReq.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlReq.REQ_GetYear(Request.Cookies("LANG").Value)
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "YearText"
                .DataValueField = "YearValue"
                .DataBind()

                If dt.Rows(LastRow)(0) = y Then
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 1).Value = y + 1
                ElseIf dt.Rows(LastRow)(0) > y Then
                    'ddlYear.Items.Add(y + 2)
                    'ddlYear.Items(LastRow + 1).Value = y + 2
                ElseIf dt.Rows(LastRow)(0) < y Then
                    ddlYear.Items.Add(y)
                    ddlYear.Items(LastRow + 1).Value = y
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 2).Value = y + 1
                End If
                .SelectedIndex = 0
            End With
        Else
            ddlYear.Items.Add(y)
            ddlYear.Items(0).Value = y
            ddlYear.Items.Add(y + 1)
            ddlYear.Items(1).Value = y + 1
            ddlYear.SelectedIndex = 0
        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        dt = Nothing
    End Sub

    Private Sub LoadLocationToDDL()
        If txtFindLocation.Text = "" Then
            dt = ctlLG.Location_Get
        Else
            dt = ctlLG.Location_GetBySearch(txtFindLocation.Text)
        End If

        ddlLocation.Items.Clear()

        With ddlLocation

            .Items.Add("---เลือกแหล่งฝึก---")
            .Items(0).Value = 0
            If dt.Rows.Count > 0 Then
                .Visible = True
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add(dt.Rows(i)(objLct.tblField(objLct.fldPos.f01_LocationName).fldName))
                    .Items(i + 1).Value = dt.Rows(i)(objLct.tblField(objLct.fldPos.f00_LocationID).fldName)
                Next
                .SelectedIndex = 0
            End If
        End With
    End Sub

    Protected Function validateData() As Boolean
        Dim result As Boolean = True
        lblValidate.Text = ""


        'If lblID.Text = "" Then
        '    If optDay.SelectedIndex = -1 Then
        '        result = False
        '        lblValidate.Text &= "- กรุณาเลือกประเภทแหล่งฝึก  <br />"
        '        lblValidate.Visible = True
        '    Else
        '        If optTime.SelectedIndex = -1 Then
        '            result = False
        '            lblValidate.Text &= "- กรุณาเลือกประเภท" & optDay.SelectedItem.Text & "  <br />"
        '            lblValidate.Visible = True
        '        ElseIf optTime.SelectedIndex = 4 Then
        '            If txtDay.Text = "" Then
        '                result = False
        '                lblValidate.Text &= "- กรุณาระบุสังกัด  <br />"
        '                lblValidate.Visible = True
        '            End If
        '        End If
        '    End If
        'End If

        If ddlLocation.SelectedValue = "0" Then
            result = False
            lblValidate.Text &= "- กรุณาระบุแหล่งฝึก  <br />"
            lblValidate.Visible = True

        End If



        'Dim n As Integer = 0
        'For i = 0 To optGroup.Items.Count - 1
        '    If optGroup.Items(i).Selected Then
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

        'txtTopWork.Text = ""

        'txtApartmentName.Text = ""
        'txtRemark.Text = ""
        'txtTravel.Text = ""
        'txtDistance.Text = ""
        'txtApartmentTel.Text = ""
        'txtApartmentAddress.Text = ""
        'txtDay.Text = ""
        'txtBrunchDetail.Text = ""
        'txtOfficeTime.Text = ""

        'txtBrunchDetail.Visible = False
        'txtDay.Visible = True
        'txtOfficeTime.Visible = True

        'pnRes1.Visible = False
        'pnRes4.Visible = False
        'pnWorkBrunch.Visible = False
        'pnWorkList.Visible = True
        'lblStep5.Text = "งานที่สามารถให้นิสิตฝึกปฏิบัติได้"

        'chkWorkList.ClearSelection()
        'For i = 0 To chkWorkList.Items.Count - 1
        '    chkWorkList.Items(i).Selected = False
        'Next

        'For i = 0 To grdREQ.Rows.Count - 1
        '    Dim txtM As TextBox = grdREQ.Rows(i).Cells(2).FindControl("txtMale")
        '    Dim txtF As TextBox = grdREQ.Rows(i).Cells(3).FindControl("txtFemale")
        '    Dim txtN As TextBox = grdREQ.Rows(i).Cells(4).FindControl("txtAll")

        '    txtM.Text = ""
        '    txtF.Text = ""
        '    txtN.Text = ""
        'Next

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If validateData() Then

            lblValidate.Visible = False
            Dim ctlL As New LocationController
            ctlLG.Location_UpdateisSameGender(ddlLocation.SelectedValue, optGender.SelectedValue)

            '    acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Locations", "แก้ไข ความต้องการ:" & ddlLocation.SelectedValue, "")


            SaveREQ_Account()
            'ClearData()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)

        Else

        End If

    End Sub

    Private Sub SaveREQ_Account()

        For n = 0 To grdSubject.Rows.Count - 1
            Dim grdREQ As New GridView
            grdREQ = grdSubject.Rows(n).Cells(0).FindControl("grdREQ")

            For i = 0 To grdREQ.Rows.Count - 1
                Dim txtM As TextBox = grdREQ.Rows(i).Cells(2).FindControl("txtMale")
                Dim txtF As TextBox = grdREQ.Rows(i).Cells(3).FindControl("txtFemale")
                Dim txtN As TextBox = grdREQ.Rows(i).Cells(4).FindControl("txtAll")

                If Not isDupicate_Account(grdREQ.DataKeys(i).Value) Then
                    ctlReq.Requirements_Add(ddlYear.SelectedValue, ddlLocation.SelectedValue, grdREQ.DataKeys(i).Value, optGender.SelectedValue, StrNull2Zero(txtM.Text), StrNull2Zero(txtF.Text), StrNull2Zero(txtN.Text))
                Else
                    ctlReq.Requirements_Update(ddlYear.SelectedValue, ddlLocation.SelectedValue, grdREQ.DataKeys(i).Value, optGender.SelectedValue, StrNull2Zero(txtM.Text), StrNull2Zero(txtF.Text), StrNull2Zero(txtN.Text))
                End If
            Next

        Next
    End Sub
    Function isDupicate_Account(pID As Integer) As Boolean
        dt = ctlReq.Requirements_Get(ddlYear.SelectedValue, ddlLocation.SelectedValue, pID)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Function isDupicate_REQ() As Boolean
        dt = ctlReq.REQ_GetByPKKey(ddlYear.SelectedValue, ddlLocation.SelectedValue, "")
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        lblID.Text = ddlLocation.SelectedValue
        LoadRequirements(ddlYear.SelectedValue, ddlLocation.SelectedValue)
        'EditData(ddlYear.SelectedValue, ddlLocation.SelectedValue, "ddlSubject.SelectedValue")
    End Sub


    'Private Sub CheckResidence(pID As Integer)

    '    pnRes1.Visible = False
    '    pnRes4.Visible = False

    '    optResidence1.Checked = False
    '    optResidence2.Checked = False
    '    optResidence3.Checked = False
    '    optResidence4.Checked = False

    '    Select Case pID
    '        Case 1
    '            pnRes1.Visible = True
    '        Case 4
    '            pnRes4.Visible = True
    '        Case Else
    '            pnRes1.Visible = False
    '            pnRes4.Visible = False
    '    End Select
    'End Sub
    'Protected Sub optResidence1_CheckedChanged(sender As Object, e As EventArgs) Handles optResidence1.CheckedChanged
    '    CheckResidence(1)
    '    optResidence1.Checked = True
    'End Sub
    'Protected Sub optResidence2_CheckedChanged(sender As Object, e As EventArgs) Handles optResidence2.CheckedChanged
    '    CheckResidence(2)
    '    optResidence2.Checked = True
    'End Sub

    'Protected Sub optResidence3_CheckedChanged(sender As Object, e As EventArgs) Handles optResidence3.CheckedChanged
    '    CheckResidence(3)
    '    optResidence3.Checked = True
    'End Sub
    'Protected Sub optResidence4_CheckedChanged(sender As Object, e As EventArgs) Handles optResidence4.CheckedChanged
    '    CheckResidence(4)
    '    optResidence4.Checked = True
    'End Sub

    Private Sub lnkFind_Click(sender As Object, e As System.EventArgs) Handles lnkFind.Click
        LoadLocationToDDL()
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadData()
    End Sub

    Private Sub LoadData()
        lblID.Text = ddlLocation.SelectedValue
        LoadRequirements(ddlYear.SelectedValue, ddlLocation.SelectedValue)
    End Sub

End Class

