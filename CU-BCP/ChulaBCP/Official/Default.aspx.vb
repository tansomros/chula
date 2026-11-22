Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Home
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlUser As New UserController
    Dim ctlC As New SystemConfigController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("UserLogin")) Then
            Response.Redirect("Login.aspx")
        End If
        If Session("userid") Is Nothing Then
            Response.Redirect("Login.aspx")
        End If

        If Not IsPostBack Then
            If Request.Cookies("ROLE_ADM").Value = True Then
                pnUserOnline.Visible = True
                LoadUserOnline()
            Else
                pnUserOnline.Visible=False
            End If

            LoadControlPanel()
        End If

    End Sub

    Private Sub LoadControlPanel()
        dt = ctlC.ControlPanel_Get(Request.Cookies("RoleID").Value)
        If dt.Rows.Count > 0 Then
            grdControlPanel.DataSource = dt
            grdControlPanel.DataBind()
        End If
    End Sub

    Private Sub LoadUserOnline()
        dt = ctlUser.Users_Online
        lblOnline.Text = ""
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1

                Select Case i Mod 4
                    Case 0
                        lblOnline.Text &= " <span class='label label-success'>" & dt.Rows(i)("Name") & "</span>"
                    Case 1
                        lblOnline.Text &= " <span class='label label-info'>" & dt.Rows(i)("Name") & "</span>"
                    Case 2
                        lblOnline.Text &= " <span class='label label-danger'>" & dt.Rows(i)("Name") & "</span>"
                    Case 3
                        lblOnline.Text &= " <span class='label label-warning'>" & dt.Rows(i)("Name") & "</span>"
                    Case Else
                        lblOnline.Text &= " <span class='label label-success'>" & dt.Rows(i)("Name") & "</span>"
                End Select

            Next

        Else
            lblOnline.Text = "ไม่มี User Online ในขณะนี้"
        End If

    End Sub

End Class