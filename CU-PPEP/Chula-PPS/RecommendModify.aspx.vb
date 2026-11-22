Imports System.Drawing

Public Class RecommendModify
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlR As New RecommendController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadSubject()
            LoadLocationToDDL()
            lblHeadName.Text = "แหล่งฝึก"

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
                    ddlSubject.Enabled = True
                    cmdSave.Visible = True
                    cmdDel.Visible = True
                Else
                    ddlLocation.Enabled = False
                    ddlSubject.Enabled = False
                    cmdSave.Visible = False
                    cmdDel.Visible = False
                End If

            ElseIf Request.Cookies("ROLE_SPA").Value = True Or Request.Cookies("ROLE_ADM").Value = True Then
                imgPending.Enabled = True
                imgApprove.Enabled = True
                imgReject.Enabled = True
                ddlLocation.Enabled = False
                ddlSubject.Enabled = False
                cmdSave.Visible = True
                cmdDel.Visible = True
            Else
                imgPending.Enabled = False
                imgApprove.Enabled = False
                imgReject.Enabled = False
                ddlLocation.Enabled = False
                ddlSubject.Enabled = False
                cmdSave.Visible = False
                cmdDel.Visible = False
                ddlSubject.Enabled = False
                txtShortDesc.ReadOnly = True
                txtShortDesc.BackColor = Color.White
                ddlSubject.BackColor = Color.White
                txtRecommend.ActiveView = DevExpress.Web.ASPxHtmlEditor.HtmlEditorView.Preview
                txtRecommend.Enabled = False
            End If
        End If

    End Sub
    Private Sub LoadSubject()
        Dim ctlSj As New SubjectController
        dt = ctlSj.Subject_GetForSearch()
        If dt.Rows.Count > 0 Then
            ddlSubject.DataSource = dt
            ddlSubject.DataTextField = "SubjectName"
            ddlSubject.DataValueField = "SubjectID"
            ddlSubject.DataBind()
            ddlSubject.SelectedIndex = 0
        End If
        dt = Nothing
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
        dt = ctlR.LocationRecommend_GetByID(Request("id"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdUID.Value = .Item("UID")
                txtShortDesc.Text = .Item("Title")
                txtRecommend.Html = .Item("Recommend")
                ddlLocation.SelectedValue = .Item("LocationID")

                If String.Concat(.Item("SubjectID")) <> "" Then
                    ddlSubject.SelectedValue = .Item("SubjectID")
                End If

                hdStdCode.Value = .Item("StudentCode")
                lblStudentName.Text = .Item("StudentName")
                lblStatus.Text = String.Concat(.Item("StatusName"))

                imgPending.ImageUrl = "images/statusicon_pending_01.png"
                imgApprove.ImageUrl = "images/statusicon_approve_01.png"
                imgReject.ImageUrl = "images/statusicon_reject_01.png"

                Select Case .Item("ApproveStatus")
                    Case "P"
                        imgPending.ImageUrl = "images/statusicon_pending_02.png"
                    Case "A"
                        imgApprove.ImageUrl = "images/statusicon_approve_02.png"
                    Case "X"
                        imgReject.ImageUrl = "images/statusicon_reject_02.png"
                End Select


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
            ctlR.LocationRecommend_Save(StrNull2Zero(hdUID.Value), Request.Cookies("Username").Value, ddlLocation.SelectedValue, ddlSubject.SelectedValue, txtShortDesc.Text, txtRecommend.Html)
        Else
            ctlR.LocationRecommend_UpdateByAdmin(StrNull2Zero(hdUID.Value), txtShortDesc.Text, txtRecommend.Html)
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Private Sub UpdateStatus(Status As String)
        ctlR.LocationRecommend_UpdateStatus(hdUID.Value, Status)
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
        If ctlR.LocationRecommend_Delete(hdUID.Value) Then
            Dim acc As New UserController
            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "LocationRecommend", "ลบคำแนะนำ:" & hdUID.Value, "")
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
            Response.Redirect("Recommend?ActionType=rcm&ItemType=rcm1")
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
        End If
    End Sub
End Class