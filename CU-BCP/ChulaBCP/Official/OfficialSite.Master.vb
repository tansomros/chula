Imports System.IO
Public Class OfficialSite
    Inherits System.Web.UI.MasterPage
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' ถ้า cookie ยังไม่หมดอายุ
            If IsNothing(Request.Cookies("UserLogin").Value) Then
                Response.Redirect("Login.aspx")
            End If
        Catch ex As Exception
            ' ถ้า cookie หมดอายุแล้ว
            Response.Redirect("Login.aspx")
        End Try

        If Not IsPostBack Then
            LoadUserDetail()
        End If
    End Sub

    Private Sub LoadUserDetail()
        Dim ctlU As New UserController
        dt = ctlU.User_GetByUserID(Request.Cookies("UserLoginID").Value)
        If dt.Rows.Count > 0 Then
            lblUserName1.Text = String.Concat(dt.Rows(0)("Name")) '& " " & String.Concat(dt.Rows(0)("LastName"))
            lblUsername2.Text = String.Concat(dt.Rows(0)("Name")) '& " " & String.Concat(dt.Rows(0)("LastName"))
            If DBNull2Str(dt.Rows(0).Item("ImagePath")) <> "" Then
                Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & UserPic & "/" & dt.Rows(0).Item("ImagePath")))

                If objfile.Exists Then
                    imgUser1.ImageUrl = "~/" & UserPic & "/" & dt.Rows(0).Item("ImagePath")
                    imgUser2.ImageUrl = "~/" & UserPic & "/" & dt.Rows(0).Item("ImagePath")
                Else
                    imgUser1.ImageUrl = "~/" & UserPic & "user_blank.jpg"
                    imgUser2.ImageUrl = "~/" & UserPic & "user_blank.jpg"
                End If
            Else
                imgUser1.ImageUrl = "~/" & UserPic & "user_blank.jpg"
                imgUser2.ImageUrl = "~/" & UserPic & "user_blank.jpg"
            End If

        End If
        dt = Nothing
    End Sub
End Class