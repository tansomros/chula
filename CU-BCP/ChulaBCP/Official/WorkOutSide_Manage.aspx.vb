Public Class WorkOutSide_Manage
    Inherits System.Web.UI.Page
    Dim ctlM As New SeminarController
    Dim ctlRf As New ReferenceValueController
    Dim dt As New DataTable
    Dim sAlert As String
    Dim isValid As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("../Login.aspx?logout=y")
        End If
        If Not IsPostBack Then
            pnAlert.Visible = False

            LoadPerson()
            LoadWorkType()
            LoadSeminarGroup()
            LoadCountry()
            LoadBudget()

            If Not Request("id") Is Nothing Then
                EditData(Request("id"))
            End If
        End If
    End Sub
    Private Sub LoadPerson()
        Dim ctlP As New PersonController
        dt = ctlP.Person_GetByType(1)
        ddlPerson.DataSource = dt
        ddlPerson.DataValueField = "PersonID"
        ddlPerson.DataTextField = "PersonName_TH"
        ddlPerson.DataBind()
        dt = Nothing
    End Sub

    Private Sub LoadWorkType()
        dt = ctlRf.ReferenceValue_GetByDomainCode("WKTYPE")
        ddlWorkType.DataSource = dt
        ddlWorkType.DataValueField = "Code"
        ddlWorkType.DataTextField = "Descriptions"
        ddlWorkType.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadSeminarGroup()
        dt = ctlRf.ReferenceValue_GetByDomainCode("SEMGRP")
        ddlGroup.DataSource = dt
        ddlGroup.DataValueField = "Code"
        ddlGroup.DataTextField = "Descriptions"
        ddlGroup.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadCountry()
        dt = ctlRf.ReferenceValue_GetByDomainCode("COUNTRY")
        ddlCountry.DataSource = dt
        ddlCountry.DataValueField = "Code"
        ddlCountry.DataTextField = "Descriptions"
        ddlCountry.DataBind()
        dt = Nothing
    End Sub
    Private Sub LoadBudget()
        dt = ctlRf.ReferenceValue_GetByDomainCode("BUDGET")
        ddlBudget.DataSource = dt
        ddlBudget.DataValueField = "Code"
        ddlBudget.DataTextField = "Descriptions"
        ddlBudget.DataBind()
        dt = Nothing
    End Sub
    Private Sub EditData(ByVal pcode As Integer)
        dt = ctlM.Seminar_GetByUID(pcode)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.lblUID.Text = DBNull2Str(dt.Rows(0)("UID"))

                ddlPerson.SelectedValue = dt.Rows(0)("PersonID")
                ddlWorkType.SelectedValue = dt.Rows(0)("WKTYPE")
                ddlGroup.SelectedValue = dt.Rows(0)("SEMGRP")
                ddlCountry.SelectedValue = dt.Rows(0)("COUNTRY")
                ddlBudget.SelectedValue = dt.Rows(0)("BUDGET")

                optIsPresented.SelectedValue = dt.Rows(0)("isPresented")

                txtStartDate.Text = dt.Rows(0)("StartDate")
                txtEndDate.Text = dt.Rows(0)("EndDate")
                txtPresentType.Text = dt.Rows(0)("PresentedType")

                txtPortfolioName.Text = DBNull2Str(dt.Rows(0)("PortfolioName"))
                txtSeminarName.Text = DBNull2Str(dt.Rows(0)("SeminarName"))

                chkStatus.Checked = True
                'chkStatus.Checked = ConvertStatusFlag2CHK(dt.Rows(0)("StatusFlag"))
            End With
        End If
        dt = Nothing
    End Sub
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If ValidateData() = False Then
            pnAlert.Visible = True
            lblAlert.Text = sAlert
            Exit Sub
        Else
            pnAlert.Visible = False
        End If

        ctlM.Seminar_Save(StrNull2Zero(lblUID.Text), ddlPerson.SelectedValue, ddlWorkType.SelectedValue, ddlGroup.SelectedValue, ConvertStrDate2InformDateString(txtStartDate.Text), ConvertStrDate2InformDateString(txtEndDate.Text), optIsPresented.SelectedValue, txtPresentType.Text, txtPortfolioName.Text, txtSeminarName.Text, ddlCountry.SelectedValue, ddlBudget.SelectedValue, Session("userid"))
        'ConvertBoolean2StatusFlag(chkStatus.Checked))
        'DisplayMessage(Me.Page, "บันทึกข้อมูลเรียบร้อย")

        Response.Redirect("WorkOutSideList.aspx")
        ClearData()
    End Sub

    Function ValidateData() As Boolean
        sAlert = "กรุณาตรวจสอบ <br />"
        isValid = True

        If txtStartDate.Text = "" Or txtEndDate.Text = "" Then
            sAlert &= "- ระบุวันที่ให้ครบถ้วน <br/>"
            isValid = False
        End If

        If Not CheckDateValid(txtStartDate.Text) Then
            sAlert &= "- ตรวจสอบวันที่ให้ถูกต้อง <br/>"
            isValid = False
        End If


        'If txtPresentType.Text = "" Then
        '    sAlert &= "- ประเภทผลงาน <br/>"
        '    isValid = False
        'End If


        If txtPortfolioName.Text.Trim = "" Then
            sAlert &= "- ชื่อผลงาน <br/>"
            isValid = False

        End If
        If txtSeminarName.Text.Trim = "" Then
            sAlert &= "- ชื่องานประชุม <br/>"
            isValid = False
        End If

        Return isValid

    End Function


    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearData()
    End Sub
    Private Sub ClearData()
        lblUID.Text = ""
        txtPortfolioName.Text = ""
        txtSeminarName.Text = ""
        chkStatus.Checked = True
    End Sub
End Class