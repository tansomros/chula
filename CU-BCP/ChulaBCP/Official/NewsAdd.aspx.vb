Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO

Public Class NewsAdd
    Inherits System.Web.UI.Page

    'Dim _from As String = ""

    Dim dtNews As New DataTable
    Dim ctlN As New NewsController

    Dim sContent, sFile As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not Page.IsPostBack Then
            cmdDelFile.Visible = False

            If Not Request("cate") Is Nothing Then
                ddlCategory.SelectedValue = Request("cate").ToUpper()
            End If

            If Not Request("NewsID") Is Nothing Then
                EditNews(Request("NewsID"))
            Else
                lblNewsID.Text = ctlN.News_GetNextNewsID().ToString()
            End If

            Dim folder As String = Server.MapPath("~/" + ImageNews + lblNewsID.Text)
            If Not Directory.Exists(folder) Then
                Directory.CreateDirectory(folder)
            End If
            ASPxFileManager1.Settings.RootFolder = folder
            ShowNewsType()
        End If
    End Sub
    Private Sub ShowNewsType()
        pnURL.Visible = False
        pnFile.Visible = False
        pnContent.Visible = False
        pnFileManager.Visible = False
        pnContentFile.Visible = False
        Select Case optType.SelectedValue
            Case "URL"
                pnURL.Visible = True
            Case "UPL"
                pnFile.Visible = True
            Case "CON"
                pnContent.Visible = True
                pnContentFile.Visible = True
                pnFileManager.Visible = True
        End Select
    End Sub
    Private Sub EditNews(NewsID As Integer)
        dtNews = ctlN.News_GetByID(NewsID)
        If dtNews.Rows.Count > 0 Then
            lblNewsID.Text = NewsID.ToString()
            txtHead.Text = String.Concat(dtNews.Rows(0).Item("NewsHead"))
            txtDetail.Html = String.Concat(dtNews.Rows(0).Item("NewsDetail"))
            ddlCategory.SelectedValue = String.Concat(dtNews.Rows(0).Item("NewsCate"))
            optType.SelectedValue = String.Concat(dtNews.Rows(0).Item("NewsType"))

            Select Case optType.SelectedValue
                Case "URL"
                    txtURL.Text = String.Concat(dtNews.Rows(0).Item("LinkPath"))
                Case "UPL"
                    hlnkNews.Text = String.Concat(dtNews.Rows(0).Item("FilePath"))
                    hlnkNews.NavigateUrl = "../" & String.Concat(dtNews.Rows(0).Item("FilePath"))
                Case "CON"
                    ASPxFileManager1.Settings.RootFolder = "~/Images/News//" + NewsID.ToString() + "/"
                    If Convert.ToString(dtNews.Rows(0).Item("AttachFileName")) <> "" Then
                        hlnkAttachs.Text = dtNews.Rows(0).Item("AttachFileName").ToString()
                        hlnkAttachs.NavigateUrl = "../" + ImageNews + lblNewsID.Text + "/" + dtNews.Rows(0).Item("AttachFileName").ToString()
                        cmdDelFile.Visible = True
                    Else
                        hlnkAttachs.Text = ""
                        cmdDelFile.Visible = False
                    End If
            End Select

            If Convert.ToString(dtNews.Rows(0).Item("CoverImagePath")) <> "" Then
                imgCover.ImageUrl = "../" + ImageCoverNews + dtNews.Rows(0).Item("CoverImagePath").ToString()
            Else
                imgCover.ImageUrl = ""
            End If
            ShowNewsType()
        End If
    End Sub

    Protected Sub bttSave_Click(sender As Object, e As System.EventArgs) Handles bttSave.Click

        Dim folder As String = Server.MapPath("~/" + ImageNews + lblNewsID.Text)
        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If

        Dim CoverImageFileName, AttachFileName As String
        Dim UlFileName As String = ""
        CoverImageFileName = ""
        AttachFileName = ""

        If FileUploadImage.HasFile Then
            UlFileName = FileUploadImage.FileName
            CoverImageFileName = Path.GetFileName(UlFileName)
            FileUploadImage.SaveAs(Server.MapPath("~/" + ImageCoverNews + UlFileName))
        Else
            CoverImageFileName = Path.GetFileName(imgCover.ImageUrl)
        End If

        If FileUploadFile.HasFile Then
            UlFileName = FileUploadFile.FileName
            AttachFileName = Path.GetFileName(UlFileName)
            FileUploadFile.SaveAs(Server.MapPath("~/" + ImageNews + lblNewsID.Text + "/" + UlFileName))
        Else
            AttachFileName = hlnkAttachs.Text
        End If


        sContent = ""
        If optType.SelectedValue = "UPL" Then
            If FileUpload.HasFile Then
                sFile = ImageNews + lblNewsID.Text + "/" + Path.GetFileName(FileUpload.PostedFile.FileName)
            End If
            txtDetail.Html = ""
        ElseIf optType.SelectedValue = "URL" Then
            sFile = ""
            txtDetail.Html = ""
        Else
            sContent = txtDetail.Html
        End If

        ctlN.News_Save(StrNull2Zero(lblNewsID.Text), ddlCategory.SelectedValue, optType.SelectedValue, txtHead.Text, sContent, CoverImageFileName, AttachFileName, sFile, txtURL.Text, ConvertBoolean2StatusFlag(chkActive.Checked))

        If FileUploadFile.HasFile Then
            ctlN.News_UpdateFileAttachName(StrNull2Zero(lblNewsID.Text), UlFileName)
        End If

        If optType.SelectedValue = "UPL" Then
            UploadFile(FileUpload)
        End If


        'EditNews(lblNewsID.Text)

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)

    End Sub
    Sub UploadFile(ByVal Fileupload As Object)
        Dim FileFullName As String = Fileupload.PostedFile.FileName
        Dim FileName As String = Path.GetFileName(FileFullName)
        If FileName <> "" Then
            Fileupload.SaveAs(Server.MapPath("~/" + ImageNews + lblNewsID.Text + "/" + FileName))
        End If
    End Sub

    Protected Sub optType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optType.SelectedIndexChanged
        ShowNewsType()
    End Sub

    Protected Sub cmdDelFile_Click(sender As Object, e As EventArgs) Handles cmdDelFile.Click
        ctlN.News_UpdateFileAttachName(StrNull2Zero(lblNewsID.Text), "")

        If chkFileExist(Server.MapPath("/" + ImageNews + lblNewsID.Text + "/" + hlnkAttachs.Text)) Then
            File.Delete(Server.MapPath("/" + ImageNews + lblNewsID.Text + "/" + hlnkAttachs.Text))
        End If

        hlnkAttachs.Text = ""
        cmdDelFile.Visible = False
    End Sub
End Class