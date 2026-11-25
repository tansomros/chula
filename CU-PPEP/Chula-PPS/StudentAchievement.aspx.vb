Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Web

Public Class StudentAchievement
    Inherits System.Web.UI.Page

    Private ReadOnly ctlAch As New StudentAchievementController()
    Private ReadOnly ctlStd As New StudentController()
    Private ReadOnly ctlUser As New UserController()

    Public Property ShowStudentColumn As Boolean = False
    Public Property CanModifyItems As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If GetCookieValue("Username") = "" Then
            Response.Redirect("Default.aspx?logout=YES")
            Exit Sub
        End If

        CanModifyItems = CanCurrentUserModify()
        ShowStudentColumn = (ResolveTargetStudentCode() = "")

        If Not IsPostBack Then
            BindYearDropdown()
            LoadStudentHeader()
            LoadAchievements()

            If Request.QueryString("saved") = "1" Then
                ScriptManager.RegisterStartupScript(
                    Me.Page,
                    Me.GetType(),
                    "SavedAlert",
                    "openModalSuccess(this,'Success','บันทึกข้อมูลผลงานนิสิตเรียบร้อยแล้ว');",
                    True)
            End If
        End If
    End Sub

    Private Sub BindYearDropdown()
        ddlYear.Items.Clear()
        ddlYear.Items.Add(New ListItem("-- ทั้งหมด --", "0"))

        Dim currentYearBe As Integer = GetCurrentAcademicYearBe()
        For y As Integer = currentYearBe + 1 To currentYearBe - 10 Step -1
            ddlYear.Items.Add(New ListItem(y.ToString(), y.ToString()))
        Next
    End Sub

    Private Sub LoadStudentHeader()
        Dim targetStdCode As String = ResolveTargetStudentCode()
        CanModifyItems = CanCurrentUserModify()
        ShowStudentColumn = (targetStdCode = "")

        cmdAdd.Visible = CanModifyItems
        cmdAddEmpty.Visible = CanModifyItems
        phStudentHead.Visible = ShowStudentColumn

        If targetStdCode <> "" Then
            pnStudentInfo.Visible = True

            Dim dtStd As DataTable = ctlStd.Student_GetByCode(targetStdCode)
            If dtStd.Rows.Count > 0 Then
                Dim row As DataRow = dtStd.Rows(0)
                lblStudentCode.Text = String.Concat(row("Student_Code")).Trim()
                lblStudentName.Text = (String.Concat(row("Prefix")) & String.Concat(row("FirstName")) & " " & String.Concat(row("LastName"))).Trim()
                lblMajorName.Text = If(String.Concat(row("MajorName")).Trim() <> "", String.Concat(row("MajorName")).Trim(), "-")
                lblStudyYear.Text = If(DBNull2Zero(row("LevelClass")) > 0, "ชั้นปีที่ " & DBNull2Zero(row("LevelClass")).ToString(), "-")
            Else
                lblStudentCode.Text = targetStdCode
                lblStudentName.Text = GetCookieValue("NameOfUser")
                lblMajorName.Text = "-"
                lblStudyYear.Text = "-"
            End If
        Else
            pnStudentInfo.Visible = False
        End If
    End Sub

    Private Sub LoadAchievements()
        Dim targetStdCode As String = ResolveTargetStudentCode()
        ShowStudentColumn = (targetStdCode = "")
        CanModifyItems = CanCurrentUserModify()
        phStudentHead.Visible = ShowStudentColumn

        Dim selectedYear As Integer = StrNull2Zero(ddlYear.SelectedValue)
        Dim selectedType As String = ddlActivityType.SelectedValue

        Dim dt As DataTable = ctlAch.Achievement_GetSearch(targetStdCode, selectedYear, selectedType, "")

        lblCount.Text = dt.Rows.Count.ToString("N0")
        UpdateSummaryStats(dt)

        If dt.Rows.Count > 0 Then
            pnTableWrap.Visible = True
            rptData.DataSource = dt
            rptData.DataBind()
            pnEmpty.Visible = False
        Else
            rptData.DataSource = Nothing
            rptData.DataBind()
            pnTableWrap.Visible = False
            pnEmpty.Visible = True
        End If
    End Sub

    Private Sub UpdateSummaryStats(ByVal dt As DataTable)
        lblStatTotal.Text = dt.Rows.Count.ToString("N0")

        Dim years As New HashSet(Of Integer)()
        Dim totalGrant As Decimal = 0D

        For Each row As DataRow In dt.Rows
            If Not IsDBNull(row("AcademicYearBe")) Then
                years.Add(Convert.ToInt32(row("AcademicYearBe")))
            End If
            If Not IsDBNull(row("GrantAmount")) Then
                totalGrant += Convert.ToDecimal(row("GrantAmount"))
            End If
        Next

        lblStatYears.Text = years.Count.ToString("N0")
        lblStatGrant.Text = totalGrant.ToString("#,##0.00")
    End Sub

    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdAdd.Click
        Response.Redirect(BuildModifyUrl("new", ""))
    End Sub

    Protected Sub cmdAddEmpty_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdAddEmpty.Click
        Response.Redirect(BuildModifyUrl("new", ""))
    End Sub

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdClear.Click
        ddlYear.SelectedIndex = 0
        ddlActivityType.SelectedIndex = 0
        LoadAchievements()
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadAchievements()
    End Sub

    Protected Sub ddlActivityType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlActivityType.SelectedIndexChanged
        LoadAchievements()
    End Sub

    Protected Sub rptData_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptData.ItemCommand
        If e.CommandName <> "DeleteItem" Then Exit Sub
        If Not CanCurrentUserModify() Then Exit Sub

        Dim itemId As Integer = StrNull2Zero(Convert.ToString(e.CommandArgument))
        If itemId <= 0 Then Exit Sub

        Dim ownerGuard As String = If(IsRole("ROLE_STD"), ResolveStudentOwnCode(), "")
        If ctlAch.Achievement_Delete(itemId, GetCookieValue("Username"), ownerGuard) Then
            ctlUser.User_GenLogfile(GetCookieValue("Username"), ACTTYPE_DEL, "StudentAchievements", "ลบผลงานนิสิต ID=" & itemId.ToString(), "")
            ScriptManager.RegisterStartupScript(
                Me.Page,
                Me.GetType(),
                "DeleteSuccess",
                "openModalSuccess(this,'Success','ลบข้อมูลผลงานนิสิตเรียบร้อยแล้ว');",
                True)
        Else
            ScriptManager.RegisterStartupScript(
                Me.Page,
                Me.GetType(),
                "DeleteWarning",
                "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบสิทธิ์และลองใหม่อีกครั้ง');",
                True)
        End If
        LoadAchievements()
    End Sub

    Protected Function BuildItemUrl(ByVal mode As String, ByVal idObj As Object) As String
        Return BuildModifyUrl(mode, String.Concat(idObj))
    End Function

    Protected Function FormatDateThai(ByVal value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then Return "-"
        Dim dtVal As Date = Convert.ToDateTime(value)
        Return DisplayShortDateTH(dtVal)
    End Function

    Protected Function FormatDateSortKey(ByVal value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then Return ""
        Dim dtVal As Date = Convert.ToDateTime(value)
        Return dtVal.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
    End Function

    Protected Function FormatGrantSortKey(ByVal value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then Return "-1"
        Dim amount As Decimal = Convert.ToDecimal(value)
        Return amount.ToString("0.00", CultureInfo.InvariantCulture)
    End Function

    Protected Function FormatGrantAmountHtml(ByVal value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then
            Return "<span class=""ach-grant-empty"">-</span>"
        End If
        Dim amount As Decimal = Convert.ToDecimal(value)
        Return "<span class=""ach-grant-val"">" & amount.ToString("#,##0.00") & "</span>"
    End Function

    Protected Function FormatOptionalSubtext(ByVal value As Object) As String
        Dim text As String = String.Concat(value).Trim()
        If text = "" Then Return ""
        Return "<span class=""ach-subtext"">" & HttpUtility.HtmlEncode(text) & "</span>"
    End Function

    Protected Function FormatOrganizerSubtext(ByVal value As Object) As String
        Dim text As String = String.Concat(value).Trim()
        If text = "" Then Return ""
        Dim organizerPrefix As String = ChrW(&HE1C) & ChrW(&HE39) & ChrW(&HE49) & ChrW(&HE08) & ChrW(&HE31) & ChrW(&HE14) & ": "
        Return "<span class=""ach-subtext""><i class=""fa fa-building-o""></i> " & HttpUtility.HtmlEncode(organizerPrefix & text) & "</span>"
    End Function

    Private Function BuildModifyUrl(ByVal mode As String, ByVal id As String) As String
        Dim actionType As String = If(Request.QueryString("ActionType") <> "", Request.QueryString("ActionType"), "port")
        Dim url As String = "StudentAchievementModify.aspx?ActionType=" & HttpUtility.UrlEncode(actionType) & "&t=" & HttpUtility.UrlEncode(mode)
        If id <> "" Then
            url &= "&id=" & HttpUtility.UrlEncode(id)
        End If
        If Not IsRole("ROLE_STD") AndAlso Request.QueryString("stdcode") <> "" Then
            url &= "&stdcode=" & HttpUtility.UrlEncode(Request.QueryString("stdcode").Trim())
        End If
        Return url
    End Function

    Private Function ResolveTargetStudentCode() As String
        If IsRole("ROLE_STD") Then
            Return ResolveStudentOwnCode()
        End If
        If Request.QueryString("stdcode") IsNot Nothing AndAlso Request.QueryString("stdcode").Trim() <> "" Then
            Return Request.QueryString("stdcode").Trim()
        End If
        Return ""
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
