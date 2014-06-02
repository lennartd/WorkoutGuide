Public Class ReverseBooleanConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim bool As Boolean = DirectCast(value, Boolean)
        If bool = True
            Return False
        End If
        Return True
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Dim bool As Boolean = DirectCast(value, Boolean)
        If bool = True
            Return False
        End If
        Return True
    End Function
End Class
