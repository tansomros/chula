Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO
Public Class ResearchDetail
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlR As New ResearchController

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then


            If Not Request("pid") Is Nothing Then
                LoadResearchDetail()
                LoadYear()
            End If
        End If
    End Sub
    Private Sub LoadYear()
        dt = ctlR.Research_GetYear()
        grdYear.DataSource = dt
        grdYear.DataBind()
    End Sub

    Private Sub LoadResearchDetail()
        dt = ctlR.Research_GetByUID(Request("pid"))
        If dt.Rows.Count > 0 Then
            lblTitle.Text = String.Concat(dt.Rows(0)("Title"))
            lblAuthor.Text = String.Concat(dt.Rows(0)("Author"))
            lblType.Text = String.Concat(dt.Rows(0)("ResearchType"))
            lblYear.Text = String.Concat(dt.Rows(0)("RYear"))

            If DBNull2Str(dt.Rows(0)("FileAbstractName")) <> "" Then
                Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & DocumentResearch & "/" & dt.Rows(0).Item("FileAbstractName")))
                If objfile.Exists Then
                    lnkAbstact.NavigateUrl = "../" & DocumentResearch & DBNull2Str(dt.Rows(0)("FileAbstractName"))
                    lnkAbstact.Visible = True
                End If
            End If
        End If
            dt = Nothing
    End Sub

    Private Sub grdYear_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdYear.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#e0f3ff';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub


End Class