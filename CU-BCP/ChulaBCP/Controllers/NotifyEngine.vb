Imports System.Net
Imports System.IO
Public Class NotifyEngine
    Public Sub LineNotify(ByVal msg As String)
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


End Class
