
Public Class RegisterMenu
    Inherits System.Web.UI.Page
    Public dtReg As New DataTable
    Dim ctlReg As New RegisterController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            dtReg = ctlReg.RegisterTopic_Get()
        End If
    End Sub
End Class