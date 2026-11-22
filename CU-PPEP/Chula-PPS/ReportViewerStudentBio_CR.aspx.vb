Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports CrystalDecisions.Shared

Public Class ReportViewerStudentBio_CR
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Public Shared ReportFormula As String

    Private crRpt As New ReportDocument()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Request.Browser.Browser = "Firefox" Then
            Panel1.Visible = True
        ElseIf Request.Browser.Browser = "IE" Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
            InitRptPreview()

            If Not Request("id") Is Nothing Then
                LoadData(Request("id"))
            Else

                LoadData("")
            End If


        End If
        'Dim s As String = ""
        'With Request.Browser
        '    s &= "Browser Capabilities" & vbCrLf
        '    s &= "Type = " & .Type & vbCrLf
        '    s &= "Name = " & .Browser & vbCrLf
        '    s &= "Version = " & .Version & vbCrLf
        '    s &= "Major Version = " & .MajorVersion & vbCrLf
        '    s &= "Minor Version = " & .MinorVersion & vbCrLf
        '    s &= "Platform = " & .Platform & vbCrLf
        '    s &= "Is Beta = " & .Beta & vbCrLf
        '    s &= "Is Crawler = " & .Crawler & vbCrLf
        '    s &= "Is AOL = " & .AOL & vbCrLf
        '    s &= "Is Win16 = " & .Win16 & vbCrLf
        '    s &= "Is Win32 = " & .Win32 & vbCrLf
        '    s &= "Supports Frames = " & .Frames & vbCrLf
        '    s &= "Supports Tables = " & .Tables & vbCrLf
        '    s &= "Supports Cookies = " & .Cookies & vbCrLf
        '    s &= "Supports VBScript = " & .VBScript & vbCrLf
        '    s &= "Supports JavaScript = " & _
        '        .EcmaScriptVersion.ToString() & vbCrLf
        '    s &= "Supports Java Applets = " & .JavaApplets & vbCrLf
        '    s &= "Supports ActiveX Controls = " & .ActiveXControls & _
        '        vbCrLf
        '    s &= "Supports JavaScript Version = " & _
        '        Request.Browser("JavaScriptVersion") & vbCrLf
        'End With

        'Label1.Text = s



    End Sub

    Private Sub InitRptPreview()
        With CrystalReportViewer1
            .HasDrillUpButton = True
            .HasExportButton = True
            .HasGotoPageButton = True
            .HasPageNavigationButtons = True
            .HasPrintButton = True
            .HasRefreshButton = True
            .HasSearchButton = True
            .HasToggleGroupTreeButton = True
            .HasViewList = True
            .HasZoomFactorList = True
            .HasCrystalLogo = False
        End With

    End Sub

    Private Sub LoadData(pID As String)

        Dim ctlstd As New StudentController

        If pID <> "" Then
            dt = ctlstd.GetStudentBio_ByID(Request("id"))
        Else

            If ReportFormula <> "" Then
                dt = ctlstd.GetStudent_ByQuery(ReportFormula)
            Else
                dt = ctlstd.GetStudent
            End If

        End If


        Dim dtMap As New DataTable("dtStudentBio") '*** DataTable Map DataSet.xsd ***'
        Dim dr As DataRow
        dtMap.Columns.Add(New DataColumn("Student_ID", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("Student_Code", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Picture_Steam", GetType(System.Byte())))
        dtMap.Columns.Add(New DataColumn("Prefix", GetType(String)))
        dtMap.Columns.Add(New DataColumn("FirstName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("LastName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("NickName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Gender", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Email", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Telephone", GetType(String)))
        dtMap.Columns.Add(New DataColumn("MobilePhone", GetType(String)))
        dtMap.Columns.Add(New DataColumn("BirthDate", GetType(String)))
        dtMap.Columns.Add(New DataColumn("BloodGroup", GetType(String)))
        dtMap.Columns.Add(New DataColumn("MajorID", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("MinorID", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("SubMinorID", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("AdvisorID", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("GPAX", GetType(Double)))
        dtMap.Columns.Add(New DataColumn("Father_FirstName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Father_LastName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Father_Career", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Father_Tel", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Mother_FirstName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Mother_LastName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Mother_Career", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Mother_Tel", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Sibling", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("ChildNo", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("Address", GetType(String)))
        dtMap.Columns.Add(New DataColumn("District", GetType(String)))
        dtMap.Columns.Add(New DataColumn("City", GetType(String)))
        dtMap.Columns.Add(New DataColumn("ProvinceID", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("ProvinceName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("ZipCode", GetType(String)))
        dtMap.Columns.Add(New DataColumn("CongenitalDisease", GetType(String)))
        dtMap.Columns.Add(New DataColumn("MedicineUsually", GetType(String)))
        dtMap.Columns.Add(New DataColumn("MedicalHistory", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Hobby", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Talent", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Expectations", GetType(String)))
        dtMap.Columns.Add(New DataColumn("PrimarySchool", GetType(String)))
        dtMap.Columns.Add(New DataColumn("PrimaryProvince", GetType(String)))
        dtMap.Columns.Add(New DataColumn("PrimaryYear", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("SecondarySchool", GetType(String)))
        dtMap.Columns.Add(New DataColumn("SecondaryProvince", GetType(String)))
        dtMap.Columns.Add(New DataColumn("SecondaryYear", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("HighSchool", GetType(String)))
        dtMap.Columns.Add(New DataColumn("HighProvince", GetType(String)))
        dtMap.Columns.Add(New DataColumn("HighYear", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("ContactName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("ContactAddress", GetType(String)))
        dtMap.Columns.Add(New DataColumn("ContactTel", GetType(String)))
        dtMap.Columns.Add(New DataColumn("ContactRelation", GetType(String)))
        dtMap.Columns.Add(New DataColumn("PicturePath", GetType(String)))
        dtMap.Columns.Add(New DataColumn("LastUpdate", GetType(String)))
        dtMap.Columns.Add(New DataColumn("CreateDate", GetType(String)))
        dtMap.Columns.Add(New DataColumn("UpdateBy", GetType(String)))
        dtMap.Columns.Add(New DataColumn("MajorName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("MinorName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("SubMinorName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("PrefixID", GetType(Integer)))
        dtMap.Columns.Add(New DataColumn("PrefixName", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Ad_Fname", GetType(String)))
        dtMap.Columns.Add(New DataColumn("Ad_Lname", GetType(String)))

        Dim i As Integer = 0
        Dim stP As String = ""
        For i = 0 To dt.Rows.Count - 1
            If DBNull2Str(dt.Rows(i)("PicturePath")) = "" Then
                stP = "nopic.jpg"
            Else
                stP = dt.Rows(i)("PicturePath")
            End If

            Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/" & stdPic & "/" & stP))

            If objfile.Exists Then
                stP = stP
            Else
                stP = "nopic.jpg"
            End If

            Dim fiStream As New FileStream(Server.MapPath(stdPic & "/" & stP), FileMode.Open)
            Dim binReader As New BinaryReader(fiStream)
            Dim pic() As Byte = {}
            pic = binReader.ReadBytes(fiStream.Length)

            dr = dtMap.NewRow
            dr("Student_ID") = dt.Rows(i)("Student_ID")
            dr("Student_Code") = dt.Rows(i)("Student_Code")
            dr("Prefix") = dt.Rows(i)("Prefix")
            dr("FirstName") = dt.Rows(i)("FirstName")
            dr("LastName") = dt.Rows(i)("LastName")
            dr("NickName") = dt.Rows(i)("NickName")
            dr("Gender") = dt.Rows(i)("Gender")
            dr("Email") = dt.Rows(i)("Email")
            dr("Telephone") = dt.Rows(i)("Telephone")
            dr("MobilePhone") = dt.Rows(i)("MobilePhone")
            dr("BirthDate") = dt.Rows(i)("BirthDate")
            dr("BloodGroup") = dt.Rows(i)("BloodGroup")
            dr("MajorID") = dt.Rows(i)("MajorID")
            dr("MinorID") = dt.Rows(i)("MinorID")
            dr("SubMinorID") = dt.Rows(i)("SubMinorID")
            dr("AdvisorID") = dt.Rows(i)("AdvisorID")
            dr("GPAX") = dt.Rows(i)("GPAX")
            dr("Father_FirstName") = dt.Rows(i)("Father_FirstName")
            dr("Father_LastName") = dt.Rows(i)("Father_LastName")
            dr("Father_Career") = dt.Rows(i)("Father_Career")
            dr("Father_Tel") = dt.Rows(i)("Father_Tel")
            dr("Mother_FirstName") = dt.Rows(i)("Mother_FirstName")
            dr("Mother_LastName") = dt.Rows(i)("Mother_LastName")
            dr("Mother_Career") = dt.Rows(i)("Mother_Career")
            dr("Mother_Tel") = dt.Rows(i)("Mother_Tel")
            dr("Sibling") = dt.Rows(i)("Sibling")
            dr("ChildNo") = dt.Rows(i)("ChildNo")
            dr("Address") = dt.Rows(i)("Address")
            dr("District") = dt.Rows(i)("District")
            dr("City") = dt.Rows(i)("City")
            dr("ProvinceID") = dt.Rows(i)("ProvinceID")
            dr("ProvinceName") = dt.Rows(i)("ProvinceName")
            dr("ZipCode") = dt.Rows(i)("ZipCode")
            dr("CongenitalDisease") = dt.Rows(i)("CongenitalDisease")
            dr("MedicineUsually") = dt.Rows(i)("MedicineUsually")
            dr("MedicalHistory") = dt.Rows(i)("MedicalHistory")
            dr("Hobby") = dt.Rows(i)("Hobby")
            dr("Talent") = dt.Rows(i)("Talent")
            dr("Expectations") = dt.Rows(i)("Expectations")
            dr("PrimarySchool") = dt.Rows(i)("PrimarySchool")
            dr("PrimaryProvince") = dt.Rows(i)("PrimaryProvince")
            dr("PrimaryYear") = dt.Rows(i)("PrimaryYear")
            dr("SecondarySchool") = dt.Rows(i)("SecondarySchool")
            dr("SecondaryProvince") = dt.Rows(i)("SecondaryProvince")
            dr("SecondaryYear") = dt.Rows(i)("SecondaryYear")
            dr("HighSchool") = dt.Rows(i)("HighSchool")
            dr("HighProvince") = dt.Rows(i)("HighProvince")
            dr("HighYear") = dt.Rows(i)("HighYear")
            dr("ContactName") = dt.Rows(i)("ContactName")
            dr("ContactAddress") = dt.Rows(i)("ContactAddress")
            dr("ContactTel") = dt.Rows(i)("ContactTel")
            dr("ContactRelation") = dt.Rows(i)("ContactRelation")
            dr("PicturePath") = dt.Rows(i)("PicturePath")
            dr("LastUpdate") = dt.Rows(i)("LastUpdate")
            dr("CreateDate") = dt.Rows(i)("CreateDate")
            dr("UpdateBy") = dt.Rows(i)("UpdateBy")
            dr("MajorName") = dt.Rows(i)("MajorName")
            dr("MinorName") = dt.Rows(i)("MinorName")
            dr("SubMinorName") = dt.Rows(i)("SubMinorName")
            dr("PrefixID") = dt.Rows(i)("PrefixID")
            dr("PrefixName") = dt.Rows(i)("PrefixName")
            dr("Ad_Fname") = dt.Rows(i)("Ad_Fname")
            dr("Ad_Lname") = dt.Rows(i)("Ad_Lname")

            dr("Picture_Steam") = pic
            dtMap.Rows.Add(dr)

            fiStream.Close()
            binReader.Close()

        Next

        'Dim rpt As New ReportDocument()

        'rpt.Load(Server.MapPath("Reports/rptStudent_Bio.rpt"))
        'Response.Buffer = False
        'Response.ClearContent()
        'Response.ClearHeaders()

        'rpt.SetDataSource(dtMap)
        'rpt.PrintToPrinter(1, True, 0, 0)

        ' ''rpt.RecordSelectionFormula = ReportFormula
        ''CrystalReportViewer1.HasPrintButton = True
        ''CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX
        ''Me.CrystalReportViewer1.ReportSource = rpt

        crRpt.Load(Server.MapPath("Reports/rptStudent_Bio.rpt"))
        crRpt.SetDatabaseLogon("sa", "11223344", "localhost", "chula_pps")

        crRpt.SetDataSource(dtMap)

        CrystalReportViewer1.DisplayGroupTree = False
        CrystalReportViewer1.ReportSource = crRpt
        CrystalReportViewer1.Visible = True
        'displayreportPDF()

    End Sub
    Private Sub displayreportPDF()
        Dim _export As New MemoryStream()
        _export = DirectCast(crRpt.ExportToStream(ExportFormatType.PortableDocFormat), MemoryStream)
        Response.Clear()
        Response.Buffer = True
        Response.ClearContent()
        Response.ClearHeaders()
        Response.ContentType = "application/pdf"
        Try
            Response.BinaryWrite(_export.ToArray())
            Response.[End]()
            Response.Flush()
            Response.Close()
        Catch generatedExceptionName As Exception
            ' throw; 
            Response.Write("Network Busy")
        Finally
            crRpt.Close()
            crRpt.Dispose()
        End Try
    End Sub


End Class