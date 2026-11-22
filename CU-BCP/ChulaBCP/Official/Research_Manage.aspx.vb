Imports System.Data
Imports System.IO

Public Class Research_Manage
    Inherits System.Web.UI.Page
    Dim ctlR As New ResearchController
    Dim ctlRf As New ReferenceValueController
    Dim dt As New DataTable
    Dim sAlert As String
    Dim isValid As Boolean

    Dim ctlP As New PersonController

    Dim dtR As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            pnAlert.Visible = False
            LoadResearchType()
            LoadResearchGroup()
            LoadResearchPresent()

            LoadPerson()
            LoadResearch()

            If Not Request("id") Is Nothing Then
                EditResearch(Request("id"))
            End If
        End If
    End Sub
    Private Sub LoadResearch()
        dtR = ctlR.Research_GetBySearch(txtSearch.Text)
        grdResearch.DataSource = dtR
        grdResearch.DataBind()
    End Sub

    Function ValidateData() As Boolean
        sAlert = "กรุณาตรวจสอบ <br />"
        isValid = True

        If txtRYear.Text = "" Then
            sAlert &= "- ปี ค.ศ. <br/>"
            isValid = False
        End If

        If txtRTitle.Text.Trim = "" Then
            sAlert &= "- ชื่อเรื่อง (ไทย) <br/>"
            isValid = False
        End If
        If ddlPerson.SelectedIndex = 0 Then
            sAlert &= "- ชื่อผู้แต่ง <br/>"
            isValid = False
        End If

        Return isValid

    End Function

    Private Sub EditResearch(UID As Integer)
        dt = ctlR.Research_GetByUID(UID)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdfResearchID.Value = .Item("UID")
                txtRYear.Text = String.Concat(.Item("RYear"))
                ddlResearchType.SelectedValue = String.Concat(.Item("RESTYPEUID"))
                ddlResearchGroup.SelectedValue = String.Concat(.Item("RESGRPUID"))
                ddlResearchPresented.SelectedValue = String.Concat(.Item("PRESENTUID"))
                'txtRTitleEN.Text = String.Concat(.Item("TitleEN"))
                txtRTitle.Text = String.Concat(.Item("TitleTH"))
                'txtRAbstract.Text = String.Concat(.Item("Abstract"))
                ddlPerson.SelectedValue = String.Concat(.Item("PersonID"))

                If Convert.ToString(.Item("FileAbstractName")) <> "" Then
                    hlnkAttachs.Text = .Item("FileAbstractName").ToString()
                    hlnkAttachs.NavigateUrl = "../" + DocumentResearch + "/" + .Item("FileAbstractName").ToString()
                    cmdDelFile.Visible = True
                Else
                    hlnkAttachs.Text = ""
                    cmdDelFile.Visible = False
                End If


            End With
        End If
        dt = Nothing
    End Sub

    Private Sub ClearResearchData()
        txtRYear.Text = ""
        'ddlResearchType.SelectedIndex = 0
        'ddlResearchGroup.SelectedIndex = 0
        'ddlResearchPresented.SelectedIndex = 0
        'txtRTitleEN.Text = ""
        ddlPerson.SelectedIndex = 0
        txtRTitle.Text = ""
        'txtRAbstract.Text = ""
        hdfResearchID.Value = 0
    End Sub

    Private Sub LoadResearchType()

        dt = ctlRf.ReferenceValue_GetByDomainCode("RESTYPE")
        ddlResearchType.DataSource = dt
        ddlResearchType.DataValueField = "Code"
        ddlResearchType.DataTextField = "Descriptions"
        ddlResearchType.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadResearchGroup()

        dt = ctlRf.ReferenceValue_GetByDomainCode("RESGRP")
        ddlResearchGroup.DataSource = dt
        ddlResearchGroup.DataValueField = "Code"
        ddlResearchGroup.DataTextField = "Descriptions"
        ddlResearchGroup.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadResearchPresent()

        dt = ctlRf.ReferenceValue_GetByDomainCode("RESPRESENT")
        ddlResearchPresented.DataSource = dt
        ddlResearchPresented.DataValueField = "Code"
        ddlResearchPresented.DataTextField = "Descriptions"
        ddlResearchPresented.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadPerson()

        dt = ctlP.Person_GetByType(1)
        ddlPerson.DataSource = dt
        ddlPerson.DataValueField = "PersonID"
        ddlPerson.DataTextField = "PersonName_TH"
        ddlPerson.DataBind()
        dt = Nothing
    End Sub


    Protected Sub cmdSaveResearch_Click(sender As Object, e As EventArgs) Handles cmdSaveResearch.Click
        If ValidateData() = False Then
            pnAlert.Visible = True
            lblAlert.Text = sAlert
            Exit Sub
        Else
            pnAlert.Visible = False
        End If

        Dim RYear As Integer = StrNull2Zero(txtRYear.Text)

        If RYear > 2500 Then
            RYear = RYear - 543
        End If

        Dim AttachFileName As String = ""
        'Dim UlFileName As String = ""

        If FileUploadFile.HasFile Then
            'UlFileName = FileUploadFile.FileName
            AttachFileName = hdfResearchID.Value + "_" + ddlPerson.SelectedValue + "_" + Path.GetFileName(FileUploadFile.FileName)
            FileUploadFile.SaveAs(Server.MapPath("~/" + DocumentResearch + AttachFileName))
        Else
            AttachFileName = hlnkAttachs.Text
        End If


        ctlR.Research_Save(StrNull2Zero(hdfResearchID.Value), RYear, ddlResearchType.SelectedValue, ddlResearchGroup.SelectedValue, ddlResearchPresented.SelectedValue, StrNull2Zero(ddlPerson.SelectedValue), txtRTitle.Text, "", "A", AttachFileName)
        LoadResearch()

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)


        ClearResearchData()
    End Sub
    Protected Sub cmdDelFile_Click(sender As Object, e As EventArgs) Handles cmdDelFile.Click
        'ctlN.News_UpdateFileAttachName(StrNull2Zero(lblNewsID.Text), "")

        If chkFileExist(Server.MapPath("/" + DocumentResearch + hlnkAttachs.Text)) Then
            File.Delete(Server.MapPath("/" + DocumentResearch + hlnkAttachs.Text))
        End If

        hlnkAttachs.Text = ""
        cmdDelFile.Visible = False
    End Sub
    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearResearchData()
    End Sub

    Protected Sub grdResearch_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdResearch.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEditR"
                    EditResearch(e.CommandArgument())
                Case "imgDelR"
                    ctlR.Research_Delete(e.CommandArgument())
                    LoadResearch()
                    hdfResearchID.Value = 0
            End Select
        End If
    End Sub

    Protected Sub grdResearch_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdResearch.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(7).FindControl("imgDelR")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#c1ffc2';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        grdResearch.PageIndex = 0
        LoadResearch()
    End Sub

    Protected Sub grdResearch_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdResearch.PageIndexChanging
        grdResearch.PageIndex = e.NewPageIndex
        LoadResearch()
    End Sub
End Class