Imports System.Web.UI
Imports System
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Public Class EventDetail
    Inherits System.Web.UI.Page

    Dim dtE As New DataTable
    Dim ctlEv As New EventController

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            'ASPxCalendar1.HighlightToday = True
            'ASPxCalendar1.HighlightWeekends = True
            'ASPxCalendar1.ShowClearButton = True
            'ASPxCalendar1.ShowTodayButton = True
            'ASPxCalendar1.ShowDayHeaders = True
            'ASPxCalendar1.ShowHeader = True
            'ASPxCalendar1.ShowWeekNumbers = True

            'ASPxCalendar1.Width = 200
            LoadEventCategory()
            LoadEventInfo(Request("uid"))
        End If

    End Sub
    Private Sub LoadEventCategory()
        dtE = ctlEv.EventCategory_GetAll
        grdCategory.DataSource = dtE
        grdCategory.DataBind()
    End Sub
    Private Sub LoadEventInfo(ByVal pcode As Integer)
        dtE = ctlEv.Events_GetByID(pcode)
        If dtE.Rows.Count > 0 Then
            With dtE.Rows(0)
                isAdd = False

                'Me.lblUID.Text = DBNull2Str(dt.Rows(0)("UID"))

                'ddlCategory.SelectedValue = .Item("EventCategoryUID")

                'txtStartDate.Text = .Item("StartDate")
                'txtEndDate.Text = .Item("EndDate")

                'ddlBHour.SelectedValue = Left(DBNull2Str(.Item("StartTime")), 2)
                'ddlBMinus.SelectedValue = Right(DBNull2Str(.Item("StartTime")), 2)
                'ddlEHour.SelectedValue = Left(DBNull2Str(.Item("EndTime")), 2)
                'ddlEMinus.SelectedValue = Right(DBNull2Str(.Item("EndTime")), 2)

                lblTime.Text = .Item("EventDate") & " " & Left(DBNull2Str(.Item("StartTime")), 2) & ":" & Right(DBNull2Str(.Item("StartTime")), 2) & " - " & Left(DBNull2Str(.Item("EndTime")), 2) & ":" & Right(DBNull2Str(.Item("EndTime")), 2)

                lblEventName.Text = DBNull2Str(.Item("EventName"))
                lblVenue.Text = DBNull2Str(.Item("Venue"))
                lblOrganizer.Text = DBNull2Str(.Item("Organizer"))
                lblCategoryName.Text = DBNull2Str(.Item("CategoryNameTH"))
                lblDetail.Text = DBNull2Str(.Item("EventDetail"))
            End With
        End If
        dtE = Nothing
    End Sub


    'Private Sub ASPxCalendar1_ValueChanged(sender As Object, e As EventArgs) Handles ASPxCalendar1.ValueChanged
    '    lblAlbumTitle.Text = ASPxCalendar1.SelectedDate
    '    lblEventHeader.Text = "Event List"
    '    LoadEvent(ConvertFormateDate(ASPxCalendar1.SelectedDate))
    'End Sub

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        Response.Redirect("Calendar.aspx?d=" & ConvertFormateDate(Calendar1.SelectedDate))
    End Sub
End Class