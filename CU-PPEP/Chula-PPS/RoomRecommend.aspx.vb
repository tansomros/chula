Public Class RoomRecommend
    Inherits System.Web.UI.Page
    Public dtRecRoom As New DataTable
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
            LoadLocationToDDL()
            LoadRecommendToGrid()
        End If
    End Sub
    Private Sub LoadLocationToDDL()
        ddlLocation.Items.Clear()
        Dim ctlL As New LocationController
        dt = ctlL.Location_GetFromRecommend("room")
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
    Private Sub LoadRecommendToGrid()
        'If Request.Cookies("ROLE_STD").Value = True Then
        '    cmdAdd.Visible = True
        '    dtRecRoom = ctlR.RoomRecommend_GetByStudent(Request.Cookies("Username").Value, Trim(txtSearch.Text))
        'Else
        '    cmdAdd.Visible = False
        '    dtRecRoom = ctlR.RoomRecommend_GetSearch(Trim(txtSearch.Text))
        'End If

        'If dtRecRoom.Rows.Count > 0 Then
        '    lblCount.Text = dtRecRoom.Rows.Count
        '    lblNo.Visible = False

        'Else
        '    lblCount.Text = 0
        '    lblNo.Visible = True

        'End If


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
        dtRecRoom = ctlR.RoomRecommend_GetSearch(type, status, stdcode, ddlLocation.SelectedValue, txtSearch.Text)
        lblCount.Text = dtRecRoom.Rows.Count


    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        LoadRecommendToGrid()
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Response.Redirect("RoomRecommendModify.aspx?new=y")
    End Sub

    Private Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        LoadRecommendToGrid()
    End Sub

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        LoadRecommendToGrid()
    End Sub

    Private Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        LoadRecommendToGrid()
    End Sub
End Class

