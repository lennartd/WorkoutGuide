Class MainWindow

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

       GridMain.DataContext = MainViewModel

        TreeViewCategories.MinHeight = TreeViewCategories.ActualHeight
    End Sub

    Private Sub ButtonVideoTitle_Click(sender As Object, e As RoutedEventArgs)

        Dim url As String = DirectCast(DirectCast(sender, Button).DataContext, Video).VideoUrl
        Process.Start(url)
    End Sub

    Private Sub ButtonVideoImage_Click(sender As Object, e As RoutedEventArgs)
        Dim url As String = DirectCast(DirectCast(sender, Button).DataContext, Video).VideoUrl
        Process.Start(url)

    End Sub

End Class
