﻿Class MainWindow

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        MainWindow.DataContext = MainViewModel

        TreeViewCategories.MinHeight = TreeViewCategories.ActualHeight


        MainViewModel.SaveToXml()
    End Sub

End Class
