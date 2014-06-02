Public Class WatchVideoWindow

    Private Sub WatchVideoWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DataContext = MainViewModel
    End Sub

    Private Sub WatchVideoWindow_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        WebBrowser.Source = Nothing
    End Sub
End Class
