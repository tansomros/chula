
Public Class LocationZone
    Inherits System.Web.UI.Page

    Dim dt As New DataTable

    Dim ctlZ As New LocationZoneController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            grdData.PageIndex = 0
            LoadZone()
        End If

    End Sub


    Private Sub LoadZone()
        dt = ctlZ.LocationZone_Get
        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadZone()
    End Sub
    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())

                    'lblCode.Text = grdData.Rows(
                Case "imgDel"
                    ctlZ.locationZone_Delete(e.CommandArgument)
                    DisplayMessage(Me, "ลบข้อมูลเรียบร้อย")
                    LoadZone()
            End Select


        End If
    End Sub


    Private Sub EditData(ByVal pID As String)

        dt = ctlZ.LocationZone_GetByID(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False

                Me.txtCode.Text = DBNull2Str(dt.Rows(0)("ZoneID"))
                txtName.Text = DBNull2Str(dt.Rows(0)("ZoneName"))

            End With
        End If
        dt = Nothing
    End Sub
    Private Sub ClearData()
        Me.txtCode.Text = ""
        txtName.Text = ""
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(4).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub


    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtName.Text = "" Or txtCode.Text = "" Then
            DisplayMessage(Me, "กรุณากรอกข้อมูลให้ครบถ้วน")
            Exit Sub
        End If
        ctlZ.LocationZone_Save(txtCode.Text, txtName.Text)
        LoadZone()
        ClearData()
        DisplayMessage(Me, "บันทึกข้อมูลเรียบร้อย")

    End Sub
End Class

