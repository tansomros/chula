
Public Class Roles
    Inherits System.Web.UI.Page

    Dim ctlRole As New RoleController
    Dim dt As New DataTable
    Dim ds As New DataSet

    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblID.Text = ""
            LoadRolesToGrid()
        End If

        txtCode.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub
    Private Sub LoadRolesToGrid()

        dt = ctlRole.GetRoles

        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()

            For i = 0 To .Rows.Count - 1
                .Rows(i).Cells(0).Text = i + 1
            Next

        End With

    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If ctlRole.Roles_Delete(e.CommandArgument) Then

                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "Roles", "Delete role:" & txtCode.Text & ">>" & txtName.Text, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadRolesToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub


    Private Sub EditData(ByVal pID As String)
        ds = ctlRole.GetRoles_ByID(pID)
        dt = ds.Tables(0)
        Dim objList As New RoleInfo
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.lblID.Text = DBNull2Str(dt.Rows(0)(objList.tblField(objList.fldPos.f00_RoleID).fldName))
                Me.txtCode.Text = DBNull2Str(dt.Rows(0)(objList.tblField(objList.fldPos.f00_RoleID).fldName))
                txtName.Text = DBNull2Str(dt.Rows(0)(objList.tblField(objList.fldPos.f01_RoleName).fldName))
                txtDesc.Text = DBNull2Str(dt.Rows(0)(objList.tblField(objList.fldPos.f02_Descriptions).fldName))
                chkStatus.Checked = CBool(dt.Rows(0)(objList.tblField(objList.fldPos.f03_IsPublic).fldName))
            End With
        End If
        dt = Nothing
        ds = Nothing
        objList = Nothing
    End Sub
    Private Sub ClearData()
        Me.lblID.Text = ""
        Me.txtCode.Text = ""
        txtName.Text = ""
        txtDesc.Text = ""
        chkStatus.Checked = True

    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(6).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If


    End Sub



    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtName.Text = "" Or txtCode.Text = "" Then

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If
        Dim item As Integer

        If lblID.Text = "" Then

            item = ctlRole.Roles_Add(txtCode.Text, txtName.Text, txtDesc.Text, Boolean2Decimal(chkStatus.Checked))

            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Roles", "Add new role:" & txtCode.Text, "Result:" & item)

        Else
            item = ctlRole.Roles_Update(lblID.Text, txtCode.Text, txtName.Text, txtDesc.Text, Boolean2Decimal(chkStatus.Checked))

            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Roles", "Update role:" & txtCode.Text, "Result:" & item)

        End If


        LoadRolesToGrid()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)


    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub
End Class

