

Public Class EventModify
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlP As New PersonController
    'Dim ctlM As New MasterController
    Dim ctlE As New EventController

    Dim sContent As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            lblColor.Text = "#0073b7"
            lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#0073b7")

            LoadEvents()
            'If Not Request("pid") Is Nothing Then
            '    LoadPersonData(Request("pid"))
            'End If
        End If
    End Sub
    'Private Sub LoadPersonData(PersonUID As Integer)
    '    dt = ctlP.Person_GetByPersonID(PersonUID)
    '    If dt.Rows.Count > 0 Then
    '        With dt.Rows(0)
    '            hdUID.Value = String.Concat(.Item("PersonUID"))
    '            lblCode.Text = String.Concat(.Item("Code"))
    '            lblPersonName.Text = String.Concat(.Item("PersonName"))
    '            lblPosition.Text = String.Concat(.Item("PositionName"))
    '            lblDepartment.Text = String.Concat(.Item("DepartmentName"))
    '            lblSection.Text = String.Concat(.Item("SectionName"))
    '            LoadEvents(hdUID.Value)

    '        End With
    '    Else
    '    End If
    'End Sub

    Private Sub LoadEvents()
        dt = ctlE.Events_GetAll()
        grdCompany.DataSource = dt
        grdCompany.DataBind()
        Dim lblc As New Label

        For i = 0 To dt.Rows.Count - 1
            lblc = grdCompany.Rows(i).Cells(7).FindControl("Label1")
            lblc.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows(i)("Barcolor"))
            lblc.ForeColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows(i)("Barcolor"))
            lblc.Height = 20
            lblc.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        Next


    End Sub



    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim Bdate, Edate As String

        Bdate = txtStartDate.Text
        Edate = txtEndDate.Text

        If (Bdate = "" Or Bdate = "dd/mm/yyyy") Or (Edate = "" Or Edate = "dd/mm/yyyy") Then
            Bdate = ""
            Edate = ""
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','กรุณาระบุวันที่เริ่มและวันที่สิ้นสุด');", True)
            Exit Sub
        Else
            Bdate = txtStartDate.Text 'SetStrDate2DBDateFormat(Bdate)
            Edate = txtEndDate.Text
        End If
        sContent = xHtmlDetail.Html

        ctlE.Events_Save(StrNull2Zero(hdUID.Value), txtName.Text, Bdate, Edate, sContent, ConvertBoolean2StatusFlag(chkStatus.Checked), lblColor.Text)

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'ผลการตรวจสอบ','บันทึกข้อมูลเรียบร้อย');", True)
        LoadEvents()
    End Sub

    Protected Sub grdCompany_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdCompany.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If ctlE.Events_Delete(e.CommandArgument) Then

                        Dim acc As New UserController
                        acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_DEL, "Event", "ลบ Event :{UID=" & hdUID.Value & "}{cid=" & e.CommandArgument() & "}", "")

                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadEvents()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModals(this,'Success','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)

                    End If
            End Select
        End If
    End Sub
    Private Sub EditData(ByVal pID As String)
        dt = ctlE.Events_GetByUID(pID)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdUID.Value = .Item("UID")
                txtName.Text = String.Concat(dt.Rows(0)("Title"))
                txtStartDate.Text = String.Concat(dt.Rows(0)("Startdate"))
                txtEndDate.Text = String.Concat(dt.Rows(0)("Enddate"))
                xHtmlDetail.Html = String.Concat(.Item("Details"))
                chkStatus.Checked = ConvertStatusFlag2CHK(.Item("Statusflag"))
            End With
        End If
        dt = Nothing
    End Sub


    Private Sub grdCompany_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdCompany.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"

            Dim imgD As Image = e.Row.Cells(1).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#d9edf7';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub btnColor1_Click(sender As Object, e As EventArgs) Handles btnColor1.Click
        lblColor.Text = "#00c0ef"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#00c0ef")
    End Sub
    Protected Sub btnColor2_Click(sender As Object, e As EventArgs) Handles btnColor2.Click
        lblColor.Text = "#0073b7"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#0073b7")
    End Sub
    Protected Sub btnColor3_Click(sender As Object, e As EventArgs) Handles btnColor3.Click
        lblColor.Text = "#3c8dbc"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#3c8dbc")
    End Sub
    Protected Sub btnColor4_Click(sender As Object, e As EventArgs) Handles btnColor4.Click
        lblColor.Text = "#39cccc"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#39cccc")
    End Sub
    Protected Sub btnColor5_Click(sender As Object, e As EventArgs) Handles btnColor5.Click
        lblColor.Text = "#f39c12"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#f39c12")
    End Sub
    Protected Sub btnColor6_Click(sender As Object, e As EventArgs) Handles btnColor6.Click
        lblColor.Text = "#FF851B"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF851B")
    End Sub
    Protected Sub btnColor7_Click(sender As Object, e As EventArgs) Handles btnColor7.Click
        lblColor.Text = "#00a65a"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#00a65a")
    End Sub
    Protected Sub btnColor8_Click(sender As Object, e As EventArgs) Handles btnColor8.Click
        lblColor.Text = "#01FF70"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#01FF70")
    End Sub
    Protected Sub btnColor9_Click(sender As Object, e As EventArgs) Handles btnColor9.Click
        lblColor.Text = "#dd4b39"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#dd4b39")
    End Sub
    Protected Sub btnColor10_Click(sender As Object, e As EventArgs) Handles btnColor10.Click
        lblColor.Text = "#605ca8"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#605ca8")
    End Sub
    Protected Sub btnColor11_Click(sender As Object, e As EventArgs) Handles btnColor11.Click
        lblColor.Text = "#F012BE"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#F012BE")
    End Sub
    Protected Sub btnColor12_Click(sender As Object, e As EventArgs) Handles btnColor12.Click
        lblColor.Text = "#777777"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#777777")
    End Sub
    Protected Sub btnColor13_Click(sender As Object, e As EventArgs) Handles btnColor13.Click
        lblColor.Text = "#001F3F"
        lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#001F3F")
    End Sub
End Class