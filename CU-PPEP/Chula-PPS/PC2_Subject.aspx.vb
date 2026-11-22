
Public Class PC2_Subject
    Inherits System.Web.UI.Page

    Dim ctlLG As New PC2Controller
    Dim dt As New DataTable
    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            ClearData()
            hdUID.Value = ""
            grdData.PageIndex = 0
            LoadSubjectToGrid()
        End If

        'txtCode.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        'txtLevelClass.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub
    Private Sub LoadSubjectToGrid()

        If txtSearch.Text = "" Then
            dt = ctlLG.PC2_Subject_Get
        Else
            dt = ctlLG.PC2_Subject_GetBySearch(txtSearch.Text)
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
                    If ctlLG.PC2_Subject_Delete(e.CommandArgument) Then
                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "PC2_Subject", "ลบรายวิชา:" & hdUID.Value & ">>" & txtNameTH.Text, "")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ลบข้อมูลเรียบร้อย');", True)
                        grdData.PageIndex = 0
                        LoadSubjectToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub
    Private Sub EditData(ByVal pID As Integer)
        dt = ctlLG.PC2_Subject_GetByUID(pID)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.hdUID.Value = DBNull2Str(dt.Rows(0)("UID"))
                txtNameTH.Text = DBNull2Str(dt.Rows(0)("NameTH"))
                txtNameEN.Text = DBNull2Str(dt.Rows(0)("NameEN"))
                chkStatus.Checked = ConvertStatusFlag2Boolean(dt.Rows(0)("StatusFlag"))
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub ClearData()
        Me.hdUID.Value = ""
        txtNameTH.Text = ""
        txtNameEN.Text = ""
        chkStatus.Checked = True
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
        If txtNameTH.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If

        ctlLG.PC2_Subject_Save(StrNull2Zero(hdUID.Value), txtNameTH.Text, txtNameEN.Text, ConvertBoolean2StatusFlag(chkStatus.Checked))

        grdData.PageIndex = 0
        LoadSubjectToGrid()
        ClearData()

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)


    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadSubjectToGrid()
    End Sub


    Protected Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadSubjectToGrid()
    End Sub
End Class

