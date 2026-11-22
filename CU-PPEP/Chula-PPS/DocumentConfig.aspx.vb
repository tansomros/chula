
Imports System.IO
Public Class DocumentConfig
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlDoc As New DocumentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then

            txtPath.Text = ctlDoc.Document_GetPath()
            If txtPath.Text = "" Then
                txtPath.Text = "D:\ProgramData\Document\Data\"
            End If
        End If
    End Sub

    Protected Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtPath.Text = "" Then
            DisplayMessage(Me.Page, "ระบุ Path ก่อน")
            Exit Sub
        End If

        If Right(txtPath.Text, 1) <> "\" Then
            txtPath.Text = txtPath.Text & "\"
        End If

        If (Directory.Exists(txtPath.Text) = False) Then
            Directory.CreateDirectory(txtPath.Text)
        End If


        ctlDoc.Document_SavePath(txtPath.Text)

        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub
End Class