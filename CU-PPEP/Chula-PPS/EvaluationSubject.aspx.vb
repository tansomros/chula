Public Class EvaluationSubject
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlCs As New AssessmentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblNotic.Visible = False
            LoadYearToDDL()
            LoadSubjectNoEvaluationToGrid()
            LoadEvaluationGroupToGrid()

        End If
    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
        Dim LastRow As Integer
        Dim ctlCrs As New CourseController
        dt = ctlCrs.Courses_GetYear
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

    Private Sub LoadEvaluationGroupToGrid()

        dt = ctlCs.EvaluationGroup_GetSearch(txtSearchEva.Text)
        If dt.Rows.Count > 0 Then

            With grdEvaluation
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
            lblNotic.Visible = False
        Else
            grdEvaluation.Visible = False
            lblNotic.Visible = True
        End If

        dt = Nothing
    End Sub

    Private Sub LoadSubjectNoEvaluationToGrid()
        Dim dt2 As New DataTable

        Dim nCount As Integer = 0

        dt = ctlCs.Subject_GetNoEvaluationSearch(StrNull2Zero(ddlYear.Text), txtSearchSubj.Text)
        nCount = dt.Rows.Count

        If dt.Rows.Count > 0 Then
            lblNo.Visible = False
            With grdSubject
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With

        Else
            grdSubject.Visible = False
        End If

        dt2 = ctlCs.Subject_GetNoEvaluationSearch2(StrNull2Zero(ddlYear.Text), txtSearchSubj.Text)
        nCount = nCount + dt2.Rows.Count

        If dt2.Rows.Count > 0 Then
            lblNo.Visible = False
            With grdSubject2
                .Visible = True
                .DataSource = dt2
                .DataBind()
            End With

        Else
            grdSubject2.Visible = False
        End If

        If nCount <= 0 Then
            lblNo.Visible = True
        Else
            lblNo.Visible = False
        End If
        dt = Nothing
        dt2 = Nothing
    End Sub


    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdEvaluation.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub
    Private Sub AddEvaluationSubject()
        Dim item As Integer
        For i = 0 To grdSubject.Rows.Count - 1
            Dim chkS As CheckBox = grdSubject.Rows(i).Cells(0).FindControl("chkSubj")
            If chkS.Checked Then
                For n = 0 To grdEvaluation.Rows.Count - 1
                    Dim chkE As CheckBox = grdEvaluation.Rows(n).Cells(0).FindControl("chkEva")
                    If chkE.Checked Then
                        item = ctlCs.AssessmentSubject_Add(StrNull2Zero(ddlYear.Text), grdSubject.DataKeys(i).Value, grdSubject.Rows(i).Cells(1).Text, grdEvaluation.DataKeys(n).Value, Request.Cookies("Username").Value)
                    End If
                Next
            End If
        Next

        For i = 0 To grdSubject2.Rows.Count - 1
            Dim chkS2 As CheckBox = grdSubject2.Rows(i).Cells(0).FindControl("chkSubj2")
            If chkS2.Checked Then
                For n = 0 To grdEvaluation.Rows.Count - 1
                    Dim chkE As CheckBox = grdEvaluation.Rows(n).Cells(0).FindControl("chkEva")
                    If chkE.Checked Then
                        item = ctlCs.AssessmentSubject_Add(StrNull2Zero(ddlYear.Text), grdSubject2.DataKeys(i).Value, grdSubject2.Rows(i).Cells(1).Text, grdEvaluation.DataKeys(n).Value, Request.Cookies("Username").Value)
                    End If
                Next
            End If
        Next


        LoadEvaluationGroupToGrid()
        LoadSubjectNoEvaluationToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Private Sub grdSubject_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubject.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Private Sub grdSubject2_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubject2.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub


    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        AddEvaluationSubject()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        For i = 0 To grdEvaluation.Rows.Count - 1
            With grdEvaluation
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                chkS.Checked = False
            End With
        Next
    End Sub

    Protected Sub cmdFindStd_Click(sender As Object, e As EventArgs) Handles cmdFindStd.Click
        LoadSubjectNoEvaluationToGrid()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadEvaluationGroupToGrid()
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadEvaluationGroupToGrid()
        LoadSubjectNoEvaluationToGrid()
    End Sub
End Class

