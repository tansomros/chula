
Public Class PreceptorList
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlP As New PersonController
    Dim ctlL As New LocationController
    Public dtPreceptor As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            BindLocationFilter()
            LoadPreceptorList()
        End If
    End Sub

    Private Sub BindLocationFilter()
        dt = ctlL.Location_Get
        With ddlLocationFind
            .Items.Clear()
            .DataSource = dt
            .DataTextField = "LocationName"
            .DataValueField = "LocationID"
            .DataBind()
            .Items.Insert(0, New ListItem("-- ทั้งหมด --", "0"))
            .SelectedIndex = 0
        End With
        dt = Nothing
    End Sub

    Private Sub LoadPreceptorList()
        dtPreceptor = ctlP.Preceptor_GetBySearch(StrNull2Zero(ddlLocationFind.SelectedValue), Trim(txtSearch.Text))
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadPreceptorList()
    End Sub

    Protected Sub ddlLocationFind_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocationFind.SelectedIndexChanged
        txtSearch.Text = ""
        LoadPreceptorList()
    End Sub

End Class
