
Public Class Certificate
    Inherits System.Web.UI.Page

    Public dtCert As New DataTable
    Dim ctlStd As New StudentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            LoadCertificate()
        End If

    End Sub
    Private Sub LoadCertificate()
        dtCert = ctlStd.Certificate_Get
    End Sub
End Class