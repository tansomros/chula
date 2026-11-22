Public Class ResultProcess
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        hlnkBack.NavigateUrl = "Default.aspx?m=h"
        'lblMsg.Text = "บันทึกข้อมูลเรียบร้อย"
        hlnkBack.Visible = True

        If Request("t") = "closed" Then
            hlnkBack.Visible = False
            'ElseIf Request("t") = "p" Then
            '    hlnkBack.Visible = True
        End If

        Select Case Request("p")
            Case "del"
                lblMsg.Text = "ท่านต้องการลบข้อมูลการลงทะเบียนนี้ใช่หรือไม่?"
                cmdDelete.Visible = True
            Case "result"
                lblMsg.Text = "ยังไม่ถึงกำหนดวันประกาศผล <br/> กรุณาติดต่อ ผู้ดูแลระบบ/อาจารย์ผู้ประสานงานรายวิชา"
            Case "complete"
                lblMsg.Text = "บันทึกข้อมูลเรียบร้อย"
            Case "unrole"
                lblMsg.Text = "ท่านไม่มีสิทธิ์เข้าถึงข้อมูลนี้ <br/> กรุณาติดต่อ ผู้ดูแลระบบ"
                hlnkBack.Visible = False
            Case "assm"
                lblMsg.Text = "หมดเขตประเมินแล้ว <br/> กรุณาติดต่อ ผู้ดูแลระบบ"

            Case "pc2result"
                lblMsg.Text = "ยังไม่ถึงกำหนดวันประกาศผล  "
            Case Else
                lblMsg.Text = "ท่านไม่มีสิทธิ์เข้าถึงข้อมูลส่วนนี้ <br/> กรุณาติดต่อ ผู้ดูแลระบบ/อาจารย์ผู้ประสานงานรายวิชา"
                hlnkBack.Visible = False
        End Select
    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click

        Select Case Request("t").ToLower()
            Case "reg"
                Dim ctlR As New RegisterController
                ctlR.Register_Delete(Request("regid"), Request("pid"), Request("tid"))
            Case "req"
                Dim ctlR As New RequestController
                ctlR.Request_Delete(Request("reqid"), Request("pid"), Request("tid"))
            Case "doc"
                Dim ctlR As New DocumentController
                ctlR.Document_Delete(Request("docid"), Request("pid"), Request("tid"))
        End Select
        Response.Redirect("ResultPage?t=p&p=delete")
    End Sub
End Class