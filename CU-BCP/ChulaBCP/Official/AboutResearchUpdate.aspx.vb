Public Class AboutResearchUpdate
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlA As New ResearchAboutController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            gLang = Request("Code")
            optCode.SelectedValue = gLang
            gPage = Request("p")

            If gPage = "News" Then
                GetNewsData()
            Else
                GetData()
            End If


            Select Case gPage
                'Case "History"
                '    lblPageTitle.Text = "ความเป็นมาสถานวิจัย"
                Case "News"
                    lblPageTitle.Text = "ข่าวสารสถานวิจัย"
                Case "Administration"
                    lblPageTitle.Text = "โครงสร้างการบริหารสถานวิจัย"
                Case "FAQ"
                    lblPageTitle.Text = "คำถามที่พบบ่อย"
                Case "ContactUs"
                    lblPageTitle.Text = "ข้อมูลการติดต่อ"
            End Select
        End If
    End Sub
    Private Sub GetData()
        dt = ctlA.ResearchAbout_Get(gLang, gPage)
        If dt.Rows.Count > 0 Then

            txtDetail.Html = dt.Rows(0)("" & gPage).ToString()
            gLang = dt.Rows(0)("Code")

        End If
        If gLang = "th" Then
            lblLang.Text = "ไทย"
        Else
            lblLang.Text = "อังกฤษ"
        End If

        dt = Nothing
    End Sub
    Private Sub GetNewsData()
        dt = ctlA.ResearchNews_Get(gLang)
        If dt.Rows.Count > 0 Then
            txtDetail.Html = dt.Rows(0)("News_Description").ToString()
            gLang = dt.Rows(0)("Code")

        End If
        dt = Nothing

        If gLang = "th" Then
            lblLang.Text = "ไทย"
        Else
            lblLang.Text = "อังกฤษ"
        End If
    End Sub

    Protected Sub bttSave_Click(sender As Object, e As EventArgs) Handles bttSave.Click
        If gPage = "News" Then
            ctlA.ResearchNews_Update(gLang, txtDetail.Html, Session("userid"))
        Else
            ctlA.ResearchAbout_Update(gLang, gPage, txtDetail.Html, Session("userid"))
        End If


        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optCode.SelectedIndexChanged
        gLang = optCode.SelectedValue
        gPage = Request("p")

        If gPage = "News" Then
            GetNewsData()
        Else
            GetData()
        End If

    End Sub
End Class