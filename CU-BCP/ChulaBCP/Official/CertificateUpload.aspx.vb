Imports System.IO
Public Class CertificateUpload
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlStd As New StudentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            LoadStudentData(Request.Cookies("PersonID").Value)
            LoadDocument()
            LoadCertificateDocument()
        End If

        'Dim scriptString As String = "javascript:return confirm(""ต้องการลบการลงทะเบียนนี้ ใช่หรือไม่?"");"
        'cmdDelete.Attributes.Add("onClick", scriptString)

        'txtGPAX.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
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

    Private Sub LoadDocument()
        Dim ctlSj As New ReferenceValueController
        dt = ctlSj.ReferenceValue_GetByDomainCode("CERT")
        ddlDocName.DataSource = dt
        ddlDocName.DataValueField = "Code"
        ddlDocName.DataTextField = "Descriptions"
        ddlDocName.DataBind()
        dt = Nothing
    End Sub

    Protected Sub cmdUpload_Click(sender As Object, e As EventArgs) Handles cmdUpload.Click
        Dim folder As String = Server.MapPath("~/" + DocumentPath + "certificate/")
        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If

        Dim AttachFileName As String
        Dim UlFileName As String = ""
        AttachFileName = ""

        If FileUploadFile.HasFile Then
            UlFileName = lblStdCode.Text + "_" + ddlDocName.SelectedValue.ToString() + Path.GetExtension(FileUploadFile.PostedFile.FileName)
            AttachFileName = Path.GetFileName(UlFileName)
            FileUploadFile.SaveAs(Server.MapPath("~/" + DocumentPath + "certificate/" + UlFileName))

            ctlStd.Certificate_Add(StrNull2Zero(hdStudentID.Value), ddlDocName.SelectedValue, ddlDocName.SelectedItem.Text, AttachFileName, Request.Cookies("UserLoginID").Value)

            LoadCertificateDocument()
        End If
    End Sub
    Private Sub LoadCertificateDocument()
        dt = ctlStd.Certificate_GetByStudent(StrNull2Zero(hdStudentID.Value))
        grdDocument.DataSource = dt
        grdDocument.DataBind()
    End Sub

    Protected Sub grdDocument_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdDocument.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel_Doc"
                    If ctlStd.certificate_Delete(e.CommandArgument, StrNull2Zero(hdStudentID.Value)) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadCertificateDocument()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If
            End Select
        End If
    End Sub

    Private Sub grdDocument_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdDocument.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบข้อมูลนี้ ?"");"
            Dim imgD As ImageButton = e.Row.Cells(2).FindControl("imgDel_Doc")
            imgD.Attributes.Add("onClick", scriptString)
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#d9edf7';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub
End Class