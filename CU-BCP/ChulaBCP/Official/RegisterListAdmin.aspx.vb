Public Class RegisterListAdmin
    Inherits System.Web.UI.Page
    Dim ctlReg As New RegisterController
    Dim dt As New DataTable
    Public dtReg As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            LoadYearToDDL()
            LoadTopic()
        End If
        LoadData()
    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlReg.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlReg.Register_GetYear
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "EduYear"
                .DataValueField = "EduYear"
                .DataBind()

                If dt.Rows(LastRow)(0) = y Then
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 1).Value = y + 1
                ElseIf dt.Rows(LastRow)(0) > y Then
                    'ddlYear.Items.Add(y + 2)
                    'ddlYear.Items(LastRow + 1).Value = y + 2
                ElseIf dt.Rows(LastRow)(0) < y Then
                    ddlYear.Items.Add(y)
                    ddlYear.Items(LastRow + 1).Value = y
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 2).Value = y + 1
                End If
                .SelectedIndex = 0
            End With
        Else
            ddlYear.Items.Add(y)
            ddlYear.Items(0).Value = y
            ddlYear.Items.Add(y + 1)
            ddlYear.Items(1).Value = y + 1
            ddlYear.SelectedIndex = 0
        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        dt = Nothing
    End Sub

    Private Sub LoadTopic()
        dt = ctlReg.RegisterTopic_Get
        ddlTopic.DataSource = dt
        ddlTopic.DataValueField = "UID"
        ddlTopic.DataTextField = "TopicName"
        ddlTopic.DataBind()
        dt = Nothing
    End Sub

    Private Sub LoadData()
        dtReg = ctlReg.Register_GetBySearch(StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddltopic.selectedvalue))
    End Sub
    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        LoadData()
    End Sub
End Class