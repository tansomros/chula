Imports System.Net
Imports System.IO
Public Class ack
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblCK.Text = "LoginID = " & Request.Cookies("UserLoginID").Value & "<br/>"
        lblCK.Text = lblCK.Text & "UserLogin = " & Request.Cookies("UserLogin").Value & "<br/>"
        lblCK.Text = lblCK.Text & "Password = " & Request.Cookies("Password").Value & "<br/>"
        'lblCK.Text  =  lblCK.Text & "LastLogin" & Request.Cookies("LastLogin").value & "<br/>"
        lblCK.Text = lblCK.Text & "Name = " & Request.Cookies("NameOfUser").Value & "<br/>"
        lblCK.Text = lblCK.Text & "RoleID = " & Request.Cookies("RoleID").Value & "<br/>"
        lblCK.Text = lblCK.Text & "PersonID = " & Request.Cookies("PersonID").Value & "<br/>"
        lblCK.Text = lblCK.Text & "LocationID = " & Request.Cookies("LocationID").Value & "<br/>"

        lblCK.Text = lblCK.Text & "ROLE_SPA = " & Request.Cookies("ROLE_SPA").Value & "<br/>"
        lblCK.Text = lblCK.Text & "ROLE_ADM = " & Request.Cookies("ROLE_ADM").Value & "<br/>"
        lblCK.Text = lblCK.Text & "ROLE_STD = " & Request.Cookies("ROLE_STD").Value & "<br/>"
        lblCK.Text = lblCK.Text & "ROLE_TCH = " & Request.Cookies("ROLE_TCH").Value & "<br/>"
        lblCK.Text = lblCK.Text & "ROLE_USR = " & Request.Cookies("ROLE_USR").Value & "<br/>"
        lblCK.Text = lblCK.Text & "ROLE_PCT = " & Request.Cookies("ROLE_PCT").Value



    End Sub
    Public Sub lineNotify(ByVal msg As String)
        Dim token As String = "GEETKyNRuV71ROCS3PziGY7L3DktgdZVUCUntng82Nv" ' "MwjfXz1Pb2EBCPe197a7Tu0qCgUjTlKqA2wc4cMQ6CZ" << Tansomros
        'Dim msg As String = "ทดสอบจ้าาา...."

        Try
            Dim request = CType(WebRequest.Create("https://notify-api.line.me/api/notify"), HttpWebRequest)
            Dim postData = String.Format("message={0}", msg)
            Dim data = Encoding.UTF8.GetBytes(postData)
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = data.Length
            request.Headers.Add("Authorization", "Bearer " & token)

            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            Dim response = CType(request.GetResponse(), HttpWebResponse)
            Dim responseString = New StreamReader(response.GetResponseStream()).ReadToEnd()
        Catch ex As Exception
            Console.Write(ex.ToString())
        End Try
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        lineNotify("ทดสอบจ้าาา...." & vbCrLf & "ขึ้นบรรทัดใหม่")
    End Sub



End Class