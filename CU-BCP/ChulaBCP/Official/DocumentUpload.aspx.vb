Imports System.Net
Imports System.IO
Public Class DocumentUpload
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlStd As New StudentController
    Dim ctlReg As New DocumentController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("UserLogin")) Then
            Response.Redirect("Login.aspx")
        End If
  
        If Not IsPostBack Then
            cmdDelete.Visible = False

            txtRegDate.Text = DisplayShortDateTH(ctlReg.GET_DATE_SERVER())

            LoadYearToDDL()
            LoadDocumentName()
            LoadStudentData(Request.Cookies("PersonID").Value)

            If Not Request("docid") = Nothing Then
                LoadRegisterData()
            End If
        End If

        'Dim scriptString As String = "javascript:return confirm(""ต้องการลบการลงทะเบียนนี้ ใช่หรือไม่?"");"
        'cmdDelete.Attributes.Add("onClick", scriptString)

        'txtGPAX.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlReg.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlReg.Document_GetYear
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
    Private Sub LoadStudentData(pid As Integer)
        dt = ctlStd.GetStudent_ByID(pid)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdStudentID.Value = String.Concat(.Item("StudentID"))
                lblStdCode.Text = .Item("StudentCode")
                lblStudentName.Text = DBNull2Str(.Item("StudentName"))
            End With
        End If
    End Sub
    Private Sub LoadRegisterData()
        dt = ctlReg.Document_GetByID(Request("docid"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdDocumentID.Value = String.Concat(.Item("UID"))
                ddlDocName.SelectedValue = .Item("DocumentUID")
                hdStudentID.Value = String.Concat(.Item("StudentID"))

                optTerm.SelectedValue = DBNull2Zero(.Item("TermNo"))
                ddlYear.SelectedValue = DBNull2Zero(.Item("EduYear"))
                txtRegDate.Text = DisplayShortDateTH(.Item("DocumentDate"))

                hlnkDocument.Text = DBNull2Str(dt.Rows(0)("filePath"))
                hlnkDocument.NavigateUrl = "../" & DocumentStudent & DBNull2Str(dt.Rows(0)("filePath"))
                hlnkDocument.Visible = True

                LoadStudentData(hdStudentID.Value)

                cmdSave.Text = "Save"
                cmdDelete.Visible = True
            End With
        End If
    End Sub

    Private Sub LoadDocumentName()
        Dim ctlSj As New ReferenceValueController
        dt = ctlSj.ReferenceValue_GetByDomainCode("DOCSEN")
        ddlDocName.DataSource = dt
        ddlDocName.DataValueField = "UID"
        ddlDocName.DataTextField = "Descriptions"
        ddlDocName.DataBind()
        dt = Nothing
    End Sub
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtRegDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุวันที่ลงทะเบียนก่อน');", True)
            Exit Sub
        End If
        If hlnkDocument.Text = "" Then
            If FileUploadFile.PostedFile.FileName = "" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ท่านยังไม่ได้เลือกไฟล์สำหรับอัพโหลด');", True)
                Exit Sub
            End If
        End If

        'pnSubject.Visible = True

        ''cmdPrint.Visible = True
        'Response.Redirect("ResultPage.aspx?t=reg&p=complete")
        Dim sDocPath As String
        sDocPath = ddlYear.SelectedValue & "/" & ctlReg.Document_GetFolderName(StrNull2Zero(ddlDocName.SelectedValue))

        Dim folder As String = Server.MapPath("~/" + DocumentStudent + sDocPath)
        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If

        Dim AttachFileName As String
        'Dim UlFileName As String = ""
        AttachFileName = ""
        Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" + DocumentStudent + sDocPath + "/" + lblStdCode.Text + Path.GetExtension(FileUploadFile.FileName)))

        If FileUploadFile.HasFile Then
            'UlFileName = FileUploadFile.FileName

            If objfile.Exists Then
                objfile.Delete()
            End If

            FileUploadFile.SaveAs(objfile.FullName)
            AttachFileName =  lblStdCode.Text + Path.GetExtension(FileUploadFile.FileName)
        End If

        ctlReg.Document_Save(StrNull2Zero(hdDocumentID.Value), StrNull2Zero(ddlDocName.SelectedValue), ConvertStrDate2DateQueryString(txtRegDate.Text), optTerm.SelectedValue, StrNull2Zero(ddlYear.SelectedValue), hdStudentID.Value, AttachFileName, Request.Cookies("UserLoginID").Value)

        'hdDocumentID.Value = ctlReg.Document_GetDocumentID(StrNull2Zero(optTerm.SelectedValue), StrNull2Zero(ddlYear.SelectedValue), hdStudentID.Value, ConvertStrDate2DateQueryString(txtRegDate.Text), "")
        Dim objuser As New UserController
        objuser.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_UPD, "Document", "บันทึก/แก้ไข การส่งงาน :" & lblStdCode.Text, "")

        'lineNotify("ส่งงาน : " & lblStudentName.Text & " ได้ทำการ Upload เอกสารส่งงาน  " & ddlDocName.SelectedItem.Text & " เข้ามาในระบบ" & vbCrLf & "")

        Response.Redirect("ResultPage.aspx?t=doc&p=complete")


    End Sub
    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Response.Redirect("ResultProcess.aspx?t=doc&p=del&docid=" & hdDocumentID.Value & "&pid=" & hdStudentID.Value & "&tid=" & ddlDocName.SelectedValue)
    End Sub
    Public Sub lineNotify(ByVal msg As String)
        Dim token As String = "GEETKyNRuV71ROCS3PziGY7L3DktgdZVUCUntng82Nv" ' "MwjfXz1Pb2EBCPe197a7Tu0qCgUjTlKqA2wc4cMQ6CZ" << Tansomros
        'Dim msg As String = "ทดสอบจ้าาา...."

        Try
            Dim request = CType(WebRequest.Create("https://notify-api.line.me/api/notify"), HttpWebRequest)
            Dim postData = String.Format("message={0}", msg)
            Dim data = Encoding.UTF8.GetBytes(postData)
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = data.Length
            request.Headers.Add("Authorization", "Bearer " & token)

            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            Dim response = CType(request.GetResponse(), HttpWebResponse)
            Dim responseString = New StreamReader(response.GetResponseStream()).ReadToEnd()
        Catch ex As Exception
            Console.Write(ex.ToString())
        End Try
    End Sub

End Class