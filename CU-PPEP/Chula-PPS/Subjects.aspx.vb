
Public Class Subjects
    Inherits System.Web.UI.Page

    Dim ctlLG As New SubjectController
    Dim dt As New DataTable 
    Dim ctlFct As New FacultyController
    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            ClearData()
            lblID.Text = ""
            ' LoadMajorToDDL()
            grdData.PageIndex = 0
            LoadSubjectToGrid()
        End If

        txtCode.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        txtLevelClass.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub
    Private Sub LoadSubjectToGrid()

        If Trim(txtSearch.Text) <> "" Then
            dt = ctlLG.Subject_GetBySearch(txtSearch.Text)
        Else
            dt = ctlLG.Subject_Get
        End If


        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()


        End With

    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If ctlLG.Subject_Delete(e.CommandArgument) Then
                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "Subject", "ลบรายวิชา:" & txtCode.Text & ">>" & txtNameTH.Text, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        grdData.PageIndex = 0
                        LoadSubjectToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If
            End Select


        End If
    End Sub

    'Private Sub LoadMajorToDDL()
    '    dt = ctlFct.GetMajor
    '    If dt.Rows.Count > 0 Then
    '        With optMajor
    '            .Enabled = True
    '            .DataSource = dt
    '            .DataTextField = "MajorName"
    '            .DataValueField = "MajorID"
    '            .DataBind()
    '            .SelectedIndex = 0
    '        End With
    '    Else

    '    End If
    '    dt = Nothing
    'End Sub

    Private Sub EditData(ByVal pID As String)
        dt = ctlLG.Subject_GetByCode(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.lblID.Text = DBNull2Str(dt.Rows(0)("SubjectCode"))
                Me.txtCode.Text = DBNull2Str(dt.Rows(0)("SubjectCode"))
                txtNameTH.Text = DBNull2Str(dt.Rows(0)("NameTH"))
                txtNameEN.Text = DBNull2Str(dt.Rows(0)("NameEN"))
                txtUnit.Text = DBNull2Str(dt.Rows(0)("SubjectUnit"))
                'optMajor.SelectedValue = DBNull2Str(dt.Rows(0)("05_MajorID")
                chkStatus.Checked = CBool(dt.Rows(0)("isPublic"))
                txtAliasName.Text = DBNull2Str(dt.Rows(0)("AliasName"))
                txtLevelClass.Text = DBNull2Str(dt.Rows(0)("LevelClass"))

                txtNameCert1EN.Text = DBNull2Str(dt.Rows(0)("NameCert1EN"))
                txtNameCert1TH.Text = DBNull2Str(dt.Rows(0)("NameCert1TH"))
                txtNameCert2EN.Text = DBNull2Str(dt.Rows(0)("NameCert2EN"))
                txtNameCert2TH.Text = DBNull2Str(dt.Rows(0)("NameCert2TH"))


            End With
        End If
        dt = Nothing
    End Sub
    Private Sub ClearData()
        Me.lblID.Text = ""
        Me.txtCode.Text = ""
        txtNameTH.Text = ""
        txtNameEN.Text = ""
        txtAliasName.Text = ""
        chkStatus.Checked = True
        txtUnit.Text = ""
        txtLevelClass.Text = ""
        txtNameCert1EN.Text = ""
        txtNameCert1TH.Text = ""
        txtNameCert2EN.Text = ""
        txtNameCert2TH.Text = ""

    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript: Return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(8).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtNameTH.Text = "" Or txtCode.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If
        'If optMajor.SelectedIndex = -1 Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาเลือกสาขาวิชาก่อน');", True)
        '    Exit Sub
        'End If

        Dim item As Integer

        If lblID.Text = "" Then
            If ctlLG.Subject_Duplicate(txtCode.Text) = True Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','รหัสวิชาซ้ำ รหัสวิชานี้มีในฐานข้อมูลแล้ว ');", True)
                Exit Sub
            End If


            item = ctlLG.Subject_Add(txtCode.Text, txtNameTH.Text, txtNameEN.Text, StrNull2Double(txtUnit.Text), "", Request.Cookies("Username").Value, Boolean2Decimal(chkStatus.Checked), txtAliasName.Text, StrNull2Zero(txtLevelClass.Text), txtNameCert1TH.Text, txtNameCert2TH.Text, txtNameCert1EN.Text, txtNameCert2EN.Text)

            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Subject", "เพิ่มใหม่ รายวิชา:" & txtCode.Text, "Result:" & item)

        Else
            item = ctlLG.Subject_Update(lblID.Text, txtCode.Text, txtNameTH.Text, txtNameEN.Text, StrNull2Double(txtUnit.Text), "", Request.Cookies("Username").Value, Boolean2Decimal(chkStatus.Checked), txtAliasName.Text, StrNull2Zero(txtLevelClass.Text), txtNameCert1TH.Text, txtNameCert2TH.Text, txtNameCert1EN.Text, txtNameCert2EN.Text)

            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Subject", "แก้ไข รายวิชา:" & txtCode.Text, "Result:" & item)

        End If

        grdData.PageIndex = 0
        LoadSubjectToGrid()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)


    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadSubjectToGrid()
    End Sub
    Protected Sub cmdAll_Click(sender As Object, e As EventArgs) Handles cmdAll.Click
        txtSearch.Text = ""
        grdData.PageIndex = 0
        LoadSubjectToGrid()
    End Sub

    Protected Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadSubjectToGrid()
    End Sub
End Class

