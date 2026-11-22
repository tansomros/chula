Public Class WorkOutSideList
    Inherits System.Web.UI.Page
    Dim ctlS As New SeminarController
    Dim dt As New DataTable
    Dim ctlRf As New ReferenceValueController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If

        If Not IsPostBack Then
            LoadYear()
            LoadWorkType()
            LoadSeminarGroup()
            LoadCountry()
            LoadBudget()
            LoadData()
        End If
    End Sub

    Private Sub LoadYear()
        dt = ctlS.Seminar_GetYear
        If dt.Rows.Count > 0 Then
            ddlYear.DataSource = dt
            ddlYear.DataValueField = "UID"
            ddlYear.DataTextField = "SYEAR"
            ddlYear.DataBind()
        Else
            ddlYear.Items.Add("")
            ddlYear.Items(0).Value = ""
            ddlYear.Items.Add(Now.Date.Year().ToString())
            ddlYear.Items(1).Value = Now.Date.Year().ToString()
        End If

        dt = Nothing
    End Sub

    Private Sub LoadWorkType()
        dt = ctlRf.ReferenceValue_GetByDomainCode("WKTYPE", "Y")
        ddlWorkType.DataSource = dt
        ddlWorkType.DataValueField = "Code"
        ddlWorkType.DataTextField = "Descriptions"
        ddlWorkType.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadSeminarGroup()
        dt = ctlRf.ReferenceValue_GetByDomainCode("SEMGRP", "Y")
        ddlGroup.DataSource = dt
        ddlGroup.DataValueField = "Code"
        ddlGroup.DataTextField = "Descriptions"
        ddlGroup.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadCountry()
        dt = ctlRf.ReferenceValue_GetByDomainCode("COUNTRY", "Y")
        ddlCountry.DataSource = dt
        ddlCountry.DataValueField = "Code"
        ddlCountry.DataTextField = "Descriptions"
        ddlCountry.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadBudget()
        dt = ctlRf.ReferenceValue_GetByDomainCode("BUDGET", "Y")
        ddlBudget.DataSource = dt
        ddlBudget.DataValueField = "Code"
        ddlBudget.DataTextField = "Descriptions"
        ddlBudget.DataBind()
        dt = Nothing
    End Sub

    Private Sub LoadData()
        dt = ctlS.Seminar_GetBySearch(StrNull2Zero(ddlYear.SelectedValue), ddlWorkType.SelectedValue, ddlGroup.SelectedValue, optIsPresented.SelectedValue, ddlCountry.SelectedValue, ddlBudget.SelectedValue, txtSearch.Text)

        grdData.DataSource = dt
            grdData.DataBind()


    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    Response.Redirect("WorkOutSide_Manage.aspx?id=" & e.CommandArgument())
                Case "imgDel"
                    ctlS.Seminar_Delete(e.CommandArgument())
                    LoadData()
            End Select


        End If
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(2).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#c1ffc2';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        LoadData()
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Response.Redirect("WorkOutSide_Manage.aspx")
    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Response.Redirect("WorkOutSide_Manage.aspx")
    End Sub

    Protected Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        FagRPT = "EXCEL"
        ReportName = "SeminarWorkByYear"
        Response.Redirect("ReportViewer.aspx?y=" & ddlYear.SelectedValue)
    End Sub
End Class