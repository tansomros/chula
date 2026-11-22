Imports System.IO
Imports Newtonsoft.Json

Public Class OfficialSite2
    Inherits MasterPage

    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If IsNothing(Request.Cookies("ChulaBCP")) Then
            Response.Redirect("Default.aspx")
        End If
        LoadUserDetail()
        If Not IsPostBack Then

            'Dim ctlPsn As New PersonController

            'Dim picPaths As String

            'dt = ctlPsn.Person_GetByPersonID(StrNull2Zero(Request.Cookies("ChulaBCP")("LoginPersonUID")))
            'picPaths = PersonPic


            'If dt.Rows.Count > 0 Then
            '    If DBNull2Str(dt.Rows(0).Item("PicturePath")) <> "" Then

            '        Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & picPaths & "/" & String.Concat(dt.Rows(0).Item("PicturePath"))))

            '        If objfile.Exists Then
            '            imgUser1.ImageUrl = "~/" & picPaths & "/" & String.Concat(dt.Rows(0).Item("PicturePath"))
            '            'imgUser2.ImageUrl = "~/" & picPaths & "/" &  String.Concat(dt.Rows(0).Item("PicturePath"))
            '            imgUser3.ImageUrl = "~/" & picPaths & "/" & String.Concat(dt.Rows(0).Item("PicturePath"))
            '            imgUser4.ImageUrl = "~/" & picPaths & "/" & String.Concat(dt.Rows(0).Item("PicturePath"))

            '        Else
            '            imgUser1.ImageUrl = "~/" & picPaths & "/user_blank.jpg"
            '            'imgUser2.ImageUrl = "~/" & picPaths & "/user_blank.jpg"
            '            imgUser3.ImageUrl = "~/" & picPaths & "/user_blank.jpg"
            '            imgUser4.ImageUrl = "~/" & picPaths & "/user_blank.jpg"
            '        End If

            '    End If
            '    lblUserName.Text = String.Concat(dt.Rows(0).Item("PersonName")) & " (" & String.Concat(dt.Rows(0).Item("Code")) & ")"
            '    'lblUserName1.Text = String.Concat(dt.Rows(0).Item("PersonName")) & " (" & dt.Rows(0).Item("Code") & ")"
            '    lblUserName2.Text = String.Concat(dt.Rows(0).Item("PersonName")) & " (" & String.Concat(dt.Rows(0).Item("Code")) & ")"
            'End If


            'If Not IsNothing(Request.Cookies("ChulaBCP")) Then
            '    LoadCalendarData()
            'End If


            'dt = Nothing

        End If

    End Sub
    Private Sub LoadUserDetail()
        Dim ctlU As New UserController
        dt = ctlU.User_GetByUserID(Request.Cookies("ChulaBCP")("userid"))
        If dt.Rows.Count > 0 Then
            lblUserName.Text = String.Concat(dt.Rows(0)("Name"))
            lblUsername2.Text = String.Concat(dt.Rows(0)("Name"))
            If DBNull2Str(dt.Rows(0).Item("ImagePath")) <> "" Then
                Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & PersonPic & "/" & dt.Rows(0).Item("ImagePath")))

                If objfile.Exists Then
                    imgUser1.ImageUrl = "~/" & PersonPic & "/" & dt.Rows(0).Item("ImagePath")
                    imgUser2.ImageUrl = "~/" & PersonPic & "/" & dt.Rows(0).Item("ImagePath")

                Else
                    imgUser1.ImageUrl = "~/" & PersonPic & "/user_blank.jpg"
                    imgUser2.ImageUrl = "~/" & PersonPic & "/user_blank.jpg"
                End If

            End If

        End If
        dt = Nothing
    End Sub

    'Dim ctlE As New TaskController
    Dim dtCalendar As New DataTable
    Public Shared json As String
    'Private Sub LoadCalendarData()
    '    If Request.Cookies("ChulaBCP")("ROLE_CAD") = True Or Request.Cookies("ChulaBCP")("ROLE_ADM") = True Or Request.Cookies("ChulaBCP")("ROLE_SPA") = True Then
    '        dtCalendar = ctlE.Event_GetByAdmin(StrNull2Zero(Request.Cookies("ChulaBCP")("LoginCompanyUID")), StrNull2Zero(Request.Cookies("ChulaBCP")("LoginPersonUID")))
    '    Else
    '        dtCalendar = ctlE.Event_GetByUser(StrNull2Zero(Request.Cookies("ChulaBCP")("LoginCompanyUID")), StrNull2Zero(Request.Cookies("ChulaBCP")("LoginPersonUID")))
    '    End If
    '    json = JsonConvert.SerializeObject(dtCalendar, Formatting.Indented)
    'End Sub


End Class