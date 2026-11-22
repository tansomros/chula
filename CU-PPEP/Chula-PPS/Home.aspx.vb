Public Class Home
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlT As New StudentController
    Dim ctlU As New UserController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            LoadMemberCount()
            LoadUserOnline()
            LoadNewsToGrid()

            'If Request.Cookies("UserProfileID").Value = 3 Then
            '    'LoadVPNAccount()
            '    'LoadCreditBalance()
            'End If

        End If

    End Sub
    'Private Sub LoadVPNAccount()
    '    Dim ctlP As New PersonController
    '    Dim ctlV As New VPNController
    '    Dim LID As Integer = ctlP.Person_GetLocationByUserID(Request.Cookies("UserID").Value)

    '    dt = ctlV.VPNAccount_GetByLocation(LID)
    '    If dt.Rows.Count > 0 Then
    '        lblVPNUser.Text = dt.Rows(0)("Username")
    '        lblVPNPass.Text = dt.Rows(0)("Password")
    '    End If
    'End Sub
    Private Sub LoadNewsToGrid()
        Dim ctlNews As New NewsController
        dt = ctlNews.News_GetFirstPage

        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()

            For i = 0 To .Rows.Count - 1
                Dim img1 As Image = .Rows(i).Cells(0).FindControl("imgNews")

                Dim hlnkN As HyperLink = .Rows(i).Cells(0).FindControl("hlnkNews")
                hlnkN.Text = dt.Rows(i)("Title")

                Select Case dt.Rows(i)("NewsType")
                    Case "URL"
                        img1.ImageUrl = "images/www.png"
                        hlnkN.NavigateUrl = String.Concat(dt.Rows(i)("LinkPath"))
                    Case "UPL"

                        Dim str As String()
                        str = Split(Right(String.Concat(dt.Rows(i)("LinkPath")), 6), ".")

                        Select Case str(1)
                            Case "pdf"
                                img1.ImageUrl = "images/pdf.png"
                            Case "doc", "docx"
                                img1.ImageUrl = "images/word.png"
                            Case "ppt", "pptx"
                                img1.ImageUrl = "images/ppt.png"
                            Case "xls", "xlsx"
                                img1.ImageUrl = "images/xls.png"
                            Case Else
                                img1.ImageUrl = "images/pdf.png"
                        End Select

                        hlnkN.NavigateUrl = tmpUpload & "/" & String.Concat(dt.Rows(i)("LinkPath"))
                    Case "CON"
                        img1.ImageUrl = "images/newspaper.jpg"
                        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
                    Case Else
                        img1.ImageUrl = "images/newspaper.jpg"
                        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
                End Select

            Next

        End With

    End Sub
    Private Sub LoadMemberCount()

        lblMember1.Text = String.Concat(ctlT.Student_GetCountByLevelClass(1).ToString("#,##0"))
        lblMember2.Text = String.Concat(ctlT.Student_GetCountByLevelClass(2).ToString("#,##0"))
        lblMember3.Text = String.Concat(ctlT.Student_GetCountByLevelClass(3).ToString("#,##0"))
        lblMember4.Text = String.Concat(ctlT.Student_GetCountByLevelClass(4).ToString("#,##0"))
        lblMember5.Text = String.Concat(ctlT.Student_GetCountByLevelClass(5).ToString("#,##0"))
        lblMember6.Text = String.Concat(ctlT.Student_GetCountByLevelClass(6).ToString("#,##0"))
        lblMember7.Text = String.Concat(ctlT.Student_GetCountByLevelClass(7).ToString("#,##0"))
        lblMember8.Text = String.Concat(ctlT.Student_GetCountByLevelClass(40).ToString("#,##0"))

    End Sub

    Private Sub LoadCreditBalance()
        Dim ctlL As New LocationCreditController

        'Label2.Text = String.Concat(ctlL.Location_GetCreditBalance(Request.Cookies("LocationID").Value).ToString("#,##0"))

    End Sub
    Private Sub LoadUserOnline()
        dt = ctlU.Users_Online
        lblOnline.Text = ""
        If dt.Rows.Count > 0 Then
            dtlMember.DataSource = dt
            dtlMember.DataBind()

            For i = 0 To dt.Rows.Count - 1

                Select Case i Mod 5
                    Case 0
                        lblOnline.Text &= " <span class='label label-success'>" & dt.Rows(i)("Name") & "</span>"
                    Case 1
                        lblOnline.Text &= " <span class='label label-info'>" & dt.Rows(i)("Name") & "</span>"
                    Case 2
                        lblOnline.Text &= " <span class='label label-danger'>" & dt.Rows(i)("Name") & "</span>"
                    Case 3
                        lblOnline.Text &= " <span class='label label-primary'>" & dt.Rows(i)("Name") & "</span>"
                    Case Else
                        lblOnline.Text &= " <span class='label label-warning'>" & dt.Rows(i)("Name") & "</span>"
                End Select
                'If i Mod 2 = 0 Then
                '    lblOnline.Text &= " <span class='label label-success'>" & dt.Rows(i)("Username") & " : " & dt.Rows(i)("Name") & "</span>"
                '    'lblOnline.Text &= "<font color=#1878ca>" & "[" & dt.Rows(i)("Username") & " : " & dt.Rows(i)("Name") & "]" & "</font>"
                'Else
                '    lblOnline.Text &= " <span class='label label-warning'>" & dt.Rows(i)("Username") & " : " & dt.Rows(i)("Name") & "</span>"
                'End If

            Next

        Else
            lblOnline.Text = "ไม่มี User Online ในขณะนี้"
        End If

    End Sub
End Class