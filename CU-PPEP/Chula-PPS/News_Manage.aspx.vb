Imports System.IO
Public Class News_Manage
    Inherits System.Web.UI.Page
    Dim ctlNews As New NewsController
    Dim sFile As String = ""
    Dim tmp As Integer
    Dim sContent As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Not Request("id") Is Nothing Then
                LoadNews()
            End If
            ShowNewsType()
        End If
    End Sub
    Private Sub LoadNews()
        Dim dtN As New DataTable

        dtN = ctlNews.GetNews_ByID(Request("id"))
        If dtN.Rows.Count > 0 Then
            With dtN.Rows(0)
                lblID.Text = .Item("NewsID")
                txtTitle.Text = .Item("Title")
                txtURL.Text = String.Concat(.Item("LinkPath"))
                hlnkNews.Text = String.Concat(.Item("LinkPath"))
                hlnkNews.NavigateUrl = tmpUpload & "/" & String.Concat(.Item("LinkPath"))
                chkFistPage.Checked = Decimal2Boolean(.Item("FirstPage"))
                chkStatus.Checked = Decimal2Boolean(.Item("IsPublic"))
                optType.SelectedValue = String.Concat(.Item("NewsType"))
                xHtmlDetail.Html = String.Concat(.Item("ContentNews"))
                txtOrder.Text = String.Concat(.Item("NewsOrder"))
                ShowNewsType()
            End With

        End If
        dtN = Nothing
    End Sub
    Private Sub SaveNews()

        Dim OrderNo As Integer = 99

        If txtTitle.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกหัวเรื่องที่ประกาศก่อน');", True)
            Exit Sub
        End If
        sContent = ""
        If optType.SelectedValue = "UPL" Then
            If FileUpload.HasFile Then
                sFile = Path.GetFileName(FileUpload.PostedFile.FileName)
            End If
            xHtmlDetail.Html = ""
        ElseIf optType.SelectedValue = "URL" Then
            sFile = txtURL.Text
            xHtmlDetail.Html = ""

        Else
            sContent = xHtmlDetail.Html
        End If


        If StrNull2Zero(txtOrder.Text) = 0 Then
            OrderNo = 99
        Else
            OrderNo = StrNull2Zero(txtOrder.Text)
        End If
        If lblID.Text <> 0 Then
            tmp = ctlNews.News_Update(lblID.Text, txtTitle.Text, Boolean2Decimal(chkFistPage.Checked), optType.SelectedValue, sFile, Boolean2Decimal(chkStatus.Checked), sContent, OrderNo)
        Else
            tmp = ctlNews.News_Add(txtTitle.Text, Boolean2Decimal(chkFistPage.Checked), optType.SelectedValue, sFile, Boolean2Decimal(chkStatus.Checked), sContent, OrderNo)
        End If

        If tmp Then
            Try
                If optType.SelectedValue = "UPL" Then
                    UploadFile(FileUpload)
                End If
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย แต่ไม่สามารถ upload ได้');", True)
            End Try


            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
        End If

        ClearData()

    End Sub
    Private Sub ClearData()
        lblID.Text = "0"
        txtTitle.Text = ""
        txtURL.Text = ""
        chkFistPage.Checked = False
        chkStatus.Checked = True
        xHtmlDetail.Html = ""
    End Sub
    Sub UploadFile(ByVal Fileupload As Object)
        Dim FileFullName As String = Fileupload.PostedFile.FileName
        Dim FileNameInfo As String = Path.GetFileName(FileFullName)
        Dim filename As String = Path.GetFileName(Fileupload.PostedFile.FileName)
        Dim objfile As FileInfo = New FileInfo(Server.MapPath(tmpUpload))
        If FileNameInfo <> "" Then
            'Fileupload.PostedFile.SaveAs(objfile.DirectoryName & "\" & filename)
            'sFile = Path.GetExtension(FileFullName)
            Fileupload.PostedFile.SaveAs(objfile.DirectoryName & "\" & tmpUpload & "\" & sFile)
        End If
        objfile = Nothing
    End Sub


    Protected Sub optType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optType.SelectedIndexChanged
        ShowNewsType()
    End Sub
    Private Sub ShowNewsType()
        pnURL.Visible = False
        pnFile.Visible = False
        pnContent.Visible = False
        Select Case optType.SelectedValue
            Case "URL"
                pnURL.Visible = True
            Case "UPL"
                pnFile.Visible = True
            Case "CON"
                pnContent.Visible = True

        End Select
    End Sub

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        ClearData()
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveNews()
    End Sub
End Class