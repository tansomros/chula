
Public Class SupervisorLocation
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlSpv As New SupervisorController
    Dim ctlCs As New Coursecontroller
    Dim ctlS As New SupervisionController
    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            Dim ctlCfg As New SystemConfigController
            lblYear.Text = ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR)
            txtDate.Text = Today.Date()
            lblNoPhase.Visible = False
            'LoadYearToDDL()
            LoadCourseToDDL()
            LoadProvinceToDDL()
            LoadLocationToDDL()
            LoadTimePhase()

            LoadSupervisorDataToGrid()

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

    'Private Sub LoadYearToDDL()
    '    Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
    '    Dim LastRow As Integer
    '    dt = ctlCs.Courses_GetYear
    '    LastRow = dt.Rows.Count - 1

    '    If dt.Rows.Count > 0 Then
    '        With ddlYear
    '            .Enabled = True
    '            .DataSource = dt
    '           .DataTextField = "YearText"
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
        If sKey = "ALL" Then
            dt = ctlS.TimePhase_GetALL4Supervisor(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlCourse.SelectedValue), StrNull2Zero(ddlLocation.SelectedValue))
        Else
            dt = ctlS.TimePhase_Get4Supervisor(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlCourse.SelectedValue), StrNull2Zero(ddlLocation.SelectedValue))
        End If

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

        dt = ctlSpv.Supervisor_GetByPerson(StrNull2Zero(lblYear.Text), Request.Cookies("ProfileID").Value)

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

        If StrNull2Zero(lblUID.Text) = 0 Then
            If Not CheckDuplicate() Then

                item = ctlSpv.Supervisor_Add(lblYear.Text, ddlCourse.SelectedValue, Request.Cookies("ProfileID").Value, ddlLocation.SelectedValue, optTimePhase.Value, ParseDateToSQL(txtDate.Text), ddlTime.SelectedValue, optTravel.Value, Request.Cookies("Username").Value)

                acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Supervisor", "เลือกสถานที่นิเทศ ปีการศึกษา " & lblYear.Text & " วิชา " & ddlCourse.SelectedItem.Text & "ผลัด " & optTimePhase.Value & " " & ddlLocation.SelectedValue, "")

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านเลือกวันและเวลาซ้ำ');", True)
            End If

        Else
            'บันทึกแก้ไข


        End If
        ClearData()
        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub
    Private Function CheckDuplicate() As Boolean
        Return ctlSpv.Supervisor_CheckDup(lblYear.Text, ddlCourse.SelectedValue, Request.Cookies("ProfileID").Value, ParseDateToSQL(txtDate.Text), ddlTime.SelectedValue)
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
                        LoadSupervisorDataToGrid()
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
        ClearData()
    End Sub

    Private Sub ClearData()
        lblUID.Text = ""
        lblLocation.Text = ""
        lblLocation.Visible = False
        ddlLocation.Visible = True
        txtDate.Text = DisplayShortDateTH(ctlSpv.GET_DATE_SERVER)

        lblNoPhase.Visible = False
        LoadCourseToDDL()
        LoadProvinceToDDL()
        LoadLocationToDDL()
        LoadTimePhase()

        LoadSupervisorDataToGrid()
    End Sub
    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged

        grdData.PageIndex = 0
        LoadProvinceToDDL()
        LoadLocationToDDL()
        LoadTimePhase()
        LoadSupervisorDataToGrid()
    End Sub

    Protected Sub ddlProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvince.SelectedIndexChanged
        LoadLocationToDDL()
        LoadTimePhase()
        LoadSupervisorDataToGrid()
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        LoadTimePhase()
        LoadSupervisorDataToGrid()
    End Sub
End Class

