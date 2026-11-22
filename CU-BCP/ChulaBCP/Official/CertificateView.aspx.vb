
Public Class CertificateView
    Inherits System.Web.UI.Page

    Public dtCert2 As New DataTable
    Dim ctlStd As New StudentController
    Dim ctlU As New UserController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            LoadCertificate()
        End If

    End Sub
    Private Sub LoadCertificate()

        If Request.Cookies("ROLE_ADM").Value = True Then
            dtCert2 = ctlStd.Certificate_GetByStudent(Request("std"))
        Else
            dtCert2 = ctlStd.Certificate_GetByStudent(Request.Cookies("PersonID").Value)
        End If

    End Sub
End Class