
Public Class AssignManual
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlCs As New courseController
    Dim acc As New UserController

    Dim strSubj() As String
    Dim gSubjCode As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then

            LoadYearToDDL()
            LoadCourseToDDL()

            ddlCourse.SelectedIndex = 0
            strSubj = Split(ddlCourse.SelectedItem.Text, " : ")
            gSubjCode = strSubj(0)

            grdStudent.PageIndex = 0
            grdData.PageIndex = 0
            LoadStudentNoAssessmentToGrid()
            LoadLocationNotFullToGrid()

        End If
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

    Private Sub LoadStudentNoAssessmentToGrid()
        Dim ctlReg As New RegisterController

        dt = ctlReg.StudentRegister_GetStudentNoAssessment(ddlYear.SelectedValue, gSubjCode, Trim(txtSearchStd.Text))

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

    Private Sub LoadLocationNotFullToGrid()
        Dim ctlREQ As New REQcontroller
        Dim M, F, N, iBalanceM, iBalanceF, iBalanceN As Integer

        dt = ctlREQ.REQ_GetLocationNotFull(StrNull2Zero(ddlYear.SelectedValue), ddlCourse.SelectedValue, Trim(txtSearch.Text))
        If dt.Rows.Count > 0 Then

            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()

                For i = 0 To .Rows.Count - 1

                    iBalanceM = ctlREQ.REQACC_GetBalanceCount(ddlYear.SelectedValue, ddlCourse.SelectedValue, StrNull2Zero(.Rows(i).Cells(10).Text), StrNull2Zero(.DataKeys(i).Value), "M")

                    iBalanceF = ctlREQ.REQACC_GetBalanceCount(ddlYear.SelectedValue, ddlCourse.SelectedValue, StrNull2Zero(.Rows(i).Cells(10).Text), StrNull2Zero(.DataKeys(i).Value), "F")

                    .Rows(i).Cells(8).Text = "ชาย=" & iBalanceM & " หญิง=" & iBalanceF

                    M = StrNull2Zero(.Rows(i).Cells(5).Text)
                    F = StrNull2Zero(.Rows(i).Cells(6).Text)
                    N = StrNull2Zero(.Rows(i).Cells(7).Text)
                    If M <> 0 Then
                        iBalanceM = M - iBalanceM
                        .Rows(i).Cells(9).Text = " ชาย=" & iBalanceM
                    End If

                    If F <> 0 Then
                        iBalanceF = F - iBalanceF
                        .Rows(i).Cells(9).Text &= " หญิง=" & iBalanceF
                    End If

                    If N <> 0 Then
                        iBalanceN = N - (iBalanceM + iBalanceF)
                        .Rows(i).Cells(9).Text &= " ไม่ระบุ=" & iBalanceN
                    End If

                Next

            End With
            cmdSave.Visible = True
            cmdClear.Visible = True
        Else
            grdData.Visible = False
            cmdSave.Visible = False
            cmdClear.Visible = False
        End If
        dt = Nothing
    End Sub
    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()
        dt = ctlCs.Courses_GetByYear(ddlYear.SelectedValue)
        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With ddlCourse
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("NameTH"))
                    .Items(i).Value = dt.Rows(i)("CourseID")
                End With
            Next
            ddlCourse.SelectedIndex = 0
        End If

    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadLocationNotFullToGrid()
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand

    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub
    Private Sub AddStudentToAssessment()

        Dim item, dIndex, RYear, LocatID, TimePhaseID As Integer
        Dim sStdCode, strA As String
        Dim bDup As Boolean = False
        Dim ctlAss As New AssessmentController
        dIndex = HiddenField1.Value

        RYear = StrNull2Zero(ddlYear.SelectedValue)

        strSubj = Split(ddlCourse.SelectedItem.Text, " : ")
        gSubjCode = strSubj(0)

        'sSubjCode = ddlCourse.SelectedValue
        LocatID = DBNull2Zero(grdData.DataKeys(dIndex).Value)
        TimePhaseID = StrNull2Zero(grdData.Rows(dIndex).Cells(10).Text)
        For i = 0 To grdStudent.Rows.Count - 1
            With grdStudent
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkStd")
                If chkS.Checked Then

                    sStdCode = .Rows(i).Cells(1).Text
                    If ctlAss.Assessment_GetCheckDup(RYear, TimePhaseID, sStdCode) <= 0 Then

                        item = ctlAss.Assessment_Add(RYear, gSubjCode, sStdCode, LocatID, TimePhaseID, ddlCourse.SelectedValue, Request.Cookies("Username").Value)

                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Assessment", "คัดเลือกนิสิตด้วยวิธีกำหนดเอง:" & gSubjCode & ">>" & grdData.DataKeys(dIndex).Value, "")

                    Else
                        bDup = True
                    End If



                End If

            End With
        Next
        LoadStudentNoAssessmentToGrid()
        LoadLocationNotFullToGrid()
        If bDup = True Then
            strA = "และ มีนิสิตบางคนไม่สามารถกำหนดให้ได้ เนื่องจากผลัดฝึกซ้ำ"
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย" & strA & "');", True)
    End Sub

    Private Sub grdStudent_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdStudent.PageIndexChanging
        grdStudent.PageIndex = e.NewPageIndex
        LoadStudentNoAssessmentToGrid()
    End Sub
    Private Sub grdStudent_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdStudent.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        AddStudentToAssessment()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        For i = 0 To grdData.Rows.Count - 1
            With grdData
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("optSelect")
                chkS.Checked = False
            End With
        Next
        For i = 0 To grdStudent.Rows.Count - 1
            With grdStudent
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkStd")
                chkS.Checked = False
            End With
        Next
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged

        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadCourseToDDL()

        strSubj = Split(ddlCourse.SelectedItem.Text, " : ")
        gSubjCode = strSubj(0)

        grdStudent.PageIndex = 0
        grdData.PageIndex = 0

        LoadStudentNoAssessmentToGrid()
        LoadLocationNotFullToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged

        strSubj = Split(ddlCourse.SelectedItem.Text, " : ")
        gSubjCode = strSubj(0)


        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadStudentNoAssessmentToGrid()
        LoadLocationNotFullToGrid()



    End Sub

    Protected Sub cmdFindStd_Click(sender As Object, e As EventArgs) Handles cmdFindStd.Click
        grdStudent.PageIndex = 0
        LoadStudentNoAssessmentToGrid()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadLocationNotFullToGrid()
    End Sub

End Class

