Imports Newtonsoft.Json
Public Class LocationMaps
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlL As New LocationController
    Public Shared json As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt = ctlL.Location_GetMaps
        json = JsonConvert.SerializeObject(dt, Formatting.Indented)
    End Sub

End Class