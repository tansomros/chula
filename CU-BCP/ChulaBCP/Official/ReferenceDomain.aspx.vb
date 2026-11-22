Public Class ReferenceDomain
    Inherits System.Web.UI.Page
    Dim ctlM As New ReferenceValueController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        If Not IsPostBack Then
            lblNo.Visible = False
            LoadData()
        End If
        'txtCode.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    lblUID.Text = grdData.Rows(e.CommandArgument()).Cells(0).Text
                    txtCode.Text = grdData.DataKeys(e.CommandArgument()).Value.ToString()
                    txtDescriptions.Text = grdData.Rows(e.CommandArgument()).Cells(2).Text

                Case "imgDel"
                    If ctlM.ReferenceDomain_Delete(e.CommandArgument) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ลบข้อมูลเรียบร้อย');", True)
                        LoadData()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(4).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Private Sub LoadData()
        dt = ctlM.ReferenceDomain_GetSearch(txtSearch.Text)
        grdData.DataSource = dt
        grdData.DataBind()
        dt = Nothing
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        LoadData()
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtCode.Text.Trim = "" Then
            DisplayMessage(Me.Page, "ท่านยังไม่ได้ระบุรหัส")
            Exit Sub
        End If
        If txtDescriptions.Text.Trim = "" Then
            DisplayMessage(Me.Page, "ท่านยังไม่ได้ระบุชื่อ")
            Exit Sub
        End If

        If lblUID.Text = "" Then
            If ctlM.ReferenceDomain_ChkDup(txtCode.Text) Then
                DisplayMessage(Me.Page, "Code นี้มีในระบบแล้ว กรุณาตั้งใหม่")
                Exit Sub
            End If
            ctlM.ReferenceDomain_Add(txtCode.Text, txtDescriptions.Text)
        Else
            ctlM.ReferenceDomain_Update(StrNull2Zero(lblUID.Text), txtCode.Text, txtDescriptions.Text)
        End If

        DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")
        LoadData()
        ClearData()
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearData()
    End Sub
    Private Sub ClearData()
        lblUID.Text = ""
        txtCode.Text = ""
        txtDescriptions.Text = ""
    End Sub
End Class