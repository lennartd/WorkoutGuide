Imports System.ComponentModel

Class MainWindow

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        MainWindow.DataContext = MainViewModel

        TreeViewCategories.MinHeight = TreeViewCategories.ActualHeight

        Sort()
    End Sub

    Private Sub SortSearchedVideos(sender As Object , e As RoutedEventArgs)
        Sort()
    End Sub

    Private Sub Sort()
        If Not SearchedVideosItemsControl Is Nothing
            Dim sortCollectionView As CollectionViewSource = CType(FindResource("SortSearchedVideos"), CollectionViewSource)
            If sortCollectionView IsNot Nothing Then
                sortCollectionView.SortDescriptions.Clear()
                Select Case SortValuesComboBox.SelectedIndex
                    Case 0 'Titel
                        If SortUpDownToggleButton.IsChecked = True 'Up
                            sortCollectionView.SortDescriptions.Add(New SortDescription("VideoTitle", ListSortDirection.Ascending))
                        Else 'Down
                            sortCollectionView.SortDescriptions.Add(New SortDescription("VideoTitle", ListSortDirection.Descending))
                        End If
                    Case 1 'Rating
                        If SortUpDownToggleButton.IsChecked = True 'Up
                            sortCollectionView.SortDescriptions.Add(New SortDescription("VideoRating", ListSortDirection.Ascending))
                        Else 'Down
                            sortCollectionView.SortDescriptions.Add(New SortDescription("VideoRating", ListSortDirection.Descending))
                        End If
                    Case 2 'Duration
                        If SortUpDownToggleButton.IsChecked = True 'Up
                            sortCollectionView.SortDescriptions.Add(New SortDescription("VideoDuration", ListSortDirection.Ascending))
                        Else 'Down
                            sortCollectionView.SortDescriptions.Add(New SortDescription("VideoDuration", ListSortDirection.Descending))
                        End If
                    Case 3 'DateAdded
                        If SortUpDownToggleButton.IsChecked = True 'Up
                            sortCollectionView.SortDescriptions.Add(New SortDescription("VideoDateAdded", ListSortDirection.Ascending))
                        Else 'Down
                            sortCollectionView.SortDescriptions.Add(New SortDescription("VideoDateAdded", ListSortDirection.Descending))
                        End If
                End Select
                sortCollectionView.SortDescriptions.Add(New SortDescription("VideoTitle", ListSortDirection.Ascending))
            End If
        End If
    End Sub

End Class
