Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Download
    Inherits System.Web.UI.Page
    Public dtDow As New DataTable
    Dim ctlM As New DownloadController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("RoleID") <> "0" Then

        End If

        If Not Page.IsPostBack Then
            LoadMedia4Student()
        End If
    End Sub

    Private Sub LoadMedia4Student()

        dtDow = ctlM.Download_GetByCategory(1)

        'With grdMedia4a
        '    .Visible = True
        '    .DataSource = dtDow
        '    .DataBind()

        'For i = 0 To .Rows.Count - 1
        '        Dim img1 As Image = .Rows(i).Cells(0).FindControl("imgNews")
        '        Dim hlnkN As HyperLink = .Rows(i).Cells(0).FindControl("hlnkMedia4a")
        '        hlnkN.Text = dtDow.Rows(i)("Descriptions")

        '        img1.ImageUrl = "images/icon/" & dtDow.Rows(i)("FileIcon")

        '        'Select Case dt.Rows(i)("MediaType")
        '        '    Case "URL"
        '        '        img1.ImageUrl = "images/www.png"
        '        '        hlnkN.NavigateUrl = String.Concat(dt.Rows(i)("LinkPath"))
        '        '    Case "UPL"
        '        '        Dim str As String()
        '        '        str = Split(Right(String.Concat(dt.Rows(i)("LinkPath")), 6), ".")

        '        '        Select Case str(1)
        '        '            Case "pdf"
        '        '                img1.ImageUrl = "images/pdf_download.png"
        '        '            Case "doc", "docx"
        '        '                img1.ImageUrl = "images/word.png"
        '        '            Case "ppt", "pptx"
        '        '                img1.ImageUrl = "images/ppt.png"
        '        '            Case "xls", "xlsx"
        '        '                img1.ImageUrl = "images/xls.png"
        '        '            Case Else
        '        '                img1.ImageUrl = "images/pdf.png"
        '        '        End Select

        '        '        hlnkN.NavigateUrl = DocumentUpload & "/4/" & String.Concat(dt.Rows(i)("LinkPath"))

        '        '    Case "CON"
        '        '        img1.ImageUrl = "images/newspaper.jpg"
        '        '        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
        '        '    Case Else
        '        '        img1.ImageUrl = "images/newspaper.jpg"
        '        '        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
        '        'End Select

        '    Next

        'End With

    End Sub
End Class