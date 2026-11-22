Imports System.Drawing
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Internal
Public Class SubjectModify
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlS As New SubjectController
    Dim ctlM As New MasterController
    Dim ctlU As New UserController
    Private Const UploadDirectory As String = "~/imgSubject/"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            cmdDelete.Visible = False
            If Not Request("cid") Is Nothing Then
                LoadSubjectData()
            End If

            'If Request.Cookies("ROLE_ADM").Value = False And Request.Cookies("ROLE_SPA").Value = False Then
            '    ddlOrganize.Enabled = False
            'Else
            '    ddlOrganize.Enabled = True
            'End If

        End If

        cmdDelete.Attributes.Add("onClick", "javascript:return confirm(""ต้องการลบข้อมูลนี้ใช่หรือไม่?"");")

    End Sub
    'Private Sub LoadLawMaster()
    '    ddlLawMaster.DataSource = ctlS.LawMaster_GetActive()
    '    ddlLawMaster.DataTextField = "Name"
    '    ddlLawMaster.DataValueField = "UID"
    '    ddlLawMaster.DataBind()
    'End Sub

    'Private Sub LoadKeyword()
    '    chkKeyword.DataSource = ctlM.Keyword_Get()
    '    chkKeyword.DataTextField = "Name"
    '    chkKeyword.DataValueField = "UID"
    '    chkKeyword.DataBind()
    'End Sub

    Private Sub LoadSubjectData()
        dt = ctlS.Subject_GetByUID(Request("cid"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdSubjectUID.Value = String.Concat(.Item("SubjectID"))
                txtCode.Text = String.Concat(.Item("SubjectCode"))
                txtNameTH.Text = String.Concat(.Item("NameTH"))
                txtNameEN.Text = String.Concat(.Item("NameEN"))
                txtUnit.Text = String.Concat(.Item("SubjectUnit"))
                ddlType.SelectedValue = String.Concat(.Item("SubjectTypeCode"))
                chkStatus.Checked = ConvertStatusFlag2CHK(String.Concat(.Item("StatusFlag")))

                If DBNull2Str(dt.Rows(0)("filePath")) <> "" Then
                    hlnkDoc.Text = DBNull2Str(dt.Rows(0)("filePath"))
                    hlnkDoc.NavigateUrl = "../" & DocumentSubject & DBNull2Str(dt.Rows(0)("filePath"))
                    hlnkDoc.Visible = True
                Else
                    hlnkDoc.Visible = False
                End If

                cmdDelete.Visible = True
            End With
        Else
        End If
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtCode.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ท่านยังไม่ได้ระบุรหัสวิชา');", True)
            Exit Sub
        End If
        If txtNameTH.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ท่านยังไม่ได้ระบุชื่อรายวิชาภาษาไทย');", True)
            Exit Sub
        End If

        'If hlnkDoc.Text = "" Then
        '    If FileUpload1.PostedFile.FileName = "" Then
        '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ท่านยังไม่ได้เลือกไฟล์สำหรับอัพโหลด');", True)
        '        Exit Sub
        '    End If
        'End If
        Dim FileName As String
        FileName = ""
        If FileUpload1.HasFile Then
            FileName = txtCode.Text & ".pdf"
        End If
        ctlS.Subject_Save(StrNull2Zero(hdSubjectUID.Value), txtCode.Text, txtNameTH.Text, txtNameEN.Text, StrNull2Double(txtUnit.Text), ddlType.SelectedValue, ConvertBoolean2StatusFlag(chkStatus.Checked), FileName, Request.Cookies("UserLoginID").Value)

        'Dim SubjectID As Integer

        'If StrNull2Zero(hdSubjectUID.Value) = 0 Then
        '    SubjectID = ctlS.Subject_GetUID(txtCode.Text)
        'Else
        '    SubjectID = hdSubjectUID.Value
        'End If

        Dim f_Path As String 'f_Extension,f_Icon,, cate 
        'f_Icon = ""
        'f_Extension = ""
        f_Path = ""
        'cate = ""

        If FileUpload1.HasFile Then
            f_Path = txtCode.Text & Path.GetExtension(FileUpload1.PostedFile.FileName)
            'ctlS.Subject_UpdateFile(SubjectID, f_Path, Request.Cookies("UserLoginID").Value)
            UploadFile(FileUpload1, f_Path)

        End If

        ctlU.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_UPD, "Subject", "บันทึก/แก้ไข ประมวลรายวิชา :{SubjectID=" & hdSubjectUID.Value & "}{SubjectCode=" & txtCode.Text & "}", "")

        If Not hdSubjectUID.Value = Nothing Then
            cmdSave.Visible = True
            cmdDelete.Visible = True
        Else
            cmdSave.Visible = False
            cmdDelete.Visible = False
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)

        'Response.Redirect("Subject.aspx?ActionType=tsk")
    End Sub

    Sub UploadFile(ByVal Fileupload As Object, sName As String)
        'Dim FileFullName As String = Fileupload.PostedFile.FileName
        'Dim FileNameInfo As String = Path.GetFileName(FileFullName)
        Dim filename As String = Path.GetFileName(Fileupload.PostedFile.FileName)
        Dim objfile As FileInfo = New FileInfo(Server.MapPath("../" & DocumentSubject & "/" & sName))
        If objfile.Exists Then
            objfile.Delete()
        End If

        'If FileNameInfo <> "" Then
        Fileupload.PostedFile.SaveAs(Server.MapPath("../" & DocumentSubject & "/" & sName))
        'End If
        'objfile = Nothing
    End Sub
    Protected Function SavePostedFile(ByVal uploadedFile As UploadedFile, ByVal UploadPath As String, ByVal UploadFileName As String) As String
        If (Not uploadedFile.IsValid) Then
            Return String.Empty
        End If

        Dim fileName As String = Path.ChangeExtension(UploadFileName, ".jpg")

        Dim fullFileName As String = CombinePath(UploadPath, fileName)
        Using original As Image = Image.FromStream(uploadedFile.FileContent)
            Using thumbnail As Image = New ImageThumbnailCreator(original).CreateImageThumbnail(New Size(350, 350))
                ImageUtils.SaveToJpeg(CType(thumbnail, Bitmap), fullFileName)
            End Using
        End Using
        UploadingUtils.RemoveFileWithDelay(fileName, fullFileName, 5)
        Return fileName
    End Function
    Protected Function CombinePath(ByVal UploadPath As String, ByVal fileName As String) As String
        Return Path.Combine(Server.MapPath(UploadPath), fileName)
    End Function

    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        'ctlE.Subject_Delete(StrNull2Zero(hdSubjectUID.Value))
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ลบ Subject เรียบร้อย');", True)
    End Sub

End Class