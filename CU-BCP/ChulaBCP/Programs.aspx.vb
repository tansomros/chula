Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class Programs
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    'Dim ctlA As New PageController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("RoleID") <> "0" Then
            bttEdit.Visible = False
        End If

        If Not Page.IsPostBack Then
            GetData()
        End If
    End Sub

    Private Sub GetData()

        'ctlA.PageContent_Counting(PAGE_PROGRAM)
        'dt = ctlA.PageContent_Get(PAGE_PROGRAM)
        Dim ctlC As New CourseController
        dt = ctlC.Course_GetViewByID(Request("id"))

        If dt.Rows.Count > 0 Then
            lblDetail.Text = dt.Rows(0)("CourseDetail_TH").ToString()
            'lblDetail.Text = dt.Rows(0)("Contents").ToString()
            lblRead.Text = "View " & Format(CInt(dt.Rows(0)("ReadCount")), "#,##0") & ""
            lblUpdate.Text = "Update " & CDate(dt.Rows(0)("MWhen"))
        End If
        dt = Nothing
    End Sub

End Class