Imports System.Net
Imports System.ComponentModel
Imports System.IO
Imports ProtoBuf

Public Class ViewModel
    Implements INotifyPropertyChanged

    Public Sub New ()
        CreateAdjustSearchResultsCommand()
        CreateChangeAllMuscleSearchFeaturesCommand()
        CreateAddVideoPreviewCommand()
        CreateAddVideoCommand()
        CreateResetSearchCommand()
        CreateOpenAddNewWorkoutWindowCommand()
        CreateAddWorkoutCommand()
        CreateAddVideoToWorkoutCommand()
        CreateDeleteSelectedWorkoutCommand()
    End Sub

#Region "Properties"
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

    Private _newVideo As Video
    Public Property NewVideo() As Video
        Get
            Return _newVideo
        End Get
        Set(ByVal value As Video)
            _newVideo = value
            RaiseProp("NewVideo")
        End Set
    End Property

    Private _statusInformation As String
    Public Property StatusInformation() As String
        Get
            Return _statusInformation
        End Get
        Set(ByVal value As String)
            _statusInformation = value
            RaiseProp("StatusInformation")
        End Set
    End Property

    Private _allWorkouts As WorkoutsList
    Public Property AllWorkouts() As WorkoutsList
        Get
            Return _allWorkouts
        End Get
        Set(ByVal value As WorkoutsList)
            _allWorkouts = value
            RaiseProp("AllWorkouts")
        End Set
    End Property

    Private _newWorkout As Workout
    Public Property NewWorkout() As Workout
        Get
            Return _newWorkout
        End Get
        Set(ByVal value As Workout)
            _newWorkout = value
            RaiseProp("NewWorkout")
        End Set
    End Property

    Private _selectedWorkout As Workout
    Public Property SelectedWorkout() As Workout
        Get
            Return _selectedWorkout
        End Get
        Set(ByVal value As Workout)
            _selectedWorkout = value
            RaiseProp("SelectedWorkout")
        End Set
    End Property

    Private _selectedVideo As Video
    Public Property SelectedVideo() As Video
        Private Get
            Return _selectedVideo
        End Get
        Set(ByVal value As Video)
            _selectedVideo = value
            RaiseProp("SelectedVideo")
        End Set
    End Property

    Private _chosenWorkout As Workout
    Public Property ChosenWorkout() As Workout
        Get
            Return _ChosenWorkout
        End Get
        Set(ByVal value As Workout)
            _ChosenWorkout = value
            RaiseProp("ChosenWorkout")
        End Set
    End Property
#End Region


    Public Function GetVideo(ByVal url As String, ByVal dateAdded As Date, ByVal rating As Integer, ByVal difficulty As Difficulty, ByVal categories As Categories) As Video

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

        Return New Video(url, title, author, description, duration, imageUrl, GetImage(imageUrl), dateAdded, rating, difficulty, categories, True)
    End Function

    Private Function GetImage(ByVal imageUrl As String) As Windows.Media.ImageSource

        Dim webUri As New Uri(imageUrl)
        Dim bDecoder As BitmapDecoder = BitmapDecoder.Create(webUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None)

        If bDecoder IsNot Nothing AndAlso bDecoder.Frames.Count > 0 Then
            Return bDecoder.Frames(0)
        End If
        Return Nothing
    End Function

    Private Function CreateSpecialCharacters(ByVal originalText As String) As String
        Return originalText.Replace("&#39;", "'")
    End Function

    Private Function CheckIfDateIsInRange(ByVal startDate as Date, ByVal endDate As Date, ByVal dateToCheck As Date)

        If dateToCheck >= startDate AndAlso dateToCheck <= endDate
            Return True
        End If
        Return False
    End Function

    Public Sub SaveToBinary()
        If Not Directory.Exists(FilesDirectory)
            Directory.CreateDirectory(FilesDirectory)
        End If

        Using file1 = File.Create(FilesDirectory & "\AllVideos.bin")
            Serializer.Serialize(file1, AllVideos)
        End Using
        Using file2 = File.Create(FilesDirectory & "\AllWorkouts.bin")
            Serializer.Serialize(file2, AllWorkouts)
        End Using
    End Sub

    Public Sub LoadFromBinary()
        If File.Exists(FilesDirectory & "\AllVideos.bin")
            Using file1 = File.OpenRead(FilesDirectory & "\AllVideos.bin")
	            AllVideos = Serializer.Deserialize(Of VideosList)(file1)
            End Using
            for i = 0 To AllVideos.Videos.Count - 1
                AllVideos.Videos(i).VideoImage = GetImage(AllVideos.Videos(i).VideoImageUrl)
            Next
        End If
        If File.Exists(FilesDirectory & "\AllWorkouts.bin")
            Using file2 = File.OpenRead(FilesDirectory & "\AllWorkouts.bin")
	            AllWorkouts = Serializer.Deserialize(Of WorkoutsList)(file2)
            End Using
            for i = 0 To AllWorkouts.Workouts.Count - 1
                for j = 0 To AllWorkouts.Workouts(i).WorkoutVideos.Videos.Count - 1
                    AllWorkouts.Workouts(i).WorkoutVideos.Videos(j).VideoImage = GetImage(AllWorkouts.Workouts(i).WorkoutVideos.Videos(j).VideoImageUrl)
                Next
            Next
        End If
    End Sub

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
            If SearchCaracteristics.SearchFeaturesCategories.CategoriesAbs = True AndAlso AllVideos.Videos(i).VideoCategories.CategoriesAbs = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesBack = True AndAlso AllVideos.Videos(i).VideoCategories.CategoriesBack = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesBiceps = True AndAlso AllVideos.Videos(i).VideoCategories.CategoriesBiceps = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesCardio = True AndAlso AllVideos.Videos(i).VideoCategories.CategoriesCardio = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesChest = True AndAlso AllVideos.Videos(i).VideoCategories.CategoriesChest = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesLeg = True AndAlso AllVideos.Videos(i).VideoCategories.CategoriesLeg = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesShoulder = True AndAlso AllVideos.Videos(i).VideoCategories.CategoriesShoulder = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesSpeed = True AndAlso AllVideos.Videos(i).VideoCategories.CategoriesSpeed = True
                SearchedVideos.Videos.Add(AllVideos.Videos(i))
            ElseIf SearchCaracteristics.SearchFeaturesCategories.CategoriesTriceps = True AndAlso AllVideos.Videos(i).VideoCategories.CategoriesTriceps = True
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

        If SearchCaracteristics.SearchFeaturesCategories.CategoriesAllMuscles = False
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

     'AddVideoPreviewCommand
    Private _addVideoPreviewCommand As ICommand
    Public Property AddVideoPreviewCommand() As ICommand
    Get
        Return _addVideoPreviewCommand
    End Get
    Set(ByVal value As ICommand)
        _addVideoPreviewCommand = value
        RaiseProp("AddVideoPreviewCommand")
    End Set
    End Property

    Private Function CanExecuteAddVideoPreviewCommand() As Boolean
        If NewVideo.VideoUrl = Nothing
            Return False
        End If
        Return True
    End Function

    Private Sub CreateAddVideoPreviewCommand()
        AddVideoPreviewCommand = New RelayCommand(AddressOf AddVideoPreviewExecute, AddressOf CanExecuteAddVideoPreviewCommand)
    End Sub

    Private Sub AddVideoPreviewExecute()
        If NewVideo.VideoUrl.StartsWith("www.")
            NewVideo.VideoUrl = "https://" & NewVideo.VideoUrl
        End If

        Try
            NewVideo = GetVideo(NewVideo.VideoUrl, Today, 0, New Difficulty(), _
                                New Categories(False, False, False, False, False, False, False, False, False, False)) 
        Catch
            NewVideo.VideoUrlOk = False
        End Try
        
    End Sub

    'AddVideoCommand
    Private _addVideoCommand As ICommand
    Public Property AddVideoCommand() As ICommand
        Get
            Return _addVideoCommand
        End Get
        Set(ByVal value As ICommand)
            _addVideoCommand = value
            RaiseProp("AddVideoCommand")
        End Set
    End Property

    Private Function CanExecuteAddVideoCommand() As Boolean
        If NewVideo Is Nothing
            Return False
        End If
        If NewVideo.VideoUrlOk = True
            If NewVideo.VideoCategories.CategoriesAbs = True Or NewVideo.VideoCategories.CategoriesBack = True Or _
                NewVideo.VideoCategories.CategoriesBiceps = True Or NewVideo.VideoCategories.CategoriesCardio = True Or _
                NewVideo.VideoCategories.CategoriesChest = True Or  NewVideo.VideoCategories.CategoriesLeg = True Or _
                NewVideo.VideoCategories.CategoriesShoulder = True Or  NewVideo.VideoCategories.CategoriesSpeed = True _
                Or  NewVideo.VideoCategories.CategoriesTriceps = True
                If NewVideo.VideoDifficulty.DifficultyEasy = True Or NewVideo.VideoDifficulty.DifficlutyIntermediate = True _
                    Or NewVideo.VideoDifficulty.DifficultyDifficult = True
                    If Not NewVideo.VideoRating = 0
                        Return True
                    End If
                End If
            End If
        End If
        Return False
    End Function

    Private Sub CreateAddVideoCommand
        AddVideoCommand = New RelayCommand(AddressOf AddVideoExecute, AddressOf CanExecuteAddVideoCommand)
    End Sub

    Private Sub AddVideoExecute
        AllVideos.Videos.Add(NewVideo)
        StatusInformation = """" & NewVideo.VideoTitle & """" & " wurde erfolgreich hinzugefügt."
        NewVideo = New Video()
        AdjustSearchResultsExecute()
    End Sub


    'ResetSearchCommand
    Private _resetSearchCommand As ICommand
    Public Property ResetSearchCommand() As ICommand
        Get
            Return _resetSearchCommand
        End Get
        Set(ByVal value As ICommand)
            _resetSearchCommand = value
            RaiseProp("ResetSearchCommand")
        End Set
    End Property

    Private Function CanExecuteResetSearchCommand() As Boolean
        If SearchCaracteristics is Nothing
            Return False
        End If
        If SearchCaracteristics.SearchFeaturesCategories.CategoriesAbs = True AndAlso SearchCaracteristics.SearchFeaturesCategories.CategoriesAllMuscles = True _
            AndAlso SearchCaracteristics.SearchFeaturesCategories.CategoriesBack = True  _
            AndAlso SearchCaracteristics.SearchFeaturesCategories.CategoriesBiceps = True AndAlso _
            SearchCaracteristics.SearchFeaturesCategories.CategoriesBiceps = True AndAlso SearchCaracteristics.SearchFeaturesCategories.CategoriesCardio = True _
            AndAlso SearchCaracteristics.SearchFeaturesCategories.CategoriesChest = True AndAlso _
            SearchCaracteristics.SearchFeaturesCategories.CategoriesLeg = True AndAlso SearchCaracteristics.SearchFeaturesCategories.CategoriesShoulder = True _
            AndAlso SearchCaracteristics.SearchFeaturesCategories.CategoriesSpeed = True AndAlso _
            SearchCaracteristics.SearchFeaturesCategories.CategoriesTriceps = True AndAlso _
            SearchCaracteristics.SearchFeaturesDateAdded.DateAddedDoesntMatter = True AndAlso _
            SearchCaracteristics.SearchFeaturesDifficulty.DifficultyDoesntMatter = True AndAlso _
            SearchCaracteristics.SearchFeaturesDuration.DurationDoesntMatter = True AndAlso SearchCaracteristics.SearchFeaturesKeyword = Nothing Then

            Return False
        End If
        Return True
    End Function

    Private Sub CreateResetSearchCommand()
        ResetSearchCommand = New RelayCommand(AddressOf ResetSearchExecute, AddressOf CanExecuteResetSearchCommand)
    End Sub

    Private Sub ResetSearchExecute()
        SearchCaracteristics = New SearchFeatures(Nothing, New Categories(True, True, True, True, True, True, True, True, True, True), New Difficulty(), New Duration(), New DateAdded())
    End Sub

    'OpenAddNewWorkoutWindowCommand
    Private _openAddNewWorkoutWindowCommand As ICommand
    Public Property OpenAddNewWorkoutWindowCommand() As ICommand
        Get
            Return _openAddNewWorkoutWindowCommand
        End Get
        Set(ByVal value As ICommand)
            _openAddNewWorkoutWindowCommand = value
            RaiseProp("OpenAddNewWorkoutWindowCommand")
        End Set
    End Property

    Private Function CanExecuteOpenAddNewWorkoutWindowCommand() As Boolean
        Return Not Windows.Application.Current.Windows.OfType (Of AddNewWorkoutWindow)().Any()
    End Function

    Private Sub CreateOpenAddNewWorkoutWindowCommand()
        OpenAddNewWorkoutWindowCommand = New RelayCommand(AddressOf OpenAddNewWorkoutWindowExecute, AddressOf CanExecuteOpenAddNewWorkoutWindowCommand)
    End Sub

    Private Sub OpenAddNewWorkoutWindowExecute()
        Dim w As New AddNewWorkoutWindow()
        w.Show()
    End Sub

    'AddWorkoutCommand
    Private _addWorkoutCommand As ICommand
    Public Property AddWorkoutCommand() As ICommand
        Get
            Return _addWorkoutCommand
        End Get
        Set(ByVal value As ICommand)
            _addWorkoutCommand = value
            RaiseProp("AddWorkoutCommand")
        End Set
    End Property

    Private Function CanExecuteAddWorkoutCommand() As Boolean
        If NewWorkout Is Nothing
            Return False
        End If
        If NewWorkout.WorkoutTitle = Nothing
            Return False
        End If
        If NewWorkout.WorkoutTitle.Trim() = Nothing
            Return False
        End If
        Return True
    End Function

    Private Sub CreateAddWorkoutCommand()
        AddWorkoutCommand = New RelayCommand(AddressOf AddWorkoutExecute, AddressOf CanExecuteAddWorkoutCommand)
    End Sub

    Private Sub AddWorkoutExecute()
        AllWorkouts.Workouts.Add(NewWorkout)
        NewWorkout = New Workout()
        For Each window As Window In Windows.Application.Current.Windows.OfType (Of AddNewWorkoutWindow)()
            window.Close()
        Next
    End Sub

    'AddVideoToWorkoutCommand
    Private _addVideoToWorkoutCommand As ICommand

    Public Property AddVideoToWorkoutCommand() As ICommand
        Get
            Return _addVideoToWorkoutCommand
        End Get
        Set(ByVal value As ICommand)
            _addVideoToWorkoutCommand = value
            RaiseProp("AddVideoToWorkoutCommand")
        End Set
    End Property

    Private Function CanExecuteAddVideoToWorkoutCommand() As Boolean
        If ChosenWorkout Is Nothing
            Return False
        End If
        Return True
    End Function

    Private Sub CreateAddVideoToWorkoutCommand()
        AddVideoToWorkoutCommand = New RelayCommand(AddressOf AddVideoToWorkoutExecute, AddressOf CanExecuteAddVideoToWorkoutCommand)
    End Sub

    Private Sub AddVideoToWorkoutExecute()
        ChosenWorkout.WorkoutVideos.Videos.Add(SelectedVideo)
         For Each window As Window In Windows.Application.Current.Windows.OfType (Of AddVideoToWorkoutWindow)()
            window.Close()
        Next
        MsgBox("""" & SelectedVideo.VideoTitle & """ wurde erfolgreich zu """ & ChosenWorkout.WorkoutTitle & """ hinzugefügt.", MsgBoxStyle.Information, _
               "Hinzufügen erfolgreich")
    End Sub

    'DeleteSelectedWorkoutCommand
    Private _deleteSelectedWorkoutCommand As ICommand
    Public Property DeleteSelectedWorkoutCommand() As ICommand
        Get
            Return _deleteSelectedWorkoutCommand
        End Get
        Set(ByVal value As ICommand)
            _deleteSelectedWorkoutCommand = value
            RaiseProp("DeleteSelectedWorkoutCommand")
        End Set
    End Property

    Private Function CanExecuteDeleteSelectedWorkoutCommand() As Boolean
        If SelectedWorkout Is Nothing
            Return False
        End If
        Return True
    End Function

    Private Sub CreateDeleteSelectedWorkoutCommand()
        DeleteSelectedWorkoutCommand = New RelayCommand(AddressOf DeleteSelectedWorkoutExecute, AddressOf CanExecuteDeleteSelectedWorkoutCommand)
    End Sub

    Private Sub DeleteSelectedWorkoutExecute()
        Dim result As MsgBoxResult = MsgBox("Möchten Sie das ausgewählte Workout """ & SelectedWorkout.WorkoutTitle & """ wirklich löschen?", _
                                            MsgBoxStyle.YesNoCancel, "Workout löschen")
        If result = MsgBoxResult.Yes
            AllWorkouts.Workouts.Remove(SelectedWorkout)
            SelectedWorkout = Nothing
        End If
    End Sub
#End Region

    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class

