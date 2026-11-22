
Public Class TurnPhase
    Inherits System.Web.UI.Page

    Dim ctlLG As New TimePhaseController
    Dim dt As New DataTable
    Dim ds As New DataSet

    Dim acc As New UserController
    Dim ctlCs As New Coursecontroller

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Request.Cookies("Username").Value Is Nothing Then
        '    Response.Redirect("Default.aspx?logout=YES")
        'End If
        If Not IsPostBack Then
            lblID.Text = ""
            LoadYearToDDL()
            LoadTurnPhaseToGrid(StrNull2Zero(ddlYearFind.SelectedValue))
        End If

        txtPhaseNo.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        txtDayCount.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")

    End Sub

    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlCs.Courses_GetYear
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "YearText"
                .DataValueField = "YearValue"
                .DataBind()
            End With

            With ddlYearFind
                .Enabled = True
                .DataSource = dt
                .DataTextField = "YearText"
                .DataValueField = "YearValue"
                .DataBind()
            End With



            If dt.Rows(LastRow)(0) = y Then
                ddlYear.Items.Add(y + 1)
                ddlYear.Items(LastRow + 1).Value = y + 1

                ddlYearFind.Items.Add(y + 1)
                ddlYearFind.Items(LastRow + 1).Value = y + 1

            ElseIf dt.Rows(LastRow)(0) > y Then
                'ddlYear.Items.Add(y + 2)
                'ddlYear.Items(LastRow + 1).Value = y + 2
            ElseIf dt.Rows(LastRow)(0) < y Then
                ddlYear.Items.Add(y)
                ddlYear.Items(LastRow + 1).Value = y
                ddlYear.Items.Add(y + 1)
                ddlYear.Items(LastRow + 2).Value = y + 1


                ddlYearFind.Items.Add(y)
                ddlYearFind.Items(LastRow + 1).Value = y
                ddlYearFind.Items.Add(y + 1)
                ddlYearFind.Items(LastRow + 2).Value = y + 1

            End If
            ddlYear.SelectedIndex = 0
            ddlYearFind.SelectedIndex = 0
        Else
            ddlYear.Items.Add(y)
            ddlYear.Items(0).Value = y
            ddlYear.Items.Add(y + 1)
            ddlYear.Items(1).Value = y + 1
            ddlYear.SelectedIndex = 0

            ddlYearFind.Items.Add(y)
            ddlYearFind.Items(0).Value = y
            ddlYearFind.Items.Add(y + 1)
            ddlYearFind.Items(1).Value = y + 1
            ddlYearFind.SelectedIndex = 0

        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        ddlYearFind.SelectedValue = Request.Cookies("EDUYEAR").Value
        dt = Nothing
    End Sub

    Private Sub LoadTurnPhaseToGrid(EduYear As Integer)

        dt = ctlLG.TurnPhase_GetByYear(EduYear)

        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()

            For i = 0 To .Rows.Count - 1
                .Rows(i).Cells(0).Text = i + 1
            Next

        End With

    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If ctlLG.TurnPhase_Delete(e.CommandArgument) Then

                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "TurnPhase", "ลบผลัดฝึก:" & txtPhaseNo.Text & ">>" & txtDesc.Text, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadTurnPhaseToGrid(StrNull2Zero(ddlYearFind.SelectedValue))
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub


    Private Sub EditData(ByVal pID As String)
        dt = ctlLG.TurnPhase_ByID(pID)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.lblID.Text = DBNull2Str(pID)
                ddlYear.SelectedValue = DBNull2Str(dt.Rows(0)("EduYear"))
                Me.txtPhaseNo.Text = DBNull2Str(dt.Rows(0)("PhaseNo"))
                txtDesc.Text = DBNull2Str(dt.Rows(0)("Descriptions"))
                txtStartDate.Text = DBNull2Str(dt.Rows(0)("StartDate"))
                txtEndDate.Text = DBNull2Str(dt.Rows(0)("EndDate"))
                txtDayCount.Text = DBNull2Str(dt.Rows(0)("DayCount"))
                chkStatus.Checked = CBool(dt.Rows(0)("StatusFlag"))



            End With
        End If
        dt = Nothing
    End Sub
    Private Sub ClearData()
        Me.lblID.Text = ""
        Me.txtPhaseNo.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""
        txtDesc.Text = ""
        chkStatus.Checked = True
        txtDayCount.Text = ""

    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(7).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtStartDate.Text = "" Or txtEndDate.Text = "" Then

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If
        Dim item As Integer
        Dim sName, Bdate, Edate As String
        Bdate = SetStrDate2DBDateFormat(txtStartDate.Text)
        Edate = SetStrDate2DBDateFormat(txtEndDate.Text)

        If Right(txtStartDate.Text, 4) = Right(txtEndDate.Text, 4) Then
            sName = DisplayLongDateNoYearTH(txtStartDate.Text) + " - " + DisplayLongDateTH(txtEndDate.Text)
        Else
            sName = DisplayLongDateTH(txtStartDate.Text) + " - " + DisplayLongDateTH(txtEndDate.Text)
        End If

        If lblID.Text = "" Then

            item = ctlLG.TurnPhase_Add(ddlYear.SelectedValue, txtPhaseNo.Text, sName, txtDesc.Text, Bdate, Edate, StrNull2Zero(txtDayCount.Text), Boolean2Decimal(chkStatus.Checked))
        Else
            item = ctlLG.TurnPhase_Update(lblID.Text, ddlYear.SelectedValue, txtPhaseNo.Text, sName, txtDesc.Text, Bdate, Edate, StrNull2Zero(txtDayCount.Text), Boolean2Decimal(chkStatus.Checked))

        End If


        LoadTurnPhaseToGrid(StrNull2Zero(ddlYearFind.SelectedValue))
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)

        txtDayCount.Text = ""
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        ddlYearFind.SelectedValue = ddlYear.SelectedValue
        LoadTurnPhaseToGrid(StrNull2Zero(ddlYear.SelectedValue))
    End Sub

    Protected Sub ddlYearFind_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYearFind.SelectedIndexChanged
        LoadTurnPhaseToGrid(StrNull2Zero(ddlYearFind.SelectedValue))
    End Sub
End Class

