
Imports DevExpress.Web.Internal
Imports System.IO

Public Class DocumentGen
    Inherits System.Web.UI.Page
    Dim ctlReg As New DocumentController
    Dim dtDoc As New DataTable
    Dim dt As New DataTable
    Public dtSTD As New DataTable
    Dim ctlM As New MasterController
    Dim doc As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SqlDataSource1.InsertParameters.Add("EduYear", "2564")
        'SqlDataSource1.InsertParameters.Add("TermNo", "1")
        'SqlDataSource1.InsertParameters.Add("TopicUID", "1")
        If IsNothing(Request.Cookies("UserLogin").Value) Then
            Response.Redirect("Login.aspx")
        End If
        SqlDataSource1.ConnectionString = ApplicationBaseClass.ConnectionString
        If Not Request("id") = Nothing Then
            dtDoc = ctlM.DocumentOfficial_Get(Request("id").ToString)
            doc = dtDoc.Rows(0).Item("DocumentName")
            lblDocumentName.Text = doc

            Dim ctlCfg As New SystemConfigController
            lblDirectory.Text = ctlCfg.SystemConfig_GetByCode("WORKDIR")
            ASPxRichEdit1.WorkDirectory = lblDirectory.Text

            If Not Directory.Exists(lblDirectory.Text) Then
                Directory.CreateDirectory(lblDirectory.Text)
            End If

        End If

        If Not IsPostBack Then
            'LoadTopic(dtDoc.Rows(0).Item("TopicTable"))
            LoadYearToDDL()
            'If Not Request("Topic") = Nothing Then
            '    If dtDoc.Rows(0).Item("FileDataName") = "RPT_Register_GetForDocument1" Then
            '        ddlYear.SelectedValue = Request("EduYear").ToString
            '        optTerm.SelectedValue = Request("TermNo").ToString
            '        ddlTopic.SelectedValue = Request("Topic").ToString
            '        txtNumber.Text = Request("Number").ToString
            '    Else
            '        txtRegDate.Text = DisplayShortDateTH(Request("DateS").ToString)
            '        txtRegEndDate.Text = DisplayShortDateTH(Request("DateE").ToString)
            '        ddlTopic.SelectedValue = Request("Topic").ToString
            '    End If

            'End If
            txtRegDate.Text = DisplayShortDateTH(ctlReg.GET_DATE_SERVER())
            txtRegEndDate.Text = DisplayShortDateTH(ctlReg.GET_DATE_SERVER())

        End If

        LoadReport()

        txtNumber.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub

    Protected Sub ASPxRichEdit1_PreRender(sender As Object, e As EventArgs) Handles ASPxRichEdit1.PreRender
        ASPxRichEdit1.Focus()
    End Sub

    'Private Sub LoadTopic(store As String)
    '    dt = ctlM.DocumentOfficial_TopicGet(store)
    '    ddlTopic.DataSource = dt
    '    ddlTopic.DataValueField = "UID"
    '    ddlTopic.DataTextField = "TopicName"
    '    ddlTopic.DataBind()
    'End Sub
    Private Sub LoadYearToDDL()
        dt = ctlM.GetEduYear
        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "EduYear"
                .DataValueField = "EduYear"
                .DataBind()
            End With
        End If

        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value

        dt = Nothing
    End Sub

    Protected Sub cmdCreate_Click(sender As Object, e As EventArgs) Handles cmdCreate.Click
        'If dtDoc.Rows(0).Item("FileDataName") = "RPT_Register_GetForDocument1" Then
        '    Response.Redirect("DocumentGen?m=v&id=" + Request("id").ToString + "&EduYear=" + ddlYear.SelectedValue + "&TermNo=" + optTerm.SelectedValue + "&Number=" + txtNumber.Text + "&Topic=" + ddlTopic.SelectedValue)
        'Else
        '    Response.Redirect("DocumentGen?m=v&id=" + Request("id").ToString + "&DateS=" + ConvertStrDate2DateQueryString(txtRegDate.Text) + "&DateE=" + (txtRegEndDate.Text) + "&Topic=" + ddlTopic.SelectedValue)
        'End If
        LoadReport()
    End Sub

    Private Sub LoadReport()
        SqlDataSource1.SelectParameters.Clear()
        SqlDataSource1.SelectCommand = dtDoc.Rows(0).Item("FileDataName")
        Select Case dtDoc.Rows(0).Item("UID")
            Case 1, 2
                SqlDataSource1.SelectParameters.Add("EduYear", ddlYear.SelectedValue)
                SqlDataSource1.SelectParameters.Add("TermNo", ddlTerm.SelectedValue)
                SqlDataSource1.SelectParameters.Add("DocUID", Request("id"))
                SqlDataSource1.SelectParameters.Add("Number", txtNumber.Text)
                SqlDataSource1.SelectParameters.Add("LevelFrom", txtLevelFrom.Text)
                SqlDataSource1.SelectParameters.Add("LevelTo", txtLevelTo.Text)
                divRequestTopic_Get.Visible = False
                divRegisterTopic_Get.Visible = True

            Case 3, 4
                SqlDataSource1.SelectParameters.Add("DateS", ConvertStrDate2DateQueryString(txtRegDate.Text))
                SqlDataSource1.SelectParameters.Add("DateE", ConvertStrDate2DateQueryString(txtRegEndDate.Text))
                SqlDataSource1.SelectParameters.Add("DocUID", Request("id"))
                divRequestTopic_Get.Visible = True
                divRegisterTopic_Get.Visible = False
        End Select
   
        ASPxRichEdit1.Open(Server.MapPath("~/Documents/Official/" + doc + ".doc"))
        ASPxRichEdit1.ActiveTabIndex = 4

    End Sub
    Protected Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        Using stream As New MemoryStream()
            ASPxRichEdit1.ExportToPdf(stream)
            HttpUtils.WriteFileToResponse(Page, stream, "ExportedDocument", True, "pdf")
        End Using
    End Sub
End Class