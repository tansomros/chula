Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Web
Public Module modGlobal
#Region "Declaration Parameter"


    Public gLang As String
    Public gPage As String

    Public isAdd As Boolean
    'Public d As String
    'Public m As String
    'Public y As String
    Public SQL As String
    'Public inRow As Integer = 0
    'Public pathPic As String = ""

    Public UserID As Integer = 0
    Public NameOfUser As String = ""
    Public UserRole As String

    'Public DateFormat As IFormatProvider = New System.Globalization.CultureInfo("en-US")

    Public DateFormat_TH As IFormatProvider = New System.Globalization.CultureInfo("th-TH")

    Public dtfInfo As DateTimeFormatInfo

    Public pnV As Boolean = False
    Public Rate_ExBed_Adult As Double = 0.0
    Public Rate_ExBed_Child As Double = 0.0

    Public bActive As Integer = 1
    Public binActive As Integer = 0

    Public DownloadPath As String = ConfigurationSettings.AppSettings("DownloadPath")
    Public tmpUpload As String = ConfigurationSettings.AppSettings("tmpUpload")
    Public PersonPic As String = ConfigurationSettings.AppSettings("PicturePerson")
    Public UserPic As String = ConfigurationSettings.AppSettings("PictureUser")
    'Public GalleryUpload As String = ConfigurationSettings.AppSettings("GalleryUpload")
    Public DocumentPath As String = ConfigurationSettings.AppSettings("DocumentPath")
    Public DocumentStudent As String = ConfigurationSettings.AppSettings("DocumentStudent")
    Public DocumentCertificate As String = ConfigurationSettings.AppSettings("DocumentCertificate")
    Public DocumentSubject As String = ConfigurationSettings.AppSettings("DocumentSubject")
    Public DocumentResearch As String = ConfigurationSettings.AppSettings("DocumentResearch")
    Public ImageNews As String = ConfigurationSettings.AppSettings("ImageNews")
    Public ImageCoverNews As String = ConfigurationSettings.AppSettings("ImageCoverNews")

    Public StudentPic As String = ConfigurationSettings.AppSettings("PictureStd")
    Public ReportPath As String = ConfigurationSettings.AppSettings("ReportPath")


    Public Reportskey As String = ""
    Public FagRPT As String = ""
    Public ReportParameter() As String
    Public ReportName As String = ""
    Public ReportTitle As String = ""
    Public FileName As String = ""
    Public SelectionFomula As String = ""

#End Region

#Region "Private Members"

    Private Const ProviderType As String = "data"
    Private Const ModuleQualifier As String = "CPA_"

    Private _providerPath As String
    Private _objectQualifier As String
    Private _databaseOwner As String

#End Region

#Region "Const_Data"
    Public Const ACTTYPE_LOG As String = "LOGIN"
    Public Const ACTTYPE_LOGOUT As String = "LOGOUT"
    Public Const ACTTYPE_ADD As String = "ADD"
    Public Const ACTTYPE_UPD As String = "UPDATE"
    Public Const ACTTYPE_DEL As String = "DELETE"
    Public Const ACTTYPE_FND As String = "FIND"
    Public Const ACTTYPE_FGT As String = "FORGOT PASS"
    Public Const CFG_EDUYEAR As String = "EDUYEAR"          'ปีการศึกษาปัจจุบัน
    Public Const CFG_ASSMYEAR As String = "ASSMYEAR"          'ปีการศึกษาสำหรับประเมิน

    Public Const CFG_STARTDATE As String = "STARTDATE"      'วันที่เริ่มเปิดให้นิสิตเลือกแหล่งฝึกร้านยาปี 6
    Public Const CFG_ENDDATE As String = "ENDDATE"          'วันสิ้นสุดให้นิสิตเลือกแหล่งฝึกร้านยาปี

    Public Const CFG_MAXLOCATION As String = "MAXLOCATION"  'จำนวนสูงสุดของแหล่งฝึกที่นิสิตสามารถเลือกได้ต่อ 1 รายวิชา
    Public Const CFG_RESULTDATE As String = "RESULTDATE"    'วันที่ประกาศผลการคัดเลือก ร้านยาแหล่งฝึก

    Public Const CFG_STARTASSM As String = "STARTASSM"        'วันที่เริ่มเปิดให้ประเมินนิสิต
    Public Const CFG_ENDASSM As String = "ENDASSM"          'วันที่สิ้นสุดการประเมินนิสิต

    Public Const isShopAccess As Integer = 1
    Public Const isReportViewerAccess As Integer = 2
    Public Const isAdminAccess As Integer = 3
    Public Const isSuperAdminAccess As Integer = 4
    Public Const isProjectManager As Integer = 5

    'Page ID
    Public Const PAGE_ABOUTUS As Integer = 1
    Public Const PAGE_CONTACTUS As Integer = 2
    Public Const PAGE_CONTACTMAP As Integer = 3
    Public Const PAGE_PROGRAM As Integer = 4
    Public Const PAGE_POLICY As Integer = 5

    Public Const PTYPE_1 As Integer = 1
    Public Const PTYPE_2 As Integer = 2
    Public Const PTYPE_3 As Integer = 3


#End Region

#Region "Genaral Function"

    Public Function chkFileExist(ByVal FileRptPath As String) As Boolean
        Dim F As New FileInfo(FileRptPath)
        Return F.Exists
    End Function
    Public Function Convert2Status(pValue As Boolean) As Integer
        If pValue = True Then
            Return 2
        Else
            Return 1
        End If
    End Function
    Public Function ConvertStatus2YN(pValue As Boolean) As String
        If pValue = True Then
            Return "Y"
        Else
            Return "N"
        End If
    End Function
    Public Function ConvertCheckedStatus2Number(pValue As Boolean) As Integer
        If pValue = True Then
            Return 1
        Else
            Return 0
        End If
    End Function
    Public Function ConvertActive2Boolean(pValue As Integer) As Boolean
        If pValue >= 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ConvertStatus2Boolean(pValue As Integer) As Boolean
        If pValue >= 2 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ConvertBoolean2Integer(ByVal pVal As Boolean) As String
        If pVal = True Then
            Return 1
        Else
            Return 0
        End If
    End Function
    Public Function ConvertBoolean2YN(ByVal pVal As Boolean) As String
        If pVal = True Then
            Return "Y"
        Else
            Return "N"
        End If
    End Function
    Public Function ConvertBoolean2StatusFlag(ByVal pVal As Boolean) As String
        If pVal = True Then
            Return "A"
        Else
            Return "D"
        End If
    End Function
    Public Function ConvertStatusFlag2CHK(ByVal pVal As String) As Boolean
        If pVal = "A" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ConvertYN2Boolean(pValue As String) As Boolean
        If pValue = "Y" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ConvertStatusFlag2Boolean(pValue As String) As Boolean
        If pValue = "A" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function Boolean2StatusFlag(pValue As Boolean) As String

        If pValue = True Then
            Return "A"
        Else
            Return "D"
        End If
    End Function

    Public Function Boolean2Decimal(pValue As Boolean) As Integer

        If pValue = True Then
            Return 1
        Else
            Return 0
        End If
    End Function

    Public Function Decimal2Boolean(pValue As Integer) As Boolean
        If pValue = 1 Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Sub DisplayMessage(ByVal page As Control, ByVal msg As String)

        Dim myScript As String = String.Format("alert('" & msg & " ');", msg)
        ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, True)

    End Sub
    Public Sub DisplayPopUpWindows(ByVal page As Control, ByVal msg As String)

        ' Dim myScript As String = String.Format(msg, msg)
        Dim myScript As String = msg
        ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, True)
    End Sub

    Public Function ParseDate(ByVal sDate As String, ByVal sKey As String) As String
        If sDate <> "" Then
            Dim str() As String
            Dim dY As Integer
            If sKey = "-" Then
                str = Split(sDate, "-")
            ElseIf sKey = "/" Then
                str = Split(sDate, "/")
            End If

            'For i = 0 To str.Length - 1
            '    sD(i) = str(i)
            'Next

            dY = Now.Date.Year - CInt(str(2))
            If dY >= 0 Then
                dY = CInt(str(2)) + 543
            Else
                dY = str(2)
            End If
            sDate = str(1) & "/" & str(0) & "/" & dY
        End If
        Return sDate
    End Function

    Public Function ParseDateToSQL(ByVal sDate As String, ByVal sKey As String) As String
        Dim str() As String
        Dim dY As Integer

        If sKey = "-" Then
            str = Split(sDate, "-")
        ElseIf sKey = "/" Then
            str = Split(sDate, "/")
        End If

        dY = Now.Date.Year - CInt(str(2))

        dY = StrNull2Zero(str(2))
        Do Until dY < 2500
            dY = dY - 543
        Loop

        sDate = dY & "-" & str(1) & "-" & str(0)
        Return sDate
    End Function


    Public Function ConvertDateToString(ByVal Adate As Date) As String
        If Not IsDBNull(Adate) Then
            Dim strDate(2) As String
            strDate(0) = CStr(Adate.Day)
            strDate(1) = CStr(Adate.Month)
            strDate(2) = CStr(Adate.Year)

            If strDate(0).Length < 2 Then
                strDate(0) = "0" & strDate(0)
            End If
            If strDate(1).Length < 2 Then
                strDate(1) = "0" & strDate(1)
            End If

            If CInt(strDate(2)) < 2550 Then
                strDate(2) = CInt(strDate(2)) + 543
            End If

            ConvertDateToString = strDate(0) & "/" & strDate(1) & "/" & strDate(2)
        Else
            ConvertDateToString = ""
        End If
        Return ConvertDateToString

    End Function

    Public Function ConvertFormateDate(ByVal Adate As Date) As String
        Dim strDate(2) As String
        strDate(0) = CStr(Adate.Day)
        strDate(1) = CStr(Adate.Month)
        strDate(2) = CStr(Adate.Year)

        If strDate(0).Length < 2 Then
            strDate(0) = "0" & strDate(0)
        End If
        If strDate(1).Length < 2 Then
            strDate(1) = "0" & strDate(1)
        End If

        ConvertFormateDate = strDate(1) & "/" & strDate(0) & "/" & strDate(2)

        Return ConvertFormateDate

    End Function


    Public Function changStringToDate(ByVal Adate As String) As String
        Dim strDate(3) As String
        strDate = Split(Adate, "-")
        changStringToDate = strDate(0) & "/" & strDate(1) & "/" & CInt(strDate(2)) '+ 543
        Return changStringToDate
    End Function

    Public Function DBNull2StrDash(ByVal str As Object) As String
        If IsDBNull(str) Then
            Return "-"
        ElseIf str = "" Then
            Return "-"
        Else
            Return CStr(str)
        End If
    End Function
    Public Function StrNull2Zero(ByVal str As String) As Integer
        If str = "" Then
            Return 0
        Else
            If Not IsNumeric(str) Then
                Return 0
            ElseIf str = "Infinity" Or str = "-Infinity" Then
                Return 0
            Else
                Return CInt(str)
            End If
        End If
    End Function

    Public Function StrRadioOption2Zero(ByVal str As String) As Integer
        If str = "" Then
            Return 0
        Else
            If Not IsNumeric(str) Then
                Return 0
            ElseIf str = "2" Then
                Return 0
            ElseIf str = "Infinity" Or str = "-Infinity" Then
                Return 0
            Else
                Return CInt(str)
            End If
        End If
    End Function

    Public Function StrNull2Long(ByVal str As String) As Long
        If str = "" Then
            Return 0
        Else
            If Not IsNumeric(str) Then
                Return 0
            Else
                Return CLng(str)
            End If
        End If
    End Function
    Public Function StrNull2Double(ByVal str As String) As Double
        If str = "" Then
            Return 0
        Else
            If Not IsNumeric(str) Then
                Return 0
            Else
                Return CDbl(str)
            End If
        End If
    End Function


    Public Function Prefix2Display(ByVal str As Object) As String
        If IsDBNull(str) Then
            Return ""
        Else
            If str = "นิติบุคคล" Or str = "" Then
                Return ""
            Else
                Return CStr(str)
            End If

        End If
    End Function

    Public Function Zero2StrNull(ByVal str As Object) As String
        If IsDBNull(str) Then
            Return ""
        Else
            If str = 0 Then
                Return ""
            Else
                Return CStr(str)
            End If

        End If
    End Function

    Public Function DisplayNoneTH(ByVal str As String) As String
        If (str.Trim() = "") Then
            Return "ไม่มี"
        Else
            Return str
        End If
    End Function

    Public Function DisplayNoneEN(ByVal str As String) As String
        If (str.Trim() = "") Then
            Return "N/A"
        Else
            Return str
        End If
    End Function



    Public Function DBNull2Str(ByVal str As Object) As String
        If IsDBNull(str) Then
            Return ""
        Else
            Return CStr(str)
        End If
    End Function
    Public Function DBNull2Zero(ByVal str As Object) As Integer
        If IsDBNull(str) Then
            Return 0
        Else
            Return CInt(str)
        End If
    End Function
    Public Function DBNull2Lng(ByVal str As Object) As Long
        If IsDBNull(str) Then
            Return 0
        Else
            If String.Concat(str) <> "" Then
                Return CLng(str)
            Else
                Return 0
            End If
        End If
    End Function
    Public Function DBNull2Dbl(ByVal str As Object) As Double
        If IsDBNull(str) Then
            Return 0
        Else
            If Not IsNumeric(str) Then
                Return 0
            Else
                Return CDbl(str)
            End If
        End If
    End Function

    Public Function FormatCardID(ByVal str As String) As String
        Dim sFormat As String = ""

        If str <> "" Then
            sFormat = Left(str, 1)
            If Mid(str, 2, 4) <> "" Then
                sFormat &= "-" & Mid(str, 2, 4)
            End If

            If Mid(str, 6, 5) <> "" Then
                sFormat &= "-" & Mid(str, 6, 5)
            End If

            If Mid(str, 11, 3) <> "" Then
                sFormat &= "-" & Mid(str, 11, 3)
            End If

        End If
        Return sFormat
    End Function
    'Public Sub UploadFile(ByVal Fileupload As Object, ByVal prmPath As String)
    '    Dim FileFullName As String = Fileupload.PostedFile.FileName
    '    Dim FileNameInfo As String = Path.GetFileName(FileFullName)
    '    Dim filename As String = Path.GetFileName(Fileupload.PostedFile.FileName)
    '    Dim objfile As FileInfo = New FileInfo(prmPath)
    '    If FileNameInfo <> "" Then
    '        Fileupload.PostedFile.SaveAs(objfile.DirectoryName & "\" & filename)
    '    End If
    '    objfile = Nothing
    'End Sub


    Public Const ScriptTimeout As Integer = 60

    ' second
    Public Const SessionTimeout As Integer = 20

    ' minute
    Public Const MAXPERPAGE As Double = 30

    ' record(s) display per page.
    Public Sub InitializePage(ByVal p As Page)
        p.Session.Timeout = SessionTimeout
        p.Server.ScriptTimeout = ScriptTimeout
    End Sub


    Public Function CheckPersonalID(ByVal uid As String) As Boolean
        Dim IntID As Integer = ((CInt(uid.Substring(0, 1)) * 13) _
                    + ((CInt(uid.Substring(1, 1)) * 12) _
                    + ((CInt(uid.Substring(2, 1)) * 11) _
                    + ((CInt(uid.Substring(3, 1)) * 10) _
                    + ((CInt(uid.Substring(4, 1)) * 9) _
                    + ((CInt(uid.Substring(5, 1)) * 8) _
                    + ((CInt(uid.Substring(6, 1)) * 7) _
                    + ((CInt(uid.Substring(7, 1)) * 6) _
                    + ((CInt(uid.Substring(8, 1)) * 5) _
                    + ((CInt(uid.Substring(9, 1)) * 4) _
                    + ((CInt(uid.Substring(10, 1)) * 3) _
                    + (CInt(uid.Substring(11, 1)) * 2))))))))))))
        Dim LastID As Integer = (IntID Mod 11)
        If (LastID = 0) Then
            LastID = 1
        ElseIf (LastID = 1) Then
            LastID = 0
        Else
            LastID = (11 - LastID)
        End If
        Return (CInt(uid.Substring(12, 1)) = LastID)
    End Function

    Public Function FormatPersonalID(ByVal uid As String) As String
        Dim IDformat As String = ""
        IDformat = (uid.Substring(0, 1) + " ")
        IDformat = (IDformat _
                    + (uid.Substring(1, 1) _
                    + (uid.Substring(2, 1) _
                    + (uid.Substring(3, 1) _
                    + (uid.Substring(4, 1) + " ")))))
        IDformat = (IDformat _
                    + (uid.Substring(5, 1) _
                    + (uid.Substring(6, 1) _
                    + (uid.Substring(7, 1) _
                    + (uid.Substring(8, 1) _
                    + (uid.Substring(9, 1) + " "))))))
        IDformat = (IDformat _
                    + (uid.Substring(10, 1) _
                    + (uid.Substring(11, 1) + " ")))
        IDformat = (IDformat + uid.Substring(12, 1))
        Return IDformat
    End Function

    '#'*- Session
    Public Sub CheckSession(ByVal p As Page)
        If (Not (p.Session("UserID")) Is Nothing) Then
            Return
        End If
        p.Response.Redirect("default.aspx")
    End Sub

    Public Sub CheckGroup(ByVal p As Page, ByVal permission As String)
        If (Not (p.Session("UserLevel")) Is Nothing) Then
            If permission.Contains(p.Session("UserLevel").ToString) Then
                Return
            End If
        End If
        p.Session("ErrorMessage") = "<br />คุณไม่ได้รับสิทธิ์ให้ใช้งานหน้านี้"
        p.Session("SendToURL") = "default.aspx"
        p.Response.Redirect("ErrorHandler.aspx")
    End Sub

    '#'*-*44L@!9
    Public Function ShowMenu(ByVal p As String, ByVal permission As String) As Boolean
        If permission.Contains(p) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function toThaiNumber(ByVal num As String) As String
        Dim tmp As String = ""
        Dim i As Integer = 0

        For i = 0 To i < num.Length

            If (num.Substring(i, 1) = "0") Then
                tmp += "๐"
            ElseIf (num.Substring(i, 1) = "1") Then
                tmp += "๑"
            ElseIf (num.Substring(i, 1) = "2") Then
                tmp += "๒"
            ElseIf (num.Substring(i, 1) = "3") Then
                tmp += "๓"
            ElseIf (num.Substring(i, 1) = "4") Then
                tmp += "๔"
            ElseIf (num.Substring(i, 1) = "5") Then
                tmp += "๕"
            ElseIf (num.Substring(i, 1) = "6") Then
                tmp += "๖"
            ElseIf (num.Substring(i, 1) = "7") Then
                tmp += "๗"
            ElseIf (num.Substring(i, 1) = "8") Then
                tmp += "๘"
            ElseIf (num.Substring(i, 1) = "9") Then
                tmp += "๙"
            Else
                tmp += num.Substring(i, 1)
            End If

        Next
        Return tmp
    End Function

    Public Function CheckDateValid(ByVal dt As String, Optional Separate As String = "/") As Boolean
        Dim isValidate As Boolean = True
        If dt <> "" Then

            Dim y As Integer = 0
            Dim d As Integer = 0
            Dim m As Integer = 0


            Dim str() As String
            str = Split(dt, Separate)

            d = StrNull2Zero(str(0))
            m = StrNull2Zero(str(1))
            y = StrNull2Zero(str(2))

            If d > 0 Then
                Select Case m
                    Case 1, 3, 5, 7, 8, 10, 12
                        If d > 31 Then
                            isValidate = False
                        End If
                    Case 2
                        If d > 29 Then
                            isValidate = False
                        End If
                    Case 4, 6, 9, 11
                        If d > 30 Then
                            isValidate = False
                        End If
                    Case Else
                        isValidate = False
                End Select
            Else
                isValidate = False
            End If

            If y = 0 Or y < 2000 Then
                isValidate = False
            End If
        Else
            isValidate = False
        End If

        Return isValidate
    End Function


    Public Function ConvertDate2DBString(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d As String = ""
        Dim m As String = ""
        d = dt.Day.ToString
        m = dt.Month.ToString

        While (d.Length < 2)
            d = ("0" + d)

        End While

        While (m.Length < 2)
            m = ("0" + m)

        End While

        If dt.Year < 2300 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return y + m + d
    End Function

    Public Function ConvertStrDate2InformDateString(ByVal dt As String) As String
        If dt <> "" Then

            Dim y As String = ""
            Dim d As String = ""
            Dim m As String = ""

            Dim iY As Integer = 0

            Dim str() As String
            str = Split(dt, "/")

            d = str(0).ToString
            m = str(1).ToString

            While (d.Length < 2)
                d = ("0" + d)
            End While

            While (m.Length < 2)
                m = ("0" + m)
            End While
            
            y = str(2)
            iY = StrNull2Zero(str(2))
            Do Until iY < 2500
                iY = iY - 543
            Loop
            y = iY.ToString()

            'If CInt(str(2)) > 2500 Then
            '    y = (CInt(str(2)) - 543).ToString
            'Else
            '    y = str(2)
            'End If

            Return m + "/" + d + "/" + y

        Else
            Return ""
        End If
    End Function

    Public Function ConvertStrDate2DateQueryString(ByVal dt As String) As String
        If dt <> "" Then

            Dim y As String = ""
            Dim d As String = ""
            Dim m As String = ""

            Dim str() As String
            str = Split(dt, "/")

            d = str(0).ToString
            m = str(1).ToString

            While (d.Length < 2)
                d = ("0" + d)
            End While

            While (m.Length < 2)
                m = ("0" + m)
            End While

            If CInt(str(2)) > 2300 Then
                y = (CInt(str(2)) - 543).ToString
            Else
                y = str(2)
            End If

            Return y + "-" + m + "-" + d  '+ " 00:00:00"

        Else
            Return ""
        End If
    End Function

    Public Function ConvertStrDate2YYYYMMDD(ByVal dt As String) As String
        If dt <> "" Then

            Dim y As String = ""
            Dim d As String = ""
            Dim m As String = ""

            Dim str() As String
            str = Split(dt, "/")

            d = str(0).ToString
            m = str(1).ToString


            While (d.Length < 2)
                d = ("0" + d)
            End While

            While (m.Length < 2)
                m = ("0" + m)
            End While

            If CInt(str(2)) < 2300 Then
                y = (CInt(str(2)) + 543).ToString
            Else
                y = str(2)
            End If

            Return y + m + d
        Else
            Return ""
        End If
    End Function

    Public Function DisplayFullDateTH(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d1 As String = ""
        Dim d2 As String = ""
        Dim m As String = ""

        Select Case dt.DayOfWeek
            Case DayOfWeek.Sunday
                d1 = "อาทิตย์"
            Case DayOfWeek.Monday
                d1 = "จันทร์"
            Case DayOfWeek.Tuesday
                d1 = "อังคาร"
            Case DayOfWeek.Wednesday
                d1 = "พุธ"
            Case DayOfWeek.Thursday
                d1 = "พฤหัสบดี"
            Case DayOfWeek.Friday
                d1 = "ศุกร์"
            Case DayOfWeek.Saturday
                d1 = "เสาร์"

        End Select

        d2 = dt.Day.ToString

        Select Case dt.Month
            Case 1 : m = "มกราคม"
            Case 2 : m = "กุมภาพันธ์"
            Case 3 : m = "มีนาคม"
            Case 4 : m = "เมษายน"
            Case 5 : m = "พฤษภาคม"
            Case 6 : m = "มิถุนายน"
            Case 7 : m = "กรกฎาคม"
            Case 8 : m = "สิงหาคม"
            Case 9 : m = "กันยายน"
            Case 10 : m = "ตุลาคม"
            Case 11 : m = "พฤศจิกายน"
            Case 12 : m = "ธันวาคม"
        End Select

        If dt.Year < 2300 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return "วัน" + d1 + "ที่ " + d2 + " " + m + " พ.ศ. " + y

    End Function

    Public Function DisplayFullDateTHwithoutDOW(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d2 As String = ""
        Dim m As String = ""
        d2 = dt.Day.ToString

        Select Case dt.Month
            Case 1 : m = "มกราคม"
            Case 2 : m = "กุมภาพันธ์"
            Case 3 : m = "มีนาคม"
            Case 4 : m = "เมษายน"
            Case 5 : m = "พฤษภาคม"
            Case 6 : m = "มิถุนายน"
            Case 7 : m = "กรกฎาคม"
            Case 8 : m = "สิงหาคม"
            Case 9 : m = "กันยายน"
            Case 10 : m = "ตุลาคม"
            Case 11 : m = "พฤศจิกายน"
            Case 12 : m = "ธันวาคม"
        End Select

        If dt.Year < 2300 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return "วันที่  " + d2 + "   เดือน  " + m + "   พ.ศ. " + y
    End Function

    Public Function DisplayDateTH(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d2 As String = ""
        Dim m As String = ""
        d2 = dt.Day.ToString
        Select Case dt.Month
            Case 1 : m = "มกราคม"
            Case 2 : m = "กุมภาพันธ์"
            Case 3 : m = "มีนาคม"
            Case 4 : m = "เมษายน"
            Case 5 : m = "พฤษภาคม"
            Case 6 : m = "มิถุนายน"
            Case 7 : m = "กรกฎาคม"
            Case 8 : m = "สิงหาคม"
            Case 9 : m = "กันยายน"
            Case 10 : m = "ตุลาคม"
            Case 11 : m = "พฤศจิกายน"
            Case 12 : m = "ธันวาคม"
        End Select

        If dt.Year < 2300 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return d2 + " " + m + " " + y
    End Function

    Public Function Convert2LetterNo(ByVal dt As String) As String
        Dim y As String = ""
        Dim i As String = ""
        y = dt.Substring(0, 4)
        i = dt.Substring(4, 3)

        Return CInt(i) & "/" & y
    End Function



    Public Function DisplayStr2DateTH(ByVal dt As String) As String
        Dim y As String = ""
        Dim d2 As String = ""
        Dim m As String = ""
        d2 = CInt(dt.Substring(6, 2)).ToString
        If (d2 = "0") Then
            d2 = ""
        Else
            d2 = d2
        End If
        m = CInt(dt.Substring(4, 2)).ToString

        Select Case m
            Case "0" : m = ""
            Case "1" : m = "มกราคม"
            Case "2" : m = "กุมภาพันธ์"
            Case "3" : m = "มีนาคม"
            Case "4" : m = "เมษายน"
            Case "5" : m = "พฤษภาคม"
            Case "6" : m = "มิถุนายน"
            Case "7" : m = "กรกฎาคม"
            Case "8" : m = "สิงหาคม"
            Case "9" : m = "กันยายน"
            Case "10" : m = "ตุลาคม"
            Case "11" : m = "พฤศจิกายน"
            Case "12" : m = "ธันวาคม"
        End Select

        y = dt.Substring(0, 4)

        Return d2 + " " + m + " " + y

    End Function

    Public Function DisplayMiniDateTH(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d2 As String = ""
        Dim m As String = ""
        d2 = dt.Day.ToString

        Select Case dt.Month
            Case 1 : m = "ม.ค."
            Case 2 : m = "ก.พ."
            Case 3 : m = "มี.ค."
            Case 4 : m = "เม.ย."
            Case 5 : m = "พ.ค."
            Case 6 : m = "มิ.ค."
            Case 7 : m = "ก.ค."
            Case 8 : m = "ส.ค."
            Case 9 : m = "ก.ย."
            Case 10 : m = "ต.ค."
            Case 11 : m = "พ.ย."
            Case 12 : m = "ธ.ค."
        End Select

        If dt.Year < 2300 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return d2 + " " + m + " " + y

    End Function

    Public Function DisplayShortDateTH(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d As String = ""
        Dim m As String = ""
        d = dt.Day.ToString
        m = dt.Month.ToString

        While (d.Length < 2)
            d = ("0" + d)

        End While

        While (m.Length < 2)
            m = ("0" + m)

        End While
        If dt.Year < 2300 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If
        Return d + "/" + m + "/" + y
    End Function

    Public Function DisplayShortDateEN(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d As String = ""
        Dim m As String = ""

        d = dt.Day.ToString
        m = dt.Month.ToString

        While (d.Length < 2)
            d = ("0" + d)

        End While

        While (m.Length < 2)
            m = ("0" + m)

        End While
        y = dt.Year.ToString
        Return (d + ("/" _
                    + (m + ("/" + y))))
    End Function

    Public Function DisplayStr2ShortDateEN(ByVal dt As String) As String
        Dim dd As String = dt.Substring(6, 2)
        Dim mm As String = dt.Substring(4, 2)
        Dim yy As String = dt.Substring(0, 4)

        While (dd.Length < 2)
            dd = ("0" + dd)

        End While

        While (mm.Length < 2)
            mm = ("0" + mm)

        End While

        Return dd + "/" + mm + "/" + yy
    End Function

    Public Function DisplayStr2ShortDateTH(ByVal dt As String) As String
        If dt <> "" And dt <> "0" Then
            Dim dd As String = dt.Substring(6, 2)
            Dim mm As String = dt.Substring(4, 2)
            Dim yy As String = dt.Substring(0, 4)

            While (dd.Length < 2)
                dd = ("0" + dd)

            End While

            While (mm.Length < 2)
                mm = ("0" + mm)

            End While
            If CInt(yy) < 2300 Then
                yy = (CInt(yy) + 543).ToString
            End If

            Return dd + "/" + mm + "/" + yy
        Else
            Return ""
        End If

    End Function
    Public Function DisplayRangeDateTH(ByVal dt1 As Date, ByVal dt2 As Date) As String
        Dim y1 As Integer = dt1.Year
        Dim d1 As Integer = dt1.Day
        Dim m1 As Integer = dt1.Month
        Dim y2 As Integer = dt2.Year
        Dim d2 As Integer = dt2.Day
        Dim m2 As Integer = dt2.Month
        Dim strDateRange As String = ""

        If dt1 = dt2 Then
            strDateRange = DisplayDateTH(dt1)
        Else
            If y1 = y2 Then
                If m1 = m2 Then
                    strDateRange = d1.ToString() + "-" + d2.ToString() + " " + DisplayMonth(dt1) + " " + DisplayYear(dt1)
                Else
                    strDateRange = d1.ToString() + " " + DisplayMonth(dt1) + "-" + d2.ToString() + " " + DisplayMonth(dt2) + " " + DisplayYear(dt1)
                End If
            Else
                strDateRange = d1.ToString() + " " + DisplayMonth(dt1) + " " + DisplayYear(dt1)
                strDateRange &= "-" + d2.ToString() + " " + DisplayMonth(dt2) + " " + DisplayYear(dt2)
            End If
        End If
        Return strDateRange
    End Function


    Public Function DisplayTime(ByVal dt As Date) As String
        Dim m As String
        Dim h As String
        h = dt.Hour.ToString
        m = dt.Minute.ToString

        While (m.Length < 2)
            m = ("0" + m)

        End While
        Return (h + ("." _
                    + (m + " .")))
    End Function
    Public Function ConvertTimeToString(ByVal dt As Date) As String
        Dim m As String
        Dim h As String
        h = dt.Hour.ToString
        m = dt.Minute.ToString

        While (m.Length < 2)
            m = ("0" + m)
        End While
        Return h & m

    End Function

    Public Function DisplayPhone(ByVal s As String) As String
        If (s.Length = 9) Then
            If (s.Substring(0, 2) = "02") Then
                Return (s.Substring(0, 2) + ("-" _
                            + (s.Substring(2, 3) + ("-" + s.Substring(5, 4)))))
            Else
                Return (s.Substring(0, 3) + ("-" _
                            + (s.Substring(3, 3) + ("-" + s.Substring(6, 3)))))
            End If
        End If
        If (s.Length = 10) Then
            Return (s.Substring(0, 3) + ("-" _
                        + (s.Substring(3, 3) + ("-" + s.Substring(6, 4)))))
        End If
        Return s
    End Function

    Public Function DisplayGenderName(ByVal s As String) As String
        If s = "M" Or s = "1" Then
            Return "ชาย"
        ElseIf s = "F" Or s = "2" Then
            Return "หญิง"
        Else
            Return ""
        End If
    End Function

    Public Function DisplayUsername(ByVal s As String) As String
        If Not New Regex("^([0-9]{13})$").IsMatch(s) Then
            Return s
        End If
        Return (s.Substring(0, 1) + ("-" _
                    + (s.Substring(1, 4) + ("-" _
                    + (s.Substring(5, 5) + ("-" _
                    + (s.Substring(10, 2) + ("-" + s.Substring(12, 1)))))))))
    End Function

    Public Function DisplayDay(ByVal dt As Date) As String
        Dim d As String = ""
        d = dt.Day.ToString
        Return d
    End Function
    Public Function DisplayNumber2Month(ByVal pM As Integer) As String
        Dim m As String = ""
        Select Case pM
            Case 1 : m = "มกราคม"
            Case 2 : m = "กุมภาพันธ์"
            Case 3 : m = "มีนาคม"
            Case 4 : m = "เมษายน"
            Case 5 : m = "พฤษภาคม"
            Case 6 : m = "มิถุนายน"
            Case 7 : m = "กรกฎาคม"
            Case 8 : m = "สิงหาคม"
            Case 9 : m = "กันยายน"
            Case 10 : m = "ตุลาคม"
            Case 11 : m = "พฤศจิกายน"
            Case 12 : m = "ธันวาคม"
        End Select

        Return m
    End Function
    Public Function DisplayMonth(ByVal dt As Date) As String
        Dim m As String = ""
        Select Case dt.Month
            Case 1 : m = "มกราคม"
            Case 2 : m = "กุมภาพันธ์"
            Case 3 : m = "มีนาคม"
            Case 4 : m = "เมษายน"
            Case 5 : m = "พฤษภาคม"
            Case 6 : m = "มิถุนายน"
            Case 7 : m = "กรกฎาคม"
            Case 8 : m = "สิงหาคม"
            Case 9 : m = "กันยายน"
            Case 10 : m = "ตุลาคม"
            Case 11 : m = "พฤศจิกายน"
            Case 12 : m = "ธันวาคม"
        End Select

        Return m
    End Function
    Public Function DisplayStr2Day(ByVal dt As String) As String
        Dim d As String = ""
        d = Val(dt)
        Return d
    End Function

    Public Function DisplayYear(ByVal dt As Date) As String
        Dim y As String = ""

        If dt.Year < 2300 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return y
    End Function

    Public Function ChkNull(ByVal str As String) As String
        If (str <> "") Then
            Return (" " + str)
        Else
            Return " -"
        End If
    End Function


    Public Function TRcolor(ByVal row As Integer) As String
        Dim tr As String = ""
        If ((row Mod 2) _
                    <> 0) Then
            tr = "<tr onMouseOver=\""this.bgColor='#DDDDDD';\"" onMouseOut=\""this.bgColor='#FFFFFF';\"">"
        Else
            tr = "<tr onMouseOver=\""this.bgColor='#DDDDDD';\"" onMouseOut=\""this.bgColor='#FFFFFF';\"">"
        End If
        Return tr
    End Function





#End Region

    Public Function NavigateURL(ByVal pageID As String, ByVal ParamArray AdditionalParameters() As String) As String
        Dim str As String = ""

        For i = 0 To AdditionalParameters.Length - 1
            If i Mod 2 = 0 Then
                str &= "&" & AdditionalParameters(i) & "="
            Else
                str &= AdditionalParameters(i)
            End If

        Next

        Return pageID & "?x=1" & str

    End Function



End Module