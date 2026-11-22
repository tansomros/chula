Public Class Event_Manage
    Inherits System.Web.UI.Page
    Dim ctlM As New EventController

    Dim dt As New DataTable
    Dim sAlert As String
    Dim isValid As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsNothing(Request.Cookies("UserLogin").Value) Then
        '    Response.Redirect("Login.aspx")
        'End If
        If Not IsPostBack Then
            pnAlert.Visible = False

            LoadCategory()

            If Not Request("id") Is Nothing Then
                EditData(Request("id"))
            End If
        End If
    End Sub
    Private Sub LoadCategory()
        dt = ctlM.EventCategory_GetAll()
        ddlCategory.DataSource = dt
        ddlCategory.DataValueField = "UID"
        ddlCategory.DataTextField = "NameTH"
        ddlCategory.DataBind()
        dt = Nothing
    End Sub

    Private Sub EditData(ByVal pcode As Integer)
        dt = ctlM.Events_GetByID(pcode)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.lblUID.Text = DBNull2Str(dt.Rows(0)("UID"))

                ddlCategory.SelectedValue = dt.Rows(0)("EventCategoryUID")

                txtStartDate.Text = dt.Rows(0)("StartDate")
                txtEndDate.Text = dt.Rows(0)("EndDate")

                ddlBHour.SelectedValue = Left(DBNull2Str(dt.Rows(0)("StartTime")), 2)
                ddlBMinus.SelectedValue = Right(DBNull2Str(dt.Rows(0)("StartTime")), 2)
                ddlEHour.SelectedValue = Left(DBNull2Str(dt.Rows(0)("EndTime")), 2)
                ddlEMinus.SelectedValue = Right(DBNull2Str(dt.Rows(0)("EndTime")), 2)

                txtEventName.Text = DBNull2Str(dt.Rows(0)("EventName"))
                txtVenue.Text = DBNull2Str(dt.Rows(0)("Venue"))
                txtOrganizer.Text = DBNull2Str(dt.Rows(0)("Organizer"))
                txtDetail.Html = DBNull2Str(dt.Rows(0)("EventDetail"))
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
        Dim BTime, ETime As String
        BTime = ddlBHour.SelectedValue & ddlBMinus.SelectedValue
        ETime = ddlEHour.SelectedValue & ddlEMinus.SelectedValue

        ctlM.Events_Save(StrNull2Zero(lblUID.Text), ddlCategory.SelectedValue, ConvertStrDate2InformDateString(txtStartDate.Text), ConvertStrDate2InformDateString(txtEndDate.Text), BTime, ETime, txtEventName.Text, txtVenue.Text, txtOrganizer.Text, txtDetail.Html)
        'ConvertBoolean2StatusFlag(chkStatus.Checked))
        '            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย")

        Response.Redirect("EventList.aspx")
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

        If txtEventName.Text.Trim = "" Then
            sAlert &= "- ชื่อกิจกรรม <br/>"
            isValid = False

        End If
        If txtVenue.Text.Trim = "" Then
            sAlert &= "- ชื่อสถานที่จัดกิจกรรม <br/>"
            isValid = False
        End If
        If txtOrganizer.Text.Trim = "" Then
            sAlert &= "- ชื่อผู้จัดงาน <br/>"
            isValid = False
        End If
        Return isValid

    End Function

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearData()
    End Sub
    Private Sub ClearData()
        lblUID.Text = ""
        txtEventName.Text = ""
        txtVenue.Text = ""
        txtOrganizer.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""
        ddlCategory.SelectedIndex = 0
    End Sub
End Class