Imports System.IO
Imports System.Data.OleDb
Imports System.Data
Imports Subgurim.Controles

Public Class Student
    Inherits System.Web.UI.Page

    Public dtStd As New DataTable
    Dim ctlStd As New StudentController
    'Dim objUser As New UserController
    'Dim ctlFct As New FacultyController
    'Dim ctlPsn As New PersonController
    Dim ctlU As New UserController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            lblResult.Visible = False
            LoadStudent()
        End If

        If (FileUploaderAJAX1.IsPosting) Then
            UploadFile()
        End If
    End Sub
    Dim objPsn As New PersonInfo

    Private Sub UploadFile()

        Dim pf As HttpPostedFileAJAX = FileUploaderAJAX1.PostedFile

        'กรณีต้องการต้องสอบชนิด file
        ' If pf.ContentType.Equals("application/vnd.ms-excel") Then
        Try
            FileUploaderAJAX1.SaveAs("~/" & tmpUpload, pf.FileName)

            Session("fname") = pf.FileName
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถอัปโหลดรได้ กรุณาลองใหม่ภายหลัง เนื่องจาก " & ex.Message & "');", True)
        End Try

        ' Else

        ' End If

    End Sub

    Private Sub LoadStudent()
        dtStd = ctlStd.Student_GetAll
    End Sub

    Protected Sub cmdImport_Click(sender As Object, e As EventArgs) Handles cmdImport.Click
        System.Threading.Thread.Sleep(3000)

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
                        ctlStd.Student_Add(.Item(0).ToString, .Item(1), .Item(2), .Item(3), .Item(4), .Item(5), .Item(6), .Item(7), .Item(8), "10")
                        k = k + 1
                    End If
                End With
            Next
            'grdData.DataSource = dtExcelRecords
            'grdData.DataBind()


            ctlU.User_GenLogfile(Request.Cookies("UserLogin").Value, ACTTYPE_ADD, "Students", "import รายชื่อเภสัชกรประจำบ้านใหม่ : " & k & " คน", "จากไฟล์ excel")

            lblResult.Text = "ข้อมูลทั้งหมด " & k & "เรคอร์ด ถูก import เรียบร้อย"
            lblResult.Visible = True
            dtExcelRecords = Nothing

            LoadStudent()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','Error : " & ex.Message & "');", True)
        End Try
    End Sub
End Class