
Public Class ReportREQByLocation
    Inherits System.Web.UI.Page
    Dim dt As New DataTable 
    Dim ctlCs As New Coursecontroller
    Dim ctlLtg As New LocationGroupController


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("Username").Value Is Nothing Then
            Response.Redirect("Default.aspx?logout=YES")
        End If
        If Not IsPostBack Then
            lblResult.Visible = False
            LoadYearToDDL()
            LoadGroupToDDL()

            'Thread.Sleep(500)
        End If
    End Sub
    Private Sub LoadGroupToDDL()
        dt = ctlLtg.LocationGroup_Get
        If dt.Rows.Count > 0 Then
            ddlType.Items.Clear()
            ddlType.Items.Add("---ทั้งหมด---")
            ddlType.Items(0).Value = 0
            For i = 1 To dt.Rows.Count
                With ddlType
                    .Items.Add("" & dt.Rows(i - 1)("Name"))
                    .Items(i).Value = dt.Rows(i - 1)("Code")
                End With
            Next

        End If
        dt = Nothing
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
    Private Sub GenDataTable()
        Dim dtReq As New DataTable
        Dim dtAcc As New DataTable

        Dim ctlREQ As New REQcontroller
        Dim sRemark As String = ""

        ctlREQ.tm_StudentRegister_Summary_Delete()
        dtReq = ctlREQ.Requirements_GetLocation(ddlYear.SelectedValue, ddlType.SelectedValue)
        If dtReq.Rows.Count > 0 Then
            For i = 0 To dtReq.Rows.Count - 1
                With dtReq.Rows(i)
                    If .Item("isSameGender") = "Y" Then
                        sRemark = "เพศเดียวกัน"
                    Else
                        sRemark = ""
                    End If
                    ctlREQ.Tmp4ReportLocation_Add(.Item("REQYEAR"), .Item("LocationID"), .Item("LocationName"), .Item("LocationGroupID"), sRemark)

                    dtAcc = ctlREQ.Requirements_Get4Report(.Item("REQYEAR"), .Item("LocationID"))
                End With

                For n = 0 To dtAcc.Rows.Count - 1
                    With dtAcc.Rows(n)
                        ctlREQ.Tmp4ReportLocation_Update(dtReq.Rows(i).Item("REQYEAR"), .Item("TimePhaseID"), dtReq.Rows(i).Item("LocationID"), .Item("LocationGroupName"), .Item("TimeDesc"), .Item("Man"), .Item("Women"), .Item("NoSpec"))
                    End With
                Next
            Next
        End If

    End Sub

    Protected Sub cmdView_Click(sender As Object, e As EventArgs) Handles cmdView.Click

        Select Case Request("m")
            Case "docstdlist"
                ReportName = "docStudentList"
                FagRPT = "DOCSTDLIST"
                Reportskey = "PDF"
                Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&lgrp=" & ddlType.SelectedValue)
            Case "etm"
                FagRPT = ""
                FagRPT = "REQESTIMATE"
                Reportskey = "XLS"
                ReportName = "RequirementEstimate"
                Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&lgrp=" & ddlType.SelectedValue)
            Case Else
                FagRPT = ""
                FagRPT = "REQACCOUNT"
                Reportskey = "XLS"
                ReportName = "RequirementAccount"
                Response.Redirect("ReportServerViewer.aspx?y=" & ddlYear.SelectedValue & "&lgrp=" & ddlType.SelectedValue)
        End Select


    End Sub
End Class

