
Public Class SupervisorManage
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlSpv As New SupervisorController
    Dim ctlCs As New Coursecontroller
    Dim ctlS As New SupervisionController
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

            'LoadYearToDDL()
            LoadCourseToDDL()
            LoadProvinceToDDL()
            LoadLocationToDDL()
            LoadTimePhase()

            LoadTeacher()

            LoadLocationDataToGrid()

            dtSPV.Columns.Add("PersonID")
            dtSPV.Columns.Add("PersonName")

            Session("dtSPV") = dtSPV

        End If

    End Sub
    Private Sub LoadProvinceToDDL()
        dt = ctlCs.Province_GetFromAssessment(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlCourse.SelectedValue))
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
    Private Sub LoadTeacher()
        Dim ctlPsn As New PersonController
        If Trim(txtSearchTeacher.Text) <> "" Then
            dt = ctlPsn.Person_GetSearch("T", txtSearchTeacher.Text)
        Else
            dt = ctlPsn.Person_GetByType("T")
        End If


        If dt.Rows.Count > 0 Then
            With grdTeacher
                .Visible = True
                .DataSource = dt
                .DataBind()

            End With
        Else
            grdTeacher.Visible = False
        End If
    End Sub


    'Private Sub LoadYearToDDL()
    '    Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
    '    Dim LastRow As Integer
    '    dt = ctlCs.Courses_GetYear
    '    LastRow = dt.Rows.Count - 1

    '    If dt.Rows.Count > 0 Then
    '        With ddlYear
    '            .Enabled = True
    ''            .DataSource = dt
    '.DataTextField = "YearText"
    '.DataValueField = "YearValue"
    '            .DataBind()

    '            If dt.Rows(LastRow)(0) = y Then
    '                ddlYear.Items.Add(y + 1)
    '                ddlYear.Items(LastRow + 1).Value = y + 1
    '            ElseIf dt.Rows(LastRow)(0) > y Then
    '                'ddlYear.Items.Add(y + 2)
    '                'ddlYear.Items(LastRow + 1).Value = y + 2
    '            ElseIf dt.Rows(LastRow)(0) < y Then
    '                ddlYear.Items.Add(y)
    '                ddlYear.Items(LastRow + 1).Value = y
    '                ddlYear.Items.Add(y + 1)
    '                ddlYear.Items(LastRow + 2).Value = y + 1
    '            End If
    '            .SelectedIndex = 0
    '        End With
    '    Else
    '        ddlYear.Items.Add(y)
    '        ddlYear.Items(0).Value = y
    '        ddlYear.Items.Add(y + 1)
    '        ddlYear.Items(1).Value = y + 1
    '        ddlYear.SelectedIndex = 0
    '    End If
    '    dt = Nothing
    'End Sub
    Private Sub LoadLocationToDDL()
        ddlLocation.Items.Clear()
        Dim dtL As New DataTable

        dtL = ctlS.Location_Get4Supervisor(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlCourse.SelectedValue), ddlProvince.SelectedValue)

        If dtL.Rows.Count > 0 Then
            ddlLocation.Items.Clear()

            With ddlLocation
                .DataSource = dtL
                .DataTextField = "LocationName"
                .DataValueField = "LocationID"
                .DataBind()
                .SelectedIndex = 0
            End With

        End If

    End Sub
    Private Sub LoadTimePhase(Optional sKey As String = "")
        Dim ctlTP As New TimePhaseController

        dt = ctlS.TimePhase_GetALL4Supervisor(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlCourse.SelectedValue), StrNull2Zero(ddlLocation.SelectedValue))

        If dt.Rows.Count > 0 Then
            With optTimePhase
                .Visible = True
                .DataSource = dt
                .TextField = "PhaseName"
                .ValueField = "TimePhaseID"
                .DataBind()
                .SelectedIndex = 0
            End With
            lblNoPhase.Visible = False
        Else
            optTimePhase.DataSource = Nothing
            optTimePhase.Visible = False
            lblNoPhase.Visible = True

            lblNoPhase.Text = "ไม่พบผลัดฝึกที่ท่านเลือก"

        End If

    End Sub
    Private Sub LoadSupervisorDataToGrid()

        dt = ctlSpv.Supervisor_GetTeacher(StrNull2Zero(lblYear.Text), ddlCourse.SelectedValue, ddlLocation.SelectedValue, optTimePhase.Value)

        If dt.Rows.Count > 0 Then
            lblNOSpv.Visible = False
            With grdSupervisor
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
            dtSPV = dt
        Else
            lblNOSpv.Visible = True
            grdSupervisor.DataSource = Nothing
            grdSupervisor.Visible = False
        End If

        dt = Nothing
    End Sub
    Private Sub LoadLocationDataToGrid()

        dt = ctlSpv.Supervisor_GetBySearch(StrNull2Zero(lblYear.Text), ddlCourse.SelectedValue, txtSearchLocation.Text)

        If dt.Rows.Count > 0 Then
            lblCount.Text = dt.Rows.Count
            lblNo.Visible = False
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With

        Else
            lblCount.Text = 0
            lblNo.Visible = True
            grdData.Visible = False
        End If
    End Sub

    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()

        dt = ctlCs.Courses_GetFromAssessment(lblYear.Text)

        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With ddlCourse
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("SubjectName"))
                    .Items(i).Value = dt.Rows(i)("CourseID")
                End With
            Next
            ddlCourse.SelectedIndex = 0
        End If

    End Sub


    Private Sub AddSupervisor()

        Dim item As Integer
        Dim CurrentDate, WorkDate As Date
        CurrentDate = ctlCs.GET_DATE_SERVER()
        WorkDate = ConvertStringDateToDate(txtDate.Text)

        If WorkDate <= CurrentDate Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านป้อนวันที่ไม่ถูกต้อง');", True)
            Exit Sub
        End If

        'If StrNull2Zero(lblUID.Text) = 0 Then

        For i = 0 To grdSupervisor.Rows.Count - 1
            If Not CheckDuplicate(grdSupervisor.DataKeys(i).Value) Then

                item = ctlSpv.Supervisor_Add(lblYear.Text, ddlCourse.SelectedValue, grdSupervisor.DataKeys(i).Value, ddlLocation.SelectedValue, optTimePhase.Value, ParseDateToSQL(txtDate.Text), ddlTime.SelectedValue, optTravel.Value, Request.Cookies("Username").Value)

                acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Supervisor", "จัดสรร อ.นิเทศ ปี " & lblYear.Text & " วิชา " & ddlCourse.SelectedItem.Text & "ผลัด " & optTimePhase.Value, ddlLocation.SelectedValue & "/PersonID:" & grdSupervisor.DataKeys(i).Value)

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
            Else

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านจัดสรรวันและเวลาซ้ำให้อาจารย์ ท่านใดท่านหนึ่ง กรุณาตรวจสอบ');", True)
            End If

        Next
        'Else
        '    'บันทึกแก้ไข


        'End If


        LoadLocationDataToGrid()

    End Sub
    Private Function CheckDuplicate(PersonID As Integer) As Boolean
        Return ctlSpv.Supervisor_CheckDup(lblYear.Text, ddlCourse.SelectedValue, PersonID, ParseDateToSQL(txtDate.Text), ddlTime.SelectedValue)
    End Function
    Private Sub EditData(pUID As Integer)
        Dim dtE As New DataTable
        dtE = ctlSpv.Supervisor_GetByUID(pUID)
        If dtE.Rows.Count > 0 Then
            ddlCourse.SelectedValue = dtE.Rows(0)("CourseID")
            LoadProvinceToDDL()
            ddlProvince.SelectedValue = dtE.Rows(0)("ProvinceID")
            LoadLocationToDDL()
            ddlLocation.SelectedValue = dtE.Rows(0)("LocationID")

            lblLocation.Text = dtE.Rows(0)("LocationName")
            lblLocation.Visible = True
            ddlLocation.Visible = False

            LoadTimePhase("ALL")
            optTimePhase.Value = dtE.Rows(0)("TimephaseID").ToString()

            txtDate.Text = DisplayShortDateTH(dtE.Rows(0)("WorkDate"))
            ddlTime.SelectedValue = dtE.Rows(0)("WorkTime")
            optTravel.Value = dtE.Rows(0)("TravelBy")
            lblUID.Text = pUID


            LoadSupervisorDataToGrid()
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','Error!!');", True)
        End If


    End Sub
    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument)

                Case "imgDel"
                    If ctlSpv.Supervisor_Delete(e.CommandArgument) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadLocationDataToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(8).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        AddSupervisor()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        lblUID.Text = ""
        lblLocation.Text = ""
        lblLocation.Visible = False
        ddlLocation.Visible = True
        txtDate.Text = ctlSpv.GET_DATE_SERVER
        LoadLocationToDDL()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged

        grdData.PageIndex = 0
        LoadProvinceToDDL()
        LoadLocationToDDL()
        LoadTimePhase()
        LoadSupervisorDataToGrid()
        LoadLocationDataToGrid()
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        LoadTimePhase()
        LoadSupervisorDataToGrid()
    End Sub



    Protected Sub ddlProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvince.SelectedIndexChanged
        LoadLocationToDDL()
        LoadTimePhase()

        LoadSupervisorDataToGrid()
    End Sub

    Protected Sub grdTeacher_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdTeacher.PageIndexChanging
        grdTeacher.PageIndex = e.NewPageIndex
        LoadTeacher()
    End Sub

    Protected Sub grdTeacher_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdTeacher.RowCommand
        If TypeOf e.CommandSource Is WebControls.LinkButton Then
            Dim ButtonPressed As WebControls.LinkButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgSelect"
                    AddTeacherToGrid(grdTeacher.DataKeys(e.CommandArgument).Value, grdTeacher.Rows(e.CommandArgument).Cells(1).Text)
            End Select


        End If
    End Sub

    Private Sub AddTeacherToGrid(PersonID As Integer, PersonName As String)
        dtSPV = Session("dtSPV")

        Dim dr As DataRow = dtSPV.NewRow()
        dr(0) = PersonID
        dr(1) = PersonName
        dtSPV.Rows.Add(dr)

        With grdSupervisor
            .Visible = True
            .DataSource = dtSPV
            .DataBind()
        End With

        Session("dtSPV") = Nothing
        Session("dtSPV") = dtSPV
    End Sub

    Protected Sub grdTeacher_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdTeacher.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub grdSupervisor_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdSupervisor.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            'Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            'Dim imgD As Image = e.Row.Cells(2).FindControl("imgDel")
            'imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdTeacher.PageIndex = 0
        LoadTeacher()
    End Sub

    Protected Sub btnSearchLocation_Click(sender As Object, e As EventArgs) Handles btnSearchLocation.Click
        LoadLocationDataToGrid()
    End Sub

    Protected Sub grdSupervisor_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdSupervisor.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"

                    dtSPV = Session("dtSPV")
                    dtSPV.Rows(e.CommandArgument).Delete()
                    Session("dtSPV") = Nothing
                    Session("dtSPV") = dtSPV

                    grdSupervisor.DataSource = Nothing
                    grdSupervisor.DataSource = dtSPV
                    grdSupervisor.DataBind()

            End Select


        End If
    End Sub

    Protected Sub optTimePhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optTimePhase.SelectedIndexChanged
        LoadSupervisorDataToGrid()
    End Sub
End Class

