
Public Class PracticeAssign
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlA As New AssessmentController
    Dim acc As New UserController
    Dim ctlSk As New SkillController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Redirect("503.aspx")
        If IsNothing(Request.Cookies("UserLogin").Value) Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblNotic.Visible = False

            LoadYearToDDL()
            LoadCourseToDDL()

            LoadSkillToDDL()
            LoadLocationToDDL()
            LoadSkillPhase()

            LoadAssessmentToGrid()
            LoadStudentNoAssessmentToGrid()

        End If
    End Sub
    Private Sub LoadYearToDDL()
        Dim ctlCs As New CourseController
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlCs.Courses_GetYear
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "CYear"
                .DataValueField = "CYear"
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

    Private Sub LoadSkillToDDL()
        If ddlSubject.SelectedValue = "" Then
            dt = ctlSk.Skill_GetByYear(ddlYear.SelectedValue)
        Else
            dt = ctlSk.Skill_GetBySubject(ddlSubject.SelectedValue)
        End If

        If dt.Rows.Count > 0 Then
            With ddlSkill
                .Enabled = True
                .DataSource = dt
                .DataTextField = "SkillName"
                .DataValueField = "SkillUID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub LoadSkillPhase()
        ddlPhase.Items.Clear()
        Dim ctlTP As New REQcontroller
        Dim dtP As New DataTable
        Dim k As Integer
        k = 0
        dtP = ctlTP.Requirements_GetByLocationSkill(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlLocation.SelectedValue), StrNull2Zero(ddlSkill.SelectedValue))
        'dtP = ctlTP.TurnPhase_GetActiveByYear(StrNull2Zero(ddlYear.SelectedValue))
        If dtP.Rows.Count > 0 Then
            For i = 0 To dtP.Rows.Count - 1
                If (DBNull2Zero(dtP.Rows(i)("NoSpec")) + DBNull2Zero(dtP.Rows(i)("Man")) + DBNull2Zero(dtP.Rows(i)("Women"))) > 0 Then
                    ddlPhase.Items.Add(dtP.Rows(i)("PhaseNo") & " : " & dtP.Rows(i)("PhaseName") & "  (ช=" & DBNull2Zero(dtP.Rows(i)("Man")) & "|ญ=" & DBNull2Zero(dtP.Rows(i)("Women")) & "|ม=" & DBNull2Zero(dtP.Rows(i)("NoSpec")) & ")")
                    ddlPhase.Items(k).Value = dtP.Rows(i)("SkillPhaseID")
                    k = k + 1
                End If
            Next
            'ddlPhase.SelectedIndex = 0

            'With ddlPhase
            '    .Visible = True
            '    .DataSource = dtP
            '    .DataTextField = "Name"
            '    .DataValueField = "PhaseNo"
            '    .DataBind()
            '    .SelectedIndex = 0
            'End With
        Else
            ddlPhase.DataSource = dtP
            ddlPhase.DataBind()
        End If
        dtP = Nothing
    End Sub

    'Private Sub LoadTimePhase()

    '    ddlPhase.Items.Clear()

    '    Dim ctlTP As New REQcontroller
    '    Dim dtP As New DataTable
    '    Dim k As Integer
    '    k = 0
    '    dtP = ctlTP.Requirements_GetByLocationSkill(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlLocation.SelectedValue), StrNull2Zero(ddlSkill.SelectedValue))
    '    'dtP = ctlTP.TurnPhase_GetActiveByYear(StrNull2Zero(ddlYear.SelectedValue))
    '    If dtP.Rows.Count > 0 Then
    '        For i = 0 To dtP.Rows.Count - 1
    '            If (DBNull2Zero(dtP.Rows(i)("NoSpec")) + DBNull2Zero(dtP.Rows(i)("Man")) + DBNull2Zero(dtP.Rows(i)("Women"))) > 0 Then
    '                ddlPhase.Items.Add(dtP.Rows(i)("PhaseNo") & " : " & dtP.Rows(i)("PhaseName"))
    '                ddlPhase.Items(k).Value = dtP.Rows(i)("TimePhaseID")
    '                k = k + 1
    '            End If

    '        Next
    '        'ddlPhase.SelectedIndex = 0

    '        'With ddlPhase
    '        '    .Visible = True
    '        '    .DataSource = dtP
    '        '    .DataTextField = "Name"
    '        '    .DataValueField = "PhaseNo"
    '        '    .DataBind()
    '        '    .SelectedIndex = 0
    '        'End With
    '    Else
    '        ddlPhase.DataSource = dtP
    '        ddlPhase.DataBind()
    '    End If
    '    dtP = Nothing
    'End Sub

    Private Sub LoadStudentNoAssessmentToGrid()
        Dim CtlCs As New CourseController
        If StrNull2Zero(ddlSkill.SelectedValue) <> 0 Then
            'Dim str() As String = Split(ddlSubject.SelectedItem.Text, " : ")

            'นศ. ทุกคน ที่ยังไม่ได้จัดสรรตามเงื่อนไขนี้ ไม่จำเป็นว่าต้องลงทะเบียนเรียนมาก่อน ในเมนูประจำปี
            dt = CtlCs.Student_GetStudentNoAssessment(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlPhase.SelectedValue), Trim(txtSearch.Text))
            If dt.Rows.Count > 0 Then

                With grdData
                    .Visible = True
                    .DataSource = dt
                    .DataBind()
                End With
                lblNotic.Visible = False
                cmdSave.Enabled = True
                cmdClear.Enabled = True
            Else
                grdData.Visible = False
                lblNotic.Text = "ไม่พบนศ.ที่ท่านค้นหา หรือ นศ.คนนี้ได้จัดสรรในผลัดที่ท่านเลือกแล้ว"
                lblNotic.Visible = True
                cmdSave.Enabled = False
                cmdClear.Enabled = False
            End If
        Else
            grdData.Visible = False
        End If

    End Sub

    Private Sub LoadAssessmentToGrid()

        If StrNull2Zero(ddlSkill.SelectedValue) <> 0 Then
            dt = ctlA.Assessment_GetBySearch(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlSkill.SelectedValue), StrNull2Zero(ddlLocation.SelectedValue), StrNull2Zero(ddlPhase.SelectedValue), Trim(txtSearchStd.Text))
            If dt.Rows.Count > 0 Then
                lblCount.Text = dt.Rows.Count
                lblNo.Visible = False
                With grdStudent
                    .Visible = True
                    .DataSource = dt
                    .DataBind()
                End With
            Else
                lblCount.Text = 0
                lblNo.Visible = True
                grdStudent.Visible = False
            End If
        Else
            lblCount.Text = 0
            lblNo.Visible = True
            grdStudent.Visible = False
        End If
    End Sub

    Private Sub LoadCourseToDDL()
        Dim ctlCs As New CourseController
        ddlSubject.Items.Clear()

        If Request.Cookies("ROLE_ADM").Value = True Then
            dt = ctlCs.Courses_GetByYear(ddlYear.SelectedValue)
        Else
            If Request.Cookies("ROLE_ADV").Value = True Then
                dt = ctlCs.Courses_GetByCoordinator(ddlYear.SelectedValue, DBNull2Zero(Request.Cookies("ProfileID").Value))
            Else
                Exit Sub
            End If
        End If

        If dt.Rows.Count > 0 Then
            ddlSubject.Items.Clear()
            ddlSubject.Items.Add("---ไม่ระบุ---")
            ddlSubject.Items(0).Value = ""
            For i = 0 To dt.Rows.Count - 1
                With ddlSubject
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("NameTH"))
                    .Items(i + 1).Value = dt.Rows(i)("CourseID")
                End With
            Next
        Else
            ddlSubject.Items.Clear()
            ddlSubject.Items.Add("")
            ddlSubject.Items(0).Value = ""
        End If

    End Sub

    Private Sub LoadLocationToDDL()
        Dim ctlSL As New SkillLocationController
        dt = ctlSL.SkillLocation_GetLocationInWorkSkill(ddlYear.SelectedValue, ddlSkill.SelectedValue)
        If dt.Rows.Count > 0 Then
            With ddlLocation
                .Enabled = True
                .DataSource = dt
                .DataTextField = "LocationName"
                .DataValueField = "LocationID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadStudentNoAssessmentToGrid()
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub
    Private Sub AddStudentToCourse()
        Dim CtlR As New REQcontroller
        Dim CtlS As New StudentController
        Dim item, AsmCount, ReqCount As Integer
        Dim stdSex As String
        stdSex = ""
        AsmCount = 0
        ReqCount = 0
        Dim str() As String = Split(ddlSubject.SelectedItem.Text, " : ")

        If ddlSubject.SelectedValue = "" Then
            str(0) = ""
        End If

        For i = 0 To grdData.Rows.Count - 1
            With grdData
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                If chkS.Checked Then

                    stdSex = CtlS.Student_GetSex(.Rows(i).Cells(1).Text)

                    'จำนวนรับระบุเพศ
                    ReqCount = CtlR.REQACC_GetCount(ddlYear.SelectedValue, ddlSkill.SelectedValue, ddlPhase.SelectedValue, ddlLocation.SelectedValue, stdSex)

                    If ReqCount = 0 Then 'ถ้าระบุเพศแล้วพบว่าไม่รับ ให้ตรวจสอบแบบไม่ระบุเพศว่ารับไหม 
                        ReqCount = CtlR.REQACC_GetCountByNoSpecSex(ddlYear.SelectedValue, ddlSkill.SelectedValue, ddlPhase.SelectedValue, ddlLocation.SelectedValue)

                        AsmCount = ctlA.Assessment_GetCount(ddlYear.SelectedValue, ddlLocation.SelectedValue, ddlPhase.SelectedValue, ddlSkill.SelectedValue)

                    Else 'ถ้ารับตามเพศ
                        AsmCount = ctlA.Assessment_GetCountBySex(ddlYear.SelectedValue, ddlLocation.SelectedValue, ddlPhase.SelectedValue, stdSex, ddlSkill.SelectedValue)
                    End If


                    If AsmCount >= ReqCount Then 'ถ้าได้ครบตามจำนวนรับแล้ว 
                        LoadAssessmentToGrid()
                        LoadStudentNoAssessmentToGrid()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลจัดสรรครบตามจำนวนที่แหล่งฝึกรับแล้ว');", True)
                        Exit Sub
                    End If

                    item = ctlA.Assessment_Add(ddlYear.SelectedValue, str(0), .Rows(i).Cells(1).Text, ddlLocation.SelectedValue, ddlPhase.SelectedValue, StrNull2Zero(ddlSkill.SelectedValue), Request.Cookies("UserLogin").Value)

                    acc.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_ADD, "Assessment", "เพิ่ม จัดสรรแหล่งฝึกให้นักศึกษา by Admin:Y=" & ddlYear.SelectedValue & ">>CID=" & ddlSubject.SelectedItem.Text & ">>LID=" & ddlLocation.SelectedItem.Text & ">>TP=" & ddlPhase.SelectedItem.Text & ">>Code=" & .Rows(i).Cells(1).Text, "")

                End If


            End With
        Next
        LoadAssessmentToGrid()
        LoadStudentNoAssessmentToGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Private Sub grdStudent_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdStudent.PageIndexChanging
        grdStudent.PageIndex = e.NewPageIndex
        LoadAssessmentToGrid()
    End Sub

    Private Sub grdStudent_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdStudent.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    If ctlA.Assessment_DeleteByID(e.CommandArgument) Then
                        acc.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_DEL, "Assessment_DeleteByUID", "ลบ นักศึกษาในรายวิชาฝึก:" & ddlSubject.SelectedValue & ">>" & e.CommandArgument, "")
                        'DisplayMessage(Me, "ลบข้อมูลเรียบร้อย")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ลบข้อมูลเรียบร้อย');", True)
                        LoadAssessmentToGrid()
                        LoadStudentNoAssessmentToGrid()
                    Else
                        'DisplayMessage(Me, "ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub

    Private Sub grdStudent_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdStudent.RowDataBound
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

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        'If StrNull2Zero(ddlDegree.SelectedValue) = 0 Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาเลือกลำดับที่ก่อน');", True)
        '    Exit Sub
        'End If
        AddStudentToCourse()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        For i = 0 To grdData.Rows.Count - 1
            With grdData
                Dim chkS As CheckBox = .Rows(i).Cells(0).FindControl("chkSelect")
                chkS.Checked = False
            End With
        Next
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged

        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadCourseToDDL()
        LoadSkillToDDL()
        LoadLocationToDDL()

        LoadSkillPhase()

        LoadAssessmentToGrid()
        LoadStudentNoAssessmentToGrid()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject.SelectedIndexChanged
        LoadSkillToDDL()

        LoadLocationToDDL()
        LoadSkillPhase()

        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadAssessmentToGrid()
        LoadStudentNoAssessmentToGrid()
    End Sub

    Protected Sub cmdFindStd_Click(sender As Object, e As EventArgs) Handles cmdFindStd.Click
        grdStudent.PageIndex = 0
        LoadAssessmentToGrid()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadStudentNoAssessmentToGrid()
    End Sub
    Protected Sub lnkSubmitAdd_Click(sender As Object, e As EventArgs) Handles lnkSubmitAdd.Click
        'Dim ctlCs As New CourseController
        'dt = ctlCs.CourseStudent_GetStudentNoRegister(ddlYear.SelectedValue, ddlCourse.SelectedValue, ddlLocation.SelectedValue)
        'If dt.Rows.Count > 0 Then
        '    For i = 0 To dt.Rows.Count - 1
        '        ctlA.Assessment_AddByStudent(StrNull2Zero(ddlYear.SelectedValue), dt.Rows(i)("Student_Code"), StrNull2Zero(ddlDegree.SelectedValue), StrNull2Zero(ddlLocation.SelectedValue), ddlCourse.SelectedValue, StrNull2Zero(ddlSkill.SelectedValue), StrNull2Zero(ddlPhase.SelectedValue), Request.Cookies("UserLogin").Value)
        '    Next
        'End If

        'acc.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_ADD, "Assessment", "เพิ่ม จัดสรรแหล่งฝึกให้นักศึกษา by Admin:Y=" & ddlYear.SelectedValue & ">>CID=" & ddlCourse.SelectedItem.Text & ">>LID=" & ddlLocation.SelectedItem.Text & ">>TP=" & ddlPhase.SelectedItem.Text, "")

        'grdStudent.PageIndex = 0
        'grdData.PageIndex = 0
        'LoadAssessmentToGrid()
        'LoadStudentNoRegisterToGrid()
        'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกเรียบร้อย');", True)
    End Sub

    Protected Sub ddlSkill_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSkill.SelectedIndexChanged
        LoadLocationToDDL()
        LoadSkillPhase()
        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadAssessmentToGrid()
        LoadStudentNoAssessmentToGrid()
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        LoadSkillPhase()
        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadAssessmentToGrid()
        LoadStudentNoAssessmentToGrid()
    End Sub

    Protected Sub ddlPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPhase.SelectedIndexChanged
        grdStudent.PageIndex = 0
        grdData.PageIndex = 0
        LoadAssessmentToGrid()
        LoadStudentNoAssessmentToGrid()
    End Sub

    'Protected Sub lnkLevelDel_Click(sender As Object, e As EventArgs) Handles lnkLevelDel.Click
    '    ctlCs.CourseStudent_DeleteByLevel(ddlLevelDel.SelectedValue, ddlCourse.SelectedValue)

    '    grdStudent.PageIndex = 0
    '    grdData.PageIndex = 0
    '    LoadAssessmentToGrid()
    '    LoadStudentNoAssessmentToGrid()
    'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกเรียบร้อย');", True)
    '    DisplayMessage(Me.Page, "ลบเรียบร้อย")

    'End Sub

    'Protected Sub lnkLevelAdd_Click(sender As Object, e As EventArgs) Handles lnkLevelAdd.Click
    '    Dim ctlStd As New StudentController
    '    dt = ctlStd.GetStudent_ByLevelClass(ddlLevelAdd.SelectedValue)
    '    If dt.Rows.Count > 0 Then
    '        For i = 0 To dt.Rows.Count - 1
    '            ctlCs.CourseStudent_Add(ddlCourse.SelectedValue, dt.Rows(i)("Student_Code"), Request.Cookies("UserLogin").Value)
    '        Next
    '    End If

    '    acc.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_ADD, "CourseStudent", "เพิ่ม นักศึกษาในรายวิชาฝึกทั้งระดับชั้นปีที่:" & ddlLevelAdd.SelectedValue & ">>" & ddlCourse.SelectedItem.Text, "")

    '    grdStudent.PageIndex = 0
    '    grdData.PageIndex = 0
    '    LoadAssessmentToGrid()
    '    LoadStudentNoRegisterToGrid()
    'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกเรียบร้อย');", True)
    '    DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    'End Sub
End Class

