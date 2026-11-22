Public Class DeanMessageUpdate
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlA As New DeanController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            gLang = Request("lang")
            optCode.SelectedValue = gLang
            If Request("msgid") = 0 Then
                lblMsgID.Text = ""
            Else
                GetData()
            End If

            If lblMsgID.Text = "" Then
                If gLang = "th" Then
                    txtHead.Text = "สารจากคณบดีสำนักวิชาแพทยศาสตร์"
                    txtTitle.Text = "ประจำเดือน....."
                Else
                    txtHead.Text = "Message from the dean of Institute of Medicine"
                    txtTitle.Text = "<Month> 2018"
                End If

            End If

        End If
    End Sub
    Private Sub GetData()
        dt = ctlA.DeanMessage_GetByID(StrNull2Zero(Request("msgid")))
        If dt.Rows.Count > 0 Then
            lblMsgID.Text = dt.Rows(0)("MsgID").ToString()
            txtHead.Text = dt.Rows(0)("MsgHead_" & gLang).ToString()
            txtTitle.Text = dt.Rows(0)("MsgTitle_" & gLang).ToString()
            txtDetail.Html = dt.Rows(0)("MsgDetail_" & gLang).ToString()
        End If
        dt = Nothing
    End Sub
    Protected Sub bttSave_Click(sender As Object, e As EventArgs) Handles bttSave.Click
        ctlA.DeanMessage_Save(gLang, StrNull2Zero(lblMsgID.Text), txtHead.Text, txtTitle.Text, txtDetail.Html, ConvertCheckedStatus2Number(chkActive.Checked), Session("userid"))
        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub

    Protected Sub optCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optCode.SelectedIndexChanged
        gLang = optCode.SelectedValue
        GetData()
        If lblMsgID.Text = "" Then
            If gLang = "th" Then
                txtHead.Text = "สารจากคณบดีสำนักวิชาแพทยศาสตร์"
                txtTitle.Text = "ประจำเดือน....."
            Else
                txtHead.Text = "Message from the dean of Institute of Medicine"
                txtTitle.Text = "<Month> 2018"
            End If

        End If
    End Sub
End Class