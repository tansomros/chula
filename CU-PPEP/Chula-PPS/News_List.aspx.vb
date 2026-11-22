
Public Class News_List
    Inherits System.Web.UI.Page

    Dim ctlNews As New NewsController
    Dim dt As New DataTable 

    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            LoadNewsToGrid()
        End If

    End Sub
    Private Sub LoadNewsToGrid()

        dt = ctlNews.GetNews

        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()

            For i = 0 To .Rows.Count - 1
                .Rows(i).Cells(0).Text = i + 1

                Dim hlnkN As HyperLink = .Rows(i).Cells(1).FindControl("hlnkNews")
                hlnkN.Text = dt.Rows(i)("Title")

                Select Case dt.Rows(i)("NewsType")
                    Case "URL"

                        hlnkN.NavigateUrl = String.Concat(dt.Rows(i)("LinkPath"))
                    Case "UPL"

                        hlnkN.NavigateUrl = tmpUpload & "/" & String.Concat(dt.Rows(i)("LinkPath"))
                    Case "CON"

                        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
                    Case Else

                        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
                End Select

                Dim chkS As CheckBox = .Rows(i).Cells(2).FindControl("chkSelect")
                chkS.Checked = Decimal2Boolean(dt.Rows(i)("FirstPage"))

            Next

        End With

    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    Response.Redirect("News_Manage.aspx?m=news&p=news-mng&id=" & grdData.DataKeys(e.CommandArgument).Value)
                Case "imgDel"
                    If ctlNews.News_Delete(grdData.DataKeys(e.CommandArgument).Value) Then

                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "News", "Delete News:" & grdData.Rows(e.CommandArgument).Cells(0).Text, grdData.Rows(e.CommandArgument).Cells(1).Text)
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadNewsToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If
            End Select
        End If
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(5).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()

            For i = 0 To .Rows.Count - 1
                Dim chkS As CheckBox = .Rows(i).Cells(2).FindControl("chkSelect")
                If chkS.Checked Then
                    ctlNews.News_SetFirstPage(StrNull2Zero(.DataKeys(i).Value))
                End If
            Next
        End With
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub
End Class

