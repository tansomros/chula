Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO

Public Class NewsPrivateAdd
    Inherits System.Web.UI.Page
    Dim Conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("strConn").ToString)
    Dim DA As New SqlDataAdapter
    Dim Com As New SqlCommand
    Dim dr As SqlDataReader

    Dim sql As String = ""
    Dim sb As New StringBuilder("")

    Dim _from As String = ""

    Dim dtNews As New DataTable
    Dim ctlN As New NewsController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("RoleID") <> "0" Then
            Response.Redirect("CustomError.aspx")
        End If
        If Session("userid") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If

        If Not Page.IsPostBack Then
            If Not Request("cate") Is Nothing Then
                ddlCategory.SelectedValue = Request("cate")
            End If

            With Conn
                If .State = ConnectionState.Open Then .Close()
                .Open()
            End With

            If Not Request("NewsID") Is Nothing Then
                EditNews(Request("NewsID"))
            Else
                sql = "SELECT     TOP (1) NewsID  FROM NewsInfoPrivate  ORDER BY NewsID DESC"
                Com = New SqlCommand(sql, Conn)
                dr = Com.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    lblNewsID.Text = CInt(dr.Item("NewsID")) + 1
                Else
                    lblNewsID.Text = 1
                End If
                dr.Close()
            End If

            Conn.Close()

            Dim folder As String = Server.MapPath("~/Images/NewsPrivate/" + lblNewsID.Text)
            If Not Directory.Exists(folder) Then
                Directory.CreateDirectory(folder)
            End If
            ASPxFileManager1.Settings.RootFolder = folder
        End If
    End Sub

    Protected Sub bttSave_Click(sender As Object, e As System.EventArgs) Handles bttSave.Click
        Dim UlFileName As String = ""
        If FileUpload1.HasFile Then
            UlFileName = FileUpload1.FileName
            Dim filename1 As String = Path.GetFileName(UlFileName)
            FileUpload1.SaveAs(Server.MapPath("~/Images/NewsPrivate/" + lblNewsID.Text + "/" + UlFileName))
        End If

        Dim folder As String = Server.MapPath("~/Images/NewsPrivate/" + lblNewsID.Text)
        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If

        Dim FilePath As String
        If FileUpload1.HasFile Then
            FilePath = "~/Images/NewsPrivate/" + lblNewsID.Text + "/" + UlFileName
        Else
            FilePath = ""
        End If

        Conn.Close()

        ctlN.PrivateNews_Save(StrNull2Zero(lblNewsID.Text), ddlCategory.SelectedValue, txtHead.Text, ConvertStatus2YN(chkTH.Checked), ConvertStatus2YN(chkEN.Checked), txtDetail.Html, ConvertCheckedStatus2Number(chkActive.Checked), UlFileName)

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "OK", "<script>alert('บันทึกเรียบร้อยแล้ว!');</script>")
        'Response.Redirect("NewsAddToUpdate.aspx?NewsID=" + lblNewsID.Text)
    End Sub

    Private Sub EditNews(NewsID As Integer)
        sb.Remove(0, sb.Length)
        sb.Append("SELECT * FROM NewsInfoPrivate WHERE (NewsID=@NewsID) ")
        sql = sb.ToString
        DA = New SqlDataAdapter(sql, Conn)
        DA.SelectCommand.Parameters.Clear()
        DA.SelectCommand.Parameters.Add("@NewsID", SqlDbType.Int).Value = NewsID
        Dim dt As New DataTable
        DA.Fill(dt)
        If dt.Rows.Count > 0 Then
            lblNewsID.Text = NewsID.ToString()
            txtHead.Text = dt.Rows(0).Item("NewsHead")
            txtDetail.Html = dt.Rows(0).Item("NewsDetail")
            If Convert.ToString(dt.Rows(0).Item("FilePath")) <> "" Then
                lblAttach.Text = "<a href='" & dt.Rows(0).Item("FilePath").ToString() + "' target='_blank'>" & Path.GetFileNameWithoutExtension((dt.Rows(0).Item("FilePath").ToString())) + "</a>"

            Else
                lblAttach.Text = "ไม่มีไฟล์แนบ"
            End If
            'ASPxFileManager1.Settings.RootFolder = "~/Images/NewsPrivate/" + NewsID.ToString() + "/"
        End If
    End Sub

End Class