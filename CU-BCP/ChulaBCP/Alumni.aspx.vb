Public Class Alumni
    Inherits System.Web.UI.Page

    Public dtAlumni As New DataTable
    Dim ctlStd As New StudentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtAlumni = ctlStd.Student_GetAlumni()
    End Sub


End Class