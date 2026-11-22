Public Class EventList
    Inherits System.Web.UI.Page
    Dim ctlS As New EventController
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            LoadCategory()
            LoadData()
        End If
    End Sub

    Private Sub LoadCategory()
        dt = ctlS.EventCategory_GetAll()
        ddlCategory.DataSource = dt
        ddlCategory.DataValueField = "UID"
        ddlCategory.DataTextField = "NameTH"
        ddlCategory.DataBind()
        dt = Nothing
    End Sub

    Private Sub LoadData()
        dt = ctlS.Events_GetBySearch(StrNull2Zero(ddlCategory.SelectedValue), ConvertStrDate2InformDateString(txtStartDate.Text), ConvertStrDate2InformDateString(txtEndDate.Text), txtEventName.Text)

        grdData.DataSource = dt
        grdData.DataBind()


    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    Response.Redirect("Event_Manage.aspx?id=" & e.CommandArgument())
                Case "imgDel"
                    ctlS.Events_Delete(e.CommandArgument())
                    LoadData()
            End Select


        End If
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(2).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#e0f3ff';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        LoadData()
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Response.Redirect("Event_Manage.aspx")
    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Response.Redirect("Event_Manage.aspx")
    End Sub
End Class