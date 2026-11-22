Public Class PC2_Quota
    Inherits System.Web.UI.Page

    Dim ctlLG As New SubjectController
    Dim ctlReq As New PC2Controller

    Dim dt As New DataTable
    Dim ds As New DataSet

    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblValidate.Visible = False
            LoadYearToDDL()
            LoadSubjectToDDL()
            LoadQuota(ddlYear.SelectedValue)
            'If Not Request("y") Is Nothing Then
            '    ddlSubject.SelectedValue = Request("id")
            '    ddlYear.SelectedValue = Request("y")
            '    LoadQuota(Request("y"), Request("id"))
            'End If
        End If

        txtQuota.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        ' txtConfirm.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub

    Private Sub LoadQuota(sYear As Integer)

        Dim dtReq As New DataTable

        dtReq = ctlReq.PC2_Quota_GetByYear(sYear)

        If dtReq.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dtReq
                .DataBind()
            End With
            lblValidate.Visible = False

        Else
            'lblValidate.Visible = True
            'lblValidate.Text = "ไม่พบกรุณากำหนดวิชาก่อน"
            grdData.Visible = False
        End If

        'optGender.SelectedValue = ctlLG.Subject_GetIsSameGender(sSubjectID)

    End Sub

    Private Sub LoadYearToDDL()

        Dim y As Integer = StrNull2Zero(DisplayYear(ctlReq.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlReq.PC2_GetYear
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

    Private Sub LoadSubjectToDDL()
        dt = ctlReq.PC2_Subject_GetNoQuota(StrNull2Zero(ddlYear.SelectedValue))
        ddlSubject.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlSubject

                .Enabled = True
                .DataSource = dt
                .DataTextField = "NameTH"
                .DataValueField = "UID"
                .DataBind()
            End With
        End If
    End Sub

    Protected Function validateData() As Boolean
        Dim result As Boolean = True
        lblValidate.Text = ""

        If ddlSubject.SelectedValue = "0" Then
            result = False
            lblValidate.Text &= "- กรุณาระบุวิชาสอบ  <br />"
            lblValidate.Visible = True

        End If

        Return result
    End Function

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If validateData() Then

            lblValidate.Visible = False

            For i = 0 To grdData.Rows.Count - 1
                Dim txtSc1 As TextBox = grdData.Rows(i).Cells(2).FindControl("txtQTY")
                ctlReq.PC2_Quota_Save(StrNull2Zero(ddlYear.SelectedValue), grdData.DataKeys(i).Value, StrNull2Zero(txtSc1.Text), Request.Cookies("UserID").Value)
            Next

            'Dim ctlL As New SubjectController
            'ctlLG.Subject_UpdateisSameGender(ddlSubject.SelectedValue, optGender.SelectedValue)

            '    acc.User_GenLogfile(Request.Cookies("UserID").Value, ACTTYPE_UPD, "Subjects", "แก้ไข ความต้องการ:" & ddlSubject.SelectedValue, "")

            'ClearData()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)
        Else

        End If

    End Sub
    Protected Sub ddlSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject.SelectedIndexChanged
        LoadSubjectToDDL()
        LoadQuota(ddlYear.SelectedValue)
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadSubjectToDDL()
        LoadQuota(ddlYear.SelectedValue)
    End Sub
    Private Sub grdData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(3).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        If validateData() Then
            ctlReq.PC2_Quota_Save(StrNull2Zero(ddlYear.SelectedValue), ddlSubject.SelectedValue, StrNull2Zero(txtQuota.Text), Request.Cookies("UserID").Value)
            LoadSubjectToDDL()
            LoadQuota(ddlYear.SelectedValue)
        End If
    End Sub

    Protected Sub grdData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    ctlReq.PC2_Quota_Delete(e.CommandArgument)
                    LoadSubjectToDDL()
                    LoadQuota(ddlYear.SelectedValue)
            End Select
        End If
    End Sub
End Class

