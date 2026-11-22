
Public Class DocumentMenu
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    'Public dtCert2 As New DataTable
    'Dim ctlStd As New StudentController
    'Dim ctlU As New UserController
    Dim ctlM As New MasterController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            LoadDocumentName()
        End If

    End Sub
    Private Sub LoadDocumentName()
        dt = ctlM.DocumentOfficial_Get()
        datalist_DocumentOfficer.DataSource = dt
        datalist_DocumentOfficer.DataBind()
    End Sub

End Class