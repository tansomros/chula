
Public Class AssessmentModify
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlAss As New AssessmentController
    Dim ctlCs As New CourseController
    Dim acc As New UserController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            'LoadSkillToDDL()
            LoadAssessment(Request("id"))
        End If
    End Sub

    Private Sub LoadLocationToDDL()
        ddlLocation.Items.Clear()
        Dim ctlL As New LocationController
        Dim dtL As New DataTable
        'dtL = ctlL.LocationAssessment_Get(StrNull2Zero(txtYear.Text))
        dtL = ctlCs.CourseLocation_GetLocationInCourse(StrNull2Zero(txtYear.Text), txtCourseID.Text)
        If dtL.Rows.Count > 0 Then
            ddlLocation.Items.Clear()
            With ddlLocation
                .Items.Add("")
                .Items(0).Value = 0

                For i = 0 To dtL.Rows.Count - 1
                    .Items.Add("" & dtL.Rows(i)("LocationName"))
                    .Items(i + 1).Value = dtL.Rows(i)("LocationID")
                Next
            End With
        End If

    End Sub
    'Private Sub LoadSkillToDDL()
    '    dt = ctlCs.Courses_Get4Selection(StrNull2Zero(txtYear.Text))
    '    If dt.Rows.Count > 0 Then
    '        With ddlSubject
    '            .Enabled = True
    '            .DataSource = dt
    '            .DataTextField = "SubjectName"
    '            .DataValueField = "CourseID"
    '            .DataBind()
    '            .SelectedIndex = 0
    '        End With
    '    End If
    '    dt = Nothing
    'End Sub

    Private Sub LoadTimePhase(Optional sKey As String = "")
        Dim ctlTP As New TimePhaseController
        Dim dtTP As New DataTable
        dtTP = ctlTP.TimePhase_GetBySubjectYear(StrNull2Zero(txtYear.Text), txtSubjectCode.Text)
        If dtTP.Rows.Count > 0 Then
            With ddlPhase
                .Visible = True
                .DataSource = dtTP
                .DataTextField = "PhaseName"
                .DataValueField = "TimePhaseID"
                .DataBind()
                .SelectedIndex = 0
            End With
        Else
            ddlPhase.DataSource = Nothing
            ddlPhase.Visible = False
        End If

    End Sub

    Private Sub LoadAssessment(AssessmentID As Integer)

        dt = ctlAss.Assessment_GetByUID(AssessmentID)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                txtYear.Text = .Item("PYear")
                txtUID.Text = .Item("AssessmentID")
                txtStudentCode.Text = .Item("Student_Code")
                txtName.Text = .Item("StudentName")
                txtCourseID.Text = .Item("CourseID")
                txtSubjectCode.Text = .Item("SubjectCode")
                txtSubjectName.Text = .Item("SubjectName")

                LoadTimePhase()
                LoadLocationToDDL()
                ddlLocation.SelectedValue = .Item("LocationID")
                ddlPhase.SelectedValue = .Item("TimePhaseID")
            End With
        End If
        dt = Nothing
    End Sub
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        ctlAss.Assessment_Update(StrNull2Zero(txtUID.Text), txtYear.Text, txtSubjectCode.Text, txtStudentCode.Text, StrNull2Zero(ddlLocation.SelectedValue), StrNull2Zero(ddlPhase.SelectedValue), StrNull2Zero(txtCourseID.Text), Request.Cookies("UserID").Value)

        DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")
    End Sub

    Protected Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click

        Response.Redirect("ReportAssesmentByStudent.aspx?ActionType=asg&ItemType=find")
    End Sub
End Class

