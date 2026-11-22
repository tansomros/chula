Public Class StudentGradeAdmin
    Inherits System.Web.UI.Page

    Dim ctlR As New ResultController
    Dim ctlStd As New StudentController
    Dim dt As New DataTable
    Dim ctlSj As New SubjectController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Request.Cookies("ROLE_ADM").Value = False Then
            Response.Redirect("Login.aspx")
        End If

        If Not IsPostBack Then
            LoadSubjectToDDL()

            txtUnit.Text = ctlSj.Subject_GetUnit(ddlSubject.SelectedValue).ToString

            LoadYearToDDL()

            If Not Request("std") = Nothing Then
                LoadStudentData(Request("std"))
                LoadResultToGrid()
            End If

        End If
    End Sub
    Private Sub LoadSubjectToDDL()

        dt = ctlSj.Subject_Get
        If dt.Rows.Count > 0 Then
            With ddlSubject
                .Enabled = True
                .DataSource = dt
                .DataValueField = "SubjectCode"
                .DataTextField = "SubjectName"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
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

    Private Sub LoadResultToGrid()
        'Try
        dt = ctlR.StudentGrade_GetBySearch(ddlYear.SelectedValue, optTerm.SelectedValue, lblStdCode.Text)

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
        'Catch ex As Exception
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','" & ex.Message & "');", True)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub EditData(uid As Integer)
        dt = ctlR.StudentGrade_GetByUID(uid)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdUID.Value = String.Concat(.Item("UID"))
                ddlSubject.SelectedValue = .Item("SubjectCode")
                ddlYear.SelectedValue = DBNull2Str(.Item("EduYear"))
                optTerm.SelectedValue = String.Concat(.Item("TermNo"))
                txtUnit.Text = String.Concat(.Item("SubjectUnit"))
                txtGrade.Text = String.Concat(.Item("Grade"))
            End With
            cmdDelete.Enabled = True
        Else
            hdUID.Value = "0"
            txtUnit.Text = ""
            txtGrade.Text = ""
        End If
    End Sub


    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    ctlR.StudentGrade_Delete(e.CommandArgument())
                    LoadResultToGrid()
            End Select
        End If
    End Sub
    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadResultToGrid()
    End Sub

    Protected Sub optTerm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optTerm.SelectedIndexChanged
        LoadResultToGrid()
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบข้อมูลนี้ ?"");"
            Dim imgD As System.Web.UI.WebControls.Image = e.Row.Cells(0).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)
        End If

    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        hdUID.Value = "0"
        txtUnit.Text = ""
        txtGrade.Text = ""
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtUnit.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุจำนวนหน่วยกิตก่อน');", True)
            Exit Sub
        End If
        If txtGrade.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุเกรดที่ได้ก่อน');", True)
            Exit Sub
        End If

        ctlR.StudentGrade_Save(StrNull2Zero(hdUID.Value), StrNull2Zero(hdStudentID.Value), lblStdCode.Text, ddlSubject.SelectedValue, StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(optTerm.SelectedValue), txtUnit.Text, txtGrade.Text, Request.Cookies("UserLoginID").Value)

        LoadResultToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)


    End Sub

    Protected Sub ddlSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject.SelectedIndexChanged

        txtUnit.Text = ctlSj.Subject_GetUnit(ddlSubject.SelectedValue).ToString
    End Sub
End Class

