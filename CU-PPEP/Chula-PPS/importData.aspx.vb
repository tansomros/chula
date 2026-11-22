Imports System.IO
Imports System.Data.OleDb
Imports System.Data
Imports Subgurim.Controles

Public Class importData
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlStd As New StudentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            grdData.PageIndex = 0

            lblResult.Visible = False
            LoadStudent()
        End If

        If (FileUploaderAJAX1.IsPosting) Then
            UploadFile()
        End If
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


    Private Sub LoadStudent()

        dt = ctlStd.GetStudent

        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()

                'For i = 0 To dt.Rows.Count - 1
                '    .Rows(i).Cells(0).Text = i + 1
                'Next

            End With
        End If
    End Sub

    Protected Sub cmdImport_Click(sender As Object, e As EventArgs) Handles cmdImport.Click
        Dim connectionString As String = ""
        Try
            lblResult.Visible = True

            Dim fileName As String = Path.GetFileName("~/" & tmpUpload & "/" & Session("fname"))
            Dim fileExtension As String = Path.GetExtension("~/" & tmpUpload & "/" & Session("fname"))

            lblResult.Text = " servermap : " & "~/" & tmpUpload & "/" & fileName

            Dim fileLocation As String = Server.MapPath("~/" & tmpUpload & "/" & fileName)

            lblResult.Text &= " fileLocation : " & fileLocation

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

            lblResult.Text &= " datatable dtExcelRecords มี " & dtExcelRecords.Rows.Count & " rec."
            'Dim k As Integer = 0
            'For i = 0 To dtExcelRecords.Rows.Count - 1
            '    With dtExcelRecords.Rows(i)
            '        If .Item(0).ToString <> "" Then
            '            ctlStd.Student_Add(.Item(0).ToString, .Item(1), .Item(2))
            '            k = k + 1
            '        End If
            '    End With
            'Next
            ''grdData.DataSource = dtExcelRecords
            ''grdData.DataBind()


            'acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Students", "import รายชื่อนิสิตใหม่ : " & k & " คน", "จากไฟล์ excel")

            'lblResult.Text = "ข้อมูลทั้งหมด " & k & "เรคอร์ด ถูก import เรียบร้อย"
            lblResult.Visible = True
            dtExcelRecords = Nothing


        Catch ex As Exception
            DisplayMessage(Me.Page, "Error : " & ex.Message)
        End Try
    End Sub
    Dim acc As New UserController
    Protected Sub cmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdSave.Click

        If txtCode.Text = "" Or txtFirstName.Text = "" Or txtLastName.Text = "" Then
            DisplayMessage(Me.Page, "กรุณป้อนข้อมูลให้ครบถ้วน")
            Exit Sub
        End If

        ctlStd.Student_Add(txtCode.Text, txtFirstName.Text, txtLastName.Text)

        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Students", "เพิ่มรายชื่อนิสิตใหม่ :" & txtFirstName.Text & " " & txtLastName.Text, "เพิ่มแบบทีละคน")

        DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")
        LoadStudent()
    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadStudent()
    End Sub

End Class