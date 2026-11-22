
Public Class Grade
    Inherits System.Web.UI.Page

    Dim ctlLG As New AssessmentController
    Dim ctlCs As New Coursecontroller
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            ClearData()
            LoadYearToDDL()
            LoadSubjectToDDL()
            grdData.PageIndex = 0
            LoadGradeToGrid()
        End If

        txtMin.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
        txtMax.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
    End Sub
    Private Sub LoadYearToDDL()
        Dim y As Integer = StrNull2Zero(DisplayYear(ctlCs.GET_DATE_SERVER))
        Dim LastRow As Integer
        dt = ctlCs.Courses_GetYear
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

            ddlYearSearch.DataSource = dt
            ddlYearSearch.DataTextField = "YearText"
            ddlYearSearch.DataValueField = "YearValue"
            ddlYearSearch.DataBind()
            ddlYearSearch.SelectedIndex = 0

        Else
            ddlYear.Items.Add(y)
            ddlYear.Items(0).Value = y
            ddlYear.Items.Add(y + 1)
            ddlYear.Items(1).Value = y + 1
            ddlYear.SelectedIndex = 0
        End If
        ddlYear.SelectedValue = Request.Cookies("ASSMYEAR").Value
        ddlYearSearch.SelectedValue = Request.Cookies("ASSMYEAR").Value
        dt = Nothing
    End Sub
    Private Sub LoadSubjectToDDL()
        Dim ctlSubj As New CourseController
        Dim dtSj As New DataTable
        If Request.Cookies("ROLE_SPA").Value = True Or Request.Cookies("ROLE_ADM").Value = True Then
            dtSj = ctlSubj.Courses_Get4Selection(ddlYear.SelectedValue)
        Else
            dtSj = ctlSubj.Courses_GetByAdv4Selection(ddlYear.SelectedValue, Request.Cookies("ProfileID").Value)
        End If

        ddlSubject.Items.Clear()
        If dtSj.Rows.Count > 0 Then
            With ddlSubject
                .DataSource = dtSj
                .DataTextField = "NameTH"
                .DataValueField = "SubjectCode"
                .DataBind()
            End With
        Else
            ddlSubject.Items.Clear()
            dtSj = Nothing
        End If
    End Sub

    Private Sub LoadGradeToGrid()
        If Request.Cookies("ROLE_SPA").Value = True Or Request.Cookies("ROLE_ADM").Value = True Then
            dt = ctlLG.Grade_Get(StrNull2Zero(ddlYearSearch.SelectedValue), txtSearch.Text)
        Else
            dt = ctlLG.Grade_GetByAdv(StrNull2Zero(ddlYearSearch.SelectedValue), Request.Cookies("ProfileID").Value, txtSearch.Text)
        End If

        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With
        dt = Nothing
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                Case "imgDel"
                    If ctlLG.Grade_Delete(e.CommandArgument) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        grdData.PageIndex = 0
                        LoadGradeToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'Warning!!','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If


            End Select


        End If
    End Sub

    Private Sub EditData(ByVal pID As Integer)
        dt = ctlLG.Grade_GetByID(pID)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                ddlYear.SelectedValue = DBNull2Str(.Item("EduYear"))
                ddlSubject.SelectedValue = DBNull2Str(.Item("SubjectCode"))
                txtGrade.Text = DBNull2Str(.Item("Grade"))
                txtMin.Text = DBNull2Str(.Item("MinScore"))
                txtMax.Text = DBNull2Str(.Item("MaxScore"))
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub ClearData()
        ddlYear.SelectedIndex = 0
        txtGrade.Text = ""
        txtMin.Text = ""
        txtMax.Text = ""
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(6).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtGrade.Text = "" Or txtMax.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If

        Dim item As Integer
        item = ctlLG.Grade_Save(StrNull2Zero(ddlYear.SelectedValue), ddlSubject.SelectedValue, txtGrade.Text, StrNull2Double(txtMin.Text), StrNull2Double(txtMax.Text))
        grdData.PageIndex = 0
        LoadGradeToGrid()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)


    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        grdData.PageIndex = 0
        LoadGradeToGrid()
    End Sub

    Protected Sub ddlYearSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYearSearch.SelectedIndexChanged
        grdData.PageIndex = 0
        LoadGradeToGrid()
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadSubjectToDDL()
    End Sub

    Protected Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadGradeToGrid()
    End Sub

End Class

