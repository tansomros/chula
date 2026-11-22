Public Class ErrorPage500
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Select Case Request("p")
            Case "grademod"
                lblMsg.Text = "ยังไม่สิ้นสุดระยะเวลาการประเมิน ท่านไม่สามารถแก้ไขเกรดได้"
                lblDetail.Text = "ท่านสามารถแก้ไขเกรดได้ หลังจากสิ้นสุดระยะเวลาการประเมินคะแนนการฝึกฯ โปรดเข้ามาใหม่ภายหลัง หรือติดต่อ หน่วยฝึกปฏิบัติวิชาชีพ"
            Case "select"
                lblMsg.Text = "ยังไม่ถึงกำหนด หรือ หมดเขตการเลือกแหล่งฝึกแล้ว <br/> กรุณาติดต่อ ผู้ดูแลระบบ/อาจารย์ผู้ประสานงานรายวิชา"
            Case "result"
                lblMsg.Text = "ยังไม่ถึงกำหนดวันประกาศผล <br/> กรุณาติดต่อ ผู้ดูแลระบบ/อาจารย์ผู้ประสานงานรายวิชา"

            Case "unrole"
                lblMsg.Text = "ท่านไม่มีสิทธิ์เข้าถึงข้อมูลนี้ <br/> กรุณาติดต่อ ผู้ดูแลระบบ"
            Case "assm"
                lblMsg.Text = "หมดเขตประเมินแล้ว <br/> กรุณาติดต่อ ผู้ดูแลระบบ"
            Case Else
                lblMsg.Text = "ท่านไม่มีสิทธิ์เข้าถึงข้อมูลส่วนนี้ <br/> กรุณาติดต่อ ผู้ดูแลระบบ/อาจารย์ผู้ประสานงานรายวิชา"

        End Select



    End Sub

End Class