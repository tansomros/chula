Public Class DataConfig
    Inherits System.Web.UI.Page
    Public dtConfig As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Request.Cookies("Username").Value Is Nothing Then
        '    Response.Redirect("Default.aspx?logout=YES")
        'End If

        Dim ctlCfg As New SystemConfigController
        dtConfig = ctlCfg.SystemConfig_Get
    End Sub
End Class