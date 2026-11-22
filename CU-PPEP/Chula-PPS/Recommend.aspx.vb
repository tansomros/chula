Public Class Recommend
    Inherits System.Web.UI.Page
    Public dtRecLoc As New DataTable
    Dim ctlR As New RecommendController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            If Request.Cookies("ROLE_STD").Value = True Then
                ddlType.SelectedIndex = 1
                cmdAdd.Visible = True
                pnStatus.Visible = False
                pnType.Visible = True
            Else
                cmdAdd.Visible = False
                pnStatus.Visible = True
                pnType.Visible = False
            End If
            LoadSubject()
            LoadLocationToDDL()
            LoadRecommendToGrid()
        End If
    End Sub
    Private Sub LoadLocationToDDL()
        ddlLocation.Items.Clear()
        Dim ctlL As New LocationController
        dt = ctlL.Location_GetFromRecommend("Location")
        If dt.Rows.Count > 0 Then
            With ddlLocation
                .DataSource = dt
                .DataTextField = "LocationName"
                .DataValueField = "LocationID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
    End Sub

    Private Sub LoadSubject()
        Dim ctlSj As New SubjectController
        dt = ctlSj.Subject_GetFromRecommend()
        If dt.Rows.Count > 0 Then
            ddlSubject.DataSource = dt
            ddlSubject.DataTextField = "SubjectName"
            ddlSubject.DataValueField = "SubjectID"
            ddlSubject.DataBind()
            ddlSubject.SelectedIndex = 0
        End If
        dt = Nothing
    End Sub
    Private Sub LoadRecommendToGrid()
        Dim status, type, stdcode As String
        If Request.Cookies("ROLE_STD").Value = True Then
            status = "A"
            type = ddlType.SelectedValue
            stdcode = Request.Cookies("Username").Value
        Else
            status = ddlStatus.SelectedValue
            type = "0"
            stdcode = ""
        End If
        dtRecLoc = ctlR.LocationRecommend_GetSearch(type, status, stdcode, ddlSubject.SelectedValue, ddlLocation.SelectedValue, txtSearch.Text)
        lblCount.Text = dtRecLoc.Rows.Count
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Response.Redirect("RecommendModify.aspx?ActionType=rcm&ItemType=rcm1&new=y")
    End Sub

    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        LoadRecommendToGrid()
    End Sub

    Private Sub ddlSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject.SelectedIndexChanged
        LoadRecommendToGrid()
    End Sub

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        LoadRecommendToGrid()
    End Sub

    Private Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        LoadRecommendToGrid()
    End Sub

    Private Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        LoadRecommendToGrid()
    End Sub
End Class

