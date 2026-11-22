Imports System.Drawing

Public Class RoomRecommendModify
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlR As New RecommendController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadLocationToDDL()

            If Not Request("id") Is Nothing Then
                LoadRecommendDetail()
            Else
                hdStdCode.Value = Request.Cookies("ProfileID").Value
                lblStudentName.Text = Request.Cookies("NameOfUser").Value
            End If

            If Request.Cookies("ROLE_STD").Value = True Then
                imgPending.Enabled = False
                imgApprove.Enabled = False
                imgReject.Enabled = False
                txtRecommend.ActiveView = DevExpress.Web.ASPxHtmlEditor.HtmlEditorView.Design

                If Request.Cookies("Username").Value = String.Concat(hdStdCode.Value) Then
                    'เป็นนิสิตเจ้าของเรื่อง
                    ddlLocation.Enabled = True
                    cmdSave.Visible = True
                    cmdDel.Visible = True
                Else
                    ddlLocation.Enabled = False
                    cmdSave.Visible = False
                    cmdDel.Visible = False
                End If

            ElseIf Request.Cookies("ROLE_SPA").Value = True Or Request.Cookies("ROLE_ADM").Value = True Then
                imgPending.Enabled = True
                imgApprove.Enabled = True
                imgReject.Enabled = True
                ddlLocation.Enabled = False
                cmdSave.Visible = True
                cmdDel.Visible = True
            Else
                imgPending.Enabled = False
                imgApprove.Enabled = False
                imgReject.Enabled = False
                ddlLocation.Enabled = False
                cmdSave.Visible = False
                cmdDel.Visible = False
                txtShortDesc.ReadOnly = True
                txtShortDesc.BackColor = Color.White
                txtRecommend.ActiveView = DevExpress.Web.ASPxHtmlEditor.HtmlEditorView.Preview
                txtRecommend.Enabled = False
            End If
        End If

    End Sub
    Private Sub LoadLocationToDDL()
        ddlLocation.Items.Clear()
        Dim ctlL As New LocationController

        If Request.Cookies("ROLE_STD").Value = True Then
            If Not Request("id") Is Nothing Then
                dt = ctlL.Location_Get()
            Else
                dt = ctlL.Location_GetByStudent(Request.Cookies("Username").Value)
                If dt.Rows.Count = 0 Then
                    Response.Redirect("ResultPage.aspx?p=rcm1")
                End If
            End If
        Else
            dt = ctlL.Location_Get()
        End If
        If dt.Rows.Count > 0 Then
            With ddlLocation
                .DataSource = dt
                .DataTextField = "LocationName"
                .DataValueField = "LocationID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
    End Sub


    Private Sub LoadRecommendDetail()
        dt = ctlR.RoomRecommend_GetByID(Request("id"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdUID.Value = String.Concat(.Item("UID"))
                txtShortDesc.Text = String.Concat(.Item("Title"))
                txtRecommend.Html = .Item("Recommend")
                hdStdCode.Value = String.Concat(.Item("StudentCode"))
                lblStudentName.Text = String.Concat(.Item("StudentName"))
                lblStatus.Text = String.Concat(.Item("StatusName"))
                ddlLocation.SelectedValue = .Item("LocationID")
                imgPending.ImageUrl = "images/statusicon_pending_01.png"
                imgApprove.ImageUrl = "images/statusicon_approve_01.png"
                imgReject.ImageUrl = "images/statusicon_reject_01.png"

                Select Case String.Concat(.Item("ApproveStatus"))
                    Case "P"
                        imgPending.ImageUrl = "images/statusicon_pending_02.png"
                    Case "A"
                        imgApprove.ImageUrl = "images/statusicon_approve_02.png"
                    Case "X"
                        imgReject.ImageUrl = "images/statusicon_reject_02.png"
                End Select

                If Request.Cookies("ROLE_STD").Value = True Then
                    If .Item("ApproveStatus") = "A" Then
                        cmdSave.Visible = False
                    Else
                        cmdSave.Visible = True
                    End If
                Else

                End If
            End With
        End If


    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtShortDesc.Text.Trim() = "" Then
            DisplayMessage(Me.Page, "ระบุคำแนะนำสั้นๆก่อน")
            Exit Sub
        End If
        If txtRecommend.Html.Trim() = "" Then
            DisplayMessage(Me.Page, "ระบุคำแนะนำก่อน")
            Exit Sub
        End If

        If Request.Cookies("ROLE_STD").Value = True Then
            ctlR.RoomRecommend_Save(StrNull2Zero(hdUID.Value), Request.Cookies("Username").Value, ddlLocation.SelectedValue, txtShortDesc.Text, txtRecommend.Html)
        Else
            ctlR.RoomRecommend_UpdateByAdmin(StrNull2Zero(hdUID.Value), txtShortDesc.Text, txtRecommend.Html)
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Private Sub UpdateStatus(Status As String)
        ctlR.RoomRecommend_UpdateStatus(hdUID.Value, Status)
        LoadRecommendDetail()
    End Sub

    Protected Sub imgPending_Click(sender As Object, e As ImageClickEventArgs) Handles imgPending.Click
        UpdateStatus("P")
    End Sub

    Protected Sub imgApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgApprove.Click
        UpdateStatus("A")
    End Sub

    Protected Sub imgReject_Click(sender As Object, e As ImageClickEventArgs) Handles imgReject.Click
        UpdateStatus("X")
    End Sub

    Private Sub cmdDel_Click(sender As Object, e As EventArgs) Handles cmdDel.Click
        If ctlR.RoomRecommend_Delete(hdUID.Value) Then
            Dim acc As New UserController
            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "RoomRecommend", "ลบคำแนะนำ:" & hdUID.Value, "")
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)

        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
        End If
    End Sub
End Class