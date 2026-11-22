Imports System.IO
Public Class Register
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlStd As New StudentController
    Dim ctlReg As New RegisterController
    Enum ProcessStep
        Begin
        Process
        Finish
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            cmdPrint.Visible = False
            cmdDelete.Visible = False
            pnSubject.Visible = False
            pnWorkSend.Visible = False

            txtRegDate.Text = DisplayShortDateTH(ctlReg.GET_DATE_SERVER())

            Session("CurrentStep") = ProcessStep.Begin
            LoadYearToDDL()
            LoadTopic()
            LoadStudentData(Request.Cookies("PersonID").Value)
            LoadSubject()
            LoadDocument()

            If Not Request("regid") = Nothing Then
                LoadRegisterData()
            End If
        End If
        cmdPrint.Attributes.Add("onClick", "window.open('" + ResolveUrl("ReportViewerStudentBio.aspx?id=" + lblStdCode.Text) + "', 'windowname', 'width=800,height=600,scrollbars=yes')")

        'Dim scriptString As String = "javascript:return confirm(""ต้องการลบการลงทะเบียนนี้ ใช่หรือไม่?"");"
        'cmdDelete.Attributes.Add("onClick", scriptString)

        'txtGPAX.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
    End Sub
    Private Sub LoadYearToDDL()
        Dim ctlM As New MasterController
        dt = ctlM.GetEduYear
        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "EduYear"
                .DataValueField = "EduYear"
                .DataBind()
            End With
        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        dt = Nothing
    End Sub

    'Private Sub LoadYearToDDL()
    '    Dim y As Integer = StrNull2Zero(DisplayYear(ctlReg.GET_DATE_SERVER))
    '    Dim LastRow As Integer
    '    dt = ctlReg.Register_GetYear
    '    LastRow = dt.Rows.Count - 1

    '    If dt.Rows.Count > 0 Then
    '        With ddlYear
    '            .Enabled = True
    '            .DataSource = dt
    '            .DataTextField = "EduYear"
    '            .DataValueField = "EduYear"
    '            .DataBind()

    '            If dt.Rows(LastRow)(0) = y Then
    '                ddlYear.Items.Add(y + 1)
    '                ddlYear.Items(LastRow + 1).Value = y + 1
    '            ElseIf dt.Rows(LastRow)(0) > y Then
    '                'ddlYear.Items.Add(y + 2)
    '                'ddlYear.Items(LastRow + 1).Value = y + 2
    '            ElseIf dt.Rows(LastRow)(0) < y Then
    '                ddlYear.Items.Add(y)
    '                ddlYear.Items(LastRow + 1).Value = y
    '                ddlYear.Items.Add(y + 1)
    '                ddlYear.Items(LastRow + 2).Value = y + 1
    '            End If
    '            .SelectedIndex = 0
    '        End With
    '    Else
    '        ddlYear.Items.Add(y)
    '        ddlYear.Items(0).Value = y
    '        ddlYear.Items.Add(y + 1)
    '        ddlYear.Items(1).Value = y + 1
    '        ddlYear.SelectedIndex = 0
    '    End If
    '    ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
    '    dt = Nothing
    'End Sub
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
        dt = ctlReg.Register_GetByID(Request("regid"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdRegisterID.Value = String.Concat(.Item("RegisterID"))
                hdStudentID.Value = String.Concat(.Item("StudentID"))

                optTerm.SelectedValue = DBNull2Zero(.Item("TermNo"))
                ddlYear.SelectedValue = DBNull2Zero(.Item("EduYear"))
                ddlTopicID.SelectedValue = DBNull2Zero(.Item("TopicUID"))
                txtRegDate.Text = DisplayShortDateTH(.Item("RegisterDate"))

                LoadStudentData(hdStudentID.Value)
                LoadRegisterSubject()
                LoadRegisterDocument()
                Session("CurrentStep") = ProcessStep.Finish
                pnSubject.Visible = False
                Select Case ddlTopicID.SelectedValue
                    Case 1
                        pnSubject.Visible = True
                End Select
                pnWorkSend.Visible = True

                cmdSave.Text = "Save"
                cmdDelete.Visible = True
            End With
        End If
    End Sub
    Private Sub LoadTopic()
        dt = ctlReg.RegisterTopic_Get
        ddlTopicID.DataSource = dt
        ddlTopicID.DataValueField = "UID"
        ddlTopicID.DataTextField = "TopicName"
        ddlTopicID.DataBind()
        dt = Nothing
    End Sub

    Private Sub LoadSubject()
        Dim ctlSj As New SubjectController
        dt = ctlSj.Subject_GetActive
        ddlSubject.DataSource = dt
        ddlSubject.DataValueField = "SubjectID"
        ddlSubject.DataTextField = "SubjectName"
        ddlSubject.DataBind()
        dt = Nothing
    End Sub


    Private Sub LoadDocument()
        Dim ctlSj As New ReferenceValueController
        dt = ctlSj.ReferenceValue_GetByDomainCode("DOCSEN")
        ddlDocName.DataSource = dt
        ddlDocName.DataValueField = "Code"
        ddlDocName.DataTextField = "Descriptions"
        ddlDocName.DataBind()
        dt = Nothing
    End Sub
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtRegDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุวันที่ลงทะเบียนก่อน');", True)
            Exit Sub
        End If

        'If txtHighYear.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุปีที่จบการศึกษาระดับมัธยมตอนปลายก่อน');", True)
        '    Exit Sub
        'End If

        If Session("CurrentStep") = ProcessStep.Begin Then

            ctlReg.Register_Save(StrNull2Zero(hdRegisterID.Value), StrNull2Zero(optTerm.SelectedValue), StrNull2Zero(ddlYear.SelectedValue), hdStudentID.Value, ConvertStrDate2DateQueryString(txtRegDate.Text), ddlTopicID.SelectedValue, Request.Cookies("UserLoginID").Value)

            hdRegisterID.Value = ctlReg.Register_GetRegisterID(StrNull2Zero(optTerm.SelectedValue), StrNull2Zero(ddlYear.SelectedValue), hdStudentID.Value, ConvertStrDate2DateQueryString(txtRegDate.Text), ddlTopicID.SelectedValue)

            LoadRegisterSubject()
            'If ddlTopicID.SelectedValue = 1 Then
            '    pnSubject.Visible = True
            'End If

            pnWorkSend.Visible = False
            Session("CurrentStep") = ProcessStep.Finish
            cmdSave.Text = "Confirm"
        ElseIf Session("CurrentStep") = ProcessStep.Process Then
            If ddlTopicID.SelectedValue = 1 Then
                pnSubject.Visible = True
            End If
        ElseIf Session("CurrentStep") = ProcessStep.Finish Then
            If grdSubject.Rows.Count <= 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาเพิ่มรายวิชาที่ลงทะเบียนก่อน');", True)
                Exit Sub
            End If

            'pnSubject.Visible = True
            Dim objuser As New UserController
            objuser.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_UPD, "Student", "บันทึก/แก้ไข บัตรลงทะเบียน :" & lblStdCode.Text, "")
            'cmdPrint.Visible = True
            Response.Redirect("ResultPage.aspx?t=reg&p=complete")
        End If
    End Sub

    Private Sub LoadRegisterSubject()
        pnSubject.Visible = True

        If StrNull2Zero(hdRegisterID.Value) <> 0 Then
            Select Case ddlTopicID.SelectedValue
                Case 1 'ลงทะเบียนปก ปี 1 
                    If ctlReg.RegisterSubject_GetCount(StrNull2Zero(hdRegisterID.Value)) <= 0 Then
                        ctlReg.RegisterSubjectTemplate_Clone(StrNull2Zero(hdRegisterID.Value), StrNull2Zero(ddlTopicID.SelectedValue), Request.Cookies("UserLoginID").Value)
                    End If

                Case 2
                Case 3 'ลงทะเบียนรักษาสภาพ
            End Select

        End If


        dt = ctlReg.RegisterSubject_Get(StrNull2Zero(hdRegisterID.Value))
        grdSubject.DataSource = dt
        grdSubject.DataBind()

        Dim Unit As Integer = 0
        For i = 0 To dt.Rows.Count - 1
            Unit = Unit + dt.Rows(i)("SubjectUnit")
        Next
        lblTotalUnit.Text = Unit
    End Sub

    Protected Sub cmdAddSubject_Click(sender As Object, e As EventArgs) Handles cmdAddSubject.Click
        If ddlSubject.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาเลือกรายวิชาก่อน');", True)
            Exit Sub
        End If

        ctlReg.RegisterSubject_Add(StrNull2Zero(hdRegisterID.Value), StrNull2Zero(ddlSubject.SelectedValue), Request.Cookies("UserLoginID").Value)

        LoadRegisterSubject()
    End Sub

    Protected Sub grdSubject_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdSubject.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    If ctlReg.RegisterSubject_Delete(e.CommandArgument, StrNull2Zero(hdRegisterID.Value)) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadRegisterSubject()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If
            End Select
        End If
    End Sub

    Private Sub grdSubject_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdSubject.RowDataBound

        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As ImageButton = e.Row.Cells(5).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#d9edf7';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub

    Protected Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click

    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Response.Redirect("ResultProcess.aspx?t=reg&p=del&regid=" & hdRegisterID.Value & "&pid=" & hdStudentID.Value & "&tid=" & ddlTopicID.SelectedValue)
    End Sub

    Protected Sub cmdUpload_Click(sender As Object, e As EventArgs) Handles cmdUpload.Click
        Dim folder As String = Server.MapPath("~/" + DocumentStudent + lblStdCode.Text)
        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If

        Dim AttachFileName As String
        Dim UlFileName As String = ""
        AttachFileName = ""

        If FileUploadFile.HasFile Then
            UlFileName = FileUploadFile.FileName
            AttachFileName = lblStdCode.Text + "/" + Path.GetFileName(UlFileName)
            FileUploadFile.SaveAs(Server.MapPath("~/" + DocumentStudent + lblStdCode.Text + "/" + UlFileName))

            ctlReg.RegisterDocument_Add(StrNull2Zero(hdRegisterID.Value), ddlDocName.SelectedValue, ddlDocName.SelectedItem.Text, AttachFileName, Request.Cookies("UserLoginID").Value)

            LoadRegisterDocument()
        End If
    End Sub
    Private Sub LoadRegisterDocument()
        dt = ctlReg.RegisterDocument_Get(StrNull2Zero(hdRegisterID.Value))
        grdDocument.DataSource = dt
        grdDocument.DataBind()
    End Sub

    Protected Sub ddlTopicID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTopicID.SelectedIndexChanged
        pnSubject.Visible = False
        If Session("CurrentStep") = ProcessStep.Finish Then
            Select Case ddlTopicID.SelectedValue
                Case 1
                    pnSubject.Visible = True
            End Select
        End If

    End Sub

    Protected Sub grdDocument_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdDocument.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel_Doc"
                    If ctlReg.RegisterDocument_Delete(e.CommandArgument, StrNull2Zero(hdRegisterID.Value)) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadRegisterDocument()
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