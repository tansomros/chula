Imports System.Net
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Text

Public Class ReportServerViewer
    Inherits System.Web.UI.Page
    Public Shared ReportFormula As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Login.aspx")
        End If
        If Not IsPostBack Then
            If Not Request("id") Is Nothing Then
                LoadData(Request("id"))
            Else
                LoadData("")
            End If
            LoadReport()
        End If
        'Response.AppendHeader("content-disposition", "attachment;filename=xxx.pdf")
        'Response.Charset = ""
        'Response.ContentType = "application/vnd.ms-pdf"
        'Response.Write("<script type='text/javascript'>window.close();</script>")
    End Sub
    Private Sub LoadData(pID As String)

        Dim ctlstd As New StudentController

        Select Case Request("m")
            Case "cert"
                'lblReportTitle.Text = "พิมพ์ใบรับรองการผ่านการฝึกปฏิบัติงาน"
                'Session("rptyear") = Request("y")
                'Session("rptcertdate") = Request("d")

                If pID <> "" Then
                    ctlstd.TMP_Student_SET2CERTBySTDID(Request("id"), Request.Cookies("Username").Value)
                Else
                    If ReportFormula <> "" Then
                        ctlstd.TMP_Student_SET2CERT(ReportFormula, Request.Cookies("Username").Value)
                    End If
                End If
            Case "certregis"
                'lblReportTitle.Text = "พิมพ์แบบรายงานการฝึกปฏิบัติงานสำหรับผู้สมัครสอบความรู้เพื่อขึ้นทะเบียนเป็นผู้ประกอบวิชาชีพเภสัชกรรม"

                If pID <> "" Then
                    ctlstd.TMP_Student_SET2CERTBySTDID(Request("id"), Request.Cookies("Username").Value)
                Else
                    If ReportFormula <> "" Then
                        ctlstd.TMP_Student_SET2CERT(ReportFormula, Request.Cookies("Username").Value)
                    End If
                End If


            Case Else
                'lblReportTitle.Text = "พิมพ์ประวัตินิสิต (Student Bio)"
                If pID <> "" Then
                    ctlstd.TMP_Student_SET2BIOBySTDID(Request("id"), Request.Cookies("Username").Value)
                Else
                    If ReportFormula <> "" Then
                        ctlstd.TMP_Student_SET2BIO(ReportFormula, Request.Cookies("Username").Value)
                    End If
                End If
        End Select
    End Sub
    Private Sub LoadReport()

        'System.Threading.Thread.Sleep(1000)
        'UpdateProgress1.Visible = True


        Dim credential As New ReportServerCredentials
        ReportViewer1.Reset()
        ReportViewer1.ServerReport.ReportServerCredentials = credential
        ReportViewer1.ServerReport.ReportServerUrl = credential.ReportServerUrl
        ReportViewer1.ShowPrintButton = True

        'Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)

        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("th-TH")
        Dim xParam As New List(Of ReportParameter)

        Select Case FagRPT

            Case "REQACCOUNT", "REQESTIMATE"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("Year", Request("y")))
                xParam.Add(New ReportParameter("LocationGroupID", Request("lgrp")))
            Case "DOCSTDLIST"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("Year", Request("y")))
                xParam.Add(New ReportParameter("LocationGroupID", Request("lgrp")))
            Case "STDLOCATION", "SCORE", "GRADE", "ASSESSMENTRESULT"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("Year", Request("y")))
                xParam.Add(New ReportParameter("SubjectCode", Request("c")))

            Case "STDASSESSMENT2", "PAYMENTBYCOURSE"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("Year", Request("y")))
                xParam.Add(New ReportParameter("CourseID", Request("c")))

            Case "STDASSBYCOURSE", "PHASECOUNT"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("Year", Request("y")))
                xParam.Add(New ReportParameter("LocationGroupID", "0"))
            Case "TIMEPHASE", "STDASSESSMENT", "STDASSBYSTD"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("Year", Request("y")))
            Case "DOC"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("Year", Request("y")))
                xParam.Add(New ReportParameter("DocumentUID", Request("d")))

            Case "HOUR"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("LevelClass", Request("lv")))
            Case "PAYAMOUNT"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("Year", Request("y")))
                xParam.Add(New ReportParameter("CourseID", "0"))
            Case "PRECEPTOR_SCORE", "PRECEPTOR_SCORE_ALL"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("Year", Request("y")))
                xParam.Add(New ReportParameter("SubjectCode", Request("c")))

                If Request.Cookies("ROLE_ADM").Value = True Or Request.Cookies("ROLE_SPA").Value = True Or Request.Cookies("ROLE_TCH").Value = True Or Request.Cookies("ROLE_ADV").Value = True Then
                    xParam.Add(New ReportParameter("LocationID", "0"))
                Else
                    If Request.Cookies("ROLE_CO1").Value = True Then
                        xParam.Add(New ReportParameter("LocationID", "0"))
                    Else
                        xParam.Add(New ReportParameter("LocationID", Request.Cookies("LocationID").Value.ToString()))
                    End If
                End If
            Case "ASSMSUMMARY"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                Reportskey = "XLS"
                xParam.Add(New ReportParameter("Year", Request("y")))
                xParam.Add(New ReportParameter("SubjectCode", Request("c")))
                xParam.Add(New ReportParameter("PhaseID", Request("p")))
                xParam.Add(New ReportParameter("LocationID", Request("l")))
            Case "PC2"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("EduYear", Request("y")))
                xParam.Add(New ReportParameter("SubjectUID", Request("sj")))
            Case "cert"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("StudentCertificate")
                xParam.Add(New ReportParameter("EduYear", Request("y").ToString()))
                xParam.Add(New ReportParameter("Username", Request.Cookies("Username").Value.ToString()))
                Reportskey = "WRD"
            Case "SWOT"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath(ReportName)
                xParam.Add(New ReportParameter("EduYear", Request("y")))
                xParam.Add(New ReportParameter("PhaseID", Request("p")))
                xParam.Add(New ReportParameter("LocationID", Request("lid")))
                Reportskey = "PDF"
        End Select

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