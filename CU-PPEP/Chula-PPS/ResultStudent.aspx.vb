
Public Class ResultStudent
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlAss As New AssessmentController
    Dim ctlCs As New Coursecontroller

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            'If Request.Cookies("ROLE_STD").Value = True Then
            '    If Not SystemOnlineTime() Then
            '        Response.Redirect("ResultPage.aspx?t=closed&p=result")
            '    End If
            'End If
            lblMsg.Visible = False
            lblResult.Visible = False
            LoadYearToDDL()
            LoadResultProcess()
        End If
    End Sub
    Private Function SystemOnlineTime() As Boolean
        Dim ctlCfg As New SystemConfigController

        Dim Bdate As Integer = StrNull2Zero(ConvertStrDate2DBString(ctlCfg.SystemConfig_GetByCode(CFG_RESULTDATE)))
        'Dim Edate As Long = DBNull2Lng(ctlCfg.SystemConfig_GetByCode(CFG_ENDDATE))
        Dim sToday As Integer = StrNull2Zero(ConvertDate2DBString(ctlCfg.GET_DATE_SERVER))

        Dim bAvailable As Boolean
        If sToday < Bdate Then
            bAvailable = False
        Else
            bAvailable = True
        End If
        Return bAvailable
    End Function

    Private Sub LoadYearToDDL()

        dt = ctlCs.Year_GetByStudent(Request.Cookies("ProfileID").Value)
        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "CYear"
                .DataValueField = "CYear"
                .DataBind()
            End With
            ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value

        Else

        End If

        dt = Nothing
    End Sub
    Private Sub LoadResultProcess()


        If ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value Then
            If Request.Cookies("ROLE_STD").Value = True Then
                If Not SystemOnlineTime() Then
                    lblMsg.Visible = True
                    grdData.Visible = False
                    Exit Sub
                Else
                    lblMsg.Visible = False
                End If
            End If
        End If

        dt = ctlAss.Assessment_GetResultByStudent(ddlYear.SelectedValue, Request.Cookies("ProfileID").Value)
        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()

                For i = 0 To dt.Rows.Count - 1
                    .Rows(i).Cells(0).Text = i + 1
                    Dim lblTH As Label = .Rows(i).Cells(2).FindControl("lblTH")
                    Dim lblEN As Label = .Rows(i).Cells(2).FindControl("lblEN")

                    lblTH.Text = dt.Rows(i)("NameTH")

                Next
            End With
            lblResult.Visible = False
            lblMsg.Visible = False
        Else
            grdData.Visible = False
            lblResult.Visible = True
        End If
        dt = Nothing
    End Sub
    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound

        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            Dim sSubjCode As String
            sSubjCode = e.Row.Cells(1).Text
            Dim lnkV As HyperLink = e.Row.Cells(7).FindControl("hlnkView")
            lnkV.NavigateUrl = "LocationDetail.aspx?ActionType=loc&ItemType=l&y=" & ddlYear.SelectedValue & "&lid=" & grdData.DataKeys(e.Row.RowIndex).Value & "&subid=" & sSubjCode & "&r=yes"

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadResultProcess()
    End Sub
End Class

