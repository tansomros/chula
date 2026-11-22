
Public Class PC2_AssignRandom
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlA As New PC2Controller
    Dim acc As New UserController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            pnSuccess.Visible = False
            LoadYearToDDL()
            LoadSubjectToDDL()
        End If
    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlA.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlA.PC2_GetYear
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "EduYear"
                .DataValueField = "EduYear"
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
    Private Sub LoadSubjectToDDL()
        ddlSubject.Items.Clear()
        dt = ctlA.PC2_Subject_GetByYear(ddlYear.SelectedValue)
        If dt.Rows.Count > 0 Then
            ddlSubject.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With ddlSubject
                    .Items.Add("" & dt.Rows(i)("NameTH"))
                    .Items(i).Value = dt.Rows(i)("UID")
                End With
            Next

        End If

    End Sub


    Private Sub Randomization(DegreeNo As Integer)
        Dim dtL As New DataTable
        Dim dtReq As New DataTable

        Dim iQuota, RYear, iQuota_Balance As Integer
        Dim sStdCode As String
        Dim SubjUID As Integer

        sStdCode = ""

        SubjUID = ddlSubject.SelectedValue
        RYear = StrNull2Zero(ddlYear.SelectedValue)

        'Get Quota from table by subject
        iQuota = ctlA.PC2_Quota_GetBySubject(RYear, SubjUID)

        If iQuota > 0 Then
            iQuota_Balance = iQuota - ctlA.PC2_Assessment_GetStudentCount(RYear, SubjUID)
            If iQuota_Balance > 0 Then
                Randomizing(iQuota_Balance, RYear, SubjUID, DegreeNo)
            End If

        End If


    End Sub
    Private Sub Randomizing(iQuota As Integer, RYear As Integer, SubjectUID As Integer, DegreeNO As Integer)

        Dim StdNum As Integer
        Dim sStdCode As String
        Dim dtRan As New DataTable

        dt = ctlA.PC2_StudentRegister_GetBySubject(RYear, SubjectUID, DegreeNO)
        dtRan = dt
        StdNum = dt.Rows.Count

        If StdNum > 0 Then
            If StdNum <= iQuota Then ' ถ้าจำนวนนิสิตเลือกลงทะเบียน < จำนวนที่รับ 
                ' คัดเลือกให้นิสิตทุกคน ไม่ต้องสุ่ม โดยจะให้สิทธิคนที่เลือกในอันดับ 1 ก่อน
                For k = 0 To StdNum - 1
                    sStdCode = dt.Rows(k)("StudentCode")
                    If ctlA.PC2_Assessment_GetCheckDup(RYear, sStdCode) <= 0 Then
                        ctlA.PC2_Assessment_Add(RYear, SubjectUID, sStdCode, Request.Cookies("UserID").Value)
                    End If

                Next
            Else ' ถ้าจำนวนนิสิตเลือกลงทะเบียน > จำนวนที่รับ 
                'ทำการ Random นิสิต
                Dim CNT, J As Integer
                Dim RS As New Random

                For t = 0 To iQuota - 1
                    CNT = StdNum - 1
                    Try
                        J = RS.Next(CNT)
                        sStdCode = dt.Rows(J)("StudentCode")

                        If ctlA.PC2_Assessment_GetCheckDup(RYear, sStdCode) <= 0 Then
                            ctlA.PC2_Assessment_Add(RYear, SubjectUID, sStdCode, Request.Cookies("UserID").Value)
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

        MaxQ = ctlCfg.SystemConfig_GetByCode(CFG_PC2_MAXSUBJECT)

        For m = 1 To MaxQ
            Randomization(m)
        Next

        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "PC2_Assessment", "ทำการสุ่มคัดเลือกนิสิต สอบ PC2", "")

        UpdateProgress1.Visible = False
        pnSuccess.Visible = True
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadSubjectToDDL()
    End Sub
End Class

