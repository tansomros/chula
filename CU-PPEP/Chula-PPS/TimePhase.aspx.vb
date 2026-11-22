
Public Class TimePhase
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
            LoadPhaseToCheckList()
            LoadSubjectToDDL()
            LoadTimePhaseToGrid()
        End If

        'txtCode.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        'txtDayCount.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")

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
            ddlYear.SelectedIndex = 0
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
    Private Sub LoadPhaseToCheckList()
        dt = ctlLG.TurnPhase_GetByYear(StrNull2Zero(ddlYear.SelectedValue))
        With chkPhase
            .Visible = True
            .DataSource = dt
            .DataTextField = "PhaseDesc"
            .DataValueField = "PhaseID"
            .DataBind()
        End With
        dt = Nothing
    End Sub
    Private Sub LoadSubjectToDDL()
        Dim dtSubj As New DataTable
        dtSubj = ctlCs.Courses_GetByYear(ddlYear.SelectedValue)
        If dtSubj.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            For i = 0 To dtSubj.Rows.Count - 1
                With ddlCourse
                    .Items.Add("" & dtSubj.Rows(i)("SubjectCode") & " : " & dtSubj.Rows(i)("NameTH"))
                    .Items(i).Value = dtSubj.Rows(i)("CourseID")
                End With
            Next
        Else
            ddlCourse.Items.Clear()
            dtSubj = Nothing
        End If
    End Sub

    Private Sub LoadTimePhaseToGrid()

        dt = ctlLG.TimePhase_GetSubject(ddlYear.SelectedValue)

        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()

            For i = 0 To .Rows.Count - 1
                .Rows(i).Cells(0).Text = i + 1
                '.Rows(i).Cells(2).Text = dt.Rows(i)("SubjectCode") & ":" & dt.Rows(i)("SubjectName")
            Next

        End With

    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(grdData.Rows(e.CommandArgument).Cells(1).Text, grdData.DataKeys(e.CommandArgument).Value)
                Case "imgDel"
                    If ctlLG.TimePhase_DeleteByYearCourse(grdData.Rows(e.CommandArgument).Cells(1).Text, grdData.DataKeys(e.CommandArgument).Value) Then

                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "TimePhase", "ลบผลัดฝึก:" & ddlYear.SelectedValue & ">>" & ddlCourse.SelectedValue, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadTimePhaseToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub


    Private Sub EditData(ByVal pYear As Integer, CourseID As Integer)
        Dim dtTP As New DataTable
        dtTP = ctlLG.TimePhase_GetBySearch(pYear, CourseID)
        If dtTP.Rows.Count > 0 Then

            isAdd = False
            Me.lblID.Text = DBNull2Str("")
            ddlYear.SelectedValue = DBNull2Str(dtTP.Rows(0)("EduYear"))
            LoadSubjectToDDL()
            ddlCourse.SelectedValue = DBNull2Str(dtTP.Rows(0)("CourseID"))
            LoadPhaseToCheckList()

            For i = 0 To dtTP.Rows.Count - 1
                For n = 0 To chkPhase.Items.Count - 1
                    If dtTP.Rows(i)("PhaseID") = chkPhase.Items(n).Value Then
                        If dtTP.Rows(i)("isPublic") = 1 Then
                            chkPhase.Items(n).Selected = True
                        Else
                            chkPhase.Items(n).Selected = False
                        End If
                    End If
                Next
            Next
        End If
        dtTP = Nothing
    End Sub
    Private Sub ClearData()
        Me.lblID.Text = ""
        chkPhase.ClearSelection()
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(5).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim n As Integer = 0
        For i = 0 To chkPhase.Items.Count - 1
            If chkPhase.Items(i).Selected Then
                n = n + 1
            End If
        Next

        If n = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากำหนดผลัดฝึกที่ต้องการเปิดก่อน');", True)
            Exit Sub
        End If


        Dim item As Integer
        Dim isSelect As String = ""
        Dim PhaseID As Integer = 0


        For i = 0 To chkPhase.Items.Count - 1
            PhaseID = chkPhase.Items(i).Value
            isSelect = ConvertBoolean2YN(chkPhase.Items(i).Selected)

            item = ctlLG.TimePhase_Save(ddlYear.SelectedValue, ddlCourse.SelectedValue, PhaseID, isSelect)

        Next



        'If lblID.Text = "" Then

        '    item = ctlLG.TimePhase_Add(txtCode.Text, txtName.Text, txtDesc.Text, Boolean2Decimal(chkStatus.Checked), ddlYear.SelectedValue, ddlCourse.SelectedValue, StrNull2Zero(txtDayCount.Text))

        '    acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "TimePhase", "เพิ่ม ผลัดฝึก:" & txtCode.Text, "Result:" & item)

        'Else
        '    item = ctlLG.TimePhase_Update(lblID.Text, txtCode.Text, txtName.Text, txtDesc.Text, Boolean2Decimal(chkStatus.Checked), ddlYear.SelectedValue, ddlCourse.SelectedValue, StrNull2Zero(txtDayCount.Text))

        '    acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "TimePhase", "แก้ไข ผลัดฝึก:" & txtCode.Text, "Result:" & item)

        'End If


        LoadTimePhaseToGrid()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)

    End Sub
    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadSubjectToDDL()
        LoadPhaseToCheckList()
        LoadTimePhaseToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        EditData(ddlYear.SelectedValue, ddlCourse.SelectedValue)
    End Sub
End Class

