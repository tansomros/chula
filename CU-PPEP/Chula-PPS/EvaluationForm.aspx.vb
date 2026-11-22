Public Class EvaluationForm
    Inherits System.Web.UI.Page

    Dim ctlA As New AssessmentController
    Dim ctlU As New UserController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            grdData.PageIndex = 0
            LoadEvaluationGroup()
            LoadEvaluationTopicToGrid()
            LoadEvaluationTopicNotSetFormToGrid()
        End If

    End Sub
    Private Sub LoadEvaluationGroup()
        dt = ctlA.EvaluationGroup_GetActive
        With ddlGroup
            .DataSource = dt
            .DataTextField = "Name"
            .DataValueField = "UID"
            .DataBind()
            .SelectedIndex = 0
        End With
        dt = Nothing
    End Sub
    Private Sub LoadEvaluationTopicToGrid()
        dt = ctlA.EvaluationTopic_GetByGroup(ddlGroup.SelectedValue)
        With grdEvaluation
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With
        dt = Nothing
        lblAlert.Visible = False
    End Sub

    Private Sub LoadEvaluationTopicNotSetFormToGrid()
        dt = ctlA.EvaluationTopicNotSetForm_Get(ddlGroup.SelectedValue, txtSearch.Text)
        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With
        dt = Nothing
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgSelect"
                    AddTopicToEvaluationForm(e.CommandArgument())
            End Select
        End If
    End Sub
    Private Sub AddTopicToEvaluationForm(pUID As Integer)
        Dim item As Integer
        item = ctlA.EvaluationForm_Add(0, pUID, StrNull2Zero(ddlGroup.SelectedValue), "A", "", Request.Cookies("UserID").Value)
        ctlU.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "EvaluationForm", "เพิ่ม หัวข้อการประเมิน/สร้างแบบฟอร์มประเมิน:" & ddlGroup.SelectedItem.Text & " >>TopicID : " & pUID, "")

        grdData.PageIndex = 0
        LoadEvaluationTopicToGrid()
        LoadEvaluationTopicNotSetFormToGrid()
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        Dim item, iSEQNO As Integer
        Dim strAlert As String = ""
        iSEQNO = 0
        For i = 0 To grdEvaluation.Rows.Count - 1
            Dim txtSEQ As TextBox = grdEvaluation.Rows(i).Cells(0).FindControl("txtNo")
            iSEQNO = StrNull2Zero(txtSEQ.Text)
            If iSEQNO <= 0 Then
                strAlert = " >> ตรวจพบว่าท่านระบุการเรียงลำดับข้อไม่ถูกต้อง (มีลำดับ 0 หรือตัวอักขระที่ไม่ใช่ตัวเลข) กรุณาแก้ไขและบันทึกอีกครั้ง"
            End If
            item = ctlA.EvaluationForm_UpdateSEQ(StrNull2Zero(grdEvaluation.DataKeys(i).Value), iSEQNO, Request.Cookies("UserID").Value)
        Next
        grdData.PageIndex = 0
        LoadEvaluationTopicToGrid()
        LoadEvaluationTopicNotSetFormToGrid()
        lblAlert.Text = "บันทึกข้อมูลเรียบร้อย" & strAlert
        lblAlert.Visible = True
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        grdData.PageIndex = 0
        LoadEvaluationTopicNotSetFormToGrid()
    End Sub
    Protected Sub cmdAll_Click(sender As Object, e As EventArgs) Handles cmdAll.Click
        txtSearch.Text = ""
        grdData.PageIndex = 0
        LoadEvaluationTopicNotSetFormToGrid()
    End Sub

    Protected Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadEvaluationTopicNotSetFormToGrid()
    End Sub
    Private Sub grdEvaluation_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdEvaluation.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบหัวข้อนี้ใช่หรือไม่?"");"
            Dim imgD As Image = e.Row.Cells(2).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub ddlGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGroup.SelectedIndexChanged
        LoadEvaluationTopicToGrid()
        grdData.PageIndex = 0
        LoadEvaluationTopicNotSetFormToGrid()
    End Sub

    Protected Sub grdEvaluation_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdEvaluation.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    ctlA.EvaluationForm_Delete(e.CommandArgument())
                    grdData.PageIndex = 0
                    LoadEvaluationTopicToGrid()
                    LoadEvaluationTopicNotSetFormToGrid()
            End Select
        End If
    End Sub
End Class

