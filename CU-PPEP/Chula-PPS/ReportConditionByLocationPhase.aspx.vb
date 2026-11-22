Imports Microsoft.Reporting.WebForms
Public Class ReportConditionByLocationPhase
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlCs As New CourseController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblResult.Visible = False
            LoadYearToDDL()
            LoadTurnPhaseToDDL()
            LoadLocationToDDL()
        End If
    End Sub
    Private Sub LoadTurnPhaseToDDL()
        Dim ctlTP As New TimePhaseController

        dt = ctlTP.TurnPhase_GetByYear(StrNull2Zero(ddlYear.SelectedValue))

        If dt.Rows.Count > 0 Then

            ddlPhase.Items.Clear()
            ddlPhase.Items.Add("---ทั้งหมด---")
            ddlPhase.Items(0).Value = 0

            For i = 1 To dt.Rows.Count
                With ddlPhase
                    .Items.Add(dt.Rows(i - 1)("PhaseDesc"))
                    .Items(i).Value = dt.Rows(i - 1)("PhaseID")
                End With
            Next

            With ddlPhase
                '.Visible = True
                '.DataSource = dt
                '.DataTextField = "Name"
                '.DataValueField = "Code"
                '.DataBind()
                .SelectedIndex = 0
            End With
        Else
            ddlPhase.DataSource = dt
            ddlPhase.DataBind()
        End If
    End Sub

    Private Sub LoadLocationToDDL()
        Dim ctlL As New LocationController

        ddlLocation.Items.Clear()
        dt = ctlL.Location_GetByPhase(StrNull2Zero(ddlYear.Text), StrNull2Zero(ddlPhase.SelectedValue))

        If dt.Rows.Count > 0 Then

            ddlLocation.Items.Clear()
            ddlLocation.Items.Add("---ทั้งหมด---")
            ddlLocation.Items(0).Value = 0

            For i = 1 To dt.Rows.Count
                With ddlLocation
                    .Items.Add(dt.Rows(i - 1)("LocationName"))
                    .Items(i).Value = dt.Rows(i - 1)("LocationID")
                End With
            Next
            ddlLocation.SelectedIndex = 0
        Else
            ddlLocation.DataSource = dt
            ddlLocation.DataBind()
        End If

        dt = Nothing
    End Sub


    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlCs.Courses_GetYear
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "YearText"
                .DataValueField = "YearValue"
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

    Protected Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        FagRPT = "PDF"
        FagRPT = "SWOT"
        Reportskey = "PDF"
        ReportName = "StudentSWOT"
        'Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&p=" & ddlPhase.SelectedValue & "&lid=" & ddlLocation.SelectedValue)
        Dim url As String = "ReportServerViewer.aspx?y=" & ddlYear.SelectedValue &
                    "&p=" & ddlPhase.SelectedValue &
                    "&lid=" & ddlLocation.SelectedValue

        ' สั่งเปิดหน้าใหม่ด้วย JavaScript
        Dim script As String = "window.open('" & url & "', '_blank');"
        ClientScript.RegisterStartupScript(Me.GetType(), "OpenReport", script, True)
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadTurnPhaseToDDL()
        UpdatePanel1.Visible = False
    End Sub

    Private Sub ddlPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPhase.SelectedIndexChanged
        LoadLocationToDDL()
        UpdatePanel1.Visible = False
    End Sub

    Private Sub LoadReport()

        System.Threading.Thread.Sleep(1000)
        UpdateProgress1.Visible = True


        Dim credential As New ReportServerCredentials
        ReportViewer1.Reset()
        ReportViewer1.ServerReport.ReportServerCredentials = credential
        ReportViewer1.ServerReport.ReportServerUrl = credential.ReportServerUrl
        ReportViewer1.ShowPrintButton = True

        'Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)

        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("th-TH")

        ReportViewer1.ServerReport.ReportPath = credential.ReportPath("StudentSWOT")

        Dim xParam As New List(Of ReportParameter)

        xParam.Add(New ReportParameter("EduYear", ddlYear.SelectedValue))
        xParam.Add(New ReportParameter("PhaseID", ddlPhase.SelectedValue))
        xParam.Add(New ReportParameter("LocationID", ddlLocation.SelectedValue))

        Reportskey = "PDF"

        ReportViewer1.ServerReport.SetParameters(xParam)

        If Reportskey = "WRD" Then
            ' Variables
            Dim warnings As Warning()
            Dim streamIds As String()
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty
            Dim extension As String = String.Empty


            ' Setup the report viewer object and get the array of bytes

            Dim bytes As Byte() = ReportViewer1.ServerReport.Render("WORD", Nothing, mimeType, encoding, extension, streamIds, Nothing)

            ' Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", Convert.ToString("attachment; filename=" & Request("id") & "_" & ConvertStrDate2DBString(Now.Date()) & "." & extension))
            Response.BinaryWrite(bytes)
            ' create the file
            Response.Flush()
        ElseIf Reportskey = "XLS" Then
            ' Variables
            Dim warnings As Warning()
            Dim streamIds As String()
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty
            Dim extension As String = String.Empty


            ' Setup the report viewer object and get the array of bytes

            Dim bytes As Byte() = ReportViewer1.ServerReport.Render("EXCEL", Nothing, mimeType, encoding, extension, streamIds, Nothing)

            If FagRPT = "DOC" Then

                Dim oFileStream As System.IO.FileStream
                Dim strFilePath As String = String.Empty
                Dim ctlDoc As New DocumentController
                Dim dtDoc As New DataTable
                Dim DocPath, FileDataName As String

                dtDoc = ctlDoc.Document_GetByUID(StrNull2Zero(Request("d")))

                DocPath = dtDoc.Rows(0)("DocumentPath")
                FileDataName = dtDoc.Rows(0)("FileDataName")

                strFilePath = DocPath & "" & FileDataName & ".xls"

                'If File.Exists(strFilePath) Then
                '    File.Delete(strFilePath)
                'End If

                'oFileStream = New System.IO.FileStream(strFilePath, System.IO.FileMode.Create)
                'oFileStream.Write(bytes, 0, bytes.Length)
                'oFileStream.Close()

                Response.AddHeader("content-disposition", Convert.ToString("attachment; filename=" & FileDataName & ".xls"))

                Response.BinaryWrite(bytes)
                Response.Flush()

                'Response.Redirect("DocumentList.aspx?action=Y")
            Else
                ' Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType

                Response.AddHeader("content-disposition", Convert.ToString("attachment; filename=CUPPEP_" & FagRPT & "_" & Request("y") & "." & extension))

                Response.BinaryWrite(bytes)
                ' create the file
                Response.Flush()
            End If
        Else
            ' Variables
            Dim warnings As Warning()
            Dim streamIds As String()
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty
            Dim extension As String = String.Empty

            ' Setup the report viewer object and get the array of bytes

            Dim bytes As Byte() = ReportViewer1.ServerReport.Render("PDF", Nothing, mimeType, encoding, extension, streamIds, Nothing)

            ' Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            'Response.AddHeader("content-disposition", Convert.ToString("attachment; filename=C:\ttt.pdf"))
            Response.BinaryWrite(bytes)
            ' create the file
            'Response.Flush()
            ' send it to the client to download
        End If

    End Sub

End Class

