Imports Microsoft.ApplicationBlocks.Data
Public Class UserLog
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlUser As New UserController
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("Default.aspx?logout=y")
        End If

        If Not IsPostBack Then
            LoadUserOnline()
            lblNo.Visible = False
            pnAlert.Visible = False
            grdData.PageIndex = 0

            With ddlUserID
                .Enabled = True
                .Items.Add("ทั้งหมด")
                .Items(0).Value = 0
                .SelectedIndex = 0
            End With


            LoadLogfile()
        End If


    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdData.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Private Sub LoadLogfile()
        SQL = ""
        SQL = "select  UL.*,U.Username,U.Name 
	from UserLogFile UL , UserLogin U 
	Where UL.UserID=U.UserID   "

        If txtBeginDate.Text <> "" And txtEndDate.Text <> "" Then
            Dim str() As String
            Dim dY As Integer
            Dim Bdate As String
            Dim Edate As String



            str = Split(Me.txtBeginDate.Text, "/")
            dY = Now.Date.Year - CInt(str(2))
            If dY < 0 Then
                dY = CInt(str(2)) - 543
            End If
            Bdate = str(1) & "/" & str(0) & "/" & dY

            str = Split(Me.txtEndDate.Text, "/")
            dY = Now.Date.Year - CInt(str(2))
            If dY < 0 Then
                dY = CInt(str(2)) - 543
            End If
            Edate = str(1) & "/" & CInt(str(0)) + 1 & "/" & dY

            If ddlUserID.SelectedValue = 0 Then
                SQL &= "  AND  UL.LogDate  between   '" & Bdate & "' and '" & Edate & "'"

            Else
                SQL &= "  AND  UL.UserID=" & ddlUserID.SelectedValue & " and  UL.LogDate  between   '" & Bdate & "' and '" & Edate & "'"

            End If
        Else
            If ddlUserID.SelectedValue <> 0 Then
                SQL &= "  AND  UL.UserID=" & ddlUserID.SelectedValue
            Else
                SQL &= "  AND UL.LogID>=1 "
            End If

            'Controls.Add(New LiteralControl("<script>alert('กรุณาตรวจสอบวันที่');</script>"))
            'Exit Sub
        End If

        SQL &= " order by UL.logid  desc"


        ds = SqlHelper.ExecuteDataset(ApplicationBaseClass.ConnectionString, CommandType.Text, SQL)
        dt = ds.Tables(0)


        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()

            End With
        Else
            grdData.Visible = False
        End If
    End Sub
    Private Sub LoadUserOnline()
        dt = ctlUser.Users_Online
        lblOnline.Text = "<font color=#006400>User ที่กำลังใช้งาน >> </font>"
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                If i Mod 2 = 0 Then
                    lblOnline.Text &= "<font color=#1878ca>" & "[" & dt.Rows(i)("Username") & " : " & dt.Rows(i)("Name") & "]" & "</font>"
                Else
                    lblOnline.Text &= "<font color=#990099>" & "[" & dt.Rows(i)("Username") & " : " & dt.Rows(i)("Name") & "]" & "</font>"
                End If

            Next

        Else
            lblOnline.Text = "ไม่มี User Online ในขณะนี้"
        End If

    End Sub
    Private Sub LoadUserToDDL()
        If txtFind.Text = "" Then
            DisplayMessage(Me.Page, "กรุณาป้อนคำค้นหาก่อน")
            Exit Sub
        Else
            dt = ctlUser.User_GetBySearch(txtFind.Text)
        End If
        ddlUserID.Items.Clear()
        If dt.Rows.Count > 0 Then
            With ddlUserID
                .Enabled = True
                .Items.Add("---ทั้งหมด---")
                .Items(0).Value = 0
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & dt.Rows(i)("Name"))
                    .Items(i + 1).Value = dt.Rows(i)("UserID")
                Next
                .SelectedIndex = 0
            End With
        End If
    End Sub
    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadLogfile()
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        grdData.PageIndex = 0
        LoadLogfile()
    End Sub

    Protected Sub lnkFind_Click(sender As Object, e As EventArgs) Handles lnkFind.Click
        LoadUserToDDL()
    End Sub
End Class