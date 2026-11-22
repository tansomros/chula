
Public Class StudentEvaluation
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlA As New AssessmentController
    Dim ctlCs As New CourseController
    Public dtStEva As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            LoadYearToDDL()
        ElseIf Request.Form("__EVENTTARGET") = "toggleConfirm" Then
            ToggleConfirmStatus(Request.Form("__EVENTARGUMENT"))
        End If
        LoadStudentEvaluationToGrid()
    End Sub

    Private Sub ToggleConfirmStatus(uidValue As String)
        Dim uid As Integer = StrNull2Zero(uidValue)
        If uid > 0 Then
            ctlA.StudentEvaluation_UpdateConfirm(uid)
        End If
    End Sub

    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlCs.Courses_GetYearByLang(Request.Cookies("LANG").Value)
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "YearText"
                .DataValueField = "YearValue"
                .DataBind()

                If dt.Rows(LastRow)(0) = y Then
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 1).Value = y + 1
                ElseIf dt.Rows(LastRow)(0) < y Then
                    ddlYear.Items.Add(y)
                    ddlYear.Items(LastRow + 1).Value = y
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 2).Value = y + 1
                End If
                .SelectedIndex = 0
            End With
        Else
            ddlYear.Items.Add(y)
            ddlYear.Items(0).Value = y
            ddlYear.Items.Add(y + 1)
            ddlYear.Items(1).Value = y + 1
            ddlYear.SelectedIndex = 0
        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        dt = Nothing
    End Sub

    Private Sub LoadStudentEvaluationToGrid()
        dtStEva = ctlA.StudentEvaluation_GetByYear(ddlYear.SelectedValue)
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadStudentEvaluationToGrid()
    End Sub

    Protected Function ConfirmDateSortKey(row As DataRow) As String
        If row("ConfirmDate") Is DBNull.Value OrElse String.IsNullOrEmpty(String.Concat(row("ConfirmDate"))) Then
            Return "0"
        End If
        Return CDate(row("ConfirmDate")).ToString("yyyyMMddHHmmss")
    End Function

    Protected Function ConfirmDateDisplay(row As DataRow) As String
        If row("ConfirmDate") Is DBNull.Value OrElse String.IsNullOrEmpty(String.Concat(row("ConfirmDate"))) Then
            Return ""
        End If
        Dim confirmDate As Date = CDate(row("ConfirmDate"))
        Return DisplayShortDateTH(confirmDate) & " " & DisplayTime(confirmDate)
    End Function
End Class

