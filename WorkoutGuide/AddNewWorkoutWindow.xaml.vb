Public Class AddNewWorkoutWindow

    Private Sub AddNewWorkoutWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        AddNewWorkoutWindow.DataContext = MainViewModel
    End Sub
End Class
