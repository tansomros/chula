Public Class BasePage
    Inherits System.Web.UI.Page

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        Dim userCookie As HttpCookie = Request.Cookies("Username")
        If userCookie Is Nothing OrElse String.IsNullOrEmpty(userCookie.Value) Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
    End Sub
End Class
