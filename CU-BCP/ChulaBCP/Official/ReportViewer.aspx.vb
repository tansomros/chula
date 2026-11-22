Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WebForms

Public Class ReportViewer
    Inherits System.Web.UI.Page

    Dim acc As New UserController
    Dim dt As New DataTable
    Public Shared ReportFormula As String
    Dim ReportFileName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Browser.Browser = "Firefox" Then
            Panel1.Visible = True
        ElseIf Request.Browser.Browser = "IE" Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
            LoadReport()
        End If
    End Sub

    Private Sub LoadReport()

        'System.Threading.Thread.Sleep(1000)
        'UpdateProgress1.Visible = True

        Dim credential As New ReportServerCredentials
        ReportViewer1.Reset()
        ReportViewer1.ServerReport.ReportServerCredentials = credential
        ReportViewer1.ServerReport.ReportServerUrl = credential.ReportServerUrl
        ReportViewer1.ShowPrintButton = True

        Dim xParam As New List(Of ReportParameter)

        Dim ctlR As New ReportController
        Select Case Request("R")
            Case "reg"
                If Not Request("std") Is Nothing Then
                    ctlR.GEN_ReportTemp(Request("std"), Request.Cookies("UserLogin").Value.ToString())
                End If
                FagRPT = "PDF"
                'ReportFileName = "RegisterCard" & "_" & Request("BDT")
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("RegisterCard")
                xParam.Add(New ReportParameter("RegisterID", Request("regid").ToString()))
                xParam.Add(New ReportParameter("Username", Request.Cookies("UserLogin").Value.ToString()))
            Case "regm"
                If Not Request("std") Is Nothing Then
                    ctlR.GEN_ReportTemp(Request("std"), Request.Cookies("UserLogin").Value.ToString())
                End If
                FagRPT = "PDF"
                'ReportFileName = "RegisterCard" & "_" & Request("BDT")
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("RegisterCard_Maintain")
                xParam.Add(New ReportParameter("RegisterID", Request("regid").ToString()))
                xParam.Add(New ReportParameter("Username", Request.Cookies("UserLogin").Value.ToString()))
            Case "grd"

                FagRPT = "PDF"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("StudentGrade")
                xParam.Add(New ReportParameter("StudentID", Request("std").ToString()))
                xParam.Add(New ReportParameter("EduYear", Request("y").ToString()))
                xParam.Add(New ReportParameter("TermNo", Request("t").ToString()))

            Case Else
                Exit Sub
                'ReportViewer1.ServerReport.ReportPath = credential.ReportPath("")
                'xParam.Add(New ReportParameter("UserID", Request.Cookies("iIncident")("userid").ToString()))
        End Select



        ReportViewer1.ServerReport.SetParameters(xParam)

        Select Case FagRPT
            Case "EXCEL"
                ' Variables
                Dim warnings As Warning()
                Dim streamIds As String()
                Dim mimeType As String = String.Empty
                Dim encoding As String = String.Empty
                Dim extension As String = String.Empty


                ' Setup the report viewer object and get the array of bytes

                Dim bytes As Byte() = ReportViewer1.ServerReport.Render("EXCEL", Nothing, mimeType, encoding, extension, streamIds, Nothing)

                ' Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                Response.AddHeader("content-disposition", Convert.ToString("attachment; filename=" & ReportName & "." & extension))
                Response.BinaryWrite(bytes)
                ' create the file
                Response.Flush()

                ' send it to the client to download
            Case Else
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

        End Select
        '
    End Sub

End Class