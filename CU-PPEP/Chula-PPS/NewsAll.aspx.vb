Public Class NewsAll
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlNews As New NewsController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ' ModalPopupExtender1.Hide()

            If Request("logout") = "YES" Then
                Session.Abandon()
                Request.Cookies("Username").Value = Nothing
            End If

            LoadNewsToGrid()
            Dim ctlS As New SystemConfigController
            lblVersion.Text = ctlS.SystemConfig_GetByCode("Version")
        End If

    End Sub
    Private Sub LoadNewsToGrid()

        dt = ctlNews.News_GetByStatus

        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()

            For i = 0 To .Rows.Count - 1
                Dim img1 As Image = .Rows(i).Cells(0).FindControl("imgNews")

                Dim hlnkN As HyperLink = .Rows(i).Cells(0).FindControl("hlnkNews")
                hlnkN.Text = dt.Rows(i)("Title")

                Select Case dt.Rows(i)("NewsType")
                    Case "URL"
                        img1.ImageUrl = "images/www.png"
                        hlnkN.NavigateUrl = String.Concat(dt.Rows(i)("LinkPath"))
                    Case "UPL"

                        Dim str As String()
                        str = Split(Right(String.Concat(dt.Rows(i)("LinkPath")), 6), ".")

                        Select Case str(1)
                            Case "pdf"
                                img1.ImageUrl = "images/pdf.png"
                            Case "doc", "docx"
                                img1.ImageUrl = "images/word.png"
                            Case "ppt", "pptx"
                                img1.ImageUrl = "images/ppt.png"
                            Case "xls", "xlsx"
                                img1.ImageUrl = "images/xls.png"
                            Case Else
                                img1.ImageUrl = "images/pdf.png"
                        End Select

                        hlnkN.NavigateUrl = tmpUpload & "/" & String.Concat(dt.Rows(i)("LinkPath"))
                    Case "CON"
                        img1.ImageUrl = "images/newspaper.jpg"
                        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
                    Case Else
                        img1.ImageUrl = "images/newspaper.jpg"
                        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
                End Select
            Next

        End With


    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub
End Class
