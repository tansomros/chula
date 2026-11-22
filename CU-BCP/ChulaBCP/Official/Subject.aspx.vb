Imports System.Data
Public Class Subject
    Inherits System.Web.UI.Page
    Dim ctlS As New SubjectController
    Public dtSj As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            LoadSubject()
        End If
    End Sub
    Private Sub LoadSubject()
        dtSj = ctlS.Subject_Get
    End Sub

End Class