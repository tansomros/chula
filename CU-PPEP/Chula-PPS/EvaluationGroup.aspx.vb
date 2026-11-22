
Public Class EvaluationGroup
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            ClearData()
            lblID.Text = ""
            LoadEvaluationGroupToGrid()
        End If
        txtMin.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        txtMax.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub
    Private Sub LoadEvaluationGroupToGrid()
        dt = ctlLG.EvaluationGroup_Get(txtSearch.Text)
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
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If ctlLG.EvaluationGroup_Delete(e.CommandArgument) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadEvaluationGroupToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub

    Private Sub EditData(ByVal pID As Integer)
        dt = ctlLG.EvaluationGroup_GetByUID(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.lblID.Text = DBNull2Str(.Item("UID"))
                txtName.Text = DBNull2Str(.Item("Name"))
                txtUnit.Text = DBNull2Str(.Item("FullScore"))
                chkStatus.Checked = ConvertStatusFlag2Boolean(DBNull2Str(.Item("StatusFlag")))
                chkTeacher.Checked = ConvertYN2Boolean(dt.Rows(0)("isAdvisor"))
                chkPreceptor.Checked = ConvertYN2Boolean(dt.Rows(0)("isPreceptor"))
                txtAiasName.Text = DBNull2Str(.Item("AliasName"))

                chkCompare.Checked = ConvertYN2Boolean(dt.Rows(0)("isCompare"))
                chkNoScore.Checked = ConvertYN2Boolean(dt.Rows(0)("isNoScore"))

                txtMin.Text = String.Concat(.Item("ScoreMin"))
                txtMax.Text = String.Concat(.Item("ScoreMax"))
                txtRemark.Html = String.Concat(.Item("Remark"))
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub ClearData()
        Me.lblID.Text = ""
        txtName.Text = ""
        txtAiasName.Text = ""
        chkStatus.Checked = True
        txtUnit.Text = ""
        txtMin.Text = "1"
        txtMax.Text = "5"
        chkTeacher.Checked = False
        chkPreceptor.Checked = False
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(10).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtName.Text = "" Or txtUnit.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If

        If txtMin.Text = "" Or txtMax.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If

        If StrNull2Zero(txtMin.Text) > StrNull2Zero(txtMax.Text) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุระดับคะแนนให้ถูกต้อง');", True)
            Exit Sub
        End If

        Dim item As Integer
        item = ctlLG.EvaluationGroup_Save(StrNull2Zero(lblID.Text), txtName.Text, txtAiasName.Text, StrNull2Zero(txtUnit.Text), ConvertBoolean2StatusFlag(chkStatus.Checked), ConvertBoolean2YN(chkTeacher.Checked), ConvertBoolean2YN(chkPreceptor.Checked), ConvertBoolean2YN(chkCompare.Checked), ConvertBoolean2YN(chkNoScore.Checked), StrNull2Zero(txtMin.Text), StrNull2Zero(txtMax.Text), txtRemark.Html)

        LoadEvaluationGroupToGrid()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)


    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadEvaluationGroupToGrid()
    End Sub
    Protected Sub cmdAll_Click(sender As Object, e As EventArgs) Handles cmdAll.Click
        txtSearch.Text = ""
        LoadEvaluationGroupToGrid()
    End Sub
End Class

