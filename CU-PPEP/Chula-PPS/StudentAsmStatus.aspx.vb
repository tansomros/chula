
Public Class StudentAsmStatus
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Public dtAsm As New DataTable
    Dim ctlAss As New AssessmentController
    Dim ctlCs As New CourseController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblResult.Visible = False
            LoadYearToDDL()
            LoadResultProcess()
        End If
    End Sub
    Private Sub LoadYearToDDL()
        dt = ctlCs.Year_GetForReportByStudent(Request.Cookies("ProfileID").Value)
        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "YearName"
                .DataValueField = "CYear"
                .DataBind()
            End With
            ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        End If
        dt = Nothing
    End Sub
    Private Sub LoadResultProcess()
        dtAsm = ctlAss.Assessment_GetStatusByStudent(ddlYear.SelectedValue, Request.Cookies("ProfileID").Value)
        If dtAsm.Rows.Count > 0 Then
            lblResult.Visible = False
        Else
            grdData.Visible = False
            lblResult.Visible = True
        End If
    End Sub
    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadResultProcess()
    End Sub
End Class

