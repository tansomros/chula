Public Class ResultPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        hlnkBack.NavigateUrl = "Student_Reg.aspx?m=1&p=101"
        hlnkPrint.NavigateUrl = "ReportViewerStudentBio.aspx?ActionType=std&id=" & Request.Cookies("ProfileID").Value
        lblMsg.Text = "บันทึกข้อมูลเรียบร้อย"

        If Request("t") = "closed" Then
            hlnkBack.Visible = False
            hlnkPrint.Visible = False
        ElseIf Request("t") = "p" Then
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
            Case "unrole"
                lblMsg.Text = "ท่านไม่มีสิทธิ์เข้าถึงข้อมูลนี้ <br/> กรุณาติดต่อ ผู้ดูแลระบบ"
                hlnkBack.Visible = False
                hlnkPrint.Visible = False
            Case "assm"
                lblMsg.Text = "<p>หมดเขตประเมินแล้ว </p><p>  The evaluation period has ended.  <br/>  Please contact the administrator for more information.</p>"
            Case "pc2result"
                lblMsg.Text = "ยังไม่ถึงกำหนดวันประกาศผล"
            Case "rcm1"
                lblMsg.Text = "นิสิตยังไม่ได้ไปฝึก จึงไม่สามารถบันทึกคำแนะนำได้"
                hlnkPrint.Visible = False
                hlnkBack.NavigateUrl = "Recommend.aspx?ActionType=rcm&ItemType=rcm1"
            Case "cert"
                lblMsg.Text = "ยังไม่ถึงกำหนดวันเปิดให้ดาวน์โหลด"
                hlnkBack.Visible = False
                hlnkPrint.Visible = False
            Case Else
                lblMsg.Text = "ท่านไม่มีสิทธิ์เข้าถึงข้อมูลส่วนนี้ <br/> กรุณาติดต่อ ผู้ดูแลระบบ/อาจารย์ผู้ประสานงานรายวิชา"
                hlnkBack.Visible = False
                hlnkPrint.Visible = False
        End Select



    End Sub

End Class