Public Class AssessmentStudentComment
    Inherits System.Web.UI.Page

    Dim ctlA As New AssessmentController
    Dim ctlStd As New StudentController
    Dim ctlM As New MasterController

    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblYear.Text = Request.Cookies("ASSMYEAR").Value
            hdTimePhaseID.Value = Request("tp")
            hdSubjectCode.Value = Request("sj")
            hdPhaseID.Value = Request("ph")
            LoadPhaseInfo()
            LoadStudentInfo()
            LoadProminent()
            LoadAssessorInfo()

            'If (Not Request("std") Is Nothing) And (Not Request("ph") Is Nothing) Then
            LoadAssessmentCommentText()
                LoadProminentAssessment()
            'End If
        End If
        txtSure.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub
    Private Sub LoadProminent()
        chkPros.Items.Clear()

        dt = ctlM.Prominent_GetByType(PROMINENT_STRENGTHS)
        With chkPros
            .Enabled = True
            .DataSource = dt
            .DataTextField = "Descriptions"
            .DataValueField = "UID"
            .DataBind()
            .Visible = True
        End With

        chkCons.Items.Clear()
        dt = ctlM.Prominent_GetByType(PROMINENT_WEAKNESSES)
        With chkCons
            .Enabled = True
            .DataSource = dt
            .DataTextField = "Descriptions"
            .DataValueField = "UID"
            .DataBind()
            .Visible = True
        End With
        chkPros.ClearSelection()
        chkCons.ClearSelection()
        dt = Nothing
    End Sub

    Private Sub LoadAssessorInfo()
        Dim ctlPs As New UserController
        lblAssessorName.Text = ctlPs.User_GetNameByUserID(Request.Cookies("UserID").Value)
    End Sub
    Private Sub LoadPhaseInfo()
        Dim ctlPh As New TimePhaseController
        dt = ctlPh.TimePhase_ByID(Request("tp"))
        If dt.Rows.Count > 0 Then
            hdTimePhaseID.Value = Request("tp")
            hdSubjectCode.Value = dt.Rows(0)("SubjectCode")
            hdPhaseID.Value = dt.Rows(0)("PhaseID")
            lblPhaseNo.Text = "ผลัดที่ " & dt.Rows(0)("PhaseNo")
        End If
        dt = Nothing
    End Sub
    Private Sub LoadStudentInfo()
        dt = ctlStd.Student_GetByCode(Request("std"))
        If dt.Rows.Count > 0 Then
            lblStudentCode.Text = String.Concat(dt.Rows(0)("Student_Code"))
            lblStudentName.Text = String.Concat(dt.Rows(0)("StudentName"))
            lblMajorName.Text = String.Concat(dt.Rows(0)("MajorName"))
            lblNickName.Text = String.Concat(dt.Rows(0)("NickName"))
        End If
        dt = Nothing
    End Sub

    Protected Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        Response.Redirect("AssesseeComment.aspx?ph=" & hdPhaseID.Value)
    End Sub

    Private Sub LoadAssessmentCommentText()
        dt = ctlA.AssessmentStudentCommentText_Get(StrNull2Zero(lblYear.Text), lblStudentCode.Text, StrNull2Zero(Request.Cookies("LocationID").Value), StrNull2Zero(hdPhaseID.Value), Request.Cookies("UserID").Value)

        If dt.Rows.Count > 0 Then
            txtWorkDetail.Text = String.Concat(dt.Rows(0)("WorkDetail"))
            txtComment1.Text = String.Concat(dt.Rows(0)("Comment1"))
            txtComment2.Text = String.Concat(dt.Rows(0)("Comment2"))
            optGrade.SelectedValue = String.Concat(dt.Rows(0)("StandingGrade"))
            optIsSure.SelectedValue = String.Concat(dt.Rows(0)("IsSure"))
            txtSure.Text = String.Concat(dt.Rows(0)("Sure"))
            txtReason.Text = String.Concat(dt.Rows(0)("Reason"))
            txtComment.Text = String.Concat(dt.Rows(0)("Comments"))
            If StrNull2Zero(String.Concat(dt.Rows(0)("PhaseID"))) = 0 Then
                hdPhaseID.Value = Request("ph")
            Else
                hdPhaseID.Value = String.Concat(dt.Rows(0)("PhaseID"))
            End If

        End If
        dt = Nothing
    End Sub

    Private Sub LoadProminentAssessment()
        chkPros.ClearSelection()

        dt = ctlM.Prominent_GetAssessment(StrNull2Zero(lblYear.Text), lblStudentCode.Text, StrNull2Zero(Request.Cookies("LocationID").Value), Request.Cookies("UserID").Value, PROMINENT_STRENGTHS)

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                For n = 0 To chkPros.Items.Count - 1
                    If dt.Rows(i)("ProsID") = chkPros.Items(n).Value Then
                        chkPros.Items(n).Selected = True
                    End If
                Next
            Next
        End If

        chkCons.ClearSelection()

        dt = ctlM.Prominent_GetAssessment(StrNull2Zero(lblYear.Text), lblStudentCode.Text, StrNull2Zero(Request.Cookies("LocationID").Value), Request.Cookies("UserID").Value, PROMINENT_WEAKNESSES)

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                For n = 0 To chkCons.Items.Count - 1
                    If dt.Rows(i)("ProsID") = chkCons.Items(n).Value Then
                        chkCons.Items(n).Selected = True
                    End If
                Next
            Next
        End If

        dt = Nothing

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If optGrade.SelectedValue = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ท่านยังไม่ได้เลือกเกรดที่ประเมินให้นิสิต');", True)
            Exit Sub
        End If
        ctlA.AssessmentStudentCommentText_Save(StrNull2Zero(lblYear.Text), lblStudentCode.Text, StrNull2Zero(Request.Cookies("LocationID").Value), StrNull2Zero(hdTimePhaseID.Value), Request.Cookies("UserID").Value, txtWorkDetail.Text, txtComment1.Text, txtComment2.Text, optGrade.SelectedValue, optIsSure.SelectedValue, StrNull2Zero(txtSure.Text), txtReason.Text, txtComment.Text, Request.Cookies("Username").Value)

        SaveProminent()

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกเรียบร้อยแล้ว.');", True)

    End Sub
    Private Sub SaveProminent()

        ctlA.AssessmentStudentComment_Delete(StrNull2Zero(lblYear.Text), lblStudentCode.Text, StrNull2Zero(Request.Cookies("LocationID").Value), StrNull2Zero(hdTimePhaseID.Value), Request.Cookies("UserID").Value)

        For i = 0 To chkPros.Items.Count - 1
            If chkPros.Items(i).Selected Then
                ctlA.AssessmentStudentComment_Save(StrNull2Zero(lblYear.Text), lblStudentCode.Text, StrNull2Zero(Request.Cookies("LocationID").Value), StrNull2Zero(hdTimePhaseID.Value), Request.Cookies("UserID").Value, StrNull2Zero(chkPros.Items(i).Value), Request.Cookies("Username").Value, "")
            End If
        Next

        For i = 0 To chkCons.Items.Count - 1
            If chkCons.Items(i).Selected Then
                ctlA.AssessmentStudentComment_Save(StrNull2Zero(lblYear.Text), lblStudentCode.Text, StrNull2Zero(Request.Cookies("LocationID").Value), StrNull2Zero(hdTimePhaseID.Value), Request.Cookies("UserID").Value, StrNull2Zero(chkCons.Items(i).Value), Request.Cookies("Username").Value, "")
            End If
        Next

    End Sub

End Class

