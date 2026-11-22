Public Class AssessmentSubject
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            ClearData()
            lblSubjectID.Text = ""
            grdData.PageIndex = 0
            LoadYearToDDL()
            LoadEvaluationToCheckList()
            LoadAssessmentSubjectToGrid()
        End If

    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlLG.GET_DATE_SERVER))
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



    Private Sub LoadEvaluationToCheckList()
        dt = ctlLG.EvaluationGroup_GetActive()
        With chkEva
            .Visible = True
            .DataSource = dt
            .DataTextField = "Name"
            .DataValueField = "UID"
            .DataBind()
        End With
        dt = Nothing
    End Sub

    Private Sub LoadAssessmentSubjectToGrid()
        dt = ctlLG.AssessmentSubject_GetSearch(StrNull2Zero(ddlYear.SelectedValue), txtSearch.Text)
        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()

            'If Request.Cookies("EDUYEAR").Value <> ddlYear.SelectedValue Then
            '    .Columns(4).Visible = False
            '    .Columns(5).Visible = False
            'Else
            '    .Columns(4).Visible = True
            '    .Columns(5).Visible = True
            'End If
            .Columns(4).Visible = True
            .Columns(5).Visible = True
        End With


        dt = Nothing
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If ctlLG.AssessmentSubject_DeleteByUID(e.CommandArgument) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadAssessmentSubjectToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If
            End Select
        End If
    End Sub

    Private Sub EditData(ByVal pID As Integer)
        dt = ctlLG.AssessmentSubject_GetByUID(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                lblEduYear.Text = DBNull2Str(.Item("EduYear"))

                If Request.Cookies("EDUYEAR").Value <> DBNull2Str(.Item("EduYear")) Then
                    cmdSave.Visible = False
                Else
                    cmdSave.Visible = True
                End If


                Me.lblSubjectID.Text = DBNull2Str(.Item("SubjectID"))
                lblSubjectName.Text = DBNull2Str(.Item("SubjectName"))
                lblSubjectCode.Text = DBNull2Str(.Item("SubjectCode"))
                For n = 0 To chkEva.Items.Count - 1
                    For i = 0 To dt.Rows.Count - 1
                        If StrNull2Zero(dt.Rows(i)("EvaluationGroupUID")) = StrNull2Zero(chkEva.Items(n).Value) Then
                            chkEva.Items(n).Selected = True
                            Exit For
                        Else
                            chkEva.Items(n).Selected = False
                        End If
                    Next
                Next
            End With
            pnEdit.Visible = True
        End If
        dt = Nothing
    End Sub
    Private Sub ClearData()
        Me.lblSubjectID.Text = ""
        lblSubjectID.Text = ""
        lblSubjectCode.Text = ""
        lblSubjectName.Text = ""
        chkEva.ClearSelection()
        pnEdit.Visible = False
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(7).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If StrNull2Zero(lblEduYear.Text) < StrNull2Zero(Request.Cookies("EDUYEAR").Value) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านไม่สามารถบันทึกแก้ไขแบบประเมินของปีย้อนหลังได้');", True)
            Exit Sub
        End If

        Dim n As Integer = 0
        For i = 0 To chkEva.Items.Count - 1
            If chkEva.Items(i).Selected Then
                n = n + 1
            End If
        Next

        If n = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาเลือกแบบประเมินที่ต้องการก่อน');", True)
            Exit Sub
        End If


        Dim item, GUID As Integer

        For i = 0 To chkEva.Items.Count - 1
            GUID = chkEva.Items(i).Value
            If chkEva.Items(i).Selected Then
                item = ctlLG.AssessmentSubject_Add(StrNull2Zero(lblEduYear.Text), lblSubjectID.Text, lblSubjectCode.Text, GUID, Request.Cookies("Username").Value)
            Else
                item = ctlLG.AssessmentSubject_Delete(StrNull2Zero(lblEduYear.Text), lblSubjectID.Text, GUID)
            End If
        Next
        grdData.PageIndex = 0
        LoadAssessmentSubjectToGrid()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)

    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadAssessmentSubjectToGrid()
    End Sub
    Protected Sub cmdAll_Click(sender As Object, e As EventArgs) Handles cmdAll.Click
        txtSearch.Text = ""
        grdData.PageIndex = 0
        LoadAssessmentSubjectToGrid()
    End Sub

    Protected Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging

        grdData.PageIndex = e.NewPageIndex
        LoadAssessmentSubjectToGrid()
    End Sub

    Protected Sub ddlYearSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        grdData.PageIndex = 0
        LoadAssessmentSubjectToGrid()
    End Sub
End Class

