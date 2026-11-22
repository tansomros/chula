Public Class TimelineSetting
    Inherits System.Web.UI.Page
    Dim ctlCfg As New SystemConfigController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtEduYear.Text = ctlCfg.SystemConfig_GetByCode("EDUYEAR")
            txtYearAssm.Text = ctlCfg.SystemConfig_GetByCode("ASSMYEAR")
            txtReg_Start.Text = ctlCfg.SystemConfig_GetByCode("STARTDATE")
            txtReg_End.Text = ctlCfg.SystemConfig_GetByCode("ENDDATE")
            txtAnnouncedDate.Text = ctlCfg.SystemConfig_GetByCode("RESULTDATE")
            txtAssess_Start.Text = ctlCfg.SystemConfig_GetByCode("STARTASSM")
            txtAssess_End.Text = ctlCfg.SystemConfig_GetByCode("ENDASSM")

            txtAssessLoc_Start.Text = ctlCfg.SystemConfig_GetByCode("STARTASSMLOC")
            txtAssessLoc_End.Text = ctlCfg.SystemConfig_GetByCode("ENDASSMLOC")

        End If
        txtEduYear.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        txtYearAssm.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
    End Sub

    Protected Sub cmdSaveYear_Click(sender As Object, e As EventArgs) Handles cmdSaveYear.Click
        If txtEduYear.Text = "" Then
            DisplayMessage(Me.Page, "ท่านไม่ได้ระบุปีการศึกษา")
            Exit Sub
        End If

        ctlCfg.DataConfig_UpdateValue("EDUYEAR", txtEduYear.Text)
        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub

    Protected Sub cmdSaveSelect_Click(sender As Object, e As EventArgs) Handles cmdSaveSelect.Click
        If txtReg_Start.Text = "" Or txtReg_End.Text = "" Then
            DisplayMessage(Me.Page, "ท่านไม่ได้ระบุวันที่ให้ครบถ้วน")
            Exit Sub
        End If
        If isStringFormatDate(txtReg_Start.Text, "/") And isStringFormatDate(txtReg_End.Text, "/") Then
            If ConvertStringDateToDate(txtReg_Start.Text) > ConvertStringDateToDate(txtReg_End.Text) Then
                DisplayMessage(Me.Page, "ท่านระบุวันที่ไม่ถูกต้อง กรุณาตรวจสอบ")
                Exit Sub
            End If
        Else
            DisplayMessage(Me.Page, "ท่านระบุวันที่ไม่ถูกต้อง (รูปแบบวันที่ วว/ดด/ปปปป) กรุณาตรวจสอบ")
            Exit Sub
        End If


        ctlCfg.DataConfig_UpdateValue("STARTDATE", txtReg_Start.Text)
        ctlCfg.DataConfig_UpdateValue("ENDDATE", txtReg_End.Text)

        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub

    Protected Sub cmdSaveAnnounced_Click(sender As Object, e As EventArgs) Handles cmdSaveAnnounced.Click
        If txtAnnouncedDate.Text = "" Then
            DisplayMessage(Me.Page, "ท่านไม่ได้ระบุวันที่ให้ครบถ้วน")
            Exit Sub
        End If
        If Not isStringFormatDate(txtAnnouncedDate.Text, "/") Then
            DisplayMessage(Me.Page, "ท่านระบุวันที่ไม่ถูกต้อง (รูปแบบวันที่ วว/ดด/ปปปป) กรุณาตรวจสอบ")
            Exit Sub
        End If


        ctlCfg.DataConfig_UpdateValue("RESULTDATE", txtAnnouncedDate.Text)
        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub

    Protected Sub cmdSaveAssessment_Click(sender As Object, e As EventArgs) Handles cmdSaveAssessment.Click
        If txtAssess_Start.Text = "" Or txtAssess_End.Text = "" Then
            DisplayMessage(Me.Page, "ท่านไม่ได้ระบุวันที่ให้ครบถ้วน")
            Exit Sub
        End If
        If isStringFormatDate(txtAssess_Start.Text, "/") And isStringFormatDate(txtAssess_End.Text, "/") Then
            If ConvertStringDateToDate(txtAssess_Start.Text) > ConvertStringDateToDate(txtAssess_End.Text) Then
                DisplayMessage(Me.Page, "ท่านระบุวันที่ไม่ถูกต้อง กรุณาตรวจสอบ")
                Exit Sub
            End If
        Else
            DisplayMessage(Me.Page, "ท่านระบุวันที่ไม่ถูกต้อง (รูปแบบวันที่ วว/ดด/ปปปป) กรุณาตรวจสอบ")
            Exit Sub
        End If

        ctlCfg.DataConfig_UpdateValue("STARTASSM", txtAssess_Start.Text)
        ctlCfg.DataConfig_UpdateValue("ENDASSM", txtAssess_End.Text)
        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub

    Protected Sub cmdSaveLocAssessment_Click(sender As Object, e As EventArgs) Handles cmdSaveLocAssessment.Click
        If txtAssessLoc_Start.Text = "" Or txtAssessLoc_End.Text = "" Then
            DisplayMessage(Me.Page, "ท่านไม่ได้ระบุวันที่ให้ครบถ้วน")
            Exit Sub
        End If
        If isStringFormatDate(txtAssessLoc_Start.Text, "/") And isStringFormatDate(txtAssessLoc_End.Text, "/") Then
            If ConvertStringDateToDate(txtAssessLoc_Start.Text) > ConvertStringDateToDate(txtAssessLoc_End.Text) Then
                DisplayMessage(Me.Page, "ท่านระบุวันที่ไม่ถูกต้อง กรุณาตรวจสอบ")
                Exit Sub
            End If
        Else
            DisplayMessage(Me.Page, "ท่านระบุวันที่ไม่ถูกต้อง (รูปแบบวันที่ วว/ดด/ปปปป) กรุณาตรวจสอบ")
            Exit Sub
        End If

        ctlCfg.DataConfig_UpdateValue("STARTASSMLOC", txtAssessLoc_Start.Text)
        ctlCfg.DataConfig_UpdateValue("ENDASSMLOC", txtAssessLoc_End.Text)
        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub

    Protected Sub cmdSaveYearAssm_Click(sender As Object, e As EventArgs) Handles cmdSaveYearAssm.Click
        If txtYearAssm.Text = "" Then
            DisplayMessage(Me.Page, "ท่านไม่ได้ระบุปีการศึกษาสำหรับการประเมิน")
            Exit Sub
        End If

        ctlCfg.DataConfig_UpdateValue("ASSMYEAR", txtYearAssm.Text)
        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub


End Class