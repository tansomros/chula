Imports System.Net
Imports System.IO
Public Class RequestExam
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
        'If IsNothing(Request.Cookies("UserID").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            cmdPrint.Visible = False
            cmdDelete.Visible = False
            optYesOrNo.SelectedValue = "N"
            pnYes1.Visible = False
            pnYes2.Visible = False
            pnYes3.Visible = False

            txtReqDate.Text = DisplayShortDateTH(ctlReq.GET_DATE_SERVER())

            Session("CurrentStep") = ProcessStep.Begin

            LoadStudentData(Request.Cookies("PersonID").Value)
            'LoadDocument()
            If Not Request("tid") = Nothing Then
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

                optYesOrNo.SelectedValue = String.Concat(.Item("IsExam"))
                txtRemarkNo.Text = String.Concat(.Item("ExamRemark"))
                txtEmphasize.Text = String.Concat(.Item("Emphasize"))
                optType.SelectedValue = String.Concat(.Item("ExamType"))

                optKnowledgeNo.SelectedValue = String.Concat(.Item("KnowledgeNo"))
                optExamRound.SelectedValue = String.Concat(.Item("ExamTime"))
                txtLocationName.Text = String.Concat(.Item("ExamLocation"))


                If optYesOrNo.SelectedValue = "N" Then
                    pnYes1.Visible = False
                    pnYes2.Visible = False
                    pnYes3.Visible = False
                    pnNo.Visible = True
                Else
                    pnNo.Visible = False
                    If DBNull2Zero(.Item("TopicUID")) = 5 Then
                        pnYes1.Visible = True
                    Else
                        pnYes1.Visible = False
                    End If
                    pnYes2.Visible = True
                    pnYes3.Visible = True
                End If


                'txtThesisName.Text = String.Concat(.Item("ThesisName"))
                'If String.Concat(.Item("AdmitDate")) <> "" Then
                '    txtAdmitDate.Text = DisplayShortDateTH(.Item("AdmitDate"))
                'End If

                'If String.Concat(.Item("AdmitTimeStart")) <> "" Then
                '    ddlStartTimeHour.SelectedValue = Left(String.Concat(.Item("AdmitTimeStart")), 2)
                '    ddlStartTimeMinute.SelectedValue = Right(String.Concat(.Item("AdmitTimeStart")), 2)
                'End If
                'If String.Concat(.Item("AdmitTimeEnd")) <> "" Then
                '    ddlEndTimeHour.SelectedValue = Left(String.Concat(.Item("AdmitTimeEnd")), 2)
                '    ddlEndTimeMinute.SelectedValue = Right(String.Concat(.Item("AdmitTimeEnd")), 2)
                'End If

                'txtRoomNo.Text = String.Concat(.Item("RoomNo"))
                'txtDepartment.Text = String.Concat(.Item("DepartmentName"))
                'txtFloor.Text = String.Concat(.Item("FloorNo"))
                'txtBuilding.Text = String.Concat(.Item("Building"))
                'txtLocationName.Text = String.Concat(.Item("LocationName"))
                'txtRoad.Text = String.Concat(.Item("Road"))
                'txtSubdistrict.Text = String.Concat(.Item("Subdistrict"))
                'txtDistrict.Text = String.Concat(.Item("District"))
                'ddlProvince.SelectedValue = String.Concat(.Item("ProvinceID"))
                'txtZipcode.Text = String.Concat(.Item("Zipcode"))

                'LoadStudentData(hdStudentID.Value)
                'LoadRequestCommittee()
                'LoadRequestDocument()
                'Session("CurrentStep") = ProcessStep.Finish

                'Select Case hdTopicUID.Value
                '    Case 1
                '        pnCommittee.Visible = True
                'End Select
                ''pnWorkSend.Visible = True

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

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtReqDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุวันที่เข้าสอบก่อน');", True)
            Exit Sub
        End If
        If optYesOrNo.SelectedValue = "N" And txtRemarkNo.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุเหตุผลที่ไม่เข้าสอบก่อน');", True)
            Exit Sub
        End If

        If optYesOrNo.SelectedValue = "Y" And txtLocationName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุสถานที่สอบก่อน');", True)
            Exit Sub
        End If

        ctlReq.RequestExam_Save(StrNull2Zero(hdRequestID.Value), ConvertStrDate2DateQueryString(txtReqDate.Text), hdStudentID.Value, StrNull2Zero(hdTopicUID.Value), optYesOrNo.SelectedValue, txtRemarkNo.Text, txtEmphasize.Text, StrNull2Zero(optType.SelectedValue), StrNull2Zero(optKnowledgeNo.SelectedValue), StrNull2Zero(optExamRound.SelectedValue), txtLocationName.Text, Request.Cookies("UserLoginID").Value)

        Response.Redirect("ResultPage.aspx?t=req&p=complete")
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

    Protected Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click

    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Response.Redirect("ResultProcess.aspx?t=req&p=del&reqid=" & hdRequestID.Value & "&pid=" & hdStudentID.Value & "&tid=" & hdTopicUID.Value)
    End Sub

    Private Sub LoadRequestDocument()
        'dt = ctlReq.RequestDocument_Get(StrNull2Zero(hdRequestID.Value))
        'grdDocument.DataSource = dt
        'grdDocument.DataBind()
    End Sub

    Protected Sub optYesOrNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optYesOrNo.SelectedIndexChanged

        If optYesOrNo.SelectedValue = "N" Then
            pnYes1.Visible = False
            pnYes2.Visible = False
            pnYes3.Visible = False
            pnNo.Visible = True
        Else
            pnNo.Visible = False
            If Request("tid") = "5" Then
                pnYes1.Visible = True
            Else
                pnYes1.Visible = False
            End If
            pnYes2.Visible = True
            pnYes3.Visible = True
        End If
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
End Class