Imports System.Collections.Generic
Imports System.Data
Imports System.Web

Public Class PracticeHistory
    Inherits System.Web.UI.Page

    Public dtHis As New DataTable
    Public Property TargetStudentCode As String = ""
    Public Property StudentFullName As String = ""
    Public Property StudentMajorName As String = "-"
    Public Property StudentStudyYear As String = "-"
    Public Property TotalPracticeCount As Integer = 0
    Public Property DistinctYearCount As Integer = 0
    Public Property DistinctLocationCount As Integer = 0
    Public Property DistinctYears As New List(Of String)()
    Public Property IsStudentRole As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadPraticeHistory()
    End Sub

    Private Sub LoadPraticeHistory()
        Dim ctlA As New AssessmentController
        Dim roleCookie As HttpCookie = Request.Cookies("ROLE_STD")
        Dim userCookie As HttpCookie = Request.Cookies("Username")
        Dim nameCookie As HttpCookie = Request.Cookies("NameOfUser")

        IsStudentRole = (roleCookie IsNot Nothing AndAlso String.Equals(roleCookie.Value, "True", StringComparison.OrdinalIgnoreCase))

        If IsStudentRole Then
            TargetStudentCode = If(userCookie IsNot Nothing, String.Concat(userCookie.Value).Trim(), "")
        Else
            TargetStudentCode = If(Request("stdcode") IsNot Nothing, String.Concat(Request("stdcode")).Trim(), "")
        End If

        If TargetStudentCode <> "" Then
            dtHis = ctlA.StudentAssessment_GetByStudent(TargetStudentCode)
        Else
            dtHis = New DataTable()
        End If

        TotalPracticeCount = dtHis.Rows.Count
        lblCount.Text = TotalPracticeCount.ToString("N0")
        lblNot.Visible = (TotalPracticeCount = 0)

        ComputeSummaryStats()
        LoadStudentHeader(If(nameCookie IsNot Nothing, String.Concat(nameCookie.Value).Trim(), ""))
    End Sub

    Private Sub ComputeSummaryStats()
        Dim yearSet As New HashSet(Of String)()
        Dim locSet As New HashSet(Of String)()

        For Each row As DataRow In dtHis.Rows
            Dim yr As String = String.Concat(row("EduYear")).Trim()
            If yr <> "" Then
                yearSet.Add(yr)
            End If

            Dim locKey As String = String.Concat(row("LocationID")).Trim()
            If locKey = "" Then
                locKey = String.Concat(row("LocationName")).Trim()
            End If
            If locKey <> "" Then
                locSet.Add(locKey)
            End If
        Next

        DistinctYearCount = yearSet.Count
        DistinctLocationCount = locSet.Count

        DistinctYears = New List(Of String)(yearSet)
        DistinctYears.Sort(Function(a, b) String.Compare(b, a, StringComparison.Ordinal))
    End Sub

    Private Sub LoadStudentHeader(ByVal fallbackName As String)
        If TargetStudentCode = "" Then
            Exit Sub
        End If

        Try
            Dim ctlStd As New StudentController()
            Dim dtStd As DataTable = ctlStd.Student_GetByCode(TargetStudentCode)
            If dtStd IsNot Nothing AndAlso dtStd.Rows.Count > 0 Then
                Dim row As DataRow = dtStd.Rows(0)
                StudentFullName = (String.Concat(row("Prefix")) & String.Concat(row("FirstName")) & " " & String.Concat(row("LastName"))).Trim()
                Dim majorVal As String = String.Concat(row("MajorName")).Trim()
                StudentMajorName = If(majorVal <> "", majorVal, "-")
                Dim lv As Integer = 0
                Integer.TryParse(String.Concat(row("LevelClass")).Trim(), lv)
                StudentStudyYear = If(lv > 0, "ชั้นปีที่ " & lv.ToString(), "-")
                Exit Sub
            End If
        Catch
            ' Fallback to history/cookie data if student lookup fails
        End Try

        If dtHis.Rows.Count > 0 AndAlso dtHis.Columns.Contains("StudentName") Then
            StudentFullName = String.Concat(dtHis.Rows(0)("StudentName")).Trim()
        Else
            StudentFullName = fallbackName
        End If
    End Sub
End Class
