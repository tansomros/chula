Imports System.Web.UI
Imports System
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Public Class Calendar
    Inherits System.Web.UI.Page

    Dim dtE As New DataTable
    Dim ctlEv As New EventController

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            lblEventHeader.Text = "Upcoming Event"

            ASPxCalendar1.HighlightToday = True
            ASPxCalendar1.HighlightWeekends = True
            ASPxCalendar1.ShowClearButton = True
            ASPxCalendar1.ShowTodayButton = True
            ASPxCalendar1.ShowDayHeaders = True
            ASPxCalendar1.ShowHeader = True
            ASPxCalendar1.ShowWeekNumbers = True

            ASPxCalendar1.Width = 200

            If Not Request("d") Is Nothing Then
                LoadEvent(Request("d"), "")
            ElseIf Not Request("cat") Is Nothing Then
                LoadEvent("", Request("cat"))
            Else
                LoadEvent("", "")
            End If

        End If

    End Sub
    Private Sub LoadEvent(pDate As String, CatUID As String)
        If CatUID <> "" Then
            dtE = ctlEv.Events_GetByCategoryUID(CatUID)
        ElseIf pDate <> "" Then
            dtE = ctlEv.Events_GetByDate(pDate)
        Else
            dtE = ctlEv.Events_GetAll
        End If

        grdData.DataSource = dtE
        grdData.DataBind()
    End Sub

    Private Sub ASPxCalendar1_ValueChanged(sender As Object, e As EventArgs) Handles ASPxCalendar1.ValueChanged
        lblAlbumTitle.Text = ASPxCalendar1.SelectedDate
        lblEventHeader.Text = "Event List"
        LoadEvent(ConvertFormateDate(ASPxCalendar1.SelectedDate), "")
    End Sub
End Class