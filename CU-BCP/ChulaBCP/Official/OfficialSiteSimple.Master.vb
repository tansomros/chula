Imports System.IO
Public Class OfficialSiteSimple
    Inherits System.Web.UI.MasterPage
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If

        If Not IsPostBack Then
            LoadUserDetail()
        End If

    End Sub

    Private Sub LoadUserDetail()
        Dim ctlU As New UserController
        dt = ctlU.User_GetByID(Session("userid"))
        If dt.Rows.Count > 0 Then
            lblMemberName.Text = String.Concat(dt.Rows(0)("Name"))

            If DBNull2Str(dt.Rows(0).Item("ImagePath")) <> "" Then
                Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & PersonPic & "/" & dt.Rows(0).Item("ImagePath")))

                If objfile.Exists Then
                    imgUser.ImageUrl = "~/" & PersonPic & "/" & dt.Rows(0).Item("ImagePath")
                Else
                    imgUser.ImageUrl = "~/" & PersonPic & "/nopic.jpg"
                End If

            End If

        End If
        dt = Nothing
    End Sub
End Class