
Public Class Major
    Inherits System.Web.UI.Page

    Dim ctlM As New MasterController
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
            LoadMajorToGrid()
        End If

    End Sub
    Private Sub LoadMajorToGrid()

        If Trim(txtSearch.Text) <> "" Then
            dt = ctlM.Major_GetSearch(txtSearch.Text)
        Else
            dt = ctlM.Major_GetAll()
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
                    If ctlM.Major_Delete(e.CommandArgument) Then

                        acc.User_GenLogfile(Request.Cookies("UserLoginID").Value, ACTTYPE_DEL, "Major", "ลบชื่อภาควิชา :" & txtNameTH.Text, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ลบข้อมูลเรียบร้อย');", True)
                        LoadMajorToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub
    Private Sub EditData(ByVal pID As String)
        dt = ctlM.Major_GetByID(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.lblID.Text = DBNull2Str(dt.Rows(0)("UID"))
                txtNameth.Text = DBNull2Str(dt.Rows(0)("NameTH"))
                txtNameen.Text = DBNull2Str(dt.Rows(0)("NameEN"))
                chkStatus.Checked = ConvertStatusFlag2CHK(dt.Rows(0)("StatusFlag"))
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub ClearData()
        Me.lblID.Text = ""
        txtNameEN.Text = ""
        txtNameTH.Text = ""
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

        If txtNameTH.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If

        Dim item As Integer

        If lblID.Text = "" Then

            If ctlM.Major_CheckDuplicate(txtNameTH.Text) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ชื่อภาควิชานี้มีอยู่่ในระบบแล้ว');", True)
                Exit Sub
            End If
            item = ctlM.Major_Add(txtNameTH.Text, txtNameEN.Text, ConvertBoolean2StatusFlag(chkStatus.Checked))
        Else
            item = ctlM.Major_Update(StrNull2Zero(lblID.Text), txtNameTH.Text, txtNameEN.Text, ConvertBoolean2StatusFlag(chkStatus.Checked))
        End If


        LoadMajorToGrid()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)


    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        LoadMajorToGrid()
    End Sub
End Class

