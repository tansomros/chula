Public Class GradeEdit
    Inherits System.Web.UI.Page

    Dim ctlA As New AssessmentController
    Dim ctlStd As New StudentController

    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblYear.Text = Request.Cookies("ASSMYEAR").Value
            'LoadStudentInfo()
            LoadEditorInfo()
            LoadAssessmentGrade(Request("uid"))
        End If
        txtScore.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub

    Private Sub LoadEditorInfo()
        Dim ctlPs As New UserController
        lblEditorName.Text = ctlPs.User_GetNameByUserID(Request.Cookies("UserID").Value)
    End Sub

    'Private Sub LoadStudentInfo()
    '    dt = ctlStd.Student_GetByCode(Request("std"))
    '    If dt.Rows.Count > 0 Then
    '        lblStudentCode.Text = String.Concat(dt.Rows(0)("Student_Code"))
    '        lblStudentName.Text = String.Concat(dt.Rows(0)("StudentName"))
    '        lblMajorName.Text = String.Concat(dt.Rows(0)("MajorName"))
    '    End If
    '    dt = Nothing
    'End Sub

    Protected Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        Response.Redirect("GradeModify.aspx")
    End Sub

    Private Sub LoadAssessmentGrade(pUID As Integer)
        dt = ctlA.StudentAssessmentGrade_GetByUID(pUID)
        If dt.Rows.Count > 0 Then
            lblYear.Text = String.Concat(dt.Rows(0)("EduYear"))
            lblSubjectCode.Text = String.Concat(dt.Rows(0)("SubjectCode"))
            lblSubjectName.Text = String.Concat(dt.Rows(0)("SubjectName"))
            hdScore.Value = String.Concat(dt.Rows(0)("TotalScore"))
            hdGrade.Value = String.Concat(dt.Rows(0)("Grade"))
            txtScore.Text = String.Concat(dt.Rows(0)("TotalScore"))
            txtGrade.Text = String.Concat(dt.Rows(0)("Grade"))

            lblStudentCode.Text = String.Concat(dt.Rows(0)("StudentCode"))
            lblStudentName.Text = String.Concat(dt.Rows(0)("StudentName"))
            lblMajorName.Text = String.Concat(dt.Rows(0)("MajorName"))


        End If
        dt = Nothing
    End Sub
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        ctlA.StudentAssessmentGrade_SaveByUID(StrNull2Zero(lblUID.Text), StrNull2Double(txtScore.Text), txtGrade.Text, Request.Cookies("Username").Value)

        ctlA.StudentAssessmentGrade_SaveReason(StrNull2Zero(lblUID.Text), StrNull2Double(hdScore.Value), StrNull2Double(txtScore.Text), hdGrade.Value, txtGrade.Text, txtReason.Text, Request.Cookies("UserID").Value, Request.Cookies("Username").Value)

        Dim acc As New UserController
        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "StudentAssessmentGradeReason", "แก้ไขคะแนนประเมิน/เกรด", lblYear.Text & "|" & lblStudentCode.Text & "|" & lblSubjectCode.Text)

        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")

    End Sub

End Class

