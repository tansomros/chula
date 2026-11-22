Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Public Class LinksUpdate
    Inherits System.Web.UI.Page
    Dim Conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("strConn").ToString)
    Dim DA As New SqlDataAdapter
    Dim Com As New SqlCommand
    Dim dr As SqlDataReader

    Dim sql As String = ""
    Dim sb As New StringBuilder("")

    Protected Sub DeleteItem(ByVal sender As Object, ByVal e As EventArgs)

        Dim lnkDel0 As LinkButton = DirectCast(sender, LinkButton)
        sb.Remove(0, sb.Length)
        sb.Append("DELETE FROM Links WHERE (lnkID=@lnkID)")
        sql = sb.ToString

        With Conn
            If .State = ConnectionState.Open Then .Close()
            Conn.Open()
        End With

        With Com
            .CommandType = CommandType.Text
            .CommandText = sql
            .Connection = Conn
            .Parameters.Clear()
            .Parameters.Add("@lnkID", SqlDbType.NVarChar).Value = lnkDel0.CommandArgument
            .ExecuteNonQuery()
        End With
        Conn.Close()
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("RoleID") <> "0" Then
            Response.Redirect("CustomError.aspx")
        End If
    End Sub

    Protected Sub bttSave_Click(sender As Object, e As System.EventArgs) Handles bttSave.Click
        sb.Remove(0, sb.Length)
        sb.Append("SELECT TOP (1) lnkID  FROM dbo.Links  ORDER BY lnkID DESC")
        sql = sb.ToString
        With Conn
            If .State = ConnectionState.Open Then .Close()
            Conn.Open()
        End With
        Dim _linkId As Integer = 0
        DA = New SqlDataAdapter(sql, Conn)
        Dim dt As New DataTable
        DA.Fill(dt)
        If dt.Rows.Count > 0 Then
            _linkId = CInt(dt.Rows(0).Item("lnkID")) + 1
        Else
            _linkId = 1
        End If

        sb.Remove(0, sb.Length)
        sb.Append("SELECT TOP (1) Orders  FROM dbo.Links  ORDER BY Orders DESC")
        sql = sb.ToString
        With Conn
            If .State = ConnectionState.Open Then .Close()
            Conn.Open()
        End With
        Dim _orders As Integer = 0
        DA = New SqlDataAdapter(sql, Conn)
        Dim dt1 As New DataTable
        DA.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            _orders = CInt(dt1.Rows(0).Item("Orders")) + 1
        Else
            _orders = 1
        End If

        Dim UlFileName As String = Nothing
        UlFileName = "Images/links/" + FileUpload1.FileName
        Me.FileUpload1.SaveAs(Server.MapPath(UlFileName))

        sb.Remove(0, sb.Length)
        sb.Append("INSERT INTO Links(lnkID, lnkName,url, ImagePath,MWhen, Orders)")
        sb.Append(" VALUES (@lnkID, @lnkName, @url, @ImagePath, @MWhen, @Orders)")
        sql = sb.ToString

        With Com
            .CommandType = CommandType.Text
            .CommandText = sql
            .Connection = Conn
            .Parameters.Clear()
            .Parameters.Add("@lnkID", SqlDbType.Int).Value = _linkId
            .Parameters.Add("@lnkName", SqlDbType.NVarChar).Value = txtName.Text
            .Parameters.Add("@url", SqlDbType.NVarChar).Value = txtUrl.Text
            .Parameters.Add("@ImagePath", SqlDbType.NVarChar).Value = UlFileName
            .Parameters.Add("@MWhen", SqlDbType.DateTime).Value = Now
            .Parameters.Add("@Orders", SqlDbType.Int).Value = _orders
            .ExecuteNonQuery()
        End With
        Conn.Close()
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Protected Sub dgvList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles dgvList.PageIndexChanging

    End Sub
End Class