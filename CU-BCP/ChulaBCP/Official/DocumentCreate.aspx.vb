Imports System.IO
Public Class DocumentCreate
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlStd As New StudentController
    Dim ctlReq As New RequestController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            cmdPrint.Visible = False
            cmdDelete.Visible = False
            pnCommittee.Visible = False


            'LoadStudentData(Request.Cookies("PersonID").Value)
            'LoadDocument()

            If Not Request("Reqid") = Nothing Then
                'LoadRequestData()
            End If
        End If

        'Dim scriptString As String = "javascript:return confirm(""ต้องการลบการลงทะเบียนนี้ ใช่หรือไม่?"");"
        'cmdDelete.Attributes.Add("onClick", scriptString)
        'txtZipcode.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click


        'ctlReq.Request_Save(StrNull2Zero(hdRequestID.Value), ConvertStrDate2DateQueryString(txtReqDate.Text), hdStudentID.Value, StrNull2Zero(ddlTopicID.SelectedValue), txtThesisName.Text, ConvertStrDate2DateQueryString(txtAdmitDate.Text), ddlStartTimeHour.SelectedValue & ddlStartTimeMinute.SelectedValue, ddlEndTimeHour.SelectedValue & ddlEndTimeMinute.SelectedValue, txtRoomNo.Text, txtDepartment.Text, txtFloor.Text, txtBuilding.Text, txtLocationName.Text, txtRoad.Text, txtSubdistrict.Text, txtDistrict.Text, ddlProvince.SelectedValue, txtZipcode.Text, Request.Cookies("UserLoginID").Value)

        'If Session("CurrentStep") = ProcessStep.Begin Then

        '    hdRequestID.Value = ctlReq.Request_GetRequestID(hdStudentID.Value, ConvertStrDate2DateQueryString(txtReqDate.Text), ddlTopicID.SelectedValue)
        '    pnCommittee.Visible = True
        '    'pnWorkSend.Visible = True
        '    Session("CurrentStep") = ProcessStep.Finish
        '    cmdSave.Text = "Save"
        'ElseIf Session("CurrentStep") = ProcessStep.Process Then
        '    pnCommittee.Visible = True
        'ElseIf Session("CurrentStep") = ProcessStep.Finish Then
        '    If grdCommittee.Rows.Count <= 0 Then
        '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาเพิ่มชื่อกรรมการควบคุมสอบก่อน');", True)
        '        Exit Sub
        '    End If

        '    'pnCommittee.Visible = True
        '    Dim objuser As New UserController
        '    objuser.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_UPD, "Request", "บันทึก/แก้ไข คำร้อง :" & lblStdCode.Text, "")
        '    'cmdPrint.Visible = True
        '    Response.Redirect("ResultPage.aspx?t=Req&p=complete")
        'End If
    End Sub

End Class