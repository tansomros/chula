
Public Class PC2_StudentSelection
    Inherits System.Web.UI.Page

    Dim ctlA As New PC2Controller

    Dim acc As New UserController
    Dim ctlCfg As New SystemConfigController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then

            If Not ctlA.PC2_Student_HasPermission(ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR), Request.Cookies("ProfileID").Value) Then
                Response.Redirect("ResultPage.aspx?p=unrole")
            End If



            lblNone.Visible = False
            If Request.Cookies("ROLE_STD").Value = True Then
                If Not SystemOnlineTime() Then
                    Response.Redirect("ResultPage.aspx?t=closed&p=pc2select")
                End If
            End If
            LoadYearToDDL()
            LoadSubjectToDDL()

            If Not Request("y") Is Nothing Then
                ddlYear.SelectedValue = Request("y")
                LoadSubjectToDDL()
                ddlSubject.SelectedValue = Request("id")
            End If

            LoadStudentSubjectToGrid()

        End If

    End Sub
    Private Function SystemOnlineTime() As Boolean
        Dim Bdate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_PC2_STARTDATE)))
        Dim Edate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_PC2_ENDDATE)))
        Dim sToday As Integer = StrNull2Zero(ConvertDate2DBString(ctlCfg.GET_DATE_SERVER))
        Dim bAvailable As Boolean
        If sToday < Bdate Then
            bAvailable = False
        ElseIf sToday > Edate Then
            bAvailable = False
        Else
            bAvailable = True
        End If
        Return bAvailable
    End Function
    Private Sub LoadSubjectToDDL()
        ddlSubject.Items.Clear()
        Dim dtC As New DataTable
        dtC = ctlA.PC2_Subject_GetByYear(StrNull2Zero(ddlYear.SelectedValue))
        If dtC.Rows.Count > 0 Then
            With ddlSubject
                .Enabled = True
                .DataSource = dtC
                .DataTextField = "NameTH"
                .DataValueField = "UID"
                .DataBind()
            End With
            ddlSubject.SelectedIndex = 0
        End If
        dtC = Nothing
    End Sub

    Private Sub LoadYearToDDL()
        Dim dtY As New DataTable
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlA.GET_DATE_SERVER))
        Dim LastRow As Integer
        dtY = ctlA.PC2_GetYear
        LastRow = dtY.Rows.Count - 1

        If dtY.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dtY
                .DataTextField = "EduYear"
                .DataValueField = "EduYear"
                .DataBind()

                If dtY.Rows(LastRow)(0) = y Then
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 1).Value = y + 1
                ElseIf dtY.Rows(LastRow)(0) > y Then
                    'ddlYear.Items.Add(y + 2)
                    'ddlYear.Items(LastRow + 1).Value = y + 2
                ElseIf dtY.Rows(LastRow)(0) < y Then
                    ddlYear.Items.Add(y)
                    ddlYear.Items(LastRow + 1).Value = y
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 2).Value = y + 1
                End If
            End With
            ddlYear.SelectedValue = ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR)
        Else
            ddlYear.Items.Add(y)
            ddlYear.Items(0).Value = y
            ddlYear.Items.Add(y + 1)
            ddlYear.Items(1).Value = y + 1
            ddlYear.SelectedIndex = 0
        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        dtY = Nothing
    End Sub

    Private Sub LoadStudentSubjectToGrid()

        If ddlSubject.Items.Count > 0 Then
            Dim dtSL As New DataTable
            Dim iQuota As Integer

            dtSL = ctlA.PC2_StudentRegister_GetByStudent(ddlYear.SelectedValue, Request.Cookies("ProfileID").Value)
            If dtSL.Rows.Count > 0 Then
                lblNo.Visible = False
                pnData.Visible = True

                With grdSubject
                    .Visible = True
                    .DataSource = dtSL
                    .DataBind()

                    For i = 0 To .Rows.Count - 1

                        Dim imgU As ImageButton = .Rows(i).Cells(2).FindControl("imgUp")
                        Dim imgD As ImageButton = .Rows(i).Cells(2).FindControl("imgDown")

                        If i = 0 Then
                            imgU.Visible = False
                        ElseIf i = .Rows.Count - 1 Then
                            imgD.Visible = False
                        End If
                        If .Rows.Count = 1 Then
                            imgU.Visible = False
                            imgD.Visible = False
                        End If

                    Next

                End With

                iQuota = StrNull2Zero(ctlCfg.SystemConfig_GetByCode(CFG_PC2_MAXSUBJECT))

                If iQuota > dtSL.Rows.Count Then
                    lblFull.Text = "นิสิตสามารถเลือกวิชาสอบได้อีก " & iQuota - dtSL.Rows.Count & " วิชา (ไม่บังคับ)"

                Else
                    lblFull.Text = "นิสิตเลือกวิชาสอบครบตามจำนวนที่กำหนดแล้ว"

                End If

                lblFull.Visible = True
            Else
                'pnData.Visible = False
                'lblNo.Visible = True
                grdSubject.Visible = False
                grdSubject.DataSource = Nothing
                lblFull.Text = ""
                lblFull.Visible = False
            End If
            dtSL = Nothing
        Else
            lblNo.Visible = True
            grdSubject.Visible = False
            grdSubject.DataSource = Nothing
            lblFull.Text = ""
            lblFull.Visible = False
        End If

    End Sub
    Private Sub grdSubject_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdSubject.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Dim str() As String
            str = Split(ddlSubject.SelectedItem.Text, " : ")

            Select Case ButtonPressed.ID
                Case "imgUp"
                    UpdateRegisterPriority(CInt(DBNull2Zero(e.CommandArgument)), "U")
                Case "imgDown"
                    UpdateRegisterPriority(CInt(DBNull2Zero(e.CommandArgument)), "D")
                Case "imgDel"

                    If ctlA.PC2_StudentRegister_CheckAssessment(StrNull2Zero(grdSubject.DataKeys(e.CommandArgument).Value)) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถลบได้ เนื่องจากท่านได้รับการคัดเลือกวิชาสอบในรายวิชานี้เรียบร้อยแล้ว');", True)
                        Exit Sub
                    Else
                        ctlA.PC2_StudentRegister_Delete(StrNull2Zero(grdSubject.DataKeys(e.CommandArgument).Value))
                        LoadStudentSubjectToGrid()
                        UpdatePriority()
                    End If
            End Select
            LoadStudentSubjectToGrid()

        End If
    End Sub
    Private Sub UpdateRegisterPriority(row As Integer, flag As String)
        Dim str() As String
        str = Split(ddlSubject.SelectedItem.Text, " : ")

        If flag = "U" Then
            ctlA.PC2_StudentRegister_UpdatePriority(StrNull2Zero(grdSubject.DataKeys(row).Value), "U", Request.Cookies("Username").Value)
            ctlA.PC2_StudentRegister_UpdatePriority(StrNull2Zero(grdSubject.DataKeys(row - 1).Value), "D", Request.Cookies("Username").Value)

        Else
            ctlA.PC2_StudentRegister_UpdatePriority(StrNull2Zero(grdSubject.DataKeys(row).Value), "D", Request.Cookies("Username").Value)
            ctlA.PC2_StudentRegister_UpdatePriority(StrNull2Zero(grdSubject.DataKeys(row + 1).Value), "U", Request.Cookies("Username").Value)
        End If

    End Sub

    Private Sub UpdatePriority()
        Dim str() As String
        str = Split(ddlSubject.SelectedItem.Text, " : ")
        For i = 0 To grdSubject.Rows.Count - 1
            ctlA.PC2_StudentRegister_UpdatePriorityItem(StrNull2Zero(grdSubject.DataKeys(i).Value), i + 1, Request.Cookies("Username").Value)
        Next
    End Sub
    Private Sub grdSubject_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubject.RowDataBound

        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(3).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub
    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadSubjectToDDL()
        LoadStudentSubjectToGrid()
    End Sub

    Protected Sub ddlSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject.SelectedIndexChanged
        LoadStudentSubjectToGrid()
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        If ddlSubject.SelectedValue = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','เลือกวิชาสอบก่อน');", True)
            Exit Sub
        End If
        Dim iCount As Integer = 0
        Dim iQuota As Integer
        Dim ctlCfg As New SystemConfigController
        Dim RegYear As Integer

        RegYear = ddlYear.SelectedValue

        iCount = ctlA.PC2_StudentRegister_GetCount(RegYear, Request.Cookies("ProfileID").Value)
        iQuota = ctlCfg.SystemConfig_GetByCode(CFG_MAXLOCATION)
        If iCount < iQuota Then
            ctlA.PC2_StudentRegister_Add(RegYear, Request.Cookies("ProfileID").Value, iCount + 1, StrNull2Zero(ddlSubject.SelectedValue), Request.Cookies("Username").Value)

            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "StudentRegister", "นิสิตเลือกวิชา PC2 :>>" & Request.Cookies("ProfileID").Value, "วิชา :" & ddlSubject.SelectedItem.Text)

            Response.Redirect("PC2_StudentSelection.aspx?m=2&p=202")

        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ท่านได้เลือกวิชาสอบครบตามจำนวนแล้ว ท่านไม่สามารถเลือกเพิ่มได้อีก');", True)
        End If


    End Sub
End Class

