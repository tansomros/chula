Public Class ResultPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        hlnkBack.NavigateUrl = "StudentReg.aspx?m=std&p=101"
        hlnkPrint.NavigateUrl = "ReportViewerStudentBio.aspx?ActionType=std&id=" & Request.Cookies("PersonID").Value
        lblMsg.Text = "บันทึกข้อมูลเรียบร้อย"

        If Request("t") = "closed" Then
            hlnkBack.Visible = False
            hlnkPrint.Visible = False
        ElseIf Request("t") = "p" Then
            hlnkBack.Visible = False
            hlnkPrint.Visible = False
        ElseIf Request("t") = "reg" Then
            hlnkBack.Visible = False
            hlnkPrint.Visible = False
        ElseIf Request("t") = "req" Then
            hlnkBack.Visible = False
            hlnkPrint.Visible = False
        ElseIf Request("t") = "doc" Then
            hlnkBack.Visible = False
            hlnkPrint.Visible = False
        End If

        Select Case Request("p")
            Case "select"
                lblMsg.Text = "ยังไม่ถึงกำหนด หรือ หมดเขตการเลือกแหล่งฝึกแล้ว <br/> กรุณาติดต่อ ผู้ดูแลระบบ/อาจารย์ผู้ประสานงานรายวิชา"
            Case "result"
                lblMsg.Text = "ยังไม่ถึงกำหนดวันประกาศผล <br/> กรุณาติดต่อ ผู้ดูแลระบบ/อาจารย์ผู้ประสานงานรายวิชา"
            Case "complete"
                lblMsg.Text = "บันทึกข้อมูลเรียบร้อย"
            Case "delete"
                lblMsg.Text = "ลบข้อมูลเรียบร้อย"
                lblMsg.ForeColor = System.Drawing.Color.Red
            Case "unrole"
                lblMsg.Text = "ท่านไม่มีสิทธิ์เข้าถึงข้อมูลนี้ <br/> กรุณาติดต่อ ผู้ดูแลระบบ"
                hlnkBack.Visible = False
                hlnkPrint.Visible = False
            Case "assm"
                lblMsg.Text = "หมดเขตประเมินแล้ว <br/> กรุณาติดต่อ ผู้ดูแลระบบ"

            Case "pc2result"
                lblMsg.Text = "ยังไม่ถึงกำหนดวันประกาศผล  "
            Case Else
                lblMsg.Text = "ท่านไม่มีสิทธิ์เข้าถึงข้อมูลส่วนนี้ <br/> กรุณาติดต่อ ผู้ดูแลระบบ/อาจารย์ผู้ประสานงานรายวิชา"
                hlnkBack.Visible = False
                hlnkPrint.Visible = False
        End Select



    End Sub

End Class