Imports System.IO
Imports Newtonsoft.Json
Public Class Site
    Inherits System.Web.UI.MasterPage

    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Request.Cookies("RoleID") = isShopAccess Then
        '    hlnkUserName.NavigateUrl = "LocationsEdit.aspx?id=" & Request.Cookies("LocationID").Value
        'Else
        '    hlnkUserName.NavigateUrl = "#"
        'End If

        'Dim userCookie As HttpCookie = Request.Cookies("Username")

        'If userCookie Is Nothing OrElse String.IsNullOrEmpty(userCookie.Value) Then
        '    Response.Redirect("Default.aspx?logout=YES")
        'End If


        'If Request.Cookies("Username").Value Is Nothing Then
        '    Response.Redirect("Default.aspx?logout=YES")
        'End If

        If Not IsPostBack Then

            If Request.Cookies("LANG").Value.ToLower() = "en" Then
                lblProgramName.Text = "Pharmacy Professional Experience Practice, "
                lblFacultyName.Text = "Faculty of Pharmacy, Chulalongkorn University"
                lblFooterName.Text = "Office of Professional Experience Program, Faculty of Pharmacy, Chulalongkorn University"
                lblAddress.Text = "254 Phaya Thai Road, Pathumwan District, Bangkok 10330 Tel. 02-218-8450-1 Fax. 02-218-8450"
            Else
                lblProgramName.Text = "ระบบฐานข้อมูลการฝึกปฏิบัติงานวิชาชีพ "
                lblFacultyName.Text = "คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย"
                lblFooterName.Text = "หน่วยฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย"
                lblAddress.Text = "254 ถนนพญาไท เขตปทุมวัน กรุงเทพมหานคร 10330 โทรศัพท์ 02-218-8450-1 โทรสาร 02-218-8450"
            End If

            'Dim ctlStd As New StudentController
            'Dim ctlPsn As New PersonController
            Dim ctlU As New UserController
            Dim picPaths As String

            dt = ctlU.User_GetInfo(StrNull2Zero(Request.Cookies("UserID").Value))


            If Request.Cookies("UserProfileID").Value = 1 Then
                'dt = ctlStd.Student_GetByCode(Request.Cookies("ProfileID").Value)
                picPaths = stdPic
                lblLocationName.Text = "คณะเภสัชศาสตร์"
            Else
                'dt = ctlPsn.Person_GetByID(StrNull2Zero(Request.Cookies("ProfileID").Value))
                picPaths = personPic

                If dt.Rows.Count > 0 Then
                    lblLocationName.Visible = True
                    lblLocationName.Text = String.Concat(dt.Rows(0).Item("LocationName"))
                End If


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
                hlnkUserName.Text = String.Concat(dt.Rows(0).Item("DisplayName"))  '& " " & dt.Rows(0).Item("LastName") & " (" & dt.Rows(0).Item("NickName") & ")"


            End If

            'Dim ctlA As New PC2Controller
            'Dim ctlCfg As New SystemConfigController

            'If ctlA.PC2_Student_HasPermission(ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR), Request.Cookies("ProfileID").Value) Then
            '    Session("PC2") = True
            'Else
            '    Session("PC2") = False
            'End If


            dt = Nothing





        End If
        LoadCalendarData()

    End Sub
    Dim ctlE As New EventController
    Dim dtCalendar As New DataTable
    Public Shared json As String
    Private Sub LoadCalendarData()
        dtCalendar = ctlE.Event_Get()
        json = JsonConvert.SerializeObject(dtCalendar, Formatting.Indented)
    End Sub

    Protected Sub lnkAccount_Click(sender As Object, e As EventArgs) Handles lnkAccount.Click

        Select Case Request.Cookies("UserProfileID").Value
            Case 1
                Response.Redirect("Student_Reg.aspx?id=" & Request.Cookies("ProfileID").Value)
            Case 2
                Response.Redirect("Person_Reg.aspx?id=" & Request.Cookies("ProfileID").Value)
            Case 3
                Response.Redirect("Person_Reg.aspx?id=" & Request.Cookies("ProfileID").Value)
            Case 4
                Response.Redirect("Person_Reg.aspx?id=" & Request.Cookies("ProfileID").Value)
        End Select

    End Sub

    Protected Sub lnkChangePass_Click(sender As Object, e As EventArgs) Handles lnkChangePass.Click
        Response.Redirect("ChangePassword.aspx")
    End Sub
End Class