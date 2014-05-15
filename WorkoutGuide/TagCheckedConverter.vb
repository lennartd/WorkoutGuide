Public Class TagCheckedConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim rating As Integer = CInt(value)
        Dim button As Integer = CInt(parameter)
        Select Case rating
            Case 0
                Return False
            Case 1
                If button = 1
                    Return True
                Else
                    Return False
                End If
            Case 2
                If button = 1 Or button = 2
                    Return True
                Else
                    Return False
                End If
            Case 3
                If button = 1 Or button = 2 Or button = 3
                    Return True
                Else
                    Return False
                End If
            Case 4
                If button = 1 Or button = 2 Or button = 3 Or button = 4
                    Return True
                Else
                    Return False
                End If
            Case 5
                If button = 1 Or button = 2 Or button = 3 Or button = 4 Or button = 5
                    Return True
                Else
                    Return False
                End If
        End Select
        Return Nothing
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return Nothing
    End Function
End Class