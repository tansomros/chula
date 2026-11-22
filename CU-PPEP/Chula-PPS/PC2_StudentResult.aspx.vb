
Public Class PC2_StudentResult
    Inherits System.Web.UI.Page

    Dim ctlA As New PC2Controller

    Dim acc As New UserController
    Dim ctlCfg As New SystemConfigController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            lblNo.Visible = False
            If Request.Cookies("ROLE_STD").Value = True Then
                If Not SystemOnlineTime() Then
                    Response.Redirect("ResultPage.aspx?t=closed&p=pc2result")
                End If
            End If
            LoadYearToDDL()
            LoadStudentResultToGrid()
        End If
    End Sub
    Private Function SystemOnlineTime() As Boolean
        Dim Bdate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_PC2_STARTDATE)))
        Dim Edate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_PC2_ENDDATE)))
        Dim sToday As Integer = StrNull2Zero(ConvertDate2DBString(ctlCfg.GET_DATE_SERVER))
        Dim bAvailable As Boolean
        bAvailable = False

        If sToday > Edate Then
            bAvailable = True
        End If
        Return bAvailable
    End Function

    Private Sub LoadYearToDDL()
        Dim dtY As New DataTable
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlA.GET_DATE_SERVER))
        Dim LastRow As Integer
        dtY = ctlA.PC2_GetYear
        LastRow = dtY.Rows.Count - 1

        If dtY.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dtY
                .DataTextField = "EduYear"
                .DataValueField = "EduYear"
                .DataBind()

                If dtY.Rows(LastRow)(0) = y Then
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 1).Value = y + 1
                ElseIf dtY.Rows(LastRow)(0) > y Then
                    'ddlYear.Items.Add(y + 2)
                    'ddlYear.Items(LastRow + 1).Value = y + 2
                ElseIf dtY.Rows(LastRow)(0) < y Then
                    ddlYear.Items.Add(y)
                    ddlYear.Items(LastRow + 1).Value = y
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 2).Value = y + 1
                End If
            End With
            ddlYear.SelectedValue = ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR)
        Else
            ddlYear.Items.Add(y)
            ddlYear.Items(0).Value = y
            ddlYear.Items.Add(y + 1)
            ddlYear.Items(1).Value = y + 1
            ddlYear.SelectedIndex = 0
        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        dtY = Nothing
    End Sub

    Private Sub LoadStudentResultToGrid()
        Dim dtSL As New DataTable
        dtSL = ctlA.PC2_Assessment_GetByStudent(ddlYear.SelectedValue, Request.Cookies("ProfileID").Value)
        If dtSL.Rows.Count > 0 Then
            lblNo.Visible = False
            With grdSubject
                .Visible = True
                .DataSource = dtSL
                .DataBind()
            End With
        Else
            lblNo.Visible = True
            grdSubject.Visible = False
            grdSubject.DataSource = Nothing
        End If

    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadStudentResultToGrid()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadStudentResultToGrid()
    End Sub
End Class

