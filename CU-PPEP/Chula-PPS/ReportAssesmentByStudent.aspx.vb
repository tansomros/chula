
Public Class ReportAssesmentByStudent
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlAss As New AssessmentController
    Dim ctlCs As New Coursecontroller
    Dim acc As New UserController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblResult.Visible = False
            LoadYearToDDL()
            LoadCourseToDDL()
        End If
    End Sub
    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()
        dt = ctlCs.Courses_GetByYear(ddlYear.SelectedValue)
        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            ddlCourse.Items.Add("---ทั้งหมด---")
            ddlCourse.Items(0).Value = 0

            For i = 1 To dt.Rows.Count
                With ddlCourse
                    .Items.Add("" & dt.Rows(i - 1)("SubjectCode") & " : " & dt.Rows(i - 1)("NameTH"))
                    .Items(i).Value = dt.Rows(i - 1)("SubjectCode")
                End With
            Next

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
    Protected Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        grdStudent.PageIndex = 0
        LoadAssessmentToGrid()
    End Sub
    Private Sub LoadAssessmentToGrid()

        dt = ctlAss.Assessment_GetByStudentSearch(ddlYear.SelectedValue, ddlCourse.SelectedValue, Trim(txtStudent.Text))
        If dt.Rows.Count > 0 Then
            With grdStudent
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
            lblResult.Visible = False
        Else
            grdStudent.Visible = False
            lblResult.Visible = True
        End If
        dt = Nothing
    End Sub
    Private Sub grdStudent_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdStudent.PageIndexChanging
        grdStudent.PageIndex = e.NewPageIndex
        LoadAssessmentToGrid()
    End Sub

    Private Sub grdStudent_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdStudent.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    Response.Redirect("AssessmentModify.aspx?ActionType=asg&ItemType=find&id=" & e.CommandArgument)
                Case "imgDel"
                    If ctlAss.Assessment_DeleteByID(e.CommandArgument) Then

                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "Assessments", "ลบ ผลการคัดเลือก :" & ddlCourse.SelectedValue, "")

                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        grdStudent.PageIndex = 0
                        LoadAssessmentToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If

            End Select


        End If
    End Sub

    Private Sub grdStudent_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdStudent.RowDataBound
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

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadCourseToDDL()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        grdStudent.PageIndex = 0
        LoadAssessmentToGrid()
    End Sub
End Class

