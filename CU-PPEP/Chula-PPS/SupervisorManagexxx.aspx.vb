
Public Class SupervisorManagexxx
    Inherits System.Web.UI.Page

    Dim ctlSubj As New SubjectController
    Dim dt As New DataTable
    Dim ctlCs As New Coursecontroller
    Dim acc As New UserController
    Dim ctlSpv As New SupervisorController
    Dim ctlPsn As New PersonController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            grdTeacher.PageIndex = 0
            'LoadYearToDDL()

            Dim ctlCfg As New SystemConfigController
            lblYear.Text = ctlCfg.SystemConfig_GetByCode(CFG_EDUYEAR)
            'txtDate.Text = Today.Date()

            'LoadYearToDDL()
            LoadCourseToDDL()
            LoadProvinceToDDL()
            LoadLocation()


            LoadTeacher()
            If Not Request("y") Is Nothing Then
                lblYear.Text = Request("y")
                LoadCourseToDDL()
                ddlCourse.SelectedValue = Request("id")
            End If

        End If

        lblSubjectName.Text = lblYear.Text
    End Sub

    Private Sub LoadCourseToDDL()
        ddlCourse.Items.Clear()

        dt = ctlCs.Courses_GetByYear(lblYear.Text)

        If dt.Rows.Count > 0 Then
            ddlCourse.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With ddlCourse
                    .Items.Add("" & dt.Rows(i)("SubjectCode") & " : " & dt.Rows(i)("NameTH"))
                    .Items(i).Value = dt.Rows(i)("CourseID")
                End With
            Next

        End If

    End Sub
    Private Sub LoadProvinceToDDL()
        dt = ctlCs.LoadProvince
        If dt.Rows.Count > 0 Then
            ddlProvince.Items.Add("---ทั้งหมด---")
            ddlProvince.Items(0).Value = "0"

            For i = 1 To dt.Rows.Count
                With ddlProvince
                    .Items.Add(dt.Rows(i - 1)("ProvinceName"))
                    .Items(i).Value = dt.Rows(i - 1)("ProvinceID")

                End With
            Next
            ddlProvince.SelectedIndex = 0
        End If
        dt = Nothing
    End Sub

    Dim ctlS As New SupervisionController

    Private Sub LoadLocation()

        Dim dtL As New DataTable

        dtL = ctlS.SupervisionLocation_Get4Supervisor(StrNull2Zero(lblYear.Text), StrNull2Zero(ddlCourse.SelectedValue), ddlProvince.SelectedValue, txtLocation.Text)

        If dtL.Rows.Count > 0 Then

            With grdLocation
                .DataSource = dtL
                .DataBind()
            End With


        End If

    End Sub


    Private Sub LoadTeacher()

        If Trim(txtSearch.Text) <> "" Then
            dt = ctlPsn.GetPerson_SearchByType("T", txtSearch.Text)
        Else
            dt = ctlPsn.GetPerson_ByType("T")
        End If


        If dt.Rows.Count > 0 Then
            With grdTeacher
                .Visible = True
                .DataSource = dt
                .DataBind()

            End With
        Else
            grdTeacher.Visible = False
        End If
    End Sub

    Private Sub grdLocation_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdLocation.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    lblYear.Text = grdLocation.Rows(e.CommandArgument()).Cells(1).Text
                    ddlCourse.SelectedValue = grdLocation.DataKeys(e.CommandArgument()).Value
                    ' LoadSupervisorToGrid()
            End Select

        End If
    End Sub
    Private Sub grdLocation_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLocation.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub
    Private Sub AddTeacherToCourse(pID As Integer, Lid As Integer, phaseNO As Integer)

        Dim item As Integer

        'item = ctlSpv.Supervisor_Add(lblyear.text, pID, Lid, StrNull2Zero(ddlSubject.SelectedValue), phaseNO, Request.Cookies("Username").Value)
        'acc.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "CourseCoordinator", "เพิ่ม อ.นิเทศ:" & lblyear.text & ">>" & ddlSubject.SelectedValue & ">>PersonID:" & pID, "")
        'LoadSupervisorToGrid()
        DisplayMessage(Me, "เพิ่มอาจารย์เรียบร้อย")
    End Sub

    Private Sub grdCoor_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSupervisor.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As Image = e.Row.Cells(2).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub

    Protected Sub ddlSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        grdLocation.PageIndex = 0
        LoadLocation()
    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdTeacher.PageIndexChanging
        grdTeacher.PageIndex = e.NewPageIndex
        LoadTeacher()
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdTeacher.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgSelect"
                    '  AddTeacherToCourse(e.CommandArgument())
            End Select


        End If
    End Sub

    Private Sub grdData_RowDataBound1(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdTeacher.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#ffdfef';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdFind.Click
        grdTeacher.PageIndex = 0
        LoadTeacher()
    End Sub


    Protected Sub cmdFindLocation_Click(sender As Object, e As ImageClickEventArgs) Handles cmdFindLocation.Click
        grdLocation.PageIndex = 0
        LoadLocation()
    End Sub

    Protected Sub ddlProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvince.SelectedIndexChanged
        LoadLocation()

    End Sub
End Class

