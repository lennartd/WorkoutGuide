Public Class WebBrowserUtility
    Private Sub New()
    End Sub

    Public Shared ReadOnly BindableSourceProperty As DependencyProperty = DependencyProperty.RegisterAttached("BindableSource", GetType(String), GetType(WebBrowserUtility), New UIPropertyMetadata(Nothing, AddressOf BindableSourcePropertyChanged))
    Public Shared Function GetBindableSource(obj As DependencyObject) As String
        Return DirectCast(obj.GetValue(BindableSourceProperty), String)
    End Function

    Public Shared Sub SetBindableSource(obj As DependencyObject, value As String)
        obj.SetValue(BindableSourceProperty, value)
    End Sub

    Public Shared Sub BindableSourcePropertyChanged(o As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim browser As WebBrowser = TryCast(o, WebBrowser)
        If browser IsNot Nothing Then
            Dim uri As String = TryCast(e.NewValue, String)
            browser.Source = If(uri IsNot Nothing, New Uri(uri), Nothing)
        End If
    End Sub
End Class
