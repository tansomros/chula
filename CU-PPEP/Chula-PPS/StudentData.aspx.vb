Imports System.IO
Imports System.Data.OleDb
Imports System.Data
Imports Subgurim.Controles

Public Class StudentData
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlStd As New StudentController
    Dim objUser As New UserController
    Dim ctlFct As New FacultyController
    Dim ctlPsn As New PersonController

    Dim objStd As New StudentInfo


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            grdData.PageIndex = 0
            isAdd = True
            lblResult.Visible = False
            ClearData()

            'ddlMinor.Enabled = False
            'ddlSubMinor.Enabled = False
            LoadStatusToDDL()

            LoadMajorToDDL()
            LoadAdvisorToDDL()
            LoadStudent()
        End If

        If (FileUploaderAJAX1.IsPosting) Then
            UploadFile()
        End If
    End Sub
    Private Sub LoadStatusToDDL()

        dt = ctlStd.StudentStatus_GetAll()

        ddlStudentStatus.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlStudentStatus
                .Enabled = True
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & dt.Rows(i)("StatusCode") & " " & dt.Rows(i)("StatusName"))
                    .Items(i).Value = dt.Rows(i)("StatusCode")
                Next

                .SelectedIndex = 0

            End With

        End If
    End Sub

    Private Sub LoadAdvisorToDDL()
         
            dt = ctlPsn.Person_GetByType("T")
        
        ddlAdvisor.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlAdvisor
                .Enabled = True
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & dt.Rows(i)("FirstName") & " " & dt.Rows(i)("LastName"))
                    .Items(i).Value = dt.Rows(i)("PersonID")
                Next

                .SelectedIndex = 0

            End With

        End If
    End Sub


    Private Sub LoadMajorToDDL()
        Dim dtMajor As New DataTable
        dtMajor = ctlFct.GetMajor
        If dtMajor.Rows.Count > 0 Then
            With ddlMajor
                .Enabled = True
                .DataSource = dtMajor
                .DataTextField = "MajorName"
                .DataValueField = "MajorID"
                .DataBind()
                .SelectedIndex = 0
            End With
        Else

        End If
        dtMajor = Nothing
    End Sub

    Private Sub LoadMinorToDDL()
        Dim dtMinor As New DataTable
        dtMinor = ctlFct.GetMinor_ByMajorID(ddlMajor.SelectedValue)
        If dtMinor.Rows.Count > 0 Then
            With ddlMinor
                .Enabled = True
                .DataSource = dtMinor
                .DataTextField = "MinorName"
                .DataValueField = "MinorID"
                .DataBind()
                .SelectedIndex = 0
            End With
        Else
            ddlMinor.Items.Clear()
            ddlMinor.DataSource = Nothing
            ddlMinor.Enabled = False
        End If
        dtMinor = Nothing
    End Sub

    Protected Sub ddlMajor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMajor.SelectedIndexChanged
        LoadMinorToDDL()
    End Sub

    Private Sub UploadFile()

        Dim pf As HttpPostedFileAJAX = FileUploaderAJAX1.PostedFile

        'กรณีต้องการต้องสอบชนิด file
        ' If pf.ContentType.Equals("application/vnd.ms-excel") Then
        Try
            Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & tmpUpload & "/" & pf.FileName))
            If objfile.Exists Then
                objfile.Delete()
            End If

            FileUploaderAJAX1.SaveAs("~/" & tmpUpload, pf.FileName)

            Session("fname") = pf.FileName
        Catch ex As Exception
            DisplayMessage(Me.Page, "ไม่สามารถอัปโหลดรได้ กรุณาลองใหม่ภายหลัง เนื่องจาก " & ex.Message)
        End Try

        ' Else

        ' End If

    End Sub

    Private Sub LoadStudent()

        dt = ctlStd.Student_GetByStatus(optStatus.SelectedValue, txtSearch.Text)
        'If dt.Rows.Count > 0 Then
        With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()

            End With

            lblStudentCount.Text = "พบรายชื่อนิสิตทั้งหมด " & dt.Rows.Count.ToString("#,###") & " คน"
        'Else
        '    grdData.DataSource = Nothing

        'End If
    End Sub

    Protected Sub cmdImport_Click(sender As Object, e As EventArgs) Handles cmdImport.Click
        System.Threading.Thread.Sleep(3000)
        UpdateProgress1.Visible = True

        Dim connectionString As String = ""
        Try

            lblResult.Visible = False

            Dim fileName As String = Path.GetFileName("~/" & tmpUpload & "/" & Session("fname"))
            Dim fileExtension As String = Path.GetExtension("~/" & tmpUpload & "/" & Session("fname"))

            Dim fileLocation As String = Server.MapPath("~/" & tmpUpload & "/" & fileName)

            'Check whether file extension is xls or xslx

            If fileExtension = ".xls" Then
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fileLocation & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=2"""
            ElseIf fileExtension = ".xlsx" Then
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fileLocation & ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=2"""
            End If

            'Create OleDB Connection and OleDb Command

            Dim con As New OleDbConnection(connectionString)
            Dim cmd As New OleDbCommand()
            cmd.CommandType = System.Data.CommandType.Text
            cmd.Connection = con
            Dim dAdapter As New OleDbDataAdapter(cmd)
            Dim dtExcelRecords As New DataTable()
            con.Open()
            Dim dtExcelSheetName As DataTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            Dim getExcelSheetName As String = dtExcelSheetName.Rows(0)("Table_Name").ToString()
            cmd.CommandText = "SELECT * FROM [" & getExcelSheetName & "]"
            dAdapter.SelectCommand = cmd
            dAdapter.Fill(dtExcelRecords)
            con.Close()
            Dim strCode As String = ""
            Dim k As Integer = 0
            For i = 0 To dtExcelRecords.Rows.Count - 1
                With dtExcelRecords.Rows(i)
                    If .Item(0).ToString <> "" Then
                        If ctlStd.Student_CheckDuplicateByCode(String.Concat(.Item(0))) Then
                            strCode = strCode & String.Concat(.Item(0)) & ","
                        Else
                            ctlStd.Student_AddByImport(.Item(0).ToString, .Item(1), .Item(2), DBNull2Zero(.Item(3)), DBNull2Zero(.Item(4)), DBNull2Zero(.Item(5)), DBNull2Zero(.Item(6)), DBNull2Dbl(.Item(7)), DBNull2Zero(.Item(8)), "10")
                            k = k + 1

                        End If
                    End If
                End With
            Next
            'grdData.DataSource = dtExcelRecords
            'grdData.DataBind()

            Dim objfile As FileInfo = New FileInfo(fileLocation)
            If objfile.Exists Then
                objfile.Delete()
            End If

            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Students", "import รายชื่อนิสิตใหม่ : " & k & " คน", "จากไฟล์ excel")

            lblResult.Text = "ข้อมูลทั้งหมด " & k & " รายการ ถูก import เรียบร้อย" & IIf(strCode <> "", vbCrLf & " รหัส " & strCode & " ไม่สามารถเพิ่มได้เนื่องจากมีในฐานข้อมูลแล้ว ", "")
            lblResult.Visible = True
            dtExcelRecords = Nothing

            LoadStudent()
            UpdateProgress1.Visible = False
        Catch ex As Exception
            DisplayMessage(Me.Page, "Error : " & ex.Message)
        End Try
    End Sub
    Dim acc As New UserController

    Function chkDup() As Boolean
        dt = ctlStd.Student_GetByCode(txtCode.Text)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtCode.Text = "" Or txtFirstName.Text = "" Or txtLastName.Text = "" Then
            DisplayMessage(Me.Page, "กรุณป้อนข้อมูลให้ครบถ้วน")
            Exit Sub
        End If

        If Not chkDup() Then
            If lblStdCode.Text = "" Then
                ctlStd.Student_Add(txtCode.Text, txtFirstName.Text, txtLastName.Text, StrNull2Zero(ddlMajor.SelectedValue), StrNull2Zero(ddlMinor.SelectedValue), 0, StrNull2Zero(ddlAdvisor.SelectedValue), StrNull2Double(txtGPAX.Text), StrNull2Zero(txtLevelClass.Text), ddlStudentStatus.SelectedValue.ToString())

                acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Students", "เพิ่มรายชื่อนิสิตใหม่ :" & txtFirstName.Text & " " & txtLastName.Text, "เพิ่มแบบทีละคน")
            Else
                ctlStd.Student_UpdateSmall(lblStdCode.Text, txtCode.Text, txtFirstName.Text, txtLastName.Text, StrNull2Zero(ddlMajor.SelectedValue), StrNull2Zero(ddlMinor.SelectedValue), 0, StrNull2Zero(ddlAdvisor.SelectedValue), StrNull2Double(txtGPAX.Text), StrNull2Zero(txtLevelClass.Text), Request.Cookies("Username").Value, ddlStudentStatus.SelectedValue.ToString())

                acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Students", "แก้ไขข้อมูลนิสิต :" & txtFirstName.Text & " " & txtLastName.Text, "เพิ่มแบบทีละคน")
            End If

        Else
            ctlStd.Student_UpdateSmall(lblStdCode.Text, txtCode.Text, txtFirstName.Text, txtLastName.Text, StrNull2Zero(ddlMajor.SelectedValue), StrNull2Zero(ddlMinor.SelectedValue), 0, StrNull2Zero(ddlAdvisor.SelectedValue), StrNull2Double(txtGPAX.Text), StrNull2Zero(txtLevelClass.Text), Request.Cookies("Username").Value, ddlStudentStatus.SelectedValue.ToString())

            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Students", "แก้ไขข้อมูลนิสิต :" & txtFirstName.Text & " " & txtLastName.Text, "เพิ่มแบบทีละคน")
        End If

        isAdd = True


        DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")
        LoadStudent()
    End Sub
    Private Sub ClearData()
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtCode.Text = ""
        isAdd = True
        lblStdCode.Text = ""
        txtGPAX.Text = ""

    End Sub
    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadStudent()
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgView"
                    'Response.Redirect("Student_Bio.aspx?ActionType=std&std=" & e.CommandArgument())
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Download", "window.location='Student_Bio.aspx?ActionType=std&stdcode=" & e.CommandArgument() & "';", True)

                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If ctlStd.Student_Delete(e.CommandArgument) Then

                        objUser.User_GenLogfile(Request.Cookies("Username").Value, "DEL", "Student", "ลบนิสิต :" & e.CommandArgument, "")

                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If

                    LoadStudent()

            End Select

        End If
    End Sub
    Private Sub EditData(ByVal pID As String)
     
        ClearData()

        dt = ctlStd.Student_GetByCode(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                lblStdCode.Text = .Item("Student_Code")
                Me.txtCode.Text = .Item("Student_Code")
                Me.txtFirstName.Text = String.Concat(.Item("FirstName"))
                txtLastName.Text = String.Concat(.Item("LastName"))

                If Not IsDBNull(.Item("MajorID")) Then
                    ddlMajor.SelectedValue = .Item("MajorID")
                End If
                If Not IsDBNull(.Item("MinorID")) Then
                    LoadMinorToDDL()
                    If .Item("MinorID").ToString <> "0" Then
                        ddlMinor.SelectedValue = .Item("MinorID")
                    End If
                End If

                If Not IsDBNull(.Item("AdvisorID")) Then
                    ddlAdvisor.SelectedValue = .Item("AdvisorID")
                End If

                txtGPAX.Text = DBNull2Dbl(.Item("GPAX"))
                If Not IsDBNull(.Item("StudentStatus")) Then
                    ddlStudentStatus.SelectedValue = .Item("StudentStatus")
                    If ddlStudentStatus.SelectedValue = "40" Then
                        txtLevelClass.Text = ""
                    Else
                        txtLevelClass.Text = String.Concat(.Item("LevelClass"))
                    End If
                End If

            End With

            ' txtCode.ReadOnly = True

        End If

        dt = Nothing
    End Sub
     
    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(8).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadStudent()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdAll_Click(sender As Object, e As EventArgs) Handles cmdAll.Click
        txtSearch.Text = ""
        optStatus.SelectedIndex = 0
        LoadStudent()
    End Sub

End Class