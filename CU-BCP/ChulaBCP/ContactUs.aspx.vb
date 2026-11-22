Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class ContactUs
    Inherits System.Web.UI.Page

    Dim ctlA As New PageController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("RoleID") <> "0" Then
            bttEdit.Visible = False
        End If
        If Not Page.IsPostBack Then
            GetData()
        End If
    End Sub

    Private Sub GetData()
        Dim dt As New DataTable
        Dim dtM As New DataTable

        ctlA.PageContent_Counting(PAGE_CONTACTUS)
        dt = ctlA.PageContent_Get(PAGE_CONTACTUS)

        lblThai.Text = dt.Rows(0)("Contents").ToString()

        dtM = ctlA.PageContent_Get(PAGE_CONTACTMAP)
        lblMaps.Text = dtM.Rows(0)("Contents")

        dt = Nothing
        dtM = Nothing
    End Sub
End Class