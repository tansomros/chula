Imports System.IO
Imports System.Data.OleDb
Imports Subgurim.Controles
Public Class VPNList
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlV As New VPNController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadVPNAccount()
        If Not IsPostBack Then
            'grdVPN.SettingsEditing.Mode = GridViewEditingMode.Inline
        End If
        If (FileUploaderAJAX1.IsPosting) Then
            UploadFile()
        End If
    End Sub
    Private Sub LoadVPNAccount()
        dt = ctlV.VPNAccount_Get
        grdVPN.DataSource = dt
        grdVPN.DataBind()
    End Sub
    Protected Sub grdVPN_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles grdVPN.RowUpdating

        Dim LocationID As Integer
        LocationID = e.Keys(0)
        ctlV.VPNAccount_Save(LocationID, e.NewValues.Item(0).ToString(), e.NewValues.Item(1).ToString(), "A")
        'grdVPN.EndUpdate()
        e.Cancel = True
        grdVPN.StartEdit(-1)
        LoadVPNAccount()
    End Sub
    Private Sub UploadFile()

        Dim pf As HttpPostedFileAJAX = FileUploaderAJAX1.PostedFile

        'กรณีต้องการต้องสอบชนิด file
        ' If pf.ContentType.Equals("application/vnd.ms-excel") Then
        Try
            FileUploaderAJAX1.SaveAs("~/" & tmpUpload, pf.FileName)

            Session("vpnname") = pf.FileName
        Catch ex As Exception
            DisplayMessage(Me.Page, "ไม่สามารถอัปโหลดรได้ กรุณาลองใหม่ภายหลัง เนื่องจาก " & ex.Message)
        End Try

        ' Else

        ' End If

    End Sub

    Protected Sub cmdImport_Click(sender As Object, e As EventArgs) Handles cmdImport.Click
        System.Threading.Thread.Sleep(3000)

        Dim connectionString As String = ""
        Try

            lblResult.Visible = False

            Dim fileName As String = Path.GetFileName("~/" & tmpUpload & "/" & Session("vpnname"))
            Dim fileExtension As String = Path.GetExtension("~/" & tmpUpload & "/" & Session("vpnname"))

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
                        ctlV.VPNAccount_Save(.Item(0).ToString, .Item(1), .Item(2), "A")
                        k = k + 1
                    End If
                End With
            Next

            lblResult.Text = "ข้อมูลทั้งหมด " & k & "เรคอร์ด ถูก import เรียบร้อย"
            lblResult.Visible = True
            dtExcelRecords = Nothing
            LoadVPNAccount()
        Catch ex As Exception
            DisplayMessage(Me.Page, "Error : " & ex.Message)
        End Try
    End Sub

End Class