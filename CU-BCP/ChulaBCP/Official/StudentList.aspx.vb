
Public Class StudentList
    Inherits System.Web.UI.Page

    Public dtStd As New DataTable
    Dim ctlStd As New StudentController
    Dim ctlU As New UserController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            LoadStudent()
        End If

    End Sub
    Private Sub LoadStudent()
        dtStd = ctlStd.Student_GetAll
    End Sub
End Class