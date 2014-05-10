Class Application

    ' Ereignisse auf Anwendungsebene wie Startup, Exit und DispatcherUnhandledException
    ' können in dieser Datei verarbeitet werden.

    Private Shared Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup

        MainViewModel = new ViewModel()

        MainViewModel.AllVideos = New VideosList()
        MainViewModel.SearchedVideos = New VideosList()
        MainViewModel.SearchCaracteristics = New SearchFeatures(Nothing, New Categories(), New Difficulty(), New Duration(), New DateAdded())
        MainViewModel.NewVideo = New Video()
        MainViewModel.AllWorkouts = New WorkoutsList()
        MainViewModel.NewWorkout = New Workout()
        MainViewModel.ChosenWorkout = New Workout()
        MainViewModel.SelectedVideo = New Video()

        Dim testCategories As Categories = New Categories(False, False, False, False, True, True, False, False, False, False)

        Dim testCategories2 As Categories = New Categories(False, True, False, False, False, False, False, False, False, False)

        Dim testDifficulty As Difficulty = New Difficulty()
        testDifficulty.DifficultyEasy = True

        Dim testDifficulty2 As Difficulty = New Difficulty()
        testDifficulty2.DifficultyDifficult = True

        MainViewModel.AllVideos.Videos.Add(MainViewModel.GetVideo("https://www.youtube.com/watch?v=HOx8-HoT7hs", Today, 0, testDifficulty, testCategories2))
        MainViewModel.AllVideos.Videos.Add(MainViewModel.GetVideo("https://www.youtube.com/watch?v=ulsj7JiSqOk", Today, 4, testDifficulty, testCategories))
        MainViewModel.AllVideos.Videos.Add(MainViewModel.GetVideo("https://www.youtube.com/watch?v=uzOe2ImO1rk&list=PLRCgg2aTq5NWNLW6o_NMUADMJ_P0r0aU9", Today, 0, testDifficulty, testCategories))
        MainViewModel.AllVideos.Videos.Add(MainViewModel.GetVideo("https://www.youtube.com/watch?v=ClXs1Ef2SKo&list=PLRCgg2aTq5NWNLW6o_NMUADMJ_P0r0aU9", Today, 0, testDifficulty2, testCategories))
        MainViewModel.AllVideos.Videos.Add(MainViewModel.GetVideo("https://www.youtube.com/watch?v=s3zAG4zvVpc", Today, 2, testDifficulty2, testCategories))

        MainViewModel.SearchedVideos = MainViewModel.AllVideos


        MainViewModel.AllWorkouts.Workouts.Add(New Workout("Mein Lieblingsworkout", "In diesem Workout finden sich all meine Lieblingsübungen wieder", _
                                                           MainViewModel.AllVideos))
    End Sub
End Class
