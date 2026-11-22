Public Class AssesseeListAdmin
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


            LoadCourseToDDL()
            LoadLocationToDDL()
            'LoadAssesseeListToGrid()
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

        dt = ctlLG.StudentAssessment_GetSearch4Admin(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlCourse.SelectedValue), StrNull2Zero(ddlLocation.SelectedValue), txtSearch.Text)

        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With
        dt = Nothing
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    Session("AssessorUID") = grdData.DataKeys(e.CommandArgument).Value
                    Session("AssessorRoleID") = grdData.Rows(e.CommandArgument).Cells(9).Text

                    Response.Redirect("AssessmentAdmin.aspx?p=a&std=" & grdData.Rows(e.CommandArgument).Cells(2).Text & "&sj=" & RTrim(grdData.Rows(e.CommandArgument).Cells(4).Text) & "&guid=" & grdData.Rows(e.CommandArgument).Cells(10).Text)
            End Select
        End If
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadAssesseeListToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        LoadLocationToDDL()
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        LoadAssesseeListToGrid()
    End Sub
End Class

