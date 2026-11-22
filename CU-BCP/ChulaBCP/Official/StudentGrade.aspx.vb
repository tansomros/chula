Public Class StudentGrade
    Inherits System.Web.UI.Page

    Dim ctlR As New ResultController
    'Dim ctlCs As New CourseController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            LoadYearToDDL()
            If Request("std") = Nothing Then
                LoadStudentData(Request.Cookies("PersonID").Value)
            Else
                LoadStudentData(Request("std"))
            End If

            LoadResultToGrid()
        End If
        UpdateProgress1.DisplayAfter = 0
        UpdateProgress1.Visible = True
    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlR.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlR.StudentGrade_GetYear()
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "EduYear"
                .DataValueField = "EduYear"
                .DataBind()

                If dt.Rows(LastRow)(0) = y Then
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 1).Value = y + 1
                ElseIf dt.Rows(LastRow)(0) > y Then
                    'ddlYear.Items.Add(y + 2)
                    'ddlYear.Items(LastRow + 1).Value = y + 2
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
    Private Sub LoadStudentData(pid As Integer)
        Dim ctlStd As New StudentController
        dt = ctlStd.GetStudent_ByID(pid)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdStudentID.Value = String.Concat(.Item("StudentID"))
                lblStdCode.Text = .Item("StudentCode")
                lblStudentName.Text = DBNull2Str(.Item("StudentName"))
                lblMajor.Text = String.Concat(.Item("MajorName"))
            End With
        End If
    End Sub
    Private Sub LoadResultToGrid()
        Try

            'System.Threading.Thread.Sleep(1000)
            UpdateProgress1.DisplayAfter = 0
            UpdateProgress1.Visible = True
            'If Request.Cookies("ROLE_ADM").Value = True Then

            dt = ctlR.StudentGrade_GetBySearch(ddlYear.SelectedValue, optTerm.SelectedValue, lblStdCode.Text)
            'Else
            '    dt = ctlR.StudentGrade_GetByPersonID(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCourse.SelectedValue), DBNull2Zero(Request.Cookies("ProfileID").Value), txtSearch.Text)
            'End If
            If dt.Rows.Count > 0 Then
                With grdData
                    .Visible = True
                    .DataSource = dt
                    .DataBind()
                End With
            Else
                grdData.DataSource = Nothing
                grdData.Visible = False
            End If

            dt = Nothing
            UpdateProgress1.Visible = False
        Catch ex As Exception
            grdData.Visible = False
        End Try
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    Response.Redirect("AssesseeEvaluationGroup.aspx?p=a&id=" & e.CommandArgument())
            End Select
        End If
    End Sub
    Protected Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        'UpdateGrade() 
        System.Threading.Thread.Sleep(1000)
        ReportName = "StudentGrade"
        FagRPT = "StudentGrade"
        Reportskey = "PDF"

        'DisplayPopUpWindows(Me.Page, "window.open('" + ResolveUrl("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&c=" & ddlCourse.SelectedValue + "','pop','width=100,height=100,left=270,top=180,titlebar=no,menubar=no,resizable=yes,toolbar=no,scrollbars=no');window.close()"))

        'Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&c=" & ddlCourse.SelectedValue)


        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Download", "window.location='ReportViewer.aspx?R=grd&y=" & ddlYear.SelectedValue & "&t=" & optTerm.SelectedValue & "&std=" & hdStudentID.Value & "';", True)


        'UpdateProgress1.Visible = True
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        LoadResultToGrid()
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadResultToGrid()
    End Sub

    Protected Sub optTerm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optTerm.SelectedIndexChanged
        LoadResultToGrid()
    End Sub
End Class

