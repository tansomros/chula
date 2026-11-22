
Public Class SupervisorSelect
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    'Dim ctlSpv As New SupervisorController
    'Dim ctlCs As New CourseController
    Dim ctlSv As New SupervisionController
    Dim acc As New UserController
    Dim dtSPV As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            Dim ctlCfg As New SystemConfigController
            lblYear.Text = ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR)
            txtDate.Text = Today.Date()

            LoadProvinceToDDL()
            LoadTimePhase()

            LoadLocationDataToGrid()

            dtSPV.Columns.Add("PersonID")
            dtSPV.Columns.Add("LocationID")
            dtSPV.Columns.Add("LocationName")
            dtSPV.Columns.Add("LocationGroupName")
            dtSPV.Columns.Add("ProvinceName")
            dtSPV.Columns.Add("PhaseName")

            Session("dtSPV") = dtSPV

        End If

    End Sub
    Private Sub LoadProvinceToDDL()
        dt = ctlSv.Province_GetFromAssessment(StrNull2Zero(lblYear.Text))
        ddlProvince.Items.Clear()
        If dt.Rows.Count > 0 Then
            ddlProvince.Items.Add("---ทั้งหมด---")
            ddlProvince.Items(0).Value = "0"

            For i = 1 To dt.Rows.Count
                With ddlProvince
                    .Items.Add(dt.Rows(i - 1)("ProvinceName"))
                    .Items(i).Value = dt.Rows(i - 1)("ProvinceID")

                End With
            Next
            ddlProvince.SelectedIndex = 0
        End If
        dt = Nothing
    End Sub

    Private Sub LoadTimePhase(Optional sKey As String = "")
        Dim ctlTP As New TimePhaseController

        dt = ctlTP.Phase_GetByYear(StrNull2Zero(lblYear.Text))

        If dt.Rows.Count > 0 Then
            With ddlPhase
                .Visible = True
                .DataSource = dt
                .DataTextField = "PhaseName"
                .DataValueField = "PhaseID"
                .DataBind()
                .SelectedIndex = 0
            End With
        Else
            ddlPhase.DataSource = Nothing
            ddlPhase.Visible = False
        End If

    End Sub

    'Private Sub LoadSupervisorDataToGrid()

    '    dt = ctlSv.Supervisor_GetTeacher(StrNull2Zero(lblYear.Text), ddlCourse.SelectedValue, ddlLocation.SelectedValue, ddlPhase.Value)

    '    If dt.Rows.Count > 0 Then
    '        lblNOSpv.Visible = False
    '        With grdData
    '            .Visible = True
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '        dtSPV = dt
    '    Else
    '        lblNOSpv.Visible = True
    '        grdData.DataSource = Nothing
    '        grdData.Visible = False
    '    End If

    '    dt = Nothing
    'End Sub
    Private Sub LoadLocationDataToGrid()

        dt = ctlSv.SupervisionLocation_GetSearch(StrNull2Zero(lblYear.Text), ddlProvince.SelectedValue, StrNull2Zero(ddlPhase.SelectedValue), txtSearch.Text)

        If dt.Rows.Count > 0 Then

            lblNoSelect.Visible = False
            With grdLocation
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
            lblNoLocation.Visible = False
        Else
            lblNoLocation.Visible = True
            grdLocation.Visible = False
        End If
    End Sub

    Private Sub AddSupervisionPlan()

        Dim PlanCode, Travel As String
        PlanCode = ""

        PlanCode = ctlSv.RunningNumber_New(CODE_SUPERVISION, lblYear.Text)

        Travel = optTravel.Value.ToString()

        Dim item As Integer
        Dim CurrentDate, WorkDate As Date
        CurrentDate = ctlSv.GET_DATE_SERVER()
        WorkDate = ConvertStringDateToDate(txtDate.Text)

        If WorkDate <= CurrentDate Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านป้อนวันที่ไม่ถูกต้อง');", True)
            Exit Sub
        End If

        ctlSv.SupervisionPlan_Save(PlanCode, StrNull2Zero(lblYear.Text), Request.Cookies("UserID").Value, ParseDateToSQL(txtDate.Text), ddlTime.SelectedValue, Travel, txtRemark.Text, Request.Cookies("Username").Value)

        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "SupervisionPlan", "เลือกสถานที่นิเทศ", "ปี|" & lblYear.Text & "|PersonID|" & Request.Cookies("UserID").Value)

        'If StrNull2Zero(lblUID.Text) = 0 Then

        For i = 0 To grdData.Rows.Count - 1
            If Not CheckDuplicate(grdData.DataKeys(i).Value) Then
                item = ctlSv.SupervisionPlanDetail_Save(PlanCode, grdData.DataKeys(i).Value, Request.Cookies("Username").Value)
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านจัดสรรวันและเวลาซ้ำให้อาจารย์ ท่านใดท่านหนึ่ง กรุณาตรวจสอบ');", True)
            End If

        Next
        'Else
        '    'บันทึกแก้ไข


        'End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)

        Response.Redirect("SupervisionPlan.aspx")

    End Sub

    Private Function CheckDuplicate(PersonID As Integer) As Boolean
        'Return ctlSpv.Supervisor_CheckDup(lblYear.Text, ddlCourse.SelectedValue, PersonID, ParseDateToSQL(txtDate.Text), ddlTime.SelectedValue)
    End Function
    'Private Sub EditData(pUID As Integer)
    '    Dim dtE As New DataTable
    '    dtE = ctlSpv.Supervisor_GetByUID(pUID)
    '    If dtE.Rows.Count > 0 Then
    '        ddlCourse.SelectedValue = dtE.Rows(0)("CourseID")
    '        LoadProvinceToDDL()
    '        ddlProvince.SelectedValue = dtE.Rows(0)("ProvinceID")
    '        LoadLocationToDDL()
    '        ddlLocation.SelectedValue = dtE.Rows(0)("LocationID")

    '        lblLocation.Text = dtE.Rows(0)("LocationName")
    '        lblLocation.Visible = True
    '        ddlLocation.Visible = False

    '        LoadTimePhase("ALL")
    '        ddlPhase.Value = dtE.Rows(0)("TimephaseID").ToString()

    '        txtDate.Text = DisplayShortDateTH(dtE.Rows(0)("WorkDate"))
    '        ddlTime.SelectedValue = dtE.Rows(0)("WorkTime")
    '        optTravel.Value = dtE.Rows(0)("TravelBy")
    '        lblUID.Text = pUID


    '        LoadSupervisorDataToGrid()
    '    Else
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','Error!!")
    '    End If


    'End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(4).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        AddSupervisionPlan()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        txtDate.Text = ctlSv.GET_DATE_SERVER
        ddlTime.SelectedIndex = 0
        txtRemark.Text = ""
        LoadLocationDataToGrid()
    End Sub
    Protected Sub ddlProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvince.SelectedIndexChanged
        LoadTimePhase()
        LoadLocationDataToGrid()
    End Sub

    Protected Sub grdLocation_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdLocation.PageIndexChanging
        grdLocation.PageIndex = e.NewPageIndex

    End Sub

    Protected Sub grdLocation_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdLocation.RowCommand
        If TypeOf e.CommandSource Is WebControls.Button Then
            Dim ButtonPressed As WebControls.Button = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgSelect"
                    AddLocationToSelectGrid(Request.Cookies("UserID").Value, grdLocation.DataKeys(e.CommandArgument).Value, grdLocation.Rows(e.CommandArgument).Cells(1).Text, grdLocation.Rows(e.CommandArgument).Cells(2).Text, grdLocation.Rows(e.CommandArgument).Cells(3).Text, ddlPhase.SelectedItem.Text)
            End Select


        End If
    End Sub

    Private Sub AddLocationToSelectGrid(PersonID As Integer, LocationID As Integer, LocationName As String, LocationGroupName As String, ProvinceName As String, PhaseName As String)

        For i = 0 To dtSPV.Rows.Count - 1
            If LocationID = dtSPV.Rows(i)("LocationID") Then
                Exit Sub
            End If
        Next

        dtSPV = Session("dtSPV")

        Dim dr As DataRow = dtSPV.NewRow()
        dr(0) = PersonID
        dr(1) = LocationID
        dr(2) = LocationName
        dr(3) = LocationGroupName
        dr(4) = ProvinceName
        dr(5) = PhaseName

        dtSPV.Rows.Add(dr)

        With grdData
            .Visible = True
            .DataSource = dtSPV
            .DataBind()
        End With

        Session("dtSPV") = Nothing
        Session("dtSPV") = dtSPV
    End Sub

    Protected Sub grdLocation_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdLocation.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub


    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdLocation.PageIndex = 0

    End Sub


    Protected Sub grdData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"

                    dtSPV = Session("dtSPV")
                    dtSPV.Rows(e.CommandArgument).Delete()
                    Session("dtSPV") = Nothing
                    Session("dtSPV") = dtSPV

                    grdData.DataSource = Nothing
                    grdData.DataSource = dtSPV
                    grdData.DataBind()

                    lblCount.Text = dtSPV.Rows.Count.ToString()
            End Select


        End If
    End Sub

    Protected Sub ddlPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPhase.SelectedIndexChanged
        LoadLocationDataToGrid()
    End Sub
End Class

