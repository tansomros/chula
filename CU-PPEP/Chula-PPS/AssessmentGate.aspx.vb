Public Class AssessmentGate
    Inherits System.Web.UI.Page

    Dim ctlPc As New PreceptorController
    Dim ctlCs As New CourseController
    Dim ctlCfg As New SystemConfigController
    Dim dt As New DataTable

    'Dim ckAssessorUID As New HttpCookie("AssessorUID")
    'Dim ckLocationAssessment As New HttpCookie("LocationAssessment")
    'Dim ckAssessorRoleID As New HttpCookie("AssessorRoleID")
    'Dim ckCourseAssessment As New HttpCookie("CourseAssessment")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            lblYear.Text = Request.Cookies("ASSMYEAR").Value

            Session("AssessorUID") = Request.Cookies("ProfileID").Value

            If Request.Cookies("UserProfileID").Value = 3 Then
                'Request.Cookies("ROLE_PCT").Value = True
                'Request.Cookies("ROLE_ADV").Value = False

                Session("LocationAssessment") = Request.Cookies("LocationID").Value

                Session("AssessorRoleID") = "P"
                Response.Redirect("AssesseeList.aspx?ActionType=" & Request("ActionType"))
            End If


            Dim Bdate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_STARTASSM)))
            Dim Edate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_ENDASSM)))

            Dim sToday As Integer = StrNull2Zero(ConvertDate2DBString(ctlCfg.GET_DATE_SERVER))

            Dim bAvailableADV As Boolean
            Dim bAvailablePCT As Boolean

            If sToday < Bdate Then
                bAvailableADV = False
                pnAdv.Visible = False
            ElseIf sToday > Edate Then
                bAvailableADV = False
                pnAdv.Visible = False
            Else
                bAvailableADV = True
                pnAdv.Visible = True
                LoadSubjectList()
            End If

            If Request.Cookies("ROLE_PCT").Value = True Then

                Dim Bdate2 As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_STARTASSM_LOC)))
                Dim Edate2 As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_ENDASSM_LOC)))

                If sToday < Bdate2 Then
                    bAvailablePCT = False
                    pnPct.Visible = False
                ElseIf sToday > Edate2 Then
                    bAvailablePCT = False
                    pnPct.Visible = False
                Else
                    bAvailablePCT = True
                    pnPct.Visible = True
                    LoadLocationtList()
                End If
            Else
                pnPct.Visible = False
            End If
            If bAvailableADV = False And bAvailablePCT = False Then
                Response.Redirect("ResultPage.aspx?t=closed&p=assm")
            End If
        End If

    End Sub



    Private Sub LoadSubjectList()
        dt = ctlCs.CourseCoordinator_Get4Assessment(StrNull2Zero(lblYear.Text), Request.Cookies("UserID").Value)

        dlAdvisor.DataSource = dt
        dlAdvisor.DataBind()

        If dt.Rows.Count > 0 Then
            pnAdv.Visible = True
            dlAdvisor.Visible = True
        Else
            pnAdv.Visible = False
            dlAdvisor.Visible = False
        End If

        dt = Nothing
    End Sub
    Private Sub LoadLocationtList()
        dt = ctlPc.PreceptorLocation_Get4Assessment(StrNull2Zero(lblYear.Text), Request.Cookies("UserID").Value)

        dlLocation.DataSource = dt
        dlLocation.DataBind()

        If dt.Rows.Count > 0 Then
            pnPct.Visible = True
            dlLocation.Visible = True
        Else
            pnPct.Visible = False
            dlLocation.Visible = False
        End If

    End Sub

    Protected Sub dlAdvisor_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles dlAdvisor.ItemCommand
        If TypeOf e.CommandSource Is WebControls.Button Then
            Dim ButtonPressed As WebControls.Button = e.CommandSource
            Select Case ButtonPressed.ID
                Case "cmdSubj"
                    'Request.Cookies("ROLE_ADV").Value = True
                    'Request.Cookies("ROLE_PCT").Value = False
                    Session("CourseAssessment") = e.CommandArgument()
                    Session("AssessorRoleID") = "A"
                    Response.Redirect("AssesseeList.aspx?ActionType=" & Request("ActionType"))
            End Select

        End If
    End Sub

    Private Sub dlLocation_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles dlLocation.ItemCommand
        If TypeOf e.CommandSource Is WebControls.Button Then
            Dim ButtonPressed As WebControls.Button = e.CommandSource
            Select Case ButtonPressed.ID
                Case "cmdLoc"
                    'Request.Cookies("ROLE_PCT.Value = True
                    'Request.Cookies("ROLE_ADV.Value = False
                    Session("LocationAssessment") = e.CommandArgument()
                    Session("AssessorRoleID") = "P"
                    Response.Redirect("AssesseeList.aspx?ActionType=" & Request("ActionType"))
            End Select

        End If
    End Sub
End Class

