Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO

Public Class NewsAdd2
    Inherits System.Web.UI.Page

    'Dim _from As String = ""

    Dim dtNews As New DataTable
    Dim ctlN As New NewsController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("ChulaBCP")) Then
            Response.Redirect("Login.aspx")
        End If

        If Not Page.IsPostBack Then
            cmdDelFile.Visible = False

            'If Not Request("cate") Is Nothing Then
            '    ddlCategory.SelectedValue = Request("cate")
            'End If

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

        End If
    End Sub

    Private Sub EditNews(NewsID As Integer)
        dtNews = ctlN.News_GetByID(NewsID)
        If dtNews.Rows.Count > 0 Then
            lblNewsID.Text = NewsID.ToString()
            txtHead.Text = dtNews.Rows(0).Item("NewsHead")
            txtDetail.Html = dtNews.Rows(0).Item("NewsDetail")
            'ddlCategory.SelectedValue = String.Concat(dtNews.Rows(0).Item("NewsCate"))
            If Convert.ToString(dtNews.Rows(0).Item("CoverImagePath")) <> "" Then
                imgCover.ImageUrl = "../" + ImageCoverNews + "/" + dtNews.Rows(0).Item("CoverImagePath").ToString()
            Else
                imgCover.ImageUrl = ""
            End If
            If Convert.ToString(dtNews.Rows(0).Item("FilePath")) <> "" Then
                hlnkAttachs.Text = dtNews.Rows(0).Item("FilePath").ToString()
                hlnkAttachs.NavigateUrl = "../" + ImageNews + lblNewsID.Text + "/" + dtNews.Rows(0).Item("FilePath").ToString()
                cmdDelFile.Visible = True
            Else
                hlnkAttachs.Text = ""
                cmdDelFile.Visible = False
            End If
            ASPxFileManager1.Settings.RootFolder = "~/Images/News//" + NewsID.ToString() + "/"
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
            FileUploadImage.SaveAs(Server.MapPath("~/" + ImageNews + lblNewsID.Text + "/" + UlFileName))
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

        ctlN.News_Save(StrNull2Zero(lblNewsID.Text), "NEWS", txtHead.Text, txtDetail.Html, CoverImageFileName, AttachFileName, ConvertBoolean2StatusFlag(chkActive.Checked))

        If FileUploadFile.HasFile Then
            ctlN.News_UpdateFileAttachName(StrNull2Zero(lblNewsID.Text), UlFileName)
        End If

        'EditNews(lblNewsID.Text)

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)

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