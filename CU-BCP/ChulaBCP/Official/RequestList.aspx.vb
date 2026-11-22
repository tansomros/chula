Public Class RequestList
    Inherits System.Web.UI.Page
    Dim ctlReq As New RequestController
    Dim dt As New DataTable
    Public dtReq As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            LoadTopicToDDL()
        End If
        LoadData()
    End Sub
    Private Sub LoadTopicToDDL()
        dt = ctlReq.RequestTopic_Get
        If dt.Rows.Count > 0 Then
            With ddlTopic
                .Enabled = True
                .DataSource = dt
                .DataTextField = "TopicName"
                .DataValueField = "UID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadData()
        If Request.Cookies("ROLE_STD").Value = True Then
            dtReq = ctlReq.Request_GetByStudent(StrNull2Zero(ddlTopic.SelectedValue), Request.Cookies("PersonID").Value)
        Else
            dtReq = ctlReq.Request_GetByTopic(StrNull2Zero(ddlTopic.SelectedValue))
        End If
    End Sub
    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        LoadData()
    End Sub
End Class