Public Class DataConfigLevel
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlCfg As New StudentController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            isAdd = True
            txtCode.ReadOnly = True
            LoadCategoryToGrid()
        End If 
        '  txtCode.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        txtValues.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub
    Private Sub LoadCategoryToGrid()
        dt = ctlCfg.Student_GetLevelClass
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
    End Sub

    Private Sub grdData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand

        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument)
            End Select

        End If


    End Sub
    Private Sub EditData(pIndex As Integer)

        isAdd = False
        chkPass.Checked = False
        txtCode.ReadOnly = True
        Me.txtCode.Text = grdData.Rows(pIndex).Cells(0).Text

        If IsNumeric(grdData.Rows(pIndex).Cells(1).Text) Then
            txtValues.Text = grdData.Rows(pIndex).Cells(1).Text
        Else
            txtValues.Text = ""
            chkPass.Checked = True
        End If


    End Sub
     
    Private Sub ClearData()
        txtCode.ReadOnly = True
        Me.txtCode.Text = ""
        txtValues.Text = "" 
        isAdd = True
    End Sub
    Dim item As Integer

    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub lnkSubmitAll_Click(sender As Object, e As EventArgs) Handles lnkSubmitAll.Click
        Try
            item = ctlCfg.StudentLevel_UpdateOneLevel

            LoadCategoryToGrid()
            ClearData()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
        End Try
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtValues.Text = "" Or txtCode.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If
        Try
            Dim isPass As String = "N"
            If chkPass.Checked Then
                isPass = "Y"
            Else
                isPass = "N"
            End If

            item = ctlCfg.StudentLevel_UpdateByCode(txtCode.Text, StrNull2Zero(txtValues.Text), isPass)
            LoadCategoryToGrid()
            ClearData()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
        End Try
    End Sub
End Class