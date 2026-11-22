Public Class AssesseeComment
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    'Dim ctlCs As New CourseController
    Dim ctlT As New TimePhaseController
    Dim ctlP As New PreceptorController
    Dim dt As New DataTable
    Public dtSwot As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        'เปิดให้ประเมินจุดเด่น จุดด้อย ได้ตลอดเวลา
        'If Not SystemOnlineTime() Then
        '    Response.Redirect("ResultPage.aspx?t=closed&p=assm")
        'End If1    

        If Not IsPostBack Then
            lblYear.Text = Request.Cookies("ASSMYEAR").Value
            lblYearEn.Text = StrNull2Zero(Request.Cookies("ASSMYEAR").Value) - 543

            If Request.Cookies("LANG").Value.ToLower() = "en" Then
                lblYearEn.Visible = True
                lblYear.Visible = False
                'lblNote.Text = "Note : The scores shown are the scores from the second evaluation only."
                cmdAll.Text = "All"
                cmdNoScore.Text = "Pending Only"
                cmdFind.Text = "Search"
            Else
                lblYearEn.Visible = False
                lblYear.Visible = True
            End If

            LoadLocationToDDL()
            LoadPhaseToDDL()

            If Not Request("ph") Is Nothing Then
                ddlPhase.SelectedValue = Request("ph")
            End If

            LoadAssesseeListToGrid()
        End If

    End Sub
    Private Function SystemOnlineTime() As Boolean
        Dim ctlCfg As New SystemConfigController

        Dim Bdate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_STARTASSM_LOC)))
        Dim Edate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_ENDASSM_LOC)))
        Dim sToday As Integer = StrNull2Zero(ConvertDate2DBString(ctlCfg.GET_DATE_SERVER))
        Dim bAvailable As Boolean
        If sToday < Bdate Then
            bAvailable = False
        ElseIf sToday > Edate Then
            bAvailable = False
        Else
            bAvailable = True
        End If
        Return bAvailable
    End Function

    Private Sub LoadLocationToDDL()
        ddlLocation.Items.Clear()
        dt = ctlP.PreceptorLocation_GetByUser(StrNull2Zero(lblYear.Text), StrNull2Zero(Request.Cookies("UserID").Value))
        If dt.Rows.Count > 0 Then
            With ddlLocation
                .DataSource = dt
                .DataTextField = "LocationName"
                .DataValueField = "LocationID"
                .DataBind()
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadPhaseToDDL()
        ddlPhase.Items.Clear()
        dt = ctlT.TimePhase_GetByLocation(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlLocation.SelectedValue))
        If dt.Rows.Count > 0 Then
            With ddlPhase
                .DataSource = dt
                .DataTextField = "Descriptions"
                .DataValueField = "PhaseID"
                .DataBind()
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadAssesseeListToGrid(Optional pStatus As String = "")
        dtSwot = ctlLG.Assessee_Get4Prominent(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlLocation.SelectedValue), StrNull2Zero(ddlPhase.SelectedValue), txtSearch.Text, pStatus)
        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With
        dt = Nothing
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    Response.Redirect("AssessmentStudentComment.aspx?std=" & e.CommandArgument() & "&ph=" & ddlPhase.SelectedValue)
            End Select
        End If
    End Sub
    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadAssesseeListToGrid()
    End Sub
    Protected Sub cmdAll_Click(sender As Object, e As EventArgs) Handles cmdAll.Click
        txtSearch.Text = ""
        LoadAssesseeListToGrid()
    End Sub

    Protected Sub cmdNoScore_Click(sender As Object, e As EventArgs) Handles cmdNoScore.Click
        LoadAssesseeListToGrid("N")
    End Sub

    Protected Sub ddlPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPhase.SelectedIndexChanged
        LoadAssesseeListToGrid()
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        LoadPhaseToDDL()
    End Sub
End Class

