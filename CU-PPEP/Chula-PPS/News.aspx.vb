Public Class News
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlNews As New NewsController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadNews(Request("id"))
        End If
    End Sub
    Private Sub LoadNews(id As Integer)
        Dim dtN As New DataTable

        dtN = ctlNews.GetNews_ByID(id)
        If dtN.Rows.Count > 0 Then
            With dtN.Rows(0)
                lblTitle.Text = .Item("Title")
                lblContent.Text = .Item("ContentNews")
                lblDate.Text = .Item("NewsDate")
            End With

        End If
        dtN = Nothing
    End Sub
End Class
