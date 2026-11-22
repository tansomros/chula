Imports System.IO
Imports System.Data.OleDb
Imports System.Data
Imports Subgurim.Controles

Public Class Student
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlStd As New StudentController
    Public dtStd As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        'If IsNothing(Request.Cookies("Username").Value) Then
        '    Response.Redirect("../Default.aspx?logout=YES")
        'End If

        If Not IsPostBack Then
            LoadMajorToDDL()
        End If
        LoadStudent()
    End Sub
    Private Sub LoadMajorToDDL()
        Dim dtMajor As New DataTable
        Dim ctlFct As New FacultyController
        dtMajor = ctlFct.Major_GetForSearch
        If dtMajor.Rows.Count > 0 Then
            With ddlMajor
                .Enabled = True
                .DataSource = dtMajor
                .DataTextField = "MajorName"
                .DataValueField = "MajorID"
                .DataBind()
                .SelectedIndex = 0
            End With
        Else

        End If
        dtMajor = Nothing
    End Sub
    Private Sub LoadStudent()
        dtStd = ctlStd.Student_GetSearch(ddlMajor.SelectedValue, ddlLevel.SelectedValue, txtSearch.Text)
    End Sub


    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadStudent()
    End Sub

    Protected Sub cmdAll_Click(sender As Object, e As EventArgs) Handles cmdAll.Click
        txtSearch.Text = ""
        ddlLevel.SelectedIndex = 0
        ddlMajor.SelectedIndex = 0
        LoadStudent()
    End Sub

End Class