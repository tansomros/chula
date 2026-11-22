Public Class User
    Inherits System.Web.UI.Page
    Dim ctlU As New UserController
    Public dtUser As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        dtUser = ctlU.User_Get
    End Sub
End Class