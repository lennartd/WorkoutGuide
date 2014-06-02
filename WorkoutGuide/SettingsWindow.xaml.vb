Public Class SettingsWindow

    Private Sub SettingsWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DataContext = MainViewModel
    End Sub
End Class
