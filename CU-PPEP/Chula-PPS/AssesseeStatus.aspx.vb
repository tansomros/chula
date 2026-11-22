Public Class AssesseeStatus
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim ctlCs As New Coursecontroller
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblYear.Text = Request.Cookies("ASSMYEAR").Value
            grdData.PageIndex = 0

            LoadCourseToDDL()
            LoadLocationToDDL()
            LoadAssesseeListToGrid()
        End If

    End Sub

    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()

        dt = ctlCs.Courses_GetFromAssessment(StrNull2Zero(lblYear.Text))

        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            With ddlCourse
                .Items.Add("")
                .Items(0).Value = 0

                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("SubjectName"))
                    .Items(i + 1).Value = dt.Rows(i)("CourseID")
                Next
            End With
        End If

    End Sub

    Private Sub LoadLocationToDDL()
        ddlLocation.Items.Clear()
        Dim ctlL As New LocationController

        dt = ctlL.LocationAssessment_GetByCourse(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlCourse.SelectedValue))

        If dt.Rows.Count > 0 Then
            ddlLocation.Items.Clear()
            With ddlLocation
                .Items.Add("")
                .Items(0).Value = 0

                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & dt.Rows(i)("LocationName"))
                    .Items(i + 1).Value = dt.Rows(i)("LocationID")
                Next
            End With
        End If

    End Sub

    Private Sub LoadAssesseeListToGrid()
        dt = ctlLG.Assessee_GetBySearch(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlCourse.SelectedValue), StrNull2Zero(ddlLocation.SelectedValue), txtSearch.Text, ddlStatus.SelectedValue)
        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With
        dt = Nothing
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadAssesseeListToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        grdData.PageIndex = 0
        LoadLocationToDDL()
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        grdData.PageIndex = 0
        LoadAssesseeListToGrid()
    End Sub

    Protected Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadAssesseeListToGrid()
    End Sub

End Class

