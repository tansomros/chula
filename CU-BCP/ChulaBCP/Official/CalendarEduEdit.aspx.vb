Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class CalendarEduEdit
    Inherits System.Web.UI.Page
    Dim Conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("strConn").ToString)
    Dim DA As New SqlDataAdapter
    Dim Com As New SqlCommand
    Dim dr As SqlDataReader

    Dim sql As String = ""
    Dim sb As New StringBuilder("")
    Dim url As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("RoleID") <> "0" Then
            Response.Redirect("CustomError.aspx")
        End If
        Page.Title = "ปฏิทินการศึกษา"
        If Not Page.IsPostBack Then
            GetData()
        End If

    End Sub

    Private Sub GetData()
        sql = "SELECT * FROM CalendarEdu WHERE (UID=1)"
        DA = New SqlDataAdapter(sql, Conn)
        With Conn
            If .State = ConnectionState.Open Then .Close()
            .Open()
        End With

        Dim dt As New DataTable
        DA.Fill(dt)
        If dt.Rows.Count > 0 Then
            ASPxHtmlEditor1.Html = Convert.ToString(dt.Rows(0).Item("CalendarDesc"))

        End If
        Conn.Close()
    End Sub

    Protected Sub bttNew_Click(sender As Object, e As System.EventArgs) Handles bttNew.Click
        sb.Remove(0, sb.Length)
        sb.Append("UPDATE CalendarEdu SET CalendarDesc=@CalendarDesc, MWhen=@MWhen WHERE (UID=1)")
        sql = sb.ToString

        With Conn
            If .State = ConnectionState.Open Then .Close()
            .Open()
        End With

        With Com
            .CommandType = CommandType.Text
            .CommandText = sql
            .Connection = Conn
            .Parameters.Clear()
            .Parameters.Add("@CalendarDesc", SqlDbType.NVarChar).Value = ASPxHtmlEditor1.Html
            .Parameters.Add("@MWhen", SqlDbType.DateTime).Value = Now
            .ExecuteNonQuery()
        End With
        Conn.Close()

        DisplayMessage(Me.Page, "บันทึกเรียบร้อย")
    End Sub

End Class