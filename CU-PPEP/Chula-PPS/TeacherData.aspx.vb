Imports System.IO
Imports System.Data.OleDb
Imports System.Data
Imports Subgurim.Controles

Public Class TeacherData
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlStd As New PersonController
    Dim ctlDept As New DepartmentController
    Dim objUser As New UserController
    Dim ctlbase As New ApplicationBaseClass


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            grdData.PageIndex = 0
            isAdd = True
            ClearData()
            UpdateProgress1.Visible = False
            lblResult.Visible = False
            LoadPositionToDDL()
            LoadPrefixToDDL()
            LoadDepartmentToDDL()
            LoadTeacher()
        End If

        If (FileUploaderAJAX1.IsPosting) Then
            UploadFile()
        End If
    End Sub

    Private Sub LoadPrefixToDDL()
        dt = ctlbase.Prefix_GetAll
        If dt.Rows.Count > 0 Then
            With ddlPrefix
                .Enabled = True
                .DataSource = dt
                .DataTextField = "PrefixName"
                .DataValueField = "PrefixID"
                .DataBind()
                '.SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub LoadDepartmentToDDL()
        dt = ctlDept.Department_GetActive
        If dt.Rows.Count > 0 Then
            With ddlDepartment
                .Enabled = True
                .DataSource = dt
                .DataTextField = "DepartmentName"
                .DataValueField = "DepartmentUID"
                .DataBind()
                '.SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub


    Private Sub UploadFile()

        Dim pf As HttpPostedFileAJAX = FileUploaderAJAX1.PostedFile

        'กรณีต้องการต้องสอบชนิด file
        ' If pf.ContentType.Equals("application/vnd.ms-excel") Then
        Try
            FileUploaderAJAX1.SaveAs("~/" & tmpUpload, pf.FileName)

            Session("fname") = pf.FileName
        Catch ex As Exception
            DisplayMessage(Me.Page, "ไม่สามารถอัปโหลดรได้ กรุณาลองใหม่ภายหลัง เนื่องจาก " & ex.Message)
        End Try

        ' Else

        ' End If

    End Sub

    Private Sub LoadTeacher()

        If Trim(txtSearch.Text) <> "" Then
            dt = ctlStd.Person_GetSearch("T", txtSearch.Text)
        Else
            dt = ctlStd.Person_GetByType("T")
        End If


        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()

                'For i = 0 To dt.Rows.Count - 1
                '    .Rows(i).Cells(0).Text = i + 1
                'Next
                lblStudentCount.Text = "พบรายชื่อทั้งหมด " & dt.Rows.Count & " คน"


            End With
        Else
            grdData.Visible = False
            lblStudentCount.Text = "ไม่พบรายชื่อที่ท่านค้นหา"
        End If
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
            Dim k As Integer = 0
            For i = 0 To dtExcelRecords.Rows.Count - 1
                With dtExcelRecords.Rows(i)
                    If .Item(0).ToString <> "" Then
                        ctlStd.Person_Add(25, .Item(0), .Item(1), .Item(2), .Item(3), .Item(4), "", "T", "Y")
                        k = k + 1
                    End If
                End With
            Next

            'grdData.DataSource = dtExcelRecords
            'grdData.DataBind()


            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Teachers", "import รายชื่ออาจารย์ใหม่ : " & k & " คน", "จากไฟล์ excel")
            UpdateProgress1.Visible = False

            lblResult.Text = "ข้อมูลทั้งหมด " & k & "เรคอร์ด ถูก import เรียบร้อย"
            lblResult.Visible = True

            LoadTeacher()
        Catch ex As Exception
            DisplayMessage(Me.Page, "Error : " & ex.Message)
        End Try

    End Sub
    Dim acc As New UserController

    'Function chkDup() As Boolean
    '    dt = ctlStd.Person_GetByID(StrNull2Zero(lblID.Text))
    '    If dt.Rows.Count > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function


    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtFirstName.Text = "" Or txtLastName.Text = "" Then
            DisplayMessage(Me.Page, "กรุณป้อนข้อมูลให้ครบถ้วน")
            Exit Sub
        End If

        If lblID.Text = "" Then

            ctlStd.Teacher_Add(ddlPrefix.SelectedValue, txtFirstName.Text, txtLastName.Text, StrNull2Zero(ddlPosition.SelectedValue), ddlPosition.SelectedItem.Text, StrNull2Zero(ddlDepartment.SelectedValue), ddlDepartment.SelectedItem.Text, ConvertBoolean2StatusFlag(chkActive.Checked))

            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Persons", "เพิ่มรายชื่ออาจารย์ใหม่ :" & txtFirstName.Text & " " & txtLastName.Text, "เพิ่มแบบทีละคน")
        Else
            ctlStd.Teacher_UpdateSmall(StrNull2Zero(lblID.Text), ddlPrefix.SelectedValue, txtFirstName.Text, txtLastName.Text, StrNull2Zero(ddlPosition.SelectedValue), ddlPosition.SelectedItem.Text, StrNull2Zero(ddlDepartment.SelectedValue), ddlDepartment.SelectedItem.Text, Request.Cookies("Username").Value, ConvertBoolean2StatusFlag(chkActive.Checked))
            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Persons", "แก้ไขชื่ออาจารย์ใหม่ :" & txtFirstName.Text & " " & txtLastName.Text, "เพิ่มแบบทีละคน")

        End If

        DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")
        isAdd = True
        LoadTeacher()
        ClearData()
    End Sub
    Private Sub ClearData()
        txtFirstName.Text = ""
        txtLastName.Text = ""
        ddlPrefix.SelectedIndex = 0
        lblID.Text = ""
        ddlDepartment.SelectedIndex = 0
        ddlPosition.SelectedIndex = 0
        lblMode.Text = "Mode : Add New"
        chkActive.Checked = True
    End Sub
    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadTeacher()
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If ctlStd.Person_Delete(e.CommandArgument) Then

                        objUser.User_GenLogfile(Request.Cookies("Username").Value, "DEL", "Persons", "ลบอาจารย์ :" & e.CommandArgument, "")

                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If

                    LoadTeacher()

            End Select

        End If
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(5).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub
     
    Private Sub EditData(ByVal pID As String)
        ClearData()

        dt = ctlStd.Person_GetByID(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                lblID.Text = .Item("PersonID")

                If String.Concat(.Item("PrefixID")) <> "" Then
                    Me.ddlPrefix.SelectedValue = String.Concat(.Item("PrefixID"))
                End If

                Me.txtFirstName.Text = String.Concat(.Item("FirstName"))
                txtLastName.Text = String.Concat(.Item("LastName"))
                ddlPosition.SelectedValue = String.Concat(.Item("PositionID"))
                ddlDepartment.SelectedValue = String.Concat(.Item("DepartmentUID"))
                chkActive.Checked = ConvertStatusFlag2Boolean(String.Concat(.Item("StatusFlag")))
                lblMode.Text = "Mode : Edit"
            End With

        End If

        dt = Nothing
    End Sub
    Private Sub LoadPositionToDDL()
        dt = ctlbase.Position_GetActive
        If dt.Rows.Count > 0 Then
            With ddlPosition
                .Enabled = True
                .DataSource = dt
                .DataTextField = "PositionName"
                .DataValueField = "PositionID"
                .DataBind()
                '.SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadTeacher()
    End Sub

    Protected Sub cmdAll_Click(sender As Object, e As EventArgs) Handles cmdAll.Click
        grdData.PageIndex = 0
        txtSearch.Text = ""
        LoadTeacher()
    End Sub
End Class