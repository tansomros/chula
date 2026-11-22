Imports DevExpress.Web
Public Class VPNModify
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlV As New VPNController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            EditData()
        End If
    End Sub

    Private Sub EditData()
        dt = ctlV.VPNAccount_GetByLocation(Request("lid"))
        If dt.Rows.Count > 0 Then
            hdLocationID.Value = Request("lid")
            lblLocationName.Text = dt.Rows(0)("LocationName").ToString()
            txtUsername.Text = dt.Rows(0)("Username").ToString()
            txtPassword.Text = dt.Rows(0)("Password").ToString()
            chkStatus.Checked = ConvertStatusFlag2Boolean(dt.Rows(0)("StatusFlag"))
        End If

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        ctlV.VPNAccount_Save(hdLocationID.Value, txtUsername.Text, txtPassword.Text, ConvertBoolean2YN(chkStatus.Checked))
        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub

    Protected Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        Response.Redirect("VPNList.aspx")
    End Sub
End Class