
Public Class PrivacyPolicy
    Inherits Page
    Dim ctlU As New UserController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If ctlU.User_CheckPolicyAccept(StrNull2Zero(Request.Cookies("userid").Value)) = "Y" Then
                Response.Redirect("Home.aspx?m=h")
            End If
        End If
    End Sub

    Protected Sub cmdSubmit_Click(sender As Object, e As EventArgs) Handles cmdSubmit.Click
        If chkAllow.Checked = False Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'Warning!','กรุณาเลือก Checkbox เพื่อแสดงความยินยอมในการเก็บรวบรวม ใช้ เปิดเผย หรือ สำเนาข้อมูลส่วนบุคคล ก่อน.');", True)

            Exit Sub
        End If

        ctlU.User_AcceptPolicy(StrNull2Zero(Request.Cookies("userid").Value))
        Response.Redirect("Home.aspx?m=h")
    End Sub

End Class