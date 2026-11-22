
Public Class AssignRandom
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlCs As New Coursecontroller
    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblResult.Visible = False
            LoadYearToDDL()
            LoadCourseToDDL()

        End If
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
    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()
        dt = ctlCs.Courses_GetByYear(ddlYear.SelectedValue)
        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With ddlCourse
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("NameTH"))
                    .Items(i).Value = dt.Rows(i)("CourseID")
                End With
            Next

        End If

    End Sub

    Dim ctlReg As New RegisterController
    Dim ctlReQ As New REQcontroller
    Dim ctlAss As New AssessmentController

    Private Sub Randomization(DegreeNo As Integer)
        Dim dtL As New DataTable
        Dim dtReq As New DataTable

        Dim iQuota, RYear, LocatID, TimePhaseID, iQuota_Balance As Integer
        Dim sSex, sSubjCode, sStdCode, sIsSameGender As String
        Dim str() As String

        sStdCode = ""
        sIsSameGender = "N"

        str = Split(ddlCourse.SelectedItem.Text, " : ")
        RYear = StrNull2Zero(ddlYear.SelectedValue)
        sSubjCode = str(0)

        'Get Location in Register Table
        dtL = ctlReQ.Requirements_GetLocationByYearCourse(RYear, ddlCourse.SelectedValue)
        If dtL.Rows.Count > 0 Then
            For i = 0 To dtL.Rows.Count - 1
                'Get REQ Account of Location i
                LocatID = dtL.Rows(i)("LocationID")
                sIsSameGender = dtL.Rows(i)("IsSameGender")
                dtReq = ctlReQ.Requirements_GetByCourse(RYear, LocatID, ddlCourse.SelectedValue)
                For n = 0 To dtReq.Rows.Count - 1 ' Loop Step by PhaseTime
                    'Process for Man
                    TimePhaseID = dtReq.Rows(n)("TimePhaseID")

                    iQuota = dtReq.Rows(n)("Man")
                    sSex = "M"
                    If iQuota > 0 Then
                        iQuota_Balance = iQuota - ctlAss.Assessment_GetStudentCountBySex(RYear, LocatID, TimePhaseID, sSex, sSubjCode)
                        If iQuota_Balance > 0 Then
                            Randomizing(iQuota_Balance, RYear, LocatID, TimePhaseID, sSex, sSubjCode, DegreeNo)
                        End If
                    End If

                    iQuota = dtReq.Rows(n)("Women")
                    sSex = "F"
                    If iQuota > 0 Then
                        iQuota_Balance = iQuota - ctlAss.Assessment_GetStudentCountBySex(RYear, LocatID, TimePhaseID, sSex, sSubjCode)
                        If iQuota_Balance > 0 Then
                            Randomizing(iQuota_Balance, RYear, LocatID, TimePhaseID, sSex, sSubjCode, DegreeNo)
                        End If
                    End If

                    iQuota = dtReq.Rows(n)("NoSpec")
                    If iQuota > 0 Then

                        If sIsSameGender = "Y" Then
                            sSex = ctlAss.Assessment_GetStudentSex(RYear, LocatID, TimePhaseID, sSubjCode)
                        Else
                            sSex = ""
                        End If

                        iQuota_Balance = iQuota - ctlAss.Assessment_GetStudentCountBySex(RYear, LocatID, TimePhaseID, "", sSubjCode)

                        If iQuota_Balance > 0 Then
                            Randomizing(iQuota_Balance, RYear, LocatID, TimePhaseID, sSex, sSubjCode, DegreeNo)
                        End If
                    End If
                Next
            Next
        End If
    End Sub
    Private Sub Randomizing(iQuota As Integer, RYear As Integer, LocatID As Integer, TimePhaseID As Integer, sSex As String, sSubjCode As String, DegreeNO As Integer)

        Dim StdNum As Integer
        Dim sStdCode As String
        Dim dtRan As New DataTable

        dt = ctlReg.StudentRegister_GetStudentBySex(RYear, LocatID, TimePhaseID, DegreeNO, sSex, sSubjCode)
        dtRan = dt
        StdNum = dt.Rows.Count

        If StdNum > 0 Then
            If StdNum <= iQuota Then ' ถ้าจำนวนนิสิตเลือกลงทะเบียน < จำนวนที่แหล่งฝึกรับ 
                ' คัดเลือกให้นิสิตทุกคน ไม่ต้องสุ่ม โดยจะให้สิทธิคนที่เลือกในอันดับ 1 ก่อน
                For k = 0 To StdNum - 1
                    sStdCode = dt.Rows(k)("Student_Code")
                    If ctlAss.Assessment_GetCheckDup(RYear, TimePhaseID, sStdCode) <= 0 Then
                        ctlAss.Assessment_Add(RYear, sSubjCode, sStdCode, LocatID, TimePhaseID, ddlCourse.SelectedValue, Request.Cookies("Username").Value)
                    End If

                Next
            Else ' ถ้าจำนวนนิสิตเลือกลงทะเบียน > จำนวนที่แหล่งฝึกรับ 
                'ทำการ Random นิสิต
                Dim CNT, J As Integer
                Dim RS As New Random

                For t = 0 To iQuota - 1
                    CNT = StdNum - 1
                    Try
                        J = RS.Next(CNT)
                        sStdCode = dt.Rows(J)("Student_Code")

                        If ctlAss.Assessment_GetCheckDup(RYear, TimePhaseID, sStdCode) <= 0 Then
                            ctlAss.Assessment_Add(RYear, sSubjCode, sStdCode, LocatID, TimePhaseID, ddlCourse.SelectedValue, Request.Cookies("Username").Value)
                        End If
                        dt.Rows.RemoveAt(J)
                    Catch ex As Exception

                    End Try

                Next
            End If
        End If
    End Sub

    Protected Sub cmdRandom_Click(sender As Object, e As EventArgs) Handles cmdRandom.Click

        System.Threading.Thread.Sleep(1000)
        UpdateProgress1.Visible = True

        Dim ctlCfg As New SystemConfigController
        Dim MaxQ As New Integer

        MaxQ = ctlCfg.SystemConfig_GetByCode(CFG_MAXLOCATION)

        For m = 1 To MaxQ
            Randomization(m)
        Next

        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Assessment", "ทำการสุ่มคัดเลือกนิสิต", "")

        UpdateProgress1.Visible = False
        lblResult.Visible = True
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadCourseToDDL()
    End Sub
End Class

