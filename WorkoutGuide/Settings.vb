Imports System.ComponentModel
Imports ProtoBuf

<ProtoContract> _
Public Class Settings
    Implements INotifyPropertyChanged

    Public Sub New()
        _openVideoInBrowser = False
    End Sub

    Public Sub New(ByVal openVideoInBrowser As Boolean)
        _openVideoInBrowser = openVideoInBrowser      
    End Sub

    Private _openVideoInBrowser As Boolean
    <ProtoMember(1)> _
    Public Property SettingsOpenVideoInBrowser() As Boolean
        Get
            Return _openVideoInBrowser
        End Get
        Set(ByVal value As Boolean)
            _openVideoInBrowser = value
            RaiseProp("SettingsOpenVideoInBrowser")
        End Set
    End Property

    Public Sub RaiseProp(ByVal propertie As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertie))
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
End Class
