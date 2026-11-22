Public Class DocumentPrint
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlDoc As New DocumentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            LoadDocumentToDDL()
            LoadDocumentToGrid()
        End If
    End Sub
    Private Sub LoadDocumentToDDL()
        ddlDocument.Items.Clear()

        dt = ctlDoc.Document_Get

        If dt.Rows.Count > 0 Then
            ddlDocument.Items.Clear()

            With ddlDocument
                .DataSource = dt
                .DataTextField = "DocumentName"
                .DataValueField = "UID"
                .DataBind()
                .SelectedIndex = 0
            End With

        End If
        dt = Nothing

    End Sub

    Private Sub LoadDocumentToGrid()

        dt = ctlDoc.DocumentPrint_Get(StrNull2Zero(ddlDocument.SelectedValue), StrNull2Zero(Request.Cookies("EDUYEAR").Value), txtSearch.Text)

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

    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        LoadDocumentToGrid()
    End Sub
End Class