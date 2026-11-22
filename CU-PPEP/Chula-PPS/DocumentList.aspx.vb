Public Class DocumentList
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    'Dim ctlCs As New Coursecontroller
    'Dim acc As New UserController
    Dim ctlDoc As New DocumentController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            Dim ctlCfg As New SystemConfigController
            cmdExport.Visible = False
            cmdCancel.Visible = False
            pnDetail.Visible = False


            lblYear.Text = ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR)
            txtDocDate.Text = DisplayDateTH(Today.Date())

            'LoadYearToDDL()
            'LoadCourseToDDL()
            'LoadDocument()
            LoadDocumentDataToGrid()

            txtDocNo.Text = ctlDoc.RunningNumber_New(Session("DocumentCode"), StrNull2Zero(lblYear.Text))

            'If Request("action") = "Y" Then
            '    lblMsg.Visible = True
            'Else
            '    lblMsg.Visible = False
            'End If
        End If

    End Sub

    'Private Sub LoadDocument()
    '    dt = ctlDoc.Documents_Get()
    '    If dt.Rows.Count > 0 Then
    '        With ddlDocumentName
    '            .Visible = True
    '            .DataSource = dt
    '            .DataTextField = "SubjectName"
    '            .DataValueField = "DocumentCode"
    '            .DataBind()
    '            .SelectedIndex = 0
    '        End With
    '    Else
    '        ddlDocumentName.DataSource = Nothing
    '    End If
    'End Sub
    Private Sub LoadDocumentDataToGrid()

        dt = ctlDoc.Document_Get()

        If dt.Rows.Count > 0 Then
            lblCount.Text = dt.Rows.Count

            With grdData
                .Visible = True
                .DataSource = dt
                .DataBind()
            End With
        Else
            lblCount.Text = 0
            grdData.Visible = False
        End If
    End Sub

    'Private Sub LoadCourseToDDL()
    '    ddlCourse.Items.Clear()

    '    dt = ctlCs.Courses_GetByYear(txtYear.Text)

    '    If dt.Rows.Count > 0 Then
    '        ddlCourse.Items.Clear()
    '        For i = 0 To dt.Rows.Count - 1
    '            With ddlCourse
    '                .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("NameTH"))
    '                .Items(i).Value = dt.Rows(i)("CourseID")
    '            End With
    '        Next

    '    End If

    'End Sub

    Private Sub EditData(pUID As Integer)


        dt = ctlDoc.DocumentTemplate_GetByUID(StrNull2Zero(lblYear.Text), pUID)
        If dt.Rows(0)("nCount") > 0 Then
            pnDetail.Visible = True
            Session("DocumentCode") = String.Concat(dt.Rows(0)("DocumentCode"))
            lblSubjectTitle.Text = String.Concat(dt.Rows(0)("SubjectTitle"))
            txtDocNo.Text = StrNull2Zero(String.Concat(dt.Rows(0)("RunningNumber"))) + 1

            'txtPhaseName.Text = String.Concat(dt.Rows(0)("PhaseName"))
            txtDocDate.Text = String.Concat(dt.Rows(0)("DocumentDate"))
            txtStdLevel.Text = String.Concat(dt.Rows(0)("LevelClass"))
            'txtContent1.Text = String.Concat(dt.Rows(0)("Content1"))
            'txtContent2.Text = String.Concat(dt.Rows(0)("Content2"))
            lblUID.Text = dt.Rows(0)("DocumentUID")

            'txtDocNo.Text = ctlDoc.RunningNumber(Request.Cookies("DocumentCode"), StrNull2Zero(txtYear.Text))

        Else
            txtDocNo.Text = "1"
            lblSubjectTitle.Text = String.Concat(dt.Rows(0)("SubjectTitle"))
            lblUID.Text = dt.Rows(0)("UID")
            pnDetail.Visible = True
        End If

        cmdExport.Visible = True
        cmdCancel.Visible = True

    End Sub
    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument)
            End Select


        End If
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            e.Row.Cells(0).Text = e.Row.RowIndex + 1

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub
    'Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged

    '    grdData.PageIndex = 0
    '    LoadDocumentDataToGrid()
    'End Sub

    Protected Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click


        Select Case StrNull2Zero(lblUID.Text)
            Case 1
                ReportName = "Document01"
                ctlDoc.DocumentPrint_Add(StrNull2Zero(lblUID.Text), StrNull2Zero(lblYear.Text), txtDocDate.Text, StrNull2Zero(txtDocNo.Text), txtStdLevel.Text, Request.Cookies("UserID").Value)
            Case 2
                ReportName = "Document02"
                ctlDoc.DocumentPrint_Add(StrNull2Zero(lblUID.Text), StrNull2Zero(lblYear.Text), txtDocDate.Text, StrNull2Zero(txtDocNo.Text), txtStdLevel.Text, Request.Cookies("UserID").Value)
            Case 3
                ReportName = "Document03"
                ctlDoc.DocumentPrint_SendTeacher_Add(StrNull2Zero(lblUID.Text), StrNull2Zero(lblYear.Text), txtDocDate.Text, StrNull2Zero(txtDocNo.Text), txtStdLevel.Text, Request.Cookies("UserID").Value)
            Case Else

        End Select


        FagRPT = "DOC"
        Reportskey = "XLS"
        pnDetail.Visible = False

        Response.Redirect("ReportServerViewer.aspx?y=" & StrNull2Zero(lblYear.Text) & "&d=" & StrNull2Zero(lblUID.Text))
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearData()
    End Sub
    Private Sub ClearData()
        lblUID.Text = ""
        'txtDocNo.Text = ctlDoc.RunningNumber(Request.Cookies("DocumentCode"), StrNull2Zero(txtYear.Text))
        txtDocDate.Text = DisplayDateTH(Today.Date())
        txtStdLevel.Text = ""

    End Sub

End Class

