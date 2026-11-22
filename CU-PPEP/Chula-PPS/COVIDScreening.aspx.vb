
Public Class COVIDScreening
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlStd As New StudentController
    Dim objUser As New UserController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            isAdd = True
            ClearData()

            lblStudentCode.Text = Request.Cookies("ProfileID").Value
            lblStudentName.Text = Request.Cookies("NameOfUser").Value
            lblStudentName2.Text = Request.Cookies("NameOfUser").Value
            LoadStudentData()

            If Not Request("uid") Is Nothing Then
                EditData(Request("uid"))
            End If
            If Request("t") = "view" Then
                cmdSave.Visible = False
            Else
                cmdSave.Visible = True
            End If
            LoadPhaseToDDL()
            LocationData()
        End If

    End Sub
    Private Sub LoadPhaseToDDL()
        Dim dtMajor As New DataTable
        dtMajor = ctlStd.StudentPhase_Get(Request.Cookies("EDUYEAR").Value, lblStudentCode.Text)
        If dtMajor.Rows.Count > 0 Then
            With ddlPhase
                .Enabled = True
                .DataSource = dtMajor
                .DataTextField = "PhaseNo"
                .DataValueField = "PhaseNo"
                .DataBind()
                .SelectedIndex = 0
            End With
        Else

        End If
        dtMajor = Nothing
    End Sub

    Dim acc As New UserController

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If lblStudentCode.Text = "" Or lblStudentName.Text = "" Then
            DisplayMessage(Me.Page, "กรุณป้อนข้อมูลให้ครบถ้วน")
            Exit Sub
        End If

        ctlStd.COVIDScreening_Save(DBNull2Zero(hdUID.Value), lblStudentCode.Text, StrNull2Zero(ddlPhase.SelectedValue), hdLocationID.Value, optSymptom1.SelectedValue, ConvertBoolean2YN(chkSymptom2.Checked), ConvertBoolean2YN(chkSymptom3.Checked), ConvertBoolean2YN(chkSymptom4.Checked), ConvertBoolean2YN(chkSymptom5.Checked), ConvertBoolean2YN(chkNoSymptom.Checked), optTravel.SelectedValue, txtTravelDesc.Text, txtStartDate.Text, txtEndDate.Text, optHistory.SelectedValue, txtHistoryDesc.Text, Request.Cookies("UserID").Value)


        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Students", "แบบรายงานความเสี่ยงต่อการสัมผัสโรค COVID-19 :" & lblStudentName.Text & " |" & ddlPhase.SelectedValue & "|" & lblLocationName.Text, "")

        isAdd = True
        DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")
    End Sub
    Private Sub ClearData()

    End Sub

    Private Sub EditData(ByVal pID As String)

        ClearData()

        dt = ctlStd.COVIDScreening_GetByUID(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                hdUID.Value = .Item("UID")
                lblStudentCode.Text = .Item("StudentCode")
                Me.lblStudentName.Text = String.Concat(.Item("StudentName"))
                Me.lblStudentName2.Text = String.Concat(.Item("StudentName"))
                hdLocationID.Value = String.Concat(.Item("LocationID"))
                lblLocationName.Text = String.Concat(.Item("LocationName"))
                ddlPhase.SelectedValue = .Item("PhaseNo")
                optSymptom1.SelectedValue = .Item("IsSymptom1")
                chkSymptom2.Checked = ConvertYN2Boolean(.Item("IsSymptom2"))
                chkSymptom3.Checked = ConvertYN2Boolean(.Item("IsSymptom3"))
                chkSymptom4.Checked = ConvertYN2Boolean(.Item("IsSymptom4"))
                chkSymptom5.Checked = ConvertYN2Boolean(.Item("IsSymptom5"))

                chkNoSymptom.Checked = ConvertYN2Boolean(.Item("IsNoSymptom"))

                optTravel.SelectedValue = .Item("IsTravel")
                txtTravelDesc.Text = .Item("TravelDesc")
                txtStartDate.Text = .Item("TravelStart")
                txtEndDate.Text = .Item("TravelEnd")

                optHistory.SelectedValue = .Item("IsHistory")
                txtHistoryDesc.Text = .Item("HistoryDesc")

            End With

        Else
            lblStudentCode.Text = Request.Cookies("ProfileID").Value
            lblStudentName.Text = Request.Cookies("NameOfUser").Value
            lblStudentName2.Text = Request.Cookies("NameOfUser").Value
        End If

        dt = Nothing
    End Sub

    Private Sub LoadStudentData()

        dt = ctlStd.GetStudentBio_ByID(lblStudentCode.Text)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                lblStudentCode.Text = .Item("Student_Code")
                lblDepartment.Text = String.Concat(.Item("MajorName")) & " คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย"
            End With

        Else
            lblStudentCode.Text = ""
            lblStudentName.Text = ""
            lblStudentName2.Text = ""
        End If

        dt = Nothing
    End Sub

    Protected Sub ddlPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPhase.SelectedIndexChanged
        LocationData()
    End Sub
    Private Sub LocationData()
        dt = ctlStd.StudentPhase_GetLocation(Request.Cookies("EDUYEAR").Value, lblStudentCode.Text, ddlPhase.SelectedValue)
        If dt.Rows.Count > 0 Then
            hdLocationID.Value = dt.Rows(0)("LocationID")
            lblLocationName.Text = dt.Rows(0)("LocationName")
            lblPhaseTime.Text = dt.Rows(0)("PhaseTime")
        Else
            hdLocationID.Value = ""
            lblLocationName.Text = ""
        End If
        dt = Nothing
    End Sub
End Class