Public Class AssessmentAdmin
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim ctlStd As New StudentController
    Dim ctlCs As New CourseController
    Dim dt As New DataTable

    Dim StandardScore As Double
    Dim isCompare As Boolean
    Dim ctlSys As New SystemConfigController



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Session("AssessorUID") Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            pnComment.Visible = False

            lblYear.Text = Request.Cookies("ASSMYEAR").Value

            dt = ctlLG.EvaluationGroup_GetByUID(Request("guid"))
            If dt.Rows.Count > 0 Then
                Session("isCompare") = String.Concat(dt.Rows(0)("isCompare"))
                Session("isNoScore") = String.Concat(dt.Rows(0)("isNoScore"))

                Session("ScoreMin") = String.Concat(dt.Rows(0)("ScoreMin"))
                Session("ScoreMax") = String.Concat(dt.Rows(0)("ScoreMax"))
            End If

            If Session("isCompare") = "Y" Then
                Session("stdscore") = StrNull2Double(ctlSys.SystemConfig_GetByCode("STDSCORE"))
            End If


            LoadStudentInfo()

            LoadCourse()
            lblEvaGroup.Text = ctlLG.EvaluationGroup_GetName(Request("guid"))
            LoadAssessmentStudentToGrid()
            LoadAssessmentInfo()
            LoadAssessorInfo()
            txtRemark.Text = ctlLG.AssessmentStudent_GetComments(StrNull2Zero(lblYear.Text), lblSubjectCode.Text, lblStdCode.Text, Request("guid"), Session("AssessorUID"))
        End If

    End Sub
    Private Sub LoadAssessorInfo()
        Dim ctlPs As New PersonController
        lblAssessorName.Text = ctlPs.Person_GetName(Session("AssessorUID"))
        If Session("AssessorRoleID") = "P" Then
            lblAssessorRole.Text = "อ.แหล่งฝึก"
        Else
            lblAssessorRole.Text = "อ.ประจำรายวิชา"
        End If

    End Sub
    Private Sub LoadStudentInfo()
        dt = ctlStd.Student_GetByCode(Request("std"))
        If dt.Rows.Count > 0 Then
            lblStdCode.Text = String.Concat(dt.Rows(0)("Student_Code"))
            lblStudentName.Text = String.Concat(dt.Rows(0)("StudentName"))
            lblMajorName.Text = String.Concat(dt.Rows(0)("MajorName"))
            lblNickName.Text = String.Concat(dt.Rows(0)("NickName"))
        End If
        dt = Nothing
    End Sub

    Private Sub LoadAssessmentInfo()
        dt = ctlStd.GetAssessmentInfo(lblYear.Text, lblStdCode.Text, lblSubjectCode.Text)
        If dt.Rows.Count > 0 Then
            'lblPhaseNo.Text = ctlStd.GetPhaseNoOfAssessment(lblYear.Text, lblCode.Text, lblSubjectCode.Text)
            lblPhaseNo.Text = dt.Rows(0)("PhaseNo")
            lblLocationID.Text = dt.Rows(0)("LocationID")
            lblLocationName.Text = dt.Rows(0)("LocationName")
        End If

    End Sub

    Private Sub LoadCourse()
        dt = ctlCs.Courses_GetSubjectBySubjectCode(Request.Cookies("ASSMYEAR").Value, Request("sj"))
        If dt.Rows.Count > 0 Then
            lblSubjectCode.Text = dt.Rows(0)("SubjectCode")
            lblSubjectName.Text = dt.Rows(0)("SubjectName")
        End If
        dt = Nothing
    End Sub

    Private Sub LoadAssessmentStudentToGrid()
        dt = ctlLG.AssessmentStudent_Get(StrNull2Zero(lblYear.Text), lblSubjectCode.Text, lblStdCode.Text, Request("guid"), Session("AssessorUID"))
        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
            SumScore()
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
                    Response.Redirect("AssessmentStudent.aspx?p=a&std=" & e.CommandArgument() & "&sj=" & lblSubjectCode.Text)
            End Select
        End If
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim acc As New UserController
        SumScore()

        If Session("isCompare") = "Y" Then
            If StrNull2Double(lblTotalScore2.Text) > 0 And StrNull2Double(lblTotalScore2.Text) < Session("stdscore") Then
                pnComment.Visible = True
                If txtComment.Text = "" Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุเหตุผล ในช่องด้านล่างก่อน');", True)
                    Exit Sub
                End If
            Else
                pnComment.Visible = False
            End If
        End If

        Dim sScore1, sScore2 As String
        sScore1 = ""
        sScore2 = ""

        For i = 0 To grdData.Rows.Count - 1

            Dim txtSc1 As TextBox = grdData.Rows(i).Cells(2).FindControl("txtScore1")
            Dim txtSc2 As TextBox = grdData.Rows(i).Cells(3).FindControl("txtScore2")
            Dim chkNA As CheckBox = grdData.Rows(i).Cells(4).FindControl("chkNot")
            sScore1 &= "|" & txtSc1.Text
            sScore2 &= "|" & txtSc2.Text

            ctlLG.AssessmentStudent_Save(StrNull2Zero(lblYear.Text), lblSubjectCode.Text, lblStdCode.Text, Session("AssessorUID"), Request("guid"), grdData.DataKeys(i).Value, StrNull2Double(txtSc1.Text), StrNull2Double(txtSc2.Text), Request.Cookies("Username").Value, Session("AssessorRoleID"), StrNull2Zero(lblLocationID.Text), ConvertBoolean2YN(chkNA.Checked))

        Next
        'ClearData() 

        ctlLG.AssessmentStudent_SaveScore(StrNull2Zero(lblYear.Text), lblSubjectCode.Text, lblStdCode.Text, Session("AssessorUID"), Request("guid"), StrNull2Double(lblScore2.Text), StrNull2Double(lblTotalScore2.Text), Request.Cookies("Username").Value, Session("AssessorRoleID"), StrNull2Zero(lblLocationID.Text), txtRemark.Text, lblAssessorName.Text)

        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_PRC, "StudentAssessment", "ทำแบบประเมิน:" & lblStdCode.Text & ">>" & lblSubjectCode.Text, "Score1:" & sScore1 & "Score2:" & sScore2)

        ctlLG.StudentAssessmentGrade_Save(StrNull2Zero(lblYear.Text), lblSubjectCode.Text, lblStdCode.Text, Request.Cookies("Username").Value)

        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_PRC, "StudentAssessmentGrade", "คำนวนเกรดจากการประเมิน Auto:" & lblStdCode.Text & ">>" & lblSubjectCode.Text, "")


        If Session("isCompare") = "Y" And txtComment.Text <> "" Then
            'บันทึกเหตุผล
            ctlLG.AssessmentStudentReason_Save(StrNull2Zero(lblYear.Text), Request("guid"), lblStdCode.Text, lblSubjectCode.Text, StrNull2Double(lblTotalScore2.Text), Request.Cookies("Username").Value, txtComment.Text)
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)

    End Sub

    Protected Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        Response.Redirect("AssesseeListAdmin.aspx?p=a&std=" & lblStdCode.Text & "&sj=" & lblSubjectCode.Text)
    End Sub

    Protected Sub txtScore1_TextChanged(sender As Object, e As EventArgs)
        SumScore()
    End Sub
    Protected Sub txtScore2_TextChanged(sender As Object, e As EventArgs)
        SumScore()
    End Sub

    Private Sub SumScore()
        Dim sScore1, sScore2, Weight, NoScore As Double
        Dim dtScore As New DataTable
        NoScore = 0
        For i = 0 To grdData.Rows.Count - 1
            Weight = 1
            Dim txtSc1 As TextBox = grdData.Rows(i).Cells(2).FindControl("txtScore1")
            Dim txtSc2 As TextBox = grdData.Rows(i).Cells(3).FindControl("txtScore2")
            Dim chkNA As CheckBox = grdData.Rows(i).Cells(4).FindControl("chkNot")

            If (StrNull2Double(txtSc1.Text) > Session("ScoreMax")) Then
                DisplayMessage(Me.Page, "ท่านสามารถให้คะแนนได้ " & Session("ScoreMin") & " - " & Session("ScoreMax") & " คะแนน")
                txtSc1.Focus()
                Exit Sub
            End If

            If (StrNull2Double(txtSc2.Text) > Session("ScoreMax")) Then
                DisplayMessage(Me.Page, "ท่านสามารถให้คะแนนได้ " & Session("ScoreMin") & " - " & Session("ScoreMax") & " คะแนน")
                txtSc2.Focus()
                Exit Sub
            End If

            Weight = ctlLG.EvaluationTopic_GetWeightRate(StrNull2Zero(grdData.DataKeys(i).Value))

            If chkNA.Checked Then
                NoScore = NoScore + (Session("ScoreMax") * Weight)
            Else
                sScore1 = sScore1 + (StrNull2Double(txtSc1.Text) * Weight)
                sScore2 = sScore2 + (StrNull2Double(txtSc2.Text) * Weight)
            End If
        Next

        lblScore1.Text = sScore1
        lblScore2.Text = sScore2
        Dim Total1, Total2 As Double

        dtScore = ctlLG.EvaluationGroup_GetBaseScore(Request("guid"))
        If dtScore.Rows.Count > 0 Then
            Total1 = (sScore1 * DBNull2Dbl(dtScore.Rows(0)("WEIGHTSCORE"))) / (DBNull2Dbl(dtScore.Rows(0)("BASESCORE") - NoScore))
            lblTotalScore1.Text = Total1.ToString("##.##")
            If lblTotalScore1.Text = "NaN" Then
                lblTotalScore1.Text = "0"
            End If

            Total2 = (sScore2 * DBNull2Dbl(dtScore.Rows(0)("WEIGHTSCORE"))) / (DBNull2Dbl(dtScore.Rows(0)("BASESCORE") - NoScore))
            lblTotalScore2.Text = Total2.ToString("##.##")
            If lblTotalScore2.Text = "NaN" Then
                lblTotalScore2.Text = 0
            End If
        End If

        'If StrNull2Double(lblTotalScore2.Text) > 0 And StrNull2Double(lblTotalScore2.Text) < Request.Cookies("stdscore")  Then
        '    pnComment.Visible = True
        'Else
        '    pnComment.Visible = False
        'End If

    End Sub

    Protected Sub cmdCal_Click(sender As Object, e As EventArgs) Handles cmdCal.Click
        SumScore()
        If Session("isCompare") = "Y" Then
            If StrNull2Double(lblTotalScore2.Text) > 0 And StrNull2Double(lblTotalScore2.Text) < Session("stdscore") Then
                pnComment.Visible = True
            Else
                pnComment.Visible = False
            End If
        End If
    End Sub
    Protected Sub chkNot_CheckedChanged(sender As Object, e As EventArgs)
        SumScore()
    End Sub

End Class

