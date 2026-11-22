Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO
Public Class SlideManage
    Inherits System.Web.UI.Page
    Dim Conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("strConn").ToString)
    Dim DA As New SqlDataAdapter
    Dim Com As New SqlCommand
    Dim dr As SqlDataReader

    Dim sql As String = ""
    Dim sb As New StringBuilder("")

    Dim IsFlag As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("RoleID") <> "0" Then
            Response.Redirect("CustomError.aspx")
        End If

        GetItem()
    End Sub

    Private Sub GetItem()
        sql = "SELECT id, img_path, title_th, url, MWhen, IaActive_img,IsActive  FROM  SlideShow ORDER BY IsActive DESC, id DESC"
        With Conn
            If .State = ConnectionState.Open Then .Close()
            .Open()
        End With
        DA = New SqlDataAdapter(sql, Conn)
        Dim dt As New DataTable
        DA.Fill(dt)
        If dt.Rows.Count > 0 Then
            grdSlide.DataSource = dt
            grdSlide.DataBind()
        End If
        Conn.Close()
    End Sub

    Protected Sub DeleteVN(ByVal sender As Object, ByVal e As EventArgs)

        Dim lnkDel As LinkButton = DirectCast(sender, LinkButton)
        sb.Remove(0, sb.Length)
        sb.Append("UPDATE SlideShow SET IsActive=0, IaActive_img='Images/icon/downarrow.png' WHERE (id=@id)")
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
            .Parameters.Add("@id", SqlDbType.NVarChar).Value = lnkDel.CommandArgument
            .ExecuteNonQuery()
        End With
        Conn.Close()
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

    End Sub
    Protected Sub IsActiveItem(ByVal sender As Object, ByVal e As EventArgs)

        Dim lnkOk As LinkButton = DirectCast(sender, LinkButton)
        sb.Remove(0, sb.Length)
        sb.Append("UPDATE SlideShow SET IsActive=1,IaActive_img='Images/icon/ok.png'   WHERE (id=@id)")
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
            .Parameters.Add("@id", SqlDbType.NVarChar).Value = lnkOk.CommandArgument
            .ExecuteNonQuery()
        End With
        Conn.Close()
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

    End Sub

    Protected Sub DeleteVN_a(ByVal sender As Object, ByVal e As EventArgs)

        Dim lnkDel0 As LinkButton = DirectCast(sender, LinkButton)
        sb.Remove(0, sb.Length)
        sb.Append("DELETE FROM SlideShow WHERE (id=@id)")
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
            .Parameters.Add("@id", SqlDbType.NVarChar).Value = lnkDel0.CommandArgument
            .ExecuteNonQuery()
        End With
        Conn.Close()
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

    End Sub

    Protected Sub bttSave_Click(sender As Object, e As System.EventArgs) Handles bttSave.Click
        Dim UlFileName As String = Nothing
        UlFileName = "imageSlide/" + FileUpload1.FileName
        Me.FileUpload1.SaveAs(Server.MapPath(UlFileName))
        sb.Remove(0, sb.Length)
        sb.Append("exec AddNewImgIndex  @img_path,@title_th,@title_en,@desc_th,@desc_en,@IsActive, @IaActive_img, @url")
        sql = sb.ToString()
        Conn.Open()
        Com = New SqlCommand(sql, Conn)
        Com.Parameters.Clear()
        Com.Parameters.Add("@img_path", SqlDbType.NVarChar).Value = UlFileName.ToString()
        Com.Parameters.Add("@title_th", SqlDbType.NVarChar).Value = txtName.Text
        Com.Parameters.Add("@title_en", SqlDbType.NVarChar).Value = ""
        Com.Parameters.Add("@desc_th", SqlDbType.NVarChar).Value = ""
        Com.Parameters.Add("@desc_en", SqlDbType.NVarChar).Value = ""
        Com.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = 1
        Com.Parameters.Add("@IaActive_img", SqlDbType.NVarChar).Value = "Images/icon/ok.png"
        If txtName0.Text.Trim = "" Then
            Com.Parameters.Add("@url", SqlDbType.NVarChar).Value = UlFileName.ToString()
        Else
            Com.Parameters.Add("@url", SqlDbType.NVarChar).Value = txtName0.Text
        End If

        Com.ExecuteNonQuery()
        Conn.Close()

        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub


End Class