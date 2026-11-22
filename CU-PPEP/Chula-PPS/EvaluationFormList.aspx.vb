Public Class EvaluationFormList
    Inherits System.Web.UI.Page

    Dim ctlA As New AssessmentController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            LoadEvaluationGroup()
            LoadEvaluationTopicToGrid()
        End If

    End Sub
    Private Sub LoadEvaluationGroup()
        dt = ctlA.EvaluationGroup_GetActive
        With ddlGroup
            .DataSource = dt
            .DataTextField = "Name"
            .DataValueField = "UID"
            .DataBind()
            .SelectedIndex = 0
        End With
        dt = Nothing
    End Sub
    Private Sub LoadEvaluationTopicToGrid()
        dt = ctlA.EvaluationTopic_GetByGroup(ddlGroup.SelectedValue)
        With grdEvaluation
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With
        dt = Nothing
        lblAlert.Visible = False
    End Sub
    Private Sub grdEvaluation_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdEvaluation.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub ddlGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGroup.SelectedIndexChanged
        LoadEvaluationTopicToGrid()
    End Sub
End Class

