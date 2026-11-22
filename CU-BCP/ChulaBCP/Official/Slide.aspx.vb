Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO
Public Class Slide
    Inherits System.Web.UI.Page

    Dim dtS As New DataTable
    Dim ctlS As New SlideController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If

        If Not IsPostBack Then
            lblUID.Text = "0"
            dgv1.PageIndex = 0
            GetItem()
        End If

    End Sub
    Private Sub GetItem()
        dtS = ctlS.SlideShow_Get
        If dtS.Rows.Count > 0 Then
            dgv1.DataSource = dtS
            dgv1.DataBind()
        End If
        dtS = Nothing
    End Sub

    Protected Sub DeleteItem(ByVal sender As Object, ByVal e As EventArgs)

        Dim lnkDel As LinkButton = DirectCast(sender, LinkButton)
        ctlS.SlideShow_Delete(lnkDel.CommandArgument)

        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

    End Sub
    Protected Sub ActiveItem(ByVal sender As Object, ByVal e As EventArgs)

        Dim lnkOk As LinkButton = DirectCast(sender, LinkButton)
        ctlS.SlideShow_ActiveSlide(lnkOk.CommandArgument)

        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

    End Sub

    Protected Sub InActiveItem(ByVal sender As Object, ByVal e As EventArgs)

        Dim lnkInActive As LinkButton = DirectCast(sender, LinkButton)
        ctlS.SlideShow_InActiveSlide(lnkInActive.CommandArgument)
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

    End Sub

    Protected Sub bttSave_Click(sender As Object, e As System.EventArgs) Handles bttSave.Click


        Dim UlFileName As String = ""
        If FileUpload1.HasFile Then
            UlFileName = FileUpload1.FileName
            Dim filename1 As String = Path.GetFileName(UlFileName)
            FileUpload1.SaveAs(Server.MapPath("~/imageSlide/" + UlFileName))
        Else
            UlFileName = ""
        End If

        Dim sURL As String

        If txtURL.Text.Trim = "" Then
            sURL = UlFileName.ToString()
        Else
            sURL = txtURL.Text
        End If

        If lblUID.Text = "0" Then
            ctlS.SlideShow_Add(UlFileName.ToString(), txtTitle.Text, "", "A", "Images/icon/ok.png", sURL)
        Else
            ctlS.SlideShow_Update(lblUID.Text, txtTitle.Text, sURL)

            lblUID.Text = "0"
            txtTitle.Text = ""
            txtURL.Text = ""
        End If

        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Protected Sub dgv1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles dgv1.PageIndexChanging
        dgv1.PageIndex = e.NewPageIndex
        GetItem()
    End Sub
    Private Sub EditData(UID As Integer)
        dtS = ctlS.SlideShow_GetByID(UID)
        If dtS.Rows.Count > 0 Then
            lblUID.Text = dtS.Rows(0)("UID")
            txtTitle.Text = String.Concat(dtS.Rows(0)("Title"))
            txtURL.Text = String.Concat(dtS.Rows(0)("URL"))
        End If

    End Sub
    Protected Sub dgv1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgv1.RowCommand
        If TypeOf e.CommandSource Is WebControls.LinkButton Then
            Dim ButtonPressed As WebControls.LinkButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "lnkEdit"
                    EditData(e.CommandArgument())
                    'Case "imgDel"
                    '    If ctlLG.ActionStatus_Delete(e.CommandArgument) Then
                    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                    '        LoadActionStatusToGrid()
                    '    Else
                    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)

                    '    End If


            End Select


        End If
    End Sub
End Class