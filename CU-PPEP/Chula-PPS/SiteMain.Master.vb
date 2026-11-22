Imports System.IO
Public Class SiteMain
    Inherits System.Web.UI.MasterPage
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then


            Dim ctlStd As New StudentController
            Dim ctlPsn As New PersonController

            Dim picPaths As String


            If Session("UserProfileID") = 1 Then
                dt = ctlStd.GetStudent_ByID(Session("ProfileID"))
                picPaths = stdPic
            Else
                dt = ctlPsn.GetPerson_ByID(Session("ProfileID"))
                picPaths = personPic
            End If

            If dt.Rows.Count > 0 Then
                If DBNull2Str(dt.Rows(0).Item("PicturePath")) <> "" Then

                    Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & picPaths & "/" & dt.Rows(0).Item("PicturePath")))

                    If objfile.Exists Then
                        imgUser.ImageUrl = "~/" & picPaths & "/" & dt.Rows(0).Item("PicturePath")
                    Else
                        imgUser.ImageUrl = "~/" & picPaths & "/nopic" & dt.Rows(0).Item("Gender") & ".jpg"

                    End If

                End If
                Me.lblUserName.Text = String.Concat(dt.Rows(0).Item("FirstName")) & " " & dt.Rows(0).Item("LastName") & "(" & dt.Rows(0).Item("NickName") & ")"

            End If

            dt = Nothing
        End If
    End Sub

End Class