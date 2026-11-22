Imports System.Net
Imports System.IO
Public Class Request
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlStd As New StudentController
    Dim ctlReq As New RequestController
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
            pnCommittee.Visible = False
            pnWorkSend.Visible = False

            txtReqDate.Text = DisplayShortDateTH(ctlReq.GET_DATE_SERVER())

            Session("CurrentStep") = ProcessStep.Begin
            LoadProvinceToDDL()
            'LoadTopic()
            LoadAdvisor()
            LoadPosition()

            LoadStudentData(Request.Cookies("PersonID").Value)
            'LoadDocument()
            If Not Request("tid") = Nothing Then
                If Request("tid") >= 7 Then
                    pnThesis.Visible = True
                Else
                    pnThesis.Visible = False
                End If

                lblTopic.Text = ctlReg.RegisterTopic_GetName(Request("tid"))
                hdTopicUID.Value = Request("tid")

            End If
            If Not Request("regid") = Nothing Then
                LoadRequestData()
            End If
        End If
        cmdPrint.Attributes.Add("onClick", "window.open('" + ResolveUrl("ReportViewerStudentBio.aspx?id=" + lblStdCode.Text) + "', 'windowname', 'width=800,height=600,scrollbars=yes')")

        'Dim scriptString As String = "javascript:return confirm(""ต้องการลบการลงทะเบียนนี้ ใช่หรือไม่?"");"
        'cmdDelete.Attributes.Add("onClick", scriptString)
        txtZipcode.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")

    End Sub
    Private Sub LoadProvinceToDDL()
        Dim ctlM As New MasterController
        dt = ctlM.Province_GetActive
        If dt.Rows.Count > 0 Then
            With ddlProvince
                .Enabled = True
                .DataSource = dt
                .DataTextField = "ProvinceName"
                .DataValueField = "ProvinceID"
                .DataBind()
                '.SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub LoadStudentData(pid As Integer)
        dt = ctlStd.GetStudent_ByID(pid)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdStudentID.Value = String.Concat(.Item("StudentID"))
                lblStdCode.Text = .Item("StudentCode")
                lblStudentName.Text = DBNull2Str(.Item("StudentName"))
                lblMajor.Text = String.Concat(.Item("MajorName"))
            End With
        End If
    End Sub
    Private Sub LoadRequestData()
        dt = ctlReq.Request_GetByID(Request("regid"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdRequestID.Value = String.Concat(.Item("RegisterID"))
                hdStudentID.Value = String.Concat(.Item("StudentID"))
                txtReqDate.Text = DisplayShortDateTH(.Item("RegisterDate"))
                hdTopicUID.Value = DBNull2Zero(.Item("TopicUID"))

                txtThesisName.Text = String.Concat(.Item("ThesisName"))
                If String.Concat(.Item("AdmitDate")) <> "" Then
                    txtAdmitDate.Text = DisplayShortDateTH(.Item("AdmitDate"))
                End If

                If String.Concat(.Item("AdmitTimeStart")) <> "" Then
                    ddlStartTimeHour.SelectedValue = Left(String.Concat(.Item("AdmitTimeStart")), 2)
                    ddlStartTimeMinute.SelectedValue = Right(String.Concat(.Item("AdmitTimeStart")), 2)
                End If
                If String.Concat(.Item("AdmitTimeEnd")) <> "" Then
                    ddlEndTimeHour.SelectedValue = Left(String.Concat(.Item("AdmitTimeEnd")), 2)
                    ddlEndTimeMinute.SelectedValue = Right(String.Concat(.Item("AdmitTimeEnd")), 2)
                End If

                txtRoomNo.Text = String.Concat(.Item("RoomNo"))
                txtDepartment.Text = String.Concat(.Item("DepartmentName"))
                txtFloor.Text = String.Concat(.Item("FloorNo"))
                txtBuilding.Text = String.Concat(.Item("Building"))
                txtLocationName.Text = String.Concat(.Item("LocationName"))
                txtRoad.Text = String.Concat(.Item("Road"))
                txtSubdistrict.Text = String.Concat(.Item("Subdistrict"))
                txtDistrict.Text = String.Concat(.Item("District"))
                ddlProvince.SelectedValue = String.Concat(.Item("ProvinceID"))
                txtZipcode.Text = String.Concat(.Item("Zipcode"))

                LoadStudentData(hdStudentID.Value)
                LoadRequestCommittee()
                LoadRequestDocument()
                Session("CurrentStep") = ProcessStep.Finish

                pnCommittee.Visible = True

                'pnWorkSend.Visible = True

                cmdSave.Text = "Save"
                cmdDelete.Visible = True
            End With
        End If
    End Sub
    'Private Sub LoadTopic()
    '    dt = ctlReq.RequestTopic_Get
    '    ddlTopicID.DataSource = dt
    '    ddlTopicID.DataValueField = "UID"
    '    ddlTopicID.DataTextField = "TopicName"
    '    ddlTopicID.DataBind()
    '    dt = Nothing
    'End Sub
    Private Sub LoadAdvisor()
        Dim ctlP As New PersonController
        Dim dtP As New DataTable
        dtP = ctlP.Teacher_Get()
        ddlAdvisor.DataSource = dtP
        ddlAdvisor.DataValueField = "PersonID"
        ddlAdvisor.DataTextField = "PersonFullName_TH"
        ddlAdvisor.DataBind()
        dt = Nothing
    End Sub


    Private Sub LoadPosition()
        Dim ctlRf As New ReferenceValueController
        dt = ctlRf.ReferenceValue_GetByDomainCode("CMMPOS")
        ddlPosition1.DataSource = dt
        ddlPosition1.DataValueField = "UID"
        ddlPosition1.DataTextField = "Descriptions"
        ddlPosition1.DataBind()

        ddlPosition2.DataSource = dt
        ddlPosition2.DataValueField = "UID"
        ddlPosition2.DataTextField = "Descriptions"
        ddlPosition2.DataBind()

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtAdmitDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุวันที่เข้าสอบก่อน');", True)
            Exit Sub
        End If
        If txtAdmitDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุวันที่เข้าสอบก่อน');", True)
            Exit Sub
        End If

        'If txtHighYear.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุปีที่จบการศึกษาระดับมัธยมตอนปลายก่อน');", True)
        '    Exit Sub
        'End If
        ctlReq.Request_Save(StrNull2Zero(hdRequestID.Value), ConvertStrDate2DateQueryString(txtReqDate.Text), hdStudentID.Value, StrNull2Zero(hdTopicUID.Value), txtThesisName.Text, ConvertStrDate2DateQueryString(txtAdmitDate.Text), ddlStartTimeHour.SelectedValue & ddlStartTimeMinute.SelectedValue, ddlEndTimeHour.SelectedValue & ddlEndTimeMinute.SelectedValue, txtRoomNo.Text, txtDepartment.Text, txtFloor.Text, txtBuilding.Text, txtLocationName.Text, txtRoad.Text, txtSubdistrict.Text, txtDistrict.Text, ddlProvince.SelectedValue, txtZipcode.Text, Request.Cookies("UserLoginID").Value)

        If Session("CurrentStep") = ProcessStep.Begin Then

            hdRequestID.Value = ctlReq.Request_GetRequestID(hdStudentID.Value, ConvertStrDate2DateQueryString(txtReqDate.Text), hdTopicUID.Value)
            pnCommittee.Visible = True
            'pnWorkSend.Visible = True
            Session("CurrentStep") = ProcessStep.Finish
            cmdSave.Text = "Save"
        ElseIf Session("CurrentStep") = ProcessStep.Process Then
            pnCommittee.Visible = True
        ElseIf Session("CurrentStep") = ProcessStep.Finish Then
            If grdCommittee.Rows.Count <= 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาเพิ่มชื่อกรรมการควบคุมสอบก่อน');", True)
                Exit Sub
            End If

            'lineNotify("ยื่นคำร้อง : " & lblStudentName.Text & " ได้ทำการ " & ddlTopicID.SelectedItem.Text & "เข้ามาในระบบ" & vbCrLf & "")

            'pnCommittee.Visible = True
            Dim objuser As New UserController
            objuser.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_UPD, "Request", "บันทึก/แก้ไข คำร้อง :" & lblStdCode.Text, "")
            'cmdPrint.Visible = True
            Response.Redirect("ResultPage.aspx?t=req&p=complete")
        End If
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

    Private Sub LoadRequestCommittee()
        dt = ctlReq.RequestCommittee_Get(StrNull2Zero(hdRequestID.Value))
        grdCommittee.DataSource = dt
        grdCommittee.DataBind()
    End Sub


    Protected Sub grdCommittee_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdCommittee.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    If ctlReq.RequestCommittee_Delete(e.CommandArgument, StrNull2Zero(hdRequestID.Value)) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadRequestCommittee()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If
            End Select
        End If
    End Sub

    Private Sub grdCommittee_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdCommittee.RowDataBound

        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As ImageButton = e.Row.Cells(3).FindControl("imgDel")
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
        Response.Redirect("ResultProcess.aspx?t=req&p=del&reqid=" & hdRequestID.Value & "&pid=" & hdStudentID.Value & "&tid=" & hdTopicUID.Value)
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

            ctlReq.RequestDocument_Add(StrNull2Zero(hdRequestID.Value), 0, "", AttachFileName, Request.Cookies("UserLoginID").Value)

            LoadRequestDocument()
        End If
    End Sub
    Private Sub LoadRequestDocument()
        'dt = ctlReq.RequestDocument_Get(StrNull2Zero(hdRequestID.Value))
        'grdDocument.DataSource = dt
        'grdDocument.DataBind()
    End Sub



    'Protected Sub ddlTopicID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTopicID.SelectedIndexChanged
    '    pnCommittee.Visible = False
    '    If Session("CurrentStep") = ProcessStep.Finish Then
    '        Select Case hdTopicUID.Value
    '            Case 1
    '                pnCommittee.Visible = True
    '        End Select
    '    End If
    'End Sub

    Protected Sub grdDocument_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdDocument.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel_Doc"
                    If ctlReq.RequestDocument_Delete(e.CommandArgument, StrNull2Zero(hdRequestID.Value)) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadRequestDocument()
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

    Protected Sub cmdAddCommittee1_Click(sender As Object, e As EventArgs) Handles cmdAddCommittee1.Click
        If ddlAdvisor.SelectedValue = Nothing Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาเลือกอาจารยคุมสอบก่อน');", True)
            Exit Sub
        End If
        If ddlPosition1.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาเลือกตำแหน่งก่อน');", True)
            Exit Sub
        End If

        ctlReq.RequestCommittee_Add(StrNull2Zero(hdRequestID.Value), StrNull2Zero(ddlAdvisor.SelectedValue), ddlAdvisor.SelectedItem.Text, "คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย", StrNull2Zero(ddlPosition1.SelectedValue), Request.Cookies("UserLoginID").Value)

        LoadRequestCommittee()
    End Sub
    Protected Sub cmdAddCommittee2_Click(sender As Object, e As EventArgs) Handles cmdAddCommittee2.Click
        If txtCommitteeName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุชื่อ-สกุลก่อน');", True)
            Exit Sub
        End If
        If txtCommitteeDepartment.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุสังกัดก่อน');", True)
            Exit Sub
        End If
        If ddlPosition2.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาเลือกตำแหน่งก่อน');", True)
            Exit Sub
        End If

        ctlReq.RequestCommittee_Add(StrNull2Zero(hdRequestID.Value), 0, txtCommitteeName.Text, txtCommitteeDepartment.Text, StrNull2Zero(ddlPosition2.SelectedValue), Request.Cookies("UserLoginID").Value)

        LoadRequestCommittee()
    End Sub

End Class