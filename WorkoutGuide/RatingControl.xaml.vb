Imports System.Windows
Imports System.Windows.Controls

Public Class RatingControl
    Inherits UserControl

    Private Sub RatingButtonClickEventHandler1(sender As Object , e As RoutedEventArgs)
        RatingControl.Tag = 1
    End Sub

    Private Sub RatingButtonClickEventHandler2(sender As Object , e As RoutedEventArgs)
        RatingControl.Tag = 2
    End Sub

    Private Sub RatingButtonClickEventHandler3(sender As Object , e As RoutedEventArgs)
       RatingControl.Tag = 3
    End Sub

    Private Sub RatingButtonClickEventHandler4(sender As Object , e As RoutedEventArgs)
        RatingControl.Tag = 4
    End Sub

    Private Sub RatingButtonClickEventHandler5(sender As Object , e As RoutedEventArgs)
        RatingControl.Tag = 5
    End Sub
End Class

