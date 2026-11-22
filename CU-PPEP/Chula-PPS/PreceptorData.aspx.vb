Public Class PreceptorData
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim ctlStd As New PersonController
    Dim objUser As New UserController
    Dim ctlbase As New ApplicationBaseClass
    Dim acc As New UserController
    Dim objLct As New LocationInfo
    Dim ctlLct As New LocationController

    Private ReadOnly PhotoFolder As String = "imgPerson/"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            isAdd = True
            ClearData()
            UpdateProgress1.Visible = False
            LoadPrefixToDDL()
            LoadLocationToDDL()
            LoadLocationWork()
            ' Check if editing from query string
            If Not Request("id") Is Nothing Then
                EditData(Request("id"))
            End If
        End If
    End Sub

    Private Sub LoadPrefixToDDL()
        dt = ctlbase.Prefix_GetForPreceptor
        If dt.Rows.Count > 0 Then
            With ddlPrefix
                .Enabled = True
                .DataSource = dt
                .DataTextField = "PrefixName"
                .DataValueField = "PrefixID"
                .DataBind()
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub LoadLocationToDDL()
        dt = ctlLct.Location_Get
        If dt.Rows.Count > 0 Then
            With ddlLocation
                .DataSource = dt
                .Visible = True
                .DataTextField = "LocationName"
                .DataValueField = "LocationID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
    End Sub

    Private Sub LoadLocationWork()
        Dim ctlL As New LocationWorkController
        dt = ctlL.LocationWork_GetByLocation(ddlLocation.SelectedValue)
        With ddlWork
            .DataSource = dt
            .DataTextField = "WorkName"
            .DataValueField = "WorkID"
            .DataBind()
        End With
        dt = Nothing
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        ' Validate required fields
        If txtFirstName.Text = "" Or txtLastName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','กรุณาป้อนชื่อและนามสกุล');", True)
            Exit Sub
        End If

        If txtMail.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','กรุณาป้อน E-mail');", True)
            Exit Sub
        End If

        If txtContactNumber.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','กรุณาป้อนเบอร์โทรศัพท์');", True)
            Exit Sub
        End If

        If txtLicenseNumber.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','กรุณาป้อนเลขที่ใบประกอบวิชาชีพ');", True)
            Exit Sub
        End If

        If txtPositionName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','กรุณาป้อนตำแหน่ง');", True)
            Exit Sub
        End If

        Dim sTraining As String
        If chkTraining.Checked Then
            sTraining = "Y"
        Else
            sTraining = "N"
        End If
        Dim sApprove As String
        If chkApprove.Checked Then
            sApprove = "Y"
        Else
            sApprove = "N"
        End If

        'Dim sMain As String
        'If chkMain.Checked Then
        '    sMain = "Y"
        'Else
        '    sMain = "N"
        'End If
        Dim sActive As String
        If chkActive.Checked Then
            sActive = "A"
        Else
            sActive = "D"
        End If


        ' Handle Photo upload
        Dim photoPath As String = ""
        Dim fileName As String = ""
        If fuPhoto.HasFile Then
            Dim ext As String = System.IO.Path.GetExtension(fuPhoto.FileName).ToLower()
            If ext = ".jpg" Or ext = ".jpeg" Or ext = ".png" Then
                If fuPhoto.PostedFile.ContentLength <= 512000 Then ' 500 KB
                    If StrNull2Zero(hdUID.Value) = 0 Then
                        fileName = "p_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ext
                    Else
                        fileName = hdUID.Value & ext
                    End If
                    Dim savePath As String = Server.MapPath(PhotoFolder & fileName)
                    fuPhoto.SaveAs(savePath)
                    photoPath = PhotoFolder & fileName
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไฟล์รูปภาพขนาดเกิน 500 KB');", True)
                    Exit Sub
                End If
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','กรุณาอัปโหลดไฟล์รูปภาพ JPG หรือ PNG เท่านั้น');", True)
                Exit Sub
            End If
        End If

        ctlStd.Person_Save(StrNull2Zero(hdUID.Value), "P", StrNull2Zero(ddlLocation.SelectedValue), StrNull2Zero(ddlWork.SelectedValue), StrNull2Zero(ddlPrefix.SelectedValue), txtFirstName.Text, txtLastName.Text, txtPositionName.Text, txtMail.Text, txtContactNumber.Text, txtLicenseNumber.Text, StrNull2Double(txtCPECredits.Text), sTraining, sApprove, sActive, fileName, txtCvLink.Text, Request.Cookies("UserID").Value)

        If hdUID.Value = "0" Then
            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Persons", "เพิ่มรายชื่ออาจารย์แหล่งฝึกใหม่ :" & txtFirstName.Text & " " & txtLastName.Text, "เพิ่มแบบทีละคน")
            ClearData()
        Else
            acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_UPD, "Persons", "แก้ไขชื่ออาจารย์แหล่งฝึก :" & txtFirstName.Text & " " & txtLastName.Text, "")
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย.');", True)

    End Sub

    Private Sub ClearData()
        hdUID.Value = "0"
        txtFirstName.Text = ""
        txtLastName.Text = ""
        ddlPrefix.SelectedIndex = 0
        txtPositionName.Text = ""
        txtMail.Text = ""
        txtContactNumber.Text = ""
        txtLicenseNumber.Text = ""
        ddlWork.SelectedIndex = 0
        txtCPECredits.Text = ""
        chkTraining.Checked = False
        chkApprove.Checked = False
        chkApprove.Checked = True
        imgPhoto.ImageUrl = "images/unpic.jpg"
        hlnkCV.Visible = False
        isAdd = True
    End Sub

    Private Sub EditData(ByVal pID As String)
        'ClearData()
        Dim dtP As New DataTable
        dtP = ctlStd.Person_GetByID(pID)

        If dtP.Rows.Count > 0 Then
            With dtP.Rows(0)
                isAdd = False
                hdUID.Value = .Item("PersonID")

                If String.Concat(.Item("PrefixID")) <> "" Then
                    Me.ddlPrefix.SelectedValue = String.Concat(.Item("PrefixID"))
                End If

                txtFirstName.Text = String.Concat(.Item("FirstName"))
                txtLastName.Text = String.Concat(.Item("LastName"))
                txtPositionName.Text = String.Concat(.Item("PositionName"))
                txtMail.Text = String.Concat(.Item("Email"))
                txtContactNumber.Text = String.Concat(.Item("Telephone"))
                txtLicenseNumber.Text = String.Concat(.Item("LicenseNo"))
                txtCPECredits.Text = String.Concat(.Item("Credits"))
                ddlLocation.SelectedValue = StrNull2Zero(.Item("LocationID"))
                LoadLocationWork()

                If String.Concat(.Item("WorkId")) <> "" Then
                    ddlWork.SelectedValue = DBNull2Zero(.Item("WorkId"))
                End If

                chkTraining.Checked = ConvertYN2Boolean(String.Concat(.Item("IsTraining")))
                chkApprove.Checked = ConvertYN2Boolean(String.Concat(.Item("ConfirmStatus")))
                'chkMain.Checked = ConvertYN2Boolean(String.Concat(.Item("IsMain")))
                chkActive.Checked = ConvertStatusFlag2Boolean(String.Concat(.Item("StatusFlag")))

                txtCvLink.Text = String.Concat(.Item("CVPath"))

                If dtP.Columns.Contains("PicturePath") Then
                    Dim pic As String = String.Concat(.Item("PicturePath"))
                    If pic <> "" Then
                        imgPhoto.ImageUrl = PhotoFolder & pic
                    End If
                End If
                If dtP.Columns.Contains("CVPath") Then
                    Dim cv As String = String.Concat(.Item("CVPath"))
                    If cv <> "" Then
                        hlnkCV.NavigateUrl = cv
                        hlnkCV.Visible = True
                    End If
                End If
            End With

        End If

        dtP = Nothing
    End Sub
End Class