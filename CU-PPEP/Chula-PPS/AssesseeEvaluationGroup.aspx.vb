Imports System.IO
Imports System.Net.Mail

Public Class AssesseeEvaluationGroup
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim ctlStd As New StudentController
    Dim ctlCs As New CourseController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Session("AssessorUID") Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If


        If Not IsPostBack Then

            lblYear.Text = Request.Cookies("ASSMYEAR").Value
            lblYearEn.Text = StrNull2Zero(Request.Cookies("ASSMYEAR").Value) - 543

            If Request.Cookies("LANG").Value.ToLower() = "en" Then
                lblYearEn.Visible = True
                lblYear.Visible = False
                lblNote.Text = "Note : The scores shown are the scores from the second evaluation only."
                cmdBack.Text = "Student list page"
                cmdSave.Text = "Save Draft"
                cmdSend.Text = "Confirm & Submit final"
            Else
                lblYearEn.Visible = False
                lblYear.Visible = True
            End If

            LoadStudentInfo()
            LoadCourse()
            LoadEvaluationGroupToGrid()

            LoadAssessorInfo()
            LoadAssessmentInfo()

            SumScore()

            GenStudentEvaluation()
            LoadStudentEvaluation()

        End If

    End Sub

    Private Sub LoadAssessorInfo()
        Dim ctlPs As New PersonController
        txtAssessorName.Text = ctlPs.Person_GetName(Session("AssessorUID"))
        If Session("AssessorRoleID") = "P" Then
            lblAssessorRole.Text = "อ.แหล่งฝึก"
        Else
            lblAssessorRole.Text = "อ.ประจำรายวิชา"
        End If

    End Sub
    Private Sub LoadAssessmentInfo()
        dt = ctlStd.GetAssessmentInfo(lblYear.Text, lblCode.Text, lblSubjectCode.Text)
        If dt.Rows.Count > 0 Then
            hdAssessmentID.Value = dt.Rows(0)("AssessmentID")
            hdTimePhaseID.Value = dt.Rows(0)("TimePhaseID")
            'lblPhaseNo.Text = ctlStd.GetPhaseNoOfAssessment(lblYear.Text, lblCode.Text, lblSubjectCode.Text)
            hdPhaseID.Value = String.Concat(dt.Rows(0)("PhaseID"))
            lblPhaseNo.Text = dt.Rows(0)("PhaseNo")
            lblLocationID.Text = dt.Rows(0)("LocationID")
            lblLocationName.Text = dt.Rows(0)("LocationName")
        End If
    End Sub
    Private Sub GenStudentEvaluation()
        ctlLG.StudentEvaluation_Generate(StrNull2Zero(lblYear.Text), lblSubjectCode.Text, lblCode.Text, Session("AssessorUID"), Request.Cookies("Username").Value, Session("AssessorRoleID"), StrNull2Zero(lblLocationID.Text), txtAssessorName.Text, optGrade.SelectedValue, txtComment.Text, StrNull2Zero(hdTimePhaseID.Value), StrNull2Double(lblFullScore.Text))
    End Sub

    Private Sub LoadStudentEvaluation()
        dt = ctlLG.StudentEvaluation_Get(lblYear.Text, lblSubjectCode.Text, lblCode.Text, Session("AssessorUID"), Session("AssessorRoleID"), StrNull2Zero(lblLocationID.Text), StrNull2Zero(hdTimePhaseID.Value))
        If dt.Rows.Count > 0 Then
            txtAssessorName.Text = dt.Rows(0)("AssessorName")
            optGrade.SelectedValue = dt.Rows(0)("EstimateGrade")
            txtComment.Text = String.Concat(dt.Rows(0)("Comments"))

            If Session("AssessorRoleID") = "P" Then
                If String.Concat(dt.Rows(0)("ConfirmStatus")) = "Y" Then
                    cmdSave.Visible = False
                    cmdSend.Visible = False
                    grdData.Columns(4).Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub LoadStudentInfo()
        dt = ctlStd.Student_GetByCode(Request("std"))
        If dt.Rows.Count > 0 Then
            lblCode.Text = String.Concat(dt.Rows(0)("Student_Code"))
            lblStudentName.Text = String.Concat(dt.Rows(0)("StudentName"))
            'lblLevelClass.Text = String.Concat(dt.Rows(0)("LevelClass"))
            lblMajorName.Text = String.Concat(dt.Rows(0)("MajorName"))
            lblNickName.Text = String.Concat(dt.Rows(0)("NickName"))

            If DBNull2Str(String.Concat(dt.Rows(0)("PicturePath"))) <> "" Then
                Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & stdPic & "/" & dt.Rows(0)("PicturePath")))

                If objfile.Exists Then
                    picStudent.ImageUrl = "~/" & stdPic & "/" & dt.Rows(0)("PicturePath")
                Else
                    picStudent.ImageUrl = "~/" & stdPic & "/nopic" & String.Concat(dt.Rows(0)("Gender")) & ".jpg"
                End If
            Else

            End If

        End If
        dt = Nothing
    End Sub
    Private Sub LoadCourse()
        dt = ctlCs.Courses_GetSubjectBySubjectCode(Request.Cookies("ASSMYEAR").Value, Request("Sj"))
        If dt.Rows.Count > 0 Then
            lblSubjectCode.Text = RTrim(dt.Rows(0)("SubjectCode"))
            lblSubjectName.Text = dt.Rows(0)("SubjectName")
            Session("CourseAssessment") = dt.Rows(0)("CourseID")
        End If
        dt = Nothing
    End Sub

    Private Sub LoadEvaluationGroupToGrid()

        If Session("AssessorRoleID") = "A" Then
            dt = ctlLG.EvaluationGroup_GetAssessment(StrNull2Zero(lblYear.Text), lblSubjectCode.Text, "A", lblCode.Text, Session("AssessorUID"))
        ElseIf Session("AssessorRoleID") = "P" Then
            dt = ctlLG.EvaluationGroup_GetAssessment(StrNull2Zero(lblYear.Text), lblSubjectCode.Text, "P", lblCode.Text, Session("AssessorUID"))
        Else
            Response.Redirect("ResultPage.aspx")
        End If

        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        Else
            grdData.DataSource = Nothing
        End If


        dt = Nothing
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    Session("AssessorName") = txtAssessorName.Text
                    SaveAssessorComment()
                    Response.Redirect("AssessmentStudent.aspx?std=" & lblCode.Text & "&guid=" & e.CommandArgument() & "&sj=" & lblSubjectCode.Text)
            End Select
        End If
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub

    Protected Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        If Session("AssessorRoleID") = "A" Then
            Response.Redirect("AssesseeList.aspx?sj=" & lblSubjectCode.Text)
        Else
            Response.Redirect("AssesseeList.aspx?ph=" & hdPhaseID.Value)
        End If
    End Sub
    Private Sub SumScore()
        Dim FullSore, Score As Double
        FullSore = 0
        Score = 0
        For i = 0 To grdData.Rows.Count - 1
            FullSore = FullSore + StrNull2Double(grdData.Rows(i).Cells(2).Text)
            Score = Score + StrNull2Double(grdData.Rows(i).Cells(3).Text)
        Next
        lblFullScore.Text = FullSore
        lblScore.Text = Score
    End Sub

    Private Sub SaveAssessorComment()
        If txtAssessorName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'Warning','Please enter your full name (evaluator) first.');", True)
            Exit Sub
        End If
        If optGrade.SelectedValue = Nothing Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'Warning','กรุณาระบุภาพรวมท่านให้เกรดการฝึกปฏิบัติงานของนิสิตคนนี้อย่างไร / Please select and indicate your overall grade for this student’s internship performance. ? ');", True)
            Exit Sub
        End If

        ctlLG.StudentEvaluation_Save(StrNull2Zero(lblYear.Text), lblSubjectCode.Text, lblCode.Text, Session("AssessorUID"), Request.Cookies("Username").Value, Session("AssessorRoleID"), StrNull2Zero(lblLocationID.Text), txtAssessorName.Text, optGrade.SelectedValue, txtComment.Text, StrNull2Zero(hdTimePhaseID.Value), StrNull2Double(lblFullScore.Text), StrNull2Double(lblScore.Text))

        Session("AssessorName") = txtAssessorName.Text
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveAssessorComment()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย/Completed successfully.');", True)
    End Sub

    Private Sub cmdSend_Click(sender As Object, e As EventArgs) Handles cmdSend.Click

        If txtAssessorName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'Warning','Please enter your full name (evaluator) first.');", True)
            Exit Sub
        End If
        If optGrade.SelectedValue = Nothing Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'Warning','กรุณาระบุภาพรวมท่านให้เกรดการฝึกปฏิบัติงานของนิสิตคนนี้อย่างไร / Please select and indicate your overall grade for this student’s internship performance. ? ');", True)
            Exit Sub
        End If

        ctlLG.StudentEvaluation_Save(StrNull2Zero(lblYear.Text), lblSubjectCode.Text, lblCode.Text, Session("AssessorUID"), Request.Cookies("Username").Value, Session("AssessorRoleID"), StrNull2Zero(lblLocationID.Text), txtAssessorName.Text, optGrade.SelectedValue, txtComment.Text, StrNull2Zero(hdTimePhaseID.Value), StrNull2Double(lblFullScore.Text), StrNull2Double(lblScore.Text))

        '--------------

        ctlLG.StudentEvaluation_Confirm(StrNull2Zero(lblYear.Text), lblSubjectCode.Text, lblCode.Text, Session("AssessorRoleID"), StrNull2Zero(lblLocationID.Text), Request.Cookies("Username").Value, StrNull2Zero(hdTimePhaseID.Value), Session("AssessorUID"))

        Dim Email As String = ""
        Dim ctlU As New UserController
        Email = ctlU.User_GetMail(StrNull2Zero(Request.Cookies("UserID").Value))
        If Email <> "" Then
            SendMailAlert(Email)
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ยืนยันการส่งคะแนนเรียบร้อย/Scores submitted successfully.');", True)
    End Sub
    Private Sub SendMailAlert(ByVal sTo As String)
        Try

            Dim MySubject As String = "ยืนยันการส่งคะแนนประเมิน"
            Dim MyMessageBody As String = ""

            MyMessageBody = "<p>เรียน อาจารย์แหล่งฝึก " & lblLocationName.Text & "<br />    ศูนย์ฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย ได้รับการยืนยันการส่งคะแนนประเมินของ " & lblStudentName.Text & " <br />"
            MyMessageBody &= " ในรายวิชา " & lblSubjectName.Text & " ปีการศึกษา " & lblYear.Text
            MyMessageBody &= "<br/>  หากต้องการเปลี่ยนแปลงหรือแก้ไขคะแนนประเมิน ให้ติดต่อหน่วยฝึกปฏิบัติงานฯ เท่านั้น  ติดต่อแจ้งเรื่องได้ที่ info.opep@pharm.chula.ac.th หรือ 02-218-8450-1 </p> <br />"
            MyMessageBody &= "<p> หน่วยฝึกปฏิบัติงานวิชาชีพ  <br />คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย  <br />254 ถนนพญาไท เขตปทุมวัน กรุงเทพมหานคร 10330  <br />โทร 02-218-8450-1 โทรสาร 02-218-8450</p>"

            Dim RecipientEmail As String = sTo
            Dim SenderEmail As String = "ppepinfo@gmail.com"
            Dim SenderDisplayName As String = "หน่วยฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย"
            Dim SenderEmailPassword As String = "hpxqeawultgdhoim"

            Dim HOST = "smtp.gmail.com"
            Dim PORT = "587" 'TLS Port

            Dim mail As New MailMessage
            mail.Subject = MySubject
            mail.Body = MyMessageBody

            mail.IsBodyHtml = True
            mail.To.Add(RecipientEmail)
            mail.From = New MailAddress(SenderEmail, SenderDisplayName)

            Dim SMTP As New SmtpClient(HOST)
            SMTP.EnableSsl = True
            SMTP.Credentials = New System.Net.NetworkCredential(SenderEmail.Trim(), SenderEmailPassword.Trim())
            SMTP.DeliveryMethod = SmtpDeliveryMethod.Network
            SMTP.Port = PORT
            SMTP.Send(mail)

        Catch ex As Exception


        End Try




    End Sub
End Class

