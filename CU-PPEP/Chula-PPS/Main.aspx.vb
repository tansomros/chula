Public Class Main
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        nbMain.AllowExpanding = True
        nbMain.AllowSelectItem = True
        nbMain.AutoCollapse = True
        nbMain.EnableHotTrack = True
        nbMain.EnableAnimation = True
    End Sub


End Class