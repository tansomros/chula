Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing

Public Class ReportViewer
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    'Public Shared ReportFormula As String

    'Private crRpt As New ReportDocument()

    'Public Shared FileName As String
    'Public Shared SelectionFomula As String
    'Public Shared ReportTitle As String
    'Public Shared ReportName As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ListAllPrinters()
        End If
        'If Request.Browser.Browser = "Firefox" Then
        '    Panel1.Visible = True
        'Else
        'InitRptPreview()

        'End If

        lblReportName.Text = ReportTitle

        Try

            If chkFileExist(Server.MapPath("~/" + FileName)) Then
                SelecttionRPT()
            Else
                lblAlert.Text = "ไม่พบไฟล์รายงาน " & ReportName & " อยู่ในระบบกรุณาตรวจสอบ"
                Exit Sub
            End If

        Catch ex As Exception
            lblAlert.Text = ex.Message & Request.ApplicationPath + FileName
        End Try


    End Sub

    Public Function setRptLogin(ByRef rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument, Optional ByRef errorMsg As String = "", Optional ByVal dtset As DataSet = Nothing) As Boolean
        Dim result As Boolean = True
        If Not rpt Is Nothing Then
            Dim logoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim objDB As New ApplicationBaseClass

            Dim sv As String = ApplicationBaseClass.sqlServer
            Dim db As String = ApplicationBaseClass.sqlDatabase
            Dim us As String = ApplicationBaseClass.sqlUsername
            Dim pw As String = ApplicationBaseClass.sqlPassword

            Try
                For Each tbl As CrystalDecisions.CrystalReports.Engine.Table In rpt.Database.Tables
                    logoninfo = tbl.LogOnInfo
                    With logoninfo.ConnectionInfo
                        .ServerName = sv
                        .DatabaseName = db
                        .UserID = us
                        .Password = pw
                    End With
                    tbl.ApplyLogOnInfo(logoninfo)
                Next

                If Not rpt.Subreports Is Nothing Then
                    If rpt.Subreports.Count > 0 Then
                        For Each subRpt As CrystalDecisions.CrystalReports.Engine.ReportDocument In rpt.Subreports
                            If Not subRpt Is Nothing Then
                                If subRpt.IsSubreport Then
                                    For Each tbl As CrystalDecisions.CrystalReports.Engine.Table In subRpt.Database.Tables
                                        logoninfo = tbl.LogOnInfo
                                        With logoninfo.ConnectionInfo
                                            .ServerName = sv
                                            .DatabaseName = db
                                            .UserID = us
                                            .Password = pw
                                        End With
                                        tbl.ApplyLogOnInfo(logoninfo)
                                    Next
                                End If

                            End If
                        Next
                    End If
                End If

            Catch ex As Exception
                errorMsg = ex.Message
                MsgBox(ex.Message)
                result = False
            End Try
        End If
        Return result
    End Function

    Public Sub SelecttionRPT()
        Dim crRpt As New ReportDocument

        crRpt.Load(Server.MapPath("~/" + FileName))
        setRptLogin(crRpt)


        'Select Case FagRPT
        '    Case "SB"

        '        Rpt.SetParameterValue("rptHeader", ReportParameter(0))
        '        Rpt.SetParameterValue("PayType", ReportParameter(1))
        '        Rpt.SetParameterValue("TotalAmount", ReportParameter(2))
        '        Rpt.SetParameterValue("AccName", ReportParameter(3))
        '        Rpt.SetParameterValue("Descrp", ReportParameter(4))
        '        Rpt.SetParameterValue("SlipDate", ReportParameter(5))

        '        CrystalReportViewer1.ReportSource = Rpt
        '        CrystalReportViewer1.RefreshReport()



        '    Case "BLS"
        '        Rpt.SetParameterValue("pmStartDate", ReportParameter(0))
        '        Rpt.RecordSelectionFormula = SelectionFomula
        '        CrystalReportViewer1.ReportSource = Rpt
        '        CrystalReportViewer1.RefreshReport()

        '    Case "BGI"
        '        Rpt.SetParameterValue("rptDate", ReportParameter(0))
        '        'Rpt.Subreports(0).RecordSelectionFormula = "{View_KCB_SubbookIncomeSummary.CreateDate}<='" & ReportParameter(0) & "'"
        '        'Rpt.Subreports(1).RecordSelectionFormula = "{View_KCB_SubbookIncomeSummary.CreateDate}<='" & ReportParameter(0) & "'"
        '        'Rpt.RecordSelectionFormula = "{View_KCB_SubbookIncomeSummary.CreateDate}<='" & ReportParameter(0) & "'"
        '        Rpt.RecordSelectionFormula = SelectionFomula
        '        CrystalReportViewer1.ReportSource = Rpt
        '        CrystalReportViewer1.RefreshReport()

        '    Case Else
        '        'setRptLogin(Rpt)

        '        ' crRpt.Load(Server.MapPath("Reports/rptStudent_Bio.rpt"))


        '        'crRpt.SetDataSource(dtMap)


        'End Select



        'CrystalReportViewer1.Visible = True
        crRpt.RecordSelectionFormula = SelectionFomula
        'CrystalReportViewer1.ReportSource = crRpt
        crRpt.Refresh()
        'CrystalReportViewer1.RefreshReport()

        crRpt.ExportToHttpResponse(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, Response, True, "StudentSend")


    End Sub

    Private Sub InitRptPreview()
        With CrystalReportViewer1
            .HasDrillUpButton = True
            .HasExportButton = True
            .HasGotoPageButton = True
            .HasPageNavigationButtons = True
            .HasPrintButton = False
            .HasRefreshButton = True
            .HasSearchButton = True
            .HasToggleGroupTreeButton = True
            .HasViewList = True
            .HasZoomFactorList = True
            .HasCrystalLogo = False
            .Visible = True
        End With

    End Sub



    Private Sub ListAllPrinters()
        For Each printerName As String In PrinterSettings.InstalledPrinters
            DropDownList1.Items.Add(printerName)
        Next
        'ค้นหา default printer     
        Dim oPS As New System.Drawing.Printing.PrinterSettings
        Try

            DropDownList1.SelectedItem.Text = oPS.PrinterName
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub cmdPrint_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdPrint.Click

        'crRpt.PrintOptions.PrinterName = DropDownList1.SelectedItem.Text
        'crRpt.PrintToPrinter(1, True, 0, 0)

    End Sub
End Class