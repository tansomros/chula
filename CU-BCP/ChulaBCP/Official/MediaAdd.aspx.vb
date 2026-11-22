Imports System.IO
Public Class MediaAdd
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlD As New DownloadController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("../Login.aspx?logout=y")
        End If
        If Not IsPostBack Then
            ClearData()
            LoadMajor()
            ddlMajor.SelectedIndex = 0

            If Not Request("id") = Nothing Then
                LoadDataEdit(Request("id"))
            End If

        End If
    End Sub

    Private Sub LoadDataEdit(ByVal tid As Integer)
        dt = ctlD.Media_GetByUID(tid)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.lblUID.Text = DBNull2Str(dt.Rows(0)("UID"))
                txtSubjectCode.Text = DBNull2Str(dt.Rows(0)("SubjectCode"))
                txtSubjectName.Text = DBNull2Str(dt.Rows(0)("SubjectName"))
                txtInstructor.Text = DBNull2Str(dt.Rows(0)("Instructor"))
                txtDescriptions.Text = DBNull2Str(dt.Rows(0)("Descriptions"))
                txtLink.Text = DBNull2Str(dt.Rows(0)("FilePath"))
                ddlMajor.SelectedValue = DBNull2Str(dt.Rows(0)("MajorUID"))

                hlnkDoc.Text = "คลิกดูสื่อการสอน"
                hlnkDoc.NavigateUrl = DBNull2Str(dt.Rows(0)("filePath"))
                hlnkDoc.Visible = True
            End With
        End If
        dt = Nothing
    End Sub


    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtDescriptions.Text.Trim = "" Then
            DisplayMessage(Me.Page, "ท่านยังไม่ได้ระบุรายละเอียด")
            Exit Sub
        End If

        If lblUID.Text = "" Then
            ctlD.Media_Add(ddlMajor.SelectedValue, txtSubjectCode.Text, txtSubjectName.Text, txtDescriptions.Text, txtInstructor.Text, txtLink.Text, Session("userid"))
        Else
            ctlD.Media_Update(StrNull2Zero(lblUID.Text), ddlMajor.SelectedValue, txtSubjectCode.Text, txtSubjectName.Text, txtDescriptions.Text, txtInstructor.Text, txtLink.Text, Session("userid"))
        End If

        DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")

        Response.Redirect("MediaList.aspx")
    End Sub
    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearData()
    End Sub
    Private Sub ClearData()
        lblUID.Text = ""
        txtDescriptions.Text = ""
    End Sub

    Private Sub LoadMajor()
        Dim dtG As New DataTable
        Dim ctlM As New MajorController

        dtG = ctlM.Major_Get()
        ddlMajor.DataSource = dtG
        ddlMajor.DataTextField = "NameTH"
        ddlMajor.DataValueField = "UID"
        ddlMajor.DataBind()
        ddlMajor.SelectedIndex = 0
        dtG = Nothing
    End Sub

End Class