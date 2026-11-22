Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO
Public Class Research
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlR As New ResearchController

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If Session("userid") Is Nothing Then
                cmdNew.Visible = False
            Else
                If Session("RoleID") = 0 Then
                    cmdNew.Visible = True
                Else
                    cmdNew.Visible = False
                End If

            End If

            LoadYear()

            If Not Request("y") Is Nothing Then
                txtYear.Text = Request("y")
                Research_Search()
            End If
        End If
    End Sub
    Private Sub LoadYear()
        dt = ctlR.Research_GetYear()
        grdYear.DataSource = dt
        grdYear.DataBind()
    End Sub

    Private Sub Research_Search()
        dt = ctlR.Research_GetBySearch(txtTitle.Text, txtAuthor.Text, txtYear.Text, txtSearch.Text)
        lblCount.Text = dt.Rows.Count.ToString()
        grdData.DataSource = dt
        grdData.DataBind()
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Research_Search()
    End Sub

    Private Sub grdYear_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdYear.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#e0f3ff';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#e0f3ff';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Response.Redirect("Research_Manage.aspx")
    End Sub
End Class