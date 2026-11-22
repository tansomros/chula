Imports Microsoft.ApplicationBlocks.Data

Public Class LogfilesByUser
    Inherits System.Web.UI.Page
    Dim ctlUser As New UserController
    Dim dt As New DataTable
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Not IsPostBack Then
            grdData.PageIndex = 0
            LoadUserToDDL()
        End If
    End Sub

    Private Sub LoadUserToDDL()
        'If txtFind.Text = "" Then
        '    DisplayMessage(Me.Page, "กรุณาป้อนคำค้นหาก่อน")
        '    Exit Sub
        'Else
        '    dt = ctlUser.User_GetBySearch(0, txtFind.Text)
        'End If

        dt = ctlUser.User_Get()
        ddlUserID.Items.Clear()
        With ddlUserID
            .Enabled = True
            .Items.Add("ทั้งหมด")
            .Items(0).Value = 0
        End With

        If dt.Rows.Count > 0 Then
            With ddlUserID
                .Enabled = True
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add("" & dt.Rows(i)("UserName") & " : " & dt.Rows(i)("DisplayName"))
                    .Items(i + 1).Value = dt.Rows(i)("UserID")
                Next
                .SelectedIndex = 0
            End With
        End If
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadLogfile()
    End Sub
    Private Sub LoadLogfile()
        SQL = ""
        SQL = "SELECT   *   FROM    View_Logfile    "

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
                SQL &= "  Where  Work_Date  between   '" & Bdate & "' and '" & Edate & "'"

            Else
                SQL &= "  Where  Userid=" & ddlUserID.SelectedValue & " and  Work_Date  between   '" & Bdate & "' and '" & Edate & "'"

            End If
        Else
            If ddlUserID.SelectedValue <> 0 Then
                SQL &= "  Where  Userid=" & ddlUserID.SelectedValue
            Else
                SQL &= "  Where logid>=1 "
            End If

            'Controls.Add(New LiteralControl("<script>alert('กรุณาตรวจสอบวันที่');</script>"))
            'Exit Sub
        End If

        SQL &= " order by logid  desc"


        ds = SqlHelper.ExecuteDataset(ApplicationBaseClass.ConnectionString, CommandType.Text, SQL)
        dt = ds.Tables(0)


        If dt.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()

                'For i = 0 To dt.Rows.Count - 1
                '    .Rows(i).Cells(0).Text = i + 1
                'Next
            End With
        Else
            grdData.Visible = False
        End If
    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadLogfile()
    End Sub
End Class