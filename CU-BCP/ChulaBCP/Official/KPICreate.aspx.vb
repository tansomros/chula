Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class KPICreate
    Inherits System.Web.UI.Page
    Dim Conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("strConn").ToString)
    Dim DA As New SqlDataAdapter
    Dim Com As New SqlCommand
    Dim dr As SqlDataReader

    Dim sql As String = ""
    Dim sb As New StringBuilder("")
    Dim _username As String = "Admin"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("userid") Is Nothing Then
            Response.Redirect("../login.aspx")
        End If

        If (Session("RoleID") >= 2) Then
            Response.Redirect("CustomError.aspx")
        End If

        If Session("uname") <> "" Then
            _username = Session("uname")
        End If
        lblErr.Text = ""
    End Sub


    Protected Sub bttNew_Click(sender As Object, e As System.EventArgs) Handles bttNew.Click
        lblErr.Text = ""
        Try
            sb.Remove(0, sb.Length)
            sb.Append("INSERT INTO qa_year_code(year_code, year_name) ")
            sb.Append(" VALUES (@year_code, @year_name)")
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
                .Parameters.Add("@year_code", SqlDbType.Int).Value = TextBox1.Text
                .Parameters.Add("@year_name", SqlDbType.NVarChar).Value = TextBox2.Text
                .ExecuteNonQuery()
            End With

            sql = "Select kpi_id, kpi_year, kpi_no  FROM dbo.KPISet  WHERE (kpi_year = N '2558')  ORDER BY kpi_id"
            DA = New SqlDataAdapter(sql, Conn)
            Dim dt As New DataTable
            DA.Fill(dt)
            If dt.Rows.Count > 0 Then
                For i As Int16 = 0 To dt.Rows.Count - 1
                    sql = "INSERT INTO KPISet (kpi_id, kpi_year, kpi_no)"
                    sql &= "  VALUES (@kpi_id, @kpi_year, @kpi_no)"

                    With Com
                        .CommandType = CommandType.Text
                        .CommandText = sql
                        .Connection = Conn
                        .Parameters.Clear()
                        .Parameters.Add("@kpi_id", SqlDbType.Int).Value = CInt(dt.Rows(i).Item("kpi_id"))
                        .Parameters.Add("@kpi_year", SqlDbType.NVarChar).Value = TextBox1.Text
                        .Parameters.Add("@kpi_no", SqlDbType.NVarChar).Value = (dt.Rows(i).Item("kpi_no"))
                        .ExecuteNonQuery()
                    End With

                Next
            End If

            Conn.Close()
            WriteLogFile(_username, "สร้าง ชุดข้อมูลพื้นฐาน kpi ปี " & TextBox1.Text)

            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
        Catch ex As Exception
            lblErr.Text = "พบข้อผิดพลาดจากการดำเนินงาน เนื่องจาก " & ex.Message
        End Try

    End Sub

    Protected Sub DeleteItem(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
        sql = "delete from qa_year_code where (year_code=@year_code)"
        With Conn
            If .State = ConnectionState.Open Then .Close()
            Conn.Open()
        End With

        With Com
            .CommandType = CommandType.Text
            .CommandText = sql
            .Connection = Conn
            .Parameters.Clear()
            .Parameters.AddWithValue("@year_code", lnkRemove.CommandArgument)
            .ExecuteNonQuery()
        End With

        sql = "delete from KPISet where (kpi_year=@kpi_year)"
        With Com
            .CommandType = CommandType.Text
            .CommandText = sql
            .Connection = Conn
            .Parameters.Clear()
            .Parameters.AddWithValue("@kpi_year", lnkRemove.CommandArgument)
            .ExecuteNonQuery()
        End With
        Conn.Close()

        WriteLogFile(_username, "ลบ ข้อมูล kpi ปี " & lnkRemove.CommandArgument)

        Response.Redirect(Request.Url.AbsoluteUri)

    End Sub

    Private Sub WriteLogFile(_username As String, _action As String)
        sb.Remove(0, sb.Length)
        sb.Append("EXEC proc_log_write @log_user, @log_action ")
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
            .Parameters.Add("@log_user", SqlDbType.NVarChar).Value = _username
            .Parameters.Add("@log_action", SqlDbType.NVarChar).Value = _action
            .ExecuteNonQuery()
        End With
    End Sub


End Class