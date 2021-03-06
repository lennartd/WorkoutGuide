﻿Imports System.ComponentModel
Imports System.IO
Imports ProtoBuf
Imports Google.YouTube
Imports Google.GData.YouTube
Imports System.Windows.Threading

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
        CreateRemoveInfoLabelCommand()
        CreateOpenSettingsWindowCommand()
        CreateOpenEditSelectedWorkoutWindowCommand()
        CreateReloadPicturesCommand()

        Dim timer As DispatcherTimer = New DispatcherTimer()
        AddHandler timer.Tick, AddressOf Timer_Tick
        timer.Interval = new TimeSpan(0,0,1)
        timer.Start()
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

    Private _allSettings As Settings
    Public Property AllSettings() As Settings
        Get
            Return _allSettings
        End Get
        Set(ByVal value As Settings)
            _allSettings = value
            RaiseProp("Settings")
        End Set
    End Property

    Private _internetConnection As Boolean
    Public Property InternetConnection() As Boolean
        Get
            Return _internetConnection
        End Get
        Set(ByVal value As Boolean)
            _internetConnection = value
            RaiseProp("InternetConnection")
        End Set
    End Property
#End Region


    Public Function GetVideo(ByVal url As String, ByVal dateAdded As Date, ByVal rating As Integer, ByVal difficulty As Difficulty, ByVal categories As Categories) As Video
        Dim videoId As String = url.Split("=")(1)

        Dim settings As New YouTubeRequestSettings("YoutubeAPI", "AIzaSyCEKSGmn79xg-lOxQSgaECmrSYoJxS06iA", "lennart_duemmel@gmx.de", "oberboihingen")
        Dim request As New YouTubeRequest(settings)

        Dim videoEntryUrl As Uri = New Uri("http://gdata.youtube.com/feeds/api/videos/" & videoId)
        Dim video As Google.YouTube.Video = request.Retrieve (Of Google.YouTube.Video)(videoEntryUrl)

        videoId = video.VideoId
        Dim title As String = video.Title
        Dim author As String = video.Author
        Dim description As String = video.Description
        Dim duration As Integer = Nothing
        For Each mediaContent As MediaContent In video.Contents
            duration = mediaContent.Duration 'in sec
        Next
        Dim imageUrl As String = "http://img.youtube.com/vi/" & videoId & "/0.jpg"
        Dim embedUrl As String = Nothing
' ReSharper disable once LoopCanBeConvertedToQuery
        For Each mediaContent As MediaContent In video.Contents
            embedUrl = mediaContent.Url
            Exit For 
        Next

        Return New Video(videoId, url, title, author, description, duration, imageUrl, GetImage(imageUrl), dateAdded, rating, difficulty, categories, True, embedUrl)
    End Function

    Private Function GetImage(ByVal imageUrl As String) As Windows.Media.ImageSource

        Dim webUri As New Uri(imageUrl)
        Dim bDecoder As BitmapDecoder = BitmapDecoder.Create(webUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None)

        If bDecoder IsNot Nothing AndAlso bDecoder.Frames.Count > 0 Then
            Return bDecoder.Frames(0)
        End If
        Return Nothing
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
        Using file3 = File.Create(FilesDirectory & "\AllSettings.bin")
            Serializer.Serialize(file3, AllSettings)
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
        If File.Exists(FilesDirectory & "\AllSettings.bin")
            Using file3 = File.OpenRead(FilesDirectory & "\AllSettings.bin")
	            AllSettings = Serializer.Deserialize(Of Settings)(file3)
            End Using
        End If
    End Sub

    Private Sub ReloadPictures()
        for i = 0 to AllVideos.Videos.Count - 1
                    AllVideos.Videos(i).VideoImage = GetImage(AllVideos.Videos(i).VideoImageUrl)
        Next
        for i = 0 To AllWorkouts.Workouts.Count - 1
            for j = 0 To AllWorkouts.Workouts(i).WorkoutVideos.Videos.Count - 1
                AllWorkouts.Workouts(i).WorkoutVideos.Videos(j).VideoImage = GetImage(AllWorkouts.Workouts(i).WorkoutVideos.Videos(j).VideoImageUrl)
            Next
        Next
        for i = 0 To SearchedVideos.Videos.Count - 1
            SearchedVideos.Videos(i).VideoImage = GetImage(SearchedVideos.Videos(i).VideoImageUrl)
        Next
    End Sub

#Region "Check Internet Connection"

    Public Const InternetErrorMessage As String = "Keine Internetverbindung erkannt. Workout Guide versucht automatisch eine neue Verbindung herzustellen."

    Private Declare Function InternetGetConnectedState Lib _
            "wininet.dll" (ByRef lpSFlags As Int32, _
            ByVal dwReserved As Int32) As Boolean

    Private Sub Timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        
        If AllVideos Is Nothing Or SearchedVideos Is Nothing Or AllWorkouts Is Nothing
            Exit Sub
        End If

        If CheckInternetConnection() = False
            If InternetConnection = True
                InternetConnection = False
                StatusInformation = InternetErrorMessage
            End If
        Else 
            If InternetConnection = False
                InternetConnection = True
                System.Threading.Thread.Sleep(3000)
                ReloadPictures()
                StatusInformation = "Verbindung zum Internet wurde wieder hergestellt."
            End If
        End If
    End Sub

    Private Function CheckInternetConnection() As Boolean
        Dim lngFlags As Long
        If InternetGetConnectedState(lngFlags, 0) Then
            ' True
            Return True
        Else
            ' False
            Return False
        End If
    End Function

#End Region

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
            If InternetConnection = False
                StatusInformation = InternetErrorMessage
            End If
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
        for i = 0 To AllVideos.Videos.Count - 1
            If NewVideo.VideoUrl = AllVideos.Videos(i).VideoUrl
                Dim result As MsgBoxResult = MsgBox("Das Video """ & NewVideo.VideoTitle & """ wurde bereits hinzugefügt. Trotzdem fortfahren?", _
                                                    MsgBoxStyle.YesNoCancel, "Mehrfach hinzufügen?")
                If Not result = MsgBoxResult.Yes
                    Exit Sub
                End If
                Exit For
            End If
        Next
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
        StatusInformation = """" & SelectedVideo.VideoTitle & """ wurde erfolgreich zu """ & ChosenWorkout.WorkoutTitle & """ hinzugefügt."
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
            StatusInformation = "Das Workout """ & SelectedWorkout.WorkoutTitle & """ wurde erfolgreich entfernt."
            SelectedWorkout = Nothing
        End If
    End Sub

    'RemoveInfoLabelCommand
    Private _removeInfoLabelCommand As ICommand
    Public Property RemoveInfoLabelCommand() As ICommand
        Get
            Return _removeInfoLabelCommand
        End Get
        Set(ByVal value As ICommand)
            _removeInfoLabelCommand = value
            RaiseProp("RemoveInfoLabelCommand")
        End Set
    End Property

    Private Function CanExecuteRemoveInfoLabelCommand() As Boolean
        Return True
    End Function

    Private Sub CreateRemoveInfoLabelCommand()
        RemoveInfoLabelCommand = New RelayCommand(AddressOf RemoveInfoLabelExecute, AddressOf CanExecuteRemoveInfoLabelCommand)
    End Sub

    Private Sub RemoveInfoLabelExecute()
        StatusInformation = Nothing
    End Sub

    'OpenSettingsWindowCommand
    Private _openSettingsWindowCommand As ICommand
    Public Property OpenSettingsWindowCommand() As ICommand
        Get
            Return _openSettingsWindowCommand
        End Get
        Set(ByVal value As ICommand)
            _openSettingsWindowCommand = value
            RaiseProp("OpenSettingsWindowCommand")
        End Set
    End Property

    Private Function CanExecuteOpenSettingsWindowCommand() As Boolean
        Return Not Windows.Application.Current.Windows.OfType (Of SettingsWindow)().Any()
    End Function

    Private Sub CreateOpenSettingsWindowCommand()
        OpenSettingsWindowCommand = New RelayCommand(AddressOf OpenSettingsWindowExecute, AddressOf CanExecuteOpenSettingsWindowCommand)
    End Sub

    Private Sub OpenSettingsWindowExecute()
        Dim w As New SettingsWindow
        w.Show()
    End Sub

    'OpenEditSelectedWorkoutWindowCommand
    Private _openEditSelectedWorkoutWindowCommand As ICommand
    Public Property OpenEditSelectedWorkoutWindowCommand() As ICommand
        Get
            Return _openEditSelectedWorkoutWindowCommand
        End Get
        Set(ByVal value As ICommand)
            _openEditSelectedWorkoutWindowCommand = value
            RaiseProp("OpenEditSelectedWorkoutWindowCommand")
        End Set
    End Property

    Private Function CanExecuteOpenEditSelectedWorkoutWindowCommand() As Boolean
        If SelectedWorkout Is Nothing
            Return False
        End If
        Return True
    End Function

    Private Sub CreateOpenEditSelectedWorkoutWindowCommand()
        OpenEditSelectedWorkoutWindowCommand = New RelayCommand(AddressOf OpenEditSelectedWorkoutWindowExecute, AddressOf CanExecuteOpenEditSelectedWorkoutWindowCommand)
    End Sub

    Private Sub OpenEditSelectedWorkoutWindowExecute()
        Dim w as New EditSelectedWorkoutWindow
        w.Show()
    End Sub

    'ReloadPicturesCommand
    Private _reloadPicturesCommand As ICommand
    Public Property ReloadPicturesCommand() As ICommand
        Get
            Return _reloadPicturesCommand
        End Get
        Set(ByVal value As ICommand)
            _reloadPicturesCommand = value
            RaiseProp("ReloadPicturesCommand")
        End Set
    End Property

    Private Function CanExecuteReloadPicturesCommand() As Boolean
        If AllVideos Is Nothing Or SearchedVideos Is Nothing Or AllWorkouts Is Nothing
            Return False
        End If
        If InternetConnection = True
            Return True
        End If
        Return False
    End Function

    Private Sub CreateReloadPicturesCommand()
        ReloadPicturesCommand = New RelayCommand(AddressOf ReloadPicturesExecute, AddressOf CanExecuteReloadPicturesCommand)
    End Sub

    Private Sub ReloadPicturesExecute()
        ReloadPictures()
        StatusInformation = "Bilder wurden erfolgreich neu geladen."
    End Sub
#End Region

    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged



















End Class

