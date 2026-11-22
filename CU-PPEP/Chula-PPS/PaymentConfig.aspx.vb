
Public Class PaymentConfig
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlPay As New PaymentController
    Dim ctlCs As New Coursecontroller


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Request.Cookies("Username").Value Is Nothing Then
        '    Response.Redirect("Default.aspx?logout=YES")
        'End If
        If Not IsPostBack Then
            grdData.PageIndex = 0

            lblID.Text = ""
            LoadSubjectToDDL()
            LoadPaymentConfig()

        End If

    End Sub

    Private Sub LoadPaymentConfig()
        If Trim(txtSearch.Text) <> "" Then
            dt = ctlPay.PaymentConfig_GetBySearch(Trim(txtSearch.Text))
        Else
            dt = ctlPay.PaymentConfig_Get
        End If

        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadPaymentConfig()
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    ctlPay.PaymentConfig_Delete(e.CommandArgument)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                    LoadPaymentConfig()
            End Select


        End If
    End Sub
    Private Sub EditData(ByVal pID As String)

        dt = ctlPay.PaymentConfig_GetByUID(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                'Me.txtCode.Text = DBNull2Str(dt.Rows(0)("PaymentName"))
                'txtCode.ReadOnly = True
                lblID.Text = DBNull2Str(dt.Rows(0)("UID"))
                ddlSubject.SelectedValue = DBNull2Str(dt.Rows(0)("SubjectCode"))
                txtRateAmount.Text = DBNull2Str(dt.Rows(0)("PayAmount"))
                txtEffectiveTo.Text = DisplayStr2ShortDateTH(DBNull2Str(dt.Rows(0)("EffectiveTo")))
                chkActive.Checked = ConvertStatusFlag2Boolean(DBNull2Str(dt.Rows(0)("IsPublic")))
                optType.SelectedValue = DBNull2Zero(dt.Rows(0)("PaymentType"))

            End With
        End If
        dt = Nothing
    End Sub

    Private Sub ClearData()
        txtRateAmount.Text = ""
        ddlSubject.SelectedIndex = 0
        txtEffectiveTo.Text = ""
        lblID.Text = ""

    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(7).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#dfffae';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub
    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        'If ddl.Text = "" Then

        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน")
        '    Exit Sub
        'End If

        ctlPay.PaymentConfig_Save(StrNull2Zero(lblID.Text), ddlSubject.SelectedValue, optType.SelectedValue, StrNull2Zero(txtRateAmount.Text), ConvertStrDate2DBString(txtEffectiveTo.Text), ConvertBoolean2StatusFlag(chkActive.Checked))


        LoadPaymentConfig()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)

    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadPaymentConfig()
    End Sub
    Private Sub LoadSubjectToDDL()
        ddlSubject.Items.Clear()
        Dim ctlSj As New SubjectController
        dt = ctlSj.Subject_Get
        If dt.Rows.Count > 0 Then
            ddlSubject.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With ddlSubject
                    .DataSource = dt
                    .DataTextField = "SubjectName"
                    .DataValueField = "SubjectCode"
                    .DataBind()
                End With
            Next

        End If

    End Sub

End Class

