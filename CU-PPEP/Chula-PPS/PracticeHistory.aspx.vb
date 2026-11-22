Public Class PracticeHistory
    Inherits System.Web.UI.Page
    Public dtHis As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadPraticeHistory()
    End Sub

    Private Sub LoadPraticeHistory()
        Dim ctlA As New AssessmentController
        If Request.Cookies("ROLE_STD").Value = True Then
            dtHis = ctlA.StudentAssessment_GetByStudent(Request.Cookies("Username").Value)
        Else
            If Not Request("stdcode") Is Nothing Then
                dtHis = ctlA.StudentAssessment_GetByStudent(Request("stdcode"))
            End If
        End If
        lblCount.Text = dtHis.Rows.Count
        If dtHis.Rows.Count > 0 Then
            lblNot.Visible = False
        Else
            lblNot.Visible = True
        End If
    End Sub
End Class