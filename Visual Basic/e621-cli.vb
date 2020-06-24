Imports System.IO
Imports System.Net
Imports Newtonsoft.Json.Linq

Module Program

    Private inpt As String
    Private dest As String

    Sub Main(args As String())

        Console.WriteLine("Enter target server")
        dest = Console.ReadLine()
        Console.WriteLine("Enter tags (optional)")
        inpt = Console.ReadLine()

        Console.WriteLine("search results for " & inpt)

        Dim array As JArray = SearchPosts(dest, inpt)
        For Each item As JObject In array
            If item("file")("url") Is Nothing Then Continue For
            Dim value As String = item("file")("url")
            Console.WriteLine(value)

        Next

    End Sub

    Function SearchPosts(destination As String, arguments As String)
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse
        Dim reader As StreamReader
        Dim url As String = "https://e926.net/posts.json"

        If destination Is "e926" Then url = "https://" & destination & ".net/posts.json"
        If destination Is "e621" Then url = "https://" & destination & ".net/posts.json"

        request = DirectCast(WebRequest.Create(url & arguments), HttpWebRequest)

        request.UserAgent = "VB.NET Client"

        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())

        Dim rawresp As String
        rawresp = reader.ReadToEnd()

        Dim jResults As Object = JObject.Parse(rawresp)
        Return JArray.Parse(jResults("posts").ToString())
    End Function

End Module
