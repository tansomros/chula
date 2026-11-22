Imports System.IO
'Imports iTextSharp.text
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text.html
'Imports iTextSharp.text.xml
'Imports iTextSharp.text.html.simpleparser

Public Class printStudentBio
    Inherits System.Web.UI.Page

    Dim ctlStd As New StudentController
    Dim objStd As New StudentInfo



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Request("id") Is Nothing Then
                LoadData(Request("id"))
                ' ASPXToPDF1.RenderAsPDF()
            End If
        End If
        ' ExportToPDF()
    End Sub

    'Private Sub ExportToPDF()

    '    Response.ContentType = "application/pdf"
    '    Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf")
    '    Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '    Dim sw As New StringWriter()
    '    Dim hw As New HtmlTextWriter(sw)
    '    Me.Page.RenderControl(hw)
    '    Dim sr As New StringReader(sw.ToString())
    '    Dim pdfDoc As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
    '    Dim htmlparser As New HTMLWorker(pdfDoc)
    '    PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
    '    pdfDoc.Open()
    '    Try
    '        htmlparser.Parse(sr)
    '    Catch ex As Exception

    '    End Try

    '    pdfDoc.Close()
    '    Response.Write(pdfDoc)
    '    Response.[End]()

    'End Sub
    Private Sub LoadData(ByVal id As String)
        Dim dtP As New DataTable


        dtP = ctlStd.GetStudentBio_ByID(Request("id"))

        If dtP.Rows.Count > 0 Then
            With dtP.Rows(0)

                lblStudentCode.Text = .Item(objStd.tblField(objStd.fldPos.f01_Student_Code).fldName)
                ' lblStdCode.Text = .Item(objStd.tblField(objStd.fldPos.f01_Student_Code).fldName)

                If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f02_Prefix).fldName)) Then
                    lblName.Text = .Item(objStd.tblField(objStd.fldPos.f02_Prefix).fldName)
                Else
                    lblName.Text = ""
                End If

                lblName.Text &= DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f03_FirstName).fldName)) & " " & DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f04_LastName).fldName))

                lblNickName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f05_NickName).fldName))


                'If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f06_Gender).fldName)) Then
                '    If .Item(objStd.tblField(objStd.fldPos.f06_Gender).fldName) = "M" Then
                '        lb()
                '    End If
                'End If

                lblMail.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f07_Email).fldName))
                lblTel.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f08_Telephone).fldName))
                lblMobile.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f09_MobilePhone).fldName))

                If DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f10_BirthDate).fldName)) <> "" Then
                    lblBirthDate.Text = DisplayStr2DateTH(.Item(objStd.tblField(objStd.fldPos.f10_BirthDate).fldName))
                End If

                If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f11_BloodGroup).fldName)) Then
                    lblBloodGroup.Text = .Item(objStd.tblField(objStd.fldPos.f11_BloodGroup).fldName)
                End If

                lblMajor.Text = ""


                If DBNull2Zero(.Item(objStd.tblField(objStd.fldPos.f12_MajorID).fldName)) <> 0 Then
                    lblMajor.Text = "สาขาวิชา" & String.Concat(.Item("MajorName")) & "  "
                End If

                If DBNull2Zero(.Item(objStd.tblField(objStd.fldPos.f13_MinorID).fldName)) <> 0 Then
                    lblMajor.Text &= "สาขา" & String.Concat(.Item("MinorName")) & "  "
                End If
                If DBNull2Zero(.Item(objStd.tblField(objStd.fldPos.f14_SubMinorID).fldName)) <> 0 Then
                    lblMajor.Text &= "แขนง" & String.Concat(.Item("SubMinorName")) & "  "
                End If

                'If Not IsDBNull(.Item(objStd.tblField(objStd.fldPos.f15_AdvisorID).fldName)) Then
                '    ddlAdvisor.SelectedValue = .Item(objStd.tblField(objStd.fldPos.f15_AdvisorID).fldName)
                'End If


                lblFname.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f16_Father_FirstName).fldName)) & " " & DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f17_Father_LastName).fldName))

                lblFCareer.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f18_Father_Career).fldName))
                lblFTel.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f19_Father_Tel).fldName))

                lblMname.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f20_Mother_FirstName).fldName)) & " " & DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f21_Mother_LastName).fldName))
                lblMCareer.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f22_Mother_Career).fldName))
                lblMTel.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f23_Mother_Tel).fldName))
                lblSibling.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f24_Sibling).fldName))
                lblChildNo.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f25_ChildNo).fldName))

                lblAddress.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f26_Address).fldName)) & " " & DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f27_District).fldName)) & " " & DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f28_City).fldName)) & " " & DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f30_ProvinceName).fldName)) & " " & DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f31_ZipCode).fldName))


                lblCongenitalDisease.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f32_CongenitalDisease).fldName))
                'lblMedicineUsually.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f33_MedicineUsually).fldName))
                'lblMedicalHistory.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f34_MedicalHistory).fldName))
                lblHobby.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f35_Hobby).fldName))
                lblTalent.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f36_Talent).fldName))
                lblExpectations.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f37_Expectations).fldName))
                lblPrimarySchool.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f38_PrimarySchool).fldName))
                lblPrimaryProvince.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f39_PrimaryProvince).fldName))
                lblPrimaryYear.Text = Zero2StrNull(.Item(objStd.tblField(objStd.fldPos.f40_PrimaryYear).fldName))
                lblSecondarySchool.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f41_SecondarySchool).fldName))
                lblSecondaryProvince.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f42_SecondaryProvince).fldName))
                lblSecondaryYear.Text = Zero2StrNull(.Item(objStd.tblField(objStd.fldPos.f43_SecondaryYear).fldName))
                lblHighSchool.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f44_HighSchool).fldName))
                lblHighProvince.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f45_HighProvince).fldName))
                lblHighYear.Text = Zero2StrNull(.Item(objStd.tblField(objStd.fldPos.f46_HighYear).fldName))
                lblContactName.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f47_ContactName).fldName))
                lblContactAddress.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f48_ContactAddress).fldName))
                lblContactTel.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f49_ContactTel).fldName))

                lblGPAX.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f54_GPAX).fldName))
                lblContactRelation.Text = DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f55_ContactRelation).fldName))

                If DBNull2Str(.Item(objStd.tblField(objStd.fldPos.f50_PicturePath).fldName)) <> "" Then
                    picStudent.ImageUrl = "~/" & stdPic & "/" & .Item(objStd.tblField(objStd.fldPos.f50_PicturePath).fldName)
                End If


            End With


        End If


        dtP = Nothing
    End Sub

    'Private Sub RenderPDF(writer As HtmlTextWriter)
    '    Dim mem As New MemoryStream()
    '    Dim twr As New StreamWriter(mem)
    '    Dim myWriter As New HtmlTextWriter(twr)
    '    MyBase.Render(myWriter)
    '    myWriter.Flush()
    '    myWriter.Dispose()
    '    Dim strmRdr As New StreamReader(mem)
    '    strmRdr.BaseStream.Position = 0
    '    Dim pageContent As String = strmRdr.ReadToEnd()
    '    strmRdr.Dispose()
    '    mem.Dispose()
    '    writer.Write(pageContent)
    '    CreatePDFDocument(pageContent)


    'End Sub


    'Private Sub CreatePDFDocument(strHtml As String)

    '    Dim strFileName As String = HttpContext.Current.Server.MapPath("test.pdf")
    '    ' step 1: creation of a document-object
    '    Dim document As New Document()
    '    ' step 2:
    '    ' we create a writer that listens to the document
    '    PdfWriter.GetInstance(document, New FileStream(strFileName, FileMode.Create))
    '    Dim se As New StringReader(strHtml)
    '    Dim obj As New HTMLWorker(document)
    '    document.Open()
    '    obj.Parse(se)
    '    document.Close()
    '    ShowPdf(strFileName)



    'End Sub

    'Private Sub ShowPdf(strFileName As String)
    '    Response.ClearContent()
    '    Response.ClearHeaders()
    '    Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName)
    '    Response.ContentType = "application/pdf"
    '    Response.WriteFile(strFileName)
    '    Response.Flush()
    '    Response.Clear()
    'End Sub




End Class