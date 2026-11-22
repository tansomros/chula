Public Class _Default
    Inherits Page

    Dim ctlG As New SlideController
    Dim ctlN As New NewsController

    Public dtS As New DataTable
    Public dtNew As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        LoadNews()
        If Not IsPostBack Then
            LoadSlide()
        End If
    End Sub
    Private Sub LoadSlide()
        dtS = ctlG.SlideShow_GetFirstPage()
    End Sub
    Private Sub LoadNews()
        dtNew = ctlN.News_GetFirstPage()
        'dlNews.DataSource = dtNew
        'dlNews.DataBind()
    End Sub
End Class