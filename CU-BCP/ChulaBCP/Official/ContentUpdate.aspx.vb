Public Class ContentUpdate
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlA As New PageController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            'gLang = Request("Code")
            Select Case Request("p").ToLower()
                Case "aboutus"
                    lblPageTitle.Text = "ประวัติและวิสัยทัศน์"
                    gPage = PAGE_ABOUTUS
                Case "administration"
                    lblPageTitle.Text = "โครงสร้างการบริหาร"
                    gPage = ""
                Case "policy"
                    lblPageTitle.Text = "นโยบายขององค์กร"
                    gPage = PAGE_POLICY
                Case "contactus"
                    lblPageTitle.Text = "ข้อมูลการติดต่อสอบถาม"
                    gPage = PAGE_CONTACTUS
                Case "programs"
                    lblPageTitle.Text = "ข้อมูลหลักสูตร"
                    gPage = PAGE_PROGRAM
            End Select
            GetData()
        End If
    End Sub
    Private Sub GetData()
        dt = ctlA.PageContent_Get(gPage)
        If dt.Rows.Count > 0 Then
            txtDetail.Html = dt.Rows(0)("Contents").ToString()
        End If
        dt = Nothing
    End Sub
    Protected Sub bttSave_Click(sender As Object, e As EventArgs) Handles bttSave.Click

        ctlA.PageContent_Update(gPage, txtDetail.Html, Request.Cookies("UserLoginID").Value)

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)


    End Sub
End Class