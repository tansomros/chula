
Public Class Provinces
    Inherits System.Web.UI.Page
     
    Dim dt As New DataTable

    Dim acc As New UserController
    Dim ctlbase As New MasterController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            grdData.PageIndex = 0
            lblCode.Text = ""
            LoadProvince()
        End If
         
    End Sub
    

    Private Sub LoadProvince()
        dt = ctlbase.LoadProvince
        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()

                For i = 0 To .Rows.Count - 1
                    .Rows(i).Cells(0).Text = i + 1
                Next

            End With
        End If
        dt = Nothing
    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadProvince()
    End Sub


     

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())

                    'lblCode.Text = grdData.Rows(
                Case "imgDel"
                    ctlbase.Province_Delete(e.CommandArgument)
                    acc.User_GenLogfile(Request.Cookies("UserLoginID").Value, ACTTYPE_DEL, "Roles", "Delete Province:" & lblCode.Text & ">>" & txtName.Text, "")
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','ลบข้อมูลเรียบร้อย');", True)
                    LoadProvince()
            End Select


        End If
    End Sub


    Private Sub EditData(ByVal pID As Integer)

        dt = ctlbase.Province_GetByID(pID)
         
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False

                Me.lblCode.Text = DBNull2Str(dt.Rows(0)("ProvinceID"))
                txtName.Text = DBNull2Str(dt.Rows(0)("ProvinceName"))

            End With
        End If
        dt = Nothing  
    End Sub
    Private Sub ClearData()
        Me.lblCode.Text = ""
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

        If txtName.Text = "" Then

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If
        Dim item As Integer

        If lblCode.Text = "" Then

            item = ctlbase.Province_Add(txtName.Text)

            acc.User_GenLogfile(Request.Cookies("UserLoginID").Value, ACTTYPE_ADD, "Province", "Add new Province:" & txtName.Text, "")

        Else
            item = ctlbase.Province_Update(lblCode.Text, txtName.Text)

            acc.User_GenLogfile(Request.Cookies("UserLoginID").Value, ACTTYPE_UPD, "Province", "Update Province:" & txtName.Text, "")

        End If

        LoadProvince()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)

    End Sub
End Class

