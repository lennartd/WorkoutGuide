Imports System.Net
Imports System.ComponentModel

Public Class ViewModel
    Implements INotifyPropertyChanged

    Private _allVideos As VideosList
    Public Property AllVideos() As VideosList
        Get
            Return _AllVideos
        End Get
        Set(ByVal value As VideosList)
            _AllVideos = value
            RaiseProp("VideosList")
        End Set
    End Property


      Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub



    Public Sub AddVideo(ByVal url As String, ByVal dateAdded As Date, ByVal rating As Integer, ByVal difficulty As String, ByVal categories As Category)

        Dim sourceString As String = New WebClient().DownloadString(url)

        'Get title
        Dim splitStringTitleStart As String() = New String() {"<title>"}
        Dim splitStringTitleEnd As String() = New String() {"</title>"}
        Dim title As String = CreateSpecialCharacters(sourceString.Split(splitStringTitleStart, StringSplitOptions.None)(1).Split(splitStringTitleEnd, StringSplitOptions.None)(0).Replace(" - YouTube", ""))

        'Get author
        Dim splitStringAuthorStart As String() = New String() {"'SHARE_CAPTION': "}
        Dim authorArray() As String = sourceString.Split(splitStringAuthorStart, StringSplitOptions.None)(1).Split(" ")

        Dim author As String = ""
        For i = 2 To authorArray.Length - 1

            author = author & " " & authorArray(i)
        Next

        author = author.Split("""")(0)
        author = CreateSpecialCharacters(author.Trim)

        'Get description
        Dim splitStringDescriptionStart As String() = New String() {"<meta name=""description"" content="""}
        Dim splitStringDescriptionEnd As String() = New String() {""">"}
        Dim description As String = CreateSpecialCharacters(sourceString.Split(splitStringDescriptionStart, StringSplitOptions.None)(1).Split(splitStringDescriptionEnd, StringSplitOptions.None)(0))

        'Get duration
        Dim splitStringDurationStart As String() = New String() {"""length_seconds"": "}
        Dim duration As Integer = CInt(sourceString.Split(splitStringDurationStart, StringSplitOptions.None)(1).Split(",")(0)) 'in sec

        'Get Image-URL
        Dim splitStringImageUrlStart As String() = New String() {"<meta property=""og:image"" content="""}
        Dim splitStringImageUrlEnd As String() = New String() {""">"}
        Dim imageUrl As String = sourceString.Split(splitStringImageUrlStart, StringSplitOptions.None)(1).Split(splitStringImageUrlEnd, StringSplitOptions.None)(0)

        Dim newVideo As New Video(url, title, author, description, duration, GetImage(imageUrl), dateAdded, rating, difficulty, categories)
        AllVideos.Videos.Add(newVideo)

    End Sub


    Private Function GetImage(ByVal pictureurl As String) As Windows.Media.ImageSource

        Dim webUri As New Uri(pictureurl)
        Dim bDecoder As BitmapDecoder = BitmapDecoder.Create(webUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None)

        If bDecoder IsNot Nothing AndAlso bDecoder.Frames.Count > 0 Then
            Return bDecoder.Frames(0)
        End If
        Return Nothing
    End Function

    Private Function CreateSpecialCharacters(ByVal originalText As String) As String
        Return originalText.Replace("&#39;", "'")
    End Function

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class

