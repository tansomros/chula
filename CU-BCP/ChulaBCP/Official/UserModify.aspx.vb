Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail.MailMessage
Imports System.Net.Dns
Imports System.Net.Sockets.AddressFamily
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Internal
Public Class UserModify
    Inherits System.Web.UI.Page
    Dim IsFlag As Boolean = False
    Dim ctlU As New UserController
    Dim dtU As New DataTable
    Private Const UploadDirectory As String = "~/images/users/"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            ddlPerson.Enabled = True
            ClearData()
            LoadPerson()
            LoadRole()
            If Not Request("uid") = Nothing Then
                EditData(StrNull2Zero(Request("uid")))
            End If
        End If
    End Sub
    Dim enc As New CryptographyEngine
    Private Sub EditData(ByVal UserID As Integer)
        dtU = ctlU.User_GetByID(UserID)
        If dtU.Rows.Count > 0 Then
            lblUserID.Text = dtU.Rows(0)("UserID")
            txtUsername.Text = dtU.Rows(0)("Username")
            txtName.Text = dtU.Rows(0)("Name")
            Username_Old.Value = txtUsername.Text
            txtPassword.Text = enc.DecryptString(dtU.Rows(0)("Password"), True)
            ddlUserGroup.SelectedValue = String.Concat(dtU.Rows(0)("UserGroupUID"))
            optRole.SelectedValue = dtU.Rows(0)("RoleID")

            chkStatus.Checked = ConvertStatusFlag2Boolean(String.Concat(dtU.Rows(0)("StatusFlag")))

            If DBNull2Str(dtU.Rows(0).Item("ImagePath")) <> "" Then
                Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & UserPic & "/" & dtU.Rows(0).Item("ImagePath")))

                If objfile.Exists Then
                    Session("userimg") = dtU.Rows(0).Item("ImagePath")
                    'uploadedImage.ImageUrl = "~/" & userpic & "/" & dtU.Rows(0).Item("ImagePath")
                Else
                    Session("userimg") = "user_blank.jpg"
                    'uploadedImage.ImageUrl = "~/" & userpic & "/user_blank.jpg"
                End If
            Else
                Session("userimg") = "user_blank.jpg"
            End If

            LoadPerson()
            ddlPerson.SelectedValue = DBNull2Zero(dtU.Rows(0)("PersonID"))
            'If ddlUserGroup.SelectedValue = 1 Then
            '    ddlPerson.Enabled = False
            'Else
            '    ddlPerson.Enabled = True
            'End If
        End If

    End Sub

    Private Sub SendPassword(UserID As Integer)

        Dim dt As New DataTable
        dt = ctlU.User_GetByID(UserID)

        If dt.Rows.Count > 0 Then
            Dim _pwd As String = "$utmed111"
            Dim _mail As String = "sutmed111@gmail.com"


            Dim user_email As String = Convert.ToString(dt.Rows(0).Item("Email"))
            Dim c As New System.Net.NetworkCredential
            c.Password = _pwd
            c.UserName = _mail

            Dim m As MailMessage = New MailMessage
            Dim Body As String = ""

            Body &= "<h3><b>เรียน " & Convert.ToString(dt.Rows(0).Item("Name")) & "</b></h3>"
            Body &= "<br/> ระบบขอแจ้งชื่อเข้าใช้และรหัสผ่าน ของท่าน ดังนี้ "
            Body &= "<br/> ชื่อเข้าใช้/email:  " & dt.Rows(0).Item("Username")
            Body &= "<br/> รหัสผ่าน: " & Convert.ToString(dt.Rows(0).Item("Password"))

            Body &= "<br/><br/>"
            Body &= "สามารถเข้าใช้งานได้โดย <a href='http://im.sut.ac.th/login.aspx' target=_blank>คลิกที่นี่...</a>"


            Body &= "<br/><br/><br/><i>ข้อความนี้ถูกส่งเมื่อ :" & Now.ToString("dd-MMM-yyyy HH:mm:ss") & "</i>"
            Body &= "<hr>"
            Body &= "----<i>ข้อความนี้ถูกสร้างอัตโนมัติ ไม่ต้องตอบกลับ</i> ---- "
            Body &= "<hr>"
            Body &= "สำนักวิชาแพทยศาสตร์ มหาวิทยาลัยเทคโนโลยีสุรนารี  111 ถ.มหาวิทยาลัย ต.สุรนารี อ.เมือง จ.นครราชสีมา 30000 "
            Body &= "<br/>โทรศัพท์ 0-4422-3951,0-4422-3930"
            m.Body = Body
            m.IsBodyHtml = True
            m.To.Add(user_email)
            'If _mailBCC.Trim <> "" Then

            'End If

            m.From = New System.Net.Mail.MailAddress(_mail, "สำนักวิชาแพทยศาสตร์ มทส.")
            m.Subject = "แจ้งชื่อเข้าใช้และรหัสผ่าน สวพ."
            'm.Headers.Add("Reply-To", txtMailUser.Text)
            Dim s As New System.Net.Mail.SmtpClient
            s.UseDefaultCredentials = True
            s.Credentials = c
            s.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            s.EnableSsl = True
            s.Port = 587
            s.Host = "smtp.gmail.com"
            s.Send(m)

            'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('ส่ง e-mail เรียบร้อย!!')", True)
        End If

    End Sub

    Function CheckUsernameDup() As Boolean
        dtU = ctlU.User_GetByUsername(txtUsername.Text.Trim())
        If dtU.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim UserID As Integer
        If lblUserID.Text = "" Then
            If CheckUsernameDup() = True Then
                lblAlert.Visible = True
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Username ซ้ำ!!')", True)
                Exit Sub
            Else
                lblAlert.Visible = False
            End If

            ctlU.User_Add(txtName.Text, txtUsername.Text, enc.EncryptString(txtPassword.Text, True), StrNull2Zero(ddlUserGroup.SelectedValue), StrNull2Zero(optRole.SelectedValue), StrNull2Zero(ddlPerson.SelectedValue), ConvertBoolean2StatusFlag(chkStatus.Checked))

            UserID = ctlU.User_GetUserID(txtUsername.Text)

        Else
            If txtUsername.Text.Trim() <> Username_Old.Value.ToString() Then
                If CheckUsernameDup() = True Then
                    lblAlert.Visible = True
                    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Username ซ้ำ!!')", True)
                    Exit Sub
                Else
                    lblAlert.Visible = False
                End If
            End If

            UserID = StrNull2Zero(lblUserID.Text)
            ctlU.User_Update(StrNull2Zero(lblUserID.Text), txtName.Text, txtUsername.Text, enc.EncryptString(txtPassword.Text, True), StrNull2Zero(ddlUserGroup.SelectedValue), StrNull2Zero(optRole.SelectedValue), StrNull2Zero(ddlPerson.SelectedValue), ConvertBoolean2StatusFlag(chkStatus.Checked))


        End If



        If Not Session("userimg") Is Nothing Then
            ctlU.User_UpdateFileName(StrNull2Zero(lblUserID.Text), lblUserID.Text & Path.GetExtension(Session("userimg")))
            RenamePictureName()
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกเรียบร้อย');", True)
        'ClearData()
    End Sub
    Private Sub RenamePictureName()
        Dim Path As String = Server.MapPath(UploadDirectory)
        Dim Fromfile As String = Path + Session("userimg")
        Dim Tofile As String = Path + lblUserID.Text + ".jpg"

        Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & UserPic & "/" & lblUserID.Text + ".jpg"))

        If objfile.Exists Then
            objfile.Delete()
        End If

        File.Move(Fromfile, Tofile)

    End Sub
    Private Sub ClearData()
        lblUserID.Text = ""
        ddlPerson.SelectedIndex = 0
        txtUsername.Text = ""
        txtPassword.Text = ""
        lblAlert.Visible = False
        Username_Old.Value = ""
    End Sub

    Private Sub LoadPerson()
        Dim ctlP As New PersonController
        Dim ctlS As New StudentController
        Dim dtP As New DataTable
        ddlPerson.Items.Clear()
        Select Case ddlUserGroup.SelectedValue
            Case 1 'Staff
                dtP = ctlP.Person_GetStaff
            Case 2  'Student
                dtP = ctlS.Student_GetByStatus("10")
            Case 3 'Faculty
                dtP = ctlP.Person_GetTeacher()
            Case 4 'Alumni
                dtP = ctlS.Student_GetByStatus("40")
        End Select

        'dtP = ctlP.Person_Get4User(ddlUserGroup.SelectedValue)
        With ddlPerson
            .DataSource = dtP
            .DataTextField = "PersonName"
            .DataValueField = "PersonID"
            .DataBind()
        End With
        dtP = Nothing
    End Sub

    Private Sub LoadRole()
        Dim ctlR As New UserRoleController

        dtU = ctlR.UserRole_Get()
        With optRole
            .DataSource = dtU
            .DataValueField = "RoleID"
            .DataTextField = "RoleName"
            .DataBind()
            .SelectedIndex = 0
        End With
        dtU = Nothing
    End Sub

    Protected Sub ddlUserGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUserGroup.SelectedIndexChanged
        txtName.Text = ""
        txtPassword.Text = ""
        txtUsername.Text = ""
        LoadPerson()
        'If ddlUserGroup.SelectedValue = 1 Then
        '    ddlPerson.Enabled = False
        'Else
        '    ddlPerson.Enabled = True
        'End If
    End Sub

    Protected Sub ddlPerson_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPerson.SelectedIndexChanged
        If ddlUserGroup.SelectedValue = "2" Then
            Dim ctlP As New StudentController
            txtUsername.Text = ctlP.Student_GetCode(StrNull2Zero(ddlPerson.SelectedValue))
            txtName.Text = ddlPerson.SelectedItem.Text
            optRole.SelectedValue = 2
        Else
            Dim ctlP As New PersonController
            txtUsername.Text = ctlP.Person_GetCode(StrNull2Zero(ddlPerson.SelectedValue))
            txtName.Text = ddlPerson.SelectedItem.Text
            optRole.SelectedValue = 3
        End If
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearData()
    End Sub

    Protected Sub UploadControl_FileUploadComplete(ByVal sender As Object, ByVal e As FileUploadCompleteEventArgs)

        If chkFileExist(Server.MapPath(UploadDirectory + Session("userimg"))) Then
            File.Delete(Server.MapPath(UploadDirectory + Session("userimg")))
        End If

        e.CallbackData = SavePostedFile(e.UploadedFile, UploadDirectory, Path.GetRandomFileName())
        Session("userimg") = e.CallbackData
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
End Class