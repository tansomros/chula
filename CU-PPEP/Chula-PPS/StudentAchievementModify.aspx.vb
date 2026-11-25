Imports System.Data
Imports System.Globalization
Imports System.Web

Public Class StudentAchievementModify
    Inherits System.Web.UI.Page

    Private ReadOnly ctlAch As New StudentAchievementController()
    Private ReadOnly ctlStd As New StudentController()
    Private ReadOnly ctlUser As New UserController()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If GetCookieValue("Username") = "" Then
            Response.Redirect("Default.aspx?logout=YES")
            Exit Sub
        End If

        If Not IsPostBack Then
            BindYearDropdown()
            ConfigureBackLinks()

            Dim itemId As Integer = StrNull2Zero(Request.QueryString("id"))
            Dim mode As String = If(Request.QueryString("t"), "new").Trim().ToLowerInvariant()

            If itemId > 0 Then
                LoadExistingRecord(itemId, mode)
            Else
                InitializeNewForm()
            End If
        End If
    End Sub

    Private Sub BindYearDropdown()
        ddlAcademicYear.Items.Clear()
        Dim currentYearBe As Integer = GetCurrentAcademicYearBe()
        For y As Integer = currentYearBe + 1 To currentYearBe - 10 Step -1
            ddlAcademicYear.Items.Add(New ListItem(y.ToString(), y.ToString()))
        Next
        If ddlAcademicYear.Items.FindByValue(currentYearBe.ToString()) IsNot Nothing Then
            ddlAcademicYear.SelectedValue = currentYearBe.ToString()
        End If
    End Sub

    Private Sub ConfigureBackLinks()
        Dim backUrl As String = BuildListUrl(False)
        hlnkBack.NavigateUrl = backUrl
        hlnkBackTop.NavigateUrl = backUrl
    End Sub

    Private Sub InitializeNewForm()
        If Not CanCurrentUserModify() Then
            Response.Redirect(BuildListUrl(False))
            Exit Sub
        End If

        lblPageTitle.Text = "บันทึกผลงานนิสิต"
        lblModeBadge.Text = "เพิ่มรายการใหม่"
        lblModeBadge.CssClass = "ach-mode-badge"
        lblBoxTitle.Text = "กรอกข้อมูลการแข่งขัน การเข้าร่วมกิจกรรม ทุนสนับสนุน และรางวัลที่ได้รับ"
        hdAchievementId.Value = "0"
        txtEventDate.Text = DisplayShortDateTH(DateTime.Now)
        chkSelfConfirm.Checked = True
        cmdDel.Visible = False
        hlnkEdit.Visible = False

        If IsRole("ROLE_STD") Then
            pnStudentInput.Visible = False
            BindStudentSummary(ResolveStudentOwnCode())
        ElseIf Request.QueryString("stdcode") IsNot Nothing AndAlso Request.QueryString("stdcode").Trim() <> "" Then
            pnStudentInput.Visible = False
            BindStudentSummary(Request.QueryString("stdcode").Trim())
        Else
            pnStudentInput.Visible = True
            lblStudentCode.Text = "-"
            lblStudentName.Text = "กรุณาระบุรหัสนิสิตด้านบนแล้วคลิก 'ดึงข้อมูลนิสิต'"
            lblMajorName.Text = "-"
        End If
    End Sub

    Private Sub BindStudentSummary(ByVal stdCode As String)
        If String.IsNullOrWhiteSpace(stdCode) Then Exit Sub

        Dim dtStd As DataTable = ctlStd.Student_GetByCode(stdCode.Trim())
        If dtStd.Rows.Count > 0 Then
            Dim row As DataRow = dtStd.Rows(0)
            lblStudentCode.Text = String.Concat(row("Student_Code")).Trim()
            txtStudentCodeInput.Text = lblStudentCode.Text
            lblStudentName.Text = (String.Concat(row("Prefix")) & String.Concat(row("FirstName")) & " " & String.Concat(row("LastName"))).Trim()
            lblMajorName.Text = If(String.Concat(row("MajorName")).Trim() <> "", String.Concat(row("MajorName")).Trim(), "-")

            Dim levelVal As Integer = DBNull2Zero(row("LevelClass"))
            If levelVal >= 1 AndAlso levelVal <= 6 AndAlso StrNull2Zero(hdAchievementId.Value) = 0 Then
                ddlStudyYear.SelectedValue = levelVal.ToString()
            End If
        Else
            lblStudentCode.Text = stdCode.Trim()
            lblStudentName.Text = If(IsRole("ROLE_STD"), GetCookieValue("NameOfUser"), "ไม่พบรหัสนิสิตในระบบ")
            lblMajorName.Text = "-"
        End If
    End Sub

    Private Sub LoadExistingRecord(ByVal achievementId As Integer, ByVal mode As String)
        Dim ownerGuard As String = If(IsRole("ROLE_STD"), ResolveStudentOwnCode(), "")
        Dim dt As DataTable = ctlAch.Achievement_GetById(achievementId, ownerGuard)

        If dt.Rows.Count = 0 Then
            Response.Redirect(BuildListUrl(False))
            Exit Sub
        End If

        Dim row As DataRow = dt.Rows(0)
        hdAchievementId.Value = Convert.ToString(row("AchievementId"))
        pnStudentInput.Visible = False

        lblStudentCode.Text = String.Concat(row("StudentCode")).Trim()
        lblStudentName.Text = If(String.Concat(row("StudentName")).Trim() <> "", String.Concat(row("StudentName")).Trim(), lblStudentCode.Text)
        lblMajorName.Text = If(String.Concat(row("MajorName")).Trim() <> "", String.Concat(row("MajorName")).Trim(), "-")

        Dim yearStr As String = Convert.ToString(row("AcademicYearBe"))
        EnsureDropdownOption(ddlAcademicYear, yearStr, yearStr)
        ddlAcademicYear.SelectedValue = yearStr

        Dim studyYearStr As String = Convert.ToString(row("StudyYear"))
        EnsureDropdownOption(ddlStudyYear, "ชั้นปีที่ " & studyYearStr, studyYearStr)
        ddlStudyYear.SelectedValue = studyYearStr

        Dim actType As String = String.Concat(row("ActivityType")).Trim()
        If actType <> "" Then
            EnsureDropdownOption(ddlActivityType, actType, actType)
            ddlActivityType.SelectedValue = actType
        End If

        If Not IsDBNull(row("EventDate")) Then
            txtEventDate.Text = DisplayShortDateTH(Convert.ToDateTime(row("EventDate")))
        End If

        txtEventName.Text = String.Concat(row("EventName"))
        txtEventNameEn.Text = String.Concat(row("EventNameEn"))
        txtAwardName.Text = String.Concat(row("AwardName"))
        txtAwardNameEn.Text = String.Concat(row("AwardNameEn"))
        txtOrganizerName.Text = String.Concat(row("OrganizerName"))

        If Not IsDBNull(row("GrantAmount")) Then
            txtGrantAmount.Text = Convert.ToDecimal(row("GrantAmount")).ToString("0.##")
        Else
            txtGrantAmount.Text = ""
        End If

        txtRemark.Text = String.Concat(row("Remark"))
        chkSelfConfirm.Checked = If(IsDBNull(row("IsSelfConfirmed")), True, Convert.ToBoolean(row("IsSelfConfirmed")))

        Dim isViewOnly As Boolean = (mode = "view") OrElse (Not CanCurrentUserModify())
        If isViewOnly Then
            lblPageTitle.Text = "รายละเอียดผลงานนิสิต"
            lblModeBadge.Text = "ดูรายละเอียด"
            lblModeBadge.CssClass = "ach-mode-badge ach-mode-badge-view"
            lblBoxTitle.Text = "ตรวจสอบรายละเอียดข้อมูลผลงานและรางวัลนิสิต"
            SetFormReadOnly(True)
            cmdSave.Visible = False
            cmdDel.Visible = False

            If CanCurrentUserModify() Then
                Dim actionType As String = If(Request.QueryString("ActionType") <> "", Request.QueryString("ActionType"), "port")
                hlnkEdit.Visible = True
                hlnkEdit.NavigateUrl = "StudentAchievementModify.aspx?ActionType=" & HttpUtility.UrlEncode(actionType) & "&t=mod&id=" & achievementId.ToString()
            End If
        Else
            lblPageTitle.Text = "แก้ไขผลงานนิสิต"
            lblModeBadge.Text = "แก้ไขข้อมูล"
            lblModeBadge.CssClass = "ach-mode-badge ach-mode-badge-edit"
            lblBoxTitle.Text = "ปรับปรุงรายละเอียดผลงานและรางวัลนิสิต"
            SetFormReadOnly(False)
            cmdSave.Visible = True
            cmdDel.Visible = True
            hlnkEdit.Visible = False
        End If
    End Sub

    Private Sub SetFormReadOnly(ByVal readOnlyMode As Boolean)
        ddlAcademicYear.Enabled = Not readOnlyMode
        ddlStudyYear.Enabled = Not readOnlyMode
        ddlActivityType.Enabled = Not readOnlyMode
        txtEventDate.ReadOnly = readOnlyMode
        txtEventName.ReadOnly = readOnlyMode
        txtEventNameEn.ReadOnly = readOnlyMode
        txtAwardName.ReadOnly = readOnlyMode
        txtAwardNameEn.ReadOnly = readOnlyMode
        txtOrganizerName.ReadOnly = readOnlyMode
        txtGrantAmount.ReadOnly = readOnlyMode
        txtRemark.ReadOnly = readOnlyMode
        chkSelfConfirm.Enabled = Not readOnlyMode
    End Sub

    Protected Sub cmdLookupStudent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdLookupStudent.Click
        If txtStudentCodeInput.Text.Trim() = "" Then
            ShowWarning("กรุณาระบุรหัสนิสิตก่อนค้นหา")
            Exit Sub
        End If
        BindStudentSummary(txtStudentCodeInput.Text.Trim())
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSave.Click
        If Not CanCurrentUserModify() Then
            ShowWarning("ท่านไม่มีสิทธิ์บันทึกหรือแก้ไขข้อมูล")
            Exit Sub
        End If

        Dim studentCode As String = lblStudentCode.Text.Trim()
        If (studentCode = "" OrElse studentCode = "-") AndAlso pnStudentInput.Visible Then
            studentCode = txtStudentCodeInput.Text.Trim()
        End If

        If studentCode = "" OrElse studentCode = "-" Then
            ShowWarning("กรุณาระบุรหัสนิสิตให้ถูกต้อง")
            Exit Sub
        End If

        If Not IsRole("ROLE_STD") AndAlso ctlStd.Student_GetByCode(studentCode).Rows.Count = 0 Then
            ShowWarning("ไม่พบรหัสนิสิต " & studentCode & " ในระบบ กรุณาตรวจสอบอีกครั้ง")
            Exit Sub
        End If

        If txtEventName.Text.Trim() = "" Then
            ShowWarning("กรุณาระบุชื่องานแข่งขัน / โครงการ / กิจกรรมที่เข้าร่วม")
            Exit Sub
        End If

        If txtAwardName.Text.Trim() = "" Then
            ShowWarning("กรุณาระบุรางวัลที่ได้รับ หรือผลการเข้าร่วม")
            Exit Sub
        End If

        Dim parsedEventDate As Date
        If Not TryParseInputDate(txtEventDate.Text, parsedEventDate) Then
            ShowWarning("กรุณาระบุวัน/เดือน/ปีที่จัดงานในรูปแบบ วว/ดด/ปปปป เช่น 15/08/2569")
            Exit Sub
        End If

        Dim grantAmount As Nullable(Of Decimal) = Nothing
        If txtGrantAmount.Text.Trim() <> "" Then
            Dim decVal As Decimal
            Dim cleanNum As String = txtGrantAmount.Text.Trim().Replace(",", "")
            If Not Decimal.TryParse(cleanNum, NumberStyles.Number, CultureInfo.InvariantCulture, decVal) OrElse decVal < 0D Then
                ShowWarning("จำนวนเงินสนับสนุน/ทุน ต้องเป็นตัวเลขที่มีค่าตั้งแต่ 0 ขึ้นไป")
                Exit Sub
            End If
            grantAmount = Math.Round(decVal, 2)
        End If

        If Not chkSelfConfirm.Checked Then
            ShowWarning("กรุณาทำเครื่องหมายรับรองความถูกต้องของข้อมูลก่อนบันทึก")
            Exit Sub
        End If

        Dim currentId As Integer = StrNull2Zero(hdAchievementId.Value)
        Dim academicYearBe As Integer = StrNull2Zero(ddlAcademicYear.SelectedValue)
        Dim studyYear As Integer = StrNull2Zero(ddlStudyYear.SelectedValue)
        If studyYear < 1 OrElse studyYear > 6 Then studyYear = 1

        Dim ownerGuard As String = If(IsRole("ROLE_STD"), ResolveStudentOwnCode(), "")
        Dim actor As String = GetCookieValue("Username")

        Dim savedId As Integer = ctlAch.Achievement_Save(
            currentId,
            studentCode,
            academicYearBe,
            studyYear,
            ddlActivityType.SelectedValue,
            txtEventName.Text,
            txtEventNameEn.Text,
            txtAwardName.Text,
            txtAwardNameEn.Text,
            txtOrganizerName.Text,
            parsedEventDate,
            grantAmount,
            txtRemark.Text,
            chkSelfConfirm.Checked,
            actor,
            ownerGuard)

        If savedId > 0 Then
            Dim actTypeLog As String = If(currentId <= 0, ACTTYPE_ADD, ACTTYPE_UPD)
            ctlUser.User_GenLogfile(actor, actTypeLog, "StudentAchievements", "บันทึกผลงานนิสิต ID=" & savedId.ToString() & " | " & studentCode & " | " & txtEventName.Text.Trim(), "")
            Response.Redirect(BuildListUrl(True))
        Else
            ShowWarning("ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบข้อมูลและลองใหม่อีกครั้ง")
        End If
    End Sub

    Protected Sub cmdDel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDel.Click
        Dim currentId As Integer = StrNull2Zero(hdAchievementId.Value)
        If currentId <= 0 OrElse Not CanCurrentUserModify() Then Exit Sub

        Dim ownerGuard As String = If(IsRole("ROLE_STD"), ResolveStudentOwnCode(), "")
        Dim actor As String = GetCookieValue("Username")

        If ctlAch.Achievement_Delete(currentId, actor, ownerGuard) Then
            ctlUser.User_GenLogfile(actor, ACTTYPE_DEL, "StudentAchievements", "ลบผลงานนิสิต ID=" & currentId.ToString(), "")
            Response.Redirect(BuildListUrl(False))
        Else
            ShowWarning("ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบสิทธิ์และลองใหม่อีกครั้ง")
        End If
    End Sub

    Private Shared Function TryParseInputDate(ByVal rawText As String, ByRef resultDate As Date) As Boolean
        If String.IsNullOrWhiteSpace(rawText) Then Return False

        Dim cleaned As String = rawText.Trim().Replace("-", "/")
        Dim parts() As String = cleaned.Split("/"c)
        If parts.Length <> 3 Then Return False

        Dim p0, p1, p2 As Integer
        If Not Integer.TryParse(parts(0), p0) OrElse Not Integer.TryParse(parts(1), p1) OrElse Not Integer.TryParse(parts(2), p2) Then
            Return False
        End If

        Dim dayVal, monthVal, yearVal As Integer
        If parts(0).Length = 4 Then
            yearVal = p0
            monthVal = p1
            dayVal = p2
        Else
            dayVal = p0
            monthVal = p1
            yearVal = p2
        End If

        While yearVal >= 2400
            yearVal -= 543
        End While

        If yearVal < 1950 OrElse yearVal > 2100 Then Return False
        If monthVal < 1 OrElse monthVal > 12 Then Return False
        If dayVal < 1 OrElse dayVal > DateTime.DaysInMonth(yearVal, monthVal) Then Return False

        resultDate = New Date(yearVal, monthVal, dayVal)
        Return True
    End Function

    Private Shared Sub EnsureDropdownOption(ByVal ddl As DropDownList, ByVal text As String, ByVal val As String)
        If ddl.Items.FindByValue(val) Is Nothing Then
            ddl.Items.Add(New ListItem(text, val))
        End If
    End Sub

    Private Sub ShowWarning(ByVal message As String)
        Dim safeMsg As String = HttpUtility.JavaScriptStringEncode(message)
        ScriptManager.RegisterStartupScript(
            Me.Page,
            Me.GetType(),
            "WarnAlert",
            "openModalWarningAlert(this,'ผลการตรวจสอบ','" & safeMsg & "');",
            True)
    End Sub

    Private Function BuildListUrl(ByVal includeSavedFlag As Boolean) As String
        Dim actionType As String = If(Request.QueryString("ActionType") <> "", Request.QueryString("ActionType"), "port")
        Dim url As String = "StudentAchievement.aspx?ActionType=" & HttpUtility.UrlEncode(actionType)
        If Not IsRole("ROLE_STD") AndAlso Request.QueryString("stdcode") <> "" Then
            url &= "&stdcode=" & HttpUtility.UrlEncode(Request.QueryString("stdcode").Trim())
        End If
        If includeSavedFlag Then
            url &= "&saved=1"
        End If
        Return url
    End Function

    Private Function ResolveStudentOwnCode() As String
        Dim profileId As String = GetCookieValue("ProfileID")
        If profileId <> "" Then Return profileId
        Return GetCookieValue("Username")
    End Function

    Private Function CanCurrentUserModify() As Boolean
        Return IsRole("ROLE_STD") OrElse IsRole("ROLE_ADM") OrElse IsRole("ROLE_SPA")
    End Function

    Private Function GetCurrentAcademicYearBe() As Integer
        Dim eduYear As Integer = StrNull2Zero(GetCookieValue("EDUYEAR"))
        If eduYear >= 2500 Then Return eduYear

        Dim nowYear As Integer = DateTime.Now.Year
        If nowYear < 2400 Then nowYear += 543
        Return nowYear
    End Function

    Private Function GetCookieValue(ByVal cookieName As String) As String
        Dim ck As HttpCookie = Request.Cookies(cookieName)
        If ck Is Nothing OrElse ck.Value Is Nothing Then
            Return ""
        End If
        Return ck.Value.Trim()
    End Function

    Private Function IsRole(ByVal roleCookieName As String) As Boolean
        Dim val As String = GetCookieValue(roleCookieName)
        Return String.Equals(val, "True", StringComparison.OrdinalIgnoreCase) OrElse val = "1"
    End Function
End Class
