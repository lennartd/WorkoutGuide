Imports System.Net
Imports System.ComponentModel

Public Class ViewModel
    Implements INotifyPropertyChanged

    Public Sub New ()
        CreateAdjustSearchResultsCommand()
        CreateChangeAllMuscleSearchFeaturesCommand()
    End Sub


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

    Private _searchedVideos As VideosList

    Public Property SearchedVideos() As VideosList
        Get
            Return _searchedVideos
        End Get
        Set(ByVal value As VideosList)
            _searchedVideos = value
            RaiseProp("SearchedVideos")
        End Set
    End Property

    Private _searchCharacteristics As SearchFeatures

    Public Property SearchCaracteristics() As SearchFeatures
        Get
            Return _searchCharacteristics
        End Get
        Set(ByVal value As SearchFeatures)
            _searchCharacteristics = value
            RaiseProp("SearchCaracteristics")
        End Set
    End Property

    Public Sub AddVideo(ByVal url As String, ByVal dateAdded As Date, ByVal rating As Integer, ByVal difficulty As Difficulty, ByVal categories As Category)

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



#Region "Commands"
    'AdjustSearchResultsCommand
    Private _adjustSearchResultsCommand As ICommand
    Public Property AdjustSearchResultsCommand() As ICommand
        Get
            Return _adjustSearchResultsCommand
        End Get
        Set(ByVal value As ICommand)
            _adjustSearchResultsCommand = value
            RaiseProp("AdjustSearchResultsCommand")
        End Set
    End Property

    Private Function CanExecuteAdjustSearchResultsCommand() As Boolean
        Return True
    End Function

    Private Sub CreateAdjustSearchResultsCommand()
        AdjustSearchResultsCommand = New RelayCommand(AddressOf AdjustSearchResultsExecute, AddressOf CanExecuteAdjustSearchResultsCommand)
    End Sub

    Private Sub AdjustSearchResultsExecute()
        
        SearchedVideos = New VideosList()

       For i = 0 To AllVideos.Videos.Count - 1
            
            'Categories
            If SearchCaracteristics.SearchFeaturesCategories.CategoriesAbs = True AndAlso AllVideos.Videos(i).VideoCategories.Abs = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesBack = True AndAlso AllVideos.Videos(i).VideoCategories.Back = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesBiceps = True AndAlso AllVideos.Videos(i).VideoCategories.Biceps = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesCardio = True AndAlso AllVideos.Videos(i).VideoCategories.Cardio = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesChest = True AndAlso AllVideos.Videos(i).VideoCategories.Chest = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesLeg = True AndAlso AllVideos.Videos(i).VideoCategories.Leg = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesShoulder = True AndAlso AllVideos.Videos(i).VideoCategories.Shoulder = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesSpeed = True AndAlso AllVideos.Videos(i).VideoCategories.Speed = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesTriceps = True AndAlso AllVideos.Videos(i).VideoCategories.Triceps = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            End If
       Next

        'Difficulty
        If SearchCaracteristics.SearchFeaturesDifficulty.DifficultyDoesntMatter = False
            Dim newSearchedVideos As VideosList = New VideosList()
            For i = 0 To SearchedVideos.Videos.Count - 1
                If SearchCaracteristics.SearchFeaturesDifficulty.DifficultyEasy = True AndAlso SearchedVideos.Videos(i).VideoDifficulty.DifficultyEasy = True
                    newSearchedVideos.Videos.Add(SearchedVideos.Videos(i))
                ElseIf SearchCaracteristics.SearchFeaturesDifficulty.DifficlutyIntermediate = True AndAlso SearchedVideos.Videos(i).VideoDifficulty.DifficlutyIntermediate = True
                    newSearchedVideos.Videos.Add(SearchedVideos.Videos(i))
                ElseIf SearchCaracteristics.SearchFeaturesDifficulty.DifficultyDifficult = True AndAlso SearchedVideos.Videos(i).VideoDifficulty.DifficultyDifficult = True
                    newSearchedVideos.Videos.Add(SearchedVideos.Videos(i))
                End If
            Next
            SearchedVideos = newSearchedVideos
        End If

        'Duration
        If SearchCaracteristics.SearchFeaturesDuration.DurationDoesntMatter = False
            Dim newSearchedVideos As VideosList = New VideosList()
            For i = 0 To SearchedVideos.Videos.Count - 1
                If SearchCaracteristics.SearchFeaturesDuration.DurationZeroToTen = True AndAlso SearchedVideos.Videos(i).VideoDuration <= 600
                    newSearchedVideos.Videos.Add(SearchedVideos.Videos(i))
                ElseIf SearchCaracteristics.SearchFeaturesDuration.DurationTenToThirty = True AndAlso SearchedVideos.Videos(i).VideoDuration >=600 _
                    AndAlso SearchedVideos.Videos(i).VideoDuration <= 1800
                    newSearchedVideos.Videos.Add(SearchedVideos.Videos(i))
                ElseIf SearchCaracteristics.SearchFeaturesDuration.DurationOverThirty = True AndAlso SearchedVideos.Videos(i).VideoDuration >=1800
                    newSearchedVideos.Videos.Add(SearchedVideos.Videos(i))
                End If
            Next
            SearchedVideos = newSearchedVideos
        End If

        'DateAdded
        If SearchCaracteristics.SearchFeaturesDateAdded.DateAddedDoesntMatter = False
            Dim newSearchedVideos As VideosList = New VideosList()
            For i = 0 To SearchedVideos.Videos.Count - 1
                If SearchCaracteristics.SearchFeaturesDateAdded.DateAddedToday = True AndAlso SearchedVideos.Videos(i).VideoDateAdded = Today
                    newSearchedVideos.Videos.Add(SearchedVideos.Videos(i))
                ElseIf SearchCaracteristics.SearchFeaturesDateAdded.DateAddedYesterday = True AndAlso SearchedVideos.Videos(i).VideoDateAdded = Today.AddDays(-1)
                    newSearchedVideos.Videos.Add(SearchedVideos.Videos(i))
                ElseIf SearchCaracteristics.SearchFeaturesDateAdded.DateAddedLastWeek = True AndAlso CheckIfDateIsInRange(Today.AddDays(-7), Today, SearchedVideos.Videos(i).VideoDateAdded) = True
                    newSearchedVideos.Videos.Add(SearchedVideos.Videos(i))
                ElseIf SearchCaracteristics.SearchFeaturesDateAdded.DateAddedLastMonth = True AndAlso CheckIfDateIsInRange(Today.AddDays(-31), Today, SearchedVideos.Videos(i).VideoDateAdded) = True
                    newSearchedVideos.Videos.Add(SearchedVideos.Videos(i))
                End If
            Next
            SearchedVideos = newSearchedVideos
        End If

        'Keyword
        If Not SearchCaracteristics.SearchFeaturesKeyword = Nothing
            Dim newSearchedVideos As VideosList = New VideosList()
            For i = 0 To SearchedVideos.Videos.Count - 1
                If SearchedVideos.Videos(i).VideoTitle.ToLower().Contains(SearchCaracteristics.SearchFeaturesKeyword.ToLower()) Or _
                SearchedVideos.Videos(i).VideoAuthor.ToLower().Contains(SearchCaracteristics.SearchFeaturesKeyword.ToLower()) Or _
                SearchedVideos.Videos(i).VideoDescription.ToLower().Contains(SearchCaracteristics.SearchFeaturesKeyword.ToLower)

                newSearchedVideos.Videos.Add(SearchedVideos.Videos(i))
                End If
            Next
            SearchedVideos = newSearchedVideos
        End If
    End Sub


    'ChangeAllMuscleSearchFeaturesCommand
    Private _changeAllMuscleSearchFeaturesCommand As ICommand
    Public Property ChangeAllMuscleSearchFeaturesCommand() As ICommand
    Get
        Return _changeAllMuscleSearchFeaturesCommand
    End Get
    Set(ByVal value As ICommand)
        _changeAllMuscleSearchFeaturesCommand = value
        RaiseProp("ChangeAllMuscleSearchFeaturesCommand")
    End Set
    End Property

    Private Function CanExecuteChangeAllMuscleSearchFeaturesCommand() As Boolean
        Return True
    End Function

    Private Sub CreateChangeAllMuscleSearchFeaturesCommand()
        ChangeAllMuscleSearchFeaturesCommand = New RelayCommand(AddressOf ChangeAllMuscleSearchFeaturesExecute, AddressOf CanExecuteChangeAllMuscleSearchFeaturesCommand)
    End Sub

    Private Sub ChangeAllMuscleSearchFeaturesExecute()

        If SearchCaracteristics.SearchFeaturesCategories.CategoriesAllMuscles = True
            SearchCaracteristics.SearchFeaturesCategories.CategoriesAllMuscles = False
            SearchCaracteristics.SearchFeaturesCategories.CategoriesAbs = False
            SearchCaracteristics.SearchFeaturesCategories.CategoriesBack = False
            SearchCaracteristics.SearchFeaturesCategories.CategoriesBiceps = False
            SearchCaracteristics.SearchFeaturesCategories.CategoriesChest = False
            SearchCaracteristics.SearchFeaturesCategories.CategoriesLeg = False
            SearchCaracteristics.SearchFeaturesCategories.CategoriesShoulder = False
            SearchCaracteristics.SearchFeaturesCategories.CategoriesTriceps = False
        Else 
            SearchCaracteristics.SearchFeaturesCategories.CategoriesAllMuscles = True
            SearchCaracteristics.SearchFeaturesCategories.CategoriesAbs = True
            SearchCaracteristics.SearchFeaturesCategories.CategoriesBack = True
            SearchCaracteristics.SearchFeaturesCategories.CategoriesBiceps = True
            SearchCaracteristics.SearchFeaturesCategories.CategoriesChest = True
            SearchCaracteristics.SearchFeaturesCategories.CategoriesLeg = True
            SearchCaracteristics.SearchFeaturesCategories.CategoriesShoulder = True
            SearchCaracteristics.SearchFeaturesCategories.CategoriesTriceps = True
        End If
    End Sub

#End Region


    Private Function CheckIfDateIsInRange(ByVal startDate as Date, ByVal endDate As Date, ByVal dateToCheck As Date)

        If dateToCheck >= startDate AndAlso dateToCheck <= endDate
            Return True
        End If
        Return False
    End Function








    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class

