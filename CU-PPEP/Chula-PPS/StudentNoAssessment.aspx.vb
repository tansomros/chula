
Public Class StudentNoAssessment
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlCs As New courseController
    Dim acc As New UserController
    Dim ctlCfg As New SystemConfigController
    Dim ctlReg As New RegisterController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Request.Cookies("ROLE_STD").Value = True Then
            If Not SystemOnlineTime() Then
                Response.Redirect("ResultPage.aspx?t=closed")
            End If
        End If


        If Not IsPostBack Then

            LoadYearToDDL()
            LoadCourseToDDL()

            If Not Request("y") Is Nothing Then
                ddlYear.SelectedValue = Request("y")
                LoadCourseToDDL()
                ddlCourse.SelectedValue = Request("id")
            End If

            grdStudent.PageIndex = 0
            LoadStudentNoAssessmentToGrid(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue))

        End If
    End Sub
    Private Function SystemOnlineTime() As Boolean
        Dim Bdate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_STARTDATE)))
        Dim Edate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_ENDDATE)))
        Dim sToday As Integer = StrNull2Zero(ConvertDate2DBString(ctlCfg.GET_DATE_SERVER))
        Dim bAvailable As Boolean
        If sToday < Bdate Then
            bAvailable = False
        ElseIf sToday > Edate Then
            bAvailable = False
        Else
            bAvailable = True
        End If
        Return bAvailable
    End Function

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
    Private Sub LoadStudentNoAssessmentToGrid(y As Integer, cid As Integer)

        dt = ctlReg.StudentRegister_GetStudentNoAssessment(y, cid, Trim(txtSearchStd.Text))

        If dt.Rows.Count > 0 Then
            lblCount.Text = dt.Rows.Count
            lblNo.Visible = False
            With grdStudent
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        Else
            lblCount.Text = 0
            lblNo.Visible = True
            grdStudent.Visible = False
            grdStudent.DataSource = Nothing
        End If

        dt = Nothing
    End Sub

    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()
        dt = ctlCs.Courses_GetByYear(StrNull2Zero(ddlYear.SelectedValue))
        If dt.Rows.Count > 0 Then

            For i = 0 To dt.Rows.Count - 1
                With ddlCourse
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("NameTH"))
                    .Items(i).Value = dt.Rows(i)("SubjectCode")
                End With
            Next
        End If
        dt = Nothing
    End Sub


    Private Sub grdStudent_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdStudent.PageIndexChanging
        grdStudent.PageIndex = e.NewPageIndex
        LoadStudentNoAssessmentToGrid(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue))
    End Sub

    Private Sub grdStudent_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdStudent.RowCommand
        If TypeOf e.CommandSource Is WebControls.LinkButton Then
            Dim ButtonPressed As WebControls.LinkButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "lnkSelect"
                    Dim str() As String
                    str = Split(ddlCourse.SelectedItem.Text, " : ")
                    If HasSelected(ddlYear.SelectedValue, str(0), e.CommandArgument) Then
                        DisplayMessage(Me.Page, "ท่านได้เลือกแหล่งฝึกนี้ ให้กับวิชาที่ท่านต้องการเรียบร้อยแล้ว")
                    Else
                        SelectLocationToBusket(ddlYear.SelectedValue, Request.Cookies("ProfileID").Value, e.CommandArgument, str(0))
                    End If
            End Select
        End If
    End Sub
    Function HasSelected(year As Integer, subj As String, LID As Integer) As Boolean
        Dim iCount As Integer = 0
        iCount = ctlReg.GetStudentRegister_CountSelected(year, Request.Cookies("ProfileID").Value, subj, LID)

        If iCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SelectLocationToBusket(RegYear, Student_Code, LocationID, SubjectCode)
        Dim iCount As Integer = 0
        iCount = ctlReg.GetStudentRegister_Count(RegYear, Student_Code, SubjectCode)

        If iCount < 2 Then

            ctlReg.StudentRegister_Add(RegYear, Student_Code, iCount + 1, LocationID, SubjectCode, 1, Request.Cookies("Username").Value)

            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "StudentRegister", "นิสิตเลือกแหล่งฝึก LID:" & LocationID & ">>" & Student_Code, "วิชา :" & SubjectCode)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'ผลการตรวจสอบ','บันทึกเรียบร้อยแล้ว');", True)
            LoadStudentNoAssessmentToGrid(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue))

        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านได้เลือกแหล่งฝึกสำหรับวิชานี้ ครบตามจำนวนแล้ว ท่านไม่สามารถเลือกเพิ่มได้อีก');", True)
        End If

    End Sub

    Private Sub grdStudent_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdStudent.RowDataBound

        'If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
        '    Dim str() As String
        '    str = Split(ddlCourse.SelectedItem.Text, " : ")
        '    Dim lnkV As HyperLink = e.Row.Cells(3).FindControl("hlnkView")
        '    lnkV.NavigateUrl = "LocationDetail.aspx?m=2&p=2&y=" & ddlYear.SelectedValue & "&lid=" & grdStudent.DataKeys(e.Row.RowIndex).Value & "&subid=" & str(0)

        'End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub


    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged

        grdStudent.PageIndex = 0
        LoadCourseToDDL()
        LoadStudentNoAssessmentToGrid(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue))
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged

        grdStudent.PageIndex = 0
        LoadStudentNoAssessmentToGrid(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue))
    End Sub

    Protected Sub cmdFindStd_Click(sender As Object, e As EventArgs) Handles cmdFindStd.Click
        grdStudent.PageIndex = 0
        LoadStudentNoAssessmentToGrid(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue))
    End Sub

End Class

