Public Class Events
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlE As New EventController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If

        If Session("ROLE_ADM") = True Or Session("ROLE_SPA") = True Then
            hlkManage.Visible = True
        Else
            hlkManage.Visible = False
        End If

        If Not IsPostBack Then
            LoadEventList()
        End If

    End Sub

    Private Sub LoadEventList()

        dt = ctlE.Events_GetList

        With grdMedia4b
            .Visible = True
            .DataSource = dt
            .DataBind()

            For i = 0 To .Rows.Count - 1
                'Dim img1 As Image = .Rows(i).Cells(0).FindControl("imgNews")

                Dim hlnkN As HyperLink = .Rows(i).Cells(0).FindControl("hlnkMedia4b")
                hlnkN.Text = dt.Rows(i)("Title")

                If dt.Rows(i)("Details") <> "" Then
                    hlnkN.NavigateUrl = "EventDetail?id=" & dt.Rows(i)("UID")
                End If


                'Select Case dt.Rows(i)("MediaType")
                '    Case "URL"
                '        img1.ImageUrl = "images/www.png"
                '        hlnkN.NavigateUrl = String.Concat(dt.Rows(i)("LinkPath"))
                '    Case "UPL"
                '        Dim str As String()
                '        str = Split(Right(String.Concat(dt.Rows(i)("LinkPath")), 6), ".")

                '        Select Case str(1)
                '            Case "pdf"
                '                img1.ImageUrl = "images/pdf_download.png"
                '            Case "doc", "docx"
                '                img1.ImageUrl = "images/word.png"
                '            Case "ppt", "pptx"
                '                img1.ImageUrl = "images/ppt.png"
                '            Case "xls", "xlsx"
                '                img1.ImageUrl = "images/xls.png"
                '            Case Else
                '                img1.ImageUrl = "images/pdf.png"
                '        End Select

                '        hlnkN.NavigateUrl = DocumentUpload & "/4/" & String.Concat(dt.Rows(i)("LinkPath"))

                '    Case "CON"
                '        img1.ImageUrl = "images/newspaper.jpg"
                '        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
                '    Case Else
                '        img1.ImageUrl = "images/newspaper.jpg"
                '        hlnkN.NavigateUrl = "News.aspx?id=" & .DataKeys(i).Value
                'End Select

            Next

        End With

    End Sub
End Class