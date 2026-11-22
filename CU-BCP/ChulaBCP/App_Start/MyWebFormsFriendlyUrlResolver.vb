Public Class MyWebFormsFriendlyUrlResolver
    Inherits Microsoft.AspNet.FriendlyUrls.Resolvers.WebFormsFriendlyUrlResolver

    Protected Overrides Function TrySetMobileMasterPage(ByVal httpContext As HttpContextBase, ByVal page As Page, ByVal mobileSuffix As String) As Boolean
        If mobileSuffix = "Mobile" Then
            Return False
        Else
            Return MyBase.TrySetMobileMasterPage(httpContext, page, mobileSuffix)
        End If
    End Function
End Class

