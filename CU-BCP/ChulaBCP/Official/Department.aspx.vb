
Public Class Department
    Inherits System.Web.UI.Page

    Dim ctlLG As New MasterController
    Dim dt As New DataTable
    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            ClearData()
            lblID.Text = ""
            ' LoadMajorToDDL()
            LoadDepartmentToGrid()
        End If

    End Sub
    Private Sub LoadDepartmentToGrid()

        If Trim(txtSearch.Text) <> "" Then
            dt = ctlLG.Department_GetSearch(txtSearch.Text)
        Else
            dt = ctlLG.Department_GetAll()
        End If
        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With

    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If ctlLG.Department_Delete(e.CommandArgument) Then

                        acc.User_GenLogfile(Request.Cookies("UserLoginID").Value, ACTTYPE_DEL, "Department", "ลบชื่อภาควิชา :" & txtName.Text, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ลบข้อมูลเรียบร้อย');", True)
                        LoadDepartmentToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub
    Private Sub EditData(ByVal pID As String)
        dt = ctlLG.Department_GetByID(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.lblID.Text = DBNull2Str(dt.Rows(0)("DepartmentUID"))
                txtName.Text = DBNull2Str(dt.Rows(0)("DepartmentName"))
                chkStatus.Checked = ConvertStatusFlag2CHK(dt.Rows(0)("StatusFlag"))
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub ClearData()
        Me.lblID.Text = ""

        txtName.Text = ""
        chkStatus.Checked = True

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



    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If

        Dim item As Integer

        If lblID.Text = "" Then

            If ctlLG.Department_CheckDuplicate(txtName.Text) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ชื่อภาควิชานี้มีอยู่่ในระบบแล้ว');", True)
                Exit Sub
            End If
            item = ctlLG.Department_Add(txtName.Text, ConvertBoolean2StatusFlag(chkStatus.Checked))
        Else
            item = ctlLG.Department_Update(StrNull2Zero(lblID.Text), txtName.Text, ConvertBoolean2StatusFlag(chkStatus.Checked))
        End If


        LoadDepartmentToGrid()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)


    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadDepartmentToGrid()
    End Sub

    Protected Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadDepartmentToGrid()
    End Sub
End Class

