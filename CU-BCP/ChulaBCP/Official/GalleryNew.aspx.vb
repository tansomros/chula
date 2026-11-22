Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO
Public Class GalleryNew
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlG As New GalleryController

    Protected Sub Page_Error(sender As Object, e As System.EventArgs) Handles Me.Error
        Response.Redirect("CustomError.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("RoleID") <> "0" Then
            Response.Redirect("../Login.aspx")
        End If

        If Not IsPostBack Then
            pnUpload.Visible = False
            If Request("AlbumID") Is Nothing Then
                lblUID.Text = ctlG.Gallery_GetMaxAlbumID() + 1
            Else
                pnUpload.Visible = True
                lblUID.Text = Request("AlbumID")
                EditAlbum()
                Dim objF As DirectoryInfo = New DirectoryInfo(Server.MapPath("\" & GalleryUpload & "\" & lblUID.Text))
                If Not objF.Exists() Then
                    objF.Create()
                End If
                ASPxFileManager1.Settings.RootFolder = "~\Images\gallery\" & lblUID.Text & "\"
            End If

        End If
    End Sub
    Private Sub EditAlbum()
        dt = ctlG.Gallery_GetByAlbumID(StrNull2Zero(lblUID.Text))
        If dt.Rows.Count > 0 Then
            txtNameTH.Text = String.Concat(dt.Rows(0)("AlbumName_TH"))
            txtTitleTH.Text = String.Concat(dt.Rows(0)("AlbumTitle_TH"))

            txtNameEN.Text = String.Concat(dt.Rows(0)("AlbumName_EN"))
            txtTitleEN.Text = String.Concat(dt.Rows(0)("AlbumTitle_EN"))

            chkTH.Checked = ConvertYN2Boolean(String.Concat(dt.Rows(0)("isTH")))
            chkEN.Checked = ConvertYN2Boolean(String.Concat(dt.Rows(0)("isEN")))

            hlnkCover.Text = String.Concat(dt.Rows(0)("imgCover"))

            hlnkCover.NavigateUrl = "../" & GalleryUpload & "/" & String.Concat(dt.Rows(0)("imgCover"))
            imgCover.ImageUrl = hlnkCover.NavigateUrl

            pnUpload.Visible = True
        End If

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        Dim folder As String = Server.MapPath("~/" & GalleryUpload & "/" & lblUID.Text.ToString())
        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If
        Dim _imgCover As String = ""

        If FileUpload1.HasFile Then
            _imgCover = "cover" & lblUID.Text & ".jpg"
            UploadFile(FileUpload1, lblUID.Text)
        Else
            _imgCover = "cover0.jpg"
        End If

        ctlG.Gallery_Save(StrNull2Zero(lblUID.Text), txtNameTH.Text, txtTitleTH.Text, txtNameEN.Text, txtTitleEN.Text, 1, 0, "", _imgCover, ConvertStatus2YN(chkTH.Checked), ConvertStatus2YN(chkEN.Checked))

        pnUpload.Visible = True
        EditAlbum()
    End Sub
    Sub UploadFile(ByVal Fileupload As Object, sName As String)
        Dim FileFullName As String = Fileupload.PostedFile.FileName
        Dim FileNameInfo As String = Path.GetFileName(FileFullName)
        Dim fileExtension As String = Path.GetExtension(Fileupload.PostedFile.FileName)
        Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & GalleryUpload))
        If FileNameInfo <> "" Then
            Fileupload.PostedFile.SaveAs(objfile.DirectoryName & "\" & "gallery" & "\" & "cover" & sName & fileExtension)
        End If
        objfile = Nothing
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Response.Redirect("GalleryList.aspx")
    End Sub
End Class