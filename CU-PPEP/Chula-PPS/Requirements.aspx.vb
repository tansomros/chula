Public Class Requirements
    Inherits System.Web.UI.Page

    Dim ctlLG As New LocationController
    Dim ctlReq As New REQcontroller

    Dim objLct As New LocationInfo
    Dim dt As New DataTable
    Dim ds As New DataSet

    Dim acc As New UserController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then

            lblValidate.Visible = False

            If Request.Cookies("LANG").Value.ToLower() = "en" Then
                lblValidate.Text = "Please specify the internship batch for the desired year first."
            Else
                lblValidate.Text = "กรุณากำหนดผลัดฝึกของปีที่ต้องการก่อน"
            End If


            LoadYearToDDL()
            LoadLocationToDDL()


            If Not Request("y") Is Nothing Then
                ddlLocation.SelectedValue = Request("id")
                ddlYear.SelectedValue = Request("y")
                LoadRequirements(Request("y"), Request("id"))
            End If

            If Request.Cookies("ROLE_PCT").Value = True Then
                optGender.Enabled = True
            Else
                optGender.Enabled = False
            End If

        End If

        '.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        ' txtConfirm.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub

    Private Sub LoadRequirements(sYear As Integer, sLocationID As Integer)

        Dim dtReq As New DataTable

        dtReq = ctlReq.RequirementEstimate_GetByLocation(sYear, sLocationID)

        If dtReq.Rows.Count > 0 Then
            With grdData
                .Visible = True
                .DataSource = dtReq
                .DataBind()
            End With
            lblValidate.Visible = False

        Else
            lblValidate.Visible = True
            lblValidate.Text = "กรุณากำหนดส่วนงานก่อน/Please specify the department first."
            grdData.Visible = False
        End If

        'optGender.SelectedValue = ctlLG.Location_GetIsSameGender(sLocationID)

    End Sub

    Private Sub LoadYearToDDL()

        Dim y As Integer = StrNull2Zero(DisplayYear(ctlReq.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlReq.REQ_GetYear(Request.Cookies("LANG").Value)
        LastRow = dt.Rows.Count - 1

        If dt.Rows.Count > 0 Then
            With ddlYear
                .Enabled = True
                .DataSource = dt
                .DataTextField = "YearText"
                .DataValueField = "YearValue"
                .DataBind()

                If dt.Rows(LastRow)(0) = y Then
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 1).Value = y + 1
                ElseIf dt.Rows(LastRow)(0) > y Then
                    'ddlYear.Items.Add(y + 2)
                    'ddlYear.Items(LastRow + 1).Value = y + 2
                ElseIf dt.Rows(LastRow)(0) < y Then
                    ddlYear.Items.Add(y)
                    ddlYear.Items(LastRow + 1).Value = y
                    ddlYear.Items.Add(y + 1)
                    ddlYear.Items(LastRow + 2).Value = y + 1
                End If
                .SelectedIndex = 0
            End With
        Else
            ddlYear.Items.Add(y)
            ddlYear.Items(0).Value = y
            ddlYear.Items.Add(y + 1)
            ddlYear.Items(1).Value = y + 1
            ddlYear.SelectedIndex = 0
        End If
        ddlYear.SelectedValue = Request.Cookies("EDUYEAR").Value
        dt = Nothing
    End Sub

    Private Sub LoadLocationToDDL()
        dt = ctlLG.Location_Get
        ddlLocation.Items.Clear()
        With ddlLocation
            .Items.Add("---เลือกแหล่งฝึก---")
            .Items(0).Value = 0
            If dt.Rows.Count > 0 Then
                .Visible = True
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add(dt.Rows(i)("LocationName"))
                    .Items(i + 1).Value = dt.Rows(i)("LocationID")
                Next
                If Request.Cookies("ROLE_PCT").Value = True Then
                    .SelectedValue = Request.Cookies("LocationID").Value
                    .Enabled = False
                    LoadRequirements(ddlYear.SelectedValue, ddlLocation.SelectedValue)
                End If


            End If
        End With
    End Sub

    Protected Function validateData() As Boolean
        Dim result As Boolean = True
        lblValidate.Text = ""

        If ddlLocation.SelectedValue = "0" Then
            result = False
            lblValidate.Text &= "- กรุณาระบุแหล่งฝึก /Select Site <br />"
            lblValidate.Visible = True

        End If

        Return result
    End Function

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If validateData() Then

            lblValidate.Visible = False

            For i = 0 To grdData.Rows.Count - 1

                Dim txtSc1 As TextBox = grdData.Rows(i).Cells(2).FindControl("txtQTY1")
                Dim txtSc2 As TextBox = grdData.Rows(i).Cells(3).FindControl("txtQTY2")
                Dim optSex As RadioButtonList = grdData.Rows(i).Cells(4).FindControl("optSex")

                ctlReq.RequirementEstimate_Save(StrNull2Zero(ddlYear.SelectedValue), ddlLocation.SelectedValue, grdData.DataKeys(i).Value, StrNull2Zero(txtSc1.Text), StrNull2Zero(txtSc2.Text), optSex.SelectedValue, optGender.SelectedValue, Request.Cookies("Username").Value)

            Next


            'Dim ctlL As New LocationController
            'ctlLG.Location_UpdateisSameGender(ddlLocation.SelectedValue, optGender.SelectedValue)

            '    acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Locations", "แก้ไข ความต้องการ:" & ddlLocation.SelectedValue, "")

            'ClearData()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','Your data has been saved successfully.');", True)
        Else

        End If

    End Sub

    Function isDupicate_Account(pID As Integer) As Boolean
        dt = ctlReq.Requirements_Get(ddlYear.SelectedValue, ddlLocation.SelectedValue, pID)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Function isDupicate_REQ() As Boolean
        dt = ctlReq.REQ_GetByPKKey(ddlYear.SelectedValue, ddlLocation.SelectedValue, "")
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function


    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        LoadRequirements(ddlYear.SelectedValue, ddlLocation.SelectedValue)
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadRequirements(ddlYear.SelectedValue, ddlLocation.SelectedValue)
    End Sub

    Private Sub grdData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim txt1 As TextBox = e.Row.Cells(2).FindControl("txtQTY1")
            Dim txt2 As TextBox = e.Row.Cells(3).FindControl("txtQTY2")
            Dim optS As RadioButtonList = e.Row.Cells(4).FindControl("optSex")

            If Request.Cookies("ROLE_PCT").Value = True Then
                txt1.Enabled = False
                txt1.BackColor = Drawing.Color.FromArgb(244, 244, 244)
                txt2.Enabled = True
                optS.Enabled = True
            Else
                txt1.Enabled = True
                txt2.Enabled = False
                txt2.BackColor = Drawing.Color.FromArgb(244, 244, 244)
                optS.Enabled = False

            End If


        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub
End Class

