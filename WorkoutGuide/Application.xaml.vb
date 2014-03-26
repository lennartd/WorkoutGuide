﻿Class Application

    ' Ereignisse auf Anwendungsebene wie Startup, Exit und DispatcherUnhandledException
    ' können in dieser Datei verarbeitet werden.

    Private Shared Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup

        MainViewModel = new ViewModel()

        MainViewModel.AllVideos = New VideosList()

        MainViewModel.AddVideo("https://www.youtube.com/watch?v=HOx8-HoT7hs", Today, 5, "mittel")
        MainViewModel.AddVideo("https://www.youtube.com/watch?v=ulsj7JiSqOk", Today, 4, "mittel")
        MainViewModel.AddVideo("https://www.youtube.com/watch?v=uzOe2ImO1rk&list=PLRCgg2aTq5NWNLW6o_NMUADMJ_P0r0aU9", Today, 0, "mittel")
        MainViewModel.AddVideo("https://www.youtube.com/watch?v=ClXs1Ef2SKo&list=PLRCgg2aTq5NWNLW6o_NMUADMJ_P0r0aU9", Today, 0, "mittel")
        MainViewModel.AddVideo("https://www.youtube.com/watch?v=s3zAG4zvVpc", Today, 2, "mittel")
    End Sub
End Class
