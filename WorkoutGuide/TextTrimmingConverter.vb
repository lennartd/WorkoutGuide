Public Class TextTrimmingConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If value Is Nothing
            Return Nothing
        End If
        Dim text As String = value.ToString()
        If text.Length > 157
            text = text.Remove(157)
            Return text & " ..."
        Else 
            Return text
        End If      
        
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return Nothing
    End Function
End Class