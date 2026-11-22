
Public Class ChangeUsername
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim objUser As New UserController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Request.Cookies("ROLE_ADM").Value = False Then
            Response.Redirect("ResultPage.aspx?p=unrole")
        End If

        If Not IsPostBack Then
            grdData.PageIndex = 0
            txtUsername.ReadOnly = False
            isAdd = True
            LoadUserAccountToGrid()
            ClearData()
        End If

        'txtUsername1.Attributes.Add("OnKeyPress", "return NotAllowThai();")
        'txtUsername2.Attributes.Add("OnKeyPress", "return NotAllowThai();")
    End Sub

    Private Sub LoadUserAccountToGrid()
        Dim dtU As New DataTable

        dtU = objUser.User_GetBySearch(0, txtSearch.Text)

        If dtU.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dtU
                .DataBind()
            End With
        Else
            grdData.DataSource = Nothing
            grdData.Visible = False
        End If
        dt = Nothing
    End Sub
    Protected Function validateData() As Boolean
        Dim result As Boolean = True
        If txtUsername1.Text = "" Or txtUsername2.Text = "" Then
            result = False
            lblvalidate.Text = "กรุณากำหนด Username ใหม่ก่อน"
            lblvalidate.Visible = True
        Else

            If txtUsername1.Text <> "" Then

                For i = 0 To txtUsername1.Text.Length - 1
                    Select Case Asc(Mid(txtUsername1.Text, i + 1, 1))
                        'Case 48 To 122
                        '    'English
                        'Case 8, 13 ' Backspace = 8, Enter = 13, Delete = 46
                        Case 161 To 240
                            'Thai
                            result = False
                            lblvalidate.Visible = True
                            lblvalidate.Text = "Username ไม่ควรมีอักษรภาษาไทย กรุณาตั้ง Username ใหม่"
                            Exit Function
                    End Select
                Next


                dt = objUser.GetUsers_ByUsername(txtUsername1.Text)
                If dt.Rows.Count > 0 Then
                    result = False
                    lblvalidate.Text = "Username นี้ซ้ำ กรุณาตั้ง Username ใหม่"
                    lblvalidate.Visible = True
                Else
                    lblvalidate.Visible = False
                End If
            End If

        End If


        Return result
    End Function


    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSave.Click

        If txtUsername.Text = "" Then
            DisplayMessage(Me.Page, "เลือก Username ที่ต้องการแก้ไขก่อน")
            Exit Sub
        End If
        If validateData() Then
            Dim item As Integer
            item = objUser.User_ChangeUsername(txtUsername.Text, txtUsername2.Text)

            objUser.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Users", "change username :" & txtUsername.Text, "Result:" & txtUsername1.Text)

            DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")
            LoadUserAccountToGrid()
            ClearData()
            lblvalidate.Visible = False
        Else
            lblvalidate.Visible = True
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง!');", True)
        End If
    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadUserAccountToGrid()
    End Sub
    Private Sub grdData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand

        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
            End Select

        End If

    End Sub
    Private Sub EditData(ByVal pUsername As String)
        ClearData()
        isAdd = False
        txtUsername.Text = pUsername
        txtUsername.BackColor = Drawing.Color.AliceBlue
        txtUsername.ReadOnly = True
    End Sub

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Private Sub ClearData()

        txtUsername.Text = ""
        txtUsername1.Text = ""
        txtUsername2.Text = ""
        txtUsername.ReadOnly = True

        isAdd = True

        lblvalidate.Visible = False

        txtUsername.Enabled = True
        txtUsername1.Enabled = True
        txtUsername2.Enabled = True

        txtUsername.BackColor = Drawing.Color.White
        txtUsername1.BackColor = Drawing.Color.White
        txtUsername2.BackColor = Drawing.Color.White
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If

    End Sub
    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadUserAccountToGrid()
    End Sub
End Class