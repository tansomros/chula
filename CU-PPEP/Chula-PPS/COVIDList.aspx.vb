
Public Class COVIDList
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlStd As New StudentController
    Dim objUser As New UserController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            grdData.PageIndex = 0
            LoadStudent()
        End If

    End Sub
    Dim objPsn As New PersonInfo

    Private Sub LoadStudent()

        If Request.Cookies("ROLE_STD").Value = True Then
            dt = ctlStd.COVIDScreening_GetByStudent(Request.Cookies("ProfileID").Value)
            cmdNew.Visible = True
        ElseIf Request.Cookies("ROLE_PCT").Value = True Then
            dt = ctlStd.COVIDScreening_GetByLocation(Request.Cookies("LocationID").Value)
            cmdNew.Visible = False
        Else
            dt = ctlStd.COVIDScreening_Get()
            cmdNew.Visible = False
        End If

        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()

            End With

        End If
    End Sub
    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadStudent()
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgView"
                    'Response.Redirect("COVIDScreeningBio.aspx?ActionType=std&std=" & e.CommandArgument())
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "view", "window.location='COVIDScreening.aspx?ActionType=covid&t=view&uid=" & e.CommandArgument() & "';", True)

                Case "imgEdit"
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "view", "window.location='COVIDScreening.aspx?ActionType=covid&t=mod&uid=" & e.CommandArgument() & "';", True)

                Case "imgDel"
                    If ctlStd.COVIDScreening_Delete(e.CommandArgument) Then

                        objUser.User_GenLogfile(Request.Cookies("Username").Value, "DEL", "COVID", "ลบ COVID-19 Screeining :" & e.CommandArgument, "")

                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If

                    LoadStudent()

            End Select

        End If
    End Sub
    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(6).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadStudent()
    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Response.Redirect("COVIDScreening.aspx?ActionType=covid")
    End Sub
End Class