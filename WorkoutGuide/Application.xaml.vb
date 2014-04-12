Class Application

    ' Ereignisse auf Anwendungsebene wie Startup, Exit und DispatcherUnhandledException
    ' können in dieser Datei verarbeitet werden.

    Private Shared Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup

        MainViewModel = new ViewModel()

        MainViewModel.AllVideos = New VideosList()
        MainViewModel.SearchedVideos = New VideosList()
        MainViewModel.SearchCaracteristics = New SearchFeatures(Nothing, New Categories(), New Difficulty(), New Duration(), New DateAdded())

        Dim testCategories As Category
        testCategories.Abs = False
        testCategories.Cardio= True
        testCategories.Chest = True

        Dim testCategories2 As Category
        testCategories2.Abs = True

        Dim testDifficulty As Difficulty = New Difficulty()
        testDifficulty.DifficultyEasy = True

        Dim testDifficulty2 As Difficulty = New Difficulty()
        testDifficulty2.DifficultyDifficult = True

        MainViewModel.AddVideo("https://www.youtube.com/watch?v=HOx8-HoT7hs", Today, 0, testDifficulty, testCategories2)
        MainViewModel.AddVideo("https://www.youtube.com/watch?v=ulsj7JiSqOk", Today, 4, testDifficulty, testCategories)
        MainViewModel.AddVideo("https://www.youtube.com/watch?v=uzOe2ImO1rk&list=PLRCgg2aTq5NWNLW6o_NMUADMJ_P0r0aU9", Today, 0, testDifficulty, testCategories)
        MainViewModel.AddVideo("https://www.youtube.com/watch?v=ClXs1Ef2SKo&list=PLRCgg2aTq5NWNLW6o_NMUADMJ_P0r0aU9", Today, 0, testDifficulty2, testCategories)
        MainViewModel.AddVideo("https://www.youtube.com/watch?v=s3zAG4zvVpc", Today, 2, testDifficulty2, testCategories)

        MainViewModel.SearchedVideos = MainViewModel.AllVideos
    End Sub
End Class
