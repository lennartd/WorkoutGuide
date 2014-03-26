Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives

Public Class RatingControl
    Inherits UserControl

    'Public Shared ReadOnly RatingValueProperty As DependencyProperty = DependencyProperty.Register("RatingValue", GetType(Integer), GetType(RatingControl), New FrameworkPropertyMetadata(Nothing, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, New PropertyChangedCallback(AddressOf RatingValueChanged)))

    'Private _maxValue As Integer = 5


    'Public Property RatingValue() As Integer
    '    Get
    '        Return CInt(GetValue(RatingValueProperty))
    '    End Get
    '    Set(value As Integer)
    '        If value < 0 Then
    '            SetValue(RatingValueProperty, 0)
    '        ElseIf value > _maxValue Then
    '            SetValue(RatingValueProperty, _maxValue)
    '        Else
    '            SetValue(RatingValueProperty, value)
    '        End If
    '    End Set
    'End Property

    'Private Shared Sub RatingValueChanged(sender As DependencyObject, e As DependencyPropertyChangedEventArgs)
    '    Dim parent As RatingControl = TryCast(sender, RatingControl)
    '    Dim ratingValue As Integer = CInt(e.NewValue)
    '    Dim children As UIElementCollection = DirectCast(parent.Content, Grid).Children
    '    Dim button As ToggleButton = Nothing

    '    For i As Integer = 0 To ratingValue - 1
    '        button = TryCast(children(i), ToggleButton)
    '        If button IsNot Nothing Then
    '            button.IsChecked = True
    '        End If
    '    Next

    '    For i As Integer = ratingValue To children.Count - 1
    '        button = TryCast(children(i), ToggleButton)
    '        If button IsNot Nothing Then
    '            button.IsChecked = False
    '        End If
    '    Next

    'End Sub

    Private Sub RatingButtonClickEventHandler(sender As Object, e As RoutedEventArgs)

        'Dim button As ToggleButton = TryCast(sender, ToggleButton)

        'Dim newRating As Integer = Integer.Parse(DirectCast(button.Tag, [String]))

        'If CBool(button.IsChecked) OrElse newRating < RatingValue Then
        '    RatingValue = newRating
        'Else
        '    RatingValue = newRating - 1
        'End If

        'e.Handled = True
    End Sub
End Class

