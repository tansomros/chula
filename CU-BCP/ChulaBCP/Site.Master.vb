Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        '    If Request.Cookies.Count > 0 Then
        '        Dim dt As New DataTable
        '        Dim ctlU As New UserController
        '        lblUsername.Text = ctlU.User_GetName(Request.Cookies("UserLoginID").Value)
        '    End If
        '    dtfInfo = Nothing

        'End If
    End Sub
End Class