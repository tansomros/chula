Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO

Public Class DocumentManager
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlD As New DocumentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("UserLogin")) Then
            Response.Redirect("Login.aspx")
        End If

        If Not Page.IsPostBack Then
            LoadYearToDDL()
            LoadDocumentName()
            OpenFileManager()
        End If
    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlD.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlD.Document_GetYear
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "EduYear"
                .DataValueField = "EduYear"
                .DataBind()

                If dt.Rows(LastRow)(0) = y Then
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 1).Value = y + 1
                ElseIf dt.Rows(LastRow)(0) > y Then
                    'ddlYear.Items.Add(y + 2)
                    'ddlYear.Items(LastRow + 1).Value = y + 2
                ElseIf dt.Rows(LastRow)(0) < y Then
                    ddlYear.Items.Add(y)
                    ddlYear.Items(LastRow + 1).Value = y
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 2).Value = y + 1
                End If
                .SelectedIndex = 0
            End With
        Else
            ddlYear.Items.Add(y)
            ddlYear.Items(0).Value = y
            ddlYear.Items.Add(y + 1)
            ddlYear.Items(1).Value = y + 1
            ddlYear.SelectedIndex = 0
        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        dt = Nothing
    End Sub
    Private Sub LoadDocumentName()
        Dim ctlSj As New ReferenceValueController
        dt = ctlSj.ReferenceValue_GetByDomainCode("DOCSEN")
        ddlDocName.DataSource = dt
        ddlDocName.DataValueField = "Code"
        ddlDocName.DataTextField = "Descriptions"
        ddlDocName.DataBind()
        dt = Nothing
    End Sub

    Private Sub OpenFileManager()
        Dim folder As String = Server.MapPath("~/" & DocumentStudent & ddlYear.SelectedValue & "/" & ddlDocName.SelectedValue)
        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If
        ASPxFileManager1.Settings.RootFolder = folder
    End Sub
    Private Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        OpenFileManager()
    End Sub

    Private Sub ddlDocName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDocName.SelectedIndexChanged
        OpenFileManager()
    End Sub
End Class