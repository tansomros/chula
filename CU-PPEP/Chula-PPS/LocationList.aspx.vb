
Public Class LocationList
    Inherits System.Web.UI.Page

    Dim ctlLG As New LocationController
    Dim dt As New DataTable
    Dim ds As New DataSet
    Dim ctlPsn As New PersonController
    Dim acc As New UserController
    Public dtLoc As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            cmdNew.Visible = False
            LoadLocationGroupToDDL()
            If Request.Cookies("ROLE_ADM").Value = True Then
                cmdNew.Visible = True
            End If
            LoadLocationToGrid()
        End If

    End Sub
    Private Sub LoadLocationToGrid()
        dtLoc = ctlLG.Location_GetBySearch(txtSearch.Text, StrNull2Zero(ddlType.SelectedValue))
        'With grdData
        '    .Visible = True
        '    .DataSource = dt
        '    .DataBind()
        'End With

    End Sub

    Private Sub LoadLocationGroupToDDL()

        Dim ctlLG As New LocationGroupController
        dt = ctlLG.LocationGroup_Get
        If dt.Rows.Count > 0 Then
            With ddlType
                .Items.Add("---ทั้งหมด---")
                .Items(0).Value = 0
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add(dt.Rows(i)("Name"))
                    .Items(i + 1).Value = dt.Rows(i)("Code")
                Next
                .SelectedIndex = 0
                .Visible = True
            End With

        End If
        dt = Nothing
    End Sub

    'Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
    '    If TypeOf e.CommandSource Is WebControls.ImageButton Then
    '        Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
    '        Select Case ButtonPressed.ID
    '            Case "imgEdit"
    '                Response.Redirect("Location.aspx?ActionType=loc&ItemType=l&lid=" & e.CommandArgument())
    '            Case "imgView"
    '                Response.Redirect("LocationDetail.aspx?ActionType=loc&ItemType=l&lid=" & e.CommandArgument())
    '            Case "imgDel"
    '                If ctlLG.Location_Delete(e.CommandArgument) Then

    '                    acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "Location", "ลบแหล่งฝึก:" & e.CommandArgument, "")
    '                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
    '                    LoadLocationToGrid()
    '                Else
    '                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
    '                End If

    '        End Select


    '    End If
    'End Sub
    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadLocationToGrid()
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        LoadLocationToGrid()
    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Response.Redirect("Location.aspx?ActionType=loc&ItemType=l")
    End Sub
End Class

