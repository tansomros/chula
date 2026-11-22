Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Web
Public Module modGlobal
#Region "Declaration Parameter"
   

    ' Public ProjectID As String
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


    Public RegName As String
    Public RegSurname As String
    Public RegID As Integer

    Public RegType As String
    Public RegCardID As String

    'Public DateFormat As IFormatProvider = New System.Globalization.CultureInfo("en-US")

    Public DateFormat_TH As IFormatProvider = New System.Globalization.CultureInfo("th-TH")

    Public dtfInfo As DateTimeFormatInfo

    Public pnV As Boolean = False
    Public Rate_ExBed_Adult As Double = 0.0
    Public Rate_ExBed_Child As Double = 0.0

    Public bActive As Integer = 1
    Public binActive As Integer = 0

    Public stdPic As String = ConfigurationSettings.AppSettings("PictureStd")
    Public personPic As String = ConfigurationSettings.AppSettings("PicturePsn")
    Public tmpUpload As String = ConfigurationSettings.AppSettings("tmpUpload")
    Public ReportPath As String = ConfigurationSettings.AppSettings("ReportPath")

    'Public ReportsName As String = ""
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
    Private Const ModuleQualifier As String = ""

    Private _providerPath As String
    Private _objectQualifier As String
    Private _databaseOwner As String

#End Region

#Region "Const_Data"
    Public Const ACTTYPE_LOG As String = "LOGIN"
    Public Const ACTTYPE_ADD As String = "ADD"
    Public Const ACTTYPE_UPD As String = "UPDATE"
    Public Const ACTTYPE_DEL As String = "DELETE"
    Public Const ACTTYPE_FND As String = "FIND"
    Public Const ACTTYPE_FGT As String = "FORGOT PASS"
    Public Const ACTTYPE_PRC As String = "PROCESS"

    Public Const CFG_EDUYEAR As String = "EDUYEAR"          'ปีการศึกษาปัจจุบัน
    Public Const CFG_ASSMYEAR As String = "ASSMYEAR"          'ปีการศึกษาสำหรับประเมิน

    Public Const CFG_STARTDATE As String = "STARTDATE"      'วันที่เริ่มเปิดให้นิสิตเลือกแหล่งฝึกร้านยาปี 6
    Public Const CFG_ENDDATE As String = "ENDDATE"          'วันสิ้นสุดให้นิสิตเลือกแหล่งฝึกร้านยาปี

    Public Const CFG_MAXLOCATION As String = "MAXLOCATION"  'จำนวนสูงสุดของแหล่งฝึกที่นิสิตสามารถเลือกได้ต่อ 1 รายวิชา
    Public Const CFG_RESULTDATE As String = "RESULTDATE"    'วันที่ประกาศผลการคัดเลือก ร้านยาแหล่งฝึก

    Public Const CFG_STARTASSM As String = "STARTASSM"        'วันที่เริ่มเปิดให้ประเมินนิสิต
    Public Const CFG_ENDASSM As String = "ENDASSM"          'วันที่สิ้นสุดการประเมินนิสิต

    Public Const CFG_MINSCORE As String = "MINSCORE"         'คะแนนประเมินต่ำสุด
    Public Const CFG_MAXSCORE As String = "MAXSCORE"         'คะแนนประเมินสูงสุด

    Public Const CFG_STARTASSM_LOC As String = "STARTASSMLOC"        'วันที่เริ่มเปิดให้ประเมินนิสิต
    Public Const CFG_ENDASSM_LOC As String = "ENDASSMLOC"          'วันที่สิ้นสุดการประเมินนิสิต

    Public Const CFG_PC2_MAXSUBJECT As String = "PC2MAXSUBJECT"

    Public Const CFG_PC2_STARTDATE As String = "PC2STARTDATE"      'วันที่เริ่มเปิดให้นิสิตเลือกแหล่งฝึกร้านยาปี 6
    Public Const CFG_PC2_ENDDATE As String = "PC2ENDDATE"          'วันสิ้นสุดให้นิสิตเลือกแหล่งฝึกร้านยาปี

    Public Const CFG_CERTDATE As String = "CERTDATE"          'วันที่เปิดให้นิสิตโหลดใบรับรอง
    Public Const CFG_HEADNAME As String = "HEADNAME"          'หัวหน้าหน่วยฝึกปฏิบัติงานวิชาชีพ
    Public Const CFG_DEANNAME As String = "DEANNAME"          'ชื่อคณบดี คณะเภสัชศาสตร์

    Public Const PROMINENT_STRENGTHS As String = "S"
    Public Const PROMINENT_WEAKNESSES As String = "W"

    'Public Const nbGroupBiography As Integer = 0
    'Public Const nbGroupStudentSelection As Integer = 1
    'Public Const nbGroupSupervision As Integer = 2
    'Public Const nbGroupStudent As Integer = 3
    'Public Const nbGroupTeacher As Integer = 4
    'Public Const nbGroupSubject As Integer = 5
    'Public Const nbGroupLocation As Integer = 6
    'Public Const nbGroupMainYearData As Integer = 7
    'Public Const nbGroupPicking As Integer = 8
    'Public Const nbGroupSelection As Integer = 9
    'Public Const nbGroupReport As Integer = 10
    'Public Const nbGroupConfiguration As Integer = 11
    'Public Const nbGroupUserManage As Integer = 12
    'Public Const nbGroupNews As Integer = 13

#Region "Running Code"
    Public Const CODE_SUPERVISION As String = "S"

    'Public Const CODE_CHECKUP As String = "CHK"
    'Public Const CODE_FRR As String = "FR"
    'Public Const CODE_ACCIDENT As String = "ACD"
    'Public Const CODE_BOC As String = "BO"
    'Public Const CODE_RTW As String = "RW"

    'Public Const CODE_COMPANY As String = "C"
    'Public Const CODE_HOSPITAL As String = "H"
    'Public Const CODE_POSITION As String = "J"
    Public Const CODE_DRUG As String = "MED"

#End Region

#End Region

#Region "Genaral Function"

    Public Function chkFileExist(ByVal FileRptPath As String) As Boolean
        Dim F As New FileInfo(FileRptPath)
        Return F.Exists
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
        Dim str() As String
        Dim dY As Integer

        If sKey = "-" Then
            str = Split(sDate, "-")
        ElseIf sKey = "/" Then
            str = Split(sDate, "/")
        End If

        dY = Now.Date.Year - CInt(str(2))
        If dY < 0 Then
            dY = CInt(str(2)) - 543
        Else
            dY = str(2)
        End If
        sDate = str(0) & "/" & str(1) & "/" & dY
        Return sDate
    End Function

    Public Function ParseDateToSQL(ByVal sDate As String) As String
        Dim str() As String
        Dim dY As Integer

        str = Split(sDate, "/")

        dY = Now.Date.Year - CInt(str(2))
        If dY < 0 Then
            dY = CInt(str(2)) - 543
        Else
            dY = str(2)
        End If
        sDate = dY & "-" & str(1) & "-" & str(0)
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
        If dY < 0 Then
            dY = CInt(str(2)) - 543
        Else
            dY = str(2)
        End If
        sDate = dY & "-" & str(1) & "-" & str(0)
        Return sDate
    End Function
    Public Function ConvertStringDateToDate(ByVal sDate As String) As Date
        Dim str() As String
        Dim dY As Integer
        str = Split(sDate, "/")
        dY = CInt(str(2))

        If dY > 2500 Then
            dY = dY
        Else
            dY = str(2)
        End If
        sDate = str(0) & "/" & str(1) & "/" & dY
        Return sDate
    End Function

    Public Function isStringFormatDate(ByVal sDate As String, KeyString As String) As Boolean
        Dim str() As String
        Dim Valid As Boolean = True
        str = Split(sDate, KeyString)

        If str(0) = "" Or (StrNull2Zero(str(0)) = 0 Or StrNull2Zero(str(0)) > 31) Then
            Valid = False
        End If
        If str(1) = "" Or (StrNull2Zero(str(1)) = 0 Or StrNull2Zero(str(1)) > 12) Then
            Valid = False
        End If
        If str(2) = "" Or (StrNull2Zero(str(2)) = 0 Or StrNull2Zero(str(2)) < 2500) Then
            Valid = False
        End If

        If Len(str(0)) < 2 Then
            Valid = False
        End If
        If Len(str(1)) < 2 Then
            Valid = False
        End If
        If Len(str(2)) < 4 Then
            Valid = False
        End If

        Return Valid
    End Function

    Public Function ConvertDateToString(ByVal Adate As Date) As String
        Dim strDate(2) As String
        strDate(0) = CStr(Adate.Day)
        strDate(1) = CStr(Adate.Month)
        strDate(2) = CStr(Adate.Year)

        ConvertDateToString = strDate(0) & "/" & strDate(1) & "/" & CInt(strDate(2)) + 543

        Return ConvertDateToString

    End Function

    Public Function ConvertFormateDate(ByVal Adate As Date) As String
        Dim strDate(2) As String
        strDate(0) = CStr(Adate.Day)
        strDate(1) = CStr(Adate.Month)
        strDate(2) = CStr(Adate.Year)

        ConvertFormateDate = strDate(1) & "/" & strDate(0) & "/" & strDate(2)

        Return ConvertFormateDate

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
        If str = "" Or str = "NaN" Or IsNumeric(str) = False Then
            Return 0
        Else
            If Not IsNumeric(str) Then
                Return 0
            Else
                Return CInt(str)
            End If
        End If
    End Function
    Public Function StrNull2Double(ByVal str As String) As Double
        If str = "" Or str = "NaN" Then
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
            If Not IsNumeric(str) Then
                Return 0
            Else
                Return CInt(str)
            End If
        End If
    End Function
    Public Function DBNull2Lng(ByVal str As Object) As Long
        If IsDBNull(str) Then
            Return 0
        Else
            If str.ToString() <> "" Then
                Return CInt(str)
            Else
                Return 0
            End If
        End If
    End Function
    Public Function DBNull2Dbl(ByVal str As Object) As Double
        If IsDBNull(str) Then
            Return 0
        Else
            If str.ToString() <> "" Then
                Return CDbl(str)
            Else
                Return 0
            End If
        End If
    End Function


    Public Sub UploadFile(ByVal Fileupload As Object, ByVal prmPath As String)
        Dim FileFullName As String = Fileupload.PostedFile.FileName
        Dim FileNameInfo As String = Path.GetFileName(FileFullName)
        Dim filename As String = Path.GetFileName(Fileupload.PostedFile.FileName)
        Dim objfile As FileInfo = New FileInfo(prmPath)
        If FileNameInfo <> "" Then
            Fileupload.PostedFile.SaveAs(objfile.DirectoryName & "\" & filename)
        End If
        objfile = Nothing
    End Sub


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

    Public Sub CheckSession(ByVal p As Page)
        If (Not (p.Request.Cookies("UserID").Value) Is Nothing) Then
            Return
        End If
        p.Response.Redirect("default.aspx")
    End Sub

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
    Public Function ConvertStatusFlag2Boolean(ByVal pVal As String) As Boolean
        If pVal = "A" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ConvertStatusFlag2Chk(ByVal pVal As String) As Boolean
        If pVal = "A" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ConvertYN2Boolean(ByVal pVal As String) As Boolean
        If pVal = "Y" Then
            Return True
        Else
            Return False
        End If
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

        If dt.Year < 2500 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return y + m + d
    End Function
    Public Function SetStrDate2DBDateFormat(ByVal dt As String) As String
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

            iY = StrNull2Zero(str(2))
            Do Until iY < 2100
                iY = iY - 543
            Loop
            y = iY.ToString

            Return m + "/" + d + "/" + y
        Else
            Return ""
        End If
    End Function

    Public Function ConvertStrDate2DBString(ByVal dt As String) As String
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

            If CInt(str(2)) < 2500 Then
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

        If dt.Year < 2500 Then
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

        If dt.Year < 2500 Then
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

        If dt.Year < 2500 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return d2 + " " + m + " " + y
    End Function
    Public Function DisplayLongDateNoYearTH(ByVal dt As Date) As String
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

        Return d2 + " " + m
    End Function

    Public Function DisplayLongDateTH(ByVal dt As Date) As String
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

        If dt.Year < 2100 Then
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
            Case 6 : m = "มิ.ย."
            Case 7 : m = "ก.ค."
            Case 8 : m = "ส.ค."
            Case 9 : m = "ก.ย."
            Case 10 : m = "ต.ค."
            Case 11 : m = "พ.ย."
            Case 12 : m = "ธ.ค."
        End Select

        If dt.Year < 2500 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return d2 + " " + m + " " + y

    End Function
    Public Function DisplayMiniDateNoYear(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d As String = ""
        Dim m As String = ""
        d = dt.Day.ToString

        Select Case dt.Month
            Case 1 : m = "ม.ค."
            Case 2 : m = "ก.พ."
            Case 3 : m = "มี.ค."
            Case 4 : m = "เม.ย."
            Case 5 : m = "พ.ค."
            Case 6 : m = "มิ.ย."
            Case 7 : m = "ก.ค."
            Case 8 : m = "ส.ค."
            Case 9 : m = "ก.ย."
            Case 10 : m = "ต.ค."
            Case 11 : m = "พ.ย."
            Case 12 : m = "ธ.ค."
        End Select

        Return d + " " + m

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
        If dt.Year < 2500 Then
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
        Return (d + ("/" + (m + ("/" + y))))
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
        If dt <> "" Then
            Dim dd As String = dt.Substring(6, 2)
            Dim mm As String = dt.Substring(4, 2)
            Dim yy As String = dt.Substring(0, 4)

            While (dd.Length < 2)
                dd = ("0" + dd)

            End While

            While (mm.Length < 2)
                mm = ("0" + mm)

            End While
            If CInt(yy) < 2500 Then
                yy = (CInt(yy) + 543).ToString
            End If

            Return dd + "/" + mm + "/" + yy
        Else
            Return ""
        End If

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
                    + (m + "")))
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

        If dt.Year < 2500 Then
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





End Module
