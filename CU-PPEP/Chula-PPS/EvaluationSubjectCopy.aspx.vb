Public Class EvaluationSubjectCopy
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlCs As New AssessmentController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblNotic.Visible = False
            txtDestinationYear.Text = Request.Cookies("EDUYEAR").Value
            LoadYearToDDL()
        End If
    End Sub

    Private Sub LoadYearToDDL()

        dt = ctlCs.AssessmentSubject_GetYear

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "EduYear"
                .DataValueField = "EduYear"
                .DataBind()

                .SelectedIndex = 0
            End With
        Else
            ddlYear.DataSource = dt
        End If

        dt = Nothing
    End Sub
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        ctlCs.AssessmentSubject_Copy(ddlYear.SelectedValue, txtDestinationYear.Text, Request.Cookies("UserID").Value)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ผูกแบบประเมินสำหรับปี " & txtDestinationYear.Text & "เรียบร้อย');", True)
    End Sub

End Class

