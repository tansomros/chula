Public Class FAQCategory
    Inherits System.Web.UI.Page
    Dim ctlM As New FAQController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        If Not IsPostBack Then
            lblNo.Visible = False
            LoadFAQ_Category()
        End If
        txtSort.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        txtUID.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"

                    EditData(e.CommandArgument())

                Case "imgDel"
                    If ctlM.FAQ_Category_Delete(e.CommandArgument) Then

                        'acc.User_GenLogfile(Session("userid"), ACTTYPE_DEL, "ลบชื่อประเภทตัวแทน :" & txtName.Text, "", "", "")

                        DisplayMessage(Me, "ลบข้อมูลเรียบร้อย")
                        LoadFAQ_Category()
                    Else
                        DisplayMessage(Me, "ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง")
                    End If


            End Select


        End If
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(6).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Private Sub EditData(ByVal pcode As Integer)
        dt = ctlM.FAQ_Category_GetByUID(pcode)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.txtUID.Text = DBNull2Str(dt.Rows(0)("UID"))
                txtUID.ReadOnly = True
                txtName.Text = DBNull2Str(dt.Rows(0)("Name"))
                txtSort.Text = DBNull2Str(dt.Rows(0)("Sort"))

                chkStatus.Checked = ConvertStatusFlag2CHK(dt.Rows(0)("StatusFlag"))
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadFAQ_Category()
        dt = ctlM.FAQ_Category_GetBySearch(txtSearch.Text)
        grdData.DataSource = dt
        grdData.DataBind()
        dt = Nothing
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        LoadFAQ_Category()
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtUID.Text.Trim = "" Then
            DisplayMessage(Me.Page, "ท่านยังไม่ได้ระบุรหัส")
            Exit Sub
        End If
        If txtName.Text.Trim = "" Then
            DisplayMessage(Me.Page, "ท่านยังไม่ได้ระบุชื่อ")
            Exit Sub
        End If
        ctlM.FAQ_Category_Save(StrNull2Zero(txtUID.Text), txtName.Text, txtSort.Text, ConvertBoolean2StatusFlag(chkStatus.Checked))
        DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")
        LoadFAQ_Category()
        ClearData()
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearData()
    End Sub
    Private Sub ClearData()
        txtUID.Text = ""
        txtName.Text = ""
        txtSort.Text = ""
        chkStatus.Checked = True
        txtUID.ReadOnly = False
    End Sub
End Class