
Imports System
Imports System.Text
'****************************************************************************
'/ <summary>YouTube player Javascript generator</summary> 
Public Class YouTubeScript
    '/ <summary>
    '/ YouTube player script generator, default:
    '/ default width = 320
    '/ default height = 240
    '/ no autoplay
    '/ </summary>
    '/ <param name="id"id</param>
    '/ <param name="auto"int</param>
    '/ <Returns>string</Returns>
    Public Shared Function Get_c(ByVal id As String) As String
        ' default size: 320x240, no autoplay
        Return Get_b(id, 0, 320, 240)
    End Function

#Region "YouTube script, autoplay option"
    '/ <summary>
    '/ YouTube player script generator, overload:
    '/ default width = 320
    '/ default height = 240
    '/ </summary>
    '/ <param name="id"id</param>
    '/ <param name="auto"int</param>
    '/ <Returns>string</Returns>
    Public Shared Function Get_a(ByVal id As String, ByVal auto As Integer) As String

        ' default size: 320x240
        Return Get_b(id, auto, 320, 240)
    End Function
#End Region

#Region "YouTube script: autoplay option, adjustable W/H"

    '/ <summary>
    '/ YouTube player script generator (w/autoplay, W/H options)
    '/ </summary>
    '/ <param name="id"id</param>
    '/ <param name="auto"int</param>
    '/ <param name="W"int</param>
    '/ <param name="H"int</param>
    '/ <Returns>string</Returns>
    Public Shared Function Get_b(ByVal id As String, ByVal auto As Integer, ByVal W As Integer, ByVal H As Integer) As String
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append("<embed src='http://www.youtube.com/v/")
        ' select the item to play
        sb.Append(id)
        sb.Append("&autoplay=")
        ' set autoplay options (indicates number of plays)
        sb.Append(auto.ToString())
        sb.Append("' ")
        sb.Append("type='application/x-shockwave-flash' ")
        sb.Append("allowscriptaccess='never' enableJavascript ='false' ")
        sb.Append("allowfullscreen='true' ")
        ' set width
        sb.Append("width='" + W.ToString() + "' ")
        ' set height
        sb.Append("height='" + H.ToString() + "' ")
        sb.Append("></embed>")

        Dim scr As String = sb.ToString()
        sb = Nothing
        Return scr
    End Function
#End Region
End Class
'/*****************************************************************************

'----------------------------------------------------------------
' Converted from C# to VB .NET Imports CSharpToVBConverter(1.2).
' Developed by: Kamal Patel (http:'www.KamalPatel.net) 

