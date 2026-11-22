'Imports CrystalDecisions.CrystalReports.Engine
Public Class Document
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlDoc As New DocumentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            cmdSave.Visible = False
            cmdCancel.Visible = False
            pnDetail.Visible = False

            LoadDocumentToGrid()

        End If
    End Sub
    Private Sub LoadDocumentToGrid()

        dt = ctlDoc.Document_Get()

        If dt.Rows.Count > 0 Then
            lblCount.Text = dt.Rows.Count

            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        Else
            lblCount.Text = 0
            grdData.Visible = False
        End If
    End Sub

    Private Sub EditData(pUID As Integer)
        dt = ctlDoc.Document_GetByUID(pUID)
        If dt.Rows.Count > 0 Then
            lblUID.Text = dt.Rows(0)("UID")
            txtDocCode.Text = String.Concat(dt.Rows(0)("DocumentCode"))
            txtName.Text = String.Concat(dt.Rows(0)("DocumentName"))
            txtSubjectTitle.Text = String.Concat(dt.Rows(0)("SubjectTitle"))
            lblFileDocName.Text = String.Concat(dt.Rows(0)("MailMergeName"))
            lblFileDataName.Text = String.Concat(dt.Rows(0)("FileDataName"))
            cmdSave.Visible = True
            cmdCancel.Visible = True
            pnDetail.Visible = True
        Else
            pnDetail.Visible = False
            cmdSave.Visible = False
            cmdCancel.Visible = False
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','Error!! กรุณาเลือกเอกสารใหม่อีกครั้ง');", True)
        End If


    End Sub
    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument)
            End Select


        End If
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            e.Row.Cells(0).Text = e.Row.RowIndex + 1

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        ctlDoc.Document_Save(lblUID.Text, txtDocCode.Text, txtName.Text, txtSubjectTitle.Text)

        'ctlDoc.GenerateDocument(StrNull2Zero(lblUID.Text), StrNull2Zero(txtDocCode.Text), StrNull2Zero(lblYear.Text), StrNull2Zero(ddlCourse.SelectedValue))


    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearData()
    End Sub
    Private Sub ClearData()
        lblUID.Text = ""
        txtDocCode.Text = ""
        txtName.Text = ""
        txtSubjectTitle.Text = ""
    End Sub

End Class