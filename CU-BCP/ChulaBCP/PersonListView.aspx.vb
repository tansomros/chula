Public Class PersonListView
    Inherits System.Web.UI.Page
    Public dtStaff As New DataTable
    Public dtBoard As New DataTable
    Dim ctlP As New PersonController

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            LoadLeader()
            LoadPerson()
        End If
        If Request("PersonType") = 1 Then
            lblHeader.Text = "คณะกรรมการบริหารหลักสูตร"
            lblbreadcrumb.Text = "คณะกรรมการบริหารหลักสูตร"
        ElseIf Request("PersonType") = 2 Then
            lblHeader.Text = "คณาจารย์"
            lblbreadcrumb.Text = "คณาจารย์"
        Else
            lblHeader.Text = "ศิษย์เก่า"
            lblbreadcrumb.Text = "ศิษย์เก่า"
        End If

    End Sub

    Private Sub LoadLeader()

        If Request("PersonType") = 1 Then
            dtBoard = ctlP.Person_GetBoard(1)
        Else
            dtBoard = ctlP.Person_GetLeader(Request("PersonType"))
        End If

        'If dtBoard.Rows.Count > 0 Then
        '    dlLeader.DataSource = dtBoard
        '    dlLeader.DataBind()
        'End If
    End Sub

    Private Sub LoadPerson()
        If Request("PersonType") = 1 Then
            dtStaff = ctlP.Person_GetBoard(2)
        Else
            dtStaff = ctlP.Person_GetStaff(Request("PersonType"))
        End If
        'If dtStaff.Rows.Count > 0 Then
        '    dlPerson.DataSource = dtStaff
        '    dlPerson.DataBind()
        'End If
    End Sub
End Class