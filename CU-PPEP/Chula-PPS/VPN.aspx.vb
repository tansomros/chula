Public Class VPN
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadVPNAccount()
    End Sub
    Private Sub LoadVPNAccount()
        Dim dt As New DataTable
        Dim ctlP As New PersonController
        Dim ctlV As New VPNController
        Dim LID As Integer = ctlP.Person_GetLocationByUserID(Request.Cookies("UserID").Value)

        dt = ctlV.VPNAccount_GetByLocation(LID)
        If dt.Rows.Count > 0 Then
            lblVPNUser.Text = dt.Rows(0)("Username")
            lblVPNPass.Text = dt.Rows(0)("Password")
        End If
    End Sub
End Class