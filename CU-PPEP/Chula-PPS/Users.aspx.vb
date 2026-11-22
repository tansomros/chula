
Public Class Users
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim objUser As New UserController
    Public dtUser As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            'grdData.PageIndex = 0

            BindUserGroup()
            ddlGroupFind.SelectedIndex = 2
            LoadUserAccountToGrid()
        End If
    End Sub

    Private Sub BindUserGroup()
        With ddlGroupFind
            .Items.Clear()
            .Items.Add("All")
            .Items(0).Value = "0"
            .Items.Add("นิสิต")
            .Items(1).Value = "1"
            .Items.Add("อาจารย์")
            .Items(2).Value = "2"
            .Items.Add("แหล่งฝึก")
            .Items(3).Value = "3"
            .Items.Add("Administrator")
            .Items(4).Value = "4"
            If Request.Cookies("ROLE_SPA").Value = True Then
                .Items.Add("Super Admin")
                .Items(5).Value = "9"
            End If
        End With
    End Sub

    Private Sub LoadUserAccountToGrid()
        dtUser = objUser.User_GetBySearch(ddlGroupFind.SelectedValue, txtSearch.Text)
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadUserAccountToGrid()
    End Sub

    Protected Sub ddlGroupFind_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGroupFind.SelectedIndexChanged
        LoadUserAccountToGrid()
    End Sub

End Class

