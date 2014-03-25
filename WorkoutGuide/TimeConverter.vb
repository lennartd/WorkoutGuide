Public Class TimeConverter
    Implements IValueConverter
    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim time As Double = DirectCast(value, Integer) 'Total number of seconds
        Dim timeSpan As TimeSpan = timeSpan.FromSeconds(time)
        If timeSpan.Hours = 0 Then
            Return timeSpan.Minutes.ToString.PadLeft(2, "0"c) & ":" & timeSpan.Seconds.ToString.PadLeft(2, "0"c)
        Else
            Return timeSpan.Hours.ToString.PadLeft(2, "0"c) & ":" & timeSpan.Minutes.ToString.PadLeft(2, "0"c) & _
           +":" & timeSpan.Seconds.ToString.PadLeft(2, "0"c)
        End If

    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return Nothing
    End Function
End Class


